using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace EmbreeSharp.SIMD;

public readonly struct PacketInt64Sse :
    ISimdInteger<PacketInt64Sse, long, PacketInt64SseMask>
{
    internal readonly Vector128<long> _value;

    private PacketInt64Sse(Vector128<long> value)
    {
        _value = value;
    }

    public static int LaneCount => 2;

    public static PacketInt64Sse AdditiveIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(0);
    }

    public static PacketInt64Sse MultiplicativeIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Sse Broadcast(long value)
    {
        return new(Vector128.Create(value));
    }

    public static unsafe PacketInt64Sse Load(ReadOnlySpan<long> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (long* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketInt64Sse LoadAligned(ReadOnlySpan<long> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (long* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketInt64Sse value, Span<long> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (long* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketInt64Sse value, Span<long> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (long* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketInt64Sse LoadUnsafe(long* source)
    {
        return new(Sse2.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketInt64Sse LoadAlignedUnsafe(long* source)
    {
        return new(Sse2.LoadAlignedVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketInt64Sse value, long* destination)
    {
        Sse2.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketInt64Sse value, long* destination)
    {
        Sse2.StoreAligned(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Sse Select(PacketInt64SseMask mask, PacketInt64Sse ifTrue, PacketInt64Sse ifFalse)
    {
        return new(Sse41.BlendVariable(ifFalse._value, ifTrue._value, mask._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Sse Abs(PacketInt64Sse value)
    {
        return Select(value < Broadcast(0), -value, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Sse Min(PacketInt64Sse left, PacketInt64Sse right)
    {
        return Select(left < right, left, right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Sse Max(PacketInt64Sse left, PacketInt64Sse right)
    {
        return Select(left > right, left, right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Sse Clamp(PacketInt64Sse value, PacketInt64Sse min, PacketInt64Sse max)
    {
        return Min(Max(value, min), max);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Sse operator +(PacketInt64Sse value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Sse operator +(PacketInt64Sse left, PacketInt64Sse right)
    {
        return new(Sse2.Add(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Sse operator -(PacketInt64Sse left, PacketInt64Sse right)
    {
        return new(Sse2.Subtract(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Sse operator *(PacketInt64Sse left, PacketInt64Sse right)
    {
        Vector128<ulong> leftValue = left._value.AsUInt64();
        Vector128<ulong> rightValue = right._value.AsUInt64();
        Vector128<ulong> h0 = Sse2.ShiftRightLogical(leftValue, 32);
        Vector128<ulong> h1 = Sse2.ShiftRightLogical(rightValue, 32);
        Vector128<ulong> low = Sse2.Multiply(leftValue.AsUInt32(), rightValue.AsUInt32());
        Vector128<ulong> mix0 = Sse2.Multiply(leftValue.AsUInt32(), h1.AsUInt32());
        Vector128<ulong> mix1 = Sse2.Multiply(h0.AsUInt32(), rightValue.AsUInt32());
        Vector128<ulong> mix = Sse2.Add(mix0, mix1);
        return new(Sse2.Add(Sse2.ShiftLeftLogical(mix, 32), low).AsInt64());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Sse operator -(PacketInt64Sse value)
    {
        return new(Sse2.Subtract(Vector128<long>.Zero, value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Sse operator &(PacketInt64Sse left, PacketInt64Sse right)
    {
        return new(Sse2.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Sse operator |(PacketInt64Sse left, PacketInt64Sse right)
    {
        return new(Sse2.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Sse operator ^(PacketInt64Sse left, PacketInt64Sse right)
    {
        return new(Sse2.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64Sse operator ~(PacketInt64Sse value)
    {
        return new(Sse2.Xor(value._value, Vector128.Create(-1L)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask operator ==(PacketInt64Sse left, PacketInt64Sse right)
    {
        return new(Sse41.CompareEqual(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask operator !=(PacketInt64Sse left, PacketInt64Sse right)
    {
        return ~(left == right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask operator <(PacketInt64Sse left, PacketInt64Sse right)
    {
        return new(Sse42.CompareGreaterThan(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask operator >(PacketInt64Sse left, PacketInt64Sse right)
    {
        return new(Sse42.CompareGreaterThan(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask operator <=(PacketInt64Sse left, PacketInt64Sse right)
    {
        return ~(left > right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask operator >=(PacketInt64Sse left, PacketInt64Sse right)
    {
        return ~(left < right);
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketInt64Sse other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}

public readonly struct PacketInt64SseMask : ISimdPacketMask<PacketInt64SseMask, long>
{
    internal readonly Vector128<long> _value;

    internal PacketInt64SseMask(Vector128<long> value)
    {
        _value = value;
    }

    public static int LaneCount => 2;

    public static PacketInt64SseMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector128.Create(-1L));
    }

    public static PacketInt64SseMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector128<long>.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask Broadcast(bool value) => value ? True : False;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool All(PacketInt64SseMask value)
    {
        return Sse2.MoveMask(value._value.AsDouble()) == 0x3;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Any(PacketInt64SseMask value)
    {
        return Sse2.MoveMask(value._value.AsDouble()) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool None(PacketInt64SseMask value)
    {
        return Sse2.MoveMask(value._value.AsDouble()) == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask AndNot(PacketInt64SseMask left, PacketInt64SseMask right)
    {
        return new(Sse2.AndNot(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask Select(PacketInt64SseMask mask, PacketInt64SseMask ifTrue, PacketInt64SseMask ifFalse)
    {
        return new(Sse41.BlendVariable(ifFalse._value, ifTrue._value, mask._value));
    }

    public static unsafe PacketInt64SseMask Load(ReadOnlySpan<long> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (long* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketInt64SseMask Load(ReadOnlySpan<bool> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (bool* ptr = values)
        {
            return LoadBoolUnsafe(ptr);
        }
    }

    public static unsafe PacketInt64SseMask LoadAligned(ReadOnlySpan<long> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (long* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketInt64SseMask value, Span<long> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (long* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void Store(PacketInt64SseMask value, Span<bool> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (bool* ptr = destination)
        {
            StoreBoolUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketInt64SseMask value, Span<long> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (long* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketInt64SseMask LoadUnsafe(long* source)
    {
        return new(Sse2.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketInt64SseMask LoadBoolUnsafe(bool* source)
    {
        short packed = Unsafe.ReadUnaligned<short>(source);
        Vector128<sbyte> byteValues = Vector128.CreateScalarUnsafe(packed).AsSByte();
        Vector128<sbyte> byteMask = Sse2.CompareGreaterThan(byteValues, Vector128<sbyte>.Zero);

        return new(Sse41.ConvertToVector128Int64(byteMask));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketInt64SseMask LoadAlignedUnsafe(long* source)
    {
        return new(Sse2.LoadAlignedVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketInt64SseMask value, long* destination)
    {
        Sse2.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreBoolUnsafe(PacketInt64SseMask value, bool* destination)
    {
        int mask = Sse2.MoveMask(value._value.AsDouble());
        for (int i = 0; i < LaneCount; i++)
        {
            destination[i] = ((mask >> i) & 1) != 0;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketInt64SseMask value, long* destination)
    {
        Sse2.StoreAligned(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask And(PacketInt64SseMask left, PacketInt64SseMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask Or(PacketInt64SseMask left, PacketInt64SseMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask Xor(PacketInt64SseMask left, PacketInt64SseMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask Not(PacketInt64SseMask value) => ~value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask operator &(PacketInt64SseMask left, PacketInt64SseMask right)
    {
        return new(Sse2.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask operator |(PacketInt64SseMask left, PacketInt64SseMask right)
    {
        return new(Sse2.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask operator ^(PacketInt64SseMask left, PacketInt64SseMask right)
    {
        return new(Sse2.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask operator ~(PacketInt64SseMask value)
    {
        return new(Sse2.Xor(value._value, Vector128.Create(-1L)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask operator ==(PacketInt64SseMask left, PacketInt64SseMask right)
    {
        return ~(left ^ right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask operator !=(PacketInt64SseMask left, PacketInt64SseMask right)
    {
        return left ^ right;
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketInt64SseMask other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}
