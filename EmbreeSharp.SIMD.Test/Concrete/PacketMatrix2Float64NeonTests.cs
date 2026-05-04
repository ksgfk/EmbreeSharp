using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix2Float64NeonTests : Matrix2Float64Tests<PacketMatrix2Float64Neon, PacketVector2Float64Neon, PacketFloat64Neon, PacketMatrix2Float64NeonMask, PacketVector2Float64NeonMask, PacketFloat64NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.IsSupported;
    protected override uint AlignmentMask => 15u;
}
