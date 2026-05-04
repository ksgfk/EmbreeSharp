namespace EmbreeSharp.SIMD;

public readonly struct ScalarInt32 :
    ISimdInteger<ScalarInt32, int, ScalarInt32Mask>
{
    internal readonly int _value;

    internal ScalarInt32(int value) => _value = value;

    public static int LaneCount => 1;

    public static ScalarInt32 AdditiveIdentity => new(0);

    public static ScalarInt32 MultiplicativeIdentity => new(1);

    public static ScalarInt32 Broadcast(int value) => new(value);

    public static unsafe ScalarInt32 Load(ReadOnlySpan<int> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));
        fixed (int* ptr = values) { return LoadUnsafe(ptr); }
    }

    public static unsafe ScalarInt32 LoadAligned(ReadOnlySpan<int> values) => Load(values);

    public static unsafe void Store(ScalarInt32 value, Span<int> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));
        fixed (int* ptr = destination) { StoreUnsafe(value, ptr); }
    }

    public static unsafe void StoreAligned(ScalarInt32 value, Span<int> destination) => Store(value, destination);

    public static unsafe ScalarInt32 LoadUnsafe(int* source) => new(source[0]);

    public static unsafe ScalarInt32 LoadAlignedUnsafe(int* source) => LoadUnsafe(source);

    public static unsafe void StoreUnsafe(ScalarInt32 value, int* destination) => destination[0] = value._value;

    public static unsafe void StoreAlignedUnsafe(ScalarInt32 value, int* destination) => StoreUnsafe(value, destination);

    public static ScalarInt32 Select(ScalarInt32Mask mask, ScalarInt32 ifTrue, ScalarInt32 ifFalse) => mask._value ? ifTrue : ifFalse;

    public static ScalarInt32 Abs(ScalarInt32 value) => new(value._value == int.MinValue ? int.MinValue : Math.Abs(value._value));

    public static ScalarInt32 Min(ScalarInt32 left, ScalarInt32 right) => new(Math.Min(left._value, right._value));

    public static ScalarInt32 Max(ScalarInt32 left, ScalarInt32 right) => new(Math.Max(left._value, right._value));

    public static ScalarInt32 Clamp(ScalarInt32 value, ScalarInt32 min, ScalarInt32 max) => Min(Max(value, min), max);

    public static ScalarInt32 operator +(ScalarInt32 value) => value;

    public static ScalarInt32 operator +(ScalarInt32 left, ScalarInt32 right) => new(unchecked(left._value + right._value));

    public static ScalarInt32 operator -(ScalarInt32 left, ScalarInt32 right) => new(unchecked(left._value - right._value));

    public static ScalarInt32 operator *(ScalarInt32 left, ScalarInt32 right) => new(unchecked(left._value * right._value));

    public static ScalarInt32 operator -(ScalarInt32 value) => new(unchecked(-value._value));

    public static ScalarInt32 operator &(ScalarInt32 left, ScalarInt32 right) => new(left._value & right._value);

    public static ScalarInt32 operator |(ScalarInt32 left, ScalarInt32 right) => new(left._value | right._value);

    public static ScalarInt32 operator ^(ScalarInt32 left, ScalarInt32 right) => new(left._value ^ right._value);

    public static ScalarInt32 operator ~(ScalarInt32 value) => new(~value._value);

    public static ScalarInt32Mask operator ==(ScalarInt32 left, ScalarInt32 right) => new(left._value == right._value);

    public static ScalarInt32Mask operator !=(ScalarInt32 left, ScalarInt32 right) => new(left._value != right._value);

    public static ScalarInt32Mask operator <(ScalarInt32 left, ScalarInt32 right) => new(left._value < right._value);

    public static ScalarInt32Mask operator >(ScalarInt32 left, ScalarInt32 right) => new(left._value > right._value);

    public static ScalarInt32Mask operator <=(ScalarInt32 left, ScalarInt32 right) => new(left._value <= right._value);

    public static ScalarInt32Mask operator >=(ScalarInt32 left, ScalarInt32 right) => new(left._value >= right._value);

    public override bool Equals(object? obj) => obj is ScalarInt32 other && _value == other._value;

    public override int GetHashCode() => _value.GetHashCode();
}


public readonly struct ScalarInt32Mask : ISimdPacketMask<ScalarInt32Mask, int>
{
    internal readonly bool _value;

    internal ScalarInt32Mask(bool value) => _value = value;

    public static int LaneCount => 1;

    public static ScalarInt32Mask True => new(true);

    public static ScalarInt32Mask False => new(false);

    public static ScalarInt32Mask Broadcast(bool value) => new(value);

    public static bool All(ScalarInt32Mask value) => value._value;

    public static bool Any(ScalarInt32Mask value) => value._value;

    public static bool None(ScalarInt32Mask value) => !value._value;

    public static ScalarInt32Mask AndNot(ScalarInt32Mask left, ScalarInt32Mask right) => new(left._value & !right._value);

    public static ScalarInt32Mask Select(ScalarInt32Mask mask, ScalarInt32Mask ifTrue, ScalarInt32Mask ifFalse) => mask._value ? ifTrue : ifFalse;

    public static unsafe ScalarInt32Mask Load(ReadOnlySpan<int> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));
        fixed (int* ptr = values) { return LoadUnsafe(ptr); }
    }

    public static unsafe ScalarInt32Mask Load(ReadOnlySpan<bool> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));
        fixed (bool* ptr = values) { return LoadBoolUnsafe(ptr); }
    }

    public static unsafe ScalarInt32Mask LoadAligned(ReadOnlySpan<int> values) => Load(values);

    public static unsafe void Store(ScalarInt32Mask value, Span<int> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));
        fixed (int* ptr = destination) { StoreUnsafe(value, ptr); }
    }

    public static unsafe void Store(ScalarInt32Mask value, Span<bool> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));
        fixed (bool* ptr = destination) { StoreBoolUnsafe(value, ptr); }
    }

    public static unsafe void StoreAligned(ScalarInt32Mask value, Span<int> destination) => Store(value, destination);

    public static unsafe ScalarInt32Mask LoadUnsafe(int* source) => new(source[0] != 0);

    public static unsafe ScalarInt32Mask LoadBoolUnsafe(bool* source) => new(source[0]);

    public static unsafe ScalarInt32Mask LoadAlignedUnsafe(int* source) => LoadUnsafe(source);

    public static unsafe void StoreUnsafe(ScalarInt32Mask value, int* destination) => destination[0] = value._value ? -1 : 0;

    public static unsafe void StoreBoolUnsafe(ScalarInt32Mask value, bool* destination) => destination[0] = value._value;

    public static unsafe void StoreAlignedUnsafe(ScalarInt32Mask value, int* destination) => StoreUnsafe(value, destination);

    public static ScalarInt32Mask And(ScalarInt32Mask left, ScalarInt32Mask right) => left & right;

    public static ScalarInt32Mask Or(ScalarInt32Mask left, ScalarInt32Mask right) => left | right;

    public static ScalarInt32Mask Xor(ScalarInt32Mask left, ScalarInt32Mask right) => left ^ right;

    public static ScalarInt32Mask Not(ScalarInt32Mask value) => ~value;

    public static ScalarInt32Mask operator &(ScalarInt32Mask left, ScalarInt32Mask right) => new(left._value & right._value);

    public static ScalarInt32Mask operator |(ScalarInt32Mask left, ScalarInt32Mask right) => new(left._value | right._value);

    public static ScalarInt32Mask operator ^(ScalarInt32Mask left, ScalarInt32Mask right) => new(left._value ^ right._value);

    public static ScalarInt32Mask operator ~(ScalarInt32Mask value) => new(!value._value);

    public static ScalarInt32Mask operator ==(ScalarInt32Mask left, ScalarInt32Mask right) => new(left._value == right._value);

    public static ScalarInt32Mask operator !=(ScalarInt32Mask left, ScalarInt32Mask right) => new(left._value != right._value);

    public override bool Equals(object? obj) => obj is ScalarInt32Mask other && _value == other._value;

    public override int GetHashCode() => _value.GetHashCode();
}
