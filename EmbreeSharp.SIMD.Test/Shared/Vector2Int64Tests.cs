using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class Vector2Int64Tests<TVector, TPacket, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdIntegerVector2<TVector, TPacket, long, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdInteger<TPacket, long, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector2Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, long>
{
    protected abstract bool IsHardwareSupported { get; }
    protected abstract uint AlignmentMask { get; }

    [TestMethod]
    public void TestArithmetic()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        long[] leftX = PadToMax([1L, -2L, 3L, -4L, 5L, -6L, 7L, -8L], n);
        long[] leftY = PadToMax([2L, 3L, -4L, 5L, -6L, 7L, -8L, 9L], n);
        long[] rightX = PadToMax([2L, 2L, 2L, 2L, 2L, 2L, 2L, 2L], n);
        long[] rightY = PadToMax([-1L, -1L, -1L, -1L, -1L, -1L, -1L, -1L], n);

        long[] addX = PadToMax([3L, 0L, 5L, -2L, 7L, -4L, 9L, -6L], n);
        long[] addY = PadToMax([1L, 2L, -5L, 4L, -7L, 6L, -9L, 8L], n);
        long[] subX = PadToMax([-1L, -4L, 1L, -6L, 3L, -8L, 5L, -10L], n);
        long[] subY = PadToMax([3L, 4L, -3L, 6L, -5L, 8L, -7L, 10L], n);
        long[] mulX = PadToMax([2L, -4L, 6L, -8L, 10L, -12L, 14L, -16L], n);
        long[] mulY = PadToMax([-2L, -3L, 4L, -5L, 6L, -7L, 8L, -9L], n);
        long[] negX = PadToMax([-1L, 2L, -3L, 4L, -5L, 6L, -7L, 8L], n);
        long[] negY = PadToMax([-2L, -3L, 4L, -5L, 6L, -7L, 8L, -9L], n);

        TVector left = TVector.Create(TPacket.Load(leftX), TPacket.Load(leftY));
        TVector right = TVector.Create(TPacket.Load(rightX), TPacket.Load(rightY));

        AssertVector(left + right, addX, addY);
        AssertVector(left - right, subX, subY);
        AssertVector(left * right, mulX, mulY);
        AssertVector(-left, negX, negY);

        long[] broadcastX = PadToMax([5L, 5L, 5L, 5L, 5L, 5L, 5L, 5L], n);
        long[] broadcastY = PadToMax([5L, 5L, 5L, 5L, 5L, 5L, 5L, 5L], n);
        TVector broadcast = TVector.Broadcast(5L);
        AssertVector(broadcast, broadcastX, broadcastY);

        long[] selectX = PadToMax([1L, 2L, 3L, 2L, 5L, 2L, 7L, 2L], n);
        long[] selectY = PadToMax([2L, -1L, -4L, -1L, -6L, -1L, -8L, -1L], n);
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
        long[] leftX = PadToMax([1L, 5L, 3L, 8L, 2L, 7L, 4L, 6L], n);
        long[] leftY = PadToMax([5L, 2L, 8L, 3L, 7L, 4L, 6L, 1L], n);
        long[] rightX = PadToMax([4L, 3L, 6L, 2L, 5L, 1L, 8L, 7L], n);
        long[] rightY = PadToMax([3L, 6L, 1L, 7L, 2L, 8L, 4L, 5L], n);

        TVector left = TVector.Create(TPacket.Load(leftX), TPacket.Load(leftY));
        TVector right = TVector.Create(TPacket.Load(rightX), TPacket.Load(rightY));

        TPacket px = TPacket.Load(PadToMax([1L, 2L, 4L, 8L, 16L, 32L, 64L, 128L], n));
        TPacket py = TPacket.Load(PadToMax([3L, 3L, 12L, 12L, 48L, 48L, 192L, 192L], n));
        AssertPacket(px & py, PadToMax([1L, 2L, 4L, 8L, 16L, 32L, 64L, 128L], n));
        AssertPacket(px | py, PadToMax([3L, 3L, 12L, 12L, 48L, 48L, 192L, 192L], n));
        AssertPacket(~px, PadToMax([-2L, -3L, -5L, -9L, -17L, -33L, -65L, -129L], n));
    }

    protected static long[] PadToMax(long[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        long[] result = new long[count];
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

    protected static void AssertVector(TVector actual, long[] expectedX, long[] expectedY)
    {
        int n = TPacket.LaneCount;
        Span<long> xValues = stackalloc long[n];
        Span<long> yValues = stackalloc long[n];
        TPacket.Store(actual.X, xValues);
        TPacket.Store(actual.Y, yValues);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], xValues[i], $"lane {i} X");
            Assert.AreEqual(expectedY[i], yValues[i], $"lane {i} Y");
        }
    }

    protected static void AssertPacket(TPacket actual, long[] expected)
    {
        int n = TPacket.LaneCount;
        Span<long> values = stackalloc long[n];
        TPacket.Store(actual, values);
        for (int i = 0; i < expected.Length; i++)
            Assert.AreEqual(expected[i], values[i], $"lane {i}");
    }
}
