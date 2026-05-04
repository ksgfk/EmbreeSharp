using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class Matrix3Float64Tests<TMatrix, TVector, TPacket, TMatrixMask, TVectorMask, TPacketMask>
    where TMatrix : unmanaged, ISimdFloatingPointMatrix3<TMatrix, TVector, TPacket, double, TMatrixMask, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdFloatingPointVector3<TVector, TPacket, double, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdFloatingPoint<TPacket, double, TPacketMask>
    where TMatrixMask : unmanaged, ISimdMatrix3Mask<TMatrixMask, TVectorMask, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector3Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, double>
{
    protected abstract bool IsHardwareSupported { get; }
    protected abstract uint AlignmentMask { get; }

    [TestMethod]
    public void TestIdentity()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TMatrix identity = TMatrix.Identity;
        double[] ones = PadToMax([1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0], n);
        double[] zeros = PadToMax([0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0], n);
        AssertMatrix(identity, ones, zeros, zeros, zeros, ones, zeros, zeros, zeros, ones);
    }

    [TestMethod]
    public void TestScale()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        double[] zeros = PadToMax([0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0], n);
        double[] ones = PadToMax([1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0], n);
        TMatrix scale = TMatrix.Scale(TPacket.Broadcast(2.0), TPacket.Broadcast(3.0));
        AssertMatrix(scale,
            PadToMax([2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0], n), zeros, zeros,
            zeros, PadToMax([3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0], n), zeros,
            zeros, zeros, ones);
    }

    [TestMethod]
    public void TestTranslate()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        double[] zeros = PadToMax([0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0], n);
        double[] ones = PadToMax([1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0], n);
        TMatrix translate = TMatrix.Translate(TPacket.Broadcast(2.0), TPacket.Broadcast(3.0));
        AssertMatrix(translate,
            ones, zeros, PadToMax([2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0, 2.0], n),
            zeros, ones, PadToMax([3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0], n),
            zeros, zeros, ones);
    }

    [TestMethod]
    public void TestTransform()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TMatrix m = TMatrix.Translate(TPacket.Broadcast(2.0), TPacket.Broadcast(3.0));
        TVector v = TVector.Create(TPacket.Broadcast(1.0), TPacket.Broadcast(1.0), TPacket.Broadcast(1.0));
        TVector result = TMatrix.Transform(m, v);
        AssertVector(result,
            PadToMax([3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0], n),
            PadToMax([4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0, 4.0], n),
            PadToMax([1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0], n));
    }

    protected static double[] PadToMax(double[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        double[] result = new double[count];
        values.CopyTo(result, 0);
        return result;
    }

    protected static void AssertMatrix(TMatrix actual,
        double[] eR0X, double[] eR0Y, double[] eR0Z,
        double[] eR1X, double[] eR1Y, double[] eR1Z,
        double[] eR2X, double[] eR2Y, double[] eR2Z,
        double epsilon = 1e-12)
    {
        int n = TPacket.LaneCount;
        Span<double> v = stackalloc double[n];
        TPacket.Store(actual.Row0.X, v); for (int i = 0; i < eR0X.Length; i++) Assert.AreEqual(eR0X[i], v[i], epsilon, $"lane {i} R0X");
        TPacket.Store(actual.Row0.Y, v); for (int i = 0; i < eR0Y.Length; i++) Assert.AreEqual(eR0Y[i], v[i], epsilon, $"lane {i} R0Y");
        TPacket.Store(actual.Row0.Z, v); for (int i = 0; i < eR0Z.Length; i++) Assert.AreEqual(eR0Z[i], v[i], epsilon, $"lane {i} R0Z");
        TPacket.Store(actual.Row1.X, v); for (int i = 0; i < eR1X.Length; i++) Assert.AreEqual(eR1X[i], v[i], epsilon, $"lane {i} R1X");
        TPacket.Store(actual.Row1.Y, v); for (int i = 0; i < eR1Y.Length; i++) Assert.AreEqual(eR1Y[i], v[i], epsilon, $"lane {i} R1Y");
        TPacket.Store(actual.Row1.Z, v); for (int i = 0; i < eR1Z.Length; i++) Assert.AreEqual(eR1Z[i], v[i], epsilon, $"lane {i} R1Z");
        TPacket.Store(actual.Row2.X, v); for (int i = 0; i < eR2X.Length; i++) Assert.AreEqual(eR2X[i], v[i], epsilon, $"lane {i} R2X");
        TPacket.Store(actual.Row2.Y, v); for (int i = 0; i < eR2Y.Length; i++) Assert.AreEqual(eR2Y[i], v[i], epsilon, $"lane {i} R2Y");
        TPacket.Store(actual.Row2.Z, v); for (int i = 0; i < eR2Z.Length; i++) Assert.AreEqual(eR2Z[i], v[i], epsilon, $"lane {i} R2Z");
    }

    protected static void AssertVector(TVector actual, double[] expectedX, double[] expectedY, double[] expectedZ, double epsilon = 1e-12)
    {
        int n = TPacket.LaneCount;
        Span<double> x = stackalloc double[n]; Span<double> y = stackalloc double[n]; Span<double> z = stackalloc double[n];
        TPacket.Store(actual.X, x); TPacket.Store(actual.Y, y); TPacket.Store(actual.Z, z);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], x[i], epsilon, $"lane {i} X");
            Assert.AreEqual(expectedY[i], y[i], epsilon, $"lane {i} Y");
            Assert.AreEqual(expectedZ[i], z[i], epsilon, $"lane {i} Z");
        }
    }
}
