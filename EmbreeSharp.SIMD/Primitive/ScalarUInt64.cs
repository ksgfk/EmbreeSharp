namespace EmbreeSharp.SIMD;

public readonly struct ScalarUInt64 :
    ISimdInteger<ScalarUInt64, ulong, ScalarUInt64Mask>
{
    internal readonly ulong _value;

    internal ScalarUInt64(ulong value) => _value = value;

    public static int LaneCount => 1;

    public static ScalarUInt64 AdditiveIdentity => new(0ul);

    public static ScalarUInt64 MultiplicativeIdentity => new(1ul);

    public static ScalarUInt64 Broadcast(ulong value) => new(value);

    public static unsafe ScalarUInt64 Load(ReadOnlySpan<ulong> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));
        fixed (ulong* ptr = values) { return LoadUnsafe(ptr); }
    }

    public static unsafe ScalarUInt64 LoadAligned(ReadOnlySpan<ulong> values) => Load(values);

    public static unsafe void Store(ScalarUInt64 value, Span<ulong> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));
        fixed (ulong* ptr = destination) { StoreUnsafe(value, ptr); }
    }

    public static unsafe void StoreAligned(ScalarUInt64 value, Span<ulong> destination) => Store(value, destination);

    public static unsafe ScalarUInt64 LoadUnsafe(ulong* source) => new(source[0]);

    public static unsafe ScalarUInt64 LoadAlignedUnsafe(ulong* source) => LoadUnsafe(source);

    public static unsafe void StoreUnsafe(ScalarUInt64 value, ulong* destination) => destination[0] = value._value;

    public static unsafe void StoreAlignedUnsafe(ScalarUInt64 value, ulong* destination) => StoreUnsafe(value, destination);

    public static ScalarUInt64 Select(ScalarUInt64Mask mask, ScalarUInt64 ifTrue, ScalarUInt64 ifFalse) => mask._value ? ifTrue : ifFalse;

    public static ScalarUInt64 Abs(ScalarUInt64 value) => new(value._value);

    public static ScalarUInt64 Min(ScalarUInt64 left, ScalarUInt64 right) => new(Math.Min(left._value, right._value));

    public static ScalarUInt64 Max(ScalarUInt64 left, ScalarUInt64 right) => new(Math.Max(left._value, right._value));

    public static ScalarUInt64 Clamp(ScalarUInt64 value, ScalarUInt64 min, ScalarUInt64 max) => Min(Max(value, min), max);

    public static ScalarUInt64 operator +(ScalarUInt64 value) => value;

    public static ScalarUInt64 operator +(ScalarUInt64 left, ScalarUInt64 right) => new(unchecked(left._value + right._value));

    public static ScalarUInt64 operator -(ScalarUInt64 left, ScalarUInt64 right) => new(unchecked(left._value - right._value));

    public static ScalarUInt64 operator *(ScalarUInt64 left, ScalarUInt64 right) => new(unchecked(left._value * right._value));

    public static ScalarUInt64 operator -(ScalarUInt64 value) => new(unchecked(0ul - value._value));

    public static ScalarUInt64 operator &(ScalarUInt64 left, ScalarUInt64 right) => new(left._value & right._value);

    public static ScalarUInt64 operator |(ScalarUInt64 left, ScalarUInt64 right) => new(left._value | right._value);

    public static ScalarUInt64 operator ^(ScalarUInt64 left, ScalarUInt64 right) => new(left._value ^ right._value);

    public static ScalarUInt64 operator ~(ScalarUInt64 value) => new(~value._value);

    public static ScalarUInt64Mask operator ==(ScalarUInt64 left, ScalarUInt64 right) => new(left._value == right._value);

    public static ScalarUInt64Mask operator !=(ScalarUInt64 left, ScalarUInt64 right) => new(left._value != right._value);

    public static ScalarUInt64Mask operator <(ScalarUInt64 left, ScalarUInt64 right) => new(left._value < right._value);

    public static ScalarUInt64Mask operator >(ScalarUInt64 left, ScalarUInt64 right) => new(left._value > right._value);

    public static ScalarUInt64Mask operator <=(ScalarUInt64 left, ScalarUInt64 right) => new(left._value <= right._value);

    public static ScalarUInt64Mask operator >=(ScalarUInt64 left, ScalarUInt64 right) => new(left._value >= right._value);

    public override bool Equals(object? obj) => obj is ScalarUInt64 other && _value == other._value;

    public override int GetHashCode() => _value.GetHashCode();
}

public readonly struct ScalarUInt64Mask : ISimdPacketMask<ScalarUInt64Mask, ulong>
{
    internal readonly bool _value;

    internal ScalarUInt64Mask(bool value) => _value = value;

    public static int LaneCount => 1;

    public static ScalarUInt64Mask True => new(true);

    public static ScalarUInt64Mask False => new(false);

    public static ScalarUInt64Mask Broadcast(bool value) => new(value);

    public static bool All(ScalarUInt64Mask value) => value._value;

    public static bool Any(ScalarUInt64Mask value) => value._value;

    public static bool None(ScalarUInt64Mask value) => !value._value;

    public static ScalarUInt64Mask AndNot(ScalarUInt64Mask left, ScalarUInt64Mask right) => new(left._value & !right._value);

    public static ScalarUInt64Mask Select(ScalarUInt64Mask mask, ScalarUInt64Mask ifTrue, ScalarUInt64Mask ifFalse) => mask._value ? ifTrue : ifFalse;

    public static unsafe ScalarUInt64Mask Load(ReadOnlySpan<ulong> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));
        fixed (ulong* ptr = values) { return LoadUnsafe(ptr); }
    }

    public static unsafe ScalarUInt64Mask Load(ReadOnlySpan<bool> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));
        fixed (bool* ptr = values) { return LoadBoolUnsafe(ptr); }
    }

    public static unsafe ScalarUInt64Mask LoadAligned(ReadOnlySpan<ulong> values) => Load(values);

    public static unsafe void Store(ScalarUInt64Mask value, Span<ulong> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));
        fixed (ulong* ptr = destination) { StoreUnsafe(value, ptr); }
    }

    public static unsafe void Store(ScalarUInt64Mask value, Span<bool> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));
        fixed (bool* ptr = destination) { StoreBoolUnsafe(value, ptr); }
    }

    public static unsafe void StoreAligned(ScalarUInt64Mask value, Span<ulong> destination) => Store(value, destination);

    public static unsafe ScalarUInt64Mask LoadUnsafe(ulong* source) => new(source[0] != 0ul);

    public static unsafe ScalarUInt64Mask LoadBoolUnsafe(bool* source) => new(source[0]);

    public static unsafe ScalarUInt64Mask LoadAlignedUnsafe(ulong* source) => LoadUnsafe(source);

    public static unsafe void StoreUnsafe(ScalarUInt64Mask value, ulong* destination) => destination[0] = value._value ? ulong.MaxValue : 0ul;

    public static unsafe void StoreBoolUnsafe(ScalarUInt64Mask value, bool* destination) => destination[0] = value._value;

    public static unsafe void StoreAlignedUnsafe(ScalarUInt64Mask value, ulong* destination) => StoreUnsafe(value, destination);

    public static ScalarUInt64Mask And(ScalarUInt64Mask left, ScalarUInt64Mask right) => left & right;

    public static ScalarUInt64Mask Or(ScalarUInt64Mask left, ScalarUInt64Mask right) => left | right;

    public static ScalarUInt64Mask Xor(ScalarUInt64Mask left, ScalarUInt64Mask right) => left ^ right;

    public static ScalarUInt64Mask Not(ScalarUInt64Mask value) => ~value;

    public static ScalarUInt64Mask operator &(ScalarUInt64Mask left, ScalarUInt64Mask right) => new(left._value & right._value);

    public static ScalarUInt64Mask operator |(ScalarUInt64Mask left, ScalarUInt64Mask right) => new(left._value | right._value);

    public static ScalarUInt64Mask operator ^(ScalarUInt64Mask left, ScalarUInt64Mask right) => new(left._value ^ right._value);

    public static ScalarUInt64Mask operator ~(ScalarUInt64Mask value) => new(!value._value);

    public static ScalarUInt64Mask operator ==(ScalarUInt64Mask left, ScalarUInt64Mask right) => new(left._value == right._value);

    public static ScalarUInt64Mask operator !=(ScalarUInt64Mask left, ScalarUInt64Mask right) => new(left._value != right._value);

    public override bool Equals(object? obj) => obj is ScalarUInt64Mask other && _value == other._value;

    public override int GetHashCode() => _value.GetHashCode();
}
