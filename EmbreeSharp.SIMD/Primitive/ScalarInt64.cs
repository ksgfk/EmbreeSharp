namespace EmbreeSharp.SIMD;

public readonly struct ScalarInt64 :
    ISimdInteger<ScalarInt64, long, ScalarInt64Mask>
{
    internal readonly long _value;

    internal ScalarInt64(long value) => _value = value;

    public static int LaneCount => 1;

    public static ScalarInt64 AdditiveIdentity => new(0L);

    public static ScalarInt64 MultiplicativeIdentity => new(1L);

    public static ScalarInt64 Broadcast(long value) => new(value);

    public static unsafe ScalarInt64 Load(ReadOnlySpan<long> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));
        fixed (long* ptr = values) { return LoadUnsafe(ptr); }
    }

    public static unsafe ScalarInt64 LoadAligned(ReadOnlySpan<long> values) => Load(values);

    public static unsafe void Store(ScalarInt64 value, Span<long> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));
        fixed (long* ptr = destination) { StoreUnsafe(value, ptr); }
    }

    public static unsafe void StoreAligned(ScalarInt64 value, Span<long> destination) => Store(value, destination);

    public static unsafe ScalarInt64 LoadUnsafe(long* source) => new(source[0]);

    public static unsafe ScalarInt64 LoadAlignedUnsafe(long* source) => LoadUnsafe(source);

    public static unsafe void StoreUnsafe(ScalarInt64 value, long* destination) => destination[0] = value._value;

    public static unsafe void StoreAlignedUnsafe(ScalarInt64 value, long* destination) => StoreUnsafe(value, destination);

    public static ScalarInt64 Select(ScalarInt64Mask mask, ScalarInt64 ifTrue, ScalarInt64 ifFalse) => mask._value ? ifTrue : ifFalse;

    public static ScalarInt64 Abs(ScalarInt64 value) => new(value._value == long.MinValue ? long.MinValue : Math.Abs(value._value));

    public static ScalarInt64 Min(ScalarInt64 left, ScalarInt64 right) => new(Math.Min(left._value, right._value));

    public static ScalarInt64 Max(ScalarInt64 left, ScalarInt64 right) => new(Math.Max(left._value, right._value));

    public static ScalarInt64 Clamp(ScalarInt64 value, ScalarInt64 min, ScalarInt64 max) => Min(Max(value, min), max);

    public static ScalarInt64 operator +(ScalarInt64 value) => value;

    public static ScalarInt64 operator +(ScalarInt64 left, ScalarInt64 right) => new(unchecked(left._value + right._value));

    public static ScalarInt64 operator -(ScalarInt64 left, ScalarInt64 right) => new(unchecked(left._value - right._value));

    public static ScalarInt64 operator *(ScalarInt64 left, ScalarInt64 right) => new(unchecked(left._value * right._value));

    public static ScalarInt64 operator -(ScalarInt64 value) => new(unchecked(-value._value));

    public static ScalarInt64 operator &(ScalarInt64 left, ScalarInt64 right) => new(left._value & right._value);

    public static ScalarInt64 operator |(ScalarInt64 left, ScalarInt64 right) => new(left._value | right._value);

    public static ScalarInt64 operator ^(ScalarInt64 left, ScalarInt64 right) => new(left._value ^ right._value);

    public static ScalarInt64 operator ~(ScalarInt64 value) => new(~value._value);

    public static ScalarInt64Mask operator ==(ScalarInt64 left, ScalarInt64 right) => new(left._value == right._value);

    public static ScalarInt64Mask operator !=(ScalarInt64 left, ScalarInt64 right) => new(left._value != right._value);

    public static ScalarInt64Mask operator <(ScalarInt64 left, ScalarInt64 right) => new(left._value < right._value);

    public static ScalarInt64Mask operator >(ScalarInt64 left, ScalarInt64 right) => new(left._value > right._value);

    public static ScalarInt64Mask operator <=(ScalarInt64 left, ScalarInt64 right) => new(left._value <= right._value);

    public static ScalarInt64Mask operator >=(ScalarInt64 left, ScalarInt64 right) => new(left._value >= right._value);

    public override bool Equals(object? obj) => obj is ScalarInt64 other && _value == other._value;

    public override int GetHashCode() => _value.GetHashCode();
}

public readonly struct ScalarInt64Mask : ISimdPacketMask<ScalarInt64Mask, long>
{
    internal readonly bool _value;

    internal ScalarInt64Mask(bool value) => _value = value;

    public static int LaneCount => 1;

    public static ScalarInt64Mask True => new(true);

    public static ScalarInt64Mask False => new(false);

    public static ScalarInt64Mask Broadcast(bool value) => new(value);

    public static bool All(ScalarInt64Mask value) => value._value;

    public static bool Any(ScalarInt64Mask value) => value._value;

    public static bool None(ScalarInt64Mask value) => !value._value;

    public static ScalarInt64Mask AndNot(ScalarInt64Mask left, ScalarInt64Mask right) => new(left._value & !right._value);

    public static ScalarInt64Mask Select(ScalarInt64Mask mask, ScalarInt64Mask ifTrue, ScalarInt64Mask ifFalse) => mask._value ? ifTrue : ifFalse;

    public static unsafe ScalarInt64Mask Load(ReadOnlySpan<long> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));
        fixed (long* ptr = values) { return LoadUnsafe(ptr); }
    }

    public static unsafe ScalarInt64Mask Load(ReadOnlySpan<bool> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));
        fixed (bool* ptr = values) { return LoadBoolUnsafe(ptr); }
    }

    public static unsafe ScalarInt64Mask LoadAligned(ReadOnlySpan<long> values) => Load(values);

    public static unsafe void Store(ScalarInt64Mask value, Span<long> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));
        fixed (long* ptr = destination) { StoreUnsafe(value, ptr); }
    }

    public static unsafe void Store(ScalarInt64Mask value, Span<bool> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));
        fixed (bool* ptr = destination) { StoreBoolUnsafe(value, ptr); }
    }

    public static unsafe void StoreAligned(ScalarInt64Mask value, Span<long> destination) => Store(value, destination);

    public static unsafe ScalarInt64Mask LoadUnsafe(long* source) => new(source[0] != 0L);

    public static unsafe ScalarInt64Mask LoadBoolUnsafe(bool* source) => new(source[0]);

    public static unsafe ScalarInt64Mask LoadAlignedUnsafe(long* source) => LoadUnsafe(source);

    public static unsafe void StoreUnsafe(ScalarInt64Mask value, long* destination) => destination[0] = value._value ? -1L : 0L;

    public static unsafe void StoreBoolUnsafe(ScalarInt64Mask value, bool* destination) => destination[0] = value._value;

    public static unsafe void StoreAlignedUnsafe(ScalarInt64Mask value, long* destination) => StoreUnsafe(value, destination);

    public static ScalarInt64Mask And(ScalarInt64Mask left, ScalarInt64Mask right) => left & right;

    public static ScalarInt64Mask Or(ScalarInt64Mask left, ScalarInt64Mask right) => left | right;

    public static ScalarInt64Mask Xor(ScalarInt64Mask left, ScalarInt64Mask right) => left ^ right;

    public static ScalarInt64Mask Not(ScalarInt64Mask value) => ~value;

    public static ScalarInt64Mask operator &(ScalarInt64Mask left, ScalarInt64Mask right) => new(left._value & right._value);

    public static ScalarInt64Mask operator |(ScalarInt64Mask left, ScalarInt64Mask right) => new(left._value | right._value);

    public static ScalarInt64Mask operator ^(ScalarInt64Mask left, ScalarInt64Mask right) => new(left._value ^ right._value);

    public static ScalarInt64Mask operator ~(ScalarInt64Mask value) => new(!value._value);

    public static ScalarInt64Mask operator ==(ScalarInt64Mask left, ScalarInt64Mask right) => new(left._value == right._value);

    public static ScalarInt64Mask operator !=(ScalarInt64Mask left, ScalarInt64Mask right) => new(left._value != right._value);

    public override bool Equals(object? obj) => obj is ScalarInt64Mask other && _value == other._value;

    public override int GetHashCode() => _value.GetHashCode();
}
