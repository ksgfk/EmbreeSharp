using System.Numerics;

namespace EmbreeSharp.SIMD;

public interface ISimdQuaternion<TSelf, TVector3, TVector4, TPacket, TScalar, TQuaternionMask, TVector3Mask, TVector4Mask, TPacketMask> :
    IUnaryPlusOperators<TSelf, TSelf>,
    IAdditionOperators<TSelf, TSelf, TSelf>,
    ISubtractionOperators<TSelf, TSelf, TSelf>,
    IMultiplyOperators<TSelf, TSelf, TSelf>,
    IDivisionOperators<TSelf, TSelf, TSelf>,
    IUnaryNegationOperators<TSelf, TSelf>,
    IEqualityOperators<TSelf, TSelf, TQuaternionMask>,
    ISimdLane
    where TSelf : unmanaged, ISimdQuaternion<TSelf, TVector3, TVector4, TPacket, TScalar, TQuaternionMask, TVector3Mask, TVector4Mask, TPacketMask>
    where TVector3 : unmanaged, ISimdFloatingPointVector3<TVector3, TPacket, TScalar, TVector3Mask, TPacketMask>
    where TVector4 : unmanaged, ISimdFloatingPointVector4<TVector4, TPacket, TScalar, TVector4Mask, TPacketMask>
    where TPacket : unmanaged, ISimdFloatingPoint<TPacket, TScalar, TPacketMask>
    where TScalar : unmanaged, IFloatingPointIeee754<TScalar>
    where TQuaternionMask : unmanaged, ISimdQuaternionMask<TQuaternionMask, TPacketMask>
    where TVector3Mask : unmanaged, ISimdVector3Mask<TVector3Mask, TPacketMask>
    where TVector4Mask : unmanaged, ISimdVector4Mask<TVector4Mask, TPacketMask>
    where TPacketMask : unmanaged, ISimdPacketMask<TPacketMask, TScalar>
{
    TPacket X { get; }

    TPacket Y { get; }

    TPacket Z { get; }

    TPacket W { get; }

    TVector3 Imag { get; }

    TPacket Real { get; }

    TVector4 Vector { get; }

    static abstract TSelf Identity { get; }

    static abstract TSelf Create(TPacket x, TPacket y, TPacket z, TPacket w);

    static abstract TSelf Create(TVector3 imag, TPacket real);

    static abstract TSelf Create(TVector4 vector);

    static abstract TSelf Broadcast(TScalar value);

    static abstract TSelf Select(TQuaternionMask mask, TSelf ifTrue, TSelf ifFalse);

    static abstract TSelf Select(TVector4Mask mask, TSelf ifTrue, TSelf ifFalse);

    static abstract TSelf Select(TPacketMask mask, TSelf ifTrue, TSelf ifFalse);

    static abstract TSelf Conjugate(TSelf value);

    static abstract TPacket Dot(TSelf left, TSelf right);

    static abstract TPacket SquaredNorm(TSelf value);

    static abstract TPacket Norm(TSelf value);

    static abstract TSelf Normalize(TSelf value);

    static abstract TSelf Reciprocal(TSelf value);

    static abstract TSelf Multiply(TSelf left, TSelf right);

    static abstract TSelf Multiply(TSelf quaternion, TPacket scalar);

    static abstract TSelf Multiply(TPacket scalar, TSelf quaternion);

    static abstract TSelf Divide(TSelf left, TSelf right);

    static abstract TSelf Divide(TSelf quaternion, TPacket scalar);

    static abstract TSelf FusedMultiplyAdd(TSelf left, TSelf right, TSelf addend);

    static abstract TSelf Rotate(TVector3 axis, TPacket angle);

    static abstract TVector3 Apply(TSelf quaternion, TVector3 vector);

    static abstract TSelf operator *(TSelf quaternion, TPacket scalar);

    static abstract TSelf operator *(TPacket scalar, TSelf quaternion);

    static abstract TSelf operator /(TSelf quaternion, TPacket scalar);
}

public interface ISimdQuaternionMask<TSelf, TPacketMask> :
    ISimdShapeMask<TSelf, TPacketMask>,
    ISimdLane
    where TSelf : unmanaged, ISimdQuaternionMask<TSelf, TPacketMask>
    where TPacketMask : unmanaged, ISimdShapeMask<TPacketMask, bool>, ISimdLane
{
    TPacketMask X { get; }

    TPacketMask Y { get; }

    TPacketMask Z { get; }

    TPacketMask W { get; }

    static abstract TSelf Create(TPacketMask x, TPacketMask y, TPacketMask z, TPacketMask w);
}
