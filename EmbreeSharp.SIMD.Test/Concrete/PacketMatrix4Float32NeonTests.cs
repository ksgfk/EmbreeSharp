using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix4Float32NeonTests : Matrix4Float32Tests<PacketMatrix4Float32Neon, PacketVector4Float32Neon, PacketFloat32Neon, PacketMatrix4Float32NeonMask, PacketVector4Float32NeonMask, PacketFloat32NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.IsSupported;
    protected override uint AlignmentMask => 15u;
}
