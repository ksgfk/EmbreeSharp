using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class Matrix4Int32Tests<TMatrix, TVector, TPacket, TMatrixMask, TVectorMask, TPacketMask>
    where TMatrix : unmanaged, ISimdIntegerMatrix4<TMatrix, TVector, TPacket, int, TMatrixMask, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdIntegerVector4<TVector, TPacket, int, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdInteger<TPacket, int, TPacketMask>
    where TMatrixMask : unmanaged, ISimdMatrix4Mask<TMatrixMask, TVectorMask, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector4Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, int>
{
    protected abstract bool IsHardwareSupported { get; }
    protected abstract uint AlignmentMask { get; }

    [TestMethod]
    public void TestIdentity()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TMatrix identity = TMatrix.Identity;
        int[] ones = PadToMax([1, 1, 1, 1, 1, 1, 1, 1], n);
        int[] zeros = PadToMax([0, 0, 0, 0, 0, 0, 0, 0], n);
        AssertMatrix(identity, ones, zeros, zeros, zeros, zeros, ones, zeros, zeros, zeros, zeros, ones, zeros, zeros, zeros, zeros, ones);
    }

    protected static int[] PadToMax(int[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        int[] result = new int[count];
        values.CopyTo(result, 0);
        return result;
    }

    protected static void AssertMatrix(TMatrix actual,
        int[] eR0X, int[] eR0Y, int[] eR0Z, int[] eR0W,
        int[] eR1X, int[] eR1Y, int[] eR1Z, int[] eR1W,
        int[] eR2X, int[] eR2Y, int[] eR2Z, int[] eR2W,
        int[] eR3X, int[] eR3Y, int[] eR3Z, int[] eR3W)
    {
        int n = TPacket.LaneCount;
        Span<int> v = stackalloc int[n];
        TPacket.Store(actual.Row0.X, v); for (int i = 0; i < eR0X.Length; i++) Assert.AreEqual(eR0X[i], v[i], $"lane {i} R0X");
        TPacket.Store(actual.Row0.Y, v); for (int i = 0; i < eR0Y.Length; i++) Assert.AreEqual(eR0Y[i], v[i], $"lane {i} R0Y");
        TPacket.Store(actual.Row0.Z, v); for (int i = 0; i < eR0Z.Length; i++) Assert.AreEqual(eR0Z[i], v[i], $"lane {i} R0Z");
        TPacket.Store(actual.Row0.W, v); for (int i = 0; i < eR0W.Length; i++) Assert.AreEqual(eR0W[i], v[i], $"lane {i} R0W");
        TPacket.Store(actual.Row1.X, v); for (int i = 0; i < eR1X.Length; i++) Assert.AreEqual(eR1X[i], v[i], $"lane {i} R1X");
        TPacket.Store(actual.Row1.Y, v); for (int i = 0; i < eR1Y.Length; i++) Assert.AreEqual(eR1Y[i], v[i], $"lane {i} R1Y");
        TPacket.Store(actual.Row1.Z, v); for (int i = 0; i < eR1Z.Length; i++) Assert.AreEqual(eR1Z[i], v[i], $"lane {i} R1Z");
        TPacket.Store(actual.Row1.W, v); for (int i = 0; i < eR1W.Length; i++) Assert.AreEqual(eR1W[i], v[i], $"lane {i} R1W");
        TPacket.Store(actual.Row2.X, v); for (int i = 0; i < eR2X.Length; i++) Assert.AreEqual(eR2X[i], v[i], $"lane {i} R2X");
        TPacket.Store(actual.Row2.Y, v); for (int i = 0; i < eR2Y.Length; i++) Assert.AreEqual(eR2Y[i], v[i], $"lane {i} R2Y");
        TPacket.Store(actual.Row2.Z, v); for (int i = 0; i < eR2Z.Length; i++) Assert.AreEqual(eR2Z[i], v[i], $"lane {i} R2Z");
        TPacket.Store(actual.Row2.W, v); for (int i = 0; i < eR2W.Length; i++) Assert.AreEqual(eR2W[i], v[i], $"lane {i} R2W");
        TPacket.Store(actual.Row3.X, v); for (int i = 0; i < eR3X.Length; i++) Assert.AreEqual(eR3X[i], v[i], $"lane {i} R3X");
        TPacket.Store(actual.Row3.Y, v); for (int i = 0; i < eR3Y.Length; i++) Assert.AreEqual(eR3Y[i], v[i], $"lane {i} R3Y");
        TPacket.Store(actual.Row3.Z, v); for (int i = 0; i < eR3Z.Length; i++) Assert.AreEqual(eR3Z[i], v[i], $"lane {i} R3Z");
        TPacket.Store(actual.Row3.W, v); for (int i = 0; i < eR3W.Length; i++) Assert.AreEqual(eR3W[i], v[i], $"lane {i} R3W");
    }

    protected static void AssertVector(TVector actual, int[] expectedX, int[] expectedY, int[] expectedZ, int[] expectedW)
    {
        int n = TPacket.LaneCount;
        Span<int> x = stackalloc int[n]; Span<int> y = stackalloc int[n];
        Span<int> z = stackalloc int[n]; Span<int> w = stackalloc int[n];
        TPacket.Store(actual.X, x); TPacket.Store(actual.Y, y);
        TPacket.Store(actual.Z, z); TPacket.Store(actual.W, w);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], x[i], $"lane {i} X");
            Assert.AreEqual(expectedY[i], y[i], $"lane {i} Y");
            Assert.AreEqual(expectedZ[i], z[i], $"lane {i} Z");
            Assert.AreEqual(expectedW[i], w[i], $"lane {i} W");
        }
    }
}
