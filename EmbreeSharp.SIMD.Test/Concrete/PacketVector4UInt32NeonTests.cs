using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector4UInt32NeonTests : Vector4UInt32Tests<PacketVector4UInt32Neon, PacketUInt32Neon, PacketVector4UInt32NeonMask, PacketUInt32NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.IsSupported;
    protected override uint AlignmentMask => 15u;
}
