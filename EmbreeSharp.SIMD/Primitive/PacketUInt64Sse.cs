using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace EmbreeSharp.SIMD;

public readonly struct PacketUInt64Sse :
    ISimdInteger<PacketUInt64Sse, ulong, PacketUInt64SseMask>
{
    internal readonly Vector128<ulong> _value;

    private PacketUInt64Sse(Vector128<ulong> value)
    {
        _value = value;
    }

    public static int LaneCount => 2;

    public static PacketUInt64Sse AdditiveIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(0);
    }

    public static PacketUInt64Sse MultiplicativeIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Sse Broadcast(ulong value)
    {
        return new(Vector128.Create(value));
    }

    public static unsafe PacketUInt64Sse Load(ReadOnlySpan<ulong> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (ulong* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketUInt64Sse LoadAligned(ReadOnlySpan<ulong> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (ulong* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketUInt64Sse value, Span<ulong> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (ulong* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketUInt64Sse value, Span<ulong> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (ulong* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt64Sse LoadUnsafe(ulong* source)
    {
        return new(Sse2.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt64Sse LoadAlignedUnsafe(ulong* source)
    {
        return new(Sse2.LoadAlignedVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketUInt64Sse value, ulong* destination)
    {
        Sse2.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketUInt64Sse value, ulong* destination)
    {
        Sse2.StoreAligned(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Sse Select(PacketUInt64SseMask mask, PacketUInt64Sse ifTrue, PacketUInt64Sse ifFalse)
    {
        return new(Sse41.BlendVariable(ifFalse._value, ifTrue._value, mask._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Sse Abs(PacketUInt64Sse value)
    {
        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Sse Min(PacketUInt64Sse left, PacketUInt64Sse right)
    {
        return Select(left < right, left, right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Sse Max(PacketUInt64Sse left, PacketUInt64Sse right)
    {
        return Select(left > right, left, right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Sse Clamp(PacketUInt64Sse value, PacketUInt64Sse min, PacketUInt64Sse max)
    {
        return Min(Max(value, min), max);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Sse operator +(PacketUInt64Sse value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Sse operator +(PacketUInt64Sse left, PacketUInt64Sse right)
    {
        return new(Sse2.Add(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Sse operator -(PacketUInt64Sse left, PacketUInt64Sse right)
    {
        return new(Sse2.Subtract(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Sse operator *(PacketUInt64Sse left, PacketUInt64Sse right)
    {
        Vector128<ulong> h0 = Sse2.ShiftRightLogical(left._value, 32);
        Vector128<ulong> h1 = Sse2.ShiftRightLogical(right._value, 32);
        Vector128<ulong> low = Sse2.Multiply(left._value.AsUInt32(), right._value.AsUInt32());
        Vector128<ulong> mix0 = Sse2.Multiply(left._value.AsUInt32(), h1.AsUInt32());
        Vector128<ulong> mix1 = Sse2.Multiply(h0.AsUInt32(), right._value.AsUInt32());
        Vector128<ulong> mix = Sse2.Add(mix0, mix1);
        return new(Sse2.Add(Sse2.ShiftLeftLogical(mix, 32), low));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Sse operator -(PacketUInt64Sse value)
    {
        return new(Sse2.Subtract(Vector128<ulong>.Zero, value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Sse operator &(PacketUInt64Sse left, PacketUInt64Sse right)
    {
        return new(Sse2.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Sse operator |(PacketUInt64Sse left, PacketUInt64Sse right)
    {
        return new(Sse2.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Sse operator ^(PacketUInt64Sse left, PacketUInt64Sse right)
    {
        return new(Sse2.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64Sse operator ~(PacketUInt64Sse value)
    {
        return new(Sse2.Xor(value._value, Vector128.Create(ulong.MaxValue)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask operator ==(PacketUInt64Sse left, PacketUInt64Sse right)
    {
        return new(Sse41.CompareEqual(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask operator !=(PacketUInt64Sse left, PacketUInt64Sse right)
    {
        return ~(left == right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask operator <(PacketUInt64Sse left, PacketUInt64Sse right)
    {
        Vector128<ulong> offset = Vector128.Create(0x8000000000000000ul);
        return new(Sse42.CompareGreaterThan(
            Sse2.Subtract(right._value, offset).AsInt64(),
            Sse2.Subtract(left._value, offset).AsInt64()).AsUInt64());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask operator >(PacketUInt64Sse left, PacketUInt64Sse right)
    {
        Vector128<ulong> offset = Vector128.Create(0x8000000000000000ul);
        return new(Sse42.CompareGreaterThan(
            Sse2.Subtract(left._value, offset).AsInt64(),
            Sse2.Subtract(right._value, offset).AsInt64()).AsUInt64());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask operator <=(PacketUInt64Sse left, PacketUInt64Sse right)
    {
        return ~(left > right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask operator >=(PacketUInt64Sse left, PacketUInt64Sse right)
    {
        return ~(left < right);
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketUInt64Sse other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}

public readonly struct PacketUInt64SseMask : ISimdPacketMask<PacketUInt64SseMask, ulong>
{
    internal readonly Vector128<ulong> _value;

    internal PacketUInt64SseMask(Vector128<ulong> value)
    {
        _value = value;
    }

    public static int LaneCount => 2;

    public static PacketUInt64SseMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector128.Create(ulong.MaxValue));
    }

    public static PacketUInt64SseMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector128<ulong>.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask Broadcast(bool value) => value ? True : False;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool All(PacketUInt64SseMask value)
    {
        return Sse2.MoveMask(value._value.AsDouble()) == 0x3;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Any(PacketUInt64SseMask value)
    {
        return Sse2.MoveMask(value._value.AsDouble()) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool None(PacketUInt64SseMask value)
    {
        return Sse2.MoveMask(value._value.AsDouble()) == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask AndNot(PacketUInt64SseMask left, PacketUInt64SseMask right)
    {
        return new(Sse2.AndNot(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask Select(PacketUInt64SseMask mask, PacketUInt64SseMask ifTrue, PacketUInt64SseMask ifFalse)
    {
        return new(Sse41.BlendVariable(ifFalse._value, ifTrue._value, mask._value));
    }

    public static unsafe PacketUInt64SseMask Load(ReadOnlySpan<ulong> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (ulong* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketUInt64SseMask Load(ReadOnlySpan<bool> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (bool* ptr = values)
        {
            return LoadBoolUnsafe(ptr);
        }
    }

    public static unsafe PacketUInt64SseMask LoadAligned(ReadOnlySpan<ulong> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (ulong* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketUInt64SseMask value, Span<ulong> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (ulong* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void Store(PacketUInt64SseMask value, Span<bool> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (bool* ptr = destination)
        {
            StoreBoolUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketUInt64SseMask value, Span<ulong> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (ulong* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt64SseMask LoadUnsafe(ulong* source)
    {
        return new(Sse2.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt64SseMask LoadBoolUnsafe(bool* source)
    {
        short packed = Unsafe.ReadUnaligned<short>(source);
        Vector128<sbyte> byteValues = Vector128.CreateScalarUnsafe(packed).AsSByte();
        Vector128<sbyte> byteMask = Sse2.CompareGreaterThan(byteValues, Vector128<sbyte>.Zero);

        return new(Sse41.ConvertToVector128Int64(byteMask).AsUInt64());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt64SseMask LoadAlignedUnsafe(ulong* source)
    {
        return new(Sse2.LoadAlignedVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketUInt64SseMask value, ulong* destination)
    {
        Sse2.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreBoolUnsafe(PacketUInt64SseMask value, bool* destination)
    {
        int mask = Sse2.MoveMask(value._value.AsDouble());
        for (int i = 0; i < LaneCount; i++)
        {
            destination[i] = ((mask >> i) & 1) != 0;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketUInt64SseMask value, ulong* destination)
    {
        Sse2.StoreAligned(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask And(PacketUInt64SseMask left, PacketUInt64SseMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask Or(PacketUInt64SseMask left, PacketUInt64SseMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask Xor(PacketUInt64SseMask left, PacketUInt64SseMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask Not(PacketUInt64SseMask value) => ~value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask operator &(PacketUInt64SseMask left, PacketUInt64SseMask right)
    {
        return new(Sse2.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask operator |(PacketUInt64SseMask left, PacketUInt64SseMask right)
    {
        return new(Sse2.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask operator ^(PacketUInt64SseMask left, PacketUInt64SseMask right)
    {
        return new(Sse2.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask operator ~(PacketUInt64SseMask value)
    {
        return new(Sse2.Xor(value._value, Vector128.Create(ulong.MaxValue)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask operator ==(PacketUInt64SseMask left, PacketUInt64SseMask right)
    {
        return ~(left ^ right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask operator !=(PacketUInt64SseMask left, PacketUInt64SseMask right)
    {
        return left ^ right;
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketUInt64SseMask other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}
