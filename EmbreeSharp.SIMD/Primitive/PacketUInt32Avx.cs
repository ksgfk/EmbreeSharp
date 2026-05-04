using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace EmbreeSharp.SIMD;

public readonly struct PacketUInt32Avx :
    ISimdInteger<PacketUInt32Avx, uint, PacketUInt32AvxMask>
{
    internal readonly Vector256<uint> _value;

    private PacketUInt32Avx(Vector256<uint> value)
    {
        _value = value;
    }

    public static int LaneCount => 8;

    public static PacketUInt32Avx AdditiveIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(0);
    }

    public static PacketUInt32Avx MultiplicativeIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Avx Broadcast(uint value)
    {
        return new(Vector256.Create(value));
    }

    public static unsafe PacketUInt32Avx Load(ReadOnlySpan<uint> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (uint* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketUInt32Avx LoadAligned(ReadOnlySpan<uint> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (uint* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketUInt32Avx value, Span<uint> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (uint* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketUInt32Avx value, Span<uint> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (uint* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt32Avx LoadUnsafe(uint* source)
    {
        return new(Avx.LoadVector256(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt32Avx LoadAlignedUnsafe(uint* source)
    {
        return new(Avx.LoadAlignedVector256(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketUInt32Avx value, uint* destination)
    {
        Avx.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketUInt32Avx value, uint* destination)
    {
        Avx.StoreAligned(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Avx Select(PacketUInt32AvxMask mask, PacketUInt32Avx ifTrue, PacketUInt32Avx ifFalse)
    {
        return new(Avx2.BlendVariable(ifFalse._value, ifTrue._value, mask._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Avx Abs(PacketUInt32Avx value)
    {
        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Avx Min(PacketUInt32Avx left, PacketUInt32Avx right)
    {
        return new(Avx2.Min(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Avx Max(PacketUInt32Avx left, PacketUInt32Avx right)
    {
        return new(Avx2.Max(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Avx Clamp(PacketUInt32Avx value, PacketUInt32Avx min, PacketUInt32Avx max)
    {
        return Min(Max(value, min), max);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Avx operator +(PacketUInt32Avx value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Avx operator +(PacketUInt32Avx left, PacketUInt32Avx right)
    {
        return new(Avx2.Add(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Avx operator -(PacketUInt32Avx left, PacketUInt32Avx right)
    {
        return new(Avx2.Subtract(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Avx operator *(PacketUInt32Avx left, PacketUInt32Avx right)
    {
        return new(Avx2.MultiplyLow(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Avx operator -(PacketUInt32Avx value)
    {
        return new(Avx2.Subtract(Vector256<uint>.Zero, value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Avx operator &(PacketUInt32Avx left, PacketUInt32Avx right)
    {
        return new(Avx2.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Avx operator |(PacketUInt32Avx left, PacketUInt32Avx right)
    {
        return new(Avx2.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Avx operator ^(PacketUInt32Avx left, PacketUInt32Avx right)
    {
        return new(Avx2.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Avx operator ~(PacketUInt32Avx value)
    {
        return new(Avx2.Xor(value._value, Vector256.Create(uint.MaxValue)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask operator ==(PacketUInt32Avx left, PacketUInt32Avx right)
    {
        return new(Avx2.CompareEqual(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask operator !=(PacketUInt32Avx left, PacketUInt32Avx right)
    {
        return ~(left == right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask operator <(PacketUInt32Avx left, PacketUInt32Avx right)
    {
        Vector256<uint> offset = Vector256.Create(0x80000000u);
        return new(Avx2.CompareGreaterThan(
            Avx2.Subtract(right._value, offset).AsInt32(),
            Avx2.Subtract(left._value, offset).AsInt32()).AsUInt32());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask operator >(PacketUInt32Avx left, PacketUInt32Avx right)
    {
        Vector256<uint> offset = Vector256.Create(0x80000000u);
        return new(Avx2.CompareGreaterThan(
            Avx2.Subtract(left._value, offset).AsInt32(),
            Avx2.Subtract(right._value, offset).AsInt32()).AsUInt32());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask operator <=(PacketUInt32Avx left, PacketUInt32Avx right)
    {
        return ~(left > right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask operator >=(PacketUInt32Avx left, PacketUInt32Avx right)
    {
        return ~(left < right);
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketUInt32Avx other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}

public readonly struct PacketUInt32AvxMask : ISimdPacketMask<PacketUInt32AvxMask, uint>
{
    internal readonly Vector256<uint> _value;

    internal PacketUInt32AvxMask(Vector256<uint> value)
    {
        _value = value;
    }

    public static int LaneCount => 8;

    public static PacketUInt32AvxMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector256.Create(uint.MaxValue));
    }

    public static PacketUInt32AvxMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector256<uint>.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask Broadcast(bool value) => value ? True : False;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool All(PacketUInt32AvxMask value)
    {
        return Avx.MoveMask(value._value.AsSingle()) == 0xFF;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Any(PacketUInt32AvxMask value)
    {
        return Avx.MoveMask(value._value.AsSingle()) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool None(PacketUInt32AvxMask value)
    {
        return Avx.MoveMask(value._value.AsSingle()) == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask AndNot(PacketUInt32AvxMask left, PacketUInt32AvxMask right)
    {
        return new(Avx2.AndNot(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask Select(PacketUInt32AvxMask mask, PacketUInt32AvxMask ifTrue, PacketUInt32AvxMask ifFalse)
    {
        return new(Avx2.BlendVariable(ifFalse._value, ifTrue._value, mask._value));
    }

    public static unsafe PacketUInt32AvxMask Load(ReadOnlySpan<uint> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (uint* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketUInt32AvxMask Load(ReadOnlySpan<bool> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (bool* ptr = values)
        {
            return LoadBoolUnsafe(ptr);
        }
    }

    public static unsafe PacketUInt32AvxMask LoadAligned(ReadOnlySpan<uint> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (uint* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketUInt32AvxMask value, Span<uint> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (uint* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void Store(PacketUInt32AvxMask value, Span<bool> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (bool* ptr = destination)
        {
            StoreBoolUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketUInt32AvxMask value, Span<uint> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (uint* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt32AvxMask LoadUnsafe(uint* source)
    {
        return new(Avx.LoadVector256(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt32AvxMask LoadBoolUnsafe(bool* source)
    {
        long packed = Unsafe.ReadUnaligned<long>(source);
        Vector128<sbyte> byteValues = Vector128.CreateScalarUnsafe(packed).AsSByte();
        Vector128<sbyte> byteMask = Sse2.CompareGreaterThan(byteValues, Vector128<sbyte>.Zero);

        return new(Avx2.ConvertToVector256Int32(byteMask).AsUInt32());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt32AvxMask LoadAlignedUnsafe(uint* source)
    {
        return new(Avx.LoadAlignedVector256(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketUInt32AvxMask value, uint* destination)
    {
        Avx.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreBoolUnsafe(PacketUInt32AvxMask value, bool* destination)
    {
        int mask = Avx.MoveMask(value._value.AsSingle());
        for (int i = 0; i < LaneCount; i++)
        {
            destination[i] = ((mask >> i) & 1) != 0;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketUInt32AvxMask value, uint* destination)
    {
        Avx.StoreAligned(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask And(PacketUInt32AvxMask left, PacketUInt32AvxMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask Or(PacketUInt32AvxMask left, PacketUInt32AvxMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask Xor(PacketUInt32AvxMask left, PacketUInt32AvxMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask Not(PacketUInt32AvxMask value) => ~value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask operator &(PacketUInt32AvxMask left, PacketUInt32AvxMask right)
    {
        return new(Avx2.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask operator |(PacketUInt32AvxMask left, PacketUInt32AvxMask right)
    {
        return new(Avx2.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask operator ^(PacketUInt32AvxMask left, PacketUInt32AvxMask right)
    {
        return new(Avx2.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask operator ~(PacketUInt32AvxMask value)
    {
        return new(Avx2.Xor(value._value, Vector256.Create(uint.MaxValue)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask operator ==(PacketUInt32AvxMask left, PacketUInt32AvxMask right)
    {
        return ~(left ^ right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask operator !=(PacketUInt32AvxMask left, PacketUInt32AvxMask right)
    {
        return left ^ right;
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketUInt32AvxMask other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}
