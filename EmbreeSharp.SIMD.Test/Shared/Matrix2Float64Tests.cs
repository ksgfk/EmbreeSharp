using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class Matrix2Float64Tests<TMatrix, TVector, TPacket, TMatrixMask, TVectorMask, TPacketMask>
    where TMatrix : unmanaged, ISimdFloatingPointMatrix2<TMatrix, TVector, TPacket, double, TMatrixMask, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdFloatingPointVector2<TVector, TPacket, double, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdFloatingPoint<TPacket, double, TPacketMask>
    where TMatrixMask : unmanaged, ISimdMatrix2Mask<TMatrixMask, TVectorMask, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector2Mask<TVectorMask, TPacketMask>
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
        double[] ones = PadToMax([1.0, 1.0, 1.0, 1.0], n);
        double[] zeros = PadToMax([0.0, 0.0, 0.0, 0.0], n);
        AssertMatrix(identity, ones, zeros, zeros, ones);

        TVector v = TVector.Create(TPacket.Broadcast(2.0), TPacket.Broadcast(3.0));
        TVector transformed = TMatrix.Transform(identity, v);
        AssertVector(transformed, PadToMax([2.0, 2.0, 2.0, 2.0], n), PadToMax([3.0, 3.0, 3.0, 3.0], n));
    }

    [TestMethod]
    public void TestArithmetic()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TMatrix left = TMatrix.Create(
            TVector.Create(TPacket.Load(PadToMax([1.0, -2.0, 3.5, -4.5], n)), TPacket.Load(PadToMax([2.0, 3.0, -4.0, 5.0], n))),
            TVector.Create(TPacket.Load(PadToMax([0.5, -1.0, 2.0, -0.5], n)), TPacket.Load(PadToMax([-0.5, 1.0, -2.0, 0.5], n))));
        TMatrix right = TMatrix.Create(
            TVector.Create(TPacket.Load(PadToMax([2.0, 2.0, 2.0, 2.0], n)), TPacket.Broadcast(0.0)),
            TVector.Create(TPacket.Broadcast(0.0), TPacket.Load(PadToMax([2.0, 2.0, 2.0, 2.0], n))));

        AssertMatrix(left * right,
            PadToMax([2.0, -4.0, 7.0, -9.0], n), PadToMax([4.0, 6.0, -8.0, 10.0], n),
            PadToMax([1.0, -2.0, 4.0, -1.0], n), PadToMax([-1.0, 2.0, -4.0, 1.0], n));

        TMatrix broadcast = TMatrix.Broadcast(3.0);
        double[] three = PadToMax([3.0, 3.0, 3.0, 3.0], n);
        AssertMatrix(broadcast, three, three, three, three);
    }

    [TestMethod]
    public void TestTransform()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TMatrix m = TMatrix.Create(
            TVector.Create(TPacket.Load(PadToMax([1.0, 2.0, 0.0, 3.0], n)), TPacket.Load(PadToMax([0.0, 1.0, 2.0, -1.0], n))),
            TVector.Create(TPacket.Load(PadToMax([-1.0, 0.0, 1.0, 2.0], n)), TPacket.Load(PadToMax([2.0, -1.0, 0.0, 1.0], n))));
        TVector v = TVector.Create(TPacket.Load(PadToMax([2.0, 3.0, 4.0, 5.0], n)), TPacket.Load(PadToMax([1.0, -1.0, 2.0, -2.0], n)));

        TVector result = TMatrix.Transform(m, v);
        AssertVector(result, PadToMax([2.0, 5.0, 4.0, 17.0], n), PadToMax([0.0, 1.0, 4.0, 8.0], n));

        TVector fmaResult = TMatrix.FusedMultiplyAdd(m, v, v);
        AssertVector(fmaResult, PadToMax([4.0, 8.0, 8.0, 22.0], n), PadToMax([1.0, 0.0, 6.0, 6.0], n), 2e-12);
    }

    [TestMethod]
    public void TestTranspose()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TMatrix m = TMatrix.Create(
            TVector.Create(TPacket.Load(PadToMax([1.0, 2.0, 3.0, 4.0], n)), TPacket.Load(PadToMax([5.0, 6.0, 7.0, 8.0], n))),
            TVector.Create(TPacket.Load(PadToMax([9.0, 10.0, 11.0, 12.0], n)), TPacket.Load(PadToMax([13.0, 14.0, 15.0, 16.0], n))));

        TMatrix transposed = TMatrix.Transpose(m);
        AssertMatrix(transposed,
            PadToMax([1.0, 2.0, 3.0, 4.0], n), PadToMax([9.0, 10.0, 11.0, 12.0], n),
            PadToMax([5.0, 6.0, 7.0, 8.0], n), PadToMax([13.0, 14.0, 15.0, 16.0], n));
    }

    [TestMethod]
    public void TestInverse()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TMatrix m = TMatrix.Create(
            TVector.Create(TPacket.Load(PadToMax([4.0, 7.0, 2.0, 3.0], n)), TPacket.Load(PadToMax([3.0, 5.0, 1.0, 2.0], n))),
            TVector.Create(TPacket.Load(PadToMax([1.0, 2.0, 3.0, 1.0], n)), TPacket.Load(PadToMax([1.0, 1.0, 2.0, 2.0], n))));

        TMatrix inv = TMatrix.Inverse(m);
        TMatrix product = TMatrix.Multiply(m, inv);
        AssertMatrix(product,
            PadToMax([1.0, 1.0, 1.0, 1.0], n), PadToMax([0.0, 0.0, 0.0, 0.0], n),
            PadToMax([0.0, 0.0, 0.0, 0.0], n), PadToMax([1.0, 1.0, 1.0, 1.0], n),
            5e-12);
    }

    [TestMethod]
    public void TestScale()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TMatrix scale = TMatrix.Scale(TPacket.Broadcast(2.0), TPacket.Broadcast(3.0));
        AssertMatrix(scale,
            PadToMax([2.0, 2.0, 2.0, 2.0], n), PadToMax([0.0, 0.0, 0.0, 0.0], n),
            PadToMax([0.0, 0.0, 0.0, 0.0], n), PadToMax([3.0, 3.0, 3.0, 3.0], n));
    }

    [TestMethod]
    public void TestDivide()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TMatrix m = TMatrix.Scale(TPacket.Broadcast(4.0), TPacket.Broadcast(6.0));
        TMatrix divided = TMatrix.Divide(m, TPacket.Broadcast(2.0));
        AssertMatrix(divided,
            PadToMax([2.0, 2.0, 2.0, 2.0], n), PadToMax([0.0, 0.0, 0.0, 0.0], n),
            PadToMax([0.0, 0.0, 0.0, 0.0], n), PadToMax([3.0, 3.0, 3.0, 3.0], n));
    }

    [TestMethod]
    public void TestRotate()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TPacket angle = TPacket.Load(PadToMax([Math.PI * 0.5, 0.0, Math.PI, -Math.PI * 0.5], n));
        TMatrix rot = TMatrix.Rotate(angle);

        double[] cosVals = new double[n];
        double[] sinVals = new double[n];
        double[] nxSinVals = new double[n];
        for (int i = 0; i < n; i++) { double a = PadToMax([Math.PI * 0.5, 0.0, Math.PI, -Math.PI * 0.5], n)[i]; cosVals[i] = Math.Cos(a); sinVals[i] = Math.Sin(a); nxSinVals[i] = -Math.Sin(a); }

        AssertMatrix(rot, cosVals, nxSinVals, sinVals, cosVals, 1e-12);
    }

    protected static double[] PadToMax(double[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        double[] result = new double[count];
        values.CopyTo(result, 0);
        return result;
    }

    protected static void AssertMatrix(TMatrix actual, double[] expectedR0X, double[] expectedR0Y, double[] expectedR1X, double[] expectedR1Y, double epsilon = 1e-12)
    {
        int n = TPacket.LaneCount;
        Span<double> r0x = stackalloc double[n]; Span<double> r0y = stackalloc double[n];
        Span<double> r1x = stackalloc double[n]; Span<double> r1y = stackalloc double[n];
        TPacket.Store(actual.Row0.X, r0x); TPacket.Store(actual.Row0.Y, r0y);
        TPacket.Store(actual.Row1.X, r1x); TPacket.Store(actual.Row1.Y, r1y);
        for (int i = 0; i < expectedR0X.Length; i++)
        {
            Assert.AreEqual(expectedR0X[i], r0x[i], epsilon, $"lane {i} R0X");
            Assert.AreEqual(expectedR0Y[i], r0y[i], epsilon, $"lane {i} R0Y");
            Assert.AreEqual(expectedR1X[i], r1x[i], epsilon, $"lane {i} R1X");
            Assert.AreEqual(expectedR1Y[i], r1y[i], epsilon, $"lane {i} R1Y");
        }
    }

    protected static void AssertVector(TVector actual, double[] expectedX, double[] expectedY, double epsilon = 1e-12)
    {
        int n = TPacket.LaneCount;
        Span<double> xValues = stackalloc double[n];
        Span<double> yValues = stackalloc double[n];
        TPacket.Store(actual.X, xValues);
        TPacket.Store(actual.Y, yValues);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], xValues[i], epsilon, $"lane {i} X");
            Assert.AreEqual(expectedY[i], yValues[i], epsilon, $"lane {i} Y");
        }
    }
}
