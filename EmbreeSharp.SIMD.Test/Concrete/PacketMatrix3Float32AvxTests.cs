using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix3Float32AvxTests : Matrix3Float32Tests<PacketMatrix3Float32Avx, PacketVector3Float32Avx, PacketFloat32Avx, PacketMatrix3Float32AvxMask, PacketVector3Float32AvxMask, PacketFloat32AvxMask>
{
    protected override bool IsHardwareSupported => Avx.IsSupported;
    protected override uint AlignmentMask => 31u;
}
