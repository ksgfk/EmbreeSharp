using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix3Float32SseTests : Matrix3Float32Tests<PacketMatrix3Float32Sse, PacketVector3Float32Sse, PacketFloat32Sse, PacketMatrix3Float32SseMask, PacketVector3Float32SseMask, PacketFloat32SseMask>
{
    protected override bool IsHardwareSupported => Sse41.IsSupported;
    protected override uint AlignmentMask => 15u;
}
