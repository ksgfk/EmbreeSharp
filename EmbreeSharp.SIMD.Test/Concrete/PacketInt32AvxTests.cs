using System.Runtime.Intrinsics.X86;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketInt32AvxTests : PacketInt32Tests<PacketInt32Avx, PacketInt32AvxMask>
{
    protected override bool IsHardwareSupported => Avx2.IsSupported;
    protected override uint AlignmentMask => 31u;
}
