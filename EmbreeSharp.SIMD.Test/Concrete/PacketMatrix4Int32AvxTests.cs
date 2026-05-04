using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix4Int32AvxTests : Matrix4Int32Tests<PacketMatrix4Int32Avx, PacketVector4Int32Avx, PacketInt32Avx, PacketMatrix4Int32AvxMask, PacketVector4Int32AvxMask, PacketInt32AvxMask>
{
    protected override bool IsHardwareSupported => Avx2.IsSupported;
    protected override uint AlignmentMask => 31u;
}
