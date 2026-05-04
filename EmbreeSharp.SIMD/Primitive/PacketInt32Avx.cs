using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace EmbreeSharp.SIMD;

public readonly struct PacketInt32Avx :
    ISimdInteger<PacketInt32Avx, int, PacketInt32AvxMask>
{
    internal readonly Vector256<int> _value;

    private PacketInt32Avx(Vector256<int> value)
    {
        _value = value;
    }

    public static int LaneCount => 8;

    public static PacketInt32Avx AdditiveIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(0);
    }

    public static PacketInt32Avx MultiplicativeIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Avx Broadcast(int value)
    {
        return new(Vector256.Create(value));
    }

    public static unsafe PacketInt32Avx Load(ReadOnlySpan<int> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (int* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketInt32Avx LoadAligned(ReadOnlySpan<int> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (int* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketInt32Avx value, Span<int> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (int* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketInt32Avx value, Span<int> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (int* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketInt32Avx LoadUnsafe(int* source)
    {
        return new(Avx.LoadVector256(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketInt32Avx LoadAlignedUnsafe(int* source)
    {
        return new(Avx.LoadAlignedVector256(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketInt32Avx value, int* destination)
    {
        Avx.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketInt32Avx value, int* destination)
    {
        Avx.StoreAligned(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Avx Select(PacketInt32AvxMask mask, PacketInt32Avx ifTrue, PacketInt32Avx ifFalse)
    {
        return new(Avx2.BlendVariable(ifFalse._value, ifTrue._value, mask._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Avx Abs(PacketInt32Avx value)
    {
        return new(Avx2.Abs(value._value).AsInt32());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Avx Min(PacketInt32Avx left, PacketInt32Avx right)
    {
        return new(Avx2.Min(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Avx Max(PacketInt32Avx left, PacketInt32Avx right)
    {
        return new(Avx2.Max(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Avx Clamp(PacketInt32Avx value, PacketInt32Avx min, PacketInt32Avx max)
    {
        return Min(Max(value, min), max);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Avx operator +(PacketInt32Avx value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Avx operator +(PacketInt32Avx left, PacketInt32Avx right)
    {
        return new(Avx2.Add(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Avx operator -(PacketInt32Avx left, PacketInt32Avx right)
    {
        return new(Avx2.Subtract(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Avx operator *(PacketInt32Avx left, PacketInt32Avx right)
    {
        return new(Avx2.MultiplyLow(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Avx operator -(PacketInt32Avx value)
    {
        return new(Avx2.Subtract(Vector256<int>.Zero, value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Avx operator &(PacketInt32Avx left, PacketInt32Avx right)
    {
        return new(Avx2.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Avx operator |(PacketInt32Avx left, PacketInt32Avx right)
    {
        return new(Avx2.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Avx operator ^(PacketInt32Avx left, PacketInt32Avx right)
    {
        return new(Avx2.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Avx operator ~(PacketInt32Avx value)
    {
        return new(Avx2.Xor(value._value, Vector256.Create(-1)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask operator ==(PacketInt32Avx left, PacketInt32Avx right)
    {
        return new(Avx2.CompareEqual(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask operator !=(PacketInt32Avx left, PacketInt32Avx right)
    {
        return ~(left == right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask operator <(PacketInt32Avx left, PacketInt32Avx right)
    {
        return new(Avx2.CompareGreaterThan(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask operator >(PacketInt32Avx left, PacketInt32Avx right)
    {
        return new(Avx2.CompareGreaterThan(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask operator <=(PacketInt32Avx left, PacketInt32Avx right)
    {
        return ~(left > right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask operator >=(PacketInt32Avx left, PacketInt32Avx right)
    {
        return ~(left < right);
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketInt32Avx other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}

public readonly struct PacketInt32AvxMask : ISimdPacketMask<PacketInt32AvxMask, int>
{
    internal readonly Vector256<int> _value;

    internal PacketInt32AvxMask(Vector256<int> value)
    {
        _value = value;
    }

    public static int LaneCount => 8;

    public static PacketInt32AvxMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector256.Create(-1));
    }

    public static PacketInt32AvxMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector256<int>.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask Broadcast(bool value) => value ? True : False;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool All(PacketInt32AvxMask value)
    {
        return Avx.MoveMask(value._value.AsSingle()) == 0xFF;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Any(PacketInt32AvxMask value)
    {
        return Avx.MoveMask(value._value.AsSingle()) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool None(PacketInt32AvxMask value)
    {
        return Avx.MoveMask(value._value.AsSingle()) == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask AndNot(PacketInt32AvxMask left, PacketInt32AvxMask right)
    {
        return new(Avx2.AndNot(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask Select(PacketInt32AvxMask mask, PacketInt32AvxMask ifTrue, PacketInt32AvxMask ifFalse)
    {
        return new(Avx2.BlendVariable(ifFalse._value, ifTrue._value, mask._value));
    }

    public static unsafe PacketInt32AvxMask Load(ReadOnlySpan<int> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (int* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketInt32AvxMask Load(ReadOnlySpan<bool> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (bool* ptr = values)
        {
            return LoadBoolUnsafe(ptr);
        }
    }

    public static unsafe PacketInt32AvxMask LoadAligned(ReadOnlySpan<int> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (int* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketInt32AvxMask value, Span<int> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (int* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void Store(PacketInt32AvxMask value, Span<bool> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (bool* ptr = destination)
        {
            StoreBoolUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketInt32AvxMask value, Span<int> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (int* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketInt32AvxMask LoadUnsafe(int* source)
    {
        return new(Avx.LoadVector256(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketInt32AvxMask LoadBoolUnsafe(bool* source)
    {
        long packed = Unsafe.ReadUnaligned<long>(source);
        Vector128<sbyte> byteValues = Vector128.CreateScalarUnsafe(packed).AsSByte();
        Vector128<sbyte> byteMask = Sse2.CompareGreaterThan(byteValues, Vector128<sbyte>.Zero);

        return new(Avx2.ConvertToVector256Int32(byteMask));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketInt32AvxMask LoadAlignedUnsafe(int* source)
    {
        return new(Avx.LoadAlignedVector256(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketInt32AvxMask value, int* destination)
    {
        Avx.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreBoolUnsafe(PacketInt32AvxMask value, bool* destination)
    {
        int mask = Avx.MoveMask(value._value.AsSingle());
        for (int i = 0; i < LaneCount; i++)
        {
            destination[i] = ((mask >> i) & 1) != 0;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketInt32AvxMask value, int* destination)
    {
        Avx.StoreAligned(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask And(PacketInt32AvxMask left, PacketInt32AvxMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask Or(PacketInt32AvxMask left, PacketInt32AvxMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask Xor(PacketInt32AvxMask left, PacketInt32AvxMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask Not(PacketInt32AvxMask value) => ~value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask operator &(PacketInt32AvxMask left, PacketInt32AvxMask right)
    {
        return new(Avx2.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask operator |(PacketInt32AvxMask left, PacketInt32AvxMask right)
    {
        return new(Avx2.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask operator ^(PacketInt32AvxMask left, PacketInt32AvxMask right)
    {
        return new(Avx2.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask operator ~(PacketInt32AvxMask value)
    {
        return new(Avx2.Xor(value._value, Vector256.Create(-1)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask operator ==(PacketInt32AvxMask left, PacketInt32AvxMask right)
    {
        return ~(left ^ right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask operator !=(PacketInt32AvxMask left, PacketInt32AvxMask right)
    {
        return left ^ right;
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketInt32AvxMask other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}
