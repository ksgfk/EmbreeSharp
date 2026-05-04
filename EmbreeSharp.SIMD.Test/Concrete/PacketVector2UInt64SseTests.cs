using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector2UInt64SseTests : Vector2UInt64Tests<PacketVector2UInt64Sse, PacketUInt64Sse, PacketVector2UInt64SseMask, PacketUInt64SseMask>
{
    protected override bool IsHardwareSupported => Sse41.IsSupported;
    protected override uint AlignmentMask => 15u;
}
