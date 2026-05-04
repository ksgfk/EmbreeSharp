using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector4UInt64AvxTests : Vector4UInt64Tests<PacketVector4UInt64Avx, PacketUInt64Avx, PacketVector4UInt64AvxMask, PacketUInt64AvxMask>
{
    protected override bool IsHardwareSupported => Avx2.IsSupported;
    protected override uint AlignmentMask => 31u;
}
