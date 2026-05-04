using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarMatrix3Int32Tests : Matrix3Int32Tests<ScalarMatrix3Int32, ScalarVector3Int32, ScalarInt32, ScalarMatrix3Int32Mask, ScalarVector3Int32Mask, ScalarInt32Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
