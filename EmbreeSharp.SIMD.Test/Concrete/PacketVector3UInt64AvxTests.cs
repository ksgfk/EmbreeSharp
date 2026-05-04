using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector3UInt64AvxTests : Vector3UInt64Tests<PacketVector3UInt64Avx, PacketUInt64Avx, PacketVector3UInt64AvxMask, PacketUInt64AvxMask>
{
    protected override bool IsHardwareSupported => Avx2.IsSupported;
    protected override uint AlignmentMask => 31u;
}
