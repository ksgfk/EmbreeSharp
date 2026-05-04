using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketQuaternionFloat32NeonTests : QuaternionFloat32Tests<PacketQuaternionFloat32Neon, PacketVector3Float32Neon, PacketVector4Float32Neon, PacketFloat32Neon, PacketQuaternionFloat32NeonMask, PacketVector3Float32NeonMask, PacketVector4Float32NeonMask, PacketFloat32NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.IsSupported;
    protected override uint AlignmentMask => 15u;
}
