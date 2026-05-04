using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix3Float64SseTests : Matrix3Float64Tests<PacketMatrix3Float64Sse, PacketVector3Float64Sse, PacketFloat64Sse, PacketMatrix3Float64SseMask, PacketVector3Float64SseMask, PacketFloat64SseMask>
{
    protected override bool IsHardwareSupported => Sse41.IsSupported;
    protected override uint AlignmentMask => 15u;
}
