using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class Vector3Float64Tests<TVector, TPacket, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdFloatingPointVector3<TVector, TPacket, double, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdFloatingPoint<TPacket, double, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector3Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, double>
{
    protected abstract bool IsHardwareSupported { get; }
    protected abstract uint AlignmentMask { get; }

    [TestMethod]
    public void TestArithmetic()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        double[] leftX = PadToMax([1d, -2d, 3.5d, -4.5d], n);
        double[] leftY = PadToMax([2d, 3d, -4d, 5d], n);
        double[] leftZ = PadToMax([0.5d, -1d, 2d, -0.5d], n);
        double[] rightX = PadToMax([0.5d, -1d, 2d, -0.5d], n);
        double[] rightY = PadToMax([-0.5d, 1d, -2d, 0.5d], n);
        double[] rightZ = PadToMax([1d, -2d, 3.5d, -4.5d], n);

        TVector left = TVector.Create(TPacket.Load(leftX), TPacket.Load(leftY), TPacket.Load(leftZ));
        TVector right = TVector.Create(TPacket.Load(rightX), TPacket.Load(rightY), TPacket.Load(rightZ));

        double[] addX = PadToMax([1.5d, -3d, 5.5d, -5d], n);
        double[] addY = PadToMax([1.5d, 4d, -6d, 5.5d], n);
        double[] addZ = PadToMax([1.5d, -3d, 5.5d, -5d], n);
        AssertVector(left + right, addX, addY, addZ);

        double[] subX = PadToMax([0.5d, -1d, 1.5d, -4d], n);
        double[] subY = PadToMax([2.5d, 2d, -2d, 4.5d], n);
        double[] subZ = PadToMax([-0.5d, 1d, -1.5d, 4d], n);
        AssertVector(left - right, subX, subY, subZ);

        AssertVector(left * right, PadToMax([0.5d, 2d, 7d, 2.25d], n), PadToMax([-1d, 3d, 8d, 2.5d], n), PadToMax([0.5d, 2d, 7d, 2.25d], n));
        AssertVector(-left, PadToMax([-1d, 2d, -3.5d, 4.5d], n), PadToMax([-2d, -3d, 4d, -5d], n), PadToMax([-0.5d, 1d, -2d, 0.5d], n));

        TVector broadcast = TVector.Broadcast(3d);
        AssertVector(broadcast, PadToMax([3d, 3d, 3d, 3d], n), PadToMax([3d, 3d, 3d, 3d], n), PadToMax([3d, 3d, 3d, 3d], n));

        TPacketMask alternateLaneMask = TPacketMask.Load(PadToMaxBool([true, false, true, false], n));
        TVectorMask mask = TVectorMask.Create(alternateLaneMask, alternateLaneMask, alternateLaneMask);
        AssertVector(TVector.Select(mask, left, right), PadToMax([1d, -1d, 3.5d, -0.5d], n), PadToMax([2d, 1d, -4d, 0.5d], n), PadToMax([0.5d, -2d, 2d, -4.5d], n));

        var leftCopy = left;
        TVectorMask eqMask = left == leftCopy;
        bool[] allTrue = PadToMaxBool([true, true, true, true], n);
        AssertVectorMask(eqMask, allTrue, allTrue, allTrue);
    }

    [TestMethod]
    public void TestDot()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TVector left = TVector.Create(TPacket.Load(PadToMax([1d, 2d, 3d, 4d], n)), TPacket.Load(PadToMax([2d, 3d, 4d, 5d], n)), TPacket.Load(PadToMax([0.5d, 1d, 2d, -1d], n)));
        TVector right = TVector.Create(TPacket.Load(PadToMax([0.5d, 1d, -1d, 2d], n)), TPacket.Load(PadToMax([-0.5d, -1d, 2d, -2d], n)), TPacket.Load(PadToMax([1d, 0.5d, -0.5d, 1d], n)));

        double[] expected = PadToMax([0d, -0.5d, 4d, -3d], n);
        TPacket dot = TVector.Dot(left, right);
        Span<double> values = stackalloc double[n];
        TPacket.Store(dot, values);
        for (int i = 0; i < n; i++)
            Assert.AreEqual(expected[i], values[i], 1e-12d, $"lane {i}");
    }

    [TestMethod]
    public void TestCross()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TVector xAxis = TVector.Create(TPacket.Broadcast(1d), TPacket.Broadcast(0d), TPacket.Broadcast(0d));
        TVector yAxis = TVector.Create(TPacket.Broadcast(0d), TPacket.Broadcast(1d), TPacket.Broadcast(0d));

        TVector cross = TVector.Cross(xAxis, yAxis);
        double[] zeroExpected = PadToMax([0d, 0d, 0d, 0d], n);
        double[] oneExpected = PadToMax([1d, 1d, 1d, 1d], n);
        AssertVector(cross, zeroExpected, zeroExpected, oneExpected);

        TVector a = TVector.Create(TPacket.Load(PadToMax([2d, 1d, 0d, 3d], n)), TPacket.Load(PadToMax([3d, -2d, 5d, 1d], n)), TPacket.Load(PadToMax([4d, 3d, -2d, -1d], n)));
        TVector b = TVector.Create(TPacket.Load(PadToMax([1d, -1d, 3d, 2d], n)), TPacket.Load(PadToMax([-2d, 4d, 1d, -1d], n)), TPacket.Load(PadToMax([3d, 2d, -1d, 4d], n)));

        double[] crossX = new double[n];
        double[] crossY = new double[n];
        double[] crossZ = new double[n];
        double[] ax = PadToMax([2d, 1d, 0d, 3d], n);
        double[] ay = PadToMax([3d, -2d, 5d, 1d], n);
        double[] az = PadToMax([4d, 3d, -2d, -1d], n);
        double[] bx = PadToMax([1d, -1d, 3d, 2d], n);
        double[] by = PadToMax([-2d, 4d, 1d, -1d], n);
        double[] bz = PadToMax([3d, 2d, -1d, 4d], n);
        for (int i = 0; i < n; i++) { crossX[i] = ay[i] * bz[i] - az[i] * by[i]; crossY[i] = az[i] * bx[i] - ax[i] * bz[i]; crossZ[i] = ax[i] * by[i] - ay[i] * bx[i]; }

        TVector result = TVector.Cross(a, b);
        AssertVector(result, crossX, crossY, crossZ, 2e-12d);
    }

    protected static double[] PadToMax(double[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        double[] result = new double[count];
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

    protected static void AssertVector(TVector actual, double[] expectedX, double[] expectedY, double[] expectedZ, double epsilon = 1e-12d)
    {
        int n = TPacket.LaneCount;
        Span<double> xValues = stackalloc double[n];
        Span<double> yValues = stackalloc double[n];
        Span<double> zValues = stackalloc double[n];
        TPacket.Store(actual.X, xValues);
        TPacket.Store(actual.Y, yValues);
        TPacket.Store(actual.Z, zValues);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], xValues[i], epsilon, $"lane {i} X");
            Assert.AreEqual(expectedY[i], yValues[i], epsilon, $"lane {i} Y");
            Assert.AreEqual(expectedZ[i], zValues[i], epsilon, $"lane {i} Z");
        }
    }

    protected static void AssertVectorMask(TVectorMask actual, bool[] expectedX, bool[] expectedY, bool[] expectedZ)
    {
        int n = TPacket.LaneCount;
        Span<double> xValues = stackalloc double[n];
        Span<double> yValues = stackalloc double[n];
        Span<double> zValues = stackalloc double[n];
        TPacketMask.Store(actual.X, xValues);
        TPacketMask.Store(actual.Y, yValues);
        TPacketMask.Store(actual.Z, zValues);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], BitConverter.DoubleToInt64Bits(xValues[i]) != 0, $"lane {i} X");
            Assert.AreEqual(expectedY[i], BitConverter.DoubleToInt64Bits(yValues[i]) != 0, $"lane {i} Y");
            Assert.AreEqual(expectedZ[i], BitConverter.DoubleToInt64Bits(zValues[i]) != 0, $"lane {i} Z");
        }
    }
}
