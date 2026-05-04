using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketVector4UInt64NeonTests : Vector4UInt64Tests<PacketVector4UInt64Neon, PacketUInt64Neon, PacketVector4UInt64NeonMask, PacketUInt64NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.IsSupported;
    protected override uint AlignmentMask => 15u;
}
