using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector3Int64AvxTests : Vector3Int64Tests<PacketVector3Int64Avx, PacketInt64Avx, PacketVector3Int64AvxMask, PacketInt64AvxMask>
{
    protected override bool IsHardwareSupported => Avx2.IsSupported;
    protected override uint AlignmentMask => 31u;
}
