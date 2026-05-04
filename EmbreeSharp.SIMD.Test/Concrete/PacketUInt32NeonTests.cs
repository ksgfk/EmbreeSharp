using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketUInt32NeonTests : PacketUInt32Tests<PacketUInt32Neon, PacketUInt32NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.IsSupported;
    protected override uint AlignmentMask => 15u;
}
