using System.Numerics;

namespace EmbreeSharp.SIMD;

public interface ISimdShapeMask<TSelf, TReducedMask> :
    IBitwiseOperators<TSelf, TSelf, TSelf>,
    IEqualityOperators<TSelf, TSelf, TSelf>
    where TSelf : unmanaged, ISimdShapeMask<TSelf, TReducedMask>
{
    static abstract TSelf True { get; }

    static abstract TSelf False { get; }

    static abstract TSelf Broadcast(TReducedMask value);

    static abstract TReducedMask All(TSelf value);

    static abstract TReducedMask Any(TSelf value);

    static abstract TReducedMask None(TSelf value);

    static abstract TSelf And(TSelf left, TSelf right);

    static abstract TSelf Or(TSelf left, TSelf right);

    static abstract TSelf Xor(TSelf left, TSelf right);

    static abstract TSelf Not(TSelf value);

    static abstract TSelf AndNot(TSelf left, TSelf right);

    static abstract TSelf Select(TSelf mask, TSelf ifTrue, TSelf ifFalse);
}

public unsafe interface ISimdPacketMask<TSelf, TScalar> :
    ISimdShapeMask<TSelf, bool>,
    ISimdLane
    where TSelf : unmanaged, ISimdPacketMask<TSelf, TScalar>
    where TScalar : unmanaged
{
    static abstract TSelf Load(ReadOnlySpan<TScalar> values);

    static abstract TSelf Load(ReadOnlySpan<bool> values);

    static abstract TSelf LoadAligned(ReadOnlySpan<TScalar> values);

    static abstract void Store(TSelf value, Span<TScalar> destination);

    static abstract void Store(TSelf value, Span<bool> destination);

    static abstract void StoreAligned(TSelf value, Span<TScalar> destination);

    static abstract TSelf LoadUnsafe(TScalar* source);

    static abstract TSelf LoadBoolUnsafe(bool* source);

    static abstract TSelf LoadAlignedUnsafe(TScalar* source);

    static abstract void StoreUnsafe(TSelf value, TScalar* destination);

    static abstract void StoreBoolUnsafe(TSelf value, bool* destination);

    static abstract void StoreAlignedUnsafe(TSelf value, TScalar* destination);
}
