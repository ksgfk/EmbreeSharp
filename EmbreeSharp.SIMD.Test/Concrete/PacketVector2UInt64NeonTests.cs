using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector2UInt64NeonTests : Vector2UInt64Tests<PacketVector2UInt64Neon, PacketUInt64Neon, PacketVector2UInt64NeonMask, PacketUInt64NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.IsSupported;
    protected override uint AlignmentMask => 15u;
}
