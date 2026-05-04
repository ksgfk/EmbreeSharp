using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;

namespace EmbreeSharp.SIMD;

public readonly struct PacketFloat64Neon :
    ISimdFloatingPoint<PacketFloat64Neon, double, PacketFloat64NeonMask>
{
    internal readonly Vector128<double> _value;

    internal PacketFloat64Neon(Vector128<double> value)
    {
        _value = value;
    }

    public static int LaneCount => 2;

    public static PacketFloat64Neon AdditiveIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(0d);
    }

    public static PacketFloat64Neon MultiplicativeIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(1d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Neon Broadcast(double value)
    {
        return new(Vector128.Create(value));
    }

    public static unsafe PacketFloat64Neon Load(ReadOnlySpan<double> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (double* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketFloat64Neon LoadAligned(ReadOnlySpan<double> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (double* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketFloat64Neon value, Span<double> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (double* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketFloat64Neon value, Span<double> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (double* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat64Neon LoadUnsafe(double* source)
    {
        return new(AdvSimd.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat64Neon LoadAlignedUnsafe(double* source)
    {
        return new(AdvSimd.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketFloat64Neon value, double* destination)
    {
        AdvSimd.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketFloat64Neon value, double* destination)
    {
        AdvSimd.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Neon Select(PacketFloat64NeonMask mask, PacketFloat64Neon ifTrue, PacketFloat64Neon ifFalse)
    {
        return new(AdvSimd.BitwiseSelect(mask._value, ifTrue._value, ifFalse._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Neon Abs(PacketFloat64Neon value)
    {
        return new(AdvSimd.Arm64.Abs(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Neon Min(PacketFloat64Neon left, PacketFloat64Neon right)
    {
        return new(AdvSimd.Arm64.Min(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Neon Max(PacketFloat64Neon left, PacketFloat64Neon right)
    {
        return new(AdvSimd.Arm64.Max(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Neon Clamp(PacketFloat64Neon value, PacketFloat64Neon min, PacketFloat64Neon max)
    {
        return Min(Max(value, min), max);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Neon Sqrt(PacketFloat64Neon value)
    {
        return new(AdvSimd.Arm64.Sqrt(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Neon Reciprocal(PacketFloat64Neon value)
    {
        Vector128<double> estimate = AdvSimd.Arm64.ReciprocalEstimate(value._value);
        estimate = AdvSimd.Arm64.Multiply(estimate, AdvSimd.Arm64.ReciprocalStep(estimate, value._value));
        estimate = AdvSimd.Arm64.Multiply(estimate, AdvSimd.Arm64.ReciprocalStep(estimate, value._value));
        estimate = AdvSimd.Arm64.Multiply(estimate, AdvSimd.Arm64.ReciprocalStep(estimate, value._value));
        return new(estimate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Neon ReciprocalSqrt(PacketFloat64Neon value)
    {
        Vector128<double> estimate = AdvSimd.Arm64.ReciprocalSquareRootEstimate(value._value);
        estimate = AdvSimd.Arm64.Multiply(estimate, AdvSimd.Arm64.ReciprocalSquareRootStep(AdvSimd.Arm64.Multiply(estimate, estimate), value._value));
        estimate = AdvSimd.Arm64.Multiply(estimate, AdvSimd.Arm64.ReciprocalSquareRootStep(AdvSimd.Arm64.Multiply(estimate, estimate), value._value));
        estimate = AdvSimd.Arm64.Multiply(estimate, AdvSimd.Arm64.ReciprocalSquareRootStep(AdvSimd.Arm64.Multiply(estimate, estimate), value._value));
        return new(estimate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Neon Floor(PacketFloat64Neon value)
    {
        return new(AdvSimd.Arm64.RoundToNegativeInfinity(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Neon Ceiling(PacketFloat64Neon value)
    {
        return new(AdvSimd.Arm64.RoundToPositiveInfinity(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Neon Truncate(PacketFloat64Neon value)
    {
        return new(AdvSimd.Arm64.RoundToZero(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Neon Round(PacketFloat64Neon value)
    {
        return new(AdvSimd.Arm64.RoundToNearest(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Neon Sin(PacketFloat64Neon value) => SimdFloatingPointMath.SinDouble<PacketFloat64Neon, PacketFloat64NeonMask>(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Neon Cos(PacketFloat64Neon value) => SimdFloatingPointMath.CosDouble<PacketFloat64Neon, PacketFloat64NeonMask>(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (PacketFloat64Neon Sin, PacketFloat64Neon Cos) SinCos(PacketFloat64Neon value) => SimdFloatingPointMath.SinCosDouble<PacketFloat64Neon, PacketFloat64NeonMask>(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Neon FusedMultiplyAdd(PacketFloat64Neon left, PacketFloat64Neon right, PacketFloat64Neon addend)
    {
        return new(AdvSimd.Arm64.FusedMultiplyAdd(addend._value, left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Neon CopySign(PacketFloat64Neon value, PacketFloat64Neon sign)
    {
        Vector128<double> signMask = Vector128.Create(-0d);
        Vector128<double> magnitude = AdvSimd.BitwiseClear(value._value, signMask);
        Vector128<double> signBits = AdvSimd.And(sign._value, signMask);
        return new(AdvSimd.Or(magnitude, signBits));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask IsFinite(PacketFloat64Neon value)
    {
        Vector128<double> magnitude = AdvSimd.Arm64.Abs(value._value);
        return new(AdvSimd.Arm64.CompareLessThan(magnitude, Vector128.Create(double.PositiveInfinity)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask IsInfinity(PacketFloat64Neon value)
    {
        Vector128<double> magnitude = AdvSimd.Arm64.Abs(value._value);
        return new(AdvSimd.Arm64.CompareEqual(magnitude, Vector128.Create(double.PositiveInfinity)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask IsNaN(PacketFloat64Neon value)
    {
        return new(~AdvSimd.Arm64.CompareEqual(value._value, value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Neon operator +(PacketFloat64Neon value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Neon operator +(PacketFloat64Neon left, PacketFloat64Neon right)
    {
        return new(AdvSimd.Arm64.Add(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Neon operator -(PacketFloat64Neon left, PacketFloat64Neon right)
    {
        return new(AdvSimd.Arm64.Subtract(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Neon operator *(PacketFloat64Neon left, PacketFloat64Neon right)
    {
        return new(AdvSimd.Arm64.Multiply(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Neon operator /(PacketFloat64Neon left, PacketFloat64Neon right)
    {
        return new(AdvSimd.Arm64.Divide(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Neon operator -(PacketFloat64Neon value)
    {
        return new(AdvSimd.Xor(value._value, Vector128.Create(-0d)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask operator ==(PacketFloat64Neon left, PacketFloat64Neon right)
    {
        return new(AdvSimd.Arm64.CompareEqual(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask operator !=(PacketFloat64Neon left, PacketFloat64Neon right)
    {
        return ~(left == right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask operator <(PacketFloat64Neon left, PacketFloat64Neon right)
    {
        return new(AdvSimd.Arm64.CompareLessThan(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask operator >(PacketFloat64Neon left, PacketFloat64Neon right)
    {
        return new(AdvSimd.Arm64.CompareGreaterThan(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask operator <=(PacketFloat64Neon left, PacketFloat64Neon right)
    {
        return new(AdvSimd.Arm64.CompareLessThanOrEqual(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask operator >=(PacketFloat64Neon left, PacketFloat64Neon right)
    {
        return new(AdvSimd.Arm64.CompareGreaterThanOrEqual(left._value, right._value));
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketFloat64Neon other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}

public readonly struct PacketFloat64NeonMask : ISimdPacketMask<PacketFloat64NeonMask, double>
{
    internal readonly Vector128<double> _value;

    internal PacketFloat64NeonMask(Vector128<double> value)
    {
        _value = value;
    }

    public static int LaneCount => 2;

    public static PacketFloat64NeonMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector128.Create(BitConverter.Int64BitsToDouble(-1)));
    }

    public static PacketFloat64NeonMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector128<double>.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask Broadcast(bool value) => value ? True : False;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool All(PacketFloat64NeonMask value)
    {
        return Vector128.ExtractMostSignificantBits(value._value) == 0x3;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Any(PacketFloat64NeonMask value)
    {
        return Vector128.ExtractMostSignificantBits(value._value) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool None(PacketFloat64NeonMask value)
    {
        return Vector128.ExtractMostSignificantBits(value._value) == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask AndNot(PacketFloat64NeonMask left, PacketFloat64NeonMask right)
    {
        return new(AdvSimd.BitwiseClear(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask Select(PacketFloat64NeonMask mask, PacketFloat64NeonMask ifTrue, PacketFloat64NeonMask ifFalse)
    {
        return new(AdvSimd.BitwiseSelect(mask._value, ifTrue._value, ifFalse._value));
    }

    public static unsafe PacketFloat64NeonMask Load(ReadOnlySpan<double> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (double* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketFloat64NeonMask Load(ReadOnlySpan<bool> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (bool* ptr = values)
        {
            return LoadBoolUnsafe(ptr);
        }
    }

    public static unsafe PacketFloat64NeonMask LoadAligned(ReadOnlySpan<double> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (double* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketFloat64NeonMask value, Span<double> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (double* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void Store(PacketFloat64NeonMask value, Span<bool> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (bool* ptr = destination)
        {
            StoreBoolUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketFloat64NeonMask value, Span<double> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (double* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat64NeonMask LoadUnsafe(double* source)
    {
        return new(AdvSimd.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat64NeonMask LoadBoolUnsafe(bool* source)
    {
        short packed = Unsafe.ReadUnaligned<short>(source);
        return new(Vector128.Create(
            (packed & 0x00FF) != 0 ? BitConverter.Int64BitsToDouble(-1) : 0d,
            (packed & unchecked((short) 0xFF00)) != 0 ? BitConverter.Int64BitsToDouble(-1) : 0d));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat64NeonMask LoadAlignedUnsafe(double* source)
    {
        return new(AdvSimd.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketFloat64NeonMask value, double* destination)
    {
        AdvSimd.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreBoolUnsafe(PacketFloat64NeonMask value, bool* destination)
    {
        for (int i = 0; i < LaneCount; i++)
        {
            destination[i] = Vector128.GetElement(value._value.AsInt64(), i) != 0;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketFloat64NeonMask value, double* destination)
    {
        AdvSimd.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask And(PacketFloat64NeonMask left, PacketFloat64NeonMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask Or(PacketFloat64NeonMask left, PacketFloat64NeonMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask Xor(PacketFloat64NeonMask left, PacketFloat64NeonMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask Not(PacketFloat64NeonMask value) => ~value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask operator &(PacketFloat64NeonMask left, PacketFloat64NeonMask right)
    {
        return new(AdvSimd.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask operator |(PacketFloat64NeonMask left, PacketFloat64NeonMask right)
    {
        return new(AdvSimd.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask operator ^(PacketFloat64NeonMask left, PacketFloat64NeonMask right)
    {
        return new(AdvSimd.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask operator ~(PacketFloat64NeonMask value)
    {
        return new(AdvSimd.Not(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask operator ==(PacketFloat64NeonMask left, PacketFloat64NeonMask right)
    {
        return ~(left ^ right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask operator !=(PacketFloat64NeonMask left, PacketFloat64NeonMask right)
    {
        return left ^ right;
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketFloat64NeonMask other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}
