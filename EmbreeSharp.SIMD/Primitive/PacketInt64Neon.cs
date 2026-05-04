using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;

namespace EmbreeSharp.SIMD;

public readonly struct PacketInt64Neon :
    ISimdInteger<PacketInt64Neon, long, PacketInt64NeonMask>
{
    internal readonly Vector128<long> _value;

    private PacketInt64Neon(Vector128<long> value)
    {
        _value = value;
    }

    public static int LaneCount => 2;

    public static PacketInt64Neon AdditiveIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(0);
    }

    public static PacketInt64Neon MultiplicativeIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Neon Broadcast(long value)
    {
        return new(Vector128.Create(value));
    }

    public static unsafe PacketInt64Neon Load(ReadOnlySpan<long> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (long* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketInt64Neon LoadAligned(ReadOnlySpan<long> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (long* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketInt64Neon value, Span<long> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (long* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketInt64Neon value, Span<long> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (long* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketInt64Neon LoadUnsafe(long* source)
    {
        return new(AdvSimd.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketInt64Neon LoadAlignedUnsafe(long* source)
    {
        return new(AdvSimd.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketInt64Neon value, long* destination)
    {
        AdvSimd.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketInt64Neon value, long* destination)
    {
        AdvSimd.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Neon Select(PacketInt64NeonMask mask, PacketInt64Neon ifTrue, PacketInt64Neon ifFalse)
    {
        return new(AdvSimd.BitwiseSelect(mask._value, ifTrue._value, ifFalse._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Neon Abs(PacketInt64Neon value)
    {
        return new(AdvSimd.Arm64.Abs(value._value).AsInt64());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Neon Min(PacketInt64Neon left, PacketInt64Neon right)
    {
        return new(Vector128.Create(
            Math.Min(Vector128.GetElement(left._value, 0), Vector128.GetElement(right._value, 0)),
            Math.Min(Vector128.GetElement(left._value, 1), Vector128.GetElement(right._value, 1))));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Neon Max(PacketInt64Neon left, PacketInt64Neon right)
    {
        return new(Vector128.Create(
            Math.Max(Vector128.GetElement(left._value, 0), Vector128.GetElement(right._value, 0)),
            Math.Max(Vector128.GetElement(left._value, 1), Vector128.GetElement(right._value, 1))));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Neon Clamp(PacketInt64Neon value, PacketInt64Neon min, PacketInt64Neon max)
    {
        return Min(Max(value, min), max);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Neon operator +(PacketInt64Neon value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Neon operator +(PacketInt64Neon left, PacketInt64Neon right)
    {
        return new(AdvSimd.Add(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Neon operator -(PacketInt64Neon left, PacketInt64Neon right)
    {
        return new(AdvSimd.Subtract(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Neon operator *(PacketInt64Neon left, PacketInt64Neon right)
    {
        return new(Vector128.Create(
            unchecked(Vector128.GetElement(left._value, 0) * Vector128.GetElement(right._value, 0)),
            unchecked(Vector128.GetElement(left._value, 1) * Vector128.GetElement(right._value, 1))));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Neon operator -(PacketInt64Neon value)
    {
        return new(AdvSimd.Subtract(Vector128<long>.Zero, value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Neon operator &(PacketInt64Neon left, PacketInt64Neon right)
    {
        return new(AdvSimd.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Neon operator |(PacketInt64Neon left, PacketInt64Neon right)
    {
        return new(AdvSimd.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Neon operator ^(PacketInt64Neon left, PacketInt64Neon right)
    {
        return new(AdvSimd.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Neon operator ~(PacketInt64Neon value)
    {
        return new(AdvSimd.Not(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask operator ==(PacketInt64Neon left, PacketInt64Neon right)
    {
        return new(AdvSimd.Arm64.CompareEqual(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask operator !=(PacketInt64Neon left, PacketInt64Neon right)
    {
        return ~(left == right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask operator <(PacketInt64Neon left, PacketInt64Neon right)
    {
        return new(AdvSimd.Arm64.CompareLessThan(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask operator >(PacketInt64Neon left, PacketInt64Neon right)
    {
        return new(AdvSimd.Arm64.CompareGreaterThan(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask operator <=(PacketInt64Neon left, PacketInt64Neon right)
    {
        return new(AdvSimd.Arm64.CompareLessThanOrEqual(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask operator >=(PacketInt64Neon left, PacketInt64Neon right)
    {
        return new(AdvSimd.Arm64.CompareGreaterThanOrEqual(left._value, right._value));
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketInt64Neon other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}

public readonly struct PacketInt64NeonMask : ISimdPacketMask<PacketInt64NeonMask, long>
{
    internal readonly Vector128<long> _value;

    internal PacketInt64NeonMask(Vector128<long> value)
    {
        _value = value;
    }

    public static int LaneCount => 2;

    public static PacketInt64NeonMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector128.Create(-1L));
    }

    public static PacketInt64NeonMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector128<long>.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask Broadcast(bool value) => value ? True : False;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool All(PacketInt64NeonMask value)
    {
        return Vector128.ExtractMostSignificantBits(value._value) == 0x3;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Any(PacketInt64NeonMask value)
    {
        return Vector128.ExtractMostSignificantBits(value._value) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool None(PacketInt64NeonMask value)
    {
        return Vector128.ExtractMostSignificantBits(value._value) == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask AndNot(PacketInt64NeonMask left, PacketInt64NeonMask right)
    {
        return new(AdvSimd.BitwiseClear(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask Select(PacketInt64NeonMask mask, PacketInt64NeonMask ifTrue, PacketInt64NeonMask ifFalse)
    {
        return new(AdvSimd.BitwiseSelect(mask._value, ifTrue._value, ifFalse._value));
    }

    public static unsafe PacketInt64NeonMask Load(ReadOnlySpan<long> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (long* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketInt64NeonMask Load(ReadOnlySpan<bool> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (bool* ptr = values)
        {
            return LoadBoolUnsafe(ptr);
        }
    }

    public static unsafe PacketInt64NeonMask LoadAligned(ReadOnlySpan<long> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (long* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketInt64NeonMask value, Span<long> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (long* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void Store(PacketInt64NeonMask value, Span<bool> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (bool* ptr = destination)
        {
            StoreBoolUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketInt64NeonMask value, Span<long> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (long* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketInt64NeonMask LoadUnsafe(long* source)
    {
        return new(AdvSimd.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketInt64NeonMask LoadBoolUnsafe(bool* source)
    {
        short packed = Unsafe.ReadUnaligned<short>(source);
        return new(Vector128.Create(
            (packed & 0x00FF) != 0 ? -1L : 0L,
            (packed & unchecked((short) 0xFF00)) != 0 ? -1L : 0L));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketInt64NeonMask LoadAlignedUnsafe(long* source)
    {
        return new(AdvSimd.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketInt64NeonMask value, long* destination)
    {
        AdvSimd.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreBoolUnsafe(PacketInt64NeonMask value, bool* destination)
    {
        for (int i = 0; i < LaneCount; i++)
        {
            destination[i] = Vector128.GetElement(value._value, i) != 0;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketInt64NeonMask value, long* destination)
    {
        AdvSimd.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask And(PacketInt64NeonMask left, PacketInt64NeonMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask Or(PacketInt64NeonMask left, PacketInt64NeonMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask Xor(PacketInt64NeonMask left, PacketInt64NeonMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask Not(PacketInt64NeonMask value) => ~value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask operator &(PacketInt64NeonMask left, PacketInt64NeonMask right)
    {
        return new(AdvSimd.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask operator |(PacketInt64NeonMask left, PacketInt64NeonMask right)
    {
        return new(AdvSimd.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask operator ^(PacketInt64NeonMask left, PacketInt64NeonMask right)
    {
        return new(AdvSimd.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask operator ~(PacketInt64NeonMask value)
    {
        return new(AdvSimd.Not(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask operator ==(PacketInt64NeonMask left, PacketInt64NeonMask right)
    {
        return ~(left ^ right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask operator !=(PacketInt64NeonMask left, PacketInt64NeonMask right)
    {
        return left ^ right;
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketInt64NeonMask other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}
