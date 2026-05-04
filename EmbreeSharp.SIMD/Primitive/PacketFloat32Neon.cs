using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;

namespace EmbreeSharp.SIMD;

public readonly struct PacketFloat32Neon :
    ISimdFloatingPoint<PacketFloat32Neon, float, PacketFloat32NeonMask>
{
    internal readonly Vector128<float> _value;

    internal PacketFloat32Neon(Vector128<float> value)
    {
        _value = value;
    }

    public static int LaneCount => 4;

    public static PacketFloat32Neon AdditiveIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(0f);
    }

    public static PacketFloat32Neon MultiplicativeIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(1f);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Neon Broadcast(float value)
    {
        return new(Vector128.Create(value));
    }

    public static unsafe PacketFloat32Neon Load(ReadOnlySpan<float> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (float* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketFloat32Neon LoadAligned(ReadOnlySpan<float> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (float* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketFloat32Neon value, Span<float> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (float* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketFloat32Neon value, Span<float> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (float* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat32Neon LoadUnsafe(float* source)
    {
        return new(AdvSimd.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat32Neon LoadAlignedUnsafe(float* source)
    {
        return new(AdvSimd.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketFloat32Neon value, float* destination)
    {
        AdvSimd.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketFloat32Neon value, float* destination)
    {
        AdvSimd.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Neon Select(PacketFloat32NeonMask mask, PacketFloat32Neon ifTrue, PacketFloat32Neon ifFalse)
    {
        return new(AdvSimd.BitwiseSelect(mask._value, ifTrue._value, ifFalse._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Neon Abs(PacketFloat32Neon value)
    {
        return new(AdvSimd.Abs(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Neon Min(PacketFloat32Neon left, PacketFloat32Neon right)
    {
        return new(AdvSimd.Min(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Neon Max(PacketFloat32Neon left, PacketFloat32Neon right)
    {
        return new(AdvSimd.Max(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Neon Clamp(PacketFloat32Neon value, PacketFloat32Neon min, PacketFloat32Neon max)
    {
        return Min(Max(value, min), max);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Neon Sqrt(PacketFloat32Neon value)
    {
        return new(AdvSimd.Arm64.Sqrt(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Neon Reciprocal(PacketFloat32Neon value)
    {
        Vector128<float> estimate = AdvSimd.ReciprocalEstimate(value._value);
        estimate = AdvSimd.Multiply(estimate, AdvSimd.ReciprocalStep(estimate, value._value));
        estimate = AdvSimd.Multiply(estimate, AdvSimd.ReciprocalStep(estimate, value._value));
        return new(estimate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Neon ReciprocalSqrt(PacketFloat32Neon value)
    {
        Vector128<float> estimate = AdvSimd.ReciprocalSquareRootEstimate(value._value);
        estimate = AdvSimd.Multiply(estimate, AdvSimd.ReciprocalSquareRootStep(AdvSimd.Multiply(estimate, estimate), value._value));
        estimate = AdvSimd.Multiply(estimate, AdvSimd.ReciprocalSquareRootStep(AdvSimd.Multiply(estimate, estimate), value._value));
        return new(estimate);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Neon Floor(PacketFloat32Neon value)
    {
        return new(AdvSimd.RoundToNegativeInfinity(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Neon Ceiling(PacketFloat32Neon value)
    {
        return new(AdvSimd.RoundToPositiveInfinity(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Neon Truncate(PacketFloat32Neon value)
    {
        return new(AdvSimd.RoundToZero(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Neon Round(PacketFloat32Neon value)
    {
        return new(AdvSimd.RoundToNearest(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Neon Sin(PacketFloat32Neon value) => SimdFloatingPointMath.SinFloat<PacketFloat32Neon, PacketFloat32NeonMask>(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Neon Cos(PacketFloat32Neon value) => SimdFloatingPointMath.CosFloat<PacketFloat32Neon, PacketFloat32NeonMask>(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (PacketFloat32Neon Sin, PacketFloat32Neon Cos) SinCos(PacketFloat32Neon value) => SimdFloatingPointMath.SinCosFloat<PacketFloat32Neon, PacketFloat32NeonMask>(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Neon FusedMultiplyAdd(PacketFloat32Neon left, PacketFloat32Neon right, PacketFloat32Neon addend)
    {
        return new(AdvSimd.FusedMultiplyAdd(addend._value, left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Neon CopySign(PacketFloat32Neon value, PacketFloat32Neon sign)
    {
        Vector128<float> signMask = Vector128.Create(-0f);
        Vector128<float> magnitude = AdvSimd.BitwiseClear(value._value, signMask);
        Vector128<float> signBits = AdvSimd.And(sign._value, signMask);
        return new(AdvSimd.Or(magnitude, signBits));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask IsFinite(PacketFloat32Neon value)
    {
        Vector128<float> magnitude = AdvSimd.Abs(value._value);
        return new(AdvSimd.CompareLessThan(magnitude, Vector128.Create(float.PositiveInfinity)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask IsInfinity(PacketFloat32Neon value)
    {
        Vector128<float> magnitude = AdvSimd.Abs(value._value);
        return new(AdvSimd.CompareEqual(magnitude, Vector128.Create(float.PositiveInfinity)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask IsNaN(PacketFloat32Neon value)
    {
        return new(~AdvSimd.CompareEqual(value._value, value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Neon operator +(PacketFloat32Neon value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Neon operator +(PacketFloat32Neon left, PacketFloat32Neon right)
    {
        return new(AdvSimd.Add(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Neon operator -(PacketFloat32Neon left, PacketFloat32Neon right)
    {
        return new(AdvSimd.Subtract(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Neon operator *(PacketFloat32Neon left, PacketFloat32Neon right)
    {
        return new(AdvSimd.Multiply(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Neon operator /(PacketFloat32Neon left, PacketFloat32Neon right)
    {
        return new(AdvSimd.Arm64.Divide(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Neon operator -(PacketFloat32Neon value)
    {
        return new(AdvSimd.Xor(value._value, Vector128.Create(-0f)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask operator ==(PacketFloat32Neon left, PacketFloat32Neon right)
    {
        return new(AdvSimd.CompareEqual(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask operator !=(PacketFloat32Neon left, PacketFloat32Neon right)
    {
        return ~(left == right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask operator <(PacketFloat32Neon left, PacketFloat32Neon right)
    {
        return new(AdvSimd.CompareLessThan(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask operator >(PacketFloat32Neon left, PacketFloat32Neon right)
    {
        return new(AdvSimd.CompareGreaterThan(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask operator <=(PacketFloat32Neon left, PacketFloat32Neon right)
    {
        return new(AdvSimd.CompareLessThanOrEqual(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask operator >=(PacketFloat32Neon left, PacketFloat32Neon right)
    {
        return new(AdvSimd.CompareGreaterThanOrEqual(left._value, right._value));
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketFloat32Neon other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}

public readonly struct PacketFloat32NeonMask : ISimdPacketMask<PacketFloat32NeonMask, float>
{
    internal readonly Vector128<float> _value;

    internal PacketFloat32NeonMask(Vector128<float> value)
    {
        _value = value;
    }

    public static int LaneCount => 4;

    public static PacketFloat32NeonMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector128.Create(BitConverter.Int32BitsToSingle(-1)));
    }

    public static PacketFloat32NeonMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector128<float>.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask Broadcast(bool value) => value ? True : False;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool All(PacketFloat32NeonMask value)
    {
        return Vector128.ExtractMostSignificantBits(value._value) == 0xF;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Any(PacketFloat32NeonMask value)
    {
        return Vector128.ExtractMostSignificantBits(value._value) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool None(PacketFloat32NeonMask value)
    {
        return Vector128.ExtractMostSignificantBits(value._value) == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask AndNot(PacketFloat32NeonMask left, PacketFloat32NeonMask right)
    {
        return new(AdvSimd.BitwiseClear(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask Select(PacketFloat32NeonMask mask, PacketFloat32NeonMask ifTrue, PacketFloat32NeonMask ifFalse)
    {
        return new(AdvSimd.BitwiseSelect(mask._value, ifTrue._value, ifFalse._value));
    }

    public static unsafe PacketFloat32NeonMask Load(ReadOnlySpan<float> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (float* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketFloat32NeonMask Load(ReadOnlySpan<bool> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (bool* ptr = values)
        {
            return LoadBoolUnsafe(ptr);
        }
    }

    public static unsafe PacketFloat32NeonMask LoadAligned(ReadOnlySpan<float> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (float* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketFloat32NeonMask value, Span<float> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (float* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void Store(PacketFloat32NeonMask value, Span<bool> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (bool* ptr = destination)
        {
            StoreBoolUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketFloat32NeonMask value, Span<float> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (float* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat32NeonMask LoadUnsafe(float* source)
    {
        return new(AdvSimd.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat32NeonMask LoadBoolUnsafe(bool* source)
    {
        int packed = Unsafe.ReadUnaligned<int>(source);
        return new(Vector128.Create(
            (packed & 0x000000FF) != 0 ? BitConverter.Int32BitsToSingle(-1) : 0f,
            (packed & 0x0000FF00) != 0 ? BitConverter.Int32BitsToSingle(-1) : 0f,
            (packed & 0x00FF0000) != 0 ? BitConverter.Int32BitsToSingle(-1) : 0f,
            (packed & unchecked((int) 0xFF000000)) != 0 ? BitConverter.Int32BitsToSingle(-1) : 0f));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat32NeonMask LoadAlignedUnsafe(float* source)
    {
        return new(AdvSimd.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketFloat32NeonMask value, float* destination)
    {
        AdvSimd.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreBoolUnsafe(PacketFloat32NeonMask value, bool* destination)
    {
        for (int i = 0; i < LaneCount; i++)
        {
            destination[i] = Vector128.GetElement(value._value.AsInt32(), i) != 0;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketFloat32NeonMask value, float* destination)
    {
        AdvSimd.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask And(PacketFloat32NeonMask left, PacketFloat32NeonMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask Or(PacketFloat32NeonMask left, PacketFloat32NeonMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask Xor(PacketFloat32NeonMask left, PacketFloat32NeonMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask Not(PacketFloat32NeonMask value) => ~value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask operator &(PacketFloat32NeonMask left, PacketFloat32NeonMask right)
    {
        return new(AdvSimd.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask operator |(PacketFloat32NeonMask left, PacketFloat32NeonMask right)
    {
        return new(AdvSimd.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask operator ^(PacketFloat32NeonMask left, PacketFloat32NeonMask right)
    {
        return new(AdvSimd.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask operator ~(PacketFloat32NeonMask value)
    {
        return new(AdvSimd.Not(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask operator ==(PacketFloat32NeonMask left, PacketFloat32NeonMask right)
    {
        return ~(left ^ right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask operator !=(PacketFloat32NeonMask left, PacketFloat32NeonMask right)
    {
        return left ^ right;
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketFloat32NeonMask other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}
