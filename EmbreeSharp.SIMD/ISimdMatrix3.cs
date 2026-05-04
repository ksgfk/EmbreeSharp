using System.Numerics;

namespace EmbreeSharp.SIMD;

public interface ISimdMatrix3<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask> :
    ISimdMatrix<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>
    where TSelf : unmanaged, ISimdMatrix3<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdVector3<TVector, TPacket, TScalar, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdNumeric<TPacket, TScalar, TPacketMask>
    where TScalar : unmanaged, INumberBase<TScalar>
    where TMatrixMask : unmanaged, ISimdMatrix3Mask<TMatrixMask, TVectorMask, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector3Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, TScalar>
{
    TVector Row0 { get; }

    TVector Row1 { get; }

    TVector Row2 { get; }

    static abstract TSelf Create(TVector row0, TVector row1, TVector row2);
}

public interface ISimdFloatingPointMatrix3<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask> :
    ISimdMatrix3<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>,
    ISimdFloatingPointMatrix<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>
    where TSelf : unmanaged, ISimdFloatingPointMatrix3<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdFloatingPointVector3<TVector, TPacket, TScalar, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdFloatingPoint<TPacket, TScalar, TPacketMask>
    where TScalar : unmanaged, IFloatingPointIeee754<TScalar>
    where TMatrixMask : unmanaged, ISimdMatrix3Mask<TMatrixMask, TVectorMask, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector3Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, TScalar>
{
    static abstract TSelf Translate(TVector translation);

    static abstract TSelf Translate(TPacket x, TPacket y);

    static abstract TSelf Rotate(TPacket angle);

    static abstract TSelf Scale(TPacket x, TPacket y);
}

public interface ISimdIntegerMatrix3<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask> :
    ISimdMatrix3<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>,
    ISimdIntegerMatrix<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>
    where TSelf : unmanaged, ISimdIntegerMatrix3<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdIntegerVector3<TVector, TPacket, TScalar, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdInteger<TPacket, TScalar, TPacketMask>
    where TScalar : unmanaged, IBinaryInteger<TScalar>
    where TMatrixMask : unmanaged, ISimdMatrix3Mask<TMatrixMask, TVectorMask, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector3Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, TScalar>
{
}

public interface ISimdMatrix3Mask<TSelf, TVectorMask, TRowReducedMask> :
    ISimdShapeMask<TSelf, TVectorMask>,
    ISimdLane
    where TSelf : unmanaged, ISimdMatrix3Mask<TSelf, TVectorMask, TRowReducedMask>
    where TVectorMask : unmanaged, ISimdShapeMask<TVectorMask, TRowReducedMask>, ISimdLane
{
    TVectorMask Row0 { get; }

    TVectorMask Row1 { get; }

    TVectorMask Row2 { get; }

    static abstract TSelf Create(TVectorMask row0, TVectorMask row1, TVectorMask row2);
}
