using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class QuaternionFloat32Tests<TQuaternion, TVector3, TVector4, TPacket, TQuaternionMask, TVector3Mask, TVector4Mask, TPacketMask>
    where TQuaternion : unmanaged, ISimdQuaternion<TQuaternion, TVector3, TVector4, TPacket, float, TQuaternionMask, TVector3Mask, TVector4Mask, TPacketMask>
    where TVector3 : unmanaged, ISimdFloatingPointVector3<TVector3, TPacket, float, TVector3Mask, TPacketMask>
    where TVector4 : unmanaged, ISimdFloatingPointVector4<TVector4, TPacket, float, TVector4Mask, TPacketMask>
    where TPacket : unmanaged, ISimdFloatingPoint<TPacket, float, TPacketMask>
    where TQuaternionMask : unmanaged, ISimdQuaternionMask<TQuaternionMask, TPacketMask>
    where TVector3Mask : unmanaged, ISimdVector3Mask<TVector3Mask, TPacketMask>
    where TVector4Mask : unmanaged, ISimdVector4Mask<TVector4Mask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, float>
{
    protected abstract bool IsHardwareSupported { get; }
    protected abstract uint AlignmentMask { get; }

    [TestMethod]
    public void TestIdentity()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TQuaternion identity = TQuaternion.Identity;
        float[] zeros = PadToMax([0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f], n);
        float[] ones = PadToMax([1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f], n);
        AssertQuaternion(identity, zeros, zeros, zeros, ones);
    }

    [TestMethod]
    public void TestArithmetic()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TQuaternion left = TQuaternion.Create(
            TPacket.Load(PadToMax([1f, -2f, 3.5f, -4.5f, 1f, 1f, 1f, 1f], n)),
            TPacket.Load(PadToMax([2f, 3f, -4f, 5f, 1f, 1f, 1f, 1f], n)),
            TPacket.Load(PadToMax([0.5f, -1f, 2f, -0.5f, 1f, 1f, 1f, 1f], n)),
            TPacket.Load(PadToMax([-0.5f, 1f, -2f, 0.5f, 1f, 1f, 1f, 1f], n)));
        TQuaternion right = TQuaternion.Create(
            TPacket.Load(PadToMax([0.5f, -1f, 2f, -0.5f, 1f, 1f, 1f, 1f], n)),
            TPacket.Load(PadToMax([-0.5f, 1f, -2f, 0.5f, 1f, 1f, 1f, 1f], n)),
            TPacket.Load(PadToMax([1f, -2f, 3.5f, -4.5f, 1f, 1f, 1f, 1f], n)),
            TPacket.Load(PadToMax([2f, 3f, -4f, 5f, 1f, 1f, 1f, 1f], n)));

        AssertQuaternion(left + right,
            PadToMax([1.5f, -3f, 5.5f, -5f, 2f, 2f, 2f, 2f], n),
            PadToMax([1.5f, 4f, -6f, 5.5f, 2f, 2f, 2f, 2f], n),
            PadToMax([1.5f, -3f, 5.5f, -5f, 2f, 2f, 2f, 2f], n),
            PadToMax([1.5f, 4f, -6f, 5.5f, 2f, 2f, 2f, 2f], n));

        AssertQuaternion(-left,
            PadToMax([-1f, 2f, -3.5f, 4.5f, -1f, -1f, -1f, -1f], n),
            PadToMax([-2f, -3f, 4f, -5f, -1f, -1f, -1f, -1f], n),
            PadToMax([-0.5f, 1f, -2f, 0.5f, -1f, -1f, -1f, -1f], n),
            PadToMax([0.5f, -1f, 2f, -0.5f, -1f, -1f, -1f, -1f], n));

        TQuaternion broadcast = TQuaternion.Broadcast(3f);
        float[] three = PadToMax([3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f], n);
        float[] zero = PadToMax([0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f], n);
        AssertQuaternion(broadcast, zero, zero, zero, three);

        TPacketMask alternateLaneMask = TPacketMask.Load(PadToMaxBool([true, false, true, false, true, false, true, false], n));
        TQuaternionMask mask = TQuaternionMask.Create(alternateLaneMask, alternateLaneMask, alternateLaneMask, alternateLaneMask);
        AssertQuaternion(TQuaternion.Select(mask, left, right),
            PadToMax([1f, -1f, 3.5f, -0.5f, 1f, 1f, 1f, 1f], n),
            PadToMax([2f, 1f, -4f, 0.5f, 1f, 1f, 1f, 1f], n),
            PadToMax([0.5f, -2f, 2f, -4.5f, 1f, 1f, 1f, 1f], n),
            PadToMax([-0.5f, 3f, -2f, 5f, 1f, 1f, 1f, 1f], n));
    }

    [TestMethod]
    public void TestConjugate()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TQuaternion q = TQuaternion.Create(
            TPacket.Load(PadToMax([1f, 2f, 3f, 4f, 1f, 1f, 1f, 1f], n)),
            TPacket.Load(PadToMax([5f, 6f, 7f, 8f, 1f, 1f, 1f, 1f], n)),
            TPacket.Load(PadToMax([9f, 10f, 11f, 12f, 1f, 1f, 1f, 1f], n)),
            TPacket.Load(PadToMax([-1f, -2f, -3f, -4f, 1f, 1f, 1f, 1f], n)));

        TQuaternion conj = TQuaternion.Conjugate(q);
        AssertQuaternion(conj,
            PadToMax([-1f, -2f, -3f, -4f, -1f, -1f, -1f, -1f], n),
            PadToMax([-5f, -6f, -7f, -8f, -1f, -1f, -1f, -1f], n),
            PadToMax([-9f, -10f, -11f, -12f, -1f, -1f, -1f, -1f], n),
            PadToMax([-1f, -2f, -3f, -4f, 1f, 1f, 1f, 1f], n));
    }

    [TestMethod]
    public void TestNormAndNormalize()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TQuaternion q = TQuaternion.Create(
            TPacket.Load(PadToMax([1f, 2f, 0f, 3f, 1f, 1f, 1f, 1f], n)),
            TPacket.Load(PadToMax([2f, 3f, 4f, 4f, 1f, 1f, 1f, 1f], n)),
            TPacket.Load(PadToMax([2f, 6f, 0f, 0f, 1f, 1f, 1f, 1f], n)),
            TPacket.Load(PadToMax([1f, 4f, 3f, 5f, 1f, 1f, 1f, 1f], n)));

        // |q| = sqrt(x^2 + y^2 + z^2 + w^2)
        float[] xs = PadToMax([1f, 2f, 0f, 3f, 1f, 1f, 1f, 1f], n);
        float[] ys = PadToMax([2f, 3f, 4f, 4f, 1f, 1f, 1f, 1f], n);
        float[] zs = PadToMax([2f, 6f, 0f, 0f, 1f, 1f, 1f, 1f], n);
        float[] ws = PadToMax([1f, 4f, 3f, 5f, 1f, 1f, 1f, 1f], n);
        float[] sqNorms = new float[n];
        float[] norms = new float[n];
        for (int i = 0; i < n; i++) { sqNorms[i] = xs[i] * xs[i] + ys[i] * ys[i] + zs[i] * zs[i] + ws[i] * ws[i]; norms[i] = MathF.Sqrt(sqNorms[i]); }

        TPacket sqNorm = TQuaternion.SquaredNorm(q);
        Span<float> sqResult = stackalloc float[n];
        TPacket.Store(sqNorm, sqResult);
        for (int i = 0; i < n; i++) Assert.AreEqual(sqNorms[i], sqResult[i], 1e-5f, $"lane {i}");

        TPacket norm = TQuaternion.Norm(q);
        Span<float> normResult = stackalloc float[n];
        TPacket.Store(norm, normResult);
        for (int i = 0; i < n; i++) Assert.AreEqual(norms[i], normResult[i], 1e-5f, $"lane {i}");

        TQuaternion normalized = TQuaternion.Normalize(q);
        float[] nx = new float[n]; float[] ny = new float[n]; float[] nz = new float[n]; float[] nw = new float[n];
        for (int i = 0; i < n; i++) { float invN = 1f / norms[i]; nx[i] = xs[i] * invN; ny[i] = ys[i] * invN; nz[i] = zs[i] * invN; nw[i] = ws[i] * invN; }
        AssertQuaternion(normalized, nx, ny, nz, nw, 2e-6f);
    }

    [TestMethod]
    public void TestMultiply()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TQuaternion a = TQuaternion.Create(
            TPacket.Load(PadToMax([1f, 2f, 0f, 3f, 1f, 1f, 1f, 1f], n)), TPacket.Load(PadToMax([0f, 1f, 2f, -1f, 1f, 1f, 1f, 1f], n)),
            TPacket.Load(PadToMax([0f, 0f, 1f, 2f, 1f, 1f, 1f, 1f], n)), TPacket.Load(PadToMax([1f, -1f, 0f, 1f, 1f, 1f, 1f, 1f], n)));
        TQuaternion b = TQuaternion.Create(
            TPacket.Load(PadToMax([2f, -1f, 1f, 0f, 1f, 1f, 1f, 1f], n)), TPacket.Load(PadToMax([1f, 2f, -1f, 1f, 1f, 1f, 1f, 1f], n)),
            TPacket.Load(PadToMax([0f, 1f, 2f, -1f, 1f, 1f, 1f, 1f], n)), TPacket.Load(PadToMax([1f, 0f, -1f, 2f, 1f, 1f, 1f, 1f], n)));

        TQuaternion product = TQuaternion.Multiply(a, b);
        ProductResult prod = ComputeProduct(PadToMax([1f, 2f, 0f, 3f, 1f, 1f, 1f, 1f], n), PadToMax([0f, 1f, 2f, -1f, 1f, 1f, 1f, 1f], n), PadToMax([0f, 0f, 1f, 2f, 1f, 1f, 1f, 1f], n), PadToMax([1f, -1f, 0f, 1f, 1f, 1f, 1f, 1f], n), PadToMax([2f, -1f, 1f, 0f, 1f, 1f, 1f, 1f], n), PadToMax([1f, 2f, -1f, 1f, 1f, 1f, 1f, 1f], n), PadToMax([0f, 1f, 2f, -1f, 1f, 1f, 1f, 1f], n), PadToMax([1f, 0f, -1f, 2f, 1f, 1f, 1f, 1f], n), n);
        AssertQuaternion(product, prod.X, prod.Y, prod.Z, prod.W, 2e-5f);
    }

    [TestMethod]
    public void TestRotateAndApply()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TVector3 axis = TVector3.Create(TPacket.Broadcast(0f), TPacket.Broadcast(0f), TPacket.Broadcast(1f));
        TPacket angle = TPacket.Load(PadToMax([MathF.PI * 0.5f, 0f, MathF.PI, -MathF.PI * 0.5f, 0f, 0f, 0f, 0f], n));
        TQuaternion rot = TQuaternion.Rotate(axis, angle);

        TVector3 v = TVector3.Create(TPacket.Broadcast(1f), TPacket.Broadcast(0f), TPacket.Broadcast(0f));
        TVector3 result = TQuaternion.Apply(rot, v);
        AssertVector3(result,
            PadToMax([0f, 1f, -1f, 0f, 1f, 1f, 1f, 1f], n),
            PadToMax([1f, 0f, 0f, -1f, 0f, 0f, 0f, 0f], n),
            PadToMax([0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f], n),
            2e-6f);
    }

    private readonly record struct ProductResult(float[] X, float[] Y, float[] Z, float[] W);

    private static ProductResult ComputeProduct(float[] ax, float[] ay, float[] az, float[] aw, float[] bx, float[] by, float[] bz, float[] bw, int n)
    {
        float[] rx = new float[n]; float[] ry = new float[n]; float[] rz = new float[n]; float[] rw = new float[n];
        for (int i = 0; i < n; i++)
        {
            float aX = ax[i], aY = ay[i], aZ = az[i], aW = aw[i];
            float bX = bx[i], bY = by[i], bZ = bz[i], bW = bw[i];
            rx[i] = aW * bX + aX * bW + aY * bZ - aZ * bY;
            ry[i] = aW * bY - aX * bZ + aY * bW + aZ * bX;
            rz[i] = aW * bZ + aX * bY - aY * bX + aZ * bW;
            rw[i] = aW * bW - aX * bX - aY * bY - aZ * bZ;
        }
        return new ProductResult(rx, ry, rz, rw);
    }

    protected static float[] PadToMax(float[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        float[] result = new float[count];
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

    protected static void AssertQuaternion(TQuaternion actual, float[] expectedX, float[] expectedY, float[] expectedZ, float[] expectedW, float epsilon = 1e-5f)
    {
        int n = TPacket.LaneCount;
        Span<float> x = stackalloc float[n]; Span<float> y = stackalloc float[n];
        Span<float> z = stackalloc float[n]; Span<float> w = stackalloc float[n];
        TPacket.Store(actual.X, x); TPacket.Store(actual.Y, y);
        TPacket.Store(actual.Z, z); TPacket.Store(actual.W, w);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], x[i], epsilon, $"lane {i} X");
            Assert.AreEqual(expectedY[i], y[i], epsilon, $"lane {i} Y");
            Assert.AreEqual(expectedZ[i], z[i], epsilon, $"lane {i} Z");
            Assert.AreEqual(expectedW[i], w[i], epsilon, $"lane {i} W");
        }
    }

    protected static void AssertVector3(TVector3 actual, float[] expectedX, float[] expectedY, float[] expectedZ, float epsilon = 1e-5f)
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
