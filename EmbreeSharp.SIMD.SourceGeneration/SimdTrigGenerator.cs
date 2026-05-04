using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Text;

namespace EmbreeSharp.SIMD.SourceGeneration;

[Generator]
public sealed class SimdTrigGenerator : IIncrementalGenerator
{
    public void Initialize(IncrementalGeneratorInitializationContext context)
    {
        context.RegisterPostInitializationOutput(static output =>
        {
            foreach (TrigSpec spec in TrigSpec.All)
            {
                output.AddSource($"{spec.PacketType}.Trig.g.cs", SourceText.From(SimdTrigTemplate.Generate(spec), Encoding.UTF8));
            }

            foreach (Vector3CrossSpec spec in Vector3CrossSpec.All)
            {
                output.AddSource($"{spec.VectorType}.Cross.g.cs", SourceText.From(Vector3CrossTemplate.Generate(spec), Encoding.UTF8));
            }
        });
    }
}

internal sealed class Vector3CrossSpec
{
    public static readonly Vector3CrossSpec[] All =
    [
        new(
            VectorType: "PacketVector3Float32Sse",
            PacketType: "PacketFloat32Sse",
            FloatOps: "Sse"),
        new(
            VectorType: "PacketVector3Float32Avx",
            PacketType: "PacketFloat32Avx",
            FloatOps: "Avx"),
        new(
            VectorType: "PacketVector3Float64Sse",
            PacketType: "PacketFloat64Sse",
            FloatOps: "Sse2"),
        new(
            VectorType: "PacketVector3Float64Avx",
            PacketType: "PacketFloat64Avx",
            FloatOps: "Avx")
    ];

    public Vector3CrossSpec(
        string VectorType,
        string PacketType,
        string FloatOps)
    {
        this.VectorType = VectorType;
        this.PacketType = PacketType;
        this.FloatOps = FloatOps;
    }

    public string VectorType { get; }
    public string PacketType { get; }
    public string FloatOps { get; }
}

internal sealed class TrigSpec
{
    public static readonly TrigSpec[] All =
    [
        new(
            PacketType: "PacketFloat32Sse",
            ScalarType: "float",
            VectorType: "Vector128<float>",
            JType: "Vector128<int>",
            IntType: "Vector128<int>",
            FloatOps: "Sse",
            IntOps: "Sse2",
            BlendOps: "Sse41",
            LoadJ: """
        j = Sse2.ConvertToVector128Int32WithTruncation(Sse.Multiply(absValue, Vector128.Create(1.2732395447351626862f)));
        j = Sse2.And(Sse2.Add(j, Vector128.Create(1)), Vector128.Create(-2));
""",
            ConvertJToFloat: "Vector128<float> jFloat = Sse2.ConvertToVector128Single(j);",
            InfinityCompare: "Sse.CompareEqual(absValue, Vector128.Create(float.PositiveInfinity))",
            Blend: "Sse41.BlendVariable",
            FloatSuffix: "f",
            SignLiteral: "-0f",
            OneLiteral: "1f",
            InfinityLiteral: "float.PositiveInfinity",
            SinSignShift: 29,
            PolyShift: 30,
            IsDouble: false),
        new(
            PacketType: "PacketFloat64Sse",
            ScalarType: "double",
            VectorType: "Vector128<double>",
            JType: "Vector128<long>",
            IntType: "Vector128<long>",
            FloatOps: "Sse2",
            IntOps: "Sse2",
            BlendOps: "Sse41",
            LoadJ: """
        Vector128<int> j32 = Sse2.ConvertToVector128Int32WithTruncation(Sse2.Multiply(absValue, Vector128.Create(1.2732395447351626862d)));
        j32 = Sse2.And(Sse2.Add(j32, Vector128.Create(1)), Vector128.Create(-2));
        j = Sse41.ConvertToVector128Int64(j32);
""",
            ConvertJToFloat: "Vector128<double> jDouble = Sse2.ConvertToVector128Double(j32);",
            InfinityCompare: "Sse2.CompareEqual(absValue, Vector128.Create(double.PositiveInfinity))",
            Blend: "Sse41.BlendVariable",
            FloatSuffix: "d",
            SignLiteral: "-0d",
            OneLiteral: "1d",
            InfinityLiteral: "double.PositiveInfinity",
            SinSignShift: 61,
            PolyShift: 62,
            IsDouble: true),
        new(
            PacketType: "PacketFloat32Avx",
            ScalarType: "float",
            VectorType: "Vector256<float>",
            JType: "Vector256<int>",
            IntType: "Vector256<int>",
            FloatOps: "Avx",
            IntOps: "Avx2",
            BlendOps: "Avx",
            LoadJ: """
        j = Avx.ConvertToVector256Int32WithTruncation(Avx.Multiply(absValue, Vector256.Create(1.2732395447351626862f)));
        j = Avx2.And(Avx2.Add(j, Vector256.Create(1)), Vector256.Create(-2));
""",
            ConvertJToFloat: "Vector256<float> jFloat = Vector256.ConvertToSingle(j);",
            InfinityCompare: "Avx.Compare(absValue, Vector256.Create(float.PositiveInfinity), FloatComparisonMode.OrderedEqualNonSignaling)",
            Blend: "Avx.BlendVariable",
            FloatSuffix: "f",
            SignLiteral: "-0f",
            OneLiteral: "1f",
            InfinityLiteral: "float.PositiveInfinity",
            SinSignShift: 29,
            PolyShift: 30,
            IsDouble: false),
        new(
            PacketType: "PacketFloat64Avx",
            ScalarType: "double",
            VectorType: "Vector256<double>",
            JType: "Vector256<long>",
            IntType: "Vector256<long>",
            FloatOps: "Avx",
            IntOps: "Avx2",
            BlendOps: "Avx",
            LoadJ: """
        Vector128<int> j32 = Avx.ConvertToVector128Int32WithTruncation(Avx.Multiply(absValue, Vector256.Create(1.2732395447351626862d)));
        j32 = Sse2.And(Sse2.Add(j32, Vector128.Create(1)), Vector128.Create(-2));
        j = Avx2.ConvertToVector256Int64(j32);
""",
            ConvertJToFloat: "Vector256<double> jDouble = Avx.ConvertToVector256Double(j32);",
            InfinityCompare: "Avx.Compare(absValue, Vector256.Create(double.PositiveInfinity), FloatComparisonMode.OrderedEqualNonSignaling)",
            Blend: "Avx.BlendVariable",
            FloatSuffix: "d",
            SignLiteral: "-0d",
            OneLiteral: "1d",
            InfinityLiteral: "double.PositiveInfinity",
            SinSignShift: 61,
            PolyShift: 62,
            IsDouble: true)
    ];

    public TrigSpec(
        string PacketType,
        string ScalarType,
        string VectorType,
        string JType,
        string IntType,
        string FloatOps,
        string IntOps,
        string BlendOps,
        string LoadJ,
        string ConvertJToFloat,
        string InfinityCompare,
        string Blend,
        string FloatSuffix,
        string SignLiteral,
        string OneLiteral,
        string InfinityLiteral,
        int SinSignShift,
        int PolyShift,
        bool IsDouble)
    {
        this.PacketType = PacketType;
        this.ScalarType = ScalarType;
        this.VectorType = VectorType;
        this.JType = JType;
        this.IntType = IntType;
        this.FloatOps = FloatOps;
        this.IntOps = IntOps;
        this.BlendOps = BlendOps;
        this.LoadJ = LoadJ;
        this.ConvertJToFloat = ConvertJToFloat;
        this.InfinityCompare = InfinityCompare;
        this.Blend = Blend;
        this.FloatSuffix = FloatSuffix;
        this.SignLiteral = SignLiteral;
        this.OneLiteral = OneLiteral;
        this.InfinityLiteral = InfinityLiteral;
        this.SinSignShift = SinSignShift;
        this.PolyShift = PolyShift;
        this.IsDouble = IsDouble;
    }

    public string PacketType { get; }
    public string ScalarType { get; }
    public string VectorType { get; }
    public string JType { get; }
    public string IntType { get; }
    public string FloatOps { get; }
    public string IntOps { get; }
    public string BlendOps { get; }
    public string LoadJ { get; }
    public string ConvertJToFloat { get; }
    public string InfinityCompare { get; }
    public string Blend { get; }
    public string FloatSuffix { get; }
    public string SignLiteral { get; }
    public string OneLiteral { get; }
    public string InfinityLiteral { get; }
    public int SinSignShift { get; }
    public int PolyShift { get; }
    public bool IsDouble { get; }

    public string JFloatName => IsDouble ? "jDouble" : "jFloat";
}

internal static class SimdTrigTemplate
{
    public static string Generate(TrigSpec spec)
    {
        string source = """
// <auto-generated />
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace EmbreeSharp.SIMD;

public readonly partial struct {{PACKET}}
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static void PrepareTrig({{VECTOR}} value, out {{JTYPE}} j, out {{VECTOR}} sinApprox, out {{VECTOR}} cosApprox)
    {
        {{VECTOR}} absValue = {{FOPS}}.AndNot({{VEC}}.Create({{SIGN_LITERAL}}), value);
{{LOAD_J}}

        {{CONVERT_J}}
        {{VECTOR}} y;
        if (Fma.IsSupported)
        {
            y = Fma.MultiplyAddNegated({{JFLOAT}}, {{VEC}}.Create({{RR0}}), absValue);
            y = Fma.MultiplyAddNegated({{JFLOAT}}, {{VEC}}.Create({{RR1}}), y);
            y = Fma.MultiplyAddNegated({{JFLOAT}}, {{VEC}}.Create({{RR2}}), y);
        }
        else
        {
            y = {{FOPS}}.Subtract(absValue, {{FOPS}}.Multiply({{JFLOAT}}, {{VEC}}.Create({{RR0}})));
            y = {{FOPS}}.Subtract(y, {{FOPS}}.Multiply({{JFLOAT}}, {{VEC}}.Create({{RR1}})));
            y = {{FOPS}}.Subtract(y, {{FOPS}}.Multiply({{JFLOAT}}, {{VEC}}.Create({{RR2}})));
        }

        {{VECTOR}} z = {{FOPS}}.Multiply(y, y);
        z = {{FOPS}}.Or(z, {{INF_CMP}});
        {{Z_POWERS}}

        {{SIN_POLY}}
        sinApprox = {{FOPS}}.Multiply(sinPoly, z);

        {{COS_POLY}}
        cosApprox = {{FOPS}}.Multiply(cosPoly, z);

        sinApprox = Fma.IsSupported
            ? Fma.MultiplyAdd(sinApprox, y, y)
            : {{FOPS}}.Add({{FOPS}}.Multiply(sinApprox, y), y);
        {{VECTOR}} halfZ = Fma.IsSupported
            ? Fma.MultiplyAdd(z, {{VEC}}.Create({{MINUS_HALF}}), {{VEC}}.Create({{ONE_LITERAL}}))
            : {{FOPS}}.Add({{FOPS}}.Multiply(z, {{VEC}}.Create({{MINUS_HALF}})), {{VEC}}.Create({{ONE_LITERAL}}));
        cosApprox = Fma.IsSupported
            ? Fma.MultiplyAdd(cosApprox, z, halfZ)
            : {{FOPS}}.Add({{FOPS}}.Multiply(cosApprox, z), halfZ);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static {{VECTOR}} ApplySinSign({{JTYPE}} j, {{VECTOR}} value, {{VECTOR}} result)
    {
        {{INTVEC}} signMask = {{VEC}}.Create({{SIGN_LITERAL}}).As{{INTSCALAR}}();
        {{INTVEC}} sign = {{INTOPS}}.And(
            {{INTOPS}}.Xor(
                {{INTOPS}}.ShiftLeftLogical(j, {{SIN_SHIFT}}),
                value.As{{INTSCALAR}}()),
            signMask);
        return {{FOPS}}.Xor(result, sign.As{{FLOATSCALAR}}());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static {{VECTOR}} ApplyCosSign({{JTYPE}} j, {{VECTOR}} result)
    {
        {{INTVEC}} signMask = {{VEC}}.Create({{SIGN_LITERAL}}).As{{INTSCALAR}}();
        {{INTVEC}} sign = {{INTOPS}}.And(
            {{INTOPS}}.ShiftLeftLogical({{INTOPS}}.Add(j, {{IVEC}}.Create({{TWO_LITERAL}})), {{SIN_SHIFT}}),
            signMask);
        return {{FOPS}}.Xor(result, sign.As{{FLOATSCALAR}}());
    }

    public static partial {{PACKET}} Sin({{PACKET}} value)
    {
        PrepareTrig(value._value, out {{JTYPE}} j, out {{VECTOR}} sinApprox, out {{VECTOR}} cosApprox);
        {{VECTOR}} result = {{BLEND}}(sinApprox, cosApprox, {{INTOPS}}.ShiftLeftLogical(j, {{POLY_SHIFT}}).As{{FLOATSCALAR}}());
        return new(ApplySinSign(j, value._value, result));
    }

    public static partial {{PACKET}} Cos({{PACKET}} value)
    {
        PrepareTrig(value._value, out {{JTYPE}} j, out {{VECTOR}} sinApprox, out {{VECTOR}} cosApprox);
        {{VECTOR}} result = {{BLEND}}(cosApprox, sinApprox, {{INTOPS}}.ShiftLeftLogical(j, {{POLY_SHIFT}}).As{{FLOATSCALAR}}());
        return new(ApplyCosSign(j, result));
    }

    public static partial ({{PACKET}} Sin, {{PACKET}} Cos) SinCos({{PACKET}} value)
    {
        PrepareTrig(value._value, out {{JTYPE}} j, out {{VECTOR}} sinApprox, out {{VECTOR}} cosApprox);
        {{VECTOR}} mask = {{INTOPS}}.ShiftLeftLogical(j, {{POLY_SHIFT}}).As{{FLOATSCALAR}}();
        {{PACKET}} sin = new(ApplySinSign(j, value._value, {{BLEND}}(sinApprox, cosApprox, mask)));
        {{PACKET}} cos = new(ApplyCosSign(j, {{BLEND}}(cosApprox, sinApprox, mask)));
        return (sin, cos);
    }
}
""";

        return source
            .Replace("{{PACKET}}", spec.PacketType)
            .Replace("{{VECTOR}}", spec.VectorType)
            .Replace("{{JTYPE}}", spec.JType)
            .Replace("{{INTVEC}}", spec.IntType)
            .Replace("{{FOPS}}", spec.FloatOps)
            .Replace("{{INTOPS}}", spec.IntOps)
            .Replace("{{BLENDOPS}}", spec.BlendOps)
            .Replace("{{BLEND}}", spec.Blend)
            .Replace("{{VEC}}", VectorPrefix(spec.VectorType))
            .Replace("{{IVEC}}", VectorPrefix(spec.IntType))
            .Replace("{{LOAD_J}}", Indent(spec.LoadJ, 8))
            .Replace("{{CONVERT_J}}", spec.ConvertJToFloat)
            .Replace("{{JFLOAT}}", spec.JFloatName)
            .Replace("{{INF_CMP}}", spec.InfinityCompare)
            .Replace("{{Z_POWERS}}", Indent(ZPowers(spec), 8))
            .Replace("{{SIN_POLY}}", Indent(SinPolynomial(spec), 8))
            .Replace("{{COS_POLY}}", Indent(CosPolynomial(spec), 8))
            .Replace("{{RR0}}", spec.IsDouble ? "0.785398125648498535156d" : "0.78515625f")
            .Replace("{{RR1}}", spec.IsDouble ? "3.77489470793079817668e-8d" : "2.4187564849853515625e-4f")
            .Replace("{{RR2}}", spec.IsDouble ? "2.69515142907905952645e-15d" : "3.77489497744594108e-8f")
            .Replace("{{SIGN_LITERAL}}", spec.SignLiteral)
            .Replace("{{ONE_LITERAL}}", spec.OneLiteral)
            .Replace("{{MINUS_HALF}}", spec.IsDouble ? "-0.5d" : "-0.5f")
            .Replace("{{TWO_LITERAL}}", spec.IsDouble ? "2L" : "2")
            .Replace("{{SIN_SHIFT}}", spec.SinSignShift.ToString())
            .Replace("{{POLY_SHIFT}}", spec.PolyShift.ToString())
            .Replace("{{INTSCALAR}}", spec.IsDouble ? "Int64" : "Int32")
            .Replace("{{FLOATSCALAR}}", spec.IsDouble ? "Double" : "Single");
    }

    private static string VectorPrefix(string vectorType)
    {
        return vectorType.StartsWith("Vector256") ? "Vector256" : "Vector128";
    }

    private static string ZPowers(TrigSpec spec)
    {
        string fops = spec.FloatOps;
        return spec.IsDouble
            ? $$"""
{{spec.VectorType}} z2 = {{fops}}.Multiply(z, z);
{{spec.VectorType}} z4 = {{fops}}.Multiply(z2, z2);
"""
            : $$"""
{{spec.VectorType}} z2 = {{fops}}.Multiply(z, z);
""";
    }

    private static string SinPolynomial(TrigSpec spec)
    {
        string vector = spec.VectorType;
        string fops = spec.FloatOps;
        string vec = VectorPrefix(spec.VectorType);
        return spec.IsDouble
            ? $$"""
{{vector}} sinPoly0 = Fma.IsSupported
    ? Fma.MultiplyAdd(z, {{vec}}.Create(8.33333333332211858878e-3d), {{vec}}.Create(-1.66666666666666307295e-1d))
    : {{fops}}.Add({{fops}}.Multiply(z, {{vec}}.Create(8.33333333332211858878e-3d)), {{vec}}.Create(-1.66666666666666307295e-1d));
{{vector}} sinPoly1 = Fma.IsSupported
    ? Fma.MultiplyAdd(z, {{vec}}.Create(2.75573136213857245213e-6d), {{vec}}.Create(-1.98412698295895385996e-4d))
    : {{fops}}.Add({{fops}}.Multiply(z, {{vec}}.Create(2.75573136213857245213e-6d)), {{vec}}.Create(-1.98412698295895385996e-4d));
{{vector}} sinPoly2 = Fma.IsSupported
    ? Fma.MultiplyAdd(z, {{vec}}.Create(1.58962301576546568060e-10d), {{vec}}.Create(-2.50507477628578072866e-8d))
    : {{fops}}.Add({{fops}}.Multiply(z, {{vec}}.Create(1.58962301576546568060e-10d)), {{vec}}.Create(-2.50507477628578072866e-8d));
{{vector}} sinPoly = Fma.IsSupported
    ? Fma.MultiplyAdd(z2, sinPoly1, sinPoly0)
    : {{fops}}.Add({{fops}}.Multiply(z2, sinPoly1), sinPoly0);
sinPoly = Fma.IsSupported
    ? Fma.MultiplyAdd(z4, sinPoly2, sinPoly)
    : {{fops}}.Add({{fops}}.Multiply(z4, sinPoly2), sinPoly);
"""
            : $$"""
{{vector}} sinPoly = Fma.IsSupported
    ? Fma.MultiplyAdd(z, {{vec}}.Create(8.3321608736e-3f), {{vec}}.Create(-1.6666654611e-1f))
    : {{fops}}.Add({{fops}}.Multiply(z, {{vec}}.Create(8.3321608736e-3f)), {{vec}}.Create(-1.6666654611e-1f));
sinPoly = Fma.IsSupported
    ? Fma.MultiplyAdd(z2, {{vec}}.Create(-1.9515295891e-4f), sinPoly)
    : {{fops}}.Add({{fops}}.Multiply(z2, {{vec}}.Create(-1.9515295891e-4f)), sinPoly);
""";
    }

    private static string CosPolynomial(TrigSpec spec)
    {
        string vector = spec.VectorType;
        string fops = spec.FloatOps;
        string vec = VectorPrefix(spec.VectorType);
        return spec.IsDouble
            ? $$"""
{{vector}} cosPoly0 = Fma.IsSupported
    ? Fma.MultiplyAdd(z, {{vec}}.Create(-1.38888888888730564116e-3d), {{vec}}.Create(4.16666666666665929218e-2d))
    : {{fops}}.Add({{fops}}.Multiply(z, {{vec}}.Create(-1.38888888888730564116e-3d)), {{vec}}.Create(4.16666666666665929218e-2d));
{{vector}} cosPoly1 = Fma.IsSupported
    ? Fma.MultiplyAdd(z, {{vec}}.Create(-2.75573141792967388112e-7d), {{vec}}.Create(2.48015872888517045348e-5d))
    : {{fops}}.Add({{fops}}.Multiply(z, {{vec}}.Create(-2.75573141792967388112e-7d)), {{vec}}.Create(2.48015872888517045348e-5d));
{{vector}} cosPoly2 = Fma.IsSupported
    ? Fma.MultiplyAdd(z, {{vec}}.Create(-1.13585365213876817300e-11d), {{vec}}.Create(2.08757008419747316778e-9d))
    : {{fops}}.Add({{fops}}.Multiply(z, {{vec}}.Create(-1.13585365213876817300e-11d)), {{vec}}.Create(2.08757008419747316778e-9d));
{{vector}} cosPoly = Fma.IsSupported
    ? Fma.MultiplyAdd(z2, cosPoly1, cosPoly0)
    : {{fops}}.Add({{fops}}.Multiply(z2, cosPoly1), cosPoly0);
cosPoly = Fma.IsSupported
    ? Fma.MultiplyAdd(z4, cosPoly2, cosPoly)
    : {{fops}}.Add({{fops}}.Multiply(z4, cosPoly2), cosPoly);
"""
            : $$"""
{{vector}} cosPoly = Fma.IsSupported
    ? Fma.MultiplyAdd(z, {{vec}}.Create(-1.388731625493765e-3f), {{vec}}.Create(4.166664568298827e-2f))
    : {{fops}}.Add({{fops}}.Multiply(z, {{vec}}.Create(-1.388731625493765e-3f)), {{vec}}.Create(4.166664568298827e-2f));
cosPoly = Fma.IsSupported
    ? Fma.MultiplyAdd(z2, {{vec}}.Create(2.443315711809948e-5f), cosPoly)
    : {{fops}}.Add({{fops}}.Multiply(z2, {{vec}}.Create(2.443315711809948e-5f)), cosPoly);
""";
    }

    private static string Indent(string value, int spaces)
    {
        string prefix = new(' ', spaces);
        string normalized = value.Trim('\r', '\n');
        return prefix + normalized.Replace("\r\n", "\n").Replace("\n", "\n" + prefix);
    }
}

internal static class Vector3CrossTemplate
{
    public static string Generate(Vector3CrossSpec spec)
    {
        string source = """
// <auto-generated />
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace EmbreeSharp.SIMD;

public readonly partial struct {{VECTOR_TYPE}}
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static partial {{VECTOR_TYPE}} Cross({{VECTOR_TYPE}} left, {{VECTOR_TYPE}} right)
    {
        {{VECTOR_TYPE}} leftYzx = new(left.Y, left.Z, left.X);
        {{VECTOR_TYPE}} rightZxy = new(right.Z, right.X, right.Y);
        {{VECTOR_TYPE}} leftZxy = new(left.Z, left.X, left.Y);
        {{VECTOR_TYPE}} rightYzx = new(right.Y, right.Z, right.X);

        return new(
            new {{PACKET_TYPE}}(Fma.IsSupported
                ? Fma.MultiplySubtract(leftYzx.X._value, rightZxy.X._value, {{FOPS}}.Multiply(leftZxy.X._value, rightYzx.X._value))
                : {{FOPS}}.Subtract({{FOPS}}.Multiply(leftYzx.X._value, rightZxy.X._value), {{FOPS}}.Multiply(leftZxy.X._value, rightYzx.X._value))),
            new {{PACKET_TYPE}}(Fma.IsSupported
                ? Fma.MultiplySubtract(leftYzx.Y._value, rightZxy.Y._value, {{FOPS}}.Multiply(leftZxy.Y._value, rightYzx.Y._value))
                : {{FOPS}}.Subtract({{FOPS}}.Multiply(leftYzx.Y._value, rightZxy.Y._value), {{FOPS}}.Multiply(leftZxy.Y._value, rightYzx.Y._value))),
            new {{PACKET_TYPE}}(Fma.IsSupported
                ? Fma.MultiplySubtract(leftYzx.Z._value, rightZxy.Z._value, {{FOPS}}.Multiply(leftZxy.Z._value, rightYzx.Z._value))
                : {{FOPS}}.Subtract({{FOPS}}.Multiply(leftYzx.Z._value, rightZxy.Z._value), {{FOPS}}.Multiply(leftZxy.Z._value, rightYzx.Z._value))));
    }
}
""";

        return source
            .Replace("{{VECTOR_TYPE}}", spec.VectorType)
            .Replace("{{PACKET_TYPE}}", spec.PacketType)
            .Replace("{{FOPS}}", spec.FloatOps);
    }
}
