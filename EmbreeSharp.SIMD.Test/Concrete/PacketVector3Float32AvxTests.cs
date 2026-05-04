using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector3Float32AvxTests : Vector3Float32Tests<PacketVector3Float32Avx, PacketFloat32Avx, PacketVector3Float32AvxMask, PacketFloat32AvxMask>
{
    protected override bool IsHardwareSupported => Avx.IsSupported;
    protected override uint AlignmentMask => 31u;
}
