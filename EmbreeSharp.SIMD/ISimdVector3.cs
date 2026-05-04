using System.Numerics;

namespace EmbreeSharp.SIMD;

public interface ISimdVector3<TSelf, TPacket, TScalar, TVectorMask, TPacketMask> :
    ISimdVector<TSelf, TPacket, TScalar, TVectorMask, TPacketMask>
    where TSelf : unmanaged, ISimdVector3<TSelf, TPacket, TScalar, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdNumeric<TPacket, TScalar, TPacketMask>
    where TScalar : unmanaged, INumberBase<TScalar>
    where TVectorMask : unmanaged, ISimdVector3Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, TScalar>
{
    TPacket X { get; }

    TPacket Y { get; }

    TPacket Z { get; }

    static abstract TSelf Create(TPacket x, TPacket y, TPacket z);
}

public interface ISimdFloatingPointVector3<TSelf, TPacket, TScalar, TVectorMask, TPacketMask> :
    ISimdVector3<TSelf, TPacket, TScalar, TVectorMask, TPacketMask>,
    ISimdFloatingPointVector<TSelf, TPacket, TScalar, TVectorMask, TPacketMask>
    where TSelf : unmanaged, ISimdFloatingPointVector3<TSelf, TPacket, TScalar, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdFloatingPoint<TPacket, TScalar, TPacketMask>
    where TScalar : unmanaged, IFloatingPointIeee754<TScalar>
    where TVectorMask : unmanaged, ISimdVector3Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, TScalar>
{
    static abstract TSelf Cross(TSelf left, TSelf right);
}

public interface ISimdIntegerVector3<TSelf, TPacket, TScalar, TVectorMask, TPacketMask> :
    ISimdVector3<TSelf, TPacket, TScalar, TVectorMask, TPacketMask>,
    ISimdIntegerVector<TSelf, TPacket, TScalar, TVectorMask, TPacketMask>
    where TSelf : unmanaged, ISimdIntegerVector3<TSelf, TPacket, TScalar, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdInteger<TPacket, TScalar, TPacketMask>
    where TScalar : unmanaged, IBinaryInteger<TScalar>
    where TVectorMask : unmanaged, ISimdVector3Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, TScalar>
{
}

public interface ISimdVector3Mask<TSelf, TPacketMask> :
    ISimdShapeMask<TSelf, TPacketMask>,
    ISimdLane
    where TSelf : unmanaged, ISimdVector3Mask<TSelf, TPacketMask>
    where TPacketMask : unmanaged, ISimdShapeMask<TPacketMask, bool>, ISimdLane
{
    TPacketMask X { get; }

    TPacketMask Y { get; }

    TPacketMask Z { get; }

    static abstract TSelf Create(TPacketMask x, TPacketMask y, TPacketMask z);
}
