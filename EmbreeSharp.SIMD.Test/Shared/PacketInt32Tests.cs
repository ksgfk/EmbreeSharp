using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class PacketInt32Tests<TPacket, TMask>
    where TPacket : unmanaged, ISimdInteger<TPacket, int, TMask>
    where TMask : unmanaged, ISimdPacketMask<TMask, int>
{
    protected abstract bool IsHardwareSupported { get; }
    protected abstract uint AlignmentMask { get; }

    [TestMethod]
    public void TestArithmeticAndMasks()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        int[] leftData = PadToMax([1, -2, 3, -4, 5, -6, 7, -8], n);
        int[] absExpected = PadToMax([1, 2, 3, 4, 5, 6, 7, 8], n);
        int[] addExpected = PadToMax([3, 0, 5, -2, 7, -4, 9, -6], n);
        int[] subExpected = PadToMax([-1, -4, 1, -6, 3, -8, 5, -10], n);
        int[] mulExpected = PadToMax([2, -4, 6, -8, 10, -12, 14, -16], n);
        bool[] positiveMask = PadToMax([true, false, true, false, true, false, true, false], n);
        int[] selectExpected = PadToMax([1, 2, 3, 2, 5, 2, 7, 2], n);

        TPacket left = TPacket.Load(leftData);
        TPacket right = TPacket.Broadcast(2);

        AssertPacket(TPacket.Abs(left), absExpected);
        AssertPacket(left + right, addExpected);
        AssertPacket(left - right, subExpected);
        AssertPacket(left * right, mulExpected);

        TMask positive = left > TPacket.Broadcast(0);
        AssertMask(positive, positiveMask);
        AssertPacket(TPacket.Select(positive, left, right), selectExpected);
    }

    [TestMethod]
    public void TestBitwiseAndCounts()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        int[] leftData = PadToMax([1, 2, 4, 8, 16, 32, 64, 128], n);
        int[] rightData = PadToMax([3, 3, 12, 12, 48, 48, 192, 192], n);
        int[] andExpected = PadToMax([1, 2, 4, 8, 16, 32, 64, 128], n);
        int[] orExpected = PadToMax([3, 3, 12, 12, 48, 48, 192, 192], n);
        int[] xorExpected = PadToMax([2, 1, 8, 4, 32, 16, 128, 64], n);
        int[] notExpected = PadToMax([-2, -3, -5, -9, -17, -33, -65, -129], n);

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
        int[] source = new int[n + 1];
        for (int i = 0; i < source.Length; i++)
        {
            source[i] = (i & 1) == 0 ? -1 : 0;
        }
        int[] destination = new int[n + 1];

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

        TMask leftMask = TMask.Load(PadToMax([true, true, false, false, false, false, false, false], n));
        TMask rightMask = TMask.Load(PadToMax([false, true, false, true, false, false, false, false], n));
        bool[] andNotExpected = PadToMax([true, false, false, false, false, false, false, false], n);
        AssertMask(TMask.AndNot(leftMask, rightMask), andNotExpected);

        int[] alignedSource = new int[n * 2];
        int[] alignedDestination = new int[n * 2];
        for (int i = 0; i < alignedSource.Length; i++)
        {
            alignedSource[i] = (i & 1) == 0 ? -1 : 0;
        }

        unsafe
        {
            fixed (int* sourcePtr = alignedSource)
            fixed (int* destinationPtr = alignedDestination)
            {
                int sourceOffset = TestHelpers.AlignedOffset<int>(sourcePtr, AlignmentMask, n);
                int destinationOffset = TestHelpers.AlignedOffset<int>(destinationPtr, AlignmentMask, n);

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
        int[] source = new int[n * 2];
        int[] destination = new int[n * 2];

        for (int i = 0; i < source.Length; i++)
        {
            source[i] = i + 1;
        }

        fixed (int* sourcePtr = source)
        fixed (int* destinationPtr = destination)
        {
            int sourceOffset = TestHelpers.AlignedOffset<int>(sourcePtr, AlignmentMask, n);
            int destinationOffset = TestHelpers.AlignedOffset<int>(destinationPtr, AlignmentMask, n);

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
        TestHelpers.ExpectArgumentException(() => TPacket.Load(new int[n - 1]));
        TestHelpers.ExpectArgumentException(() => TPacket.LoadAligned(new int[n - 1]));

        TPacket value = default;
        TestHelpers.ExpectArgumentException(() => TPacket.Store(value, new int[n - 1]));
        TestHelpers.ExpectArgumentException(() => TPacket.StoreAligned(value, new int[n - 1]));

        TestHelpers.ExpectArgumentException(() => TMask.Load(new int[n - 1]));
        TestHelpers.ExpectArgumentException(() => TMask.Load(new bool[n - 1]));
        TestHelpers.ExpectArgumentException(() => TMask.LoadAligned(new int[n - 1]));

        TMask mask = default;
        TestHelpers.ExpectArgumentException(() => TMask.Store(mask, new int[n - 1]));
        TestHelpers.ExpectArgumentException(() => TMask.Store(mask, new bool[n - 1]));
        TestHelpers.ExpectArgumentException(() => TMask.StoreAligned(mask, new int[n - 1]));
    }

    protected static int[] PadToMax(int[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        int[] result = new int[count];
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

    protected static void AssertPacket(TPacket actual, int[] expected)
    {
        int n = TPacket.LaneCount;
        Span<int> values = stackalloc int[n];
        TPacket.Store(actual, values);

        for (int i = 0; i < expected.Length; i++)
        {
            Assert.AreEqual(expected[i], values[i], $"lane {i}");
        }
    }

    protected static void AssertMask(TMask actual, bool[] expected)
    {
        int n = TPacket.LaneCount;
        Span<int> values = stackalloc int[n];
        TMask.Store(actual, values);

        for (int i = 0; i < expected.Length; i++)
        {
            Assert.AreEqual(expected[i], values[i] != 0, $"lane {i}");
        }
    }
}
