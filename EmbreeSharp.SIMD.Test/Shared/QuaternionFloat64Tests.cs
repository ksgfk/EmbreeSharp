using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class QuaternionFloat64Tests<TQuaternion, TVector3, TVector4, TPacket, TQuaternionMask, TVector3Mask, TVector4Mask, TPacketMask>
    where TQuaternion : unmanaged, ISimdQuaternion<TQuaternion, TVector3, TVector4, TPacket, double, TQuaternionMask, TVector3Mask, TVector4Mask, TPacketMask>
    where TVector3 : unmanaged, ISimdFloatingPointVector3<TVector3, TPacket, double, TVector3Mask, TPacketMask>
    where TVector4 : unmanaged, ISimdFloatingPointVector4<TVector4, TPacket, double, TVector4Mask, TPacketMask>
    where TPacket : unmanaged, ISimdFloatingPoint<TPacket, double, TPacketMask>
    where TQuaternionMask : unmanaged, ISimdQuaternionMask<TQuaternionMask, TPacketMask>
    where TVector3Mask : unmanaged, ISimdVector3Mask<TVector3Mask, TPacketMask>
    where TVector4Mask : unmanaged, ISimdVector4Mask<TVector4Mask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, double>
{
    protected abstract bool IsHardwareSupported { get; }
    protected abstract uint AlignmentMask { get; }

    [TestMethod]
    public void TestIdentity()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TQuaternion identity = TQuaternion.Identity;
        double[] zeros = PadToMax([0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0], n);
        double[] ones = PadToMax([1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0, 1.0], n);
        AssertQuaternion(identity, zeros, zeros, zeros, ones);
    }

    [TestMethod]
    public void TestConjugate()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TQuaternion q = TQuaternion.Create(
            TPacket.Load(PadToMax([1.0, 2.0, 3.0, 4.0, 1.0, 1.0, 1.0, 1.0], n)),
            TPacket.Load(PadToMax([5.0, 6.0, 7.0, 8.0, 1.0, 1.0, 1.0, 1.0], n)),
            TPacket.Load(PadToMax([9.0, 10.0, 11.0, 12.0, 1.0, 1.0, 1.0, 1.0], n)),
            TPacket.Load(PadToMax([-1.0, -2.0, -3.0, -4.0, 1.0, 1.0, 1.0, 1.0], n)));

        TQuaternion conj = TQuaternion.Conjugate(q);
        AssertQuaternion(conj,
            PadToMax([-1.0, -2.0, -3.0, -4.0, -1.0, -1.0, -1.0, -1.0], n),
            PadToMax([-5.0, -6.0, -7.0, -8.0, -1.0, -1.0, -1.0, -1.0], n),
            PadToMax([-9.0, -10.0, -11.0, -12.0, -1.0, -1.0, -1.0, -1.0], n),
            PadToMax([-1.0, -2.0, -3.0, -4.0, 1.0, 1.0, 1.0, 1.0], n));
    }

    protected static double[] PadToMax(double[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        double[] result = new double[count];
        values.CopyTo(result, 0);
        return result;
    }

    protected static void AssertQuaternion(TQuaternion actual, double[] expectedX, double[] expectedY, double[] expectedZ, double[] expectedW, double epsilon = 1e-12)
    {
        int n = TPacket.LaneCount;
        Span<double> x = stackalloc double[n]; Span<double> y = stackalloc double[n];
        Span<double> z = stackalloc double[n]; Span<double> w = stackalloc double[n];
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
}
