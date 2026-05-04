using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix3Float64NeonTests : Matrix3Float64Tests<PacketMatrix3Float64Neon, PacketVector3Float64Neon, PacketFloat64Neon, PacketMatrix3Float64NeonMask, PacketVector3Float64NeonMask, PacketFloat64NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.IsSupported;
    protected override uint AlignmentMask => 15u;
}
