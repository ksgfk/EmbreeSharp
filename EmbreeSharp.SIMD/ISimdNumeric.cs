using System.Numerics;

namespace EmbreeSharp.SIMD;

public unsafe interface ISimdNumeric<TSelf, TScalar, TMask> :
    IAdditiveIdentity<TSelf, TSelf>,
    IMultiplicativeIdentity<TSelf, TSelf>,
    IUnaryPlusOperators<TSelf, TSelf>,
    IAdditionOperators<TSelf, TSelf, TSelf>,
    ISubtractionOperators<TSelf, TSelf, TSelf>,
    IMultiplyOperators<TSelf, TSelf, TSelf>,
    IUnaryNegationOperators<TSelf, TSelf>,
    IEqualityOperators<TSelf, TSelf, TMask>,
    IComparisonOperators<TSelf, TSelf, TMask>,
    ISimdLane
    where TSelf : unmanaged, ISimdNumeric<TSelf, TScalar, TMask>
    where TScalar : unmanaged, INumberBase<TScalar>
    where TMask : unmanaged, ISimdPacketMask<TMask, TScalar>
{
    static abstract TSelf Broadcast(TScalar value);

    static abstract TSelf Load(ReadOnlySpan<TScalar> values);

    static abstract TSelf LoadAligned(ReadOnlySpan<TScalar> values);

    static abstract void Store(TSelf value, Span<TScalar> destination);

    static abstract void StoreAligned(TSelf value, Span<TScalar> destination);

    static abstract TSelf LoadUnsafe(TScalar* source);

    static abstract TSelf LoadAlignedUnsafe(TScalar* source);

    static abstract void StoreUnsafe(TSelf value, TScalar* destination);

    static abstract void StoreAlignedUnsafe(TSelf value, TScalar* destination);

    static abstract TSelf Select(TMask mask, TSelf ifTrue, TSelf ifFalse);

    static abstract TSelf Abs(TSelf value);

    static abstract TSelf Min(TSelf left, TSelf right);

    static abstract TSelf Max(TSelf left, TSelf right);

    static abstract TSelf Clamp(TSelf value, TSelf min, TSelf max);
}

public interface ISimdFloatingPoint<TSelf, TScalar, TMask> :
    ISimdNumeric<TSelf, TScalar, TMask>,
    IDivisionOperators<TSelf, TSelf, TSelf>
    where TSelf : unmanaged, ISimdFloatingPoint<TSelf, TScalar, TMask>
    where TScalar : unmanaged, IFloatingPointIeee754<TScalar>
    where TMask : unmanaged, ISimdPacketMask<TMask, TScalar>
{
    static abstract TSelf Sqrt(TSelf value);

    static abstract TSelf Reciprocal(TSelf value);

    static abstract TSelf ReciprocalSqrt(TSelf value);

    static abstract TSelf Floor(TSelf value);

    static abstract TSelf Ceiling(TSelf value);

    static abstract TSelf Truncate(TSelf value);

    static abstract TSelf Round(TSelf value);

    static abstract TSelf Sin(TSelf value);

    static abstract TSelf Cos(TSelf value);

    static abstract (TSelf Sin, TSelf Cos) SinCos(TSelf value);

    static abstract TSelf FusedMultiplyAdd(TSelf left, TSelf right, TSelf addend);

    static abstract TSelf CopySign(TSelf value, TSelf sign);

    static abstract TMask IsFinite(TSelf value);

    static abstract TMask IsInfinity(TSelf value);

    static abstract TMask IsNaN(TSelf value);
}

public interface ISimdInteger<TSelf, TScalar, TMask> :
    ISimdNumeric<TSelf, TScalar, TMask>,
    IBitwiseOperators<TSelf, TSelf, TSelf>
    where TSelf : unmanaged, ISimdInteger<TSelf, TScalar, TMask>
    where TScalar : unmanaged, IBinaryInteger<TScalar>
    where TMask : unmanaged, ISimdPacketMask<TMask, TScalar>
{
}
