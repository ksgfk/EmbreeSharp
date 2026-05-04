namespace EmbreeSharp.SIMD;

public readonly struct ScalarUInt32 :
    ISimdInteger<ScalarUInt32, uint, ScalarUInt32Mask>
{
    internal readonly uint _value;

    internal ScalarUInt32(uint value) => _value = value;

    public static int LaneCount => 1;

    public static ScalarUInt32 AdditiveIdentity => new(0u);

    public static ScalarUInt32 MultiplicativeIdentity => new(1u);

    public static ScalarUInt32 Broadcast(uint value) => new(value);

    public static unsafe ScalarUInt32 Load(ReadOnlySpan<uint> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));
        fixed (uint* ptr = values) { return LoadUnsafe(ptr); }
    }

    public static unsafe ScalarUInt32 LoadAligned(ReadOnlySpan<uint> values) => Load(values);

    public static unsafe void Store(ScalarUInt32 value, Span<uint> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));
        fixed (uint* ptr = destination) { StoreUnsafe(value, ptr); }
    }

    public static unsafe void StoreAligned(ScalarUInt32 value, Span<uint> destination) => Store(value, destination);

    public static unsafe ScalarUInt32 LoadUnsafe(uint* source) => new(source[0]);

    public static unsafe ScalarUInt32 LoadAlignedUnsafe(uint* source) => LoadUnsafe(source);

    public static unsafe void StoreUnsafe(ScalarUInt32 value, uint* destination) => destination[0] = value._value;

    public static unsafe void StoreAlignedUnsafe(ScalarUInt32 value, uint* destination) => StoreUnsafe(value, destination);

    public static ScalarUInt32 Select(ScalarUInt32Mask mask, ScalarUInt32 ifTrue, ScalarUInt32 ifFalse) => mask._value ? ifTrue : ifFalse;

    public static ScalarUInt32 Abs(ScalarUInt32 value) => new(value._value);

    public static ScalarUInt32 Min(ScalarUInt32 left, ScalarUInt32 right) => new(Math.Min(left._value, right._value));

    public static ScalarUInt32 Max(ScalarUInt32 left, ScalarUInt32 right) => new(Math.Max(left._value, right._value));

    public static ScalarUInt32 Clamp(ScalarUInt32 value, ScalarUInt32 min, ScalarUInt32 max) => Min(Max(value, min), max);

    public static ScalarUInt32 operator +(ScalarUInt32 value) => value;

    public static ScalarUInt32 operator +(ScalarUInt32 left, ScalarUInt32 right) => new(unchecked(left._value + right._value));

    public static ScalarUInt32 operator -(ScalarUInt32 left, ScalarUInt32 right) => new(unchecked(left._value - right._value));

    public static ScalarUInt32 operator *(ScalarUInt32 left, ScalarUInt32 right) => new(unchecked(left._value * right._value));

    public static ScalarUInt32 operator -(ScalarUInt32 value) => new(unchecked(0u - value._value));

    public static ScalarUInt32 operator &(ScalarUInt32 left, ScalarUInt32 right) => new(left._value & right._value);

    public static ScalarUInt32 operator |(ScalarUInt32 left, ScalarUInt32 right) => new(left._value | right._value);

    public static ScalarUInt32 operator ^(ScalarUInt32 left, ScalarUInt32 right) => new(left._value ^ right._value);

    public static ScalarUInt32 operator ~(ScalarUInt32 value) => new(~value._value);

    public static ScalarUInt32Mask operator ==(ScalarUInt32 left, ScalarUInt32 right) => new(left._value == right._value);

    public static ScalarUInt32Mask operator !=(ScalarUInt32 left, ScalarUInt32 right) => new(left._value != right._value);

    public static ScalarUInt32Mask operator <(ScalarUInt32 left, ScalarUInt32 right) => new(left._value < right._value);

    public static ScalarUInt32Mask operator >(ScalarUInt32 left, ScalarUInt32 right) => new(left._value > right._value);

    public static ScalarUInt32Mask operator <=(ScalarUInt32 left, ScalarUInt32 right) => new(left._value <= right._value);

    public static ScalarUInt32Mask operator >=(ScalarUInt32 left, ScalarUInt32 right) => new(left._value >= right._value);

    public override bool Equals(object? obj) => obj is ScalarUInt32 other && _value == other._value;

    public override int GetHashCode() => _value.GetHashCode();
}

public readonly struct ScalarUInt32Mask : ISimdPacketMask<ScalarUInt32Mask, uint>
{
    internal readonly bool _value;

    internal ScalarUInt32Mask(bool value) => _value = value;

    public static int LaneCount => 1;

    public static ScalarUInt32Mask True => new(true);

    public static ScalarUInt32Mask False => new(false);

    public static ScalarUInt32Mask Broadcast(bool value) => new(value);

    public static bool All(ScalarUInt32Mask value) => value._value;

    public static bool Any(ScalarUInt32Mask value) => value._value;

    public static bool None(ScalarUInt32Mask value) => !value._value;

    public static ScalarUInt32Mask AndNot(ScalarUInt32Mask left, ScalarUInt32Mask right) => new(left._value & !right._value);

    public static ScalarUInt32Mask Select(ScalarUInt32Mask mask, ScalarUInt32Mask ifTrue, ScalarUInt32Mask ifFalse) => mask._value ? ifTrue : ifFalse;

    public static unsafe ScalarUInt32Mask Load(ReadOnlySpan<uint> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));
        fixed (uint* ptr = values) { return LoadUnsafe(ptr); }
    }

    public static unsafe ScalarUInt32Mask Load(ReadOnlySpan<bool> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));
        fixed (bool* ptr = values) { return LoadBoolUnsafe(ptr); }
    }

    public static unsafe ScalarUInt32Mask LoadAligned(ReadOnlySpan<uint> values) => Load(values);

    public static unsafe void Store(ScalarUInt32Mask value, Span<uint> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));
        fixed (uint* ptr = destination) { StoreUnsafe(value, ptr); }
    }

    public static unsafe void Store(ScalarUInt32Mask value, Span<bool> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));
        fixed (bool* ptr = destination) { StoreBoolUnsafe(value, ptr); }
    }

    public static unsafe void StoreAligned(ScalarUInt32Mask value, Span<uint> destination) => Store(value, destination);

    public static unsafe ScalarUInt32Mask LoadUnsafe(uint* source) => new(source[0] != 0u);

    public static unsafe ScalarUInt32Mask LoadBoolUnsafe(bool* source) => new(source[0]);

    public static unsafe ScalarUInt32Mask LoadAlignedUnsafe(uint* source) => LoadUnsafe(source);

    public static unsafe void StoreUnsafe(ScalarUInt32Mask value, uint* destination) => destination[0] = value._value ? uint.MaxValue : 0u;

    public static unsafe void StoreBoolUnsafe(ScalarUInt32Mask value, bool* destination) => destination[0] = value._value;

    public static unsafe void StoreAlignedUnsafe(ScalarUInt32Mask value, uint* destination) => StoreUnsafe(value, destination);

    public static ScalarUInt32Mask And(ScalarUInt32Mask left, ScalarUInt32Mask right) => left & right;

    public static ScalarUInt32Mask Or(ScalarUInt32Mask left, ScalarUInt32Mask right) => left | right;

    public static ScalarUInt32Mask Xor(ScalarUInt32Mask left, ScalarUInt32Mask right) => left ^ right;

    public static ScalarUInt32Mask Not(ScalarUInt32Mask value) => ~value;

    public static ScalarUInt32Mask operator &(ScalarUInt32Mask left, ScalarUInt32Mask right) => new(left._value & right._value);

    public static ScalarUInt32Mask operator |(ScalarUInt32Mask left, ScalarUInt32Mask right) => new(left._value | right._value);

    public static ScalarUInt32Mask operator ^(ScalarUInt32Mask left, ScalarUInt32Mask right) => new(left._value ^ right._value);

    public static ScalarUInt32Mask operator ~(ScalarUInt32Mask value) => new(!value._value);

    public static ScalarUInt32Mask operator ==(ScalarUInt32Mask left, ScalarUInt32Mask right) => new(left._value == right._value);

    public static ScalarUInt32Mask operator !=(ScalarUInt32Mask left, ScalarUInt32Mask right) => new(left._value != right._value);

    public override bool Equals(object? obj) => obj is ScalarUInt32Mask other && _value == other._value;

    public override int GetHashCode() => _value.GetHashCode();
}
