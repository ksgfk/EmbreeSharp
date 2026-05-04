using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix3Int32SseTests : Matrix3Int32Tests<PacketMatrix3Int32Sse, PacketVector3Int32Sse, PacketInt32Sse, PacketMatrix3Int32SseMask, PacketVector3Int32SseMask, PacketInt32SseMask>
{
    protected override bool IsHardwareSupported => Sse41.IsSupported;
    protected override uint AlignmentMask => 15u;
}
