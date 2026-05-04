using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class PacketUInt64Tests<TPacket, TMask>
    where TPacket : unmanaged, ISimdInteger<TPacket, ulong, TMask>
    where TMask : unmanaged, ISimdPacketMask<TMask, ulong>
{
    protected abstract bool IsHardwareSupported { get; }
    protected abstract uint AlignmentMask { get; }

    [TestMethod]
    public void TestArithmeticAndMasks()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        ulong[] leftData = PadToMax([1ul, 2ul, 3ul, 4ul], n);
        ulong[] absExpected = PadToMax([1ul, 2ul, 3ul, 4ul], n);
        ulong[] addExpected = PadToMax([3ul, 4ul, 5ul, 6ul], n);
        ulong[] subExpected = PadToMax([ulong.MaxValue, 0ul, 1ul, 2ul], n);
        ulong[] mulExpected = PadToMax([2ul, 4ul, 6ul, 8ul], n);

        TPacket left = TPacket.Load(leftData);
        TPacket right = TPacket.Broadcast(2ul);

        AssertPacket(TPacket.Abs(left), absExpected);
        AssertPacket(left + right, addExpected);
        AssertPacket(left - right, subExpected);
        AssertPacket(left * right, mulExpected);

        ulong[] valuesData = PadToMax([0ul, 1ul, 0x7FFFFFFFFFFFFFFFul, ulong.MaxValue], n);
        bool[] aboveMask = PadToMax([false, false, false, true], n);
        ulong[] selectExpected = PadToMax([99ul, 99ul, 99ul, ulong.MaxValue], n);

        TPacket values = TPacket.Load(valuesData);
        TMask above = values > TPacket.Broadcast(0x8000000000000000ul);
        AssertMask(above, aboveMask);
        AssertPacket(TPacket.Select(above, values, TPacket.Broadcast(99ul)), selectExpected);
    }

    [TestMethod]
    public void TestLargeMultiplyAndUnsignedOrdering()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        ulong[] leftLarge = PadToMax([1ul << 40, 3000000000ul, ulong.MaxValue, 0x8000000000000000ul], n);
        ulong[] rightLarge = PadToMax([3ul, 4ul, 2ul, 2ul], n);
        ulong[] largeMulExpected = PadToMax([3298534883328ul, 12000000000ul, ulong.MaxValue - 1ul, 0ul], n);

        TPacket leftLargeP = TPacket.Load(leftLarge);
        TPacket rightLargeP = TPacket.Load(rightLarge);

        AssertPacket(leftLargeP * rightLargeP, largeMulExpected);

        ulong[] minLeftData = PadToMax([0ul, ulong.MaxValue, 0x8000000000000000ul, 7ul], n);
        ulong[] minRightData = PadToMax([1ul, 1ul, 0x7FFFFFFFFFFFFFFFul, 8ul], n);
        ulong[] minExpected = PadToMax([0ul, 1ul, 0x7FFFFFFFFFFFFFFFul, 7ul], n);
        ulong[] maxExpected = PadToMax([1ul, ulong.MaxValue, 0x8000000000000000ul, 8ul], n);

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
        ulong[] leftData = PadToMax([1ul, 2ul, 4ul, 8ul], n);
        ulong[] rightData = PadToMax([3ul, 3ul, 12ul, 12ul], n);
        ulong[] andExpected = PadToMax([1ul, 2ul, 4ul, 8ul], n);
        ulong[] orExpected = PadToMax([3ul, 3ul, 12ul, 12ul], n);
        ulong[] xorExpected = PadToMax([2ul, 1ul, 8ul, 4ul], n);
        ulong[] notExpected = PadToMax([
            ulong.MaxValue - 1ul, ulong.MaxValue - 2ul, ulong.MaxValue - 4ul, ulong.MaxValue - 8ul
        ], n);

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
        ulong maxValue = ulong.MaxValue;
        ulong[] source = new ulong[n + 1];
        for (int i = 0; i < source.Length; i++)
        {
            source[i] = (i & 1) == 0 ? maxValue : 0ul;
        }
        ulong[] destination = new ulong[n + 1];

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

        ulong[] alignedSource = new ulong[n * 2];
        ulong[] alignedDestination = new ulong[n * 2];
        for (int i = 0; i < alignedSource.Length; i++)
        {
            alignedSource[i] = (i & 1) == 0 ? maxValue : 0ul;
        }

        unsafe
        {
            fixed (ulong* sourcePtr = alignedSource)
            fixed (ulong* destinationPtr = alignedDestination)
            {
                int sourceOffset = TestHelpers.AlignedOffset<ulong>(sourcePtr, AlignmentMask, n);
                int destinationOffset = TestHelpers.AlignedOffset<ulong>(destinationPtr, AlignmentMask, n);

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
        ulong[] source = new ulong[n * 2];
        ulong[] destination = new ulong[n * 2];

        for (int i = 0; i < source.Length; i++)
        {
            source[i] = (ulong)(i + 1);
        }

        fixed (ulong* sourcePtr = source)
        fixed (ulong* destinationPtr = destination)
        {
            int sourceOffset = TestHelpers.AlignedOffset<ulong>(sourcePtr, AlignmentMask, n);
            int destinationOffset = TestHelpers.AlignedOffset<ulong>(destinationPtr, AlignmentMask, n);

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
        TestHelpers.ExpectArgumentException(() => TPacket.Load(new ulong[n - 1]));
        TestHelpers.ExpectArgumentException(() => TPacket.LoadAligned(new ulong[n - 1]));

        TPacket value = default;
        TestHelpers.ExpectArgumentException(() => TPacket.Store(value, new ulong[n - 1]));
        TestHelpers.ExpectArgumentException(() => TPacket.StoreAligned(value, new ulong[n - 1]));

        TestHelpers.ExpectArgumentException(() => TMask.Load(new ulong[n - 1]));
        TestHelpers.ExpectArgumentException(() => TMask.Load(new bool[n - 1]));
        TestHelpers.ExpectArgumentException(() => TMask.LoadAligned(new ulong[n - 1]));

        TMask mask = default;
        TestHelpers.ExpectArgumentException(() => TMask.Store(mask, new ulong[n - 1]));
        TestHelpers.ExpectArgumentException(() => TMask.Store(mask, new bool[n - 1]));
        TestHelpers.ExpectArgumentException(() => TMask.StoreAligned(mask, new ulong[n - 1]));
    }

    protected static ulong[] PadToMax(ulong[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        ulong[] result = new ulong[count];
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

    protected static void AssertPacket(TPacket actual, ulong[] expected)
    {
        int n = TPacket.LaneCount;
        Span<ulong> values = stackalloc ulong[n];
        TPacket.Store(actual, values);

        for (int i = 0; i < expected.Length; i++)
        {
            Assert.AreEqual(expected[i], values[i], $"lane {i}");
        }
    }

    protected static void AssertMask(TMask actual, bool[] expected)
    {
        int n = TPacket.LaneCount;
        Span<ulong> values = stackalloc ulong[n];
        TMask.Store(actual, values);

        for (int i = 0; i < expected.Length; i++)
        {
            Assert.AreEqual(expected[i], values[i] != 0, $"lane {i}");
        }
    }
}
