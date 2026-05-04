using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector2Int32NeonTests : Vector2Int32Tests<PacketVector2Int32Neon, PacketInt32Neon, PacketVector2Int32NeonMask, PacketInt32NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.IsSupported;
    protected override uint AlignmentMask => 15u;
}
