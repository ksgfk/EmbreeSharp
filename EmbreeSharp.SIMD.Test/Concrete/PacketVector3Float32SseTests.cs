using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector3Float32SseTests : Vector3Float32Tests<PacketVector3Float32Sse, PacketFloat32Sse, PacketVector3Float32SseMask, PacketFloat32SseMask>
{
    protected override bool IsHardwareSupported => Sse41.IsSupported;
    protected override uint AlignmentMask => 15u;
}
