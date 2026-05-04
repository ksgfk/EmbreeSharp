using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarVector3UInt32Tests : Vector3UInt32Tests<ScalarVector3UInt32, ScalarUInt32, ScalarVector3UInt32Mask, ScalarUInt32Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
