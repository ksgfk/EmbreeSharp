using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace EmbreeSharp.SIMD;

public readonly partial struct PacketFloat32Avx :
    ISimdFloatingPoint<PacketFloat32Avx, float, PacketFloat32AvxMask>
{
    internal readonly Vector256<float> _value;

    internal PacketFloat32Avx(Vector256<float> value)
    {
        _value = value;
    }

    public static int LaneCount => 8;

    public static PacketFloat32Avx AdditiveIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(0f);
    }

    public static PacketFloat32Avx MultiplicativeIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(1f);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Avx Broadcast(float value)
    {
        return new(Vector256.Create(value));
    }

    public static unsafe PacketFloat32Avx Load(ReadOnlySpan<float> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (float* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketFloat32Avx LoadAligned(ReadOnlySpan<float> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (float* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketFloat32Avx value, Span<float> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (float* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketFloat32Avx value, Span<float> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (float* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat32Avx LoadUnsafe(float* source)
    {
        return new(Avx.LoadVector256(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat32Avx LoadAlignedUnsafe(float* source)
    {
        return new(Avx.LoadAlignedVector256(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketFloat32Avx value, float* destination)
    {
        Avx.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketFloat32Avx value, float* destination)
    {
        Avx.StoreAligned(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Avx Select(PacketFloat32AvxMask mask, PacketFloat32Avx ifTrue, PacketFloat32Avx ifFalse)
    {
        return new(Avx.BlendVariable(ifFalse._value, ifTrue._value, mask._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Avx Abs(PacketFloat32Avx value)
    {
        return new(Avx.AndNot(Vector256.Create(-0f), value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Avx Min(PacketFloat32Avx left, PacketFloat32Avx right)
    {
        return new(Avx.Min(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Avx Max(PacketFloat32Avx left, PacketFloat32Avx right)
    {
        return new(Avx.Max(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Avx Clamp(PacketFloat32Avx value, PacketFloat32Avx min, PacketFloat32Avx max)
    {
        return Min(Max(value, min), max);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Avx Sqrt(PacketFloat32Avx value)
    {
        return new(Avx.Sqrt(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Avx Reciprocal(PacketFloat32Avx value)
    {
        Vector256<float> estimate = Avx.Reciprocal(value._value);
        Vector256<float> doubledEstimate = Avx.Add(estimate, estimate);
        Vector256<float> product = Avx.Multiply(estimate, value._value);

        Vector256<float> refined = Fma.IsSupported
            ? Fma.MultiplyAddNegated(product, estimate, doubledEstimate)
            : Avx.Subtract(doubledEstimate, Avx.Multiply(estimate, product));

        return new(Avx.BlendVariable(refined, estimate, product));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Avx ReciprocalSqrt(PacketFloat32Avx value)
    {
        Vector256<float> half = Vector256.Create(0.5f);
        Vector256<float> three = Vector256.Create(3f);
        Vector256<float> estimate = Avx.ReciprocalSqrt(value._value);
        Vector256<float> halfEstimate = Avx.Multiply(estimate, half);
        Vector256<float> product = Avx.Multiply(estimate, value._value);

        Vector256<float> correction = Fma.IsSupported
            ? Fma.MultiplyAddNegated(product, estimate, three)
            : Avx.Subtract(three, Avx.Multiply(product, estimate));

        Vector256<float> refined = Avx.Multiply(correction, halfEstimate);
        return new(Avx.BlendVariable(refined, estimate, product));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Avx Floor(PacketFloat32Avx value)
    {
        return new(Avx.Floor(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Avx Ceiling(PacketFloat32Avx value)
    {
        return new(Avx.Ceiling(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Avx Truncate(PacketFloat32Avx value)
    {
        return new(Avx.RoundToZero(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Avx Round(PacketFloat32Avx value)
    {
        return new(Avx.RoundToNearestInteger(value._value));
    }

    public static partial PacketFloat32Avx Sin(PacketFloat32Avx value);

    public static partial PacketFloat32Avx Cos(PacketFloat32Avx value);

    public static partial (PacketFloat32Avx Sin, PacketFloat32Avx Cos) SinCos(PacketFloat32Avx value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Avx FusedMultiplyAdd(PacketFloat32Avx left, PacketFloat32Avx right, PacketFloat32Avx addend)
    {
        return new(Fma.IsSupported
            ? Fma.MultiplyAdd(left._value, right._value, addend._value)
            : Avx.Add(Avx.Multiply(left._value, right._value), addend._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Avx CopySign(PacketFloat32Avx value, PacketFloat32Avx sign)
    {
        Vector256<float> signMask = Vector256.Create(-0f);
        Vector256<float> magnitude = Avx.AndNot(signMask, value._value);
        Vector256<float> signBits = Avx.And(signMask, sign._value);
        return new(Avx.Or(magnitude, signBits));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask IsFinite(PacketFloat32Avx value)
    {
        Vector256<float> magnitude = Avx.AndNot(Vector256.Create(-0f), value._value);
        return new(Avx.Compare(magnitude, Vector256.Create(float.PositiveInfinity), FloatComparisonMode.OrderedLessThanNonSignaling));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask IsInfinity(PacketFloat32Avx value)
    {
        Vector256<float> magnitude = Avx.AndNot(Vector256.Create(-0f), value._value);
        return new(Avx.Compare(magnitude, Vector256.Create(float.PositiveInfinity), FloatComparisonMode.OrderedEqualNonSignaling));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask IsNaN(PacketFloat32Avx value)
    {
        return new(Avx.Compare(value._value, value._value, FloatComparisonMode.UnorderedNonSignaling));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Avx operator +(PacketFloat32Avx value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Avx operator +(PacketFloat32Avx left, PacketFloat32Avx right)
    {
        return new(Avx.Add(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Avx operator -(PacketFloat32Avx left, PacketFloat32Avx right)
    {
        return new(Avx.Subtract(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Avx operator *(PacketFloat32Avx left, PacketFloat32Avx right)
    {
        return new(Avx.Multiply(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Avx operator /(PacketFloat32Avx left, PacketFloat32Avx right)
    {
        return new(Avx.Divide(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Avx operator -(PacketFloat32Avx value)
    {
        return new(Avx.Xor(value._value, Vector256.Create(-0f)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask operator ==(PacketFloat32Avx left, PacketFloat32Avx right)
    {
        return new(Avx.Compare(left._value, right._value, FloatComparisonMode.OrderedEqualNonSignaling));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask operator !=(PacketFloat32Avx left, PacketFloat32Avx right)
    {
        return new(Avx.Compare(left._value, right._value, FloatComparisonMode.UnorderedNotEqualNonSignaling));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask operator <(PacketFloat32Avx left, PacketFloat32Avx right)
    {
        return new(Avx.Compare(left._value, right._value, FloatComparisonMode.OrderedLessThanNonSignaling));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask operator >(PacketFloat32Avx left, PacketFloat32Avx right)
    {
        return new(Avx.Compare(left._value, right._value, FloatComparisonMode.OrderedGreaterThanNonSignaling));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask operator <=(PacketFloat32Avx left, PacketFloat32Avx right)
    {
        return new(Avx.Compare(left._value, right._value, FloatComparisonMode.OrderedLessThanOrEqualNonSignaling));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask operator >=(PacketFloat32Avx left, PacketFloat32Avx right)
    {
        return new(Avx.Compare(left._value, right._value, FloatComparisonMode.OrderedGreaterThanOrEqualNonSignaling));
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketFloat32Avx other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();

}

public readonly struct PacketFloat32AvxMask : ISimdPacketMask<PacketFloat32AvxMask, float>
{
    internal readonly Vector256<float> _value;

    internal PacketFloat32AvxMask(Vector256<float> value)
    {
        _value = value;
    }

    public static int LaneCount => 8;

    public static PacketFloat32AvxMask True
    {
        get
        {
            return new(Vector256.Create(-1).AsSingle());
        }
    }

    public static PacketFloat32AvxMask False
    {
        get
        {
            return new(Vector256<float>.Zero);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask Broadcast(bool value) => value ? True : False;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool All(PacketFloat32AvxMask value)
    {
        return Avx.MoveMask(value._value) == 0xFF;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Any(PacketFloat32AvxMask value)
    {
        return Avx.MoveMask(value._value) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool None(PacketFloat32AvxMask value)
    {
        return Avx.MoveMask(value._value) == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask AndNot(PacketFloat32AvxMask left, PacketFloat32AvxMask right)
    {
        return new(Avx.AndNot(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask Select(PacketFloat32AvxMask mask, PacketFloat32AvxMask ifTrue, PacketFloat32AvxMask ifFalse)
    {
        return new(Avx.BlendVariable(ifFalse._value, ifTrue._value, mask._value));
    }

    public static unsafe PacketFloat32AvxMask Load(ReadOnlySpan<float> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (float* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketFloat32AvxMask Load(ReadOnlySpan<bool> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (bool* ptr = values)
        {
            return LoadBoolUnsafe(ptr);
        }
    }

    public static unsafe PacketFloat32AvxMask LoadAligned(ReadOnlySpan<float> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (float* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketFloat32AvxMask value, Span<float> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (float* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void Store(PacketFloat32AvxMask value, Span<bool> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (bool* ptr = destination)
        {
            StoreBoolUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketFloat32AvxMask value, Span<float> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (float* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat32AvxMask LoadUnsafe(float* source)
    {
        return new(Avx.LoadVector256(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat32AvxMask LoadBoolUnsafe(bool* source)
    {
        long packed = Unsafe.ReadUnaligned<long>(source);
        Vector128<sbyte> byteValues = Vector128.CreateScalarUnsafe(packed).AsSByte();
        Vector128<sbyte> byteMask = Sse2.CompareGreaterThan(byteValues, Vector128<sbyte>.Zero);
        Vector128<int> lower = Sse41.ConvertToVector128Int32(byteMask);
        Vector128<sbyte> upperBytes = Sse2.ShiftRightLogical128BitLane(byteMask, 4);
        Vector128<int> upper = Sse41.ConvertToVector128Int32(upperBytes);

        return new(Avx.InsertVector128(lower.ToVector256(), upper, 1).AsSingle());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat32AvxMask LoadAlignedUnsafe(float* source)
    {
        return new(Avx.LoadAlignedVector256(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketFloat32AvxMask value, float* destination)
    {
        Avx.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreBoolUnsafe(PacketFloat32AvxMask value, bool* destination)
    {
        int mask = Avx.MoveMask(value._value);
        for (int i = 0; i < LaneCount; i++)
        {
            destination[i] = ((mask >> i) & 1) != 0;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketFloat32AvxMask value, float* destination)
    {
        Avx.StoreAligned(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask And(PacketFloat32AvxMask left, PacketFloat32AvxMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask Or(PacketFloat32AvxMask left, PacketFloat32AvxMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask Xor(PacketFloat32AvxMask left, PacketFloat32AvxMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask Not(PacketFloat32AvxMask value) => ~value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask operator &(PacketFloat32AvxMask left, PacketFloat32AvxMask right)
    {
        return new(Avx.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask operator |(PacketFloat32AvxMask left, PacketFloat32AvxMask right)
    {
        return new(Avx.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask operator ^(PacketFloat32AvxMask left, PacketFloat32AvxMask right)
    {
        return new(Avx.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask operator ~(PacketFloat32AvxMask value)
    {
        return new(Avx.Xor(value._value, Vector256.Create(-1).AsSingle()));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask operator ==(PacketFloat32AvxMask left, PacketFloat32AvxMask right)
    {
        return ~(left ^ right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask operator !=(PacketFloat32AvxMask left, PacketFloat32AvxMask right)
    {
        return left ^ right;
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketFloat32AvxMask other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}
