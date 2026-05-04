using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class PacketFloat64Tests<TPacket, TMask>
    where TPacket : unmanaged, ISimdFloatingPoint<TPacket, double, TMask>
    where TMask : unmanaged, ISimdPacketMask<TMask, double>
{
    protected abstract bool IsHardwareSupported { get; }
    protected abstract uint AlignmentMask { get; }

    [TestMethod]
    public void TestArithmeticAndMasks()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        double[] leftData = PadToMax([1d, -2d, 3.5d, -4.5d], n);
        double[] absExpected = PadToMax([1d, 2d, 3.5d, 4.5d], n);
        double[] addExpected = PadToMax([3d, 0d, 5.5d, -2.5d], n);
        double[] mulExpected = PadToMax([2d, -4d, 7d, -9d], n);
        double[] fmaExpected = PadToMax([4d, -2d, 9d, -7d], n);
        bool[] positiveMask = PadToMax([true, false, true, false], n);
        double[] selectExpected = PadToMax([1d, 2d, 3.5d, 2d], n);

        TPacket left = TPacket.Load(leftData);
        TPacket right = TPacket.Broadcast(2d);

        AssertPacket(TPacket.Abs(left), absExpected);
        AssertPacket(left + right, addExpected);
        AssertPacket(left * right, mulExpected);
        AssertPacket(TPacket.FusedMultiplyAdd(left, right, right), fmaExpected);

        TMask positive = left > TPacket.Broadcast(0d);
        AssertMask(positive, positiveMask);
        AssertPacket(TPacket.Select(positive, left, right), selectExpected);
    }

    [TestMethod]
    public void TestRoundingAndSpecialValues()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        double[] valueData = PadToMax([1.2d, -1.2d, 1.5d, 2.5d], n);
        double[] floorExpected = PadToMax([1d, -2d, 1d, 2d], n);
        double[] ceilExpected = PadToMax([2d, -1d, 2d, 3d], n);
        double[] truncExpected = PadToMax([1d, -1d, 1d, 2d], n);
        double[] sqrtInput = PadToMax([1d, 4d, 9d, 16d], n);
        double[] sqrtExpected = PadToMax([1d, 2d, 3d, 4d], n);
        double[] recipInput = PadToMax([1d, 2d, 4d, 0.5d], n);
        double[] recipExpected = PadToMax([1d, 0.5d, 0.25d, 2d], n);
        double[] recipSqrtInput = PadToMax([1d, 4d, 16d, 0.25d], n);
        double[] recipSqrtExpected = PadToMax([1d, 0.5d, 0.25d, 2d], n);

        TPacket value = TPacket.Load(valueData);

        AssertPacket(TPacket.Floor(value), floorExpected);
        AssertPacket(TPacket.Ceiling(value), ceilExpected);
        AssertPacket(TPacket.Truncate(value), truncExpected);
        AssertPacket(TPacket.Sqrt(TPacket.Load(sqrtInput)), sqrtExpected);
        AssertPacket(TPacket.Reciprocal(TPacket.Load(recipInput)), recipExpected);
        AssertPacket(TPacket.ReciprocalSqrt(TPacket.Load(recipSqrtInput)), recipSqrtExpected);

        double negativeNaN = BitConverter.Int64BitsToDouble(unchecked((long)0xFFF8000000000000UL));
        double[] specialData = PadToMax([1d, double.PositiveInfinity, double.NegativeInfinity, negativeNaN], n);
        bool[] finiteExpected = PadToMax([true, false, false, false], n);
        bool[] infExpected = PadToMax([false, true, true, false], n);
        bool[] nanExpected = PadToMax([false, false, false, true], n);
        double[] copySignData = PadToMax([1d, double.PositiveInfinity, double.NegativeInfinity, negativeNaN], n);
        double[] copySignExpected = PadToMax([3d, 3d, -3d, -3d], n);

        TPacket special = TPacket.Load(specialData);

        AssertMask(TPacket.IsFinite(special), finiteExpected);
        AssertMask(TPacket.IsInfinity(special), infExpected);
        AssertMask(TPacket.IsNaN(special), nanExpected);

        TPacket signed = TPacket.CopySign(TPacket.Broadcast(3d), TPacket.Load(copySignData));
        AssertPacket(signed, copySignExpected);
    }

    [TestMethod]
    public void TestTrigonometricFunctions()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        double[] angles = PadToMax([
            -Math.PI,
            -Math.PI * 0.5d,
            0.25d,
            Math.PI * 1.5d
        ], n);

        TPacket value = TPacket.Load(angles);
        var (sin, cos) = TPacket.SinCos(value);

        double[] expectedSin = new double[n];
        double[] expectedCos = new double[n];
        for (int i = 0; i < n; i++)
        {
            expectedSin[i] = Math.Sin(angles[i]);
            expectedCos[i] = Math.Cos(angles[i]);
        }

        AssertPacket(TPacket.Sin(value), expectedSin, 1e-12d);
        AssertPacket(TPacket.Cos(value), expectedCos, 1e-12d);
        AssertPacket(sin, expectedSin, 1e-12d);
        AssertPacket(cos, expectedCos, 1e-12d);
    }

    [TestMethod]
    public unsafe void TestAlignedLoadStore()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        double[] source = new double[n * 2];
        double[] destination = new double[n * 2];

        for (int i = 0; i < source.Length; i++)
        {
            source[i] = i + 1;
        }

        fixed (double* sourcePtr = source)
        fixed (double* destinationPtr = destination)
        {
            int sourceOffset = TestHelpers.AlignedOffset<double>(sourcePtr, AlignmentMask, n);
            int destinationOffset = TestHelpers.AlignedOffset<double>(destinationPtr, AlignmentMask, n);

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
        double active = BitConverter.Int64BitsToDouble(-1);

        double[] source = new double[n + 1];
        for (int i = 0; i < source.Length; i++)
        {
            source[i] = (i & 1) == 0 ? active : 0d;
        }
        double[] destination = new double[n + 1];

        TMask mask = TMask.Load(source);
        TMask.Store(mask, destination);

        for (int i = 0; i < n; i++)
        {
            Assert.AreEqual(BitConverter.DoubleToInt64Bits(source[i]), BitConverter.DoubleToInt64Bits(destination[i]), $"lane {i}");
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

        TMask leftMask = TMask.Load(PadToMax([true, true, false, false], n));
        TMask rightMask = TMask.Load(PadToMax([false, true, false, true], n));
        bool[] andNotExpected = PadToMax([true, false, false, false], n);
        AssertMask(TMask.AndNot(leftMask, rightMask), andNotExpected);

        double[] alignedSource = new double[n * 2];
        double[] alignedDestination = new double[n * 2];
        for (int i = 0; i < alignedSource.Length; i++)
        {
            alignedSource[i] = (i & 1) == 0 ? active : 0d;
        }

        unsafe
        {
            fixed (double* sourcePtr = alignedSource)
            fixed (double* destinationPtr = alignedDestination)
            {
                int sourceOffset = TestHelpers.AlignedOffset<double>(sourcePtr, AlignmentMask, n);
                int destinationOffset = TestHelpers.AlignedOffset<double>(destinationPtr, AlignmentMask, n);

                TMask alignedMask = TMask.LoadAligned(alignedSource.AsSpan(sourceOffset, n));
                TMask.StoreAligned(alignedMask, alignedDestination.AsSpan(destinationOffset, n));

                for (int i = 0; i < n; i++)
                {
                    Assert.AreEqual(BitConverter.DoubleToInt64Bits(alignedSource[sourceOffset + i]), BitConverter.DoubleToInt64Bits(alignedDestination[destinationOffset + i]), $"aligned lane {i}");
                }
            }
        }
    }

    [TestMethod]
    public void TestLoadStoreRejectShortSpans()
    {
        int n = TPacket.LaneCount;
        TestHelpers.ExpectArgumentException(() => TPacket.Load(new double[n - 1]));
        TestHelpers.ExpectArgumentException(() => TPacket.LoadAligned(new double[n - 1]));

        TPacket value = default;
        TestHelpers.ExpectArgumentException(() => TPacket.Store(value, new double[n - 1]));
        TestHelpers.ExpectArgumentException(() => TPacket.StoreAligned(value, new double[n - 1]));

        TestHelpers.ExpectArgumentException(() => TMask.Load(new double[n - 1]));
        TestHelpers.ExpectArgumentException(() => TMask.Load(new bool[n - 1]));
        TestHelpers.ExpectArgumentException(() => TMask.LoadAligned(new double[n - 1]));

        TMask mask = default;
        TestHelpers.ExpectArgumentException(() => TMask.Store(mask, new double[n - 1]));
        TestHelpers.ExpectArgumentException(() => TMask.Store(mask, new bool[n - 1]));
        TestHelpers.ExpectArgumentException(() => TMask.StoreAligned(mask, new double[n - 1]));
    }

    protected static double[] PadToMax(double[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        double[] result = new double[count];
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

    protected static void AssertPacket(TPacket actual, double[] expected, double epsilon = 1e-12d)
    {
        int n = TPacket.LaneCount;
        Span<double> values = stackalloc double[n];
        TPacket.Store(actual, values);

        for (int i = 0; i < expected.Length; i++)
        {
            Assert.AreEqual(expected[i], values[i], epsilon, $"lane {i}");
        }
    }

    protected static void AssertMask(TMask actual, bool[] expected)
    {
        int n = TPacket.LaneCount;
        Span<double> values = stackalloc double[n];
        TMask.Store(actual, values);

        for (int i = 0; i < expected.Length; i++)
        {
            Assert.AreEqual(expected[i], BitConverter.DoubleToInt64Bits(values[i]) != 0, $"lane {i}");
        }
    }
}
