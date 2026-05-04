using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarVector3Int32Tests : Vector3Int32Tests<ScalarVector3Int32, ScalarInt32, ScalarVector3Int32Mask, ScalarInt32Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
