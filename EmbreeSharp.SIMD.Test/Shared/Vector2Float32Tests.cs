using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class Vector2Float32Tests<TVector, TPacket, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdFloatingPointVector2<TVector, TPacket, float, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdFloatingPoint<TPacket, float, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector2Mask<TVectorMask, TPacketMask>
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
        float[] rightX = PadToMax([0.5f, -1f, 2f, -0.5f, 1f, -1.5f, 0.25f, -2f], n);
        float[] rightY = PadToMax([-0.5f, 1f, -2f, 0.5f, -1f, 1.5f, -0.25f, 2f], n);

        float[] addX = PadToMax([1.5f, -3f, 5.5f, -5f, 6.25f, -8.25f, 8.25f, -11f], n);
        float[] addY = PadToMax([1.5f, 4f, -6f, 5.5f, -7f, 8.5f, -8.25f, 11f], n);
        float[] subX = PadToMax([0.5f, -1f, 1.5f, -4f, 4.25f, -5.25f, 7.75f, -7f], n);
        float[] subY = PadToMax([2.5f, 2f, -2f, 4.5f, -5f, 5.5f, -7.75f, 7f], n);
        float[] mulX = PadToMax([0.5f, 2f, 7f, 2.25f, 5.25f, 10.125f, 2f, 18f], n);
        float[] mulY = PadToMax([-1f, 3f, 8f, 2.5f, 6f, 10.5f, 2f, 18f], n);
        float[] negX = PadToMax([-1f, 2f, -3.5f, 4.5f, -5.25f, 6.75f, -8f, 9f], n);
        float[] negY = PadToMax([-2f, -3f, 4f, -5f, 6f, -7f, 8f, -9f], n);
        float[] broadcastX = PadToMax([3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f], n);
        float[] broadcastY = PadToMax([3f, 3f, 3f, 3f, 3f, 3f, 3f, 3f], n);

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

        TVector broadcast = TVector.Broadcast(3f);
        AssertVector(broadcast, broadcastX, broadcastY);

        float[] selectXExpected = PadToMax([1f, -1f, 3.5f, -0.5f, 5.25f, -1.5f, 8f, -2f], n);
        float[] selectYExpected = PadToMax([2f, 1f, -4f, 0.5f, -6f, 1.5f, -8f, 2f], n);
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
        float[] leftX = PadToMax([1f, 2f, 3f, 4f, 0f, -1f, 0.5f, 0.25f], n);
        float[] leftY = PadToMax([2f, 3f, 4f, 5f, 0f, 2f, -0.5f, 0.5f], n);
        float[] rightX = PadToMax([0.5f, 1f, -1f, 2f, 1f, 0.5f, 0.25f, 2f], n);
        float[] rightY = PadToMax([-0.5f, -1f, 2f, -2f, 1f, -0.5f, 0.75f, 1f], n);
        float[] dotExpected = PadToMax([-0.5f, -1f, 5f, -2f, 0f, -1.5f, -0.25f, 1f], n);

        TPacket leftXPacket = TPacket.Load(leftX);
        TPacket leftYPacket = TPacket.Load(leftY);
        TPacket rightXPacket = TPacket.Load(rightX);
        TPacket rightYPacket = TPacket.Load(rightY);
        TVector left = TVector.Create(leftXPacket, leftYPacket);
        TVector right = TVector.Create(rightXPacket, rightYPacket);

        TPacket dot = TVector.Dot(left, right);
        Span<float> dotValues = stackalloc float[n];
        TPacket.Store(dot, dotValues);
        for (int i = 0; i < n; i++)
        {
            Assert.AreEqual(dotExpected[i], dotValues[i], 1e-5f, $"lane {i}");
        }
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

    protected static void AssertVectorMask(TVectorMask actual, bool[] expectedX, bool[] expectedY)
    {
        int n = TPacket.LaneCount;
        Span<float> xValues = stackalloc float[n];
        Span<float> yValues = stackalloc float[n];
        TPacketMask.Store(actual.X, xValues);
        TPacketMask.Store(actual.Y, yValues);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], BitConverter.SingleToInt32Bits(xValues[i]) != 0, $"lane {i} X");
            Assert.AreEqual(expectedY[i], BitConverter.SingleToInt32Bits(yValues[i]) != 0, $"lane {i} Y");
        }
    }
}
