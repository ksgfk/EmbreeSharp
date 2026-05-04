namespace EmbreeSharp.SIMD;

public readonly struct ScalarFloat32 :
    ISimdFloatingPoint<ScalarFloat32, float, ScalarFloat32Mask>
{
    internal readonly float _value;

    internal ScalarFloat32(float value) => _value = value;

    public static int LaneCount => 1;

    public static ScalarFloat32 AdditiveIdentity => new(0f);

    public static ScalarFloat32 MultiplicativeIdentity => new(1f);

    public static ScalarFloat32 Broadcast(float value) => new(value);

    public static unsafe ScalarFloat32 Load(ReadOnlySpan<float> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));
        fixed (float* ptr = values) { return LoadUnsafe(ptr); }
    }

    public static unsafe ScalarFloat32 LoadAligned(ReadOnlySpan<float> values) => Load(values);

    public static unsafe void Store(ScalarFloat32 value, Span<float> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));
        fixed (float* ptr = destination) { StoreUnsafe(value, ptr); }
    }

    public static unsafe void StoreAligned(ScalarFloat32 value, Span<float> destination) => Store(value, destination);

    public static unsafe ScalarFloat32 LoadUnsafe(float* source) => new(source[0]);

    public static unsafe ScalarFloat32 LoadAlignedUnsafe(float* source) => LoadUnsafe(source);

    public static unsafe void StoreUnsafe(ScalarFloat32 value, float* destination) => destination[0] = value._value;

    public static unsafe void StoreAlignedUnsafe(ScalarFloat32 value, float* destination) => StoreUnsafe(value, destination);

    public static ScalarFloat32 Select(ScalarFloat32Mask mask, ScalarFloat32 ifTrue, ScalarFloat32 ifFalse) => mask._value ? ifTrue : ifFalse;

    public static ScalarFloat32 Abs(ScalarFloat32 value) => new(MathF.Abs(value._value));

    public static ScalarFloat32 Min(ScalarFloat32 left, ScalarFloat32 right) => new(MathF.Min(left._value, right._value));

    public static ScalarFloat32 Max(ScalarFloat32 left, ScalarFloat32 right) => new(MathF.Max(left._value, right._value));

    public static ScalarFloat32 Clamp(ScalarFloat32 value, ScalarFloat32 min, ScalarFloat32 max) => Min(Max(value, min), max);

    public static ScalarFloat32 Sqrt(ScalarFloat32 value) => new(MathF.Sqrt(value._value));

    public static ScalarFloat32 Reciprocal(ScalarFloat32 value) => new(1f / value._value);

    public static ScalarFloat32 ReciprocalSqrt(ScalarFloat32 value) => new(1f / MathF.Sqrt(value._value));

    public static ScalarFloat32 Floor(ScalarFloat32 value) => new(MathF.Floor(value._value));

    public static ScalarFloat32 Ceiling(ScalarFloat32 value) => new(MathF.Ceiling(value._value));

    public static ScalarFloat32 Truncate(ScalarFloat32 value) => new(MathF.Truncate(value._value));

    public static ScalarFloat32 Round(ScalarFloat32 value) => new(MathF.Round(value._value));

    public static ScalarFloat32 Sin(ScalarFloat32 value) => new(MathF.Sin(value._value));

    public static ScalarFloat32 Cos(ScalarFloat32 value) => new(MathF.Cos(value._value));

    public static (ScalarFloat32 Sin, ScalarFloat32 Cos) SinCos(ScalarFloat32 value) => (Sin(value), Cos(value));

    public static ScalarFloat32 FusedMultiplyAdd(ScalarFloat32 left, ScalarFloat32 right, ScalarFloat32 addend) => new(MathF.FusedMultiplyAdd(left._value, right._value, addend._value));

    public static ScalarFloat32 CopySign(ScalarFloat32 value, ScalarFloat32 sign) => new(float.CopySign(value._value, sign._value));

    public static ScalarFloat32Mask IsFinite(ScalarFloat32 value) => new(float.IsFinite(value._value));

    public static ScalarFloat32Mask IsInfinity(ScalarFloat32 value) => new(float.IsInfinity(value._value));

    public static ScalarFloat32Mask IsNaN(ScalarFloat32 value) => new(float.IsNaN(value._value));

    public static ScalarFloat32 operator +(ScalarFloat32 value) => value;

    public static ScalarFloat32 operator +(ScalarFloat32 left, ScalarFloat32 right) => new(left._value + right._value);

    public static ScalarFloat32 operator -(ScalarFloat32 left, ScalarFloat32 right) => new(left._value - right._value);

    public static ScalarFloat32 operator *(ScalarFloat32 left, ScalarFloat32 right) => new(left._value * right._value);

    public static ScalarFloat32 operator /(ScalarFloat32 left, ScalarFloat32 right) => new(left._value / right._value);

    public static ScalarFloat32 operator -(ScalarFloat32 value) => new(-value._value);

    public static ScalarFloat32Mask operator ==(ScalarFloat32 left, ScalarFloat32 right) => new(left._value == right._value);

    public static ScalarFloat32Mask operator !=(ScalarFloat32 left, ScalarFloat32 right) => new(left._value != right._value);

    public static ScalarFloat32Mask operator <(ScalarFloat32 left, ScalarFloat32 right) => new(left._value < right._value);

    public static ScalarFloat32Mask operator >(ScalarFloat32 left, ScalarFloat32 right) => new(left._value > right._value);

    public static ScalarFloat32Mask operator <=(ScalarFloat32 left, ScalarFloat32 right) => new(left._value <= right._value);

    public static ScalarFloat32Mask operator >=(ScalarFloat32 left, ScalarFloat32 right) => new(left._value >= right._value);

    public override bool Equals(object? obj) => obj is ScalarFloat32 other && _value.Equals(other._value);

    public override int GetHashCode() => _value.GetHashCode();
}

public readonly struct ScalarFloat32Mask : ISimdPacketMask<ScalarFloat32Mask, float>
{
    internal readonly bool _value;

    internal ScalarFloat32Mask(bool value) => _value = value;

    public static int LaneCount => 1;

    public static ScalarFloat32Mask True => new(true);

    public static ScalarFloat32Mask False => new(false);

    public static ScalarFloat32Mask Broadcast(bool value) => new(value);

    public static bool All(ScalarFloat32Mask value) => value._value;

    public static bool Any(ScalarFloat32Mask value) => value._value;

    public static bool None(ScalarFloat32Mask value) => !value._value;

    public static ScalarFloat32Mask AndNot(ScalarFloat32Mask left, ScalarFloat32Mask right) => new(left._value & !right._value);

    public static ScalarFloat32Mask Select(ScalarFloat32Mask mask, ScalarFloat32Mask ifTrue, ScalarFloat32Mask ifFalse) => mask._value ? ifTrue : ifFalse;

    public static unsafe ScalarFloat32Mask Load(ReadOnlySpan<float> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));
        fixed (float* ptr = values) { return LoadUnsafe(ptr); }
    }

    public static unsafe ScalarFloat32Mask Load(ReadOnlySpan<bool> values)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(values.Length, LaneCount, nameof(values));
        fixed (bool* ptr = values) { return LoadBoolUnsafe(ptr); }
    }

    public static unsafe ScalarFloat32Mask LoadAligned(ReadOnlySpan<float> values) => Load(values);

    public static unsafe void Store(ScalarFloat32Mask value, Span<float> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));
        fixed (float* ptr = destination) { StoreUnsafe(value, ptr); }
    }

    public static unsafe void Store(ScalarFloat32Mask value, Span<bool> destination)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(destination.Length, LaneCount, nameof(destination));
        fixed (bool* ptr = destination) { StoreBoolUnsafe(value, ptr); }
    }

    public static unsafe void StoreAligned(ScalarFloat32Mask value, Span<float> destination) => Store(value, destination);

    public static unsafe ScalarFloat32Mask LoadUnsafe(float* source) => new(BitConverter.SingleToInt32Bits(source[0]) != 0);

    public static unsafe ScalarFloat32Mask LoadBoolUnsafe(bool* source) => new(source[0]);

    public static unsafe ScalarFloat32Mask LoadAlignedUnsafe(float* source) => LoadUnsafe(source);

    public static unsafe void StoreUnsafe(ScalarFloat32Mask value, float* destination) => destination[0] = value._value ? BitConverter.Int32BitsToSingle(-1) : 0f;

    public static unsafe void StoreBoolUnsafe(ScalarFloat32Mask value, bool* destination) => destination[0] = value._value;

    public static unsafe void StoreAlignedUnsafe(ScalarFloat32Mask value, float* destination) => StoreUnsafe(value, destination);

    public static ScalarFloat32Mask And(ScalarFloat32Mask left, ScalarFloat32Mask right) => left & right;

    public static ScalarFloat32Mask Or(ScalarFloat32Mask left, ScalarFloat32Mask right) => left | right;

    public static ScalarFloat32Mask Xor(ScalarFloat32Mask left, ScalarFloat32Mask right) => left ^ right;

    public static ScalarFloat32Mask Not(ScalarFloat32Mask value) => ~value;

    public static ScalarFloat32Mask operator &(ScalarFloat32Mask left, ScalarFloat32Mask right) => new(left._value & right._value);

    public static ScalarFloat32Mask operator |(ScalarFloat32Mask left, ScalarFloat32Mask right) => new(left._value | right._value);

    public static ScalarFloat32Mask operator ^(ScalarFloat32Mask left, ScalarFloat32Mask right) => new(left._value ^ right._value);

    public static ScalarFloat32Mask operator ~(ScalarFloat32Mask value) => new(!value._value);

    public static ScalarFloat32Mask operator ==(ScalarFloat32Mask left, ScalarFloat32Mask right) => new(left._value == right._value);

    public static ScalarFloat32Mask operator !=(ScalarFloat32Mask left, ScalarFloat32Mask right) => new(left._value != right._value);

    public override bool Equals(object? obj) => obj is ScalarFloat32Mask other && _value == other._value;

    public override int GetHashCode() => _value.GetHashCode();
}
