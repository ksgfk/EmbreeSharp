using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector3UInt64NeonTests : Vector3UInt64Tests<PacketVector3UInt64Neon, PacketUInt64Neon, PacketVector3UInt64NeonMask, PacketUInt64NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.IsSupported;
    protected override uint AlignmentMask => 15u;
}
