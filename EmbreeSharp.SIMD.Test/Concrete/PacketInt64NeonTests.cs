using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketInt64NeonTests : PacketInt64Tests<PacketInt64Neon, PacketInt64NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.Arm64.IsSupported;
    protected override uint AlignmentMask => 15u;
}
