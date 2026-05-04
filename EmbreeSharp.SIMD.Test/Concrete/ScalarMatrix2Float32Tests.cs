using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class ScalarMatrix2Float32Tests : Matrix2Float32Tests<ScalarMatrix2Float32, ScalarVector2Float32, ScalarFloat32, ScalarMatrix2Float32Mask, ScalarVector2Float32Mask, ScalarFloat32Mask>
{
    protected override bool IsHardwareSupported => true;
    protected override uint AlignmentMask => 0u;
}
