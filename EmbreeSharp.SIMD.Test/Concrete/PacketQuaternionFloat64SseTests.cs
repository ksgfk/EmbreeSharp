using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketQuaternionFloat64SseTests : QuaternionFloat64Tests<PacketQuaternionFloat64Sse, PacketVector3Float64Sse, PacketVector4Float64Sse, PacketFloat64Sse, PacketQuaternionFloat64SseMask, PacketVector3Float64SseMask, PacketVector4Float64SseMask, PacketFloat64SseMask>
{
    protected override bool IsHardwareSupported => Sse41.IsSupported;
    protected override uint AlignmentMask => 15u;
}
