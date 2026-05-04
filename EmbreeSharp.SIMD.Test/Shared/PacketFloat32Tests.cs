using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class PacketFloat32Tests<TPacket, TMask>
    where TPacket : unmanaged, ISimdFloatingPoint<TPacket, float, TMask>
    where TMask : unmanaged, ISimdPacketMask<TMask, float>
{
    protected abstract bool IsHardwareSupported { get; }
    protected abstract uint AlignmentMask { get; }

    [TestMethod]
    public void TestArithmeticAndMasks()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        float[] leftData = PadToMax([1f, -2f, 3.5f, -4.5f, 5.25f, -6.75f, 8f, -9f], n);
        float[] absExpected = PadToMax([1f, 2f, 3.5f, 4.5f, 5.25f, 6.75f, 8f, 9f], n);
        float[] addExpected = PadToMax([3f, 0f, 5.5f, -2.5f, 7.25f, -4.75f, 10f, -7f], n);
        float[] mulExpected = PadToMax([2f, -4f, 7f, -9f, 10.5f, -13.5f, 16f, -18f], n);
        float[] fmaExpected = PadToMax([4f, -2f, 9f, -7f, 12.5f, -11.5f, 18f, -16f], n);
        bool[] positiveMask = PadToMax([true, false, true, false, true, false, true, false], n);
        float[] selectExpected = PadToMax([1f, 2f, 3.5f, 2f, 5.25f, 2f, 8f, 2f], n);

        TPacket left = TPacket.Load(leftData);
        TPacket right = TPacket.Broadcast(2f);

        AssertPacket(TPacket.Abs(left), absExpected);
        AssertPacket(left + right, addExpected);
        AssertPacket(left * right, mulExpected);
        AssertPacket(TPacket.FusedMultiplyAdd(left, right, right), fmaExpected);

        TMask positive = left > TPacket.Broadcast(0f);
        AssertMask(positive, positiveMask);
        AssertPacket(TPacket.Select(positive, left, right), selectExpected);
    }

    [TestMethod]
    public void TestRoundingAndSpecialValues()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        float[] valueData = PadToMax([1.2f, -1.2f, 1.5f, 2.5f, -2.5f, 9f, 0.25f, -0f], n);
        float[] floorExpected = PadToMax([1f, -2f, 1f, 2f, -3f, 9f, 0f, -0f], n);
        float[] ceilExpected = PadToMax([2f, -1f, 2f, 3f, -2f, 9f, 1f, -0f], n);
        float[] truncExpected = PadToMax([1f, -1f, 1f, 2f, -2f, 9f, 0f, -0f], n);
        float[] sqrtInput = PadToMax([1f, 4f, 9f, 16f, 25f, 36f, 49f, 64f], n);
        float[] sqrtExpected = PadToMax([1f, 2f, 3f, 4f, 5f, 6f, 7f, 8f], n);

        TPacket value = TPacket.Load(valueData);

        AssertPacket(TPacket.Floor(value), floorExpected);
        AssertPacket(TPacket.Ceiling(value), ceilExpected);
        AssertPacket(TPacket.Truncate(value), truncExpected);
        AssertPacket(TPacket.Sqrt(TPacket.Load(sqrtInput)), sqrtExpected);

        float[] specialData = PadToMax([
            1f,
            float.PositiveInfinity,
            float.NegativeInfinity,
            float.NaN,
            -0f,
            float.MaxValue,
            float.Epsilon,
            -float.Epsilon
        ], n);
        bool[] finiteExpected = PadToMax([true, false, false, false, true, true, true, true], n);
        bool[] infExpected = PadToMax([false, true, true, false, false, false, false, false], n);
        bool[] nanExpected = PadToMax([false, false, false, true, false, false, false, false], n);
        float[] copySignData = PadToMax([1f, float.PositiveInfinity, float.NegativeInfinity, float.NaN, -0f, float.MaxValue, float.Epsilon, -float.Epsilon], n);
        float[] copySignExpected = PadToMax([3f, 3f, -3f, -3f, -3f, 3f, 3f, -3f], n);

        TPacket special = TPacket.Load(specialData);

        AssertMask(TPacket.IsFinite(special), finiteExpected);
        AssertMask(TPacket.IsInfinity(special), infExpected);
        AssertMask(TPacket.IsNaN(special), nanExpected);

        TPacket signed = TPacket.CopySign(TPacket.Broadcast(3f), TPacket.Load(copySignData));
        AssertPacket(signed, copySignExpected);
    }

    [TestMethod]
    public void TestTrigonometricFunctions()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        float[] angles = PadToMax([
            -MathF.PI,
            -MathF.PI * 0.5f,
            -0.25f,
            0f,
            0.25f,
            MathF.PI * 0.5f,
            MathF.PI,
            MathF.PI * 1.5f
        ], n);

        TPacket value = TPacket.Load(angles);
        var (sin, cos) = TPacket.SinCos(value);

        float[] expectedSin = new float[n];
        float[] expectedCos = new float[n];
        for (int i = 0; i < n; i++)
        {
            expectedSin[i] = MathF.Sin(angles[i]);
            expectedCos[i] = MathF.Cos(angles[i]);
        }

        AssertPacket(TPacket.Sin(value), expectedSin, 2e-6f);
        AssertPacket(TPacket.Cos(value), expectedCos, 2e-6f);
        AssertPacket(sin, expectedSin, 2e-6f);
        AssertPacket(cos, expectedCos, 2e-6f);
    }

    [TestMethod]
    public unsafe void TestAlignedLoadStore()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        float[] source = new float[n * 2];
        float[] destination = new float[n * 2];

        for (int i = 0; i < source.Length; i++)
        {
            source[i] = i + 1;
        }

        fixed (float* sourcePtr = source)
        fixed (float* destinationPtr = destination)
        {
            int sourceOffset = TestHelpers.AlignedOffset<float>(sourcePtr, AlignmentMask, n);
            int destinationOffset = TestHelpers.AlignedOffset<float>(destinationPtr, AlignmentMask, n);

            TPacket value = TPacket.LoadAligned(source.AsSpan(sourceOffset, n));
            TPacket.StoreAligned(value, destination.AsSpan(destinationOffset, n));

            for (int i = 0; i < n; i++)
            {
                Assert.AreEqual(source[sourceOffset + i], destination[destinationOffset + i], $"lane {i}");
            }
        }
    }

    [TestMethod]
    public void TestMaskLoadStore()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        float active = BitConverter.Int32BitsToSingle(-1);

        float[] source = new float[n + 1];
        for (int i = 0; i < source.Length; i++)
        {
            source[i] = (i & 1) == 0 ? active : 0f;
        }
        float[] destination = new float[n + 1];

        TMask mask = TMask.Load(source);
        TMask.Store(mask, destination);

        for (int i = 0; i < n; i++)
        {
            Assert.AreEqual(BitConverter.SingleToInt32Bits(source[i]), BitConverter.SingleToInt32Bits(destination[i]), $"lane {i}");
        }

        bool[] boolSource = new bool[n + 1];
        for (int i = 0; i < boolSource.Length; i++)
        {
            boolSource[i] = (i & 1) == 0;
        }
        bool[] boolDestination = new bool[n + 1];

        TMask boolMask = TMask.Load(boolSource);
        TMask.Store(boolMask, boolDestination);

        for (int i = 0; i < n; i++)
        {
            Assert.AreEqual(boolSource[i], boolDestination[i], $"bool lane {i}");
        }

        TMask leftMask = TMask.Load(PadToMax([true, true, false, false, false, false, false, false], n));
        TMask rightMask = TMask.Load(PadToMax([false, true, false, true, false, false, false, false], n));
        bool[] andNotExpected = PadToMax([true, false, false, false, false, false, false, false], n);
        AssertMask(TMask.AndNot(leftMask, rightMask), andNotExpected);

        float[] alignedSource = new float[n * 2];
        float[] alignedDestination = new float[n * 2];
        for (int i = 0; i < alignedSource.Length; i++)
        {
            alignedSource[i] = (i & 1) == 0 ? active : 0f;
        }

        unsafe
        {
            fixed (float* sourcePtr = alignedSource)
            fixed (float* destinationPtr = alignedDestination)
            {
                int sourceOffset = TestHelpers.AlignedOffset<float>(sourcePtr, AlignmentMask, n);
                int destinationOffset = TestHelpers.AlignedOffset<float>(destinationPtr, AlignmentMask, n);

                TMask alignedMask = TMask.LoadAligned(alignedSource.AsSpan(sourceOffset, n));
                TMask.StoreAligned(alignedMask, alignedDestination.AsSpan(destinationOffset, n));

                for (int i = 0; i < n; i++)
                {
                    Assert.AreEqual(BitConverter.SingleToInt32Bits(alignedSource[sourceOffset + i]), BitConverter.SingleToInt32Bits(alignedDestination[destinationOffset + i]), $"aligned lane {i}");
                }
            }
        }
    }

    [TestMethod]
    public unsafe void TestLoadStoreAcceptLongerSpans()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        float[] source = new float[n + 1];
        for (int i = 0; i < source.Length; i++)
        {
            source[i] = i + 1;
        }
        float[] destination = new float[n + 1];

        TPacket value = TPacket.Load(source);
        TPacket.Store(value, destination);

        for (int i = 0; i < n; i++)
        {
            Assert.AreEqual(source[i], destination[i], $"float lane {i}");
        }

        float active = BitConverter.Int32BitsToSingle(-1);
        float[] maskSource = new float[n + 1];
        for (int i = 0; i < maskSource.Length; i++)
        {
            maskSource[i] = (i & 1) == 0 ? active : 0f;
        }
        float[] maskDestination = new float[n + 1];

        TMask mask = TMask.Load(maskSource);
        TMask.Store(mask, maskDestination);

        for (int i = 0; i < n; i++)
        {
            Assert.AreEqual(BitConverter.SingleToInt32Bits(maskSource[i]), BitConverter.SingleToInt32Bits(maskDestination[i]), $"mask lane {i}");
        }

        float[] alignedSource = new float[n * 3];
        float[] alignedDestination = new float[n * 3];
        for (int i = 0; i < alignedSource.Length; i++)
        {
            alignedSource[i] = i + 1;
        }

        fixed (float* sourcePtr = alignedSource)
        fixed (float* destinationPtr = alignedDestination)
        {
            int sourceOffset = TestHelpers.AlignedOffset<float>(sourcePtr, AlignmentMask, n);
            int destinationOffset = TestHelpers.AlignedOffset<float>(destinationPtr, AlignmentMask, n);

            TPacket alignedValue = TPacket.LoadAligned(alignedSource.AsSpan(sourceOffset, n + 1));
            TPacket.StoreAligned(alignedValue, alignedDestination.AsSpan(destinationOffset, n + 1));

            for (int i = 0; i < n; i++)
            {
                Assert.AreEqual(alignedSource[sourceOffset + i], alignedDestination[destinationOffset + i], $"aligned float lane {i}");
            }
        }
    }

    [TestMethod]
    public void TestLoadStoreRejectShortSpans()
    {
        int n = TPacket.LaneCount;
        TestHelpers.ExpectArgumentException(() => TPacket.Load(new float[n - 1]));
        TestHelpers.ExpectArgumentException(() => TPacket.LoadAligned(new float[n - 1]));

        TPacket value = default;
        TestHelpers.ExpectArgumentException(() => TPacket.Store(value, new float[n - 1]));
        TestHelpers.ExpectArgumentException(() => TPacket.StoreAligned(value, new float[n - 1]));

        TestHelpers.ExpectArgumentException(() => TMask.Load(new float[n - 1]));
        TestHelpers.ExpectArgumentException(() => TMask.Load(new bool[n - 1]));
        TestHelpers.ExpectArgumentException(() => TMask.LoadAligned(new float[n - 1]));

        TMask mask = default;
        TestHelpers.ExpectArgumentException(() => TMask.Store(mask, new float[n - 1]));
        TestHelpers.ExpectArgumentException(() => TMask.Store(mask, new bool[n - 1]));
        TestHelpers.ExpectArgumentException(() => TMask.StoreAligned(mask, new float[n - 1]));
    }

    protected static float[] PadToMax(float[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        float[] result = new float[count];
        values.CopyTo(result, 0);
        return result;
    }

    protected static bool[] PadToMax(bool[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        bool[] result = new bool[count];
        values.CopyTo(result, 0);
        return result;
    }

    protected static void AssertPacket(TPacket actual, float[] expected, float epsilon = 1e-5f)
    {
        int n = TPacket.LaneCount;
        Span<float> values = stackalloc float[n];
        TPacket.Store(actual, values);

        for (int i = 0; i < expected.Length; i++)
        {
            Assert.AreEqual(expected[i], values[i], epsilon, $"lane {i}");
        }
    }

    protected static void AssertMask(TMask actual, bool[] expected)
    {
        int n = TPacket.LaneCount;
        Span<float> values = stackalloc float[n];
        TMask.Store(actual, values);

        for (int i = 0; i < expected.Length; i++)
        {
            Assert.AreEqual(expected[i], BitConverter.SingleToInt32Bits(values[i]) != 0, $"lane {i}");
        }
    }
}
