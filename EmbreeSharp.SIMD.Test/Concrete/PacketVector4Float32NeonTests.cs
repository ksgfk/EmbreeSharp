using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector4Float32NeonTests : Vector4Float32Tests<PacketVector4Float32Neon, PacketFloat32Neon, PacketVector4Float32NeonMask, PacketFloat32NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.IsSupported;
    protected override uint AlignmentMask => 15u;
}
