using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarVector4UInt64Tests : Vector4UInt64Tests<ScalarVector4UInt64, ScalarUInt64, ScalarVector4UInt64Mask, ScalarUInt64Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
