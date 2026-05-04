using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace EmbreeSharp.SIMD;

public readonly partial struct PacketFloat64Sse :
    ISimdFloatingPoint<PacketFloat64Sse, double, PacketFloat64SseMask>
{
    internal readonly Vector128<double> _value;

    internal PacketFloat64Sse(Vector128<double> value)
    {
        _value = value;
    }

    public static int LaneCount => 2;

    public static PacketFloat64Sse AdditiveIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(0d);
    }

    public static PacketFloat64Sse MultiplicativeIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(1d);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Sse Broadcast(double value)
    {
        return new(Vector128.Create(value));
    }

    public static unsafe PacketFloat64Sse Load(ReadOnlySpan<double> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (double* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketFloat64Sse LoadAligned(ReadOnlySpan<double> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (double* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketFloat64Sse value, Span<double> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (double* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketFloat64Sse value, Span<double> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (double* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat64Sse LoadUnsafe(double* source)
    {
        return new(Sse2.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat64Sse LoadAlignedUnsafe(double* source)
    {
        return new(Sse2.LoadAlignedVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketFloat64Sse value, double* destination)
    {
        Sse2.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketFloat64Sse value, double* destination)
    {
        Sse2.StoreAligned(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Sse Select(PacketFloat64SseMask mask, PacketFloat64Sse ifTrue, PacketFloat64Sse ifFalse)
    {
        return new(Sse41.BlendVariable(ifFalse._value, ifTrue._value, mask._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Sse Abs(PacketFloat64Sse value)
    {
        return new(Sse2.AndNot(Vector128.Create(-0d), value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Sse Min(PacketFloat64Sse left, PacketFloat64Sse right)
    {
        return new(Sse2.Min(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Sse Max(PacketFloat64Sse left, PacketFloat64Sse right)
    {
        return new(Sse2.Max(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Sse Clamp(PacketFloat64Sse value, PacketFloat64Sse min, PacketFloat64Sse max)
    {
        return Min(Max(value, min), max);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Sse Sqrt(PacketFloat64Sse value)
    {
        return new(Sse2.Sqrt(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Sse Reciprocal(PacketFloat64Sse value)
    {
        return new(Sse2.Divide(Vector128.Create(1d), value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Sse ReciprocalSqrt(PacketFloat64Sse value)
    {
        return new(Sse2.Divide(Vector128.Create(1d), Sse2.Sqrt(value._value)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Sse Floor(PacketFloat64Sse value)
    {
        return new(Sse41.Floor(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Sse Ceiling(PacketFloat64Sse value)
    {
        return new(Sse41.Ceiling(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Sse Truncate(PacketFloat64Sse value)
    {
        return new(Sse41.RoundToZero(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Sse Round(PacketFloat64Sse value)
    {
        return new(Sse41.RoundToNearestInteger(value._value));
    }

    public static partial PacketFloat64Sse Sin(PacketFloat64Sse value);

    public static partial PacketFloat64Sse Cos(PacketFloat64Sse value);

    public static partial (PacketFloat64Sse Sin, PacketFloat64Sse Cos) SinCos(PacketFloat64Sse value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Sse FusedMultiplyAdd(PacketFloat64Sse left, PacketFloat64Sse right, PacketFloat64Sse addend)
    {
        return new(Fma.IsSupported
            ? Fma.MultiplyAdd(left._value, right._value, addend._value)
            : Sse2.Add(Sse2.Multiply(left._value, right._value), addend._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Sse CopySign(PacketFloat64Sse value, PacketFloat64Sse sign)
    {
        Vector128<double> signMask = Vector128.Create(-0d);
        Vector128<double> magnitude = Sse2.AndNot(signMask, value._value);
        Vector128<double> signBits = Sse2.And(signMask, sign._value);
        return new(Sse2.Or(magnitude, signBits));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask IsFinite(PacketFloat64Sse value)
    {
        Vector128<double> magnitude = Sse2.AndNot(Vector128.Create(-0d), value._value);
        return new(Sse2.CompareLessThan(magnitude, Vector128.Create(double.PositiveInfinity)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask IsInfinity(PacketFloat64Sse value)
    {
        Vector128<double> magnitude = Sse2.AndNot(Vector128.Create(-0d), value._value);
        return new(Sse2.CompareEqual(magnitude, Vector128.Create(double.PositiveInfinity)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask IsNaN(PacketFloat64Sse value)
    {
        return new(Sse2.CompareUnordered(value._value, value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Sse operator +(PacketFloat64Sse value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Sse operator +(PacketFloat64Sse left, PacketFloat64Sse right)
    {
        return new(Sse2.Add(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Sse operator -(PacketFloat64Sse left, PacketFloat64Sse right)
    {
        return new(Sse2.Subtract(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Sse operator *(PacketFloat64Sse left, PacketFloat64Sse right)
    {
        return new(Sse2.Multiply(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Sse operator /(PacketFloat64Sse left, PacketFloat64Sse right)
    {
        return new(Sse2.Divide(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Sse operator -(PacketFloat64Sse value)
    {
        return new(Sse2.Xor(value._value, Vector128.Create(-0d)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask operator ==(PacketFloat64Sse left, PacketFloat64Sse right)
    {
        return new(Sse2.CompareEqual(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask operator !=(PacketFloat64Sse left, PacketFloat64Sse right)
    {
        return new(Sse2.CompareNotEqual(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask operator <(PacketFloat64Sse left, PacketFloat64Sse right)
    {
        return new(Sse2.CompareLessThan(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask operator >(PacketFloat64Sse left, PacketFloat64Sse right)
    {
        return new(Sse2.CompareGreaterThan(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask operator <=(PacketFloat64Sse left, PacketFloat64Sse right)
    {
        return new(Sse2.CompareLessThanOrEqual(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask operator >=(PacketFloat64Sse left, PacketFloat64Sse right)
    {
        return new(Sse2.CompareGreaterThanOrEqual(left._value, right._value));
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketFloat64Sse other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}

public readonly struct PacketFloat64SseMask : ISimdPacketMask<PacketFloat64SseMask, double>
{
    internal readonly Vector128<double> _value;

    internal PacketFloat64SseMask(Vector128<double> value)
    {
        _value = value;
    }

    public static int LaneCount => 2;

    public static PacketFloat64SseMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector128.Create(BitConverter.Int64BitsToDouble(-1)));
    }

    public static PacketFloat64SseMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector128<double>.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask Broadcast(bool value) => value ? True : False;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool All(PacketFloat64SseMask value)
    {
        return Sse2.MoveMask(value._value) == 0x3;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Any(PacketFloat64SseMask value)
    {
        return Sse2.MoveMask(value._value) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool None(PacketFloat64SseMask value)
    {
        return Sse2.MoveMask(value._value) == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask AndNot(PacketFloat64SseMask left, PacketFloat64SseMask right)
    {
        return new(Sse2.AndNot(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask Select(PacketFloat64SseMask mask, PacketFloat64SseMask ifTrue, PacketFloat64SseMask ifFalse)
    {
        return new(Sse41.BlendVariable(ifFalse._value, ifTrue._value, mask._value));
    }

    public static unsafe PacketFloat64SseMask Load(ReadOnlySpan<double> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (double* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketFloat64SseMask Load(ReadOnlySpan<bool> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (bool* ptr = values)
        {
            return LoadBoolUnsafe(ptr);
        }
    }

    public static unsafe PacketFloat64SseMask LoadAligned(ReadOnlySpan<double> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (double* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketFloat64SseMask value, Span<double> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (double* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void Store(PacketFloat64SseMask value, Span<bool> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (bool* ptr = destination)
        {
            StoreBoolUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketFloat64SseMask value, Span<double> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (double* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat64SseMask LoadUnsafe(double* source)
    {
        return new(Sse2.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat64SseMask LoadBoolUnsafe(bool* source)
    {
        short packed = Unsafe.ReadUnaligned<short>(source);
        Vector128<sbyte> byteValues = Vector128.CreateScalarUnsafe(packed).AsSByte();
        Vector128<sbyte> byteMask = Sse2.CompareGreaterThan(byteValues, Vector128<sbyte>.Zero);

        return new(Sse41.ConvertToVector128Int64(byteMask).AsDouble());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat64SseMask LoadAlignedUnsafe(double* source)
    {
        return new(Sse2.LoadAlignedVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketFloat64SseMask value, double* destination)
    {
        Sse2.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreBoolUnsafe(PacketFloat64SseMask value, bool* destination)
    {
        int mask = Sse2.MoveMask(value._value);
        for (int i = 0; i < LaneCount; i++)
        {
            destination[i] = ((mask >> i) & 1) != 0;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketFloat64SseMask value, double* destination)
    {
        Sse2.StoreAligned(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask And(PacketFloat64SseMask left, PacketFloat64SseMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask Or(PacketFloat64SseMask left, PacketFloat64SseMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask Xor(PacketFloat64SseMask left, PacketFloat64SseMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask Not(PacketFloat64SseMask value) => ~value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask operator &(PacketFloat64SseMask left, PacketFloat64SseMask right)
    {
        return new(Sse2.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask operator |(PacketFloat64SseMask left, PacketFloat64SseMask right)
    {
        return new(Sse2.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask operator ^(PacketFloat64SseMask left, PacketFloat64SseMask right)
    {
        return new(Sse2.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask operator ~(PacketFloat64SseMask value)
    {
        return new(Sse2.Xor(value._value, Vector128.Create(BitConverter.Int64BitsToDouble(-1))));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask operator ==(PacketFloat64SseMask left, PacketFloat64SseMask right)
    {
        return ~(left ^ right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask operator !=(PacketFloat64SseMask left, PacketFloat64SseMask right)
    {
        return left ^ right;
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketFloat64SseMask other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}
