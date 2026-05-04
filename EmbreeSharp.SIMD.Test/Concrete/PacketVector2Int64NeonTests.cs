using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector2Int64NeonTests : Vector2Int64Tests<PacketVector2Int64Neon, PacketInt64Neon, PacketVector2Int64NeonMask, PacketInt64NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.IsSupported;
    protected override uint AlignmentMask => 15u;
}
