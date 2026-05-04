using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix4Float64NeonTests : Matrix4Float64Tests<PacketMatrix4Float64Neon, PacketVector4Float64Neon, PacketFloat64Neon, PacketMatrix4Float64NeonMask, PacketVector4Float64NeonMask, PacketFloat64NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.IsSupported;
    protected override uint AlignmentMask => 15u;
}
