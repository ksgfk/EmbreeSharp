using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarVector2UInt64Tests : Vector2UInt64Tests<ScalarVector2UInt64, ScalarUInt64, ScalarVector2UInt64Mask, ScalarUInt64Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
