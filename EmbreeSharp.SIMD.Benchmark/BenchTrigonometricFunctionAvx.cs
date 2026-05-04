using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace EmbreeSharp.SIMD.Benchmark;

[DisassemblyDiagnoser]
[GroupBenchmarksBy(BenchmarkLogicalGroupRule.ByCategory)]
[CategoriesColumn]
public unsafe class BenchTrigonometricFunctionAvx
{
    private ScalarFloat32x8* _scalarFloatInput = null;
    private PacketFloat32Avx* _packetFloatInput = null;
    private Vector256<float>* _vectorFloatInput = null;
    private ScalarFloat64x4* _scalarDoubleInput = null;
    private PacketFloat64Avx* _packetDoubleInput = null;
    private Vector256<double>* _vectorDoubleInput = null;

    [GlobalSetup]
    public void Setup()
    {
        if (!Avx.IsSupported)
        {
            throw new PlatformNotSupportedException("AVX is required for PacketFloat32Avx and PacketFloat64Avx benchmarks.");
        }

        Span<float> floatValues =
        [
            -9.5f,
            -3.25f,
            -1.0f,
            -0.125f,
            0.25f,
            1.5f,
            4.0f,
            11.0f
        ];

        Span<double> doubleValues =
        [
            -9.5d,
            -1.25d,
            0.5d,
            8.0d
        ];

        _scalarFloatInput = (ScalarFloat32x8*)NativeMemory.AlignedAlloc((nuint)sizeof(ScalarFloat32x8), 32);
        *_scalarFloatInput = ScalarFloat32x8.Load(floatValues);

        _packetFloatInput = (PacketFloat32Avx*)NativeMemory.AlignedAlloc((nuint)sizeof(PacketFloat32Avx), 32);
        *_packetFloatInput = PacketFloat32Avx.Load(floatValues);

        _vectorFloatInput = (Vector256<float>*)NativeMemory.AlignedAlloc((nuint)sizeof(Vector256<float>), 32);
        *_vectorFloatInput = Vector256.Create(floatValues);

        _scalarDoubleInput = (ScalarFloat64x4*)NativeMemory.AlignedAlloc((nuint)sizeof(ScalarFloat64x4), 32);
        *_scalarDoubleInput = ScalarFloat64x4.Load(doubleValues);

        _packetDoubleInput = (PacketFloat64Avx*)NativeMemory.AlignedAlloc((nuint)sizeof(PacketFloat64Avx), 32);
        *_packetDoubleInput = PacketFloat64Avx.Load(doubleValues);

        _vectorDoubleInput = (Vector256<double>*)NativeMemory.AlignedAlloc((nuint)sizeof(Vector256<double>), 32);
        *_vectorDoubleInput = Vector256.Create(doubleValues);

    }

    [GlobalCleanup]
    public void Dispose()
    {
        if (_scalarFloatInput != null)
        {
            NativeMemory.AlignedFree(_scalarFloatInput);
            _scalarFloatInput = null;
        }

        if (_packetFloatInput != null)
        {
            NativeMemory.AlignedFree(_packetFloatInput);
            _packetFloatInput = null;
        }

        if (_vectorFloatInput != null)
        {
            NativeMemory.AlignedFree(_vectorFloatInput);
            _vectorFloatInput = null;
        }

        if (_scalarDoubleInput != null)
        {
            NativeMemory.AlignedFree(_scalarDoubleInput);
            _scalarDoubleInput = null;
        }

        if (_packetDoubleInput != null)
        {
            NativeMemory.AlignedFree(_packetDoubleInput);
            _packetDoubleInput = null;
        }

        if (_vectorDoubleInput != null)
        {
            NativeMemory.AlignedFree(_vectorDoubleInput);
            _vectorDoubleInput = null;
        }
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Float32Sin")]
    public ScalarFloat32x8 FloatSinScalar()
    {
        return ScalarFloat32x8.Sin(*_scalarFloatInput);
    }

    [Benchmark]
    [BenchmarkCategory("Float32Sin")]
    public PacketFloat32Avx FloatSinAvx()
    {
        return PacketFloat32Avx.Sin(*_packetFloatInput);
    }

    [Benchmark]
    [BenchmarkCategory("Float32Sin")]
    public Vector256<float> FloatSinVector256()
    {
        return Vector256.Sin(*_vectorFloatInput);
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Float32Cos")]
    public ScalarFloat32x8 FloatCosScalar()
    {
        return ScalarFloat32x8.Cos(*_scalarFloatInput);
    }

    [Benchmark]
    [BenchmarkCategory("Float32Cos")]
    public PacketFloat32Avx FloatCosAvx()
    {
        return PacketFloat32Avx.Cos(*_packetFloatInput);
    }

    [Benchmark]
    [BenchmarkCategory("Float32Cos")]
    public Vector256<float> FloatCosVector256()
    {
        return Vector256.Cos(*_vectorFloatInput);
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Float32SinCos")]
    public ScalarFloat32Pairx8 FloatSinCosScalar()
    {
        return ScalarFloat32x8.SinCos(*_scalarFloatInput);
    }

    [Benchmark]
    [BenchmarkCategory("Float32SinCos")]
    public PacketFloat32AvxPair FloatSinCosAvx()
    {
        (PacketFloat32Avx sin, PacketFloat32Avx cos) = PacketFloat32Avx.SinCos(*_packetFloatInput);
        return new(sin, cos);
    }

    [Benchmark]
    [BenchmarkCategory("Float32SinCos")]
    public Vector256FloatPair FloatSinCosVector256()
    {
        (Vector256<float> sin, Vector256<float> cos) = Vector256.SinCos(*_vectorFloatInput);
        return new(sin, cos);
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Float64Sin")]
    public ScalarFloat64x4 DoubleSinScalar()
    {
        return ScalarFloat64x4.Sin(*_scalarDoubleInput);
    }

    [Benchmark]
    [BenchmarkCategory("Float64Sin")]
    public PacketFloat64Avx DoubleSinAvx()
    {
        return PacketFloat64Avx.Sin(*_packetDoubleInput);
    }

    [Benchmark]
    [BenchmarkCategory("Float64Sin")]
    public Vector256<double> DoubleSinVector256()
    {
        return Vector256.Sin(*_vectorDoubleInput);
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Float64Cos")]
    public ScalarFloat64x4 DoubleCosScalar()
    {
        return ScalarFloat64x4.Cos(*_scalarDoubleInput);
    }

    [Benchmark]
    [BenchmarkCategory("Float64Cos")]
    public PacketFloat64Avx DoubleCosAvx()
    {
        return PacketFloat64Avx.Cos(*_packetDoubleInput);
    }

    [Benchmark]
    [BenchmarkCategory("Float64Cos")]
    public Vector256<double> DoubleCosVector256()
    {
        return Vector256.Cos(*_vectorDoubleInput);
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Float64SinCos")]
    public ScalarFloat64Pairx4 DoubleSinCosScalar()
    {
        return ScalarFloat64x4.SinCos(*_scalarDoubleInput);
    }

    [Benchmark]
    [BenchmarkCategory("Float64SinCos")]
    public PacketFloat64AvxPair DoubleSinCosAvx()
    {
        (PacketFloat64Avx sin, PacketFloat64Avx cos) = PacketFloat64Avx.SinCos(*_packetDoubleInput);
        return new(sin, cos);
    }

    [Benchmark]
    [BenchmarkCategory("Float64SinCos")]
    public Vector256DoublePair DoubleSinCosVector256()
    {
        (Vector256<double> sin, Vector256<double> cos) = Vector256.SinCos(*_vectorDoubleInput);
        return new(sin, cos);
    }

}
