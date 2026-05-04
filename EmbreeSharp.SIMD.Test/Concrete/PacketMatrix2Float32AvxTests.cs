using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix2Float32AvxTests : Matrix2Float32Tests<PacketMatrix2Float32Avx, PacketVector2Float32Avx, PacketFloat32Avx, PacketMatrix2Float32AvxMask, PacketVector2Float32AvxMask, PacketFloat32AvxMask>
{
    protected override bool IsHardwareSupported => Avx.IsSupported;
    protected override uint AlignmentMask => 31u;
}
