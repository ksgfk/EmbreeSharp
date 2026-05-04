using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarMatrix4Float32Tests : Matrix4Float32Tests<ScalarMatrix4Float32, ScalarVector4Float32, ScalarFloat32, ScalarMatrix4Float32Mask, ScalarVector4Float32Mask, ScalarFloat32Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
