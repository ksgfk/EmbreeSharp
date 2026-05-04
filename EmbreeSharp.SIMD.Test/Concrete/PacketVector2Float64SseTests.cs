using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector2Float64SseTests : Vector2Float64Tests<PacketVector2Float64Sse, PacketFloat64Sse, PacketVector2Float64SseMask, PacketFloat64SseMask>
{
    protected override bool IsHardwareSupported => Sse41.IsSupported;
    protected override uint AlignmentMask => 15u;
}
