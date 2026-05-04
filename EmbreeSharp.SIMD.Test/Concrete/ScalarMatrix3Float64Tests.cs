using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarMatrix3Float64Tests : Matrix3Float64Tests<ScalarMatrix3Float64, ScalarVector3Float64, ScalarFloat64, ScalarMatrix3Float64Mask, ScalarVector3Float64Mask, ScalarFloat64Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
