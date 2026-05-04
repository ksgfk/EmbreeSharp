using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector4UInt32AvxTests : Vector4UInt32Tests<PacketVector4UInt32Avx, PacketUInt32Avx, PacketVector4UInt32AvxMask, PacketUInt32AvxMask>
{
    protected override bool IsHardwareSupported => Avx2.IsSupported;
    protected override uint AlignmentMask => 31u;
}
