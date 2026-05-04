using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarVector3Float64Tests : Vector3Float64Tests<ScalarVector3Float64, ScalarFloat64, ScalarVector3Float64Mask, ScalarFloat64Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
