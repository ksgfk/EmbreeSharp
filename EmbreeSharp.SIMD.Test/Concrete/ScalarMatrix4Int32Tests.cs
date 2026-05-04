using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarMatrix4Int32Tests : Matrix4Int32Tests<ScalarMatrix4Int32, ScalarVector4Int32, ScalarInt32, ScalarMatrix4Int32Mask, ScalarVector4Int32Mask, ScalarInt32Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
