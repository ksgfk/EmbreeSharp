using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct ScalarVector2Float64 :
    ISimdFloatingPointVector2<ScalarVector2Float64, ScalarFloat64, double, ScalarVector2Float64Mask, ScalarFloat64Mask>
{
    public ScalarVector2Float64(ScalarFloat64 x, ScalarFloat64 y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => ScalarFloat64.LaneCount;

    public ScalarFloat64 X { get; }

    public ScalarFloat64 Y { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float64 Create(ScalarFloat64 x, ScalarFloat64 y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float64 Broadcast(double value)
    {
        ScalarFloat64 packet = ScalarFloat64.Broadcast(value);
        return new(packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float64 Select(ScalarVector2Float64Mask mask, ScalarVector2Float64 ifTrue, ScalarVector2Float64 ifFalse)
    {
        return new(
            ScalarFloat64.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarFloat64.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float64 Select(ScalarFloat64Mask mask, ScalarVector2Float64 ifTrue, ScalarVector2Float64 ifFalse)
    {
        return new(
            ScalarFloat64.Select(mask, ifTrue.X, ifFalse.X),
            ScalarFloat64.Select(mask, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFloat64 Dot(ScalarVector2Float64 left, ScalarVector2Float64 right)
    {
        return ScalarFloat64.FusedMultiplyAdd(left.Y, right.Y, left.X * right.X);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float64 operator +(ScalarVector2Float64 value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float64 operator +(ScalarVector2Float64 left, ScalarVector2Float64 right) => new(left.X + right.X, left.Y + right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float64 operator -(ScalarVector2Float64 left, ScalarVector2Float64 right) => new(left.X - right.X, left.Y - right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float64 operator *(ScalarVector2Float64 left, ScalarVector2Float64 right) => new(left.X * right.X, left.Y * right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float64 operator -(ScalarVector2Float64 value) => new(-value.X, -value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float64Mask operator ==(ScalarVector2Float64 left, ScalarVector2Float64 right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float64Mask operator !=(ScalarVector2Float64 left, ScalarVector2Float64 right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector2Float64 other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}

public readonly struct ScalarVector2Float64Mask :
    ISimdVector2Mask<ScalarVector2Float64Mask, ScalarFloat64Mask>
{
    public ScalarVector2Float64Mask(ScalarFloat64Mask x, ScalarFloat64Mask y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => ScalarFloat64Mask.LaneCount;

    public ScalarFloat64Mask X { get; }

    public ScalarFloat64Mask Y { get; }

    public static ScalarVector2Float64Mask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarFloat64Mask.True, ScalarFloat64Mask.True);
    }

    public static ScalarVector2Float64Mask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarFloat64Mask.False, ScalarFloat64Mask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float64Mask Create(ScalarFloat64Mask x, ScalarFloat64Mask y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float64Mask Broadcast(ScalarFloat64Mask value) => new(value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFloat64Mask All(ScalarVector2Float64Mask value) => value.X & value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFloat64Mask Any(ScalarVector2Float64Mask value) => value.X | value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFloat64Mask None(ScalarVector2Float64Mask value) => ~(value.X | value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float64Mask AndNot(ScalarVector2Float64Mask left, ScalarVector2Float64Mask right)
    {
        return new(
            ScalarFloat64Mask.AndNot(left.X, right.X),
            ScalarFloat64Mask.AndNot(left.Y, right.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float64Mask Select(ScalarVector2Float64Mask mask, ScalarVector2Float64Mask ifTrue, ScalarVector2Float64Mask ifFalse)
    {
        return new(
            ScalarFloat64Mask.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarFloat64Mask.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float64Mask And(ScalarVector2Float64Mask left, ScalarVector2Float64Mask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float64Mask Or(ScalarVector2Float64Mask left, ScalarVector2Float64Mask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float64Mask Xor(ScalarVector2Float64Mask left, ScalarVector2Float64Mask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float64Mask Not(ScalarVector2Float64Mask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float64Mask operator &(ScalarVector2Float64Mask left, ScalarVector2Float64Mask right) => new(left.X & right.X, left.Y & right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float64Mask operator |(ScalarVector2Float64Mask left, ScalarVector2Float64Mask right) => new(left.X | right.X, left.Y | right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float64Mask operator ^(ScalarVector2Float64Mask left, ScalarVector2Float64Mask right) => new(left.X ^ right.X, left.Y ^ right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float64Mask operator ~(ScalarVector2Float64Mask value) => new(~value.X, ~value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float64Mask operator ==(ScalarVector2Float64Mask left, ScalarVector2Float64Mask right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Float64Mask operator !=(ScalarVector2Float64Mask left, ScalarVector2Float64Mask right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector2Float64Mask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}