using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector4Int64AvxTests : Vector4Int64Tests<PacketVector4Int64Avx, PacketInt64Avx, PacketVector4Int64AvxMask, PacketInt64AvxMask>
{
    protected override bool IsHardwareSupported => Avx2.IsSupported;
    protected override uint AlignmentMask => 31u;
}
