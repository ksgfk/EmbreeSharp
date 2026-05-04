using EmbreeSharp.SIMD;

namespace EmbreeSharp.SIMD.Test;

public abstract class Matrix2Int32Tests<TMatrix, TVector, TPacket, TMatrixMask, TVectorMask, TPacketMask>
    where TMatrix : unmanaged, ISimdIntegerMatrix2<TMatrix, TVector, TPacket, int, TMatrixMask, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdIntegerVector2<TVector, TPacket, int, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdInteger<TPacket, int, TPacketMask>
    where TMatrixMask : unmanaged, ISimdMatrix2Mask<TMatrixMask, TVectorMask, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector2Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, int>
{
    protected abstract bool IsHardwareSupported { get; }
    protected abstract uint AlignmentMask { get; }

    [TestMethod]
    public void TestIdentity()
    {
        if (!IsHardwareSupported) { Assert.Inconclusive("Hardware not supported."); }

        int n = TPacket.LaneCount;
        TMatrix identity = TMatrix.Identity;
        int[] ones = PadToMax([1, 1, 1, 1, 1, 1, 1, 1], n);
        int[] zeros = PadToMax([0, 0, 0, 0, 0, 0, 0, 0], n);
        AssertMatrix(identity, ones, zeros, zeros, ones);
    }

    protected static int[] PadToMax(int[] values, int count)
    {
        if (values.Length >= count) return values[..count];
        int[] result = new int[count];
        values.CopyTo(result, 0);
        return result;
    }

    protected static void AssertMatrix(TMatrix actual, int[] expectedR0X, int[] expectedR0Y, int[] expectedR1X, int[] expectedR1Y)
    {
        int n = TPacket.LaneCount;
        Span<int> r0x = stackalloc int[n]; Span<int> r0y = stackalloc int[n];
        Span<int> r1x = stackalloc int[n]; Span<int> r1y = stackalloc int[n];
        TPacket.Store(actual.Row0.X, r0x); TPacket.Store(actual.Row0.Y, r0y);
        TPacket.Store(actual.Row1.X, r1x); TPacket.Store(actual.Row1.Y, r1y);
        for (int i = 0; i < expectedR0X.Length; i++)
        {
            Assert.AreEqual(expectedR0X[i], r0x[i], $"lane {i} R0X");
            Assert.AreEqual(expectedR0Y[i], r0y[i], $"lane {i} R0Y");
            Assert.AreEqual(expectedR1X[i], r1x[i], $"lane {i} R1X");
            Assert.AreEqual(expectedR1Y[i], r1y[i], $"lane {i} R1Y");
        }
    }

    protected static void AssertVector(TVector actual, int[] expectedX, int[] expectedY)
    {
        int n = TPacket.LaneCount;
        Span<int> xValues = stackalloc int[n];
        Span<int> yValues = stackalloc int[n];
        TPacket.Store(actual.X, xValues);
        TPacket.Store(actual.Y, yValues);
        for (int i = 0; i < expectedX.Length; i++)
        {
            Assert.AreEqual(expectedX[i], xValues[i], $"lane {i} X");
            Assert.AreEqual(expectedY[i], yValues[i], $"lane {i} Y");
        }
    }
}
