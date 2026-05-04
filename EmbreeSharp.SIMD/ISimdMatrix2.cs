using System.Numerics;

namespace EmbreeSharp.SIMD;

public interface ISimdMatrix2<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask> :
    ISimdMatrix<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>
    where TSelf : unmanaged, ISimdMatrix2<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdVector2<TVector, TPacket, TScalar, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdNumeric<TPacket, TScalar, TPacketMask>
    where TScalar : unmanaged, INumberBase<TScalar>
    where TMatrixMask : unmanaged, ISimdMatrix2Mask<TMatrixMask, TVectorMask, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector2Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, TScalar>
{
    TVector Row0 { get; }

    TVector Row1 { get; }

    static abstract TSelf Create(TVector row0, TVector row1);
}

public interface ISimdFloatingPointMatrix2<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask> :
    ISimdMatrix2<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>,
    ISimdFloatingPointMatrix<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>
    where TSelf : unmanaged, ISimdFloatingPointMatrix2<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdFloatingPointVector2<TVector, TPacket, TScalar, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdFloatingPoint<TPacket, TScalar, TPacketMask>
    where TScalar : unmanaged, IFloatingPointIeee754<TScalar>
    where TMatrixMask : unmanaged, ISimdMatrix2Mask<TMatrixMask, TVectorMask, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector2Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, TScalar>
{
    static abstract TSelf Rotate(TPacket angle);

    static abstract TSelf Scale(TPacket x, TPacket y);
}

public interface ISimdIntegerMatrix2<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask> :
    ISimdMatrix2<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>,
    ISimdIntegerMatrix<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>
    where TSelf : unmanaged, ISimdIntegerMatrix2<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdIntegerVector2<TVector, TPacket, TScalar, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdInteger<TPacket, TScalar, TPacketMask>
    where TScalar : unmanaged, IBinaryInteger<TScalar>
    where TMatrixMask : unmanaged, ISimdMatrix2Mask<TMatrixMask, TVectorMask, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector2Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, TScalar>
{
}

public interface ISimdMatrix2Mask<TSelf, TVectorMask, TRowReducedMask> :
    ISimdShapeMask<TSelf, TVectorMask>,
    ISimdLane
    where TSelf : unmanaged, ISimdMatrix2Mask<TSelf, TVectorMask, TRowReducedMask>
    where TVectorMask : unmanaged, ISimdShapeMask<TVectorMask, TRowReducedMask>, ISimdLane
{
    TVectorMask Row0 { get; }

    TVectorMask Row1 { get; }

    static abstract TSelf Create(TVectorMask row0, TVectorMask row1);
}
