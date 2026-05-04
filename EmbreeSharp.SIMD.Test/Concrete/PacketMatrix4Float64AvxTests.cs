using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix4Float64AvxTests : Matrix4Float64Tests<PacketMatrix4Float64Avx, PacketVector4Float64Avx, PacketFloat64Avx, PacketMatrix4Float64AvxMask, PacketVector4Float64AvxMask, PacketFloat64AvxMask>
{
    protected override bool IsHardwareSupported => Avx.IsSupported;
    protected override uint AlignmentMask => 31u;
}
