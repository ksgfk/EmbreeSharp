using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix3Int32NeonTests : Matrix3Int32Tests<PacketMatrix3Int32Neon, PacketVector3Int32Neon, PacketInt32Neon, PacketMatrix3Int32NeonMask, PacketVector3Int32NeonMask, PacketInt32NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.IsSupported;
    protected override uint AlignmentMask => 15u;
}
