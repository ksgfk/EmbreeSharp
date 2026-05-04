using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarVector2Float32Tests : Vector2Float32Tests<ScalarVector2Float32, ScalarFloat32, ScalarVector2Float32Mask, ScalarFloat32Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
