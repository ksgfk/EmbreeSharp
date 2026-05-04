using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class Vector4UInt64Tests<TVector, TPacket, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdIntegerVector4<TVector, TPacket, ulong, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdInteger<TPacket, ulong, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector4Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, ulong>
{
    protected abstract bool IsHardwareSupported { get; }
    protected abstract uint AlignmentMask { get; }

    [TestMethod]
    public void TestArithmetic()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        ulong[] leftX = PadToMax([1ul, 2ul, 3ul, 4ul], n);
        ulong[] leftY = PadToMax([2ul, 3ul, 4ul, 5ul], n);
        ulong[] leftZ = PadToMax([0ul, 1ul, 2ul, 3ul], n);
        ulong[] leftW = PadToMax([9ul, 8ul, 7ul, 6ul], n);
        ulong[] rightX = PadToMax([2ul, 2ul, 2ul, 2ul], n);
        ulong[] rightY = PadToMax([3ul, 3ul, 3ul, 3ul], n);
        ulong[] rightZ = PadToMax([4ul, 4ul, 4ul, 4ul], n);
        ulong[] rightW = PadToMax([5ul, 5ul, 5ul, 5ul], n);

        TVector left = TVector.Create(TPacket.Load(leftX), TPacket.Load(leftY), TPacket.Load(leftZ), TPacket.Load(leftW));
        TVector right = TVector.Create(TPacket.Load(rightX), TPacket.Load(rightY), TPacket.Load(rightZ), TPacket.Load(rightW));

        AssertVector(left + right, PadToMax([3ul, 4ul, 5ul, 6ul], n), PadToMax([5ul, 6ul, 7ul, 8ul], n), PadToMax([4ul, 5ul, 6ul, 7ul], n), PadToMax([14ul, 13ul, 12ul, 11ul], n));
        AssertVector(left - right, PadToMax([ulong.MaxValue, 0ul, 1ul, 2ul], n), PadToMax([ulong.MaxValue, 0ul, 1ul, 2ul], n), PadToMax([ulong.MaxValue - 3ul, ulong.MaxValue - 2ul, ulong.MaxValue - 1ul, ulong.MaxValue], n), PadToMax([4ul, 3ul, 2ul, 1ul], n));
        AssertVector(left * right, PadToMax([2ul, 4ul, 6ul, 8ul], n), PadToMax([6ul, 9ul, 12ul, 15ul], n), PadToMax([0ul, 4ul, 8ul, 12ul], n), PadToMax([45ul, 40ul, 35ul, 30ul], n));

        TVector broadcast = TVector.Broadcast(42ul);
        ulong[] fortyTwo = PadToMax([42ul, 42ul, 42ul, 42ul], n);
        AssertVector(broadcast, fortyTwo, fortyTwo, fortyTwo, fortyTwo);

        TPacketMask alternateLaneMask = TPacketMask.Load(PadToMaxBool([true, false, true, false], n));
        TVectorMask mask = TVectorMask.Create(alternateLaneMask, alternateLaneMask, alternateLaneMask, alternateLaneMask);
        AssertVector(TVector.Select(mask, left, right), PadToMax([1ul, 2ul, 3ul, 2ul], n), PadToMax([2ul, 3ul, 4ul, 3ul], n), PadToMax([0ul, 4ul, 2ul, 4ul], n), PadToMax([9ul, 5ul, 7ul, 5ul], n));

        var leftCopy = left;
        TVectorMask eqMask = left == leftCopy;
        bool[] allTrue = PadToMaxBool([true, true, true, true], n);
        AssertVectorMask(eqMask, allTrue, allTrue, allTrue, allTrue);
    }

    [TestMethod]
    public void TestLargeMultiply()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        ulong[] leftX = PadToMax([1ul << 40, 3000000000ul, ulong.MaxValue, 0x8000000000000000ul], n);
        ulong[] leftY = PadToMax([1ul << 39, 2000000000ul, ulong.MaxValue - 1ul, 0x4000000000000000ul], n);
        ulong[] leftZ = PadToMax([1ul << 38, 1000000000ul, ulong.MaxValue - 2ul, 0x2000000000000000ul], n);
        ulong[] leftW = PadToMax([1ul << 41, 4000000000ul, ulong.MaxValue - 3ul, 0x1000000000000000ul], n);
        ulong[] rightX = PadToMax([3ul, 4ul, 2ul, 2ul], n);
        ulong[] rightY = PadToMax([4ul, 3ul, 2ul, 2ul], n);
        ulong[] rightZ = PadToMax([5ul, 2ul, 2ul, 2ul], n);
        ulong[] rightW = PadToMax([2ul, 5ul, 2ul, 2ul], n);

        TPacket leftXPacket = TPacket.Load(leftX);
        TPacket leftYPacket = TPacket.Load(leftY);
        TPacket leftZPacket = TPacket.Load(leftZ);
        TPacket leftWPacket = TPacket.Load(leftW);
        TPacket rightXPacket = TPacket.Load(rightX);
        TPacket rightYPacket = TPacket.Load(rightY);
        TPacket rightZPacket = TPacket.Load(rightZ);
        TPacket rightWPacket = TPacket.Load(rightW);

        TVector mulResult = TVector.Create(leftXPacket * rightXPacket, leftYPacket * rightYPacket, leftZPacket * rightZPacket, leftWPacket * rightWPacket);
        AssertVector(mulResult, PadToMax([3298534883328ul, 12000000000ul, ulong.MaxValue - 1ul, 0ul], n), PadToMax([2199023255552ul, 6000000000ul, ulong.MaxValue - 3ul, 0x8000000000000000ul], n), PadToMax([1374389534720ul, 2000000000ul, ulong.MaxValue - 5ul, 0x4000000000000000ul], n), PadToMax([4398046511104ul, 20000000000ul, ulong.MaxValue - 7ul, 0x2000000000000000ul], n));
    }

    [TestMethod]
    public void TestBitwise()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        ulong[] leftX = PadToMax([1ul, 2ul, 4ul, 8ul], n);
        ulong[] leftY = PadToMax([3ul, 6ul, 12ul, 24ul], n);
        ulong[] leftZ = PadToMax([1ul, 3ul, 7ul, 15ul], n);
        ulong[] leftW = PadToMax([2ul, 5ul, 11ul, 23ul], n);
        ulong[] rightX = PadToMax([3ul, 3ul, 12ul, 12ul], n);
        ulong[] rightY = PadToMax([5ul, 5ul, 20ul, 20ul], n);
        ulong[] rightZ = PadToMax([3ul, 3ul, 11ul, 11ul], n);
        ulong[] rightW = PadToMax([6ul, 6ul, 22ul, 22ul], n);

        TPacket leftXPacket = TPacket.Load(leftX);
        TPacket leftYPacket = TPacket.Load(leftY);
        TPacket leftZPacket = TPacket.Load(leftZ);
        TPacket leftWPacket = TPacket.Load(leftW);
        TPacket rightXPacket = TPacket.Load(rightX);
        TPacket rightYPacket = TPacket.Load(rightY);
        TPacket rightZPacket = TPacket.Load(rightZ);
        TPacket rightWPacket = TPacket.Load(rightW);

        TVector andResult = TVector.Create(leftXPacket & rightXPacket, leftYPacket & rightYPacket, leftZPacket & rightZPacket, leftWPacket & rightWPacket);
        AssertVector(andResult, PadToMax([1ul, 2ul, 4ul, 8ul], n), PadToMax([1ul, 4ul, 4ul, 16ul], n), PadToMax([1ul, 3ul, 3ul, 11ul], n), PadToMax([2ul, 4ul, 2ul, 22ul], n));

        TVector orResult = TVector.Create(leftXPacket | rightXPacket, leftYPacket | rightYPacket, leftZPacket | rightZPacket, leftWPacket | rightWPacket);
        AssertVector(orResult, PadToMax([3ul, 3ul, 12ul, 12ul], n), PadToMax([7ul, 7ul, 28ul, 28ul], n), PadToMax([3ul, 3ul, 15ul, 15ul], n), PadToMax([6ul, 7ul, 31ul, 23ul], n));

        TVector xorResult = TVector.Create(leftXPacket ^ rightXPacket, leftYPacket ^ rightYPacket, leftZPacket ^ rightZPacket, leftWPacket ^ rightWPacket);
        AssertVector(xorResult, PadToMax([2ul, 1ul, 8ul, 4ul], n), PadToMax([6ul, 3ul, 24ul, 12ul], n), PadToMax([2ul, 0ul, 12ul, 4ul], n), PadToMax([4ul, 3ul, 29ul, 1ul], n));

        TVector notResult = TVector.Create(~leftXPacket, ~leftYPacket, ~leftZPacket, ~leftWPacket);
        AssertVector(notResult, PadToMax([ulong.MaxValue - 1ul, ulong.MaxValue - 2ul, ulong.MaxValue - 4ul, ulong.MaxValue - 8ul], n), PadToMax([ulong.MaxValue - 3ul, ulong.MaxValue - 6ul, ulong.MaxValue - 12ul, ulong.MaxValue - 24ul], n), PadToMax([ulong.MaxValue - 1ul, ulong.MaxValue - 3ul, ulong.MaxValue - 7ul, ulong.MaxValue - 15ul], n), PadToMax([ulong.MaxValue - 2ul, ulong.MaxValue - 5ul, ulong.MaxValue - 11ul, ulong.MaxValue - 23ul], n));
    }

    protected static ulong[] PadToMax(ulong[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        ulong[] result = new ulong[count];
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

    protected static void AssertVector(TVector actual, ulong[] expectedX, ulong[] expectedY, ulong[] expectedZ, ulong[] expectedW)
    {
        int n = TPacket.LaneCount;
        Span<ulong> xValues = stackalloc ulong[n];
        Span<ulong> yValues = stackalloc ulong[n];
        Span<ulong> zValues = stackalloc ulong[n];
        Span<ulong> wValues = stackalloc ulong[n];
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
        Span<ulong> xValues = stackalloc ulong[n];
        Span<ulong> yValues = stackalloc ulong[n];
        Span<ulong> zValues = stackalloc ulong[n];
        Span<ulong> wValues = stackalloc ulong[n];
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
