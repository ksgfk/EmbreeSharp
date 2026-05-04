using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketFloat32AvxTrigAccuracyTests
{
    [TestMethod]
    public void TestTrigonometricAccuracyAcrossRange()
    {
        if (!Avx2.IsSupported)
        {
            Assert.Inconclusive("AVX2 is not supported on this machine.");
        }

        FloatTrigStats sinStats = default;
        FloatTrigStats cosStats = default;
        FloatTrigStats sinCosSinStats = default;
        FloatTrigStats sinCosCosStats = default;

        float[] edgeValues =
        [
            -8192f,
            -4096.25f,
            -2f * MathF.PI,
            -MathF.PI,
            -MathF.PI * 0.5f,
            -float.Epsilon,
            -0f,
            0f,
            float.Epsilon,
            MathF.PI * 0.5f,
            MathF.PI,
            2f * MathF.PI,
            4096.25f,
            8192f,
            11f,
            -9.5f
        ];

        for (int i = 0; i < edgeValues.Length; i += PacketFloat32Avx.LaneCount)
        {
            AccumulateFloatTrigErrors(
                edgeValues.AsSpan(i, PacketFloat32Avx.LaneCount),
                ref sinStats,
                ref cosStats,
                ref sinCosSinStats,
                ref sinCosCosStats);
        }

        Span<float> values = stackalloc float[PacketFloat32Avx.LaneCount];
        const int sampleCount = 16 * 1024;
        const float range = 8192f;
        float step = (2f * range) / (sampleCount - 1);

        for (int i = 0; i < sampleCount; i += PacketFloat32Avx.LaneCount)
        {
            for (int lane = 0; lane < PacketFloat32Avx.LaneCount; lane++)
            {
                values[lane] = -range + ((i + lane) * step);
            }

            AccumulateFloatTrigErrors(
                values,
                ref sinStats,
                ref cosStats,
                ref sinCosSinStats,
                ref sinCosCosStats);
        }

        AssertFloatTrigStats("PacketFloat32Avx.Sin", sinStats);
        AssertFloatTrigStats("PacketFloat32Avx.Cos", cosStats);
        AssertFloatTrigStats("PacketFloat32Avx.SinCos.Sin", sinCosSinStats);
        AssertFloatTrigStats("PacketFloat32Avx.SinCos.Cos", sinCosCosStats);
    }

    private static void AccumulateFloatTrigErrors(
        ReadOnlySpan<float> input,
        ref FloatTrigStats sinStats,
        ref FloatTrigStats cosStats,
        ref FloatTrigStats sinCosSinStats,
        ref FloatTrigStats sinCosCosStats)
    {
        PacketFloat32Avx value = PacketFloat32Avx.Load(input);
        PacketFloat32Avx sin = PacketFloat32Avx.Sin(value);
        PacketFloat32Avx cos = PacketFloat32Avx.Cos(value);
        (PacketFloat32Avx sinCosSin, PacketFloat32Avx sinCosCos) = PacketFloat32Avx.SinCos(value);

        Span<float> sinValues = stackalloc float[PacketFloat32Avx.LaneCount];
        Span<float> cosValues = stackalloc float[PacketFloat32Avx.LaneCount];
        Span<float> sinCosSinValues = stackalloc float[PacketFloat32Avx.LaneCount];
        Span<float> sinCosCosValues = stackalloc float[PacketFloat32Avx.LaneCount];

        PacketFloat32Avx.Store(sin, sinValues);
        PacketFloat32Avx.Store(cos, cosValues);
        PacketFloat32Avx.Store(sinCosSin, sinCosSinValues);
        PacketFloat32Avx.Store(sinCosCos, sinCosCosValues);

        for (int lane = 0; lane < PacketFloat32Avx.LaneCount; lane++)
        {
            float expectedSin = MathF.Sin(input[lane]);
            float expectedCos = MathF.Cos(input[lane]);
            sinStats.Accumulate(input[lane], expectedSin, sinValues[lane]);
            cosStats.Accumulate(input[lane], expectedCos, cosValues[lane]);
            sinCosSinStats.Accumulate(input[lane], expectedSin, sinCosSinValues[lane]);
            sinCosCosStats.Accumulate(input[lane], expectedCos, sinCosCosValues[lane]);
        }
    }

    private static void AssertFloatTrigStats(string name, FloatTrigStats stats)
    {
        Assert.IsTrue(stats.MaxAbs <= 2.5e-7f, $"{name} max abs error {stats.MaxAbs:R} at {stats.MaxAbsInput:R} exceeded tolerance.");
        Assert.IsTrue(stats.MaxUlpAwayFromZero <= 4, $"{name} max ULP {stats.MaxUlpAwayFromZero} at {stats.MaxUlpInput:R} exceeded tolerance.");
    }

    private struct FloatTrigStats
    {
        public float MaxAbs;
        public float MaxAbsInput;
        public int MaxUlpAwayFromZero;
        public float MaxUlpInput;

        public void Accumulate(float input, float expected, float actual)
        {
            float absError = float.Abs(actual - expected);
            if (absError > MaxAbs)
            {
                MaxAbs = absError;
                MaxAbsInput = input;
            }

            if (float.Abs(expected) <= 1e-3f)
            {
                return;
            }

            int ulp = UlpDistance(expected, actual);
            if (ulp > MaxUlpAwayFromZero)
            {
                MaxUlpAwayFromZero = ulp;
                MaxUlpInput = input;
            }
        }

        private static int UlpDistance(float left, float right)
        {
            int leftBits = OrderedBits(left);
            int rightBits = OrderedBits(right);
            return int.Abs(leftBits - rightBits);
        }

        private static int OrderedBits(float value)
        {
            int bits = BitConverter.SingleToInt32Bits(value);
            return bits < 0 ? int.MinValue - bits : bits;
        }
    }
}
