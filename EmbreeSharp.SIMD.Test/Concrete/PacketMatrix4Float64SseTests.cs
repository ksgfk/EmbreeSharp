using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix4Float64SseTests : Matrix4Float64Tests<PacketMatrix4Float64Sse, PacketVector4Float64Sse, PacketFloat64Sse, PacketMatrix4Float64SseMask, PacketVector4Float64SseMask, PacketFloat64SseMask>
{
    protected override bool IsHardwareSupported => Sse41.IsSupported;
    protected override uint AlignmentMask => 15u;
}
