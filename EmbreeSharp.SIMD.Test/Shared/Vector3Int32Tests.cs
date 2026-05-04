using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class Vector3Int32Tests<TVector, TPacket, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdIntegerVector3<TVector, TPacket, int, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdInteger<TPacket, int, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector3Mask<TVectorMask, TPacketMask>
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
        int[] leftZ = PadToMax([0, -1, 2, 0, 1, -1, 0, -2], n);
        int[] rightX = PadToMax([0, -1, 2, 0, 1, -1, 0, -2], n);
        int[] rightY = PadToMax([0, 1, -2, 0, -1, 1, 0, 2], n);
        int[] rightZ = PadToMax([1, -2, 3, -4, 5, -6, 7, -8], n);

        TVector left = TVector.Create(TPacket.Load(leftX), TPacket.Load(leftY), TPacket.Load(leftZ));
        TVector right = TVector.Create(TPacket.Load(rightX), TPacket.Load(rightY), TPacket.Load(rightZ));

        int[] addX = PadToMax([1, -3, 5, -4, 6, -7, 7, -10], n);
        int[] addY = PadToMax([2, 4, -6, 5, -7, 8, -8, 11], n);
        int[] addZ = PadToMax([1, -3, 5, -4, 6, -7, 7, -10], n);
        AssertVector(left + right, addX, addY, addZ);

        int[] subX = PadToMax([1, -1, 1, -4, 4, -5, 7, -6], n);
        int[] subY = PadToMax([2, 2, -2, 5, -5, 6, -8, 7], n);
        int[] subZ = PadToMax([-1, 1, -1, 4, -4, 5, -7, 6], n);
        AssertVector(left - right, subX, subY, subZ);

        AssertVector(left * right, PadToMax([0, 2, 6, 0, 5, 6, 0, 16], n), PadToMax([0, 3, 8, 0, 6, 7, 0, 18], n), PadToMax([0, 2, 6, 0, 5, 6, 0, 16], n));
        AssertVector(-left, PadToMax([-1, 2, -3, 4, -5, 6, -7, 8], n), PadToMax([-2, -3, 4, -5, 6, -7, 8, -9], n), PadToMax([0, 1, -2, 0, -1, 1, 0, 2], n));

        TVector broadcast = TVector.Broadcast(3);
        AssertVector(broadcast, PadToMax([3, 3, 3, 3, 3, 3, 3, 3], n), PadToMax([3, 3, 3, 3, 3, 3, 3, 3], n), PadToMax([3, 3, 3, 3, 3, 3, 3, 3], n));

        TPacketMask alternateLaneMask = TPacketMask.Load(PadToMaxBool([true, false, true, false, true, false, true, false], n));
        TVectorMask mask = TVectorMask.Create(alternateLaneMask, alternateLaneMask, alternateLaneMask);
        AssertVector(TVector.Select(mask, left, right), PadToMax([1, -1, 3, 0, 5, -1, 7, -2], n), PadToMax([2, 1, -4, 0, -6, 1, -8, 2], n), PadToMax([0, -2, 2, -4, 1, -6, 0, -8], n));

        var leftCopy = left;
        TVectorMask eqMask = left == leftCopy;
        bool[] allTrue = PadToMaxBool([true, true, true, true, true, true, true, true], n);
        AssertVectorMask(eqMask, allTrue, allTrue, allTrue);
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

    protected static void AssertVector(TVector actual, int[] expectedX, int[] expectedY, int[] expectedZ)
    {
        int n = TPacket.LaneCount;
        Span<int> xValues = stackalloc int[n];
        Span<int> yValues = stackalloc int[n];
        Span<int> zValues = stackalloc int[n];
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
        Span<int> xValues = stackalloc int[n];
        Span<int> yValues = stackalloc int[n];
        Span<int> zValues = stackalloc int[n];
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
