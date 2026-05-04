using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketQuaternionFloat64AvxTests : QuaternionFloat64Tests<PacketQuaternionFloat64Avx, PacketVector3Float64Avx, PacketVector4Float64Avx, PacketFloat64Avx, PacketQuaternionFloat64AvxMask, PacketVector3Float64AvxMask, PacketVector4Float64AvxMask, PacketFloat64AvxMask>
{
    protected override bool IsHardwareSupported => Avx.IsSupported;
    protected override uint AlignmentMask => 31u;
}
