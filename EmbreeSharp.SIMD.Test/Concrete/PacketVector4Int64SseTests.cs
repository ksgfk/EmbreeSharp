using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector4Int64SseTests : Vector4Int64Tests<PacketVector4Int64Sse, PacketInt64Sse, PacketVector4Int64SseMask, PacketInt64SseMask>
{
    protected override bool IsHardwareSupported => Sse41.IsSupported;
    protected override uint AlignmentMask => 15u;
}
