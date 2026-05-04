using System.Numerics;

namespace EmbreeSharp.SIMD;

public interface ISimdVector2<TSelf, TPacket, TScalar, TVectorMask, TPacketMask> :
    ISimdVector<TSelf, TPacket, TScalar, TVectorMask, TPacketMask>
    where TSelf : unmanaged, ISimdVector2<TSelf, TPacket, TScalar, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdNumeric<TPacket, TScalar, TPacketMask>
    where TScalar : unmanaged, INumberBase<TScalar>
    where TVectorMask : unmanaged, ISimdVector2Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, TScalar>
{
    TPacket X { get; }

    TPacket Y { get; }

    static abstract TSelf Create(TPacket x, TPacket y);
}

public interface ISimdFloatingPointVector2<TSelf, TPacket, TScalar, TVectorMask, TPacketMask> :
    ISimdVector2<TSelf, TPacket, TScalar, TVectorMask, TPacketMask>,
    ISimdFloatingPointVector<TSelf, TPacket, TScalar, TVectorMask, TPacketMask>
    where TSelf : unmanaged, ISimdFloatingPointVector2<TSelf, TPacket, TScalar, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdFloatingPoint<TPacket, TScalar, TPacketMask>
    where TScalar : unmanaged, IFloatingPointIeee754<TScalar>
    where TVectorMask : unmanaged, ISimdVector2Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, TScalar>
{
}

public interface ISimdIntegerVector2<TSelf, TPacket, TScalar, TVectorMask, TPacketMask> :
    ISimdVector2<TSelf, TPacket, TScalar, TVectorMask, TPacketMask>,
    ISimdIntegerVector<TSelf, TPacket, TScalar, TVectorMask, TPacketMask>
    where TSelf : unmanaged, ISimdIntegerVector2<TSelf, TPacket, TScalar, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdInteger<TPacket, TScalar, TPacketMask>
    where TScalar : unmanaged, IBinaryInteger<TScalar>
    where TVectorMask : unmanaged, ISimdVector2Mask<TVectorMask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, TScalar>
{
}

public interface ISimdVector2Mask<TSelf, TPacketMask> :
    ISimdShapeMask<TSelf, TPacketMask>,
    ISimdLane
    where TSelf : unmanaged, ISimdVector2Mask<TSelf, TPacketMask>
    where TPacketMask : unmanaged, ISimdShapeMask<TPacketMask, bool>, ISimdLane
{
    TPacketMask X { get; }

    TPacketMask Y { get; }

    static abstract TSelf Create(TPacketMask x, TPacketMask y);
}
