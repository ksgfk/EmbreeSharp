using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class Vector3Float32Tests<TVector, TPacket, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdFloatingPointVector3<TVector, TPacket, float, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdFloatingPoint<TPacket, float, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector3Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, float>
{
    protected abstract bool IsHardwareSupported { get; }
    protected abstract uint AlignmentMask { get; }

    [TestMethod]
    public void TestArithmetic()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        float[] leftX = PadToMax([1f, -2f, 3.5f, -4.5f, 5.25f, -6.75f, 8f, -9f], n);
        float[] leftY = PadToMax([2f, 3f, -4f, 5f, -6f, 7f, -8f, 9f], n);
        float[] leftZ = PadToMax([0.5f, -1f, 2f, -0.5f, 1f, -1.5f, 0.25f, -2f], n);
        float[] rightX = PadToMax([0.5f, -1f, 2f, -0.5f, 1f, -1.5f, 0.25f, -2f], n);
        float[] rightY = PadToMax([-0.5f, 1f, -2f, 0.5f, -1f, 1.5f, -0.25f, 2f], n);
        float[] rightZ = PadToMax([1f, -2f, 3.5f, -4.5f, 5.25f, -6.75f, 8f, -9f], n);

        TPacket leftXPacket = TPacket.Load(leftX);
        TPacket leftYPacket = TPacket.Load(leftY);
        TPacket leftZPacket = TPacket.Load(leftZ);
        TVector left = TVector.Create(leftXPacket, leftYPacket, leftZPacket);
        TVector right = TVector.Create(TPacket.Load(rightX), TPacket.Load(rightY), TPacket.Load(rightZ));

        float[] addX = PadToMax([1.5f, -3f, 5.5f, -5f, 6.25f, -8.25f, 8.25f, -11f], n);
        float[] addY = PadToMax([1.5f, 4f, -6f, 5.5f, -7f, 8.5f, -8.25f, 11f], n);
        float[] addZ = PadToMax([1.5f, -3f, 5.5f, -5f, 6.25f, -8.25f, 8.25f, -11f], n);
        AssertVector(left + right, addX, addY, addZ);

        float[] subX = PadToMax([0.5f, -1f, 1.5f, -4f, 4.25f, -5.25f, 7.75f, -7f], n);
        float[] subY = PadToMax([2.5f, 2f, -2f, 4.5f, -5f, 5.5f, -7.75f, 7f], n);
        float[] subZ = PadToMax([-0.5f, 1f, -1.5f, 4f, -4.25f, 5.25f, -7.75f, 7f], n);
        AssertVector(left - right, subX, subY, subZ);

        AssertVector(left * right, PadToMax([0.5f, 2f, 7f, 2.25f, 5.25f, 10.125f, 2f, 18f], n), PadToMax([-1f, 3f, 8f, 2.5f, 6f, 10.5f, 2f, 18f], n), PadToMax([0.5f, 2f, 7f, 2.25f, 5.25f, 10.125f, 2f, 18f], n));
        AssertVector(-left, PadToMax([-1f, 2f, -3.5f, 4.5f, -5.25f, 6.75f, -8f, 9f], n), PadToMax([-2f, -3f, 4f, -5f, 6f, -7f, 8f, -9f], n), PadToMax([-0.5f, 1f, -2f, 0.5f, -1f, 1.5f, -0.25f, 2f], n));

        TVector broadcast = TVector.Broadcast(3f);
        AssertVector(broadcast, PadToMax([3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f], n), PadToMax([3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f], n), PadToMax([3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f], n));

        TPacketMask alternateLaneMask = TPacketMask.Load(PadToMaxBool([true, false, true, false, true, false, true, false], n));
        TVectorMask mask = TVectorMask.Create(alternateLaneMask, alternateLaneMask, alternateLaneMask);
        AssertVector(TVector.Select(mask, left, right), PadToMax([1f, -1f, 3.5f, -0.5f, 5.25f, -1.5f, 8f, -2f], n), PadToMax([2f, 1f, -4f, 0.5f, -6f, 1.5f, -8f, 2f], n), PadToMax([0.5f, -2f, 2f, -4.5f, 1f, -6.75f, 0.25f, -9f], n));

        var leftCopy = left;
        TVectorMask eqMask = left == leftCopy;
        bool[] allTrue = PadToMaxBool([true, true, true, true, true, true, true, true], n);
        AssertVectorMask(eqMask, allTrue, allTrue, allTrue);
    }

    [TestMethod]
    public void TestDot()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TVector left = TVector.Create(TPacket.Load(PadToMax([1f, 2f, 3f, 4f, 0f, -1f, 0.5f, 0.25f], n)), TPacket.Load(PadToMax([2f, 3f, 4f, 5f, 0f, 2f, -0.5f, 0.5f], n)), TPacket.Load(PadToMax([0.5f, 1f, 2f, -1f, 3f, 0.5f, 1f, 0.75f], n)));
        TVector right = TVector.Create(TPacket.Load(PadToMax([0.5f, 1f, -1f, 2f, 1f, 0.5f, 0.25f, 2f], n)), TPacket.Load(PadToMax([-0.5f, -1f, 2f, -2f, 1f, -0.5f, 0.75f, 1f], n)), TPacket.Load(PadToMax([1f, 0.5f, -0.5f, 1f, 2f, -1f, 0.5f, 0.25f], n)));

        float[] expected = PadToMax([0f, -0.5f, 4f, -3f, 6f, -2f, 0.25f, 1.1875f], n);
        TPacket dot = TVector.Dot(left, right);
        Span<float> values = stackalloc float[n];
        TPacket.Store(dot, values);
        for (int i = 0; i < n; i++)
            Assert.AreEqual(expected[i], values[i], 1e-5f, $"lane {i}");
    }

    [TestMethod]
    public void TestCross()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TVector xAxis = TVector.Create(TPacket.Broadcast(1f), TPacket.Broadcast(0f), TPacket.Broadcast(0f));
        TVector yAxis = TVector.Create(TPacket.Broadcast(0f), TPacket.Broadcast(1f), TPacket.Broadcast(0f));

        TVector cross = TVector.Cross(xAxis, yAxis);
        float[] zeroExpected = PadToMax([0f, 0f, 0f, 0f, 0f, 0f, 0f, 0f], n);
        float[] oneExpected = PadToMax([1f, 1f, 1f, 1f, 1f, 1f, 1f, 1f], n);
        AssertVector(cross, zeroExpected, zeroExpected, oneExpected);

        TVector a = TVector.Create(TPacket.Load(PadToMax([2f, 1f, 0f, 3f, -1f, 4f, 0.5f, 2f], n)), TPacket.Load(PadToMax([3f, -2f, 5f, 1f, 2f, -3f, 1.5f, 0f], n)), TPacket.Load(PadToMax([4f, 3f, -2f, -1f, 3f, 2f, -0.5f, 1f], n)));
        TVector b = TVector.Create(TPacket.Load(PadToMax([1f, -1f, 3f, 2f, 0f, 1f, 2f, -1f], n)), TPacket.Load(PadToMax([-2f, 4f, 1f, -1f, 3f, 2f, 0f, 1f], n)), TPacket.Load(PadToMax([3f, 2f, -1f, 4f, 1f, -1f, 1f, 3f], n)));

        // a × b = (ay*bz - az*by, az*bx - ax*bz, ax*by - ay*bx)
        float[] crossX = new float[n];
        float[] crossY = new float[n];
        float[] crossZ = new float[n];
        float[] ax = PadToMax([2f, 1f, 0f, 3f, -1f, 4f, 0.5f, 2f], n);
        float[] ay = PadToMax([3f, -2f, 5f, 1f, 2f, -3f, 1.5f, 0f], n);
        float[] az = PadToMax([4f, 3f, -2f, -1f, 3f, 2f, -0.5f, 1f], n);
        float[] bx = PadToMax([1f, -1f, 3f, 2f, 0f, 1f, 2f, -1f], n);
        float[] by = PadToMax([-2f, 4f, 1f, -1f, 3f, 2f, 0f, 1f], n);
        float[] bz = PadToMax([3f, 2f, -1f, 4f, 1f, -1f, 1f, 3f], n);
        for (int i = 0; i < n; i++) { crossX[i] = ay[i] * bz[i] - az[i] * by[i]; crossY[i] = az[i] * bx[i] - ax[i] * bz[i]; crossZ[i] = ax[i] * by[i] - ay[i] * bx[i]; }

        TVector result = TVector.Cross(a, b);
        AssertVector(result, crossX, crossY, crossZ, 2e-5f);
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

    protected static void AssertVector(TVector actual, float[] expectedX, float[] expectedY, float[] expectedZ, float epsilon = 1e-5f)
    {
        int n = TPacket.LaneCount;
        Span<float> xValues = stackalloc float[n];
        Span<float> yValues = stackalloc float[n];
        Span<float> zValues = stackalloc float[n];
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
        Span<float> xValues = stackalloc float[n];
        Span<float> yValues = stackalloc float[n];
        Span<float> zValues = stackalloc float[n];
        TPacketMask.Store(actual.X, xValues);
        TPacketMask.Store(actual.Y, yValues);
        TPacketMask.Store(actual.Z, zValues);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], BitConverter.SingleToInt32Bits(xValues[i]) != 0, $"lane {i} X");
            Assert.AreEqual(expectedY[i], BitConverter.SingleToInt32Bits(yValues[i]) != 0, $"lane {i} Y");
            Assert.AreEqual(expectedZ[i], BitConverter.SingleToInt32Bits(zValues[i]) != 0, $"lane {i} Z");
        }
    }
}
