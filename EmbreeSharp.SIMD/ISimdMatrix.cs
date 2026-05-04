using System.Numerics;

namespace EmbreeSharp.SIMD;

public interface ISimdMatrix<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask> :
    IEqualityOperators<TSelf, TSelf, TMatrixMask>,
    ISimdLane
    where TSelf : unmanaged, ISimdMatrix<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdVector<TVector, TPacket, TScalar, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdNumeric<TPacket, TScalar, TPacketMask>
    where TScalar : unmanaged, INumberBase<TScalar>
    where TMatrixMask : unmanaged, ISimdShapeMask<TMatrixMask, TVectorMask>, ISimdLane
    where TVectorMask : unmanaged, ISimdShapeMask<TVectorMask, TPacketMask>, ISimdLane
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, TScalar>
{
    static abstract TSelf Identity { get; }

    static abstract TSelf Broadcast(TScalar value);

    static abstract TSelf Select(TMatrixMask mask, TSelf ifTrue, TSelf ifFalse);

    static abstract TSelf Select(TVectorMask mask, TSelf ifTrue, TSelf ifFalse);

    static abstract TSelf Select(TPacketMask mask, TSelf ifTrue, TSelf ifFalse);

    static abstract TSelf Multiply(TSelf left, TSelf right);

    static abstract TSelf Multiply(TSelf matrix, TPacket scalar);

    static abstract TSelf Multiply(TPacket scalar, TSelf matrix);

    static abstract TSelf operator *(TSelf left, TSelf right);

    static abstract TVector operator *(TSelf matrix, TVector vector);

    static abstract TSelf operator *(TSelf matrix, TPacket scalar);

    static abstract TSelf operator *(TPacket scalar, TSelf matrix);
}

public interface ISimdFloatingPointMatrix<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask> :
    ISimdMatrix<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>,
    IDivisionOperators<TSelf, TPacket, TSelf>
    where TSelf : unmanaged, ISimdFloatingPointMatrix<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdFloatingPointVector<TVector, TPacket, TScalar, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdFloatingPoint<TPacket, TScalar, TPacketMask>
    where TScalar : unmanaged, IFloatingPointIeee754<TScalar>
    where TMatrixMask : unmanaged, ISimdShapeMask<TMatrixMask, TVectorMask>, ISimdLane
    where TVectorMask : unmanaged, ISimdShapeMask<TVectorMask, TPacketMask>, ISimdLane
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, TScalar>
{
    static abstract TSelf FusedMultiplyAdd(TSelf left, TSelf right, TSelf addend);

    static abstract TVector FusedMultiplyAdd(TSelf matrix, TVector vector, TVector addend);

    static abstract TVector Transform(TSelf matrix, TVector vector);

    static abstract TSelf Transpose(TSelf matrix);

    static abstract TSelf Scale(TVector scale);

    static abstract TSelf Divide(TSelf matrix, TPacket scalar);

    static abstract TSelf Inverse(TSelf matrix);

    static abstract TSelf InverseTranspose(TSelf matrix);
}

public interface ISimdIntegerMatrix<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask> :
    ISimdMatrix<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>
    where TSelf : unmanaged, ISimdIntegerMatrix<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdIntegerVector<TVector, TPacket, TScalar, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdInteger<TPacket, TScalar, TPacketMask>
    where TScalar : unmanaged, IBinaryInteger<TScalar>
    where TMatrixMask : unmanaged, ISimdShapeMask<TMatrixMask, TVectorMask>, ISimdLane
    where TVectorMask : unmanaged, ISimdShapeMask<TVectorMask, TPacketMask>, ISimdLane
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, TScalar>
{
}
