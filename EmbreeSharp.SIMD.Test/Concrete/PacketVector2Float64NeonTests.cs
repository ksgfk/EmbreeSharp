using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector2Float64NeonTests : Vector2Float64Tests<PacketVector2Float64Neon, PacketFloat64Neon, PacketVector2Float64NeonMask, PacketFloat64NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.IsSupported;
    protected override uint AlignmentMask => 15u;
}
