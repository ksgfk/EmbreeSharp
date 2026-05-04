using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector3Int64SseTests : Vector3Int64Tests<PacketVector3Int64Sse, PacketInt64Sse, PacketVector3Int64SseMask, PacketInt64SseMask>
{
    protected override bool IsHardwareSupported => Sse41.IsSupported;
    protected override uint AlignmentMask => 15u;
}
