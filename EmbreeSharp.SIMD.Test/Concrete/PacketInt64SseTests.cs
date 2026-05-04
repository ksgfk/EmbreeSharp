using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketInt64SseTests : PacketInt64Tests<PacketInt64Sse, PacketInt64SseMask>
{
    protected override bool IsHardwareSupported => Sse42.IsSupported;
    protected override uint AlignmentMask => 15u;
}
