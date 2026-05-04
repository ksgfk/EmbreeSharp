using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector2Int64AvxTests : Vector2Int64Tests<PacketVector2Int64Avx, PacketInt64Avx, PacketVector2Int64AvxMask, PacketInt64AvxMask>
{
    protected override bool IsHardwareSupported => Avx2.IsSupported;
    protected override uint AlignmentMask => 31u;
}
