using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace EmbreeSharp.SIMD;

public readonly struct PacketInt32Sse :
    ISimdInteger<PacketInt32Sse, int, PacketInt32SseMask>
{
    internal readonly Vector128<int> _value;

    private PacketInt32Sse(Vector128<int> value)
    {
        _value = value;
    }

    public static int LaneCount => 4;

    public static PacketInt32Sse AdditiveIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(0);
    }

    public static PacketInt32Sse MultiplicativeIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Sse Broadcast(int value)
    {
        return new(Vector128.Create(value));
    }

    public static unsafe PacketInt32Sse Load(ReadOnlySpan<int> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (int* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketInt32Sse LoadAligned(ReadOnlySpan<int> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (int* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketInt32Sse value, Span<int> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (int* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketInt32Sse value, Span<int> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (int* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketInt32Sse LoadUnsafe(int* source)
    {
        return new(Sse2.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketInt32Sse LoadAlignedUnsafe(int* source)
    {
        return new(Sse2.LoadAlignedVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketInt32Sse value, int* destination)
    {
        Sse2.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketInt32Sse value, int* destination)
    {
        Sse2.StoreAligned(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Sse Select(PacketInt32SseMask mask, PacketInt32Sse ifTrue, PacketInt32Sse ifFalse)
    {
        return new(Sse41.BlendVariable(ifFalse._value, ifTrue._value, mask._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Sse Abs(PacketInt32Sse value)
    {
        return new(Ssse3.Abs(value._value).AsInt32());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Sse Min(PacketInt32Sse left, PacketInt32Sse right)
    {
        return new(Sse41.Min(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Sse Max(PacketInt32Sse left, PacketInt32Sse right)
    {
        return new(Sse41.Max(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Sse Clamp(PacketInt32Sse value, PacketInt32Sse min, PacketInt32Sse max)
    {
        return Min(Max(value, min), max);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Sse operator +(PacketInt32Sse value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Sse operator +(PacketInt32Sse left, PacketInt32Sse right)
    {
        return new(Sse2.Add(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Sse operator -(PacketInt32Sse left, PacketInt32Sse right)
    {
        return new(Sse2.Subtract(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Sse operator *(PacketInt32Sse left, PacketInt32Sse right)
    {
        return new(Sse41.MultiplyLow(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Sse operator -(PacketInt32Sse value)
    {
        return new(Sse2.Subtract(Vector128<int>.Zero, value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Sse operator &(PacketInt32Sse left, PacketInt32Sse right)
    {
        return new(Sse2.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Sse operator |(PacketInt32Sse left, PacketInt32Sse right)
    {
        return new(Sse2.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Sse operator ^(PacketInt32Sse left, PacketInt32Sse right)
    {
        return new(Sse2.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32Sse operator ~(PacketInt32Sse value)
    {
        return new(Sse2.Xor(value._value, Vector128.Create(-1)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask operator ==(PacketInt32Sse left, PacketInt32Sse right)
    {
        return new(Sse2.CompareEqual(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask operator !=(PacketInt32Sse left, PacketInt32Sse right)
    {
        return ~(left == right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask operator <(PacketInt32Sse left, PacketInt32Sse right)
    {
        return new(Sse2.CompareGreaterThan(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask operator >(PacketInt32Sse left, PacketInt32Sse right)
    {
        return new(Sse2.CompareGreaterThan(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask operator <=(PacketInt32Sse left, PacketInt32Sse right)
    {
        return ~(left > right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask operator >=(PacketInt32Sse left, PacketInt32Sse right)
    {
        return ~(left < right);
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketInt32Sse other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}

public readonly struct PacketInt32SseMask : ISimdPacketMask<PacketInt32SseMask, int>
{
    internal readonly Vector128<int> _value;

    internal PacketInt32SseMask(Vector128<int> value)
    {
        _value = value;
    }

    public static int LaneCount => 4;

    public static PacketInt32SseMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector128.Create(-1));
    }

    public static PacketInt32SseMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector128<int>.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask Broadcast(bool value) => value ? True : False;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool All(PacketInt32SseMask value)
    {
        return Sse.MoveMask(value._value.AsSingle()) == 0xF;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Any(PacketInt32SseMask value)
    {
        return Sse.MoveMask(value._value.AsSingle()) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool None(PacketInt32SseMask value)
    {
        return Sse.MoveMask(value._value.AsSingle()) == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask AndNot(PacketInt32SseMask left, PacketInt32SseMask right)
    {
        return new(Sse2.AndNot(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask Select(PacketInt32SseMask mask, PacketInt32SseMask ifTrue, PacketInt32SseMask ifFalse)
    {
        return new(Sse41.BlendVariable(ifFalse._value, ifTrue._value, mask._value));
    }

    public static unsafe PacketInt32SseMask Load(ReadOnlySpan<int> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (int* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketInt32SseMask Load(ReadOnlySpan<bool> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (bool* ptr = values)
        {
            return LoadBoolUnsafe(ptr);
        }
    }

    public static unsafe PacketInt32SseMask LoadAligned(ReadOnlySpan<int> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (int* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketInt32SseMask value, Span<int> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (int* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void Store(PacketInt32SseMask value, Span<bool> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (bool* ptr = destination)
        {
            StoreBoolUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketInt32SseMask value, Span<int> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (int* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketInt32SseMask LoadUnsafe(int* source)
    {
        return new(Sse2.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketInt32SseMask LoadBoolUnsafe(bool* source)
    {
        int packed = Unsafe.ReadUnaligned<int>(source);
        Vector128<sbyte> byteValues = Vector128.CreateScalarUnsafe(packed).AsSByte();
        Vector128<sbyte> byteMask = Sse2.CompareGreaterThan(byteValues, Vector128<sbyte>.Zero);

        return new(Sse41.ConvertToVector128Int32(byteMask));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketInt32SseMask LoadAlignedUnsafe(int* source)
    {
        return new(Sse2.LoadAlignedVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketInt32SseMask value, int* destination)
    {
        Sse2.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreBoolUnsafe(PacketInt32SseMask value, bool* destination)
    {
        int mask = Sse.MoveMask(value._value.AsSingle());
        for (int i = 0; i < LaneCount; i++)
        {
            destination[i] = ((mask >> i) & 1) != 0;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketInt32SseMask value, int* destination)
    {
        Sse2.StoreAligned(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask And(PacketInt32SseMask left, PacketInt32SseMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask Or(PacketInt32SseMask left, PacketInt32SseMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask Xor(PacketInt32SseMask left, PacketInt32SseMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask Not(PacketInt32SseMask value) => ~value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask operator &(PacketInt32SseMask left, PacketInt32SseMask right)
    {
        return new(Sse2.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask operator |(PacketInt32SseMask left, PacketInt32SseMask right)
    {
        return new(Sse2.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask operator ^(PacketInt32SseMask left, PacketInt32SseMask right)
    {
        return new(Sse2.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask operator ~(PacketInt32SseMask value)
    {
        return new(Sse2.Xor(value._value, Vector128.Create(-1)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask operator ==(PacketInt32SseMask left, PacketInt32SseMask right)
    {
        return ~(left ^ right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask operator !=(PacketInt32SseMask left, PacketInt32SseMask right)
    {
        return left ^ right;
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketInt32SseMask other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}
