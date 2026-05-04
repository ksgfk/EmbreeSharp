using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class PacketInt64Tests<TPacket, TMask>
    where TPacket : unmanaged, ISimdInteger<TPacket, long, TMask>
    where TMask : unmanaged, ISimdPacketMask<TMask, long>
{
    protected abstract bool IsHardwareSupported { get; }
    protected abstract uint AlignmentMask { get; }

    [TestMethod]
    public void TestArithmeticAndMasks()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        long[] leftData = PadToMax([1L, -2L, 3L, -4L], n);
        long[] absExpected = PadToMax([1L, 2L, 3L, 4L], n);
        long[] addExpected = PadToMax([3L, 0L, 5L, -2L], n);
        long[] subExpected = PadToMax([-1L, -4L, 1L, -6L], n);
        long[] mulExpected = PadToMax([2L, -4L, 6L, -8L], n);
        bool[] positiveMask = PadToMax([true, false, true, false], n);
        long[] selectExpected = PadToMax([1L, 2L, 3L, 2L], n);

        TPacket left = TPacket.Load(leftData);
        TPacket right = TPacket.Broadcast(2L);

        AssertPacket(TPacket.Abs(left), absExpected);
        AssertPacket(left + right, addExpected);
        AssertPacket(left - right, subExpected);
        AssertPacket(left * right, mulExpected);

        TMask positive = left > TPacket.Broadcast(0L);
        AssertMask(positive, positiveMask);
        AssertPacket(TPacket.Select(positive, left, right), selectExpected);
    }

    [TestMethod]
    public void TestLargeMultiplyAndOrdering()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        long[] leftLarge = PadToMax([1L << 40, -(1L << 40), 3000000000L, -3000000000L], n);
        long[] rightLarge = PadToMax([3L, 3L, 4L, -4L], n);
        long[] largeMulExpected = PadToMax([3298534883328L, -3298534883328L, 12000000000L, 12000000000L], n);

        TPacket leftLargeP = TPacket.Load(leftLarge);
        TPacket rightLargeP = TPacket.Load(rightLarge);

        AssertPacket(leftLargeP * rightLargeP, largeMulExpected);

        long[] minLeftData = PadToMax([long.MinValue, -1L, 0L, long.MaxValue], n);
        long[] minRightData = PadToMax([0L, -2L, 1L, long.MaxValue], n);
        long[] minExpected = PadToMax([long.MinValue, -2L, 0L, long.MaxValue], n);
        long[] maxExpected = PadToMax([0L, -1L, 1L, long.MaxValue], n);

        TPacket minLeft = TPacket.Load(minLeftData);
        TPacket minRight = TPacket.Load(minRightData);

        AssertPacket(TPacket.Min(minLeft, minRight), minExpected);
        AssertPacket(TPacket.Max(minLeft, minRight), maxExpected);
    }

    [TestMethod]
    public void TestBitwiseAndCounts()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        long[] leftData = PadToMax([1L, 2L, 4L, 8L], n);
        long[] rightData = PadToMax([3L, 3L, 12L, 12L], n);
        long[] andExpected = PadToMax([1L, 2L, 4L, 8L], n);
        long[] orExpected = PadToMax([3L, 3L, 12L, 12L], n);
        long[] xorExpected = PadToMax([2L, 1L, 8L, 4L], n);
        long[] notExpected = PadToMax([-2L, -3L, -5L, -9L], n);

        TPacket left = TPacket.Load(leftData);
        TPacket right = TPacket.Load(rightData);

        AssertPacket(left & right, andExpected);
        AssertPacket(left | right, orExpected);
        AssertPacket(left ^ right, xorExpected);
        AssertPacket(~left, notExpected);
    }

    [TestMethod]
    public void TestMaskLoadStore()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        long[] source = new long[n + 1];
        for (int i = 0; i < source.Length; i++)
        {
            source[i] = (i & 1) == 0 ? -1L : 0L;
        }
        long[] destination = new long[n + 1];

        TMask mask = TMask.Load(source);
        TMask.Store(mask, destination);

        for (int i = 0; i < n; i++)
        {
            Assert.AreEqual(source[i], destination[i], $"lane {i}");
        }

        bool[] boolSource = new bool[n + 1];
        for (int i = 0; i < boolSource.Length; i++)
        {
            boolSource[i] = (i & 1) == 0;
        }
        bool[] boolDestination = new bool[n + 1];

        TMask boolMask = TMask.Load(boolSource);
        TMask.Store(boolMask, boolDestination);

        for (int i = 0; i < n; i++)
        {
            Assert.AreEqual(boolSource[i], boolDestination[i], $"bool lane {i}");
        }

        TMask leftMask = TMask.Load(PadToMax([true, true, false, false], n));
        TMask rightMask = TMask.Load(PadToMax([false, true, false, true], n));
        bool[] andNotExpected = PadToMax([true, false, false, false], n);
        AssertMask(TMask.AndNot(leftMask, rightMask), andNotExpected);

        long[] alignedSource = new long[n * 2];
        long[] alignedDestination = new long[n * 2];
        for (int i = 0; i < alignedSource.Length; i++)
        {
            alignedSource[i] = (i & 1) == 0 ? -1L : 0L;
        }

        unsafe
        {
            fixed (long* sourcePtr = alignedSource)
            fixed (long* destinationPtr = alignedDestination)
            {
                int sourceOffset = TestHelpers.AlignedOffset<long>(sourcePtr, AlignmentMask, n);
                int destinationOffset = TestHelpers.AlignedOffset<long>(destinationPtr, AlignmentMask, n);

                TMask alignedMask = TMask.LoadAligned(alignedSource.AsSpan(sourceOffset, n));
                TMask.StoreAligned(alignedMask, alignedDestination.AsSpan(destinationOffset, n));

                for (int i = 0; i < n; i++)
                {
                    Assert.AreEqual(alignedSource[sourceOffset + i], alignedDestination[destinationOffset + i], $"aligned lane {i}");
                }
            }
        }
    }

    [TestMethod]
    public unsafe void TestAlignedLoadStore()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        long[] source = new long[n * 2];
        long[] destination = new long[n * 2];

        for (int i = 0; i < source.Length; i++)
        {
            source[i] = i + 1;
        }

        fixed (long* sourcePtr = source)
        fixed (long* destinationPtr = destination)
        {
            int sourceOffset = TestHelpers.AlignedOffset<long>(sourcePtr, AlignmentMask, n);
            int destinationOffset = TestHelpers.AlignedOffset<long>(destinationPtr, AlignmentMask, n);

            TPacket value = TPacket.LoadAligned(source.AsSpan(sourceOffset, n));
            TPacket.StoreAligned(value, destination.AsSpan(destinationOffset, n));

            for (int i = 0; i < n; i++)
            {
                Assert.AreEqual(source[sourceOffset + i], destination[destinationOffset + i], $"lane {i}");
            }
        }
    }

    [TestMethod]
    public void TestLoadStoreRejectShortSpans()
    {
        int n = TPacket.LaneCount;
        TestHelpers.ExpectArgumentException(() => TPacket.Load(new long[n - 1]));
        TestHelpers.ExpectArgumentException(() => TPacket.LoadAligned(new long[n - 1]));

        TPacket value = default;
        TestHelpers.ExpectArgumentException(() => TPacket.Store(value, new long[n - 1]));
        TestHelpers.ExpectArgumentException(() => TPacket.StoreAligned(value, new long[n - 1]));

        TestHelpers.ExpectArgumentException(() => TMask.Load(new long[n - 1]));
        TestHelpers.ExpectArgumentException(() => TMask.Load(new bool[n - 1]));
        TestHelpers.ExpectArgumentException(() => TMask.LoadAligned(new long[n - 1]));

        TMask mask = default;
        TestHelpers.ExpectArgumentException(() => TMask.Store(mask, new long[n - 1]));
        TestHelpers.ExpectArgumentException(() => TMask.Store(mask, new bool[n - 1]));
        TestHelpers.ExpectArgumentException(() => TMask.StoreAligned(mask, new long[n - 1]));
    }

    protected static long[] PadToMax(long[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        long[] result = new long[count];
        values.CopyTo(result, 0);
        return result;
    }

    protected static bool[] PadToMax(bool[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        bool[] result = new bool[count];
        values.CopyTo(result, 0);
        return result;
    }

    protected static void AssertPacket(TPacket actual, long[] expected)
    {
        int n = TPacket.LaneCount;
        Span<long> values = stackalloc long[n];
        TPacket.Store(actual, values);

        for (int i = 0; i < expected.Length; i++)
        {
            Assert.AreEqual(expected[i], values[i], $"lane {i}");
        }
    }

    protected static void AssertMask(TMask actual, bool[] expected)
    {
        int n = TPacket.LaneCount;
        Span<long> values = stackalloc long[n];
        TMask.Store(actual, values);

        for (int i = 0; i < expected.Length; i++)
        {
            Assert.AreEqual(expected[i], values[i] != 0, $"lane {i}");
        }
    }
}
