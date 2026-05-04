using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class Matrix3Float32Tests<TMatrix, TVector, TPacket, TMatrixMask, TVectorMask, TPacketMask>
    where TMatrix : unmanaged, ISimdFloatingPointMatrix3<TMatrix, TVector, TPacket, float, TMatrixMask, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdFloatingPointVector3<TVector, TPacket, float, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdFloatingPoint<TPacket, float, TPacketMask>
    where TMatrixMask : unmanaged, ISimdMatrix3Mask<TMatrixMask, TVectorMask, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector3Mask<TVectorMask, TPacketMask>
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
        AssertMatrix(identity,
            ones, zeros, zeros,
            zeros, ones, zeros,
            zeros, zeros, ones);
    }

    [TestMethod]
    public void TestArithmetic()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        float[] zeros = PadToMax([0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f], n);
        TMatrix left = TMatrix.Create(
            TVector.Create(TPacket.Load(PadToMax([1f, 2f, 3f, 4f, 1f, 1f, 1f, 1f], n)), TPacket.Broadcast(0f), TPacket.Broadcast(0f)),
            TVector.Create(TPacket.Broadcast(0f), TPacket.Load(PadToMax([2f, 3f, 4f, 5f, 1f, 1f, 1f, 1f], n)), TPacket.Broadcast(0f)),
            TVector.Create(TPacket.Broadcast(0f), TPacket.Broadcast(0f), TPacket.Load(PadToMax([3f, 4f, 5f, 6f, 1f, 1f, 1f, 1f], n))));
        TMatrix right = TMatrix.Create(
            TVector.Create(TPacket.Load(PadToMax([2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f], n)), TPacket.Broadcast(0f), TPacket.Broadcast(0f)),
            TVector.Create(TPacket.Broadcast(0f), TPacket.Load(PadToMax([3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f], n)), TPacket.Broadcast(0f)),
            TVector.Create(TPacket.Broadcast(0f), TPacket.Broadcast(0f), TPacket.Load(PadToMax([4f, 4f, 4f, 4f, 4f, 4f, 4f, 4f], n))));

        TMatrix product = TMatrix.Multiply(left, right);
        AssertMatrix(product,
            PadToMax([2f, 4f, 6f, 8f, 2f, 2f, 2f, 2f], n), zeros, zeros,
            zeros, PadToMax([6f, 9f, 12f, 15f, 3f, 3f, 3f, 3f], n), zeros,
            zeros, zeros, PadToMax([12f, 16f, 20f, 24f, 4f, 4f, 4f, 4f], n));

        TMatrix broadcast = TMatrix.Broadcast(5f);
        float[] five = PadToMax([5f, 5f, 5f, 5f, 5f, 5f, 5f, 5f], n);
        AssertMatrix(broadcast, five, five, five, five, five, five, five, five, five);
    }

    [TestMethod]
    public void TestTransform()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TMatrix m = TMatrix.Create(
            TVector.Create(TPacket.Load(PadToMax([1f, 2f, 0f, 3f, 1f, 1f, 1f, 1f], n)), TPacket.Load(PadToMax([0f, 1f, 2f, -1f, 1f, 1f, 1f, 1f], n)), TPacket.Load(PadToMax([0f, 0f, 1f, 1f, 1f, 1f, 1f, 1f], n))),
            TVector.Create(TPacket.Load(PadToMax([-1f, 0f, 1f, 2f, 1f, 1f, 1f, 1f], n)), TPacket.Load(PadToMax([2f, -1f, 0f, 1f, 1f, 1f, 1f, 1f], n)), TPacket.Load(PadToMax([0f, 1f, 0f, -1f, 1f, 1f, 1f, 1f], n))),
            TVector.Create(TPacket.Load(PadToMax([0f, 1f, 0f, -1f, 1f, 1f, 1f, 1f], n)), TPacket.Load(PadToMax([1f, 0f, -1f, 0f, 1f, 1f, 1f, 1f], n)), TPacket.Load(PadToMax([1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f], n))));
        TVector v = TVector.Create(TPacket.Load(PadToMax([2f, 3f, 4f, 5f, 1f, 1f, 1f, 1f], n)), TPacket.Load(PadToMax([1f, -1f, 2f, -2f, 1f, 1f, 1f, 1f], n)), TPacket.Load(PadToMax([3f, 2f, 1f, 0f, 1f, 1f, 1f, 1f], n)));

        TVector result = TMatrix.Transform(m, v);
        AssertVector(result,
            PadToMax([2f, 5f, 5f, 17f, 3f, 3f, 3f, 3f], n),
            PadToMax([0f, 3f, 4f, 8f, 3f, 3f, 3f, 3f], n),
            PadToMax([4f, 5f, -1f, -5f, 3f, 3f, 3f, 3f], n),
            2e-5f);
    }

    [TestMethod]
    public void TestTranspose()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        float[] zeros = PadToMax([0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f], n);
        TMatrix m = TMatrix.Create(
            TVector.Create(TPacket.Load(PadToMax([1f, 2f, 3f, 4f, 1f, 1f, 1f, 1f], n)), TPacket.Load(PadToMax([5f, 6f, 7f, 8f, 1f, 1f, 1f, 1f], n)), TPacket.Broadcast(0f)),
            TVector.Create(TPacket.Load(PadToMax([9f, 10f, 11f, 12f, 1f, 1f, 1f, 1f], n)), TPacket.Load(PadToMax([13f, 14f, 15f, 16f, 1f, 1f, 1f, 1f], n)), TPacket.Broadcast(0f)),
            TVector.Create(TPacket.Broadcast(0f), TPacket.Broadcast(0f), TPacket.Broadcast(1f)));

        TMatrix transposed = TMatrix.Transpose(m);
        AssertMatrix(transposed,
            PadToMax([1f, 2f, 3f, 4f, 1f, 1f, 1f, 1f], n), PadToMax([9f, 10f, 11f, 12f, 1f, 1f, 1f, 1f], n), zeros,
            PadToMax([5f, 6f, 7f, 8f, 1f, 1f, 1f, 1f], n), PadToMax([13f, 14f, 15f, 16f, 1f, 1f, 1f, 1f], n), zeros,
            zeros, zeros, PadToMax([1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f], n));
    }

    [TestMethod]
    public void TestScale()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        float[] zeros = PadToMax([0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f], n);
        float[] ones = PadToMax([1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f], n);
        TMatrix scale = TMatrix.Scale(TPacket.Broadcast(2f), TPacket.Broadcast(3f));
        AssertMatrix(scale,
            PadToMax([2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f], n), zeros, zeros,
            zeros, PadToMax([3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f], n), zeros,
            zeros, zeros, ones);
    }

    [TestMethod]
    public void TestTranslate()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        float[] zeros = PadToMax([0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f], n);
        float[] ones = PadToMax([1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f], n);
        TMatrix translate = TMatrix.Translate(TPacket.Broadcast(2f), TPacket.Broadcast(3f));
        AssertMatrix(translate,
            ones, zeros, PadToMax([2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f], n),
            zeros, ones, PadToMax([3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f], n),
            zeros, zeros, ones);
    }

    [TestMethod]
    public void TestDivide()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TMatrix m = TMatrix.Scale(TPacket.Broadcast(4f), TPacket.Broadcast(6f));
        TMatrix divided = TMatrix.Divide(m, TPacket.Broadcast(2f));
        float[] zeros = PadToMax([0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f], n);
        float[] ones = PadToMax([1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f], n);
        AssertMatrix(divided,
            PadToMax([2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f], n), zeros, zeros,
            zeros, PadToMax([3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f], n), zeros,
            zeros, zeros, PadToMax([0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f], n));
    }

    [TestMethod]
    public void TestRotate()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TPacket angle = TPacket.Load(PadToMax([MathF.PI * 0.5f, 0f, MathF.PI, -MathF.PI * 0.5f, 0f, 0f, 0f, 0f], n));
        TMatrix rot = TMatrix.Rotate(angle);

        TVector v = TVector.Create(TPacket.Broadcast(1f), TPacket.Broadcast(0f), TPacket.Broadcast(0f));
        TVector result = TMatrix.Transform(rot, v);

        float[] angles = PadToMax([MathF.PI * 0.5f, 0f, MathF.PI, -MathF.PI * 0.5f, 0f, 0f, 0f, 0f], n);
        float[] cosVals = new float[n];
        float[] sinVals = new float[n];
        for (int i = 0; i < n; i++) { cosVals[i] = MathF.Cos(angles[i]); sinVals[i] = MathF.Sin(angles[i]); }
        AssertVector(result, cosVals, sinVals, PadToMax([0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f], n), 2e-6f);
    }

    protected static float[] PadToMax(float[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        float[] result = new float[count];
        values.CopyTo(result, 0);
        return result;
    }

    protected static void AssertMatrix(TMatrix actual,
        float[] eR0X, float[] eR0Y, float[] eR0Z,
        float[] eR1X, float[] eR1Y, float[] eR1Z,
        float[] eR2X, float[] eR2Y, float[] eR2Z,
        float epsilon = 1e-5f)
    {
        int n = TPacket.LaneCount;
        Span<float> v = stackalloc float[n];
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

    protected static void AssertVector(TVector actual, float[] expectedX, float[] expectedY, float[] expectedZ, float epsilon = 1e-5f)
    {
        int n = TPacket.LaneCount;
        Span<float> x = stackalloc float[n]; Span<float> y = stackalloc float[n]; Span<float> z = stackalloc float[n];
        TPacket.Store(actual.X, x); TPacket.Store(actual.Y, y); TPacket.Store(actual.Z, z);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], x[i], epsilon, $"lane {i} X");
            Assert.AreEqual(expectedY[i], y[i], epsilon, $"lane {i} Y");
            Assert.AreEqual(expectedZ[i], z[i], epsilon, $"lane {i} Z");
        }
    }
}
