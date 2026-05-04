using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketUInt64NeonTests : PacketUInt64Tests<PacketUInt64Neon, PacketUInt64NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.Arm64.IsSupported;
    protected override uint AlignmentMask => 15u;
}
