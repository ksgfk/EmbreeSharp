using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector3UInt32SseTests : Vector3UInt32Tests<PacketVector3UInt32Sse, PacketUInt32Sse, PacketVector3UInt32SseMask, PacketUInt32SseMask>
{
    protected override bool IsHardwareSupported => Sse41.IsSupported;
    protected override uint AlignmentMask => 15u;
}
