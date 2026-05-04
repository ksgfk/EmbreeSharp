using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class Vector4Float64Tests<TVector, TPacket, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdFloatingPointVector4<TVector, TPacket, double, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdFloatingPoint<TPacket, double, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector4Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, double>
{
    protected abstract bool IsHardwareSupported { get; }
    protected abstract uint AlignmentMask { get; }

    [TestMethod]
    public void TestArithmetic()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        double[] leftX = PadToMax([1.0, -2.0, 3.5, -4.5, 5.25, -6.75, 8.0, -9.0], n);
        double[] leftY = PadToMax([2.0, 3.0, -4.0, 5.0, -6.0, 7.0, -8.0, 9.0], n);
        double[] leftZ = PadToMax([0.5, -1.0, 2.0, -0.5, 1.0, -1.5, 0.25, -2.0], n);
        double[] leftW = PadToMax([-0.5, 1.0, -2.0, 0.5, -1.0, 1.5, -0.25, 2.0], n);
        double[] rightX = PadToMax([0.5, -1.0, 2.0, -0.5, 1.0, -1.5, 0.25, -2.0], n);
        double[] rightY = PadToMax([-0.5, 1.0, -2.0, 0.5, -1.0, 1.5, -0.25, 2.0], n);
        double[] rightZ = PadToMax([1.0, -2.0, 3.5, -4.5, 5.25, -6.75, 8.0, -9.0], n);
        double[] rightW = PadToMax([2.0, 3.0, -4.0, 5.0, -6.0, 7.0, -8.0, 9.0], n);

        TVector left = TVector.Create(TPacket.Load(leftX), TPacket.Load(leftY), TPacket.Load(leftZ), TPacket.Load(leftW));
        TVector right = TVector.Create(TPacket.Load(rightX), TPacket.Load(rightY), TPacket.Load(rightZ), TPacket.Load(rightW));

        AssertVector(left + right, PadToMax([1.5, -3.0, 5.5, -5.0, 6.25, -8.25, 8.25, -11.0], n), PadToMax([1.5, 4.0, -6.0, 5.5, -7.0, 8.5, -8.25, 11.0], n), PadToMax([1.5, -3.0, 5.5, -5.0, 6.25, -8.25, 8.25, -11.0], n), PadToMax([1.5, 4.0, -6.0, 5.5, -7.0, 8.5, -8.25, 11.0], n));
        AssertVector(left - right, PadToMax([0.5, -1.0, 1.5, -4.0, 4.25, -5.25, 7.75, -7.0], n), PadToMax([2.5, 2.0, -2.0, 4.5, -5.0, 5.5, -7.75, 7.0], n), PadToMax([-0.5, 1.0, -1.5, 4.0, -4.25, 5.25, -7.75, 7.0], n), PadToMax([-2.5, -2.0, 2.0, -4.5, 5.0, -5.5, 7.75, -7.0], n));
        AssertVector(left * right, PadToMax([0.5, 2.0, 7.0, 2.25, 5.25, 10.125, 2.0, 18.0], n), PadToMax([-1.0, 3.0, 8.0, 2.5, 6.0, 10.5, 2.0, 18.0], n), PadToMax([0.5, 2.0, 7.0, 2.25, 5.25, 10.125, 2.0, 18.0], n), PadToMax([-1.0, 3.0, 8.0, 2.5, 6.0, 10.5, 2.0, 18.0], n));
        AssertVector(-left, PadToMax([-1.0, 2.0, -3.5, 4.5, -5.25, 6.75, -8.0, 9.0], n), PadToMax([-2.0, -3.0, 4.0, -5.0, 6.0, -7.0, 8.0, -9.0], n), PadToMax([-0.5, 1.0, -2.0, 0.5, -1.0, 1.5, -0.25, 2.0], n), PadToMax([0.5, -1.0, 2.0, -0.5, 1.0, -1.5, 0.25, -2.0], n));

        TVector broadcast = TVector.Broadcast(3.0);
        double[] three = PadToMax([3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0], n);
        AssertVector(broadcast, three, three, three, three);

        TPacketMask alternateLaneMask = TPacketMask.Load(PadToMaxBool([true, false, true, false, true, false, true, false], n));
        TVectorMask mask = TVectorMask.Create(alternateLaneMask, alternateLaneMask, alternateLaneMask, alternateLaneMask);
        AssertVector(TVector.Select(mask, left, right), PadToMax([1.0, -1.0, 3.5, -0.5, 5.25, -1.5, 8.0, -2.0], n), PadToMax([2.0, 1.0, -4.0, 0.5, -6.0, 1.5, -8.0, 2.0], n), PadToMax([0.5, -2.0, 2.0, -4.5, 1.0, -6.75, 0.25, -9.0], n), PadToMax([-0.5, 3.0, -2.0, 5.0, -1.0, 7.0, -0.25, 9.0], n));

        var leftCopy = left;
        TVectorMask eqMask = left == leftCopy;
        bool[] allTrue = PadToMaxBool([true, true, true, true, true, true, true, true], n);
        AssertVectorMask(eqMask, allTrue, allTrue, allTrue, allTrue);
    }

    [TestMethod]
    public void TestDot()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TVector left = TVector.Create(
            TPacket.Load(PadToMax([1.0, 2.0, 3.0, 4.0, 0.0, -1.0, 0.5, 0.25], n)),
            TPacket.Load(PadToMax([2.0, 3.0, 4.0, 5.0, 0.0, 2.0, -0.5, 0.5], n)),
            TPacket.Load(PadToMax([0.5, 1.0, 2.0, -1.0, 3.0, 0.5, 1.0, 0.75], n)),
            TPacket.Load(PadToMax([-0.5, 0.0, 1.0, 2.0, -1.0, 0.25, 0.5, 0.125], n)));
        TVector right = TVector.Create(
            TPacket.Load(PadToMax([0.5, 1.0, -1.0, 2.0, 1.0, 0.5, 0.25, 2.0], n)),
            TPacket.Load(PadToMax([-0.5, -1.0, 2.0, -2.0, 1.0, -0.5, 0.75, 1.0], n)),
            TPacket.Load(PadToMax([1.0, 0.5, -0.5, 1.0, 2.0, -1.0, 0.5, 0.25], n)),
            TPacket.Load(PadToMax([0.25, 0.0, 0.5, -1.0, 0.5, 0.75, 0.125, 0.5], n)));

        double[] expected = PadToMax([-0.125, -0.5, 4.5, -5.0, 5.5, -1.8125, 0.3125, 1.25], n);
        TPacket dot = TVector.Dot(left, right);
        Span<double> values = stackalloc double[n];
        TPacket.Store(dot, values);
        for (int i = 0; i < n; i++)
            Assert.AreEqual(expected[i], values[i], 1e-12, $"lane {i}");
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

    protected static void AssertVector(TVector actual, double[] expectedX, double[] expectedY, double[] expectedZ, double[] expectedW, double epsilon = 1e-12)
    {
        int n = TPacket.LaneCount;
        Span<double> xValues = stackalloc double[n];
        Span<double> yValues = stackalloc double[n];
        Span<double> zValues = stackalloc double[n];
        Span<double> wValues = stackalloc double[n];
        TPacket.Store(actual.X, xValues);
        TPacket.Store(actual.Y, yValues);
        TPacket.Store(actual.Z, zValues);
        TPacket.Store(actual.W, wValues);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], xValues[i], epsilon, $"lane {i} X");
            Assert.AreEqual(expectedY[i], yValues[i], epsilon, $"lane {i} Y");
            Assert.AreEqual(expectedZ[i], zValues[i], epsilon, $"lane {i} Z");
            Assert.AreEqual(expectedW[i], wValues[i], epsilon, $"lane {i} W");
        }
    }

    protected static void AssertVectorMask(TVectorMask actual, bool[] expectedX, bool[] expectedY, bool[] expectedZ, bool[] expectedW)
    {
        int n = TPacket.LaneCount;
        Span<double> xValues = stackalloc double[n];
        Span<double> yValues = stackalloc double[n];
        Span<double> zValues = stackalloc double[n];
        Span<double> wValues = stackalloc double[n];
        TPacketMask.Store(actual.X, xValues);
        TPacketMask.Store(actual.Y, yValues);
        TPacketMask.Store(actual.Z, zValues);
        TPacketMask.Store(actual.W, wValues);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], BitConverter.DoubleToInt64Bits(xValues[i]) != 0, $"lane {i} X");
            Assert.AreEqual(expectedY[i], BitConverter.DoubleToInt64Bits(yValues[i]) != 0, $"lane {i} Y");
            Assert.AreEqual(expectedZ[i], BitConverter.DoubleToInt64Bits(zValues[i]) != 0, $"lane {i} Z");
            Assert.AreEqual(expectedW[i], BitConverter.DoubleToInt64Bits(wValues[i]) != 0, $"lane {i} W");
        }
    }
}
