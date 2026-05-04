using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace EmbreeSharp.SIMD;

public readonly partial struct PacketFloat32Sse :
    ISimdFloatingPoint<PacketFloat32Sse, float, PacketFloat32SseMask>
{
    internal readonly Vector128<float> _value;

    internal PacketFloat32Sse(Vector128<float> value)
    {
        _value = value;
    }

    public static int LaneCount => 4;

    public static PacketFloat32Sse AdditiveIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(0f);
    }

    public static PacketFloat32Sse MultiplicativeIdentity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Broadcast(1f);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Sse Broadcast(float value)
    {
        return new(Vector128.Create(value));
    }

    public static unsafe PacketFloat32Sse Load(ReadOnlySpan<float> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (float* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketFloat32Sse LoadAligned(ReadOnlySpan<float> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (float* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketFloat32Sse value, Span<float> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (float* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketFloat32Sse value, Span<float> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (float* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat32Sse LoadUnsafe(float* source)
    {
        return new(Sse.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat32Sse LoadAlignedUnsafe(float* source)
    {
        return new(Sse.LoadAlignedVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketFloat32Sse value, float* destination)
    {
        Sse.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketFloat32Sse value, float* destination)
    {
        Sse.StoreAligned(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Sse Select(PacketFloat32SseMask mask, PacketFloat32Sse ifTrue, PacketFloat32Sse ifFalse)
    {
        return new(Sse41.BlendVariable(ifFalse._value, ifTrue._value, mask._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Sse Abs(PacketFloat32Sse value)
    {
        return new(Sse.AndNot(Vector128.Create(-0f), value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Sse Min(PacketFloat32Sse left, PacketFloat32Sse right)
    {
        return new(Sse.Min(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Sse Max(PacketFloat32Sse left, PacketFloat32Sse right)
    {
        return new(Sse.Max(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Sse Clamp(PacketFloat32Sse value, PacketFloat32Sse min, PacketFloat32Sse max)
    {
        return Min(Max(value, min), max);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Sse Sqrt(PacketFloat32Sse value)
    {
        return new(Sse.Sqrt(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Sse Reciprocal(PacketFloat32Sse value)
    {
        Vector128<float> estimate = Sse.Reciprocal(value._value);
        Vector128<float> doubledEstimate = Sse.Add(estimate, estimate);
        Vector128<float> product = Sse.Multiply(estimate, value._value);

        Vector128<float> refined = Fma.IsSupported
            ? Fma.MultiplyAddNegated(product, estimate, doubledEstimate)
            : Sse.Subtract(doubledEstimate, Sse.Multiply(estimate, product));

        return new(Sse41.BlendVariable(refined, estimate, product));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Sse ReciprocalSqrt(PacketFloat32Sse value)
    {
        Vector128<float> estimate = Sse.ReciprocalSqrt(value._value);
        Vector128<float> halfEstimate = Sse.Multiply(estimate, Vector128.Create(0.5f));
        Vector128<float> product = Sse.Multiply(estimate, value._value);

        Vector128<float> correction = Fma.IsSupported
            ? Fma.MultiplyAddNegated(product, estimate, Vector128.Create(3f))
            : Sse.Subtract(Vector128.Create(3f), Sse.Multiply(product, estimate));

        Vector128<float> refined = Sse.Multiply(correction, halfEstimate);
        return new(Sse41.BlendVariable(refined, estimate, product));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Sse Floor(PacketFloat32Sse value)
    {
        return new(Sse41.Floor(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Sse Ceiling(PacketFloat32Sse value)
    {
        return new(Sse41.Ceiling(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Sse Truncate(PacketFloat32Sse value)
    {
        return new(Sse41.RoundToZero(value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Sse Round(PacketFloat32Sse value)
    {
        return new(Sse41.RoundToNearestInteger(value._value));
    }

    public static partial PacketFloat32Sse Sin(PacketFloat32Sse value);

    public static partial PacketFloat32Sse Cos(PacketFloat32Sse value);

    public static partial (PacketFloat32Sse Sin, PacketFloat32Sse Cos) SinCos(PacketFloat32Sse value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Sse FusedMultiplyAdd(PacketFloat32Sse left, PacketFloat32Sse right, PacketFloat32Sse addend)
    {
        return new(Fma.IsSupported
            ? Fma.MultiplyAdd(left._value, right._value, addend._value)
            : Sse.Add(Sse.Multiply(left._value, right._value), addend._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Sse CopySign(PacketFloat32Sse value, PacketFloat32Sse sign)
    {
        Vector128<float> signMask = Vector128.Create(-0f);
        Vector128<float> magnitude = Sse.AndNot(signMask, value._value);
        Vector128<float> signBits = Sse.And(signMask, sign._value);
        return new(Sse.Or(magnitude, signBits));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask IsFinite(PacketFloat32Sse value)
    {
        Vector128<float> magnitude = Sse.AndNot(Vector128.Create(-0f), value._value);
        return new(Sse.CompareLessThan(magnitude, Vector128.Create(float.PositiveInfinity)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask IsInfinity(PacketFloat32Sse value)
    {
        Vector128<float> magnitude = Sse.AndNot(Vector128.Create(-0f), value._value);
        return new(Sse.CompareEqual(magnitude, Vector128.Create(float.PositiveInfinity)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask IsNaN(PacketFloat32Sse value)
    {
        return new(Sse.CompareUnordered(value._value, value._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Sse operator +(PacketFloat32Sse value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Sse operator +(PacketFloat32Sse left, PacketFloat32Sse right)
    {
        return new(Sse.Add(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Sse operator -(PacketFloat32Sse left, PacketFloat32Sse right)
    {
        return new(Sse.Subtract(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Sse operator *(PacketFloat32Sse left, PacketFloat32Sse right)
    {
        return new(Sse.Multiply(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Sse operator /(PacketFloat32Sse left, PacketFloat32Sse right)
    {
        return new(Sse.Divide(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Sse operator -(PacketFloat32Sse value)
    {
        return new(Sse.Xor(value._value, Vector128.Create(-0f)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask operator ==(PacketFloat32Sse left, PacketFloat32Sse right)
    {
        return new(Sse.CompareEqual(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask operator !=(PacketFloat32Sse left, PacketFloat32Sse right)
    {
        return new(Sse.CompareNotEqual(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask operator <(PacketFloat32Sse left, PacketFloat32Sse right)
    {
        return new(Sse.CompareLessThan(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask operator >(PacketFloat32Sse left, PacketFloat32Sse right)
    {
        return new(Sse.CompareGreaterThan(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask operator <=(PacketFloat32Sse left, PacketFloat32Sse right)
    {
        return new(Sse.CompareLessThanOrEqual(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask operator >=(PacketFloat32Sse left, PacketFloat32Sse right)
    {
        return new(Sse.CompareGreaterThanOrEqual(left._value, right._value));
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketFloat32Sse other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}

public readonly struct PacketFloat32SseMask : ISimdPacketMask<PacketFloat32SseMask, float>
{
    internal readonly Vector128<float> _value;

    internal PacketFloat32SseMask(Vector128<float> value)
    {
        _value = value;
    }

    public static int LaneCount => 4;

    public static PacketFloat32SseMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector128.Create(BitConverter.Int32BitsToSingle(-1)));
    }

    public static PacketFloat32SseMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(Vector128<float>.Zero);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask Broadcast(bool value) => value ? True : False;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool All(PacketFloat32SseMask value)
    {
        return Sse.MoveMask(value._value) == 0xF;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Any(PacketFloat32SseMask value)
    {
        return Sse.MoveMask(value._value) != 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool None(PacketFloat32SseMask value)
    {
        return Sse.MoveMask(value._value) == 0;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask AndNot(PacketFloat32SseMask left, PacketFloat32SseMask right)
    {
        return new(Sse.AndNot(right._value, left._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask Select(PacketFloat32SseMask mask, PacketFloat32SseMask ifTrue, PacketFloat32SseMask ifFalse)
    {
        return new(Sse41.BlendVariable(ifFalse._value, ifTrue._value, mask._value));
    }

    public static unsafe PacketFloat32SseMask Load(ReadOnlySpan<float> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (float* ptr = values)
        {
            return LoadUnsafe(ptr);
        }
    }

    public static unsafe PacketFloat32SseMask Load(ReadOnlySpan<bool> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (bool* ptr = values)
        {
            return LoadBoolUnsafe(ptr);
        }
    }

    public static unsafe PacketFloat32SseMask LoadAligned(ReadOnlySpan<float> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));

        fixed (float* ptr = values)
        {
            return LoadAlignedUnsafe(ptr);
        }
    }

    public static unsafe void Store(PacketFloat32SseMask value, Span<float> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (float* ptr = destination)
        {
            StoreUnsafe(value, ptr);
        }
    }

    public static unsafe void Store(PacketFloat32SseMask value, Span<bool> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (bool* ptr = destination)
        {
            StoreBoolUnsafe(value, ptr);
        }
    }

    public static unsafe void StoreAligned(PacketFloat32SseMask value, Span<float> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));

        fixed (float* ptr = destination)
        {
            StoreAlignedUnsafe(value, ptr);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat32SseMask LoadUnsafe(float* source)
    {
        return new(Sse.LoadVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat32SseMask LoadBoolUnsafe(bool* source)
    {
        int packed = Unsafe.ReadUnaligned<int>(source);
        Vector128<sbyte> byteValues = Vector128.CreateScalarUnsafe(packed).AsSByte();
        Vector128<sbyte> byteMask = Sse2.CompareGreaterThan(byteValues, Vector128<sbyte>.Zero);

        return new(Sse41.ConvertToVector128Int32(byteMask).AsSingle());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe PacketFloat32SseMask LoadAlignedUnsafe(float* source)
    {
        return new(Sse.LoadAlignedVector128(source));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreUnsafe(PacketFloat32SseMask value, float* destination)
    {
        Sse.Store(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreBoolUnsafe(PacketFloat32SseMask value, bool* destination)
    {
        int mask = Sse.MoveMask(value._value);
        for (int i = 0; i < LaneCount; i++)
        {
            destination[i] = ((mask >> i) & 1) != 0;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe void StoreAlignedUnsafe(PacketFloat32SseMask value, float* destination)
    {
        Sse.StoreAligned(destination, value._value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask And(PacketFloat32SseMask left, PacketFloat32SseMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask Or(PacketFloat32SseMask left, PacketFloat32SseMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask Xor(PacketFloat32SseMask left, PacketFloat32SseMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask Not(PacketFloat32SseMask value) => ~value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask operator &(PacketFloat32SseMask left, PacketFloat32SseMask right)
    {
        return new(Sse.And(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask operator |(PacketFloat32SseMask left, PacketFloat32SseMask right)
    {
        return new(Sse.Or(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask operator ^(PacketFloat32SseMask left, PacketFloat32SseMask right)
    {
        return new(Sse.Xor(left._value, right._value));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask operator ~(PacketFloat32SseMask value)
    {
        return new(Sse.Xor(value._value, Vector128.Create(BitConverter.Int32BitsToSingle(-1))));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask operator ==(PacketFloat32SseMask left, PacketFloat32SseMask right)
    {
        return ~(left ^ right);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask operator !=(PacketFloat32SseMask left, PacketFloat32SseMask right)
    {
        return left ^ right;
    }

    public override bool Equals(object? obj)
    {
        return obj is PacketFloat32SseMask other && _value.Equals(other._value);
    }

    public override int GetHashCode() => _value.GetHashCode();
}
