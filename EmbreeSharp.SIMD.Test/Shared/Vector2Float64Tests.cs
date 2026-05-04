using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class Vector2Float64Tests<TVector, TPacket, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdFloatingPointVector2<TVector, TPacket, double, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdFloatingPoint<TPacket, double, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector2Mask<TVectorMask, TPacketMask>
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
        double[] rightX = PadToMax([0.5, -1.0, 2.0, -0.5, 1.0, -1.5, 0.25, -2.0], n);
        double[] rightY = PadToMax([-0.5, 1.0, -2.0, 0.5, -1.0, 1.5, -0.25, 2.0], n);

        double[] addX = PadToMax([1.5, -3.0, 5.5, -5.0, 6.25, -8.25, 8.25, -11.0], n);
        double[] addY = PadToMax([1.5, 4.0, -6.0, 5.5, -7.0, 8.5, -8.25, 11.0], n);
        double[] subX = PadToMax([0.5, -1.0, 1.5, -4.0, 4.25, -5.25, 7.75, -7.0], n);
        double[] subY = PadToMax([2.5, 2.0, -2.0, 4.5, -5.0, 5.5, -7.75, 7.0], n);
        double[] mulX = PadToMax([0.5, 2.0, 7.0, 2.25, 5.25, 10.125, 2.0, 18.0], n);
        double[] mulY = PadToMax([-1.0, 3.0, 8.0, 2.5, 6.0, 10.5, 2.0, 18.0], n);
        double[] negX = PadToMax([-1.0, 2.0, -3.5, 4.5, -5.25, 6.75, -8.0, 9.0], n);
        double[] negY = PadToMax([-2.0, -3.0, 4.0, -5.0, 6.0, -7.0, 8.0, -9.0], n);
        double[] broadcastX = PadToMax([3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0], n);
        double[] broadcastY = PadToMax([3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0, 3.0], n);

        TPacket leftXPacket = TPacket.Load(leftX);
        TPacket leftYPacket = TPacket.Load(leftY);
        TPacket rightXPacket = TPacket.Load(rightX);
        TPacket rightYPacket = TPacket.Load(rightY);
        TVector left = TVector.Create(leftXPacket, leftYPacket);
        TVector right = TVector.Create(rightXPacket, rightYPacket);

        AssertVector(left + right, addX, addY);
        AssertVector(left - right, subX, subY);
        AssertVector(left * right, mulX, mulY);
        AssertVector(-left, negX, negY);

        TVector broadcast = TVector.Broadcast(3.0);
        AssertVector(broadcast, broadcastX, broadcastY);

        double[] selectXExpected = PadToMax([1.0, -1.0, 3.5, -0.5, 5.25, -1.5, 8.0, -2.0], n);
        double[] selectYExpected = PadToMax([2.0, 1.0, -4.0, 0.5, -6.0, 1.5, -8.0, 2.0], n);
        TPacketMask alternateLaneMask = TPacketMask.Load(PadToMaxBool([true, false, true, false, true, false, true, false], n));
        TVectorMask mask = TVectorMask.Create(alternateLaneMask, alternateLaneMask);
        AssertVector(TVector.Select(mask, left, right), selectXExpected, selectYExpected);

        var leftCopy = left;
        TVectorMask eqMask = left == leftCopy;
        bool[] allTrue = PadToMaxBool([true, true, true, true, true, true, true, true], n);
        AssertVectorMask(eqMask, allTrue, allTrue);
    }

    [TestMethod]
    public void TestDot()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        double[] leftX = PadToMax([1.0, 2.0, 3.0, 4.0, 0.0, -1.0, 0.5, 0.25], n);
        double[] leftY = PadToMax([2.0, 3.0, 4.0, 5.0, 0.0, 2.0, -0.5, 0.5], n);
        double[] rightX = PadToMax([0.5, 1.0, -1.0, 2.0, 1.0, 0.5, 0.25, 2.0], n);
        double[] rightY = PadToMax([-0.5, -1.0, 2.0, -2.0, 1.0, -0.5, 0.75, 1.0], n);
        double[] dotExpected = PadToMax([-0.5, -1.0, 5.0, -2.0, 0.0, -1.5, -0.25, 1.0], n);

        TPacket leftXPacket = TPacket.Load(leftX);
        TPacket leftYPacket = TPacket.Load(leftY);
        TPacket rightXPacket = TPacket.Load(rightX);
        TPacket rightYPacket = TPacket.Load(rightY);
        TVector left = TVector.Create(leftXPacket, leftYPacket);
        TVector right = TVector.Create(rightXPacket, rightYPacket);

        TPacket dot = TVector.Dot(left, right);
        Span<double> dotValues = stackalloc double[n];
        TPacket.Store(dot, dotValues);
        for (int i = 0; i < n; i++)
        {
            Assert.AreEqual(dotExpected[i], dotValues[i], 1e-12, $"lane {i}");
        }
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

    protected static void AssertVectorMask(TVectorMask actual, bool[] expectedX, bool[] expectedY)
    {
        int n = TPacket.LaneCount;
        Span<double> xValues = stackalloc double[n];
        Span<double> yValues = stackalloc double[n];
        TPacketMask.Store(actual.X, xValues);
        TPacketMask.Store(actual.Y, yValues);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], BitConverter.DoubleToInt64Bits(xValues[i]) != 0, $"lane {i} X");
            Assert.AreEqual(expectedY[i], BitConverter.DoubleToInt64Bits(yValues[i]) != 0, $"lane {i} Y");
        }
    }
}
