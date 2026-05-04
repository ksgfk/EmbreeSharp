using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarQuaternionFloat32Tests : QuaternionFloat32Tests<ScalarQuaternionFloat32, ScalarVector3Float32, ScalarVector4Float32, ScalarFloat32, ScalarQuaternionFloat32Mask, ScalarVector3Float32Mask, ScalarVector4Float32Mask, ScalarFloat32Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
