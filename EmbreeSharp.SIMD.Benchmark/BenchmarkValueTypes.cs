using System.Runtime.Intrinsics;

namespace EmbreeSharp.SIMD.Benchmark;

public readonly struct PacketFloat32AvxPair(PacketFloat32Avx a, PacketFloat32Avx b)
{
    public readonly PacketFloat32Avx A = a;
    public readonly PacketFloat32Avx B = b;
}

public readonly struct PacketFloat64AvxPair(PacketFloat64Avx a, PacketFloat64Avx b)
{
    public readonly PacketFloat64Avx A = a;
    public readonly PacketFloat64Avx B = b;
}

public readonly struct PacketFloat32SsePair(PacketFloat32Sse a, PacketFloat32Sse b)
{
    public readonly PacketFloat32Sse A = a;
    public readonly PacketFloat32Sse B = b;
}

public readonly struct PacketFloat64SsePair(PacketFloat64Sse a, PacketFloat64Sse b)
{
    public readonly PacketFloat64Sse A = a;
    public readonly PacketFloat64Sse B = b;
}

public readonly struct Vector256FloatPair(Vector256<float> a, Vector256<float> b)
{
    public readonly Vector256<float> A = a;
    public readonly Vector256<float> B = b;
}

public readonly struct Vector256DoublePair(Vector256<double> a, Vector256<double> b)
{
    public readonly Vector256<double> A = a;
    public readonly Vector256<double> B = b;
}

public readonly struct Vector128FloatPair(Vector128<float> a, Vector128<float> b)
{
    public readonly Vector128<float> A = a;
    public readonly Vector128<float> B = b;
}

public readonly struct Vector128DoublePair(Vector128<double> a, Vector128<double> b)
{
    public readonly Vector128<double> A = a;
    public readonly Vector128<double> B = b;
}

public readonly struct ScalarFloat32Pairx8(ScalarFloat32x8 a, ScalarFloat32x8 b)
{
    public readonly ScalarFloat32x8 A = a;
    public readonly ScalarFloat32x8 B = b;
}

public readonly struct ScalarFloat32Pairx4(ScalarFloat32x4 a, ScalarFloat32x4 b)
{
    public readonly ScalarFloat32x4 A = a;
    public readonly ScalarFloat32x4 B = b;
}

public readonly struct ScalarFloat64Pairx4(ScalarFloat64x4 a, ScalarFloat64x4 b)
{
    public readonly ScalarFloat64x4 A = a;
    public readonly ScalarFloat64x4 B = b;
}

public readonly struct ScalarFloat64Pairx2(ScalarFloat64x2 a, ScalarFloat64x2 b)
{
    public readonly ScalarFloat64x2 A = a;
    public readonly ScalarFloat64x2 B = b;
}

public readonly struct ScalarFloat32x8
{
    private readonly float _v0;
    private readonly float _v1;
    private readonly float _v2;
    private readonly float _v3;
    private readonly float _v4;
    private readonly float _v5;
    private readonly float _v6;
    private readonly float _v7;

    private ScalarFloat32x8(
        float v0,
        float v1,
        float v2,
        float v3,
        float v4,
        float v5,
        float v6,
        float v7)
    {
        _v0 = v0;
        _v1 = v1;
        _v2 = v2;
        _v3 = v3;
        _v4 = v4;
        _v5 = v5;
        _v6 = v6;
        _v7 = v7;
    }

    public static ScalarFloat32x8 Load(ReadOnlySpan<float> values)
    {
        return new(
            values[0],
            values[1],
            values[2],
            values[3],
            values[4],
            values[5],
            values[6],
            values[7]);
    }

    public void Store(Span<float> values)
    {
        values[0] = _v0;
        values[1] = _v1;
        values[2] = _v2;
        values[3] = _v3;
        values[4] = _v4;
        values[5] = _v5;
        values[6] = _v6;
        values[7] = _v7;
    }

    public static ScalarFloat32x8 Sin(ScalarFloat32x8 value)
    {
        return new(
            MathF.Sin(value._v0),
            MathF.Sin(value._v1),
            MathF.Sin(value._v2),
            MathF.Sin(value._v3),
            MathF.Sin(value._v4),
            MathF.Sin(value._v5),
            MathF.Sin(value._v6),
            MathF.Sin(value._v7));
    }

    public static ScalarFloat32x8 Cos(ScalarFloat32x8 value)
    {
        return new(
            MathF.Cos(value._v0),
            MathF.Cos(value._v1),
            MathF.Cos(value._v2),
            MathF.Cos(value._v3),
            MathF.Cos(value._v4),
            MathF.Cos(value._v5),
            MathF.Cos(value._v6),
            MathF.Cos(value._v7));
    }

    public static ScalarFloat32Pairx8 SinCos(ScalarFloat32x8 value)
    {
        (float s0, float c0) = MathF.SinCos(value._v0);
        (float s1, float c1) = MathF.SinCos(value._v1);
        (float s2, float c2) = MathF.SinCos(value._v2);
        (float s3, float c3) = MathF.SinCos(value._v3);
        (float s4, float c4) = MathF.SinCos(value._v4);
        (float s5, float c5) = MathF.SinCos(value._v5);
        (float s6, float c6) = MathF.SinCos(value._v6);
        (float s7, float c7) = MathF.SinCos(value._v7);

        return new(
            new(s0, s1, s2, s3, s4, s5, s6, s7),
            new(c0, c1, c2, c3, c4, c5, c6, c7));
    }
}

public readonly struct ScalarFloat32x4
{
    private readonly float _v0;
    private readonly float _v1;
    private readonly float _v2;
    private readonly float _v3;

    private ScalarFloat32x4(
        float v0,
        float v1,
        float v2,
        float v3)
    {
        _v0 = v0;
        _v1 = v1;
        _v2 = v2;
        _v3 = v3;
    }

    public static ScalarFloat32x4 Load(ReadOnlySpan<float> values)
    {
        return new(
            values[0],
            values[1],
            values[2],
            values[3]);
    }

    public void Store(Span<float> values)
    {
        values[0] = _v0;
        values[1] = _v1;
        values[2] = _v2;
        values[3] = _v3;
    }

    public static ScalarFloat32x4 Sin(ScalarFloat32x4 value)
    {
        return new(
            MathF.Sin(value._v0),
            MathF.Sin(value._v1),
            MathF.Sin(value._v2),
            MathF.Sin(value._v3));
    }

    public static ScalarFloat32x4 Cos(ScalarFloat32x4 value)
    {
        return new(
            MathF.Cos(value._v0),
            MathF.Cos(value._v1),
            MathF.Cos(value._v2),
            MathF.Cos(value._v3));
    }

    public static ScalarFloat32Pairx4 SinCos(ScalarFloat32x4 value)
    {
        (float s0, float c0) = MathF.SinCos(value._v0);
        (float s1, float c1) = MathF.SinCos(value._v1);
        (float s2, float c2) = MathF.SinCos(value._v2);
        (float s3, float c3) = MathF.SinCos(value._v3);

        return new(
            new(s0, s1, s2, s3),
            new(c0, c1, c2, c3));
    }
}

public readonly struct ScalarFloat64x4
{
    private readonly double _v0;
    private readonly double _v1;
    private readonly double _v2;
    private readonly double _v3;

    private ScalarFloat64x4(
        double v0,
        double v1,
        double v2,
        double v3)
    {
        _v0 = v0;
        _v1 = v1;
        _v2 = v2;
        _v3 = v3;
    }

    public static ScalarFloat64x4 Load(ReadOnlySpan<double> values)
    {
        return new(
            values[0],
            values[1],
            values[2],
            values[3]);
    }

    public void Store(Span<double> values)
    {
        values[0] = _v0;
        values[1] = _v1;
        values[2] = _v2;
        values[3] = _v3;
    }

    public static ScalarFloat64x4 Sin(ScalarFloat64x4 value)
    {
        return new(
            Math.Sin(value._v0),
            Math.Sin(value._v1),
            Math.Sin(value._v2),
            Math.Sin(value._v3));
    }

    public static ScalarFloat64x4 Cos(ScalarFloat64x4 value)
    {
        return new(
            Math.Cos(value._v0),
            Math.Cos(value._v1),
            Math.Cos(value._v2),
            Math.Cos(value._v3));
    }

    public static ScalarFloat64Pairx4 SinCos(ScalarFloat64x4 value)
    {
        (double s0, double c0) = Math.SinCos(value._v0);
        (double s1, double c1) = Math.SinCos(value._v1);
        (double s2, double c2) = Math.SinCos(value._v2);
        (double s3, double c3) = Math.SinCos(value._v3);

        return new(
            new(s0, s1, s2, s3),
            new(c0, c1, c2, c3));
    }
}

public readonly struct ScalarFloat64x2
{
    private readonly double _v0;
    private readonly double _v1;

    private ScalarFloat64x2(
        double v0,
        double v1)
    {
        _v0 = v0;
        _v1 = v1;
    }

    public static ScalarFloat64x2 Load(ReadOnlySpan<double> values)
    {
        return new(
            values[0],
            values[1]);
    }

    public void Store(Span<double> values)
    {
        values[0] = _v0;
        values[1] = _v1;
    }

    public static ScalarFloat64x2 Sin(ScalarFloat64x2 value)
    {
        return new(
            Math.Sin(value._v0),
            Math.Sin(value._v1));
    }

    public static ScalarFloat64x2 Cos(ScalarFloat64x2 value)
    {
        return new(
            Math.Cos(value._v0),
            Math.Cos(value._v1));
    }

    public static ScalarFloat64Pairx2 SinCos(ScalarFloat64x2 value)
    {
        (double s0, double c0) = Math.SinCos(value._v0);
        (double s1, double c1) = Math.SinCos(value._v1);

        return new(
            new(s0, s1),
            new(c0, c1));
    }
}
