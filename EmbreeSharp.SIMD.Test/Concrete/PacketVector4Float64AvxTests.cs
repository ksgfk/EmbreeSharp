using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector4Float64AvxTests : Vector4Float64Tests<PacketVector4Float64Avx, PacketFloat64Avx, PacketVector4Float64AvxMask, PacketFloat64AvxMask>
{
    protected override bool IsHardwareSupported => Avx.IsSupported;
    protected override uint AlignmentMask => 31u;
}
