using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix4Int32NeonTests : Matrix4Int32Tests<PacketMatrix4Int32Neon, PacketVector4Int32Neon, PacketInt32Neon, PacketMatrix4Int32NeonMask, PacketVector4Int32NeonMask, PacketInt32NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.IsSupported;
    protected override uint AlignmentMask => 15u;
}
