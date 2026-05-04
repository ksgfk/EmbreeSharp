using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class Matrix3Int32Tests<TMatrix, TVector, TPacket, TMatrixMask, TVectorMask, TPacketMask>
    where TMatrix : unmanaged, ISimdIntegerMatrix3<TMatrix, TVector, TPacket, int, TMatrixMask, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdIntegerVector3<TVector, TPacket, int, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdInteger<TPacket, int, TPacketMask>
    where TMatrixMask : unmanaged, ISimdMatrix3Mask<TMatrixMask, TVectorMask, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector3Mask<TVectorMask, TPacketMask>
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
        AssertMatrix(identity, ones, zeros, zeros, zeros, ones, zeros, zeros, zeros, ones);
    }

    protected static int[] PadToMax(int[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        int[] result = new int[count];
        values.CopyTo(result, 0);
        return result;
    }

    protected static void AssertMatrix(TMatrix actual,
        int[] eR0X, int[] eR0Y, int[] eR0Z,
        int[] eR1X, int[] eR1Y, int[] eR1Z,
        int[] eR2X, int[] eR2Y, int[] eR2Z)
    {
        int n = TPacket.LaneCount;
        Span<int> v = stackalloc int[n];
        TPacket.Store(actual.Row0.X, v); for (int i = 0; i < eR0X.Length; i++) Assert.AreEqual(eR0X[i], v[i], $"lane {i} R0X");
        TPacket.Store(actual.Row0.Y, v); for (int i = 0; i < eR0Y.Length; i++) Assert.AreEqual(eR0Y[i], v[i], $"lane {i} R0Y");
        TPacket.Store(actual.Row0.Z, v); for (int i = 0; i < eR0Z.Length; i++) Assert.AreEqual(eR0Z[i], v[i], $"lane {i} R0Z");
        TPacket.Store(actual.Row1.X, v); for (int i = 0; i < eR1X.Length; i++) Assert.AreEqual(eR1X[i], v[i], $"lane {i} R1X");
        TPacket.Store(actual.Row1.Y, v); for (int i = 0; i < eR1Y.Length; i++) Assert.AreEqual(eR1Y[i], v[i], $"lane {i} R1Y");
        TPacket.Store(actual.Row1.Z, v); for (int i = 0; i < eR1Z.Length; i++) Assert.AreEqual(eR1Z[i], v[i], $"lane {i} R1Z");
        TPacket.Store(actual.Row2.X, v); for (int i = 0; i < eR2X.Length; i++) Assert.AreEqual(eR2X[i], v[i], $"lane {i} R2X");
        TPacket.Store(actual.Row2.Y, v); for (int i = 0; i < eR2Y.Length; i++) Assert.AreEqual(eR2Y[i], v[i], $"lane {i} R2Y");
        TPacket.Store(actual.Row2.Z, v); for (int i = 0; i < eR2Z.Length; i++) Assert.AreEqual(eR2Z[i], v[i], $"lane {i} R2Z");
    }

    protected static void AssertVector(TVector actual, int[] expectedX, int[] expectedY, int[] expectedZ)
    {
        int n = TPacket.LaneCount;
        Span<int> x = stackalloc int[n]; Span<int> y = stackalloc int[n]; Span<int> z = stackalloc int[n];
        TPacket.Store(actual.X, x); TPacket.Store(actual.Y, y); TPacket.Store(actual.Z, z);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], x[i], $"lane {i} X");
            Assert.AreEqual(expectedY[i], y[i], $"lane {i} Y");
            Assert.AreEqual(expectedZ[i], z[i], $"lane {i} Z");
        }
    }
}
