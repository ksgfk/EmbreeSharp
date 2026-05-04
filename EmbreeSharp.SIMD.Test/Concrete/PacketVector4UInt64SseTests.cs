using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector4UInt64SseTests : Vector4UInt64Tests<PacketVector4UInt64Sse, PacketUInt64Sse, PacketVector4UInt64SseMask, PacketUInt64SseMask>
{
    protected override bool IsHardwareSupported => Sse41.IsSupported;
    protected override uint AlignmentMask => 15u;
}
