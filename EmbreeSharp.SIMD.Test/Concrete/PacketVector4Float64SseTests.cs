using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector4Float64SseTests : Vector4Float64Tests<PacketVector4Float64Sse, PacketFloat64Sse, PacketVector4Float64SseMask, PacketFloat64SseMask>
{
    protected override bool IsHardwareSupported => Sse41.IsSupported;
    protected override uint AlignmentMask => 15u;
}
