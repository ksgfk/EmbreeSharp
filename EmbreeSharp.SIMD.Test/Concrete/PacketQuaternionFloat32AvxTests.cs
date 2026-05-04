using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketQuaternionFloat32AvxTests : QuaternionFloat32Tests<PacketQuaternionFloat32Avx, PacketVector3Float32Avx, PacketVector4Float32Avx, PacketFloat32Avx, PacketQuaternionFloat32AvxMask, PacketVector3Float32AvxMask, PacketVector4Float32AvxMask, PacketFloat32AvxMask>
{
    protected override bool IsHardwareSupported => Avx.IsSupported;
    protected override uint AlignmentMask => 31u;
}
