using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarVector3Int64Tests : Vector3Int64Tests<ScalarVector3Int64, ScalarInt64, ScalarVector3Int64Mask, ScalarInt64Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
