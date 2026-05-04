using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix4Float32AvxTests : Matrix4Float32Tests<PacketMatrix4Float32Avx, PacketVector4Float32Avx, PacketFloat32Avx, PacketMatrix4Float32AvxMask, PacketVector4Float32AvxMask, PacketFloat32AvxMask>
{
    protected override bool IsHardwareSupported => Avx.IsSupported;
    protected override uint AlignmentMask => 31u;
}
