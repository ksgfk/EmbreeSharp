using System.Runtime.Intrinsics.Arm;
using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

[TestClass]
public sealed class PacketMatrix2Int32NeonTests : Matrix2Int32Tests<PacketMatrix2Int32Neon, PacketVector2Int32Neon, PacketInt32Neon, PacketMatrix2Int32NeonMask, PacketVector2Int32NeonMask, PacketInt32NeonMask>
{
    protected override bool IsHardwareSupported => AdvSimd.IsSupported;
    protected override uint AlignmentMask => 15u;
}
