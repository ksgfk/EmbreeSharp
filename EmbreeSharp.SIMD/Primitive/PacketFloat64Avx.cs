using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace EmbreeSharp.SIMD;

public readonly partial struct PacketFloat64Avx :
    ISimdFloatingPoint<PacketFloat64Avx, double, PacketFloat64AvxMask>
{
    internal readonly Vector256<double> _value;

    internal PacketFloat64Avx(Vector256<double> value)
    {
        _value = value;
    }

    public static int LaneCount => 4;

    public static PacketFloat64Avx AdditiveIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(0d);
    }

    public static PacketFloat64Avx MultiplicativeIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(1d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Avx Broadcast(double value)
    {
        return new(Vector256.Create(value));
    }

    public static unsafe PacketFloat64Avx Load(ReadOnlySpan<double> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (double* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketFloat64Avx LoadAligned(ReadOnlySpan<double> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (double* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketFloat64Avx value, Span<double> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (double* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketFloat64Avx value, Span<double> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (double* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat64Avx LoadUnsafe(double* source)
    {
        return new(Avx.LoadVector256(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat64Avx LoadAlignedUnsafe(double* source)
    {
        return new(Avx.LoadAlignedVector256(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketFloat64Avx value, double* destination)
    {
        Avx.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketFloat64Avx value, double* destination)
    {
        Avx.StoreAligned(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Avx Select(PacketFloat64AvxMask mask, PacketFloat64Avx ifTrue, PacketFloat64Avx ifFalse)
    {
        return new(Avx.BlendVariable(ifFalse._value, ifTrue._value, mask._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Avx Abs(PacketFloat64Avx value)
    {
        return new(Avx.AndNot(Vector256.Create(-0d), value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Avx Min(PacketFloat64Avx left, PacketFloat64Avx right)
    {
        return new(Avx.Min(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Avx Max(PacketFloat64Avx left, PacketFloat64Avx right)
    {
        return new(Avx.Max(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Avx Clamp(PacketFloat64Avx value, PacketFloat64Avx min, PacketFloat64Avx max)
    {
        return Min(Max(value, min), max);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Avx Sqrt(PacketFloat64Avx value)
    {
        return new(Avx.Sqrt(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Avx Reciprocal(PacketFloat64Avx value)
    {
        return new(Avx.Divide(Vector256.Create(1d), value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Avx ReciprocalSqrt(PacketFloat64Avx value)
    {
        return new(Avx.Divide(Vector256.Create(1d), Avx.Sqrt(value._value)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Avx Floor(PacketFloat64Avx value)
    {
        return new(Avx.Floor(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Avx Ceiling(PacketFloat64Avx value)
    {
        return new(Avx.Ceiling(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Avx Truncate(PacketFloat64Avx value)
    {
        return new(Avx.RoundToZero(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Avx Round(PacketFloat64Avx value)
    {
        return new(Avx.RoundToNearestInteger(value._value));
    }

    public static partial PacketFloat64Avx Sin(PacketFloat64Avx value);

    public static partial PacketFloat64Avx Cos(PacketFloat64Avx value);

    public static partial (PacketFloat64Avx Sin, PacketFloat64Avx Cos) SinCos(PacketFloat64Avx value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Avx FusedMultiplyAdd(PacketFloat64Avx left, PacketFloat64Avx right, PacketFloat64Avx addend)
    {
        return new(Fma.IsSupported
            ? Fma.MultiplyAdd(left._value, right._value, addend._value)
            : Avx.Add(Avx.Multiply(left._value, right._value), addend._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Avx CopySign(PacketFloat64Avx value, PacketFloat64Avx sign)
    {
        Vector256<double> signMask = Vector256.Create(-0d);
        Vector256<double> magnitude = Avx.AndNot(signMask, value._value);
        Vector256<double> signBits = Avx.And(signMask, sign._value);
        return new(Avx.Or(magnitude, signBits));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask IsFinite(PacketFloat64Avx value)
    {
        Vector256<double> magnitude = Avx.AndNot(Vector256.Create(-0d), value._value);
        return new(Avx.Compare(magnitude, Vector256.Create(double.PositiveInfinity), FloatComparisonMode.OrderedLessThanNonSignaling));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask IsInfinity(PacketFloat64Avx value)
    {
        Vector256<double> magnitude = Avx.AndNot(Vector256.Create(-0d), value._value);
        return new(Avx.Compare(magnitude, Vector256.Create(double.PositiveInfinity), FloatComparisonMode.OrderedEqualNonSignaling));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask IsNaN(PacketFloat64Avx value)
    {
        return new(Avx.Compare(value._value, value._value, FloatComparisonMode.UnorderedNonSignaling));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Avx operator +(PacketFloat64Avx value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Avx operator +(PacketFloat64Avx left, PacketFloat64Avx right)
    {
        return new(Avx.Add(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Avx operator -(PacketFloat64Avx left, PacketFloat64Avx right)
    {
        return new(Avx.Subtract(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Avx operator *(PacketFloat64Avx left, PacketFloat64Avx right)
    {
        return new(Avx.Multiply(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Avx operator /(PacketFloat64Avx left, PacketFloat64Avx right)
    {
        return new(Avx.Divide(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Avx operator -(PacketFloat64Avx value)
    {
        return new(Avx.Xor(value._value, Vector256.Create(-0d)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask operator ==(PacketFloat64Avx left, PacketFloat64Avx right)
    {
        return new(Avx.Compare(left._value, right._value, FloatComparisonMode.OrderedEqualNonSignaling));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask operator !=(PacketFloat64Avx left, PacketFloat64Avx right)
    {
        return new(Avx.Compare(left._value, right._value, FloatComparisonMode.UnorderedNotEqualNonSignaling));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask operator <(PacketFloat64Avx left, PacketFloat64Avx right)
    {
        return new(Avx.Compare(left._value, right._value, FloatComparisonMode.OrderedLessThanNonSignaling));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask operator >(PacketFloat64Avx left, PacketFloat64Avx right)
    {
        return new(Avx.Compare(left._value, right._value, FloatComparisonMode.OrderedGreaterThanNonSignaling));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask operator <=(PacketFloat64Avx left, PacketFloat64Avx right)
    {
        return new(Avx.Compare(left._value, right._value, FloatComparisonMode.OrderedLessThanOrEqualNonSignaling));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask operator >=(PacketFloat64Avx left, PacketFloat64Avx right)
    {
        return new(Avx.Compare(left._value, right._value, FloatComparisonMode.OrderedGreaterThanOrEqualNonSignaling));
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketFloat64Avx other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}

public readonly struct PacketFloat64AvxMask : ISimdPacketMask<PacketFloat64AvxMask, double>
{
    internal readonly Vector256<double> _value;

    internal PacketFloat64AvxMask(Vector256<double> value)
    {
        _value = value;
    }

    public static int LaneCount => 4;

    public static PacketFloat64AvxMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new(Vector256.Create(-1L).AsDouble());
        }
    }

    public static PacketFloat64AvxMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            return new(Vector256<double>.Zero);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask Broadcast(bool value) => value ? True : False;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool All(PacketFloat64AvxMask value)
    {
        return Avx.MoveMask(value._value) == 0xF;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Any(PacketFloat64AvxMask value)
    {
        return Avx.MoveMask(value._value) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool None(PacketFloat64AvxMask value)
    {
        return Avx.MoveMask(value._value) == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask AndNot(PacketFloat64AvxMask left, PacketFloat64AvxMask right)
    {
        return new(Avx.AndNot(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask Select(PacketFloat64AvxMask mask, PacketFloat64AvxMask ifTrue, PacketFloat64AvxMask ifFalse)
    {
        return new(Avx.BlendVariable(ifFalse._value, ifTrue._value, mask._value));
    }

    public static unsafe PacketFloat64AvxMask Load(ReadOnlySpan<double> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (double* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketFloat64AvxMask Load(ReadOnlySpan<bool> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (bool* ptr = values)
        {
            return LoadBoolUnsafe(ptr);
        }
    }

    public static unsafe PacketFloat64AvxMask LoadAligned(ReadOnlySpan<double> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (double* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketFloat64AvxMask value, Span<double> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (double* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void Store(PacketFloat64AvxMask value, Span<bool> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (bool* ptr = destination)
        {
            StoreBoolUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketFloat64AvxMask value, Span<double> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (double* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat64AvxMask LoadUnsafe(double* source)
    {
        return new(Avx.LoadVector256(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat64AvxMask LoadBoolUnsafe(bool* source)
    {
        int packed = Unsafe.ReadUnaligned<int>(source);
        Vector128<sbyte> byteValues = Vector128.CreateScalarUnsafe(packed).AsSByte();
        Vector128<sbyte> byteMask = Sse2.CompareGreaterThan(byteValues, Vector128<sbyte>.Zero);
        Vector128<long> lower = Sse41.ConvertToVector128Int64(byteMask);
        Vector128<sbyte> upperBytes = Sse2.ShiftRightLogical128BitLane(byteMask, 2);
        Vector128<long> upper = Sse41.ConvertToVector128Int64(upperBytes);

        return new(Avx.InsertVector128(lower.ToVector256(), upper, 1).AsDouble());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat64AvxMask LoadAlignedUnsafe(double* source)
    {
        return new(Avx.LoadAlignedVector256(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketFloat64AvxMask value, double* destination)
    {
        Avx.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreBoolUnsafe(PacketFloat64AvxMask value, bool* destination)
    {
        int mask = Avx.MoveMask(value._value);
        for (int i = 0; i < LaneCount; i++)
        {
            destination[i] = ((mask >> i) & 1) != 0;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketFloat64AvxMask value, double* destination)
    {
        Avx.StoreAligned(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask And(PacketFloat64AvxMask left, PacketFloat64AvxMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask Or(PacketFloat64AvxMask left, PacketFloat64AvxMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask Xor(PacketFloat64AvxMask left, PacketFloat64AvxMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask Not(PacketFloat64AvxMask value) => ~value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask operator &(PacketFloat64AvxMask left, PacketFloat64AvxMask right)
    {
        return new(Avx.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask operator |(PacketFloat64AvxMask left, PacketFloat64AvxMask right)
    {
        return new(Avx.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask operator ^(PacketFloat64AvxMask left, PacketFloat64AvxMask right)
    {
        return new(Avx.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask operator ~(PacketFloat64AvxMask value)
    {
        return new(Avx.Xor(value._value, Vector256.Create(-1L).AsDouble()));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask operator ==(PacketFloat64AvxMask left, PacketFloat64AvxMask right)
    {
        return ~(left ^ right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask operator !=(PacketFloat64AvxMask left, PacketFloat64AvxMask right)
    {
        return left ^ right;
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketFloat64AvxMask other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}
