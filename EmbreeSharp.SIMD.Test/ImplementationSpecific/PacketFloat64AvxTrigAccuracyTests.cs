using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketFloat64AvxTrigAccuracyTests
{
    [TestMethod]
    public void TestTrigonometricAccuracyAcrossRange()
    {
        if (!Avx2.IsSupported)
        {
            Assert.Inconclusive("AVX2 is not supported on this machine.");
        }

        DoubleTrigStats sinStats = default;
        DoubleTrigStats cosStats = default;
        DoubleTrigStats sinCosSinStats = default;
        DoubleTrigStats sinCosCosStats = default;

        double[] edgeValues =
        [
            -8192d,
            -4096.25d,
            -2d * Math.PI,
            -Math.PI,
            -Math.PI * 0.5d,
            -double.Epsilon,
            -0d,
            0d,
            double.Epsilon,
            Math.PI * 0.5d,
            Math.PI,
            2d * Math.PI,
            4096.25d,
            8192d,
            8d,
            -9.5d
        ];

        for (int i = 0; i < edgeValues.Length; i += PacketFloat64Avx.LaneCount)
        {
            AccumulateDoubleTrigErrors(
                edgeValues.AsSpan(i, PacketFloat64Avx.LaneCount),
                ref sinStats,
                ref cosStats,
                ref sinCosSinStats,
                ref sinCosCosStats);
        }

        Span<double> values = stackalloc double[PacketFloat64Avx.LaneCount];
        const int sampleCount = 16 * 1024;
        const double range = 8192d;
        double step = (2d * range) / (sampleCount - 1);

        for (int i = 0; i < sampleCount; i += PacketFloat64Avx.LaneCount)
        {
            for (int lane = 0; lane < PacketFloat64Avx.LaneCount; lane++)
            {
                values[lane] = -range + ((i + lane) * step);
            }

            AccumulateDoubleTrigErrors(
                values,
                ref sinStats,
                ref cosStats,
                ref sinCosSinStats,
                ref sinCosCosStats);
        }

        AssertDoubleTrigStats("PacketFloat64Avx.Sin", sinStats);
        AssertDoubleTrigStats("PacketFloat64Avx.Cos", cosStats);
        AssertDoubleTrigStats("PacketFloat64Avx.SinCos.Sin", sinCosSinStats);
        AssertDoubleTrigStats("PacketFloat64Avx.SinCos.Cos", sinCosCosStats);
    }

    private static void AccumulateDoubleTrigErrors(
        ReadOnlySpan<double> input,
        ref DoubleTrigStats sinStats,
        ref DoubleTrigStats cosStats,
        ref DoubleTrigStats sinCosSinStats,
        ref DoubleTrigStats sinCosCosStats)
    {
        PacketFloat64Avx value = PacketFloat64Avx.Load(input);
        PacketFloat64Avx sin = PacketFloat64Avx.Sin(value);
        PacketFloat64Avx cos = PacketFloat64Avx.Cos(value);
        (PacketFloat64Avx sinCosSin, PacketFloat64Avx sinCosCos) = PacketFloat64Avx.SinCos(value);

        Span<double> sinValues = stackalloc double[PacketFloat64Avx.LaneCount];
        Span<double> cosValues = stackalloc double[PacketFloat64Avx.LaneCount];
        Span<double> sinCosSinValues = stackalloc double[PacketFloat64Avx.LaneCount];
        Span<double> sinCosCosValues = stackalloc double[PacketFloat64Avx.LaneCount];

        PacketFloat64Avx.Store(sin, sinValues);
        PacketFloat64Avx.Store(cos, cosValues);
        PacketFloat64Avx.Store(sinCosSin, sinCosSinValues);
        PacketFloat64Avx.Store(sinCosCos, sinCosCosValues);

        for (int lane = 0; lane < PacketFloat64Avx.LaneCount; lane++)
        {
            double expectedSin = Math.Sin(input[lane]);
            double expectedCos = Math.Cos(input[lane]);
            sinStats.Accumulate(input[lane], expectedSin, sinValues[lane]);
            cosStats.Accumulate(input[lane], expectedCos, cosValues[lane]);
            sinCosSinStats.Accumulate(input[lane], expectedSin, sinCosSinValues[lane]);
            sinCosCosStats.Accumulate(input[lane], expectedCos, sinCosCosValues[lane]);
        }
    }

    private static void AssertDoubleTrigStats(string name, DoubleTrigStats stats)
    {
        Assert.IsTrue(stats.MaxAbs <= 5e-16d, $"{name} max abs error {stats.MaxAbs:R} at {stats.MaxAbsInput:R} exceeded tolerance.");
        Assert.IsTrue(stats.MaxUlpAwayFromZero <= 4, $"{name} max ULP {stats.MaxUlpAwayFromZero} at {stats.MaxUlpInput:R} exceeded tolerance.");
    }

    private struct DoubleTrigStats
    {
        public double MaxAbs;
        public double MaxAbsInput;
        public long MaxUlpAwayFromZero;
        public double MaxUlpInput;

        public void Accumulate(double input, double expected, double actual)
        {
            double absError = double.Abs(actual - expected);
            if (absError > MaxAbs)
            {
                MaxAbs = absError;
                MaxAbsInput = input;
            }

            if (double.Abs(expected) <= 1e-3d)
            {
                return;
            }

            long ulp = UlpDistance(expected, actual);
            if (ulp > MaxUlpAwayFromZero)
            {
                MaxUlpAwayFromZero = ulp;
                MaxUlpInput = input;
            }
        }

        private static long UlpDistance(double left, double right)
        {
            long leftBits = OrderedBits(left);
            long rightBits = OrderedBits(right);
            return long.Abs(leftBits - rightBits);
        }

        private static long OrderedBits(double value)
        {
            long bits = BitConverter.DoubleToInt64Bits(value);
            return bits < 0 ? long.MinValue - bits : bits;
        }
    }
}
