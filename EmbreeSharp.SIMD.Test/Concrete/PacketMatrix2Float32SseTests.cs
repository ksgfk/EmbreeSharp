using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix2Float32SseTests : Matrix2Float32Tests<PacketMatrix2Float32Sse, PacketVector2Float32Sse, PacketFloat32Sse, PacketMatrix2Float32SseMask, PacketVector2Float32SseMask, PacketFloat32SseMask>
{
    protected override bool IsHardwareSupported => Sse41.IsSupported;
    protected override uint AlignmentMask => 15u;
}
