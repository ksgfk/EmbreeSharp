using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace EmbreeSharp.SIMD;

public readonly struct PacketUInt32Sse :
    ISimdInteger<PacketUInt32Sse, uint, PacketUInt32SseMask>
{
    internal readonly Vector128<uint> _value;

    private PacketUInt32Sse(Vector128<uint> value)
    {
        _value = value;
    }

    public static int LaneCount => 4;

    public static PacketUInt32Sse AdditiveIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(0);
    }

    public static PacketUInt32Sse MultiplicativeIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(1);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Sse Broadcast(uint value)
    {
        return new(Vector128.Create(value));
    }

    public static unsafe PacketUInt32Sse Load(ReadOnlySpan<uint> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (uint* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketUInt32Sse LoadAligned(ReadOnlySpan<uint> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (uint* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketUInt32Sse value, Span<uint> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (uint* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketUInt32Sse value, Span<uint> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (uint* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt32Sse LoadUnsafe(uint* source)
    {
        return new(Sse2.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt32Sse LoadAlignedUnsafe(uint* source)
    {
        return new(Sse2.LoadAlignedVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketUInt32Sse value, uint* destination)
    {
        Sse2.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketUInt32Sse value, uint* destination)
    {
        Sse2.StoreAligned(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Sse Select(PacketUInt32SseMask mask, PacketUInt32Sse ifTrue, PacketUInt32Sse ifFalse)
    {
        return new(Sse41.BlendVariable(ifFalse._value, ifTrue._value, mask._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Sse Abs(PacketUInt32Sse value)
    {
        return value;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Sse Min(PacketUInt32Sse left, PacketUInt32Sse right)
    {
        return new(Sse41.Min(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Sse Max(PacketUInt32Sse left, PacketUInt32Sse right)
    {
        return new(Sse41.Max(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Sse Clamp(PacketUInt32Sse value, PacketUInt32Sse min, PacketUInt32Sse max)
    {
        return Min(Max(value, min), max);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Sse operator +(PacketUInt32Sse value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Sse operator +(PacketUInt32Sse left, PacketUInt32Sse right)
    {
        return new(Sse2.Add(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Sse operator -(PacketUInt32Sse left, PacketUInt32Sse right)
    {
        return new(Sse2.Subtract(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Sse operator *(PacketUInt32Sse left, PacketUInt32Sse right)
    {
        return new(Sse41.MultiplyLow(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Sse operator -(PacketUInt32Sse value)
    {
        return new(Sse2.Subtract(Vector128<uint>.Zero, value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Sse operator &(PacketUInt32Sse left, PacketUInt32Sse right)
    {
        return new(Sse2.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Sse operator |(PacketUInt32Sse left, PacketUInt32Sse right)
    {
        return new(Sse2.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Sse operator ^(PacketUInt32Sse left, PacketUInt32Sse right)
    {
        return new(Sse2.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32Sse operator ~(PacketUInt32Sse value)
    {
        return new(Sse2.Xor(value._value, Vector128.Create(uint.MaxValue)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask operator ==(PacketUInt32Sse left, PacketUInt32Sse right)
    {
        return new(Sse2.CompareEqual(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask operator !=(PacketUInt32Sse left, PacketUInt32Sse right)
    {
        return ~(left == right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask operator <(PacketUInt32Sse left, PacketUInt32Sse right)
    {
        Vector128<uint> offset = Vector128.Create(0x80000000u);
        return new(Sse2.CompareGreaterThan(
            Sse2.Subtract(right._value, offset).AsInt32(),
            Sse2.Subtract(left._value, offset).AsInt32()).AsUInt32());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask operator >(PacketUInt32Sse left, PacketUInt32Sse right)
    {
        Vector128<uint> offset = Vector128.Create(0x80000000u);
        return new(Sse2.CompareGreaterThan(
            Sse2.Subtract(left._value, offset).AsInt32(),
            Sse2.Subtract(right._value, offset).AsInt32()).AsUInt32());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask operator <=(PacketUInt32Sse left, PacketUInt32Sse right)
    {
        return ~(left > right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask operator >=(PacketUInt32Sse left, PacketUInt32Sse right)
    {
        return ~(left < right);
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketUInt32Sse other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}

public readonly struct PacketUInt32SseMask : ISimdPacketMask<PacketUInt32SseMask, uint>
{
    internal readonly Vector128<uint> _value;

    internal PacketUInt32SseMask(Vector128<uint> value)
    {
        _value = value;
    }

    public static int LaneCount => 4;

    public static PacketUInt32SseMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector128.Create(uint.MaxValue));
    }

    public static PacketUInt32SseMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector128<uint>.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask Broadcast(bool value) => value ? True : False;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool All(PacketUInt32SseMask value)
    {
        return Sse.MoveMask(value._value.AsSingle()) == 0xF;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Any(PacketUInt32SseMask value)
    {
        return Sse.MoveMask(value._value.AsSingle()) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool None(PacketUInt32SseMask value)
    {
        return Sse.MoveMask(value._value.AsSingle()) == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask AndNot(PacketUInt32SseMask left, PacketUInt32SseMask right)
    {
        return new(Sse2.AndNot(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask Select(PacketUInt32SseMask mask, PacketUInt32SseMask ifTrue, PacketUInt32SseMask ifFalse)
    {
        return new(Sse41.BlendVariable(ifFalse._value, ifTrue._value, mask._value));
    }

    public static unsafe PacketUInt32SseMask Load(ReadOnlySpan<uint> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (uint* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketUInt32SseMask Load(ReadOnlySpan<bool> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (bool* ptr = values)
        {
            return LoadBoolUnsafe(ptr);
        }
    }

    public static unsafe PacketUInt32SseMask LoadAligned(ReadOnlySpan<uint> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (uint* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketUInt32SseMask value, Span<uint> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (uint* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void Store(PacketUInt32SseMask value, Span<bool> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (bool* ptr = destination)
        {
            StoreBoolUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketUInt32SseMask value, Span<uint> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (uint* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt32SseMask LoadUnsafe(uint* source)
    {
        return new(Sse2.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt32SseMask LoadBoolUnsafe(bool* source)
    {
        int packed = Unsafe.ReadUnaligned<int>(source);
        Vector128<sbyte> byteValues = Vector128.CreateScalarUnsafe(packed).AsSByte();
        Vector128<sbyte> byteMask = Sse2.CompareGreaterThan(byteValues, Vector128<sbyte>.Zero);

        return new(Sse41.ConvertToVector128Int32(byteMask).AsUInt32());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketUInt32SseMask LoadAlignedUnsafe(uint* source)
    {
        return new(Sse2.LoadAlignedVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketUInt32SseMask value, uint* destination)
    {
        Sse2.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreBoolUnsafe(PacketUInt32SseMask value, bool* destination)
    {
        int mask = Sse.MoveMask(value._value.AsSingle());
        for (int i = 0; i < LaneCount; i++)
        {
            destination[i] = ((mask >> i) & 1) != 0;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketUInt32SseMask value, uint* destination)
    {
        Sse2.StoreAligned(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask And(PacketUInt32SseMask left, PacketUInt32SseMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask Or(PacketUInt32SseMask left, PacketUInt32SseMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask Xor(PacketUInt32SseMask left, PacketUInt32SseMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask Not(PacketUInt32SseMask value) => ~value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask operator &(PacketUInt32SseMask left, PacketUInt32SseMask right)
    {
        return new(Sse2.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask operator |(PacketUInt32SseMask left, PacketUInt32SseMask right)
    {
        return new(Sse2.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask operator ^(PacketUInt32SseMask left, PacketUInt32SseMask right)
    {
        return new(Sse2.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask operator ~(PacketUInt32SseMask value)
    {
        return new(Sse2.Xor(value._value, Vector128.Create(uint.MaxValue)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask operator ==(PacketUInt32SseMask left, PacketUInt32SseMask right)
    {
        return ~(left ^ right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask operator !=(PacketUInt32SseMask left, PacketUInt32SseMask right)
    {
        return left ^ right;
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketUInt32SseMask other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}
