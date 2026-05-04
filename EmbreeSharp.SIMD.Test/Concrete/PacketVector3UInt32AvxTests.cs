using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector3UInt32AvxTests : Vector3UInt32Tests<PacketVector3UInt32Avx, PacketUInt32Avx, PacketVector3UInt32AvxMask, PacketUInt32AvxMask>
{
    protected override bool IsHardwareSupported => Avx2.IsSupported;
    protected override uint AlignmentMask => 31u;
}
