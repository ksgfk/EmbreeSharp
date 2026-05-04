using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix4Float32SseTests : Matrix4Float32Tests<PacketMatrix4Float32Sse, PacketVector4Float32Sse, PacketFloat32Sse, PacketMatrix4Float32SseMask, PacketVector4Float32SseMask, PacketFloat32SseMask>
{
    protected override bool IsHardwareSupported => Sse41.IsSupported;
    protected override uint AlignmentMask => 15u;
}
