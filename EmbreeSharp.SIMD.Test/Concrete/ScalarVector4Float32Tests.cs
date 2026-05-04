using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarVector4Float32Tests : Vector4Float32Tests<ScalarVector4Float32, ScalarFloat32, ScalarVector4Float32Mask, ScalarFloat32Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
