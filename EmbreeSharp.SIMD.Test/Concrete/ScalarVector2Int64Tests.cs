using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarVector2Int64Tests : Vector2Int64Tests<ScalarVector2Int64, ScalarInt64, ScalarVector2Int64Mask, ScalarInt64Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
