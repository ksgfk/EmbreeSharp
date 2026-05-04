using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector2Int32AvxTests : Vector2Int32Tests<PacketVector2Int32Avx, PacketInt32Avx, PacketVector2Int32AvxMask, PacketInt32AvxMask>
{
    protected override bool IsHardwareSupported => Avx2.IsSupported;
    protected override uint AlignmentMask => 31u;
}
