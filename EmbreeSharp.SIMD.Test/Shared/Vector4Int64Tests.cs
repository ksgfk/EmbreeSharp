using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class Vector4Int64Tests<TVector, TPacket, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdIntegerVector4<TVector, TPacket, long, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdInteger<TPacket, long, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector4Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, long>
{
    protected abstract bool IsHardwareSupported { get; }
    protected abstract uint AlignmentMask { get; }

    [TestMethod]
    public void TestArithmetic()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        long[] leftX = PadToMax([1L, -2L, 3L, -4L], n);
        long[] leftY = PadToMax([2L, 3L, -4L, 5L], n);
        long[] leftZ = PadToMax([0L, -1L, 2L, -3L], n);
        long[] leftW = PadToMax([-1L, 0L, -2L, 3L], n);
        long[] rightX = PadToMax([2L, 2L, 2L, 2L], n);
        long[] rightY = PadToMax([3L, 3L, 3L, 3L], n);
        long[] rightZ = PadToMax([4L, 4L, 4L, 4L], n);
        long[] rightW = PadToMax([5L, 5L, 5L, 5L], n);

        TVector left = TVector.Create(TPacket.Load(leftX), TPacket.Load(leftY), TPacket.Load(leftZ), TPacket.Load(leftW));
        TVector right = TVector.Create(TPacket.Load(rightX), TPacket.Load(rightY), TPacket.Load(rightZ), TPacket.Load(rightW));

        AssertVector(left + right, PadToMax([3L, 0L, 5L, -2L], n), PadToMax([5L, 6L, -1L, 8L], n), PadToMax([4L, 3L, 6L, 1L], n), PadToMax([4L, 5L, 3L, 8L], n));
        AssertVector(left - right, PadToMax([-1L, -4L, 1L, -6L], n), PadToMax([-1L, 0L, -7L, 2L], n), PadToMax([-4L, -5L, -2L, -7L], n), PadToMax([-6L, -5L, -7L, -2L], n));
        AssertVector(left * right, PadToMax([2L, -4L, 6L, -8L], n), PadToMax([6L, 9L, -12L, 15L], n), PadToMax([0L, -4L, 8L, -12L], n), PadToMax([-5L, 0L, -10L, 15L], n));
        AssertVector(-left, PadToMax([-1L, 2L, -3L, 4L], n), PadToMax([-2L, -3L, 4L, -5L], n), PadToMax([0L, 1L, -2L, 3L], n), PadToMax([1L, 0L, 2L, -3L], n));

        TVector broadcast = TVector.Broadcast(42L);
        long[] fortyTwo = PadToMax([42L, 42L, 42L, 42L], n);
        AssertVector(broadcast, fortyTwo, fortyTwo, fortyTwo, fortyTwo);

        TPacketMask alternateLaneMask = TPacketMask.Load(PadToMaxBool([true, false, true, false], n));
        TVectorMask mask = TVectorMask.Create(alternateLaneMask, alternateLaneMask, alternateLaneMask, alternateLaneMask);
        AssertVector(TVector.Select(mask, left, right), PadToMax([1L, 2L, 3L, 2L], n), PadToMax([2L, 3L, -4L, 3L], n), PadToMax([0L, 4L, 2L, 4L], n), PadToMax([-1L, 5L, -2L, 5L], n));

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
        long[] leftX = PadToMax([1L << 40, -(1L << 40), 3000000000L, -3000000000L], n);
        long[] leftY = PadToMax([1L << 39, -(1L << 39), 2000000000L, -2000000000L], n);
        long[] leftZ = PadToMax([1L << 38, -(1L << 38), 1000000000L, -1000000000L], n);
        long[] leftW = PadToMax([1L << 41, -(1L << 41), 4000000000L, -4000000000L], n);
        long[] rightX = PadToMax([3L, 3L, 4L, -4L], n);
        long[] rightY = PadToMax([4L, 4L, 3L, -3L], n);
        long[] rightZ = PadToMax([5L, 5L, 2L, -2L], n);
        long[] rightW = PadToMax([2L, 2L, 5L, -5L], n);

        TPacket leftXPacket = TPacket.Load(leftX);
        TPacket leftYPacket = TPacket.Load(leftY);
        TPacket leftZPacket = TPacket.Load(leftZ);
        TPacket leftWPacket = TPacket.Load(leftW);
        TPacket rightXPacket = TPacket.Load(rightX);
        TPacket rightYPacket = TPacket.Load(rightY);
        TPacket rightZPacket = TPacket.Load(rightZ);
        TPacket rightWPacket = TPacket.Load(rightW);

        TVector mulResult = TVector.Create(leftXPacket * rightXPacket, leftYPacket * rightYPacket, leftZPacket * rightZPacket, leftWPacket * rightWPacket);
        AssertVector(mulResult, PadToMax([3298534883328L, -3298534883328L, 12000000000L, 12000000000L], n), PadToMax([2199023255552L, -2199023255552L, 6000000000L, 6000000000L], n), PadToMax([1374389534720L, -1374389534720L, 2000000000L, 2000000000L], n), PadToMax([4398046511104L, -4398046511104L, 20000000000L, 20000000000L], n));
    }

    [TestMethod]
    public void TestBitwise()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        long[] leftX = PadToMax([1L, 2L, 4L, 8L], n);
        long[] leftY = PadToMax([3L, 6L, 12L, 24L], n);
        long[] leftZ = PadToMax([1L, 3L, 7L, 15L], n);
        long[] leftW = PadToMax([2L, 5L, 11L, 23L], n);
        long[] rightX = PadToMax([3L, 3L, 12L, 12L], n);
        long[] rightY = PadToMax([5L, 5L, 20L, 20L], n);
        long[] rightZ = PadToMax([3L, 3L, 11L, 11L], n);
        long[] rightW = PadToMax([6L, 6L, 22L, 22L], n);

        TPacket leftXPacket = TPacket.Load(leftX);
        TPacket leftYPacket = TPacket.Load(leftY);
        TPacket leftZPacket = TPacket.Load(leftZ);
        TPacket leftWPacket = TPacket.Load(leftW);
        TPacket rightXPacket = TPacket.Load(rightX);
        TPacket rightYPacket = TPacket.Load(rightY);
        TPacket rightZPacket = TPacket.Load(rightZ);
        TPacket rightWPacket = TPacket.Load(rightW);

        TVector andResult = TVector.Create(leftXPacket & rightXPacket, leftYPacket & rightYPacket, leftZPacket & rightZPacket, leftWPacket & rightWPacket);
        AssertVector(andResult, PadToMax([1L, 2L, 4L, 8L], n), PadToMax([1L, 4L, 4L, 16L], n), PadToMax([1L, 3L, 3L, 11L], n), PadToMax([2L, 4L, 2L, 22L], n));

        TVector orResult = TVector.Create(leftXPacket | rightXPacket, leftYPacket | rightYPacket, leftZPacket | rightZPacket, leftWPacket | rightWPacket);
        AssertVector(orResult, PadToMax([3L, 3L, 12L, 12L], n), PadToMax([7L, 7L, 28L, 28L], n), PadToMax([3L, 3L, 15L, 15L], n), PadToMax([6L, 7L, 31L, 23L], n));

        TVector xorResult = TVector.Create(leftXPacket ^ rightXPacket, leftYPacket ^ rightYPacket, leftZPacket ^ rightZPacket, leftWPacket ^ rightWPacket);
        AssertVector(xorResult, PadToMax([2L, 1L, 8L, 4L], n), PadToMax([6L, 3L, 24L, 12L], n), PadToMax([2L, 0L, 12L, 4L], n), PadToMax([4L, 3L, 29L, 1L], n));

        TVector notResult = TVector.Create(~leftXPacket, ~leftYPacket, ~leftZPacket, ~leftWPacket);
        AssertVector(notResult, PadToMax([-2L, -3L, -5L, -9L], n), PadToMax([-4L, -7L, -13L, -25L], n), PadToMax([-2L, -4L, -8L, -16L], n), PadToMax([-3L, -6L, -12L, -24L], n));
    }

    protected static long[] PadToMax(long[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        long[] result = new long[count];
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

    protected static void AssertVector(TVector actual, long[] expectedX, long[] expectedY, long[] expectedZ, long[] expectedW)
    {
        int n = TPacket.LaneCount;
        Span<long> xValues = stackalloc long[n];
        Span<long> yValues = stackalloc long[n];
        Span<long> zValues = stackalloc long[n];
        Span<long> wValues = stackalloc long[n];
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
        Span<long> xValues = stackalloc long[n];
        Span<long> yValues = stackalloc long[n];
        Span<long> zValues = stackalloc long[n];
        Span<long> wValues = stackalloc long[n];
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
