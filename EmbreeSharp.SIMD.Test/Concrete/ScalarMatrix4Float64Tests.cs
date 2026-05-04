using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarMatrix4Float64Tests : Matrix4Float64Tests<ScalarMatrix4Float64, ScalarVector4Float64, ScalarFloat64, ScalarMatrix4Float64Mask, ScalarVector4Float64Mask, ScalarFloat64Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
