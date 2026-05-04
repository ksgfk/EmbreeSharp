using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix3Int32AvxTests : Matrix3Int32Tests<PacketMatrix3Int32Avx, PacketVector3Int32Avx, PacketInt32Avx, PacketMatrix3Int32AvxMask, PacketVector3Int32AvxMask, PacketInt32AvxMask>
{
    protected override bool IsHardwareSupported => Avx2.IsSupported;
    protected override uint AlignmentMask => 31u;
}
