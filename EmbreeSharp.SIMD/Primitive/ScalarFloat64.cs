namespace EmbreeSharp.SIMD;

public readonly struct ScalarFloat64 :
    ISimdFloatingPoint<ScalarFloat64, double, ScalarFloat64Mask>
{
    internal readonly double _value;

    internal ScalarFloat64(double value) => _value = value;

    public static int LaneCount => 1;

    public static ScalarFloat64 AdditiveIdentity => new(0d);

    public static ScalarFloat64 MultiplicativeIdentity => new(1d);

    public static ScalarFloat64 Broadcast(double value) => new(value);

    public static unsafe ScalarFloat64 Load(ReadOnlySpan<double> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));
        fixed (double* ptr = values) { return LoadUnsafe(ptr); }
    }

    public static unsafe ScalarFloat64 LoadAligned(ReadOnlySpan<double> values) => Load(values);

    public static unsafe void Store(ScalarFloat64 value, Span<double> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));
        fixed (double* ptr = destination) { StoreUnsafe(value, ptr); }
    }

    public static unsafe void StoreAligned(ScalarFloat64 value, Span<double> destination) => Store(value, destination);

    public static unsafe ScalarFloat64 LoadUnsafe(double* source) => new(source[0]);

    public static unsafe ScalarFloat64 LoadAlignedUnsafe(double* source) => LoadUnsafe(source);

    public static unsafe void StoreUnsafe(ScalarFloat64 value, double* destination) => destination[0] = value._value;

    public static unsafe void StoreAlignedUnsafe(ScalarFloat64 value, double* destination) => StoreUnsafe(value, destination);

    public static ScalarFloat64 Select(ScalarFloat64Mask mask, ScalarFloat64 ifTrue, ScalarFloat64 ifFalse) => mask._value ? ifTrue : ifFalse;

    public static ScalarFloat64 Abs(ScalarFloat64 value) => new(Math.Abs(value._value));

    public static ScalarFloat64 Min(ScalarFloat64 left, ScalarFloat64 right) => new(Math.Min(left._value, right._value));

    public static ScalarFloat64 Max(ScalarFloat64 left, ScalarFloat64 right) => new(Math.Max(left._value, right._value));

    public static ScalarFloat64 Clamp(ScalarFloat64 value, ScalarFloat64 min, ScalarFloat64 max) => Min(Max(value, min), max);

    public static ScalarFloat64 Sqrt(ScalarFloat64 value) => new(Math.Sqrt(value._value));

    public static ScalarFloat64 Reciprocal(ScalarFloat64 value) => new(1d / value._value);

    public static ScalarFloat64 ReciprocalSqrt(ScalarFloat64 value) => new(1d / Math.Sqrt(value._value));

    public static ScalarFloat64 Floor(ScalarFloat64 value) => new(Math.Floor(value._value));

    public static ScalarFloat64 Ceiling(ScalarFloat64 value) => new(Math.Ceiling(value._value));

    public static ScalarFloat64 Truncate(ScalarFloat64 value) => new(Math.Truncate(value._value));

    public static ScalarFloat64 Round(ScalarFloat64 value) => new(Math.Round(value._value));

    public static ScalarFloat64 Sin(ScalarFloat64 value) => new(Math.Sin(value._value));

    public static ScalarFloat64 Cos(ScalarFloat64 value) => new(Math.Cos(value._value));

    public static (ScalarFloat64 Sin, ScalarFloat64 Cos) SinCos(ScalarFloat64 value) => (Sin(value), Cos(value));

    public static ScalarFloat64 FusedMultiplyAdd(ScalarFloat64 left, ScalarFloat64 right, ScalarFloat64 addend) => new(Math.FusedMultiplyAdd(left._value, right._value, addend._value));

    public static ScalarFloat64 CopySign(ScalarFloat64 value, ScalarFloat64 sign) => new(double.CopySign(value._value, sign._value));

    public static ScalarFloat64Mask IsFinite(ScalarFloat64 value) => new(double.IsFinite(value._value));

    public static ScalarFloat64Mask IsInfinity(ScalarFloat64 value) => new(double.IsInfinity(value._value));

    public static ScalarFloat64Mask IsNaN(ScalarFloat64 value) => new(double.IsNaN(value._value));

    public static ScalarFloat64 operator +(ScalarFloat64 value) => value;

    public static ScalarFloat64 operator +(ScalarFloat64 left, ScalarFloat64 right) => new(left._value + right._value);

    public static ScalarFloat64 operator -(ScalarFloat64 left, ScalarFloat64 right) => new(left._value - right._value);

    public static ScalarFloat64 operator *(ScalarFloat64 left, ScalarFloat64 right) => new(left._value * right._value);

    public static ScalarFloat64 operator /(ScalarFloat64 left, ScalarFloat64 right) => new(left._value / right._value);

    public static ScalarFloat64 operator -(ScalarFloat64 value) => new(-value._value);

    public static ScalarFloat64Mask operator ==(ScalarFloat64 left, ScalarFloat64 right) => new(left._value == right._value);

    public static ScalarFloat64Mask operator !=(ScalarFloat64 left, ScalarFloat64 right) => new(left._value != right._value);

    public static ScalarFloat64Mask operator <(ScalarFloat64 left, ScalarFloat64 right) => new(left._value < right._value);

    public static ScalarFloat64Mask operator >(ScalarFloat64 left, ScalarFloat64 right) => new(left._value > right._value);

    public static ScalarFloat64Mask operator <=(ScalarFloat64 left, ScalarFloat64 right) => new(left._value <= right._value);

    public static ScalarFloat64Mask operator >=(ScalarFloat64 left, ScalarFloat64 right) => new(left._value >= right._value);

    public override bool Equals(object? obj) => obj is ScalarFloat64 other && _value.Equals(other._value);

    public override int GetHashCode() => _value.GetHashCode();
}

public readonly struct ScalarFloat64Mask : ISimdPacketMask<ScalarFloat64Mask, double>
{
    internal readonly bool _value;

    internal ScalarFloat64Mask(bool value) => _value = value;

    public static int LaneCount => 1;

    public static ScalarFloat64Mask True => new(true);

    public static ScalarFloat64Mask False => new(false);

    public static ScalarFloat64Mask Broadcast(bool value) => new(value);

    public static bool All(ScalarFloat64Mask value) => value._value;

    public static bool Any(ScalarFloat64Mask value) => value._value;

    public static bool None(ScalarFloat64Mask value) => !value._value;

    public static ScalarFloat64Mask AndNot(ScalarFloat64Mask left, ScalarFloat64Mask right) => new(left._value & !right._value);

    public static ScalarFloat64Mask Select(ScalarFloat64Mask mask, ScalarFloat64Mask ifTrue, ScalarFloat64Mask ifFalse) => mask._value ? ifTrue : ifFalse;

    public static unsafe ScalarFloat64Mask Load(ReadOnlySpan<double> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));
        fixed (double* ptr = values) { return LoadUnsafe(ptr); }
    }

    public static unsafe ScalarFloat64Mask Load(ReadOnlySpan<bool> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));
        fixed (bool* ptr = values) { return LoadBoolUnsafe(ptr); }
    }

    public static unsafe ScalarFloat64Mask LoadAligned(ReadOnlySpan<double> values) => Load(values);

    public static unsafe void Store(ScalarFloat64Mask value, Span<double> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));
        fixed (double* ptr = destination) { StoreUnsafe(value, ptr); }
    }

    public static unsafe void Store(ScalarFloat64Mask value, Span<bool> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));
        fixed (bool* ptr = destination) { StoreBoolUnsafe(value, ptr); }
    }

    public static unsafe void StoreAligned(ScalarFloat64Mask value, Span<double> destination) => Store(value, destination);

    public static unsafe ScalarFloat64Mask LoadUnsafe(double* source) => new(BitConverter.DoubleToInt64Bits(source[0]) != 0);

    public static unsafe ScalarFloat64Mask LoadBoolUnsafe(bool* source) => new(source[0]);

    public static unsafe ScalarFloat64Mask LoadAlignedUnsafe(double* source) => LoadUnsafe(source);

    public static unsafe void StoreUnsafe(ScalarFloat64Mask value, double* destination) => destination[0] = value._value ? BitConverter.Int64BitsToDouble(-1L) : 0d;

    public static unsafe void StoreBoolUnsafe(ScalarFloat64Mask value, bool* destination) => destination[0] = value._value;

    public static unsafe void StoreAlignedUnsafe(ScalarFloat64Mask value, double* destination) => StoreUnsafe(value, destination);

    public static ScalarFloat64Mask And(ScalarFloat64Mask left, ScalarFloat64Mask right) => left & right;

    public static ScalarFloat64Mask Or(ScalarFloat64Mask left, ScalarFloat64Mask right) => left | right;

    public static ScalarFloat64Mask Xor(ScalarFloat64Mask left, ScalarFloat64Mask right) => left ^ right;

    public static ScalarFloat64Mask Not(ScalarFloat64Mask value) => ~value;

    public static ScalarFloat64Mask operator &(ScalarFloat64Mask left, ScalarFloat64Mask right) => new(left._value & right._value);

    public static ScalarFloat64Mask operator |(ScalarFloat64Mask left, ScalarFloat64Mask right) => new(left._value | right._value);

    public static ScalarFloat64Mask operator ^(ScalarFloat64Mask left, ScalarFloat64Mask right) => new(left._value ^ right._value);

    public static ScalarFloat64Mask operator ~(ScalarFloat64Mask value) => new(!value._value);

    public static ScalarFloat64Mask operator ==(ScalarFloat64Mask left, ScalarFloat64Mask right) => new(left._value == right._value);

    public static ScalarFloat64Mask operator !=(ScalarFloat64Mask left, ScalarFloat64Mask right) => new(left._value != right._value);

    public override bool Equals(object? obj) => obj is ScalarFloat64Mask other && _value == other._value;

    public override int GetHashCode() => _value.GetHashCode();
}
