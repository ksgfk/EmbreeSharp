using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarQuaternionFloat64Tests : QuaternionFloat64Tests<ScalarQuaternionFloat64, ScalarVector3Float64, ScalarVector4Float64, ScalarFloat64, ScalarQuaternionFloat64Mask, ScalarVector3Float64Mask, ScalarVector4Float64Mask, ScalarFloat64Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
