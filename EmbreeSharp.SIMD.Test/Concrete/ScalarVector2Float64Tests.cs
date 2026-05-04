using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarVector2Float64Tests : Vector2Float64Tests<ScalarVector2Float64, ScalarFloat64, ScalarVector2Float64Mask, ScalarFloat64Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
