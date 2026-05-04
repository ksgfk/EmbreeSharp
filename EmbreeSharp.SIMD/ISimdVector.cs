using System.Numerics;

namespace EmbreeSharp.SIMD;

public interface ISimdVector<TSelf, TPacket, TScalar, TVectorMask, TPacketMask> :
    IUnaryPlusOperators<TSelf, TSelf>,
    IAdditionOperators<TSelf, TSelf, TSelf>,
    ISubtractionOperators<TSelf, TSelf, TSelf>,
    IMultiplyOperators<TSelf, TSelf, TSelf>,
    IUnaryNegationOperators<TSelf, TSelf>,
    IEqualityOperators<TSelf, TSelf, TVectorMask>,
    ISimdLane
    where TSelf : unmanaged, ISimdVector<TSelf, TPacket, TScalar, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdNumeric<TPacket, TScalar, TPacketMask>
    where TScalar : unmanaged, INumberBase<TScalar>
    where TVectorMask : unmanaged, ISimdShapeMask<TVectorMask, TPacketMask>, ISimdLane
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, TScalar>
{
    static abstract TSelf Broadcast(TScalar value);

    static abstract TSelf Select(TVectorMask mask, TSelf ifTrue, TSelf ifFalse);

    static abstract TSelf Select(TPacketMask mask, TSelf ifTrue, TSelf ifFalse);
}

public interface ISimdFloatingPointVector<TSelf, TPacket, TScalar, TVectorMask, TPacketMask> :
    ISimdVector<TSelf, TPacket, TScalar, TVectorMask, TPacketMask>
    where TSelf : unmanaged, ISimdFloatingPointVector<TSelf, TPacket, TScalar, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdFloatingPoint<TPacket, TScalar, TPacketMask>
    where TScalar : unmanaged, IFloatingPointIeee754<TScalar>
    where TVectorMask : unmanaged, ISimdShapeMask<TVectorMask, TPacketMask>, ISimdLane
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, TScalar>
{
    static abstract TPacket Dot(TSelf left, TSelf right);
}

public interface ISimdIntegerVector<TSelf, TPacket, TScalar, TVectorMask, TPacketMask> :
    ISimdVector<TSelf, TPacket, TScalar, TVectorMask, TPacketMask>
    where TSelf : unmanaged, ISimdIntegerVector<TSelf, TPacket, TScalar, TVectorMask, TPacketMask>
    where TPacket : unmanaged, ISimdInteger<TPacket, TScalar, TPacketMask>
    where TScalar : unmanaged, IBinaryInteger<TScalar>
    where TVectorMask : unmanaged, ISimdShapeMask<TVectorMask, TPacketMask>, ISimdLane
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, TScalar>
{
}
