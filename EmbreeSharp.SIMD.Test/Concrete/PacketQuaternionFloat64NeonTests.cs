using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketQuaternionFloat64NeonTests : QuaternionFloat64Tests<PacketQuaternionFloat64Neon, PacketVector3Float64Neon, PacketVector4Float64Neon, PacketFloat64Neon, PacketQuaternionFloat64NeonMask, PacketVector3Float64NeonMask, PacketVector4Float64NeonMask, PacketFloat64NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.IsSupported;
    protected override uint AlignmentMask => 15u;
}
