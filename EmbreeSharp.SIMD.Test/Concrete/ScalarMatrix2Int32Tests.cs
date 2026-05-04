using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarMatrix2Int32Tests : Matrix2Int32Tests<ScalarMatrix2Int32, ScalarVector2Int32, ScalarInt32, ScalarMatrix2Int32Mask, ScalarVector2Int32Mask, ScalarInt32Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
