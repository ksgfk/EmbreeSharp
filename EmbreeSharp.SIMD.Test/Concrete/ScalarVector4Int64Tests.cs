using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarVector4Int64Tests : Vector4Int64Tests<ScalarVector4Int64, ScalarInt64, ScalarVector4Int64Mask, ScalarInt64Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
