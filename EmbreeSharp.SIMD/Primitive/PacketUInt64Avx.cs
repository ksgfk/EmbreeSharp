using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace EmbreeSharp.SIMD;

public readonly struct PacketUInt64Avx :
    ISimdInteger<PacketUInt64Avx, ulong, PacketUInt64AvxMask>
{
    internal readonly Vector256<ulong> _value;

    private PacketUInt64Avx(Vector256<ulong> value)
    {
        _value = value;
    }

    public static int LaneCount => 4;

    public static PacketUInt64Avx AdditiveIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(0);
    }

    public static PacketUInt64Avx MultiplicativeIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Avx Broadcast(ulong value)
    {
        return new(Vector256.Create(value));
    }

    public static unsafe PacketUInt64Avx Load(ReadOnlySpan<ulong> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (ulong* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketUInt64Avx LoadAligned(ReadOnlySpan<ulong> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (ulong* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketUInt64Avx value, Span<ulong> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (ulong* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketUInt64Avx value, Span<ulong> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (ulong* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt64Avx LoadUnsafe(ulong* source)
    {
        return new(Avx.LoadVector256(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt64Avx LoadAlignedUnsafe(ulong* source)
    {
        return new(Avx.LoadAlignedVector256(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketUInt64Avx value, ulong* destination)
    {
        Avx.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketUInt64Avx value, ulong* destination)
    {
        Avx.StoreAligned(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Avx Select(PacketUInt64AvxMask mask, PacketUInt64Avx ifTrue, PacketUInt64Avx ifFalse)
    {
        return new(Avx2.BlendVariable(ifFalse._value, ifTrue._value, mask._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Avx Abs(PacketUInt64Avx value)
    {
        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Avx Min(PacketUInt64Avx left, PacketUInt64Avx right)
    {
        return Select(left < right, left, right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Avx Max(PacketUInt64Avx left, PacketUInt64Avx right)
    {
        return Select(left > right, left, right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Avx Clamp(PacketUInt64Avx value, PacketUInt64Avx min, PacketUInt64Avx max)
    {
        return Min(Max(value, min), max);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Avx operator +(PacketUInt64Avx value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Avx operator +(PacketUInt64Avx left, PacketUInt64Avx right)
    {
        return new(Avx2.Add(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Avx operator -(PacketUInt64Avx left, PacketUInt64Avx right)
    {
        return new(Avx2.Subtract(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Avx operator *(PacketUInt64Avx left, PacketUInt64Avx right)
    {
        Vector256<ulong> h0 = Avx2.ShiftRightLogical(left._value, 32);
        Vector256<ulong> h1 = Avx2.ShiftRightLogical(right._value, 32);
        Vector256<ulong> low = Avx2.Multiply(left._value.AsUInt32(), right._value.AsUInt32());
        Vector256<ulong> mix0 = Avx2.Multiply(left._value.AsUInt32(), h1.AsUInt32());
        Vector256<ulong> mix1 = Avx2.Multiply(h0.AsUInt32(), right._value.AsUInt32());
        Vector256<ulong> mix = Avx2.Add(mix0, mix1);
        return new(Avx2.Add(Avx2.ShiftLeftLogical(mix, 32), low));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Avx operator -(PacketUInt64Avx value)
    {
        return new(Avx2.Subtract(Vector256<ulong>.Zero, value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Avx operator &(PacketUInt64Avx left, PacketUInt64Avx right)
    {
        return new(Avx2.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Avx operator |(PacketUInt64Avx left, PacketUInt64Avx right)
    {
        return new(Avx2.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Avx operator ^(PacketUInt64Avx left, PacketUInt64Avx right)
    {
        return new(Avx2.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Avx operator ~(PacketUInt64Avx value)
    {
        return new(Avx2.Xor(value._value, Vector256.Create(ulong.MaxValue)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask operator ==(PacketUInt64Avx left, PacketUInt64Avx right)
    {
        return new(Avx2.CompareEqual(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask operator !=(PacketUInt64Avx left, PacketUInt64Avx right)
    {
        return ~(left == right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask operator <(PacketUInt64Avx left, PacketUInt64Avx right)
    {
        Vector256<ulong> offset = Vector256.Create(0x8000000000000000ul);
        return new(Avx2.CompareGreaterThan(
            Avx2.Subtract(right._value, offset).AsInt64(),
            Avx2.Subtract(left._value, offset).AsInt64()).AsUInt64());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask operator >(PacketUInt64Avx left, PacketUInt64Avx right)
    {
        Vector256<ulong> offset = Vector256.Create(0x8000000000000000ul);
        return new(Avx2.CompareGreaterThan(
            Avx2.Subtract(left._value, offset).AsInt64(),
            Avx2.Subtract(right._value, offset).AsInt64()).AsUInt64());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask operator <=(PacketUInt64Avx left, PacketUInt64Avx right)
    {
        return ~(left > right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask operator >=(PacketUInt64Avx left, PacketUInt64Avx right)
    {
        return ~(left < right);
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketUInt64Avx other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}

public readonly struct PacketUInt64AvxMask : ISimdPacketMask<PacketUInt64AvxMask, ulong>
{
    internal readonly Vector256<ulong> _value;

    internal PacketUInt64AvxMask(Vector256<ulong> value)
    {
        _value = value;
    }

    public static int LaneCount => 4;

    public static PacketUInt64AvxMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector256.Create(ulong.MaxValue));
    }

    public static PacketUInt64AvxMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector256<ulong>.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask Broadcast(bool value) => value ? True : False;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool All(PacketUInt64AvxMask value)
    {
        return Avx.MoveMask(value._value.AsDouble()) == 0xF;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Any(PacketUInt64AvxMask value)
    {
        return Avx.MoveMask(value._value.AsDouble()) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool None(PacketUInt64AvxMask value)
    {
        return Avx.MoveMask(value._value.AsDouble()) == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask AndNot(PacketUInt64AvxMask left, PacketUInt64AvxMask right)
    {
        return new(Avx2.AndNot(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask Select(PacketUInt64AvxMask mask, PacketUInt64AvxMask ifTrue, PacketUInt64AvxMask ifFalse)
    {
        return new(Avx2.BlendVariable(ifFalse._value, ifTrue._value, mask._value));
    }

    public static unsafe PacketUInt64AvxMask Load(ReadOnlySpan<ulong> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (ulong* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketUInt64AvxMask Load(ReadOnlySpan<bool> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (bool* ptr = values)
        {
            return LoadBoolUnsafe(ptr);
        }
    }

    public static unsafe PacketUInt64AvxMask LoadAligned(ReadOnlySpan<ulong> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (ulong* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketUInt64AvxMask value, Span<ulong> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (ulong* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void Store(PacketUInt64AvxMask value, Span<bool> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (bool* ptr = destination)
        {
            StoreBoolUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketUInt64AvxMask value, Span<ulong> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (ulong* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt64AvxMask LoadUnsafe(ulong* source)
    {
        return new(Avx.LoadVector256(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt64AvxMask LoadBoolUnsafe(bool* source)
    {
        int packed = Unsafe.ReadUnaligned<int>(source);
        Vector128<sbyte> byteValues = Vector128.CreateScalarUnsafe(packed).AsSByte();
        Vector128<sbyte> byteMask = Sse2.CompareGreaterThan(byteValues, Vector128<sbyte>.Zero);

        return new(Avx2.ConvertToVector256Int64(byteMask).AsUInt64());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt64AvxMask LoadAlignedUnsafe(ulong* source)
    {
        return new(Avx.LoadAlignedVector256(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketUInt64AvxMask value, ulong* destination)
    {
        Avx.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreBoolUnsafe(PacketUInt64AvxMask value, bool* destination)
    {
        int mask = Avx.MoveMask(value._value.AsDouble());
        for (int i = 0; i < LaneCount; i++)
        {
            destination[i] = ((mask >> i) & 1) != 0;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketUInt64AvxMask value, ulong* destination)
    {
        Avx.StoreAligned(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask And(PacketUInt64AvxMask left, PacketUInt64AvxMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask Or(PacketUInt64AvxMask left, PacketUInt64AvxMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask Xor(PacketUInt64AvxMask left, PacketUInt64AvxMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask Not(PacketUInt64AvxMask value) => ~value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask operator &(PacketUInt64AvxMask left, PacketUInt64AvxMask right)
    {
        return new(Avx2.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask operator |(PacketUInt64AvxMask left, PacketUInt64AvxMask right)
    {
        return new(Avx2.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask operator ^(PacketUInt64AvxMask left, PacketUInt64AvxMask right)
    {
        return new(Avx2.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask operator ~(PacketUInt64AvxMask value)
    {
        return new(Avx2.Xor(value._value, Vector256.Create(ulong.MaxValue)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask operator ==(PacketUInt64AvxMask left, PacketUInt64AvxMask right)
    {
        return ~(left ^ right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask operator !=(PacketUInt64AvxMask left, PacketUInt64AvxMask right)
    {
        return left ^ right;
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketUInt64AvxMask other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}
