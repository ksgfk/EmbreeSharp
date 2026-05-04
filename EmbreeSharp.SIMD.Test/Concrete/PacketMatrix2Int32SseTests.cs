using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix2Int32SseTests : Matrix2Int32Tests<PacketMatrix2Int32Sse, PacketVector2Int32Sse, PacketInt32Sse, PacketMatrix2Int32SseMask, PacketVector2Int32SseMask, PacketInt32SseMask>
{
    protected override bool IsHardwareSupported => Sse41.IsSupported;
    protected override uint AlignmentMask => 15u;
}
