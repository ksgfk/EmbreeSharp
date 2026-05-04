using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector2UInt32SseTests : Vector2UInt32Tests<PacketVector2UInt32Sse, PacketUInt32Sse, PacketVector2UInt32SseMask, PacketUInt32SseMask>
{
    protected override bool IsHardwareSupported => Sse41.IsSupported;
    protected override uint AlignmentMask => 15u;
}
