using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector3UInt32NeonTests : Vector3UInt32Tests<PacketVector3UInt32Neon, PacketUInt32Neon, PacketVector3UInt32NeonMask, PacketUInt32NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.IsSupported;
    protected override uint AlignmentMask => 15u;
}
