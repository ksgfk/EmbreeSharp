using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarVector2UInt32Tests : Vector2UInt32Tests<ScalarVector2UInt32, ScalarUInt32, ScalarVector2UInt32Mask, ScalarUInt32Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
