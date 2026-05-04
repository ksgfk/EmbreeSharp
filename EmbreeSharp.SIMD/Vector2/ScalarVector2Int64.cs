using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct ScalarVector2Int64 :
    ISimdIntegerVector2<ScalarVector2Int64, ScalarInt64, long, ScalarVector2Int64Mask, ScalarInt64Mask>
{
    public ScalarVector2Int64(ScalarInt64 x, ScalarInt64 y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => ScalarInt64.LaneCount;

    public ScalarInt64 X { get; }

    public ScalarInt64 Y { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int64 Create(ScalarInt64 x, ScalarInt64 y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int64 Broadcast(long value)
    {
        ScalarInt64 packet = ScalarInt64.Broadcast(value);
        return new(packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int64 Select(ScalarVector2Int64Mask mask, ScalarVector2Int64 ifTrue, ScalarVector2Int64 ifFalse)
    {
        return new(
            ScalarInt64.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarInt64.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int64 Select(ScalarInt64Mask mask, ScalarVector2Int64 ifTrue, ScalarVector2Int64 ifFalse)
    {
        return new(
            ScalarInt64.Select(mask, ifTrue.X, ifFalse.X),
            ScalarInt64.Select(mask, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int64 operator +(ScalarVector2Int64 value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int64 operator +(ScalarVector2Int64 left, ScalarVector2Int64 right) => new(left.X + right.X, left.Y + right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int64 operator -(ScalarVector2Int64 left, ScalarVector2Int64 right) => new(left.X - right.X, left.Y - right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int64 operator *(ScalarVector2Int64 left, ScalarVector2Int64 right) => new(left.X * right.X, left.Y * right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int64 operator -(ScalarVector2Int64 value) => new(-value.X, -value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int64Mask operator ==(ScalarVector2Int64 left, ScalarVector2Int64 right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int64Mask operator !=(ScalarVector2Int64 left, ScalarVector2Int64 right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector2Int64 other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}

public readonly struct ScalarVector2Int64Mask :
    ISimdVector2Mask<ScalarVector2Int64Mask, ScalarInt64Mask>
{
    public ScalarVector2Int64Mask(ScalarInt64Mask x, ScalarInt64Mask y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => ScalarInt64Mask.LaneCount;

    public ScalarInt64Mask X { get; }

    public ScalarInt64Mask Y { get; }

    public static ScalarVector2Int64Mask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarInt64Mask.True, ScalarInt64Mask.True);
    }

    public static ScalarVector2Int64Mask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarInt64Mask.False, ScalarInt64Mask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int64Mask Create(ScalarInt64Mask x, ScalarInt64Mask y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int64Mask Broadcast(ScalarInt64Mask value) => new(value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarInt64Mask All(ScalarVector2Int64Mask value) => value.X & value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarInt64Mask Any(ScalarVector2Int64Mask value) => value.X | value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarInt64Mask None(ScalarVector2Int64Mask value) => ~(value.X | value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int64Mask AndNot(ScalarVector2Int64Mask left, ScalarVector2Int64Mask right)
    {
        return new(
            ScalarInt64Mask.AndNot(left.X, right.X),
            ScalarInt64Mask.AndNot(left.Y, right.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int64Mask Select(ScalarVector2Int64Mask mask, ScalarVector2Int64Mask ifTrue, ScalarVector2Int64Mask ifFalse)
    {
        return new(
            ScalarInt64Mask.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarInt64Mask.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int64Mask And(ScalarVector2Int64Mask left, ScalarVector2Int64Mask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int64Mask Or(ScalarVector2Int64Mask left, ScalarVector2Int64Mask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int64Mask Xor(ScalarVector2Int64Mask left, ScalarVector2Int64Mask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int64Mask Not(ScalarVector2Int64Mask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int64Mask operator &(ScalarVector2Int64Mask left, ScalarVector2Int64Mask right) => new(left.X & right.X, left.Y & right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int64Mask operator |(ScalarVector2Int64Mask left, ScalarVector2Int64Mask right) => new(left.X | right.X, left.Y | right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int64Mask operator ^(ScalarVector2Int64Mask left, ScalarVector2Int64Mask right) => new(left.X ^ right.X, left.Y ^ right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int64Mask operator ~(ScalarVector2Int64Mask value) => new(~value.X, ~value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int64Mask operator ==(ScalarVector2Int64Mask left, ScalarVector2Int64Mask right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int64Mask operator !=(ScalarVector2Int64Mask left, ScalarVector2Int64Mask right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector2Int64Mask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}