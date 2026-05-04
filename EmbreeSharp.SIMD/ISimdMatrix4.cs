using System.Numerics;

namespace EmbreeSharp.SIMD;

public interface ISimdMatrix4<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask> :
    ISimdMatrix<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>
    where TSelf : unmanaged, ISimdMatrix4<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdVector4<TVector, TPacket, TScalar, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdNumeric<TPacket, TScalar, TPacketMask>
    where TScalar : unmanaged, INumberBase<TScalar>
    where TMatrixMask : unmanaged, ISimdMatrix4Mask<TMatrixMask, TVectorMask, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector4Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, TScalar>
{
    TVector Row0 { get; }

    TVector Row1 { get; }

    TVector Row2 { get; }

    TVector Row3 { get; }

    static abstract TSelf Create(TVector row0, TVector row1, TVector row2, TVector row3);
}

public interface ISimdFloatingPointMatrix4<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask> :
    ISimdMatrix4<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>,
    ISimdFloatingPointMatrix<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>
    where TSelf : unmanaged, ISimdFloatingPointMatrix4<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdFloatingPointVector4<TVector, TPacket, TScalar, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdFloatingPoint<TPacket, TScalar, TPacketMask>
    where TScalar : unmanaged, IFloatingPointIeee754<TScalar>
    where TMatrixMask : unmanaged, ISimdMatrix4Mask<TMatrixMask, TVectorMask, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector4Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, TScalar>
{
    static abstract TSelf Translate(TVector translation);

    static abstract TSelf Translate(TPacket x, TPacket y, TPacket z);

    static abstract TSelf Rotate(TVector axis, TPacket angle);

    static abstract TSelf Scale(TPacket x, TPacket y, TPacket z);
}

public interface ISimdIntegerMatrix4<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask> :
    ISimdMatrix4<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>,
    ISimdIntegerMatrix<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>
    where TSelf : unmanaged, ISimdIntegerMatrix4<TSelf, TVector, TPacket, TScalar, TMatrixMask, TVectorMask, TPacketMask>
    where TVector : unmanaged, ISimdIntegerVector4<TVector, TPacket, TScalar, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdInteger<TPacket, TScalar, TPacketMask>
    where TScalar : unmanaged, IBinaryInteger<TScalar>
    where TMatrixMask : unmanaged, ISimdMatrix4Mask<TMatrixMask, TVectorMask, TPacketMask>
    where TVectorMask : unmanaged, ISimdVector4Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, TScalar>
{
}

public interface ISimdMatrix4Mask<TSelf, TVectorMask, TRowReducedMask> :
    ISimdShapeMask<TSelf, TVectorMask>,
    ISimdLane
    where TSelf : unmanaged, ISimdMatrix4Mask<TSelf, TVectorMask, TRowReducedMask>
    where TVectorMask : unmanaged, ISimdShapeMask<TVectorMask, TRowReducedMask>, ISimdLane
{
    TVectorMask Row0 { get; }

    TVectorMask Row1 { get; }

    TVectorMask Row2 { get; }

    TVectorMask Row3 { get; }

    static abstract TSelf Create(TVectorMask row0, TVectorMask row1, TVectorMask row2, TVectorMask row3);
}
