using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector2UInt32NeonTests : Vector2UInt32Tests<PacketVector2UInt32Neon, PacketUInt32Neon, PacketVector2UInt32NeonMask, PacketUInt32NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.IsSupported;
    protected override uint AlignmentMask => 15u;
}
