using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class Matrix2Float32Tests<TMatrix, TVector, TPacket, TMatrixMask, TVectorMask, TPacketMask>
    where TMatrix : unmanaged, ISimdFloatingPointMatrix2<TMatrix, TVector, TPacket, float, TMatrixMask, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdFloatingPointVector2<TVector, TPacket, float, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdFloatingPoint<TPacket, float, TPacketMask>
    where TMatrixMask : unmanaged, ISimdMatrix2Mask<TMatrixMask, TVectorMask, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector2Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, float>
{
    protected abstract bool IsHardwareSupported { get; }
    protected abstract uint AlignmentMask { get; }

    [TestMethod]
    public void TestIdentity()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TMatrix identity = TMatrix.Identity;
        float[] ones = PadToMax([1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f], n);
        float[] zeros = PadToMax([0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f], n);
        AssertMatrix(identity, ones, zeros, zeros, ones);

        TVector v = TVector.Create(TPacket.Broadcast(2f), TPacket.Broadcast(3f));
        TVector transformed = TMatrix.Transform(identity, v);
        AssertVector(transformed, PadToMax([2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f], n), PadToMax([3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f], n));
    }

    [TestMethod]
    public void TestArithmetic()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TMatrix left = TMatrix.Create(
            TVector.Create(TPacket.Load(PadToMax([1f, -2f, 3.5f, -4.5f, 1f, 1f, 1f, 1f], n)), TPacket.Load(PadToMax([2f, 3f, -4f, 5f, 1f, 1f, 1f, 1f], n))),
            TVector.Create(TPacket.Load(PadToMax([0.5f, -1f, 2f, -0.5f, 1f, 1f, 1f, 1f], n)), TPacket.Load(PadToMax([-0.5f, 1f, -2f, 0.5f, 1f, 1f, 1f, 1f], n))));
        TMatrix right = TMatrix.Create(
            TVector.Create(TPacket.Load(PadToMax([2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f], n)), TPacket.Broadcast(0f)),
            TVector.Create(TPacket.Broadcast(0f), TPacket.Load(PadToMax([2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f], n))));

        AssertMatrix(left * right,
            PadToMax([2f, -4f, 7f, -9f, 2f, 2f, 2f, 2f], n), PadToMax([4f, 6f, -8f, 10f, 2f, 2f, 2f, 2f], n),
            PadToMax([1f, -2f, 4f, -1f, 2f, 2f, 2f, 2f], n), PadToMax([-1f, 2f, -4f, 1f, 2f, 2f, 2f, 2f], n));

        TMatrix broadcast = TMatrix.Broadcast(3f);
        float[] three = PadToMax([3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f], n);
        AssertMatrix(broadcast, three, three, three, three);
    }

    [TestMethod]
    public void TestTransform()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TMatrix m = TMatrix.Create(
            TVector.Create(TPacket.Load(PadToMax([1f, 2f, 0f, 3f, 1f, 1f, 1f, 1f], n)), TPacket.Load(PadToMax([0f, 1f, 2f, -1f, 1f, 1f, 1f, 1f], n))),
            TVector.Create(TPacket.Load(PadToMax([-1f, 0f, 1f, 2f, 1f, 1f, 1f, 1f], n)), TPacket.Load(PadToMax([2f, -1f, 0f, 1f, 1f, 1f, 1f, 1f], n))));
        TVector v = TVector.Create(TPacket.Load(PadToMax([2f, 3f, 4f, 5f, 1f, 1f, 1f, 1f], n)), TPacket.Load(PadToMax([1f, -1f, 2f, -2f, 1f, 1f, 1f, 1f], n)));

        TVector result = TMatrix.Transform(m, v);
        AssertVector(result, PadToMax([2f, 5f, 4f, 17f, 2f, 2f, 2f, 2f], n), PadToMax([0f, 1f, 4f, 8f, 2f, 2f, 2f, 2f], n));

        TVector fmaResult = TMatrix.FusedMultiplyAdd(m, v, v);
        AssertVector(fmaResult, PadToMax([4f, 8f, 8f, 22f, 3f, 3f, 3f, 3f], n), PadToMax([1f, 0f, 6f, 6f, 3f, 3f, 3f, 3f], n), 2e-5f);
    }

    [TestMethod]
    public void TestTranspose()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TMatrix m = TMatrix.Create(
            TVector.Create(TPacket.Load(PadToMax([1f, 2f, 3f, 4f, 1f, 1f, 1f, 1f], n)), TPacket.Load(PadToMax([5f, 6f, 7f, 8f, 1f, 1f, 1f, 1f], n))),
            TVector.Create(TPacket.Load(PadToMax([9f, 10f, 11f, 12f, 1f, 1f, 1f, 1f], n)), TPacket.Load(PadToMax([13f, 14f, 15f, 16f, 1f, 1f, 1f, 1f], n))));

        TMatrix transposed = TMatrix.Transpose(m);
        AssertMatrix(transposed,
            PadToMax([1f, 2f, 3f, 4f, 1f, 1f, 1f, 1f], n), PadToMax([9f, 10f, 11f, 12f, 1f, 1f, 1f, 1f], n),
            PadToMax([5f, 6f, 7f, 8f, 1f, 1f, 1f, 1f], n), PadToMax([13f, 14f, 15f, 16f, 1f, 1f, 1f, 1f], n));
    }

    [TestMethod]
    public void TestInverse()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TMatrix m = TMatrix.Create(
            TVector.Create(TPacket.Load(PadToMax([4f, 7f, 2f, 3f, 2f, 2f, 2f, 2f], n)), TPacket.Load(PadToMax([3f, 5f, 1f, 2f, 1f, 1f, 1f, 1f], n))),
            TVector.Create(TPacket.Load(PadToMax([1f, 2f, 3f, 1f, 1f, 1f, 1f, 1f], n)), TPacket.Load(PadToMax([1f, 1f, 2f, 1f, 2f, 2f, 2f, 2f], n))));

        TMatrix inv = TMatrix.Inverse(m);
        TMatrix product = TMatrix.Multiply(m, inv);
        AssertMatrix(product,
            PadToMax([1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f], n), PadToMax([0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f], n),
            PadToMax([0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f], n), PadToMax([1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f], n),
            5e-5f);
    }

    [TestMethod]
    public void TestScale()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TMatrix scale = TMatrix.Scale(TPacket.Broadcast(2f), TPacket.Broadcast(3f));
        AssertMatrix(scale,
            PadToMax([2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f], n), PadToMax([0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f], n),
            PadToMax([0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f], n), PadToMax([3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f], n));
    }

    [TestMethod]
    public void TestDivide()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TMatrix m = TMatrix.Scale(TPacket.Broadcast(4f), TPacket.Broadcast(6f));
        TMatrix divided = TMatrix.Divide(m, TPacket.Broadcast(2f));
        AssertMatrix(divided,
            PadToMax([2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f], n), PadToMax([0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f], n),
            PadToMax([0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f], n), PadToMax([3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f], n));
    }

    [TestMethod]
    public void TestRotate()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TPacket angle = TPacket.Load(PadToMax([MathF.PI * 0.5f, 0f, MathF.PI, -MathF.PI * 0.5f, 0f, 0f, 0f, 0f], n));
        TMatrix rot = TMatrix.Rotate(angle);

        float[] cosVals = new float[n];
        float[] sinVals = new float[n];
        float[] nxSinVals = new float[n];
        for (int i = 0; i < n; i++) { float a = PadToMax([MathF.PI * 0.5f, 0f, MathF.PI, -MathF.PI * 0.5f, 0f, 0f, 0f, 0f], n)[i]; cosVals[i] = MathF.Cos(a); sinVals[i] = MathF.Sin(a); nxSinVals[i] = -MathF.Sin(a); }

        AssertMatrix(rot, cosVals, nxSinVals, sinVals, cosVals, 2e-6f);
    }

    protected static float[] PadToMax(float[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        float[] result = new float[count];
        values.CopyTo(result, 0);
        return result;
    }

    protected static void AssertMatrix(TMatrix actual, float[] expectedR0X, float[] expectedR0Y, float[] expectedR1X, float[] expectedR1Y, float epsilon = 1e-5f)
    {
        int n = TPacket.LaneCount;
        Span<float> r0x = stackalloc float[n]; Span<float> r0y = stackalloc float[n];
        Span<float> r1x = stackalloc float[n]; Span<float> r1y = stackalloc float[n];
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

    protected static void AssertVector(TVector actual, float[] expectedX, float[] expectedY, float epsilon = 1e-5f)
    {
        int n = TPacket.LaneCount;
        Span<float> xValues = stackalloc float[n];
        Span<float> yValues = stackalloc float[n];
        TPacket.Store(actual.X, xValues);
        TPacket.Store(actual.Y, yValues);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], xValues[i], epsilon, $"lane {i} X");
            Assert.AreEqual(expectedY[i], yValues[i], epsilon, $"lane {i} Y");
        }
    }
}
