using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix4Int32SseTests : Matrix4Int32Tests<PacketMatrix4Int32Sse, PacketVector4Int32Sse, PacketInt32Sse, PacketMatrix4Int32SseMask, PacketVector4Int32SseMask, PacketInt32SseMask>
{
    protected override bool IsHardwareSupported => Sse41.IsSupported;
    protected override uint AlignmentMask => 15u;
}
