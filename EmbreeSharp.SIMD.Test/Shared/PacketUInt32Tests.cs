using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class PacketUInt32Tests<TPacket, TMask>
    where TPacket : unmanaged, ISimdInteger<TPacket, uint, TMask>
    where TMask : unmanaged, ISimdPacketMask<TMask, uint>
{
    protected abstract bool IsHardwareSupported { get; }
    protected abstract uint AlignmentMask { get; }

    [TestMethod]
    public void TestArithmeticAndMasks()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        uint[] leftData = PadToMax([1u, 2u, 3u, 4u, 5u, 6u, 7u, 8u], n);
        uint[] absExpected = PadToMax([1u, 2u, 3u, 4u, 5u, 6u, 7u, 8u], n);
        uint[] addExpected = PadToMax([3u, 4u, 5u, 6u, 7u, 8u, 9u, 10u], n);
        uint[] subExpected = PadToMax([uint.MaxValue, 0u, 1u, 2u, 3u, 4u, 5u, 6u], n);
        uint[] mulExpected = PadToMax([2u, 4u, 6u, 8u, 10u, 12u, 14u, 16u], n);

        TPacket left = TPacket.Load(leftData);
        TPacket right = TPacket.Broadcast(2u);

        AssertPacket(TPacket.Abs(left), absExpected);
        AssertPacket(left + right, addExpected);
        AssertPacket(left - right, subExpected);
        AssertPacket(left * right, mulExpected);

        uint[] valuesData = PadToMax([0u, 1u, 0x7FFFFFFFu, 0x80000000u, uint.MaxValue, 42u, 4000000000u, 3000000000u], n);
        bool[] aboveMask = PadToMax([false, false, false, false, true, false, true, true], n);
        uint[] selectExpected = PadToMax([99u, 99u, 99u, 99u, uint.MaxValue, 99u, 4000000000u, 3000000000u], n);

        TPacket values = TPacket.Load(valuesData);
        TMask above = values > TPacket.Broadcast(0x80000000u);
        AssertMask(above, aboveMask);
        AssertPacket(TPacket.Select(above, values, TPacket.Broadcast(99u)), selectExpected);
    }

    [TestMethod]
    public void TestBitwiseAndUnsignedOrdering()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        uint[] leftData = PadToMax([1u, 2u, 4u, 8u, 16u, 32u, 64u, 128u], n);
        uint[] rightData = PadToMax([3u, 3u, 12u, 12u, 48u, 48u, 192u, 192u], n);
        uint[] andExpected = PadToMax([1u, 2u, 4u, 8u, 16u, 32u, 64u, 128u], n);
        uint[] orExpected = PadToMax([3u, 3u, 12u, 12u, 48u, 48u, 192u, 192u], n);
        uint[] xorExpected = PadToMax([2u, 1u, 8u, 4u, 32u, 16u, 128u, 64u], n);
        uint[] notExpected = PadToMax([
            uint.MaxValue - 1u, uint.MaxValue - 2u, uint.MaxValue - 4u, uint.MaxValue - 8u,
            uint.MaxValue - 16u, uint.MaxValue - 32u, uint.MaxValue - 64u, uint.MaxValue - 128u
        ], n);

        TPacket left = TPacket.Load(leftData);
        TPacket right = TPacket.Load(rightData);

        AssertPacket(left & right, andExpected);
        AssertPacket(left | right, orExpected);
        AssertPacket(left ^ right, xorExpected);
        AssertPacket(~left, notExpected);

        uint[] minLeftData = PadToMax([0u, uint.MaxValue, 0x80000000u, 7u, 100u, 3000000000u, 42u, 9u], n);
        uint[] minRightData = PadToMax([1u, 1u, 0x7FFFFFFFu, 8u, 99u, 4000000000u, 42u, 10u], n);
        uint[] minExpected = PadToMax([0u, 1u, 0x7FFFFFFFu, 7u, 99u, 3000000000u, 42u, 9u], n);
        uint[] maxExpected = PadToMax([1u, uint.MaxValue, 0x80000000u, 8u, 100u, 4000000000u, 42u, 10u], n);

        TPacket minLeft = TPacket.Load(minLeftData);
        TPacket minRight = TPacket.Load(minRightData);

        AssertPacket(TPacket.Min(minLeft, minRight), minExpected);
        AssertPacket(TPacket.Max(minLeft, minRight), maxExpected);
    }

    [TestMethod]
    public void TestMaskLoadStore()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        uint maxValue = uint.MaxValue;
        uint[] source = new uint[n + 1];
        for (int i = 0; i < source.Length; i++)
        {
            source[i] = (i & 1) == 0 ? maxValue : 0u;
        }
        uint[] destination = new uint[n + 1];

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

        uint[] alignedSource = new uint[n * 2];
        uint[] alignedDestination = new uint[n * 2];
        for (int i = 0; i < alignedSource.Length; i++)
        {
            alignedSource[i] = (i & 1) == 0 ? maxValue : 0u;
        }

        unsafe
        {
            fixed (uint* sourcePtr = alignedSource)
            fixed (uint* destinationPtr = alignedDestination)
            {
                int sourceOffset = TestHelpers.AlignedOffset<uint>(sourcePtr, AlignmentMask, n);
                int destinationOffset = TestHelpers.AlignedOffset<uint>(destinationPtr, AlignmentMask, n);

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
        uint[] source = new uint[n * 2];
        uint[] destination = new uint[n * 2];

        for (int i = 0; i < source.Length; i++)
        {
            source[i] = (uint)(i + 1);
        }

        fixed (uint* sourcePtr = source)
        fixed (uint* destinationPtr = destination)
        {
            int sourceOffset = TestHelpers.AlignedOffset<uint>(sourcePtr, AlignmentMask, n);
            int destinationOffset = TestHelpers.AlignedOffset<uint>(destinationPtr, AlignmentMask, n);

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
        TestHelpers.ExpectArgumentException(() => TPacket.Load(new uint[n - 1]));
        TestHelpers.ExpectArgumentException(() => TPacket.LoadAligned(new uint[n - 1]));

        TPacket value = default;
        TestHelpers.ExpectArgumentException(() => TPacket.Store(value, new uint[n - 1]));
        TestHelpers.ExpectArgumentException(() => TPacket.StoreAligned(value, new uint[n - 1]));

        TestHelpers.ExpectArgumentException(() => TMask.Load(new uint[n - 1]));
        TestHelpers.ExpectArgumentException(() => TMask.Load(new bool[n - 1]));
        TestHelpers.ExpectArgumentException(() => TMask.LoadAligned(new uint[n - 1]));

        TMask mask = default;
        TestHelpers.ExpectArgumentException(() => TMask.Store(mask, new uint[n - 1]));
        TestHelpers.ExpectArgumentException(() => TMask.Store(mask, new bool[n - 1]));
        TestHelpers.ExpectArgumentException(() => TMask.StoreAligned(mask, new uint[n - 1]));
    }

    protected static uint[] PadToMax(uint[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        uint[] result = new uint[count];
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

    protected static void AssertPacket(TPacket actual, uint[] expected)
    {
        int n = TPacket.LaneCount;
        Span<uint> values = stackalloc uint[n];
        TPacket.Store(actual, values);

        for (int i = 0; i < expected.Length; i++)
        {
            Assert.AreEqual(expected[i], values[i], $"lane {i}");
        }
    }

    protected static void AssertMask(TMask actual, bool[] expected)
    {
        int n = TPacket.LaneCount;
        Span<uint> values = stackalloc uint[n];
        TMask.Store(actual, values);

        for (int i = 0; i < expected.Length; i++)
        {
            Assert.AreEqual(expected[i], values[i] != 0, $"lane {i}");
        }
    }
}
