using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector3UInt64SseTests : Vector3UInt64Tests<PacketVector3UInt64Sse, PacketUInt64Sse, PacketVector3UInt64SseMask, PacketUInt64SseMask>
{
    protected override bool IsHardwareSupported => Sse41.IsSupported;
    protected override uint AlignmentMask => 15u;
}
