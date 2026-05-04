using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector2UInt64AvxTests : Vector2UInt64Tests<PacketVector2UInt64Avx, PacketUInt64Avx, PacketVector2UInt64AvxMask, PacketUInt64AvxMask>
{
    protected override bool IsHardwareSupported => Avx2.IsSupported;
    protected override uint AlignmentMask => 31u;
}
