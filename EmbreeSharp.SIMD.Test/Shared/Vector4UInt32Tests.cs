using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class Vector4UInt32Tests<TVector, TPacket, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdIntegerVector4<TVector, TPacket, uint, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdInteger<TPacket, uint, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector4Mask<TVectorMask, TPacketMask>
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
        uint[] leftW = PadToMax([9u, 8u, 7u, 6u, 5u, 4u, 3u, 2u], n);
        uint[] rightX = PadToMax([2u, 2u, 2u, 2u, 2u, 2u, 2u, 2u], n);
        uint[] rightY = PadToMax([3u, 3u, 3u, 3u, 3u, 3u, 3u, 3u], n);
        uint[] rightZ = PadToMax([4u, 4u, 4u, 4u, 4u, 4u, 4u, 4u], n);
        uint[] rightW = PadToMax([5u, 5u, 5u, 5u, 5u, 5u, 5u, 5u], n);

        TVector left = TVector.Create(TPacket.Load(leftX), TPacket.Load(leftY), TPacket.Load(leftZ), TPacket.Load(leftW));
        TVector right = TVector.Create(TPacket.Load(rightX), TPacket.Load(rightY), TPacket.Load(rightZ), TPacket.Load(rightW));

        AssertVector(left + right, PadToMax([3u, 4u, 5u, 6u, 7u, 8u, 9u, 10u], n), PadToMax([5u, 6u, 7u, 8u, 9u, 10u, 11u, 12u], n), PadToMax([4u, 5u, 6u, 7u, 8u, 9u, 10u, 11u], n), PadToMax([14u, 13u, 12u, 11u, 10u, 9u, 8u, 7u], n));
        AssertVector(left - right, PadToMax([uint.MaxValue, 0u, 1u, 2u, 3u, 4u, 5u, 6u], n), PadToMax([uint.MaxValue, 0u, 1u, 2u, 3u, 4u, 5u, 6u], n), PadToMax([uint.MaxValue - 3u, uint.MaxValue - 2u, uint.MaxValue - 1u, uint.MaxValue, 0u, 1u, 2u, 3u], n), PadToMax([4u, 3u, 2u, 1u, 0u, uint.MaxValue, uint.MaxValue - 1u, uint.MaxValue - 2u], n));
        AssertVector(left * right, PadToMax([2u, 4u, 6u, 8u, 10u, 12u, 14u, 16u], n), PadToMax([6u, 9u, 12u, 15u, 18u, 21u, 24u, 27u], n), PadToMax([0u, 4u, 8u, 12u, 16u, 20u, 24u, 28u], n), PadToMax([45u, 40u, 35u, 30u, 25u, 20u, 15u, 10u], n));

        TVector broadcast = TVector.Broadcast(42u);
        uint[] fortyTwo = PadToMax([42u, 42u, 42u, 42u, 42u, 42u, 42u, 42u], n);
        AssertVector(broadcast, fortyTwo, fortyTwo, fortyTwo, fortyTwo);

        TPacketMask alternateLaneMask = TPacketMask.Load(PadToMaxBool([true, false, true, false, false, false, true, false], n));
        TVectorMask mask = TVectorMask.Create(alternateLaneMask, alternateLaneMask, alternateLaneMask, alternateLaneMask);
        AssertVector(TVector.Select(mask, left, right), PadToMax([1u, 2u, 3u, 2u, 2u, 2u, 7u, 2u], n), PadToMax([2u, 3u, 4u, 3u, 3u, 3u, 8u, 3u], n), PadToMax([0u, 4u, 2u, 4u, 4u, 4u, 6u, 4u], n), PadToMax([9u, 5u, 7u, 5u, 5u, 5u, 3u, 5u], n));

        var leftCopy = left;
        TVectorMask eqMask = left == leftCopy;
        bool[] allTrue = PadToMaxBool([true, true, true, true, true, true, true, true], n);
        AssertVectorMask(eqMask, allTrue, allTrue, allTrue, allTrue);
    }

    [TestMethod]
    public void TestBitwise()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        uint[] leftX = PadToMax([1u, 2u, 4u, 8u, 16u, 32u, 64u, 128u], n);
        uint[] leftY = PadToMax([3u, 6u, 12u, 24u, 48u, 96u, 192u, 384u], n);
        uint[] leftZ = PadToMax([1u, 3u, 7u, 15u, 31u, 63u, 127u, 255u], n);
        uint[] leftW = PadToMax([2u, 5u, 11u, 23u, 47u, 95u, 191u, 383u], n);
        uint[] rightX = PadToMax([3u, 3u, 12u, 12u, 48u, 48u, 192u, 192u], n);
        uint[] rightY = PadToMax([5u, 5u, 20u, 20u, 80u, 80u, 320u, 320u], n);
        uint[] rightZ = PadToMax([3u, 3u, 11u, 11u, 47u, 47u, 191u, 191u], n);
        uint[] rightW = PadToMax([6u, 6u, 22u, 22u, 94u, 94u, 382u, 382u], n);

        TPacket leftXPacket = TPacket.Load(leftX);
        TPacket leftYPacket = TPacket.Load(leftY);
        TPacket leftZPacket = TPacket.Load(leftZ);
        TPacket leftWPacket = TPacket.Load(leftW);
        TPacket rightXPacket = TPacket.Load(rightX);
        TPacket rightYPacket = TPacket.Load(rightY);
        TPacket rightZPacket = TPacket.Load(rightZ);
        TPacket rightWPacket = TPacket.Load(rightW);

        TVector andResult = TVector.Create(leftXPacket & rightXPacket, leftYPacket & rightYPacket, leftZPacket & rightZPacket, leftWPacket & rightWPacket);
        AssertVector(andResult, PadToMax([1u, 2u, 4u, 8u, 16u, 32u, 64u, 128u], n), PadToMax([1u, 4u, 4u, 16u, 16u, 64u, 64u, 256u], n), PadToMax([1u, 3u, 3u, 11u, 15u, 47u, 63u, 191u], n), PadToMax([2u, 4u, 2u, 22u, 14u, 94u, 62u, 382u], n));

        TVector orResult = TVector.Create(leftXPacket | rightXPacket, leftYPacket | rightYPacket, leftZPacket | rightZPacket, leftWPacket | rightWPacket);
        AssertVector(orResult, PadToMax([3u, 3u, 12u, 12u, 48u, 48u, 192u, 192u], n), PadToMax([7u, 7u, 28u, 28u, 112u, 112u, 448u, 448u], n), PadToMax([3u, 3u, 15u, 15u, 63u, 63u, 255u, 255u], n), PadToMax([6u, 7u, 31u, 23u, 127u, 95u, 511u, 383u], n));

        TVector xorResult = TVector.Create(leftXPacket ^ rightXPacket, leftYPacket ^ rightYPacket, leftZPacket ^ rightZPacket, leftWPacket ^ rightWPacket);
        AssertVector(xorResult, PadToMax([2u, 1u, 8u, 4u, 32u, 16u, 128u, 64u], n), PadToMax([6u, 3u, 24u, 12u, 96u, 48u, 384u, 192u], n), PadToMax([2u, 0u, 12u, 4u, 48u, 16u, 192u, 64u], n), PadToMax([4u, 3u, 29u, 1u, 113u, 1u, 449u, 1u], n));

        TVector notResult = TVector.Create(~leftXPacket, ~leftYPacket, ~leftZPacket, ~leftWPacket);
        AssertVector(notResult, PadToMax([uint.MaxValue - 1u, uint.MaxValue - 2u, uint.MaxValue - 4u, uint.MaxValue - 8u, uint.MaxValue - 16u, uint.MaxValue - 32u, uint.MaxValue - 64u, uint.MaxValue - 128u], n), PadToMax([uint.MaxValue - 3u, uint.MaxValue - 6u, uint.MaxValue - 12u, uint.MaxValue - 24u, uint.MaxValue - 48u, uint.MaxValue - 96u, uint.MaxValue - 192u, uint.MaxValue - 384u], n), PadToMax([uint.MaxValue - 1u, uint.MaxValue - 3u, uint.MaxValue - 7u, uint.MaxValue - 15u, uint.MaxValue - 31u, uint.MaxValue - 63u, uint.MaxValue - 127u, uint.MaxValue - 255u], n), PadToMax([uint.MaxValue - 2u, uint.MaxValue - 5u, uint.MaxValue - 11u, uint.MaxValue - 23u, uint.MaxValue - 47u, uint.MaxValue - 95u, uint.MaxValue - 191u, uint.MaxValue - 383u], n));
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

    protected static void AssertVector(TVector actual, uint[] expectedX, uint[] expectedY, uint[] expectedZ, uint[] expectedW)
    {
        int n = TPacket.LaneCount;
        Span<uint> xValues = stackalloc uint[n];
        Span<uint> yValues = stackalloc uint[n];
        Span<uint> zValues = stackalloc uint[n];
        Span<uint> wValues = stackalloc uint[n];
        TPacket.Store(actual.X, xValues);
        TPacket.Store(actual.Y, yValues);
        TPacket.Store(actual.Z, zValues);
        TPacket.Store(actual.W, wValues);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], xValues[i], $"lane {i} X");
            Assert.AreEqual(expectedY[i], yValues[i], $"lane {i} Y");
            Assert.AreEqual(expectedZ[i], zValues[i], $"lane {i} Z");
            Assert.AreEqual(expectedW[i], wValues[i], $"lane {i} W");
        }
    }

    protected static void AssertVectorMask(TVectorMask actual, bool[] expectedX, bool[] expectedY, bool[] expectedZ, bool[] expectedW)
    {
        int n = TPacket.LaneCount;
        Span<uint> xValues = stackalloc uint[n];
        Span<uint> yValues = stackalloc uint[n];
        Span<uint> zValues = stackalloc uint[n];
        Span<uint> wValues = stackalloc uint[n];
        TPacketMask.Store(actual.X, xValues);
        TPacketMask.Store(actual.Y, yValues);
        TPacketMask.Store(actual.Z, zValues);
        TPacketMask.Store(actual.W, wValues);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], xValues[i] != 0, $"lane {i} X");
            Assert.AreEqual(expectedY[i], yValues[i] != 0, $"lane {i} Y");
            Assert.AreEqual(expectedZ[i], zValues[i] != 0, $"lane {i} Z");
            Assert.AreEqual(expectedW[i], wValues[i] != 0, $"lane {i} W");
        }
    }
}
