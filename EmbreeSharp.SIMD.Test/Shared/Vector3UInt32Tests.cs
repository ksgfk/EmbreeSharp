using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class Vector3UInt32Tests<TVector, TPacket, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdIntegerVector3<TVector, TPacket, uint, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdInteger<TPacket, uint, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector3Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, uint>
{
    protected abstract bool IsHardwareSupported { get; }
    protected abstract uint AlignmentMask { get; }

    [TestMethod]
    public void TestArithmetic()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        uint[] leftX = PadToMax([1u, 2u, 3u, 4u, 5u, 6u, 7u, 8u], n);
        uint[] leftY = PadToMax([2u, 3u, 4u, 5u, 6u, 7u, 8u, 9u], n);
        uint[] leftZ = PadToMax([0u, 1u, 2u, 3u, 4u, 5u, 6u, 7u], n);
        uint[] rightX = PadToMax([0u, 1u, 2u, 3u, 4u, 5u, 6u, 7u], n);
        uint[] rightY = PadToMax([0u, 1u, 2u, 3u, 4u, 5u, 6u, 7u], n);
        uint[] rightZ = PadToMax([1u, 2u, 3u, 4u, 5u, 6u, 7u, 8u], n);

        TVector left = TVector.Create(TPacket.Load(leftX), TPacket.Load(leftY), TPacket.Load(leftZ));
        TVector right = TVector.Create(TPacket.Load(rightX), TPacket.Load(rightY), TPacket.Load(rightZ));

        uint[] addX = PadToMax([1u, 3u, 5u, 7u, 9u, 11u, 13u, 15u], n);
        uint[] addY = PadToMax([2u, 4u, 6u, 8u, 10u, 12u, 14u, 16u], n);
        uint[] addZ = PadToMax([1u, 3u, 5u, 7u, 9u, 11u, 13u, 15u], n);
        AssertVector(left + right, addX, addY, addZ);

        uint[] subX = PadToMax([1u, 1u, 1u, 1u, 1u, 1u, 1u, 1u], n);
        uint[] subY = PadToMax([2u, 2u, 2u, 2u, 2u, 2u, 2u, 2u], n);
        uint[] subZ = PadToMax([uint.MaxValue, uint.MaxValue, uint.MaxValue, uint.MaxValue, uint.MaxValue, uint.MaxValue, uint.MaxValue, uint.MaxValue], n);
        AssertVector(left - right, subX, subY, subZ);

        AssertVector(left * right, PadToMax([0u, 2u, 6u, 12u, 20u, 30u, 42u, 56u], n), PadToMax([0u, 3u, 8u, 15u, 24u, 35u, 48u, 63u], n), PadToMax([0u, 2u, 6u, 12u, 20u, 30u, 42u, 56u], n));

        TVector broadcast = TVector.Broadcast(3u);
        AssertVector(broadcast, PadToMax([3u, 3u, 3u, 3u, 3u, 3u, 3u, 3u], n), PadToMax([3u, 3u, 3u, 3u, 3u, 3u, 3u, 3u], n), PadToMax([3u, 3u, 3u, 3u, 3u, 3u, 3u, 3u], n));

        TPacketMask alternateLaneMask = TPacketMask.Load(PadToMaxBool([false, true, false, false, false, false, false, false], n));
        TVectorMask mask = TVectorMask.Create(alternateLaneMask, alternateLaneMask, alternateLaneMask);
        AssertVector(TVector.Select(mask, left, right), PadToMax([0u, 2u, 2u, 3u, 4u, 5u, 6u, 7u], n), PadToMax([0u, 3u, 2u, 3u, 4u, 5u, 6u, 7u], n), PadToMax([1u, 1u, 3u, 4u, 5u, 6u, 7u, 8u], n));

        var leftCopy = left;
        TVectorMask eqMask = left == leftCopy;
        bool[] allTrue = PadToMaxBool([true, true, true, true, true, true, true, true], n);
        AssertVectorMask(eqMask, allTrue, allTrue, allTrue);
    }

    protected static uint[] PadToMax(uint[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        uint[] result = new uint[count];
        values.CopyTo(result, 0);
        return result;
    }

    protected static bool[] PadToMaxBool(bool[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        bool[] result = new bool[count];
        values.CopyTo(result, 0);
        return result;
    }

    protected static void AssertVector(TVector actual, uint[] expectedX, uint[] expectedY, uint[] expectedZ)
    {
        int n = TPacket.LaneCount;
        Span<uint> xValues = stackalloc uint[n];
        Span<uint> yValues = stackalloc uint[n];
        Span<uint> zValues = stackalloc uint[n];
        TPacket.Store(actual.X, xValues);
        TPacket.Store(actual.Y, yValues);
        TPacket.Store(actual.Z, zValues);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], xValues[i], $"lane {i} X");
            Assert.AreEqual(expectedY[i], yValues[i], $"lane {i} Y");
            Assert.AreEqual(expectedZ[i], zValues[i], $"lane {i} Z");
        }
    }

    protected static void AssertVectorMask(TVectorMask actual, bool[] expectedX, bool[] expectedY, bool[] expectedZ)
    {
        int n = TPacket.LaneCount;
        Span<uint> xValues = stackalloc uint[n];
        Span<uint> yValues = stackalloc uint[n];
        Span<uint> zValues = stackalloc uint[n];
        TPacketMask.Store(actual.X, xValues);
        TPacketMask.Store(actual.Y, yValues);
        TPacketMask.Store(actual.Z, zValues);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], xValues[i] != 0, $"lane {i} X");
            Assert.AreEqual(expectedY[i], yValues[i] != 0, $"lane {i} Y");
            Assert.AreEqual(expectedZ[i], zValues[i] != 0, $"lane {i} Z");
        }
    }
}
