using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;

namespace EmbreeSharp.SIMD;

public readonly struct PacketUInt64Neon :
    ISimdInteger<PacketUInt64Neon, ulong, PacketUInt64NeonMask>
{
    internal readonly Vector128<ulong> _value;

    private PacketUInt64Neon(Vector128<ulong> value)
    {
        _value = value;
    }

    public static int LaneCount => 2;

    public static PacketUInt64Neon AdditiveIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(0);
    }

    public static PacketUInt64Neon MultiplicativeIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Neon Broadcast(ulong value)
    {
        return new(Vector128.Create(value));
    }

    public static unsafe PacketUInt64Neon Load(ReadOnlySpan<ulong> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (ulong* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketUInt64Neon LoadAligned(ReadOnlySpan<ulong> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (ulong* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketUInt64Neon value, Span<ulong> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (ulong* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketUInt64Neon value, Span<ulong> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (ulong* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt64Neon LoadUnsafe(ulong* source)
    {
        return new(AdvSimd.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt64Neon LoadAlignedUnsafe(ulong* source)
    {
        return new(AdvSimd.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketUInt64Neon value, ulong* destination)
    {
        AdvSimd.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketUInt64Neon value, ulong* destination)
    {
        AdvSimd.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Neon Select(PacketUInt64NeonMask mask, PacketUInt64Neon ifTrue, PacketUInt64Neon ifFalse)
    {
        return new(AdvSimd.BitwiseSelect(mask._value, ifTrue._value, ifFalse._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Neon Abs(PacketUInt64Neon value)
    {
        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Neon Min(PacketUInt64Neon left, PacketUInt64Neon right)
    {
        return new(Vector128.Create(
            Math.Min(Vector128.GetElement(left._value, 0), Vector128.GetElement(right._value, 0)),
            Math.Min(Vector128.GetElement(left._value, 1), Vector128.GetElement(right._value, 1))));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Neon Max(PacketUInt64Neon left, PacketUInt64Neon right)
    {
        return new(Vector128.Create(
            Math.Max(Vector128.GetElement(left._value, 0), Vector128.GetElement(right._value, 0)),
            Math.Max(Vector128.GetElement(left._value, 1), Vector128.GetElement(right._value, 1))));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Neon Clamp(PacketUInt64Neon value, PacketUInt64Neon min, PacketUInt64Neon max)
    {
        return Min(Max(value, min), max);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Neon operator +(PacketUInt64Neon value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Neon operator +(PacketUInt64Neon left, PacketUInt64Neon right)
    {
        return new(AdvSimd.Add(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Neon operator -(PacketUInt64Neon left, PacketUInt64Neon right)
    {
        return new(AdvSimd.Subtract(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Neon operator *(PacketUInt64Neon left, PacketUInt64Neon right)
    {
        return new(Vector128.Create(
            unchecked(Vector128.GetElement(left._value, 0) * Vector128.GetElement(right._value, 0)),
            unchecked(Vector128.GetElement(left._value, 1) * Vector128.GetElement(right._value, 1))));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Neon operator -(PacketUInt64Neon value)
    {
        return new(AdvSimd.Subtract(Vector128<ulong>.Zero, value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Neon operator &(PacketUInt64Neon left, PacketUInt64Neon right)
    {
        return new(AdvSimd.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Neon operator |(PacketUInt64Neon left, PacketUInt64Neon right)
    {
        return new(AdvSimd.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Neon operator ^(PacketUInt64Neon left, PacketUInt64Neon right)
    {
        return new(AdvSimd.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Neon operator ~(PacketUInt64Neon value)
    {
        return new(AdvSimd.Not(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask operator ==(PacketUInt64Neon left, PacketUInt64Neon right)
    {
        return new(AdvSimd.Arm64.CompareEqual(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask operator !=(PacketUInt64Neon left, PacketUInt64Neon right)
    {
        return ~(left == right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask operator <(PacketUInt64Neon left, PacketUInt64Neon right)
    {
        return new(AdvSimd.Arm64.CompareLessThan(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask operator >(PacketUInt64Neon left, PacketUInt64Neon right)
    {
        return new(AdvSimd.Arm64.CompareGreaterThan(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask operator <=(PacketUInt64Neon left, PacketUInt64Neon right)
    {
        return new(AdvSimd.Arm64.CompareLessThanOrEqual(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask operator >=(PacketUInt64Neon left, PacketUInt64Neon right)
    {
        return new(AdvSimd.Arm64.CompareGreaterThanOrEqual(left._value, right._value));
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketUInt64Neon other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}

public readonly struct PacketUInt64NeonMask : ISimdPacketMask<PacketUInt64NeonMask, ulong>
{
    internal readonly Vector128<ulong> _value;

    internal PacketUInt64NeonMask(Vector128<ulong> value)
    {
        _value = value;
    }

    public static int LaneCount => 2;

    public static PacketUInt64NeonMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector128.Create(ulong.MaxValue));
    }

    public static PacketUInt64NeonMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector128<ulong>.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask Broadcast(bool value) => value ? True : False;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool All(PacketUInt64NeonMask value)
    {
        return Vector128.ExtractMostSignificantBits(value._value) == 0x3;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Any(PacketUInt64NeonMask value)
    {
        return Vector128.ExtractMostSignificantBits(value._value) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool None(PacketUInt64NeonMask value)
    {
        return Vector128.ExtractMostSignificantBits(value._value) == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask AndNot(PacketUInt64NeonMask left, PacketUInt64NeonMask right)
    {
        return new(AdvSimd.BitwiseClear(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask Select(PacketUInt64NeonMask mask, PacketUInt64NeonMask ifTrue, PacketUInt64NeonMask ifFalse)
    {
        return new(AdvSimd.BitwiseSelect(mask._value, ifTrue._value, ifFalse._value));
    }

    public static unsafe PacketUInt64NeonMask Load(ReadOnlySpan<ulong> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (ulong* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketUInt64NeonMask Load(ReadOnlySpan<bool> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (bool* ptr = values)
        {
            return LoadBoolUnsafe(ptr);
        }
    }

    public static unsafe PacketUInt64NeonMask LoadAligned(ReadOnlySpan<ulong> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (ulong* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketUInt64NeonMask value, Span<ulong> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (ulong* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void Store(PacketUInt64NeonMask value, Span<bool> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (bool* ptr = destination)
        {
            StoreBoolUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketUInt64NeonMask value, Span<ulong> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (ulong* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt64NeonMask LoadUnsafe(ulong* source)
    {
        return new(AdvSimd.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt64NeonMask LoadBoolUnsafe(bool* source)
    {
        short packed = Unsafe.ReadUnaligned<short>(source);
        return new(Vector128.Create(
            (packed & 0x00FF) != 0 ? ulong.MaxValue : 0ul,
            (packed & unchecked((short) 0xFF00)) != 0 ? ulong.MaxValue : 0ul));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt64NeonMask LoadAlignedUnsafe(ulong* source)
    {
        return new(AdvSimd.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketUInt64NeonMask value, ulong* destination)
    {
        AdvSimd.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreBoolUnsafe(PacketUInt64NeonMask value, bool* destination)
    {
        for (int i = 0; i < LaneCount; i++)
        {
            destination[i] = Vector128.GetElement(value._value, i) != 0;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketUInt64NeonMask value, ulong* destination)
    {
        AdvSimd.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask And(PacketUInt64NeonMask left, PacketUInt64NeonMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask Or(PacketUInt64NeonMask left, PacketUInt64NeonMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask Xor(PacketUInt64NeonMask left, PacketUInt64NeonMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask Not(PacketUInt64NeonMask value) => ~value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask operator &(PacketUInt64NeonMask left, PacketUInt64NeonMask right)
    {
        return new(AdvSimd.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask operator |(PacketUInt64NeonMask left, PacketUInt64NeonMask right)
    {
        return new(AdvSimd.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask operator ^(PacketUInt64NeonMask left, PacketUInt64NeonMask right)
    {
        return new(AdvSimd.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask operator ~(PacketUInt64NeonMask value)
    {
        return new(AdvSimd.Not(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask operator ==(PacketUInt64NeonMask left, PacketUInt64NeonMask right)
    {
        return ~(left ^ right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask operator !=(PacketUInt64NeonMask left, PacketUInt64NeonMask right)
    {
        return left ^ right;
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketUInt64NeonMask other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}
