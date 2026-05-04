using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;

namespace EmbreeSharp.SIMD;

public readonly struct PacketUInt32Neon :
    ISimdInteger<PacketUInt32Neon, uint, PacketUInt32NeonMask>
{
    internal readonly Vector128<uint> _value;

    private PacketUInt32Neon(Vector128<uint> value)
    {
        _value = value;
    }

    public static int LaneCount => 4;

    public static PacketUInt32Neon AdditiveIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(0);
    }

    public static PacketUInt32Neon MultiplicativeIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Neon Broadcast(uint value)
    {
        return new(Vector128.Create(value));
    }

    public static unsafe PacketUInt32Neon Load(ReadOnlySpan<uint> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (uint* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketUInt32Neon LoadAligned(ReadOnlySpan<uint> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (uint* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketUInt32Neon value, Span<uint> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (uint* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketUInt32Neon value, Span<uint> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (uint* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt32Neon LoadUnsafe(uint* source)
    {
        return new(AdvSimd.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt32Neon LoadAlignedUnsafe(uint* source)
    {
        return new(AdvSimd.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketUInt32Neon value, uint* destination)
    {
        AdvSimd.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketUInt32Neon value, uint* destination)
    {
        AdvSimd.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Neon Select(PacketUInt32NeonMask mask, PacketUInt32Neon ifTrue, PacketUInt32Neon ifFalse)
    {
        return new(AdvSimd.BitwiseSelect(mask._value, ifTrue._value, ifFalse._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Neon Abs(PacketUInt32Neon value)
    {
        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Neon Min(PacketUInt32Neon left, PacketUInt32Neon right)
    {
        return new(AdvSimd.Min(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Neon Max(PacketUInt32Neon left, PacketUInt32Neon right)
    {
        return new(AdvSimd.Max(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Neon Clamp(PacketUInt32Neon value, PacketUInt32Neon min, PacketUInt32Neon max)
    {
        return Min(Max(value, min), max);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Neon operator +(PacketUInt32Neon value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Neon operator +(PacketUInt32Neon left, PacketUInt32Neon right)
    {
        return new(AdvSimd.Add(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Neon operator -(PacketUInt32Neon left, PacketUInt32Neon right)
    {
        return new(AdvSimd.Subtract(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Neon operator *(PacketUInt32Neon left, PacketUInt32Neon right)
    {
        return new(AdvSimd.Multiply(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Neon operator -(PacketUInt32Neon value)
    {
        return new(AdvSimd.Subtract(Vector128<uint>.Zero, value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Neon operator &(PacketUInt32Neon left, PacketUInt32Neon right)
    {
        return new(AdvSimd.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Neon operator |(PacketUInt32Neon left, PacketUInt32Neon right)
    {
        return new(AdvSimd.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Neon operator ^(PacketUInt32Neon left, PacketUInt32Neon right)
    {
        return new(AdvSimd.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Neon operator ~(PacketUInt32Neon value)
    {
        return new(AdvSimd.Not(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask operator ==(PacketUInt32Neon left, PacketUInt32Neon right)
    {
        return new(AdvSimd.CompareEqual(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask operator !=(PacketUInt32Neon left, PacketUInt32Neon right)
    {
        return ~(left == right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask operator <(PacketUInt32Neon left, PacketUInt32Neon right)
    {
        return new(AdvSimd.CompareLessThan(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask operator >(PacketUInt32Neon left, PacketUInt32Neon right)
    {
        return new(AdvSimd.CompareGreaterThan(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask operator <=(PacketUInt32Neon left, PacketUInt32Neon right)
    {
        return new(AdvSimd.CompareLessThanOrEqual(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask operator >=(PacketUInt32Neon left, PacketUInt32Neon right)
    {
        return new(AdvSimd.CompareGreaterThanOrEqual(left._value, right._value));
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketUInt32Neon other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}

public readonly struct PacketUInt32NeonMask : ISimdPacketMask<PacketUInt32NeonMask, uint>
{
    internal readonly Vector128<uint> _value;

    internal PacketUInt32NeonMask(Vector128<uint> value)
    {
        _value = value;
    }

    public static int LaneCount => 4;

    public static PacketUInt32NeonMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector128.Create(uint.MaxValue));
    }

    public static PacketUInt32NeonMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector128<uint>.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask Broadcast(bool value) => value ? True : False;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool All(PacketUInt32NeonMask value)
    {
        return Vector128.ExtractMostSignificantBits(value._value) == 0xF;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Any(PacketUInt32NeonMask value)
    {
        return Vector128.ExtractMostSignificantBits(value._value) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool None(PacketUInt32NeonMask value)
    {
        return Vector128.ExtractMostSignificantBits(value._value) == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask AndNot(PacketUInt32NeonMask left, PacketUInt32NeonMask right)
    {
        return new(AdvSimd.BitwiseClear(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask Select(PacketUInt32NeonMask mask, PacketUInt32NeonMask ifTrue, PacketUInt32NeonMask ifFalse)
    {
        return new(AdvSimd.BitwiseSelect(mask._value, ifTrue._value, ifFalse._value));
    }

    public static unsafe PacketUInt32NeonMask Load(ReadOnlySpan<uint> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (uint* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketUInt32NeonMask Load(ReadOnlySpan<bool> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (bool* ptr = values)
        {
            return LoadBoolUnsafe(ptr);
        }
    }

    public static unsafe PacketUInt32NeonMask LoadAligned(ReadOnlySpan<uint> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (uint* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketUInt32NeonMask value, Span<uint> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (uint* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void Store(PacketUInt32NeonMask value, Span<bool> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (bool* ptr = destination)
        {
            StoreBoolUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketUInt32NeonMask value, Span<uint> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (uint* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt32NeonMask LoadUnsafe(uint* source)
    {
        return new(AdvSimd.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt32NeonMask LoadBoolUnsafe(bool* source)
    {
        int packed = Unsafe.ReadUnaligned<int>(source);
        return new(Vector128.Create(
            (packed & 0x000000FF) != 0 ? uint.MaxValue : 0u,
            (packed & 0x0000FF00) != 0 ? uint.MaxValue : 0u,
            (packed & 0x00FF0000) != 0 ? uint.MaxValue : 0u,
            (packed & unchecked((int) 0xFF000000)) != 0 ? uint.MaxValue : 0u));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt32NeonMask LoadAlignedUnsafe(uint* source)
    {
        return new(AdvSimd.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketUInt32NeonMask value, uint* destination)
    {
        AdvSimd.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreBoolUnsafe(PacketUInt32NeonMask value, bool* destination)
    {
        for (int i = 0; i < LaneCount; i++)
        {
            destination[i] = Vector128.GetElement(value._value, i) != 0;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketUInt32NeonMask value, uint* destination)
    {
        AdvSimd.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask And(PacketUInt32NeonMask left, PacketUInt32NeonMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask Or(PacketUInt32NeonMask left, PacketUInt32NeonMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask Xor(PacketUInt32NeonMask left, PacketUInt32NeonMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask Not(PacketUInt32NeonMask value) => ~value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask operator &(PacketUInt32NeonMask left, PacketUInt32NeonMask right)
    {
        return new(AdvSimd.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask operator |(PacketUInt32NeonMask left, PacketUInt32NeonMask right)
    {
        return new(AdvSimd.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask operator ^(PacketUInt32NeonMask left, PacketUInt32NeonMask right)
    {
        return new(AdvSimd.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask operator ~(PacketUInt32NeonMask value)
    {
        return new(AdvSimd.Not(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask operator ==(PacketUInt32NeonMask left, PacketUInt32NeonMask right)
    {
        return ~(left ^ right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask operator !=(PacketUInt32NeonMask left, PacketUInt32NeonMask right)
    {
        return left ^ right;
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketUInt32NeonMask other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}
