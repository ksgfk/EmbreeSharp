using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix3Float32NeonTests : Matrix3Float32Tests<PacketMatrix3Float32Neon, PacketVector3Float32Neon, PacketFloat32Neon, PacketMatrix3Float32NeonMask, PacketVector3Float32NeonMask, PacketFloat32NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.IsSupported;
    protected override uint AlignmentMask => 15u;
}
