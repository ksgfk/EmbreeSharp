using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector2Float32SseTests : Vector2Float32Tests<PacketVector2Float32Sse, PacketFloat32Sse, PacketVector2Float32SseMask, PacketFloat32SseMask>
{
    protected override bool IsHardwareSupported => Sse41.IsSupported;
    protected override uint AlignmentMask => 15u;
}
