using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketUInt64SseTests : PacketUInt64Tests<PacketUInt64Sse, PacketUInt64SseMask>
{
    protected override bool IsHardwareSupported => Sse42.IsSupported;
    protected override uint AlignmentMask => 15u;
}
