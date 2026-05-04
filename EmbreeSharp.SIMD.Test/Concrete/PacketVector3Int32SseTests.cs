using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector3Int32SseTests : Vector3Int32Tests<PacketVector3Int32Sse, PacketInt32Sse, PacketVector3Int32SseMask, PacketInt32SseMask>
{
    protected override bool IsHardwareSupported => Sse41.IsSupported;
    protected override uint AlignmentMask => 15u;
}
