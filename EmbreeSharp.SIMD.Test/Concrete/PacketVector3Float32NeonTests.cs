using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector3Float32NeonTests : Vector3Float32Tests<PacketVector3Float32Neon, PacketFloat32Neon, PacketVector3Float32NeonMask, PacketFloat32NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.IsSupported;
    protected override uint AlignmentMask => 15u;
}
