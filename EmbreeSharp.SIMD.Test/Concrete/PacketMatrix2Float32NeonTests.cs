using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix2Float32NeonTests : Matrix2Float32Tests<PacketMatrix2Float32Neon, PacketVector2Float32Neon, PacketFloat32Neon, PacketMatrix2Float32NeonMask, PacketVector2Float32NeonMask, PacketFloat32NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.IsSupported;
    protected override uint AlignmentMask => 15u;
}
