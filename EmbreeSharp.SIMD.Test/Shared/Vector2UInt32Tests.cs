using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class Vector2UInt32Tests<TVector, TPacket, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdIntegerVector2<TVector, TPacket, uint, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdInteger<TPacket, uint, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector2Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, uint>
{
    protected abstract bool IsHardwareSupported { get; }
    protected abstract uint AlignmentMask { get; }

    [TestMethod]
    public void TestArithmetic()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        uint[] leftX = PadToMax([1u, 2u, 3u, 4u, 5u, 6u, 7u, 8u], n);
        uint[] leftY = PadToMax([2u, 3u, 4u, 5u, 6u, 7u, 8u, 9u], n);
        uint[] rightX = PadToMax([2u, 2u, 2u, 2u, 2u, 2u, 2u, 2u], n);
        uint[] rightY = PadToMax([1u, 1u, 1u, 1u, 1u, 1u, 1u, 1u], n);

        uint[] addX = PadToMax([3u, 4u, 5u, 6u, 7u, 8u, 9u, 10u], n);
        uint[] addY = PadToMax([3u, 4u, 5u, 6u, 7u, 8u, 9u, 10u], n);
        uint[] subX = PadToMax([uint.MaxValue, 0u, 1u, 2u, 3u, 4u, 5u, 6u], n);
        uint[] subY = PadToMax([1u, 2u, 3u, 4u, 5u, 6u, 7u, 8u], n);
        uint[] mulX = PadToMax([2u, 4u, 6u, 8u, 10u, 12u, 14u, 16u], n);
        uint[] mulY = PadToMax([2u, 3u, 4u, 5u, 6u, 7u, 8u, 9u], n);

        TVector left = TVector.Create(TPacket.Load(leftX), TPacket.Load(leftY));
        TVector right = TVector.Create(TPacket.Load(rightX), TPacket.Load(rightY));

        AssertVector(left + right, addX, addY);
        AssertVector(left - right, subX, subY);
        AssertVector(left * right, mulX, mulY);

        uint[] broadcastX = PadToMax([5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u], n);
        uint[] broadcastY = PadToMax([5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u], n);
        TVector broadcast = TVector.Broadcast(5u);
        AssertVector(broadcast, broadcastX, broadcastY);

        uint[] selectX = PadToMax([1u, 2u, 3u, 2u, 5u, 2u, 7u, 2u], n);
        uint[] selectY = PadToMax([2u, 1u, 4u, 1u, 6u, 1u, 8u, 1u], n);
        bool[] alternateLanes = PadToMaxBool([true, false, true, false, true, false, true, false], n);
        TPacketMask alternateLaneMask = TPacketMask.Load(alternateLanes);
        TVectorMask mask = TVectorMask.Create(alternateLaneMask, alternateLaneMask);
        AssertVector(TVector.Select(mask, left, right), selectX, selectY);
    }

    [TestMethod]
    public void TestMinMax()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        uint[] leftX = PadToMax([1u, 5u, 3u, 8u, 2u, 7u, 4u, 6u], n);
        uint[] leftY = PadToMax([5u, 2u, 8u, 3u, 7u, 4u, 6u, 1u], n);
        uint[] rightX = PadToMax([4u, 3u, 6u, 2u, 5u, 1u, 8u, 7u], n);
        uint[] rightY = PadToMax([3u, 6u, 1u, 7u, 2u, 8u, 4u, 5u], n);

        TVector left = TVector.Create(TPacket.Load(leftX), TPacket.Load(leftY));
        TVector right = TVector.Create(TPacket.Load(rightX), TPacket.Load(rightY));

        TPacket px = TPacket.Load(PadToMax([1u, 2u, 4u, 8u, 16u, 32u, 64u, 128u], n));
        TPacket py = TPacket.Load(PadToMax([3u, 3u, 12u, 12u, 48u, 48u, 192u, 192u], n));
        AssertPacket(px & py, PadToMax([1u, 2u, 4u, 8u, 16u, 32u, 64u, 128u], n));
        AssertPacket(px | py, PadToMax([3u, 3u, 12u, 12u, 48u, 48u, 192u, 192u], n));
    }

    protected static uint[] PadToMax(uint[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        uint[] result = new uint[count];
        values.CopyTo(result, 0);
        return result;
    }

    protected static bool[] PadToMaxBool(bool[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        bool[] result = new bool[count];
        values.CopyTo(result, 0);
        return result;
    }

    protected static void AssertVector(TVector actual, uint[] expectedX, uint[] expectedY)
    {
        int n = TPacket.LaneCount;
        Span<uint> xValues = stackalloc uint[n];
        Span<uint> yValues = stackalloc uint[n];
        TPacket.Store(actual.X, xValues);
        TPacket.Store(actual.Y, yValues);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], xValues[i], $"lane {i} X");
            Assert.AreEqual(expectedY[i], yValues[i], $"lane {i} Y");
        }
    }

    protected static void AssertPacket(TPacket actual, uint[] expected)
    {
        int n = TPacket.LaneCount;
        Span<uint> values = stackalloc uint[n];
        TPacket.Store(actual, values);
        for (int i = 0; i < expected.Length; i++)
            Assert.AreEqual(expected[i], values[i], $"lane {i}");
    }
}
