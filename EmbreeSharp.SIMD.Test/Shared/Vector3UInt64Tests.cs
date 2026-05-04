using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class Vector3UInt64Tests<TVector, TPacket, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdIntegerVector3<TVector, TPacket, ulong, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdInteger<TPacket, ulong, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector3Mask<TVectorMask, TPacketMask>
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
        ulong[] rightX = PadToMax([0ul, 1ul, 2ul, 3ul], n);
        ulong[] rightY = PadToMax([0ul, 1ul, 2ul, 3ul], n);
        ulong[] rightZ = PadToMax([1ul, 2ul, 3ul, 4ul], n);

        TVector left = TVector.Create(TPacket.Load(leftX), TPacket.Load(leftY), TPacket.Load(leftZ));
        TVector right = TVector.Create(TPacket.Load(rightX), TPacket.Load(rightY), TPacket.Load(rightZ));

        ulong[] addX = PadToMax([1ul, 3ul, 5ul, 7ul], n);
        ulong[] addY = PadToMax([2ul, 4ul, 6ul, 8ul], n);
        ulong[] addZ = PadToMax([1ul, 3ul, 5ul, 7ul], n);
        AssertVector(left + right, addX, addY, addZ);

        ulong[] subX = PadToMax([1ul, 1ul, 1ul, 1ul], n);
        ulong[] subY = PadToMax([2ul, 2ul, 2ul, 2ul], n);
        ulong[] subZ = PadToMax([ulong.MaxValue, ulong.MaxValue, ulong.MaxValue, ulong.MaxValue], n);
        AssertVector(left - right, subX, subY, subZ);

        AssertVector(left * right, PadToMax([0ul, 2ul, 6ul, 12ul], n), PadToMax([0ul, 3ul, 8ul, 15ul], n), PadToMax([0ul, 2ul, 6ul, 12ul], n));

        TVector broadcast = TVector.Broadcast(3ul);
        AssertVector(broadcast, PadToMax([3ul, 3ul, 3ul, 3ul], n), PadToMax([3ul, 3ul, 3ul, 3ul], n), PadToMax([3ul, 3ul, 3ul, 3ul], n));

        TPacketMask alternateLaneMask = TPacketMask.Load(PadToMaxBool([false, true, false, false], n));
        TVectorMask mask = TVectorMask.Create(alternateLaneMask, alternateLaneMask, alternateLaneMask);
        AssertVector(TVector.Select(mask, left, right), PadToMax([0ul, 2ul, 2ul, 3ul], n), PadToMax([0ul, 3ul, 2ul, 3ul], n), PadToMax([1ul, 1ul, 3ul, 4ul], n));

        var leftCopy = left;
        TVectorMask eqMask = left == leftCopy;
        bool[] allTrue = PadToMaxBool([true, true, true, true], n);
        AssertVectorMask(eqMask, allTrue, allTrue, allTrue);
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

    protected static void AssertVector(TVector actual, ulong[] expectedX, ulong[] expectedY, ulong[] expectedZ)
    {
        int n = TPacket.LaneCount;
        Span<ulong> xValues = stackalloc ulong[n];
        Span<ulong> yValues = stackalloc ulong[n];
        Span<ulong> zValues = stackalloc ulong[n];
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
        Span<ulong> xValues = stackalloc ulong[n];
        Span<ulong> yValues = stackalloc ulong[n];
        Span<ulong> zValues = stackalloc ulong[n];
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
