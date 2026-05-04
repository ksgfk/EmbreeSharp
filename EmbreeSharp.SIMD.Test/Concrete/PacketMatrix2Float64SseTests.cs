using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix2Float64SseTests : Matrix2Float64Tests<PacketMatrix2Float64Sse, PacketVector2Float64Sse, PacketFloat64Sse, PacketMatrix2Float64SseMask, PacketVector2Float64SseMask, PacketFloat64SseMask>
{
    protected override bool IsHardwareSupported => Sse41.IsSupported;
    protected override uint AlignmentMask => 15u;
}
