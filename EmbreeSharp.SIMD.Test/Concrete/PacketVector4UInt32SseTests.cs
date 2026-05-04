using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector4UInt32SseTests : Vector4UInt32Tests<PacketVector4UInt32Sse, PacketUInt32Sse, PacketVector4UInt32SseMask, PacketUInt32SseMask>
{
    protected override bool IsHardwareSupported => Sse41.IsSupported;
    protected override uint AlignmentMask => 15u;
}
