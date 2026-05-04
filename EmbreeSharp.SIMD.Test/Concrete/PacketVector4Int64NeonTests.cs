using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector4Int64NeonTests : Vector4Int64Tests<PacketVector4Int64Neon, PacketInt64Neon, PacketVector4Int64NeonMask, PacketInt64NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.IsSupported;
    protected override uint AlignmentMask => 15u;
}
