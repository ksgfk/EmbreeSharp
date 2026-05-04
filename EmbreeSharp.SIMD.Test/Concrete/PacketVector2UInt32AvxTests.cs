using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector2UInt32AvxTests : Vector2UInt32Tests<PacketVector2UInt32Avx, PacketUInt32Avx, PacketVector2UInt32AvxMask, PacketUInt32AvxMask>
{
    protected override bool IsHardwareSupported => Avx2.IsSupported;
    protected override uint AlignmentMask => 31u;
}
