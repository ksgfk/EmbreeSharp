using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix3Float64AvxTests : Matrix3Float64Tests<PacketMatrix3Float64Avx, PacketVector3Float64Avx, PacketFloat64Avx, PacketMatrix3Float64AvxMask, PacketVector3Float64AvxMask, PacketFloat64AvxMask>
{
    protected override bool IsHardwareSupported => Avx.IsSupported;
    protected override uint AlignmentMask => 31u;
}
