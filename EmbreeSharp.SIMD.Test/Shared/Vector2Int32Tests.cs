using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class Vector2Int32Tests<TVector, TPacket, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdIntegerVector2<TVector, TPacket, int, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdInteger<TPacket, int, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector2Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, int>
{
    protected abstract bool IsHardwareSupported { get; }
    protected abstract uint AlignmentMask { get; }

    [TestMethod]
    public void TestArithmetic()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        int[] leftX = PadToMax([1, -2, 3, -4, 5, -6, 7, -8], n);
        int[] leftY = PadToMax([2, 3, -4, 5, -6, 7, -8, 9], n);
        int[] rightX = PadToMax([2, 2, 2, 2, 2, 2, 2, 2], n);
        int[] rightY = PadToMax([-1, -1, -1, -1, -1, -1, -1, -1], n);

        int[] addX = PadToMax([3, 0, 5, -2, 7, -4, 9, -6], n);
        int[] addY = PadToMax([1, 2, -5, 4, -7, 6, -9, 8], n);
        int[] subX = PadToMax([-1, -4, 1, -6, 3, -8, 5, -10], n);
        int[] subY = PadToMax([3, 4, -3, 6, -5, 8, -7, 10], n);
        int[] mulX = PadToMax([2, -4, 6, -8, 10, -12, 14, -16], n);
        int[] mulY = PadToMax([-2, -3, 4, -5, 6, -7, 8, -9], n);
        int[] negX = PadToMax([-1, 2, -3, 4, -5, 6, -7, 8], n);
        int[] negY = PadToMax([-2, -3, 4, -5, 6, -7, 8, -9], n);

        TVector left = TVector.Create(TPacket.Load(leftX), TPacket.Load(leftY));
        TVector right = TVector.Create(TPacket.Load(rightX), TPacket.Load(rightY));

        AssertVector(left + right, addX, addY);
        AssertVector(left - right, subX, subY);
        AssertVector(left * right, mulX, mulY);
        AssertVector(-left, negX, negY);

        int[] broadcastX = PadToMax([5, 5, 5, 5, 5, 5, 5, 5], n);
        int[] broadcastY = PadToMax([5, 5, 5, 5, 5, 5, 5, 5], n);
        TVector broadcast = TVector.Broadcast(5);
        AssertVector(broadcast, broadcastX, broadcastY);

        int[] selectX = PadToMax([1, 2, 3, 2, 5, 2, 7, 2], n);
        int[] selectY = PadToMax([2, -1, -4, -1, -6, -1, -8, -1], n);
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
        int[] leftX = PadToMax([1, 5, 3, 8, 2, 7, 4, 6], n);
        int[] leftY = PadToMax([5, 2, 8, 3, 7, 4, 6, 1], n);
        int[] rightX = PadToMax([4, 3, 6, 2, 5, 1, 8, 7], n);
        int[] rightY = PadToMax([3, 6, 1, 7, 2, 8, 4, 5], n);

        TVector left = TVector.Create(TPacket.Load(leftX), TPacket.Load(leftY));
        TVector right = TVector.Create(TPacket.Load(rightX), TPacket.Load(rightY));

        // Test bitwise at packet level only (vector doesn't have IBitwiseOperators)
        TPacket px = TPacket.Load(leftX);
        TPacket py = TPacket.Load(rightX);
        AssertPacket(px & py, PadToMax([0, 1, 2, 0, 0, 1, 0, 6], n));
        AssertPacket(px | py, PadToMax([5, 7, 7, 10, 7, 7, 12, 7], n));
        AssertPacket(~px, PadToMax([-2, -6, -4, -9, -3, -8, -5, -7], n));
    }

    protected static void AssertPacket(TPacket actual, int[] expected)
    {
        int n = TPacket.LaneCount;
        Span<int> values = stackalloc int[n];
        TPacket.Store(actual, values);
        for (int i = 0; i < expected.Length; i++)
            Assert.AreEqual(expected[i], values[i], $"lane {i}");
    }

    protected static int[] PadToMax(int[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        int[] result = new int[count];
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

    protected static void AssertVector(TVector actual, int[] expectedX, int[] expectedY)
    {
        int n = TPacket.LaneCount;
        Span<int> xValues = stackalloc int[n];
        Span<int> yValues = stackalloc int[n];
        TPacket.Store(actual.X, xValues);
        TPacket.Store(actual.Y, yValues);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], xValues[i], $"lane {i} X");
            Assert.AreEqual(expectedY[i], yValues[i], $"lane {i} Y");
        }
    }
}
