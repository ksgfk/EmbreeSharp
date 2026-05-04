using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix2Int32AvxTests : Matrix2Int32Tests<PacketMatrix2Int32Avx, PacketVector2Int32Avx, PacketInt32Avx, PacketMatrix2Int32AvxMask, PacketVector2Int32AvxMask, PacketInt32AvxMask>
{
    protected override bool IsHardwareSupported => Avx2.IsSupported;
    protected override uint AlignmentMask => 31u;
}
