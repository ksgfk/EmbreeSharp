using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarVector4Int32Tests : Vector4Int32Tests<ScalarVector4Int32, ScalarInt32, ScalarVector4Int32Mask, ScalarInt32Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
