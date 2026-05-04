using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarVector4UInt32Tests : Vector4UInt32Tests<ScalarVector4UInt32, ScalarUInt32, ScalarVector4UInt32Mask, ScalarUInt32Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
