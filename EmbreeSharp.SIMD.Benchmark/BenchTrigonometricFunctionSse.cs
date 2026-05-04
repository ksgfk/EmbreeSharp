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
public unsafe class BenchTrigonometricFunctionSse
{
    private ScalarFloat32x4* _scalarFloatInput = null;
    private PacketFloat32Sse* _packetFloatInput = null;
    private Vector128<float>* _vectorFloatInput = null;
    private ScalarFloat64x2* _scalarDoubleInput = null;
    private PacketFloat64Sse* _packetDoubleInput = null;
    private Vector128<double>* _vectorDoubleInput = null;

    [GlobalSetup]
    public void Setup()
    {
        if (!Sse41.IsSupported)
        {
            throw new PlatformNotSupportedException("SSE4.1 is required for PacketFloat32Sse and PacketFloat64Sse benchmarks.");
        }

        Span<float> floatValues =
        [
            -3.25f,
            -0.125f,
            0.25f,
            1.5f
        ];

        Span<double> doubleValues =
        [
            -1.25d,
            0.5d
        ];

        _scalarFloatInput = (ScalarFloat32x4*)NativeMemory.AlignedAlloc((nuint)sizeof(ScalarFloat32x4), 16);
        *_scalarFloatInput = ScalarFloat32x4.Load(floatValues);

        _packetFloatInput = (PacketFloat32Sse*)NativeMemory.AlignedAlloc((nuint)sizeof(PacketFloat32Sse), 16);
        *_packetFloatInput = PacketFloat32Sse.Load(floatValues);

        _vectorFloatInput = (Vector128<float>*)NativeMemory.AlignedAlloc((nuint)sizeof(Vector128<float>), 16);
        *_vectorFloatInput = Vector128.Create(floatValues);

        _scalarDoubleInput = (ScalarFloat64x2*)NativeMemory.AlignedAlloc((nuint)sizeof(ScalarFloat64x2), 16);
        *_scalarDoubleInput = ScalarFloat64x2.Load(doubleValues);

        _packetDoubleInput = (PacketFloat64Sse*)NativeMemory.AlignedAlloc((nuint)sizeof(PacketFloat64Sse), 16);
        *_packetDoubleInput = PacketFloat64Sse.Load(doubleValues);

        _vectorDoubleInput = (Vector128<double>*)NativeMemory.AlignedAlloc((nuint)sizeof(Vector128<double>), 16);
        *_vectorDoubleInput = Vector128.Create(doubleValues);
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
    public ScalarFloat32x4 FloatSinScalar()
    {
        return ScalarFloat32x4.Sin(*_scalarFloatInput);
    }

    [Benchmark]
    [BenchmarkCategory("Float32Sin")]
    public PacketFloat32Sse FloatSinSse()
    {
        return PacketFloat32Sse.Sin(*_packetFloatInput);
    }

    [Benchmark]
    [BenchmarkCategory("Float32Sin")]
    public Vector128<float> FloatSinVector128()
    {
        return Vector128.Sin(*_vectorFloatInput);
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Float32Cos")]
    public ScalarFloat32x4 FloatCosScalar()
    {
        return ScalarFloat32x4.Cos(*_scalarFloatInput);
    }

    [Benchmark]
    [BenchmarkCategory("Float32Cos")]
    public PacketFloat32Sse FloatCosSse()
    {
        return PacketFloat32Sse.Cos(*_packetFloatInput);
    }

    [Benchmark]
    [BenchmarkCategory("Float32Cos")]
    public Vector128<float> FloatCosVector128()
    {
        return Vector128.Cos(*_vectorFloatInput);
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Float32SinCos")]
    public ScalarFloat32Pairx4 FloatSinCosScalar()
    {
        return ScalarFloat32x4.SinCos(*_scalarFloatInput);
    }

    [Benchmark]
    [BenchmarkCategory("Float32SinCos")]
    public PacketFloat32SsePair FloatSinCosSse()
    {
        (PacketFloat32Sse sin, PacketFloat32Sse cos) = PacketFloat32Sse.SinCos(*_packetFloatInput);
        return new(sin, cos);
    }

    [Benchmark]
    [BenchmarkCategory("Float32SinCos")]
    public Vector128FloatPair FloatSinCosVector128()
    {
        (Vector128<float> sin, Vector128<float> cos) = Vector128.SinCos(*_vectorFloatInput);
        return new(sin, cos);
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Float64Sin")]
    public ScalarFloat64x2 DoubleSinScalar()
    {
        return ScalarFloat64x2.Sin(*_scalarDoubleInput);
    }

    [Benchmark]
    [BenchmarkCategory("Float64Sin")]
    public PacketFloat64Sse DoubleSinSse()
    {
        return PacketFloat64Sse.Sin(*_packetDoubleInput);
    }

    [Benchmark]
    [BenchmarkCategory("Float64Sin")]
    public Vector128<double> DoubleSinVector128()
    {
        return Vector128.Sin(*_vectorDoubleInput);
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Float64Cos")]
    public ScalarFloat64x2 DoubleCosScalar()
    {
        return ScalarFloat64x2.Cos(*_scalarDoubleInput);
    }

    [Benchmark]
    [BenchmarkCategory("Float64Cos")]
    public PacketFloat64Sse DoubleCosSse()
    {
        return PacketFloat64Sse.Cos(*_packetDoubleInput);
    }

    [Benchmark]
    [BenchmarkCategory("Float64Cos")]
    public Vector128<double> DoubleCosVector128()
    {
        return Vector128.Cos(*_vectorDoubleInput);
    }

    [Benchmark(Baseline = true)]
    [BenchmarkCategory("Float64SinCos")]
    public ScalarFloat64Pairx2 DoubleSinCosScalar()
    {
        return ScalarFloat64x2.SinCos(*_scalarDoubleInput);
    }

    [Benchmark]
    [BenchmarkCategory("Float64SinCos")]
    public PacketFloat64SsePair DoubleSinCosSse()
    {
        (PacketFloat64Sse sin, PacketFloat64Sse cos) = PacketFloat64Sse.SinCos(*_packetDoubleInput);
        return new(sin, cos);
    }

    [Benchmark]
    [BenchmarkCategory("Float64SinCos")]
    public Vector128DoublePair DoubleSinCosVector128()
    {
        (Vector128<double> sin, Vector128<double> cos) = Vector128.SinCos(*_vectorDoubleInput);
        return new(sin, cos);
    }
}
