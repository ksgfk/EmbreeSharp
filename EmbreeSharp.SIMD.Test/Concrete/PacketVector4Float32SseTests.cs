using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector4Float32SseTests : Vector4Float32Tests<PacketVector4Float32Sse, PacketFloat32Sse, PacketVector4Float32SseMask, PacketFloat32SseMask>
{
    protected override bool IsHardwareSupported => Sse41.IsSupported;
    protected override uint AlignmentMask => 15u;
}
