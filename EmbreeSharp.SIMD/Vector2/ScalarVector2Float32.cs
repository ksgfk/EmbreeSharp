using System.Runtime.CompilerServices;
using System.Numerics;

namespace EmbreeSharp.SIMD;

public readonly struct ScalarVector2Float32 :
    ISimdFloatingPointVector2<ScalarVector2Float32, ScalarFloat32, float, ScalarVector2Float32Mask, ScalarFloat32Mask>
{
    internal readonly Vector2 _value;

    public ScalarVector2Float32(ScalarFloat32 x, ScalarFloat32 y) => _value = new Vector2(x._value, y._value);

    internal ScalarVector2Float32(Vector2 value) => _value = value;

    public static int LaneCount => ScalarFloat32.LaneCount;

    public ScalarFloat32 X => new(_value.X);

    public ScalarFloat32 Y => new(_value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float32 Create(ScalarFloat32 x, ScalarFloat32 y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float32 Broadcast(float value) => new(new Vector2(value));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float32 Select(ScalarVector2Float32Mask mask, ScalarVector2Float32 ifTrue, ScalarVector2Float32 ifFalse)
    {
        return new(
            ScalarFloat32.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarFloat32.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float32 Select(ScalarFloat32Mask mask, ScalarVector2Float32 ifTrue, ScalarVector2Float32 ifFalse)
    {
        return new(
            ScalarFloat32.Select(mask, ifTrue.X, ifFalse.X),
            ScalarFloat32.Select(mask, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFloat32 Dot(ScalarVector2Float32 left, ScalarVector2Float32 right) => ScalarFloat32.FusedMultiplyAdd(left.Y, right.Y, left.X * right.X);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float32 operator +(ScalarVector2Float32 value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float32 operator +(ScalarVector2Float32 left, ScalarVector2Float32 right) => new(left._value + right._value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float32 operator -(ScalarVector2Float32 left, ScalarVector2Float32 right) => new(left._value - right._value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float32 operator *(ScalarVector2Float32 left, ScalarVector2Float32 right) => new(left._value * right._value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float32 operator -(ScalarVector2Float32 value) => new(-value._value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float32Mask operator ==(ScalarVector2Float32 left, ScalarVector2Float32 right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float32Mask operator !=(ScalarVector2Float32 left, ScalarVector2Float32 right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj) => obj is ScalarVector2Float32 other && _value.Equals(other._value);

    public override int GetHashCode() => _value.GetHashCode();
}

public readonly struct ScalarVector2Float32Mask :
    ISimdVector2Mask<ScalarVector2Float32Mask, ScalarFloat32Mask>
{
    public ScalarVector2Float32Mask(ScalarFloat32Mask x, ScalarFloat32Mask y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => ScalarFloat32Mask.LaneCount;

    public ScalarFloat32Mask X { get; }

    public ScalarFloat32Mask Y { get; }

    public static ScalarVector2Float32Mask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarFloat32Mask.True, ScalarFloat32Mask.True);
    }

    public static ScalarVector2Float32Mask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarFloat32Mask.False, ScalarFloat32Mask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float32Mask Create(ScalarFloat32Mask x, ScalarFloat32Mask y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float32Mask Broadcast(ScalarFloat32Mask value) => new(value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFloat32Mask All(ScalarVector2Float32Mask value) => value.X & value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFloat32Mask Any(ScalarVector2Float32Mask value) => value.X | value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFloat32Mask None(ScalarVector2Float32Mask value) => ~(value.X | value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float32Mask AndNot(ScalarVector2Float32Mask left, ScalarVector2Float32Mask right)
    {
        return new(
            ScalarFloat32Mask.AndNot(left.X, right.X),
            ScalarFloat32Mask.AndNot(left.Y, right.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float32Mask Select(ScalarVector2Float32Mask mask, ScalarVector2Float32Mask ifTrue, ScalarVector2Float32Mask ifFalse)
    {
        return new(
            ScalarFloat32Mask.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarFloat32Mask.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float32Mask And(ScalarVector2Float32Mask left, ScalarVector2Float32Mask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float32Mask Or(ScalarVector2Float32Mask left, ScalarVector2Float32Mask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float32Mask Xor(ScalarVector2Float32Mask left, ScalarVector2Float32Mask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float32Mask Not(ScalarVector2Float32Mask value) => ~value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float32Mask operator &(ScalarVector2Float32Mask left, ScalarVector2Float32Mask right) => new(left.X & right.X, left.Y & right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float32Mask operator |(ScalarVector2Float32Mask left, ScalarVector2Float32Mask right) => new(left.X | right.X, left.Y | right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float32Mask operator ^(ScalarVector2Float32Mask left, ScalarVector2Float32Mask right) => new(left.X ^ right.X, left.Y ^ right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float32Mask operator ~(ScalarVector2Float32Mask value) => new(~value.X, ~value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float32Mask operator ==(ScalarVector2Float32Mask left, ScalarVector2Float32Mask right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float32Mask operator !=(ScalarVector2Float32Mask left, ScalarVector2Float32Mask right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj) => obj is ScalarVector2Float32Mask other && X.Equals(other.X) && Y.Equals(other.Y);

    public override int GetHashCode() => HashCode.Combine(X, Y);
}
