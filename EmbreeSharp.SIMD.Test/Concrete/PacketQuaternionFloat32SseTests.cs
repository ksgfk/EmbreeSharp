using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketQuaternionFloat32SseTests : QuaternionFloat32Tests<PacketQuaternionFloat32Sse, PacketVector3Float32Sse, PacketVector4Float32Sse, PacketFloat32Sse, PacketQuaternionFloat32SseMask, PacketVector3Float32SseMask, PacketVector4Float32SseMask, PacketFloat32SseMask>
{
    protected override bool IsHardwareSupported => Sse41.IsSupported;
    protected override uint AlignmentMask => 15u;
}
