using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarMatrix2Float64Tests : Matrix2Float64Tests<ScalarMatrix2Float64, ScalarVector2Float64, ScalarFloat64, ScalarMatrix2Float64Mask, ScalarVector2Float64Mask, ScalarFloat64Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
