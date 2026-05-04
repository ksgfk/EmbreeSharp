using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarMatrix3Float32Tests : Matrix3Float32Tests<ScalarMatrix3Float32, ScalarVector3Float32, ScalarFloat32, ScalarMatrix3Float32Mask, ScalarVector3Float32Mask, ScalarFloat32Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
