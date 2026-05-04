using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarVector2Int32Tests : Vector2Int32Tests<ScalarVector2Int32, ScalarInt32, ScalarVector2Int32Mask, ScalarInt32Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
