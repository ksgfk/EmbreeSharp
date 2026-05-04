using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector4Int32SseTests : Vector4Int32Tests<PacketVector4Int32Sse, PacketInt32Sse, PacketVector4Int32SseMask, PacketInt32SseMask>
{
    protected override bool IsHardwareSupported => Sse41.IsSupported;
    protected override uint AlignmentMask => 15u;
}
