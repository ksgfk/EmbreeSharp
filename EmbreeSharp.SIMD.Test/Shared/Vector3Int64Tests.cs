using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class Vector3Int64Tests<TVector, TPacket, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdIntegerVector3<TVector, TPacket, long, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdInteger<TPacket, long, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector3Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, long>
{
    protected abstract bool IsHardwareSupported { get; }
    protected abstract uint AlignmentMask { get; }

    [TestMethod]
    public void TestArithmetic()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        long[] leftX = PadToMax([1L, -2L, 3L, -4L], n);
        long[] leftY = PadToMax([2L, 3L, -4L, 5L], n);
        long[] leftZ = PadToMax([0L, -1L, 2L, 0L], n);
        long[] rightX = PadToMax([0L, -1L, 2L, 0L], n);
        long[] rightY = PadToMax([0L, 1L, -2L, 0L], n);
        long[] rightZ = PadToMax([1L, -2L, 3L, -4L], n);

        TVector left = TVector.Create(TPacket.Load(leftX), TPacket.Load(leftY), TPacket.Load(leftZ));
        TVector right = TVector.Create(TPacket.Load(rightX), TPacket.Load(rightY), TPacket.Load(rightZ));

        long[] addX = PadToMax([1L, -3L, 5L, -4L], n);
        long[] addY = PadToMax([2L, 4L, -6L, 5L], n);
        long[] addZ = PadToMax([1L, -3L, 5L, -4L], n);
        AssertVector(left + right, addX, addY, addZ);

        long[] subX = PadToMax([1L, -1L, 1L, -4L], n);
        long[] subY = PadToMax([2L, 2L, -2L, 5L], n);
        long[] subZ = PadToMax([-1L, 1L, -1L, 4L], n);
        AssertVector(left - right, subX, subY, subZ);

        AssertVector(left * right, PadToMax([0L, 2L, 6L, 0L], n), PadToMax([0L, 3L, 8L, 0L], n), PadToMax([0L, 2L, 6L, 0L], n));
        AssertVector(-left, PadToMax([-1L, 2L, -3L, 4L], n), PadToMax([-2L, -3L, 4L, -5L], n), PadToMax([0L, 1L, -2L, 0L], n));

        TVector broadcast = TVector.Broadcast(3L);
        AssertVector(broadcast, PadToMax([3L, 3L, 3L, 3L], n), PadToMax([3L, 3L, 3L, 3L], n), PadToMax([3L, 3L, 3L, 3L], n));

        TPacketMask alternateLaneMask = TPacketMask.Load(PadToMaxBool([true, false, true, false], n));
        TVectorMask mask = TVectorMask.Create(alternateLaneMask, alternateLaneMask, alternateLaneMask);
        AssertVector(TVector.Select(mask, left, right), PadToMax([1L, -1L, 3L, 0L], n), PadToMax([2L, 1L, -4L, 0L], n), PadToMax([0L, -2L, 2L, -4L], n));

        var leftCopy = left;
        TVectorMask eqMask = left == leftCopy;
        bool[] allTrue = PadToMaxBool([true, true, true, true], n);
        AssertVectorMask(eqMask, allTrue, allTrue, allTrue);
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

    protected static void AssertVector(TVector actual, long[] expectedX, long[] expectedY, long[] expectedZ)
    {
        int n = TPacket.LaneCount;
        Span<long> xValues = stackalloc long[n];
        Span<long> yValues = stackalloc long[n];
        Span<long> zValues = stackalloc long[n];
        TPacket.Store(actual.X, xValues);
        TPacket.Store(actual.Y, yValues);
        TPacket.Store(actual.Z, zValues);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], xValues[i], $"lane {i} X");
            Assert.AreEqual(expectedY[i], yValues[i], $"lane {i} Y");
            Assert.AreEqual(expectedZ[i], zValues[i], $"lane {i} Z");
        }
    }

    protected static void AssertVectorMask(TVectorMask actual, bool[] expectedX, bool[] expectedY, bool[] expectedZ)
    {
        int n = TPacket.LaneCount;
        Span<long> xValues = stackalloc long[n];
        Span<long> yValues = stackalloc long[n];
        Span<long> zValues = stackalloc long[n];
        TPacketMask.Store(actual.X, xValues);
        TPacketMask.Store(actual.Y, yValues);
        TPacketMask.Store(actual.Z, zValues);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], xValues[i] != 0, $"lane {i} X");
            Assert.AreEqual(expectedY[i], yValues[i] != 0, $"lane {i} Y");
            Assert.AreEqual(expectedZ[i], zValues[i] != 0, $"lane {i} Z");
        }
    }
}
