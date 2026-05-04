using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketUInt32AvxTests : PacketUInt32Tests<PacketUInt32Avx, PacketUInt32AvxMask>
{
    protected override bool IsHardwareSupported => Avx2.IsSupported;
    protected override uint AlignmentMask => 31u;
}
