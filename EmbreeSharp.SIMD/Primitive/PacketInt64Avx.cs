using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace EmbreeSharp.SIMD;

public readonly struct PacketInt64Avx :
    ISimdInteger<PacketInt64Avx, long, PacketInt64AvxMask>
{
    internal readonly Vector256<long> _value;

    private PacketInt64Avx(Vector256<long> value)
    {
        _value = value;
    }

    public static int LaneCount => 4;

    public static PacketInt64Avx AdditiveIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(0);
    }

    public static PacketInt64Avx MultiplicativeIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Avx Broadcast(long value)
    {
        return new(Vector256.Create(value));
    }

    public static unsafe PacketInt64Avx Load(ReadOnlySpan<long> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (long* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketInt64Avx LoadAligned(ReadOnlySpan<long> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (long* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketInt64Avx value, Span<long> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (long* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketInt64Avx value, Span<long> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (long* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketInt64Avx LoadUnsafe(long* source)
    {
        return new(Avx.LoadVector256(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketInt64Avx LoadAlignedUnsafe(long* source)
    {
        return new(Avx.LoadAlignedVector256(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketInt64Avx value, long* destination)
    {
        Avx.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketInt64Avx value, long* destination)
    {
        Avx.StoreAligned(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Avx Select(PacketInt64AvxMask mask, PacketInt64Avx ifTrue, PacketInt64Avx ifFalse)
    {
        return new(Avx2.BlendVariable(ifFalse._value, ifTrue._value, mask._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Avx Abs(PacketInt64Avx value)
    {
        return Select(value < Broadcast(0), -value, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Avx Min(PacketInt64Avx left, PacketInt64Avx right)
    {
        return Select(left < right, left, right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Avx Max(PacketInt64Avx left, PacketInt64Avx right)
    {
        return Select(left > right, left, right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Avx Clamp(PacketInt64Avx value, PacketInt64Avx min, PacketInt64Avx max)
    {
        return Min(Max(value, min), max);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Avx operator +(PacketInt64Avx value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Avx operator +(PacketInt64Avx left, PacketInt64Avx right)
    {
        return new(Avx2.Add(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Avx operator -(PacketInt64Avx left, PacketInt64Avx right)
    {
        return new(Avx2.Subtract(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Avx operator *(PacketInt64Avx left, PacketInt64Avx right)
    {
        Vector256<ulong> leftValue = left._value.AsUInt64();
        Vector256<ulong> rightValue = right._value.AsUInt64();
        Vector256<ulong> h0 = Avx2.ShiftRightLogical(leftValue, 32);
        Vector256<ulong> h1 = Avx2.ShiftRightLogical(rightValue, 32);
        Vector256<ulong> low = Avx2.Multiply(leftValue.AsUInt32(), rightValue.AsUInt32());
        Vector256<ulong> mix0 = Avx2.Multiply(leftValue.AsUInt32(), h1.AsUInt32());
        Vector256<ulong> mix1 = Avx2.Multiply(h0.AsUInt32(), rightValue.AsUInt32());
        Vector256<ulong> mix = Avx2.Add(mix0, mix1);
        return new(Avx2.Add(Avx2.ShiftLeftLogical(mix, 32), low).AsInt64());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Avx operator -(PacketInt64Avx value)
    {
        return new(Avx2.Subtract(Vector256<long>.Zero, value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Avx operator &(PacketInt64Avx left, PacketInt64Avx right)
    {
        return new(Avx2.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Avx operator |(PacketInt64Avx left, PacketInt64Avx right)
    {
        return new(Avx2.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Avx operator ^(PacketInt64Avx left, PacketInt64Avx right)
    {
        return new(Avx2.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Avx operator ~(PacketInt64Avx value)
    {
        return new(Avx2.Xor(value._value, Vector256.Create(-1L)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask operator ==(PacketInt64Avx left, PacketInt64Avx right)
    {
        return new(Avx2.CompareEqual(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask operator !=(PacketInt64Avx left, PacketInt64Avx right)
    {
        return ~(left == right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask operator <(PacketInt64Avx left, PacketInt64Avx right)
    {
        return new(Avx2.CompareGreaterThan(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask operator >(PacketInt64Avx left, PacketInt64Avx right)
    {
        return new(Avx2.CompareGreaterThan(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask operator <=(PacketInt64Avx left, PacketInt64Avx right)
    {
        return ~(left > right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask operator >=(PacketInt64Avx left, PacketInt64Avx right)
    {
        return ~(left < right);
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketInt64Avx other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}

public readonly struct PacketInt64AvxMask : ISimdPacketMask<PacketInt64AvxMask, long>
{
    internal readonly Vector256<long> _value;

    internal PacketInt64AvxMask(Vector256<long> value)
    {
        _value = value;
    }

    public static int LaneCount => 4;

    public static PacketInt64AvxMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector256.Create(-1L));
    }

    public static PacketInt64AvxMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector256<long>.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask Broadcast(bool value) => value ? True : False;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool All(PacketInt64AvxMask value)
    {
        return Avx.MoveMask(value._value.AsDouble()) == 0xF;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Any(PacketInt64AvxMask value)
    {
        return Avx.MoveMask(value._value.AsDouble()) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool None(PacketInt64AvxMask value)
    {
        return Avx.MoveMask(value._value.AsDouble()) == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask AndNot(PacketInt64AvxMask left, PacketInt64AvxMask right)
    {
        return new(Avx2.AndNot(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask Select(PacketInt64AvxMask mask, PacketInt64AvxMask ifTrue, PacketInt64AvxMask ifFalse)
    {
        return new(Avx2.BlendVariable(ifFalse._value, ifTrue._value, mask._value));
    }

    public static unsafe PacketInt64AvxMask Load(ReadOnlySpan<long> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (long* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketInt64AvxMask Load(ReadOnlySpan<bool> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (bool* ptr = values)
        {
            return LoadBoolUnsafe(ptr);
        }
    }

    public static unsafe PacketInt64AvxMask LoadAligned(ReadOnlySpan<long> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (long* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketInt64AvxMask value, Span<long> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (long* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void Store(PacketInt64AvxMask value, Span<bool> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (bool* ptr = destination)
        {
            StoreBoolUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketInt64AvxMask value, Span<long> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (long* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketInt64AvxMask LoadUnsafe(long* source)
    {
        return new(Avx.LoadVector256(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketInt64AvxMask LoadBoolUnsafe(bool* source)
    {
        int packed = Unsafe.ReadUnaligned<int>(source);
        Vector128<sbyte> byteValues = Vector128.CreateScalarUnsafe(packed).AsSByte();
        Vector128<sbyte> byteMask = Sse2.CompareGreaterThan(byteValues, Vector128<sbyte>.Zero);

        return new(Avx2.ConvertToVector256Int64(byteMask));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketInt64AvxMask LoadAlignedUnsafe(long* source)
    {
        return new(Avx.LoadAlignedVector256(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketInt64AvxMask value, long* destination)
    {
        Avx.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreBoolUnsafe(PacketInt64AvxMask value, bool* destination)
    {
        int mask = Avx.MoveMask(value._value.AsDouble());
        for (int i = 0; i < LaneCount; i++)
        {
            destination[i] = ((mask >> i) & 1) != 0;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketInt64AvxMask value, long* destination)
    {
        Avx.StoreAligned(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask And(PacketInt64AvxMask left, PacketInt64AvxMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask Or(PacketInt64AvxMask left, PacketInt64AvxMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask Xor(PacketInt64AvxMask left, PacketInt64AvxMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask Not(PacketInt64AvxMask value) => ~value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask operator &(PacketInt64AvxMask left, PacketInt64AvxMask right)
    {
        return new(Avx2.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask operator |(PacketInt64AvxMask left, PacketInt64AvxMask right)
    {
        return new(Avx2.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask operator ^(PacketInt64AvxMask left, PacketInt64AvxMask right)
    {
        return new(Avx2.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask operator ~(PacketInt64AvxMask value)
    {
        return new(Avx2.Xor(value._value, Vector256.Create(-1L)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask operator ==(PacketInt64AvxMask left, PacketInt64AvxMask right)
    {
        return ~(left ^ right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask operator !=(PacketInt64AvxMask left, PacketInt64AvxMask right)
    {
        return left ^ right;
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketInt64AvxMask other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}
