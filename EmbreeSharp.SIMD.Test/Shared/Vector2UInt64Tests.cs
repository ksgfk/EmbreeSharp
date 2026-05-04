using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class Vector2UInt64Tests<TVector, TPacket, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdIntegerVector2<TVector, TPacket, ulong, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdInteger<TPacket, ulong, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector2Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, ulong>
{
    protected abstract bool IsHardwareSupported { get; }
    protected abstract uint AlignmentMask { get; }

    [TestMethod]
    public void TestArithmetic()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        ulong[] leftX = PadToMax([1ul, 2ul, 3ul, 4ul, 5ul, 6ul, 7ul, 8ul], n);
        ulong[] leftY = PadToMax([2ul, 3ul, 4ul, 5ul, 6ul, 7ul, 8ul, 9ul], n);
        ulong[] rightX = PadToMax([2ul, 2ul, 2ul, 2ul, 2ul, 2ul, 2ul, 2ul], n);
        ulong[] rightY = PadToMax([1ul, 1ul, 1ul, 1ul, 1ul, 1ul, 1ul, 1ul], n);

        ulong[] addX = PadToMax([3ul, 4ul, 5ul, 6ul, 7ul, 8ul, 9ul, 10ul], n);
        ulong[] addY = PadToMax([3ul, 4ul, 5ul, 6ul, 7ul, 8ul, 9ul, 10ul], n);
        ulong[] subX = PadToMax([ulong.MaxValue, 0ul, 1ul, 2ul, 3ul, 4ul, 5ul, 6ul], n);
        ulong[] subY = PadToMax([1ul, 2ul, 3ul, 4ul, 5ul, 6ul, 7ul, 8ul], n);
        ulong[] mulX = PadToMax([2ul, 4ul, 6ul, 8ul, 10ul, 12ul, 14ul, 16ul], n);
        ulong[] mulY = PadToMax([2ul, 3ul, 4ul, 5ul, 6ul, 7ul, 8ul, 9ul], n);

        TVector left = TVector.Create(TPacket.Load(leftX), TPacket.Load(leftY));
        TVector right = TVector.Create(TPacket.Load(rightX), TPacket.Load(rightY));

        AssertVector(left + right, addX, addY);
        AssertVector(left - right, subX, subY);
        AssertVector(left * right, mulX, mulY);

        ulong[] broadcastX = PadToMax([5ul, 5ul, 5ul, 5ul, 5ul, 5ul, 5ul, 5ul], n);
        ulong[] broadcastY = PadToMax([5ul, 5ul, 5ul, 5ul, 5ul, 5ul, 5ul, 5ul], n);
        TVector broadcast = TVector.Broadcast(5ul);
        AssertVector(broadcast, broadcastX, broadcastY);

        ulong[] selectX = PadToMax([1ul, 2ul, 3ul, 2ul, 5ul, 2ul, 7ul, 2ul], n);
        ulong[] selectY = PadToMax([2ul, 1ul, 4ul, 1ul, 6ul, 1ul, 8ul, 1ul], n);
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
        ulong[] leftX = PadToMax([1ul, 5ul, 3ul, 8ul, 2ul, 7ul, 4ul, 6ul], n);
        ulong[] leftY = PadToMax([5ul, 2ul, 8ul, 3ul, 7ul, 4ul, 6ul, 1ul], n);
        ulong[] rightX = PadToMax([4ul, 3ul, 6ul, 2ul, 5ul, 1ul, 8ul, 7ul], n);
        ulong[] rightY = PadToMax([3ul, 6ul, 1ul, 7ul, 2ul, 8ul, 4ul, 5ul], n);

        TVector left = TVector.Create(TPacket.Load(leftX), TPacket.Load(leftY));
        TVector right = TVector.Create(TPacket.Load(rightX), TPacket.Load(rightY));

        TPacket px = TPacket.Load(PadToMax([1ul, 2ul, 4ul, 8ul, 16ul, 32ul, 64ul, 128ul], n));
        TPacket py = TPacket.Load(PadToMax([3ul, 3ul, 12ul, 12ul, 48ul, 48ul, 192ul, 192ul], n));
        AssertPacket(px & py, PadToMax([1ul, 2ul, 4ul, 8ul, 16ul, 32ul, 64ul, 128ul], n));
        AssertPacket(px | py, PadToMax([3ul, 3ul, 12ul, 12ul, 48ul, 48ul, 192ul, 192ul], n));
    }

    protected static ulong[] PadToMax(ulong[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        ulong[] result = new ulong[count];
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

    protected static void AssertVector(TVector actual, ulong[] expectedX, ulong[] expectedY)
    {
        int n = TPacket.LaneCount;
        Span<ulong> xValues = stackalloc ulong[n];
        Span<ulong> yValues = stackalloc ulong[n];
        TPacket.Store(actual.X, xValues);
        TPacket.Store(actual.Y, yValues);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], xValues[i], $"lane {i} X");
            Assert.AreEqual(expectedY[i], yValues[i], $"lane {i} Y");
        }
    }

    protected static void AssertPacket(TPacket actual, ulong[] expected)
    {
        int n = TPacket.LaneCount;
        Span<ulong> values = stackalloc ulong[n];
        TPacket.Store(actual, values);
        for (int i = 0; i < expected.Length; i++)
            Assert.AreEqual(expected[i], values[i], $"lane {i}");
    }
}
