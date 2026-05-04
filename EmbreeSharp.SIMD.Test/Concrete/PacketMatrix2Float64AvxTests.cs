using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix2Float64AvxTests : Matrix2Float64Tests<PacketMatrix2Float64Avx, PacketVector2Float64Avx, PacketFloat64Avx, PacketMatrix2Float64AvxMask, PacketVector2Float64AvxMask, PacketFloat64AvxMask>
{
    protected override bool IsHardwareSupported => Avx.IsSupported;
    protected override uint AlignmentMask => 31u;
}
