using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class Vector4Int32Tests<TVector, TPacket, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdIntegerVector4<TVector, TPacket, int, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdInteger<TPacket, int, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector4Mask<TVectorMask, TPacketMask>
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
        int[] leftZ = PadToMax([0, -1, 2, -3, 4, -5, 6, -7], n);
        int[] leftW = PadToMax([-1, 0, -2, 3, -4, 5, -6, 7], n);
        int[] rightX = PadToMax([2, 2, 2, 2, 2, 2, 2, 2], n);
        int[] rightY = PadToMax([3, 3, 3, 3, 3, 3, 3, 3], n);
        int[] rightZ = PadToMax([4, 4, 4, 4, 4, 4, 4, 4], n);
        int[] rightW = PadToMax([5, 5, 5, 5, 5, 5, 5, 5], n);

        TVector left = TVector.Create(TPacket.Load(leftX), TPacket.Load(leftY), TPacket.Load(leftZ), TPacket.Load(leftW));
        TVector right = TVector.Create(TPacket.Load(rightX), TPacket.Load(rightY), TPacket.Load(rightZ), TPacket.Load(rightW));

        AssertVector(left + right, PadToMax([3, 0, 5, -2, 7, -4, 9, -6], n), PadToMax([5, 6, -1, 8, -3, 10, -5, 12], n), PadToMax([4, 3, 6, 1, 8, -1, 10, -3], n), PadToMax([4, 5, 3, 8, 1, 10, -1, 12], n));
        AssertVector(left - right, PadToMax([-1, -4, 1, -6, 3, -8, 5, -10], n), PadToMax([-1, 0, -7, 2, -9, 4, -11, 6], n), PadToMax([-4, -5, -2, -7, 0, -9, 2, -11], n), PadToMax([-6, -5, -7, -2, -9, 0, -11, 2], n));
        AssertVector(left * right, PadToMax([2, -4, 6, -8, 10, -12, 14, -16], n), PadToMax([6, 9, -12, 15, -18, 21, -24, 27], n), PadToMax([0, -4, 8, -12, 16, -20, 24, -28], n), PadToMax([-5, 0, -10, 15, -20, 25, -30, 35], n));
        AssertVector(-left, PadToMax([-1, 2, -3, 4, -5, 6, -7, 8], n), PadToMax([-2, -3, 4, -5, 6, -7, 8, -9], n), PadToMax([0, 1, -2, 3, -4, 5, -6, 7], n), PadToMax([1, 0, 2, -3, 4, -5, 6, -7], n));

        TVector broadcast = TVector.Broadcast(42);
        int[] fortyTwo = PadToMax([42, 42, 42, 42, 42, 42, 42, 42], n);
        AssertVector(broadcast, fortyTwo, fortyTwo, fortyTwo, fortyTwo);

        TPacketMask alternateLaneMask = TPacketMask.Load(PadToMaxBool([true, false, true, false, true, false, true, false], n));
        TVectorMask mask = TVectorMask.Create(alternateLaneMask, alternateLaneMask, alternateLaneMask, alternateLaneMask);
        AssertVector(TVector.Select(mask, left, right), PadToMax([1, 2, 3, 2, 5, 2, 7, 2], n), PadToMax([2, 3, -4, 3, -6, 3, -8, 3], n), PadToMax([0, 4, 2, 4, 4, 4, 6, 4], n), PadToMax([-1, 5, -2, 5, -4, 5, -6, 5], n));

        var leftCopy = left;
        TVectorMask eqMask = left == leftCopy;
        bool[] allTrue = PadToMaxBool([true, true, true, true, true, true, true, true], n);
        AssertVectorMask(eqMask, allTrue, allTrue, allTrue, allTrue);
    }

    [TestMethod]
    public void TestBitwise()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        int[] leftX = PadToMax([1, 2, 4, 8, 16, 32, 64, 128], n);
        int[] leftY = PadToMax([3, 6, 12, 24, 48, 96, 192, 384], n);
        int[] leftZ = PadToMax([1, 3, 7, 15, 31, 63, 127, 255], n);
        int[] leftW = PadToMax([2, 5, 11, 23, 47, 95, 191, 383], n);
        int[] rightX = PadToMax([3, 3, 12, 12, 48, 48, 192, 192], n);
        int[] rightY = PadToMax([5, 5, 20, 20, 80, 80, 320, 320], n);
        int[] rightZ = PadToMax([3, 3, 11, 11, 47, 47, 191, 191], n);
        int[] rightW = PadToMax([6, 6, 22, 22, 94, 94, 382, 382], n);

        TPacket leftXPacket = TPacket.Load(leftX);
        TPacket leftYPacket = TPacket.Load(leftY);
        TPacket leftZPacket = TPacket.Load(leftZ);
        TPacket leftWPacket = TPacket.Load(leftW);
        TPacket rightXPacket = TPacket.Load(rightX);
        TPacket rightYPacket = TPacket.Load(rightY);
        TPacket rightZPacket = TPacket.Load(rightZ);
        TPacket rightWPacket = TPacket.Load(rightW);

        TVector andResult = TVector.Create(leftXPacket & rightXPacket, leftYPacket & rightYPacket, leftZPacket & rightZPacket, leftWPacket & rightWPacket);
        AssertVector(andResult, PadToMax([1, 2, 4, 8, 16, 32, 64, 128], n), PadToMax([1, 4, 4, 16, 16, 64, 64, 256], n), PadToMax([1, 3, 3, 11, 15, 47, 63, 191], n), PadToMax([2, 4, 2, 22, 14, 94, 62, 382], n));

        TVector orResult = TVector.Create(leftXPacket | rightXPacket, leftYPacket | rightYPacket, leftZPacket | rightZPacket, leftWPacket | rightWPacket);
        AssertVector(orResult, PadToMax([3, 3, 12, 12, 48, 48, 192, 192], n), PadToMax([7, 7, 28, 28, 112, 112, 448, 448], n), PadToMax([3, 3, 15, 15, 63, 63, 255, 255], n), PadToMax([6, 7, 31, 23, 127, 95, 511, 383], n));

        TVector xorResult = TVector.Create(leftXPacket ^ rightXPacket, leftYPacket ^ rightYPacket, leftZPacket ^ rightZPacket, leftWPacket ^ rightWPacket);
        AssertVector(xorResult, PadToMax([2, 1, 8, 4, 32, 16, 128, 64], n), PadToMax([6, 3, 24, 12, 96, 48, 384, 192], n), PadToMax([2, 0, 12, 4, 48, 16, 192, 64], n), PadToMax([4, 3, 29, 1, 113, 1, 449, 1], n));

        TVector notResult = TVector.Create(~leftXPacket, ~leftYPacket, ~leftZPacket, ~leftWPacket);
        AssertVector(notResult, PadToMax([-2, -3, -5, -9, -17, -33, -65, -129], n), PadToMax([-4, -7, -13, -25, -49, -97, -193, -385], n), PadToMax([-2, -4, -8, -16, -32, -64, -128, -256], n), PadToMax([-3, -6, -12, -24, -48, -96, -192, -384], n));
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

    protected static void AssertVector(TVector actual, int[] expectedX, int[] expectedY, int[] expectedZ, int[] expectedW)
    {
        int n = TPacket.LaneCount;
        Span<int> xValues = stackalloc int[n];
        Span<int> yValues = stackalloc int[n];
        Span<int> zValues = stackalloc int[n];
        Span<int> wValues = stackalloc int[n];
        TPacket.Store(actual.X, xValues);
        TPacket.Store(actual.Y, yValues);
        TPacket.Store(actual.Z, zValues);
        TPacket.Store(actual.W, wValues);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], xValues[i], $"lane {i} X");
            Assert.AreEqual(expectedY[i], yValues[i], $"lane {i} Y");
            Assert.AreEqual(expectedZ[i], zValues[i], $"lane {i} Z");
            Assert.AreEqual(expectedW[i], wValues[i], $"lane {i} W");
        }
    }

    protected static void AssertVectorMask(TVectorMask actual, bool[] expectedX, bool[] expectedY, bool[] expectedZ, bool[] expectedW)
    {
        int n = TPacket.LaneCount;
        Span<int> xValues = stackalloc int[n];
        Span<int> yValues = stackalloc int[n];
        Span<int> zValues = stackalloc int[n];
        Span<int> wValues = stackalloc int[n];
        TPacketMask.Store(actual.X, xValues);
        TPacketMask.Store(actual.Y, yValues);
        TPacketMask.Store(actual.Z, zValues);
        TPacketMask.Store(actual.W, wValues);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], xValues[i] != 0, $"lane {i} X");
            Assert.AreEqual(expectedY[i], yValues[i] != 0, $"lane {i} Y");
            Assert.AreEqual(expectedZ[i], zValues[i] != 0, $"lane {i} Z");
            Assert.AreEqual(expectedW[i], wValues[i] != 0, $"lane {i} W");
        }
    }
}
