using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector3Int64NeonTests : Vector3Int64Tests<PacketVector3Int64Neon, PacketInt64Neon, PacketVector3Int64NeonMask, PacketInt64NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.IsSupported;
    protected override uint AlignmentMask => 15u;
}
