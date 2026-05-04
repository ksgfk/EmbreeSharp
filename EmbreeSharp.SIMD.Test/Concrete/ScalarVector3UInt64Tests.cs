using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarVector3UInt64Tests : Vector3UInt64Tests<ScalarVector3UInt64, ScalarUInt64, ScalarVector3UInt64Mask, ScalarUInt64Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
