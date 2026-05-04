using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct ScalarVector2Int32 :
    ISimdIntegerVector2<ScalarVector2Int32, ScalarInt32, int, ScalarVector2Int32Mask, ScalarInt32Mask>
{
    public ScalarVector2Int32(ScalarInt32 x, ScalarInt32 y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => ScalarInt32.LaneCount;

    public ScalarInt32 X { get; }

    public ScalarInt32 Y { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int32 Create(ScalarInt32 x, ScalarInt32 y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int32 Broadcast(int value)
    {
        ScalarInt32 packet = ScalarInt32.Broadcast(value);
        return new(packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int32 Select(ScalarVector2Int32Mask mask, ScalarVector2Int32 ifTrue, ScalarVector2Int32 ifFalse)
    {
        return new(
            ScalarInt32.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarInt32.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int32 Select(ScalarInt32Mask mask, ScalarVector2Int32 ifTrue, ScalarVector2Int32 ifFalse)
    {
        return new(
            ScalarInt32.Select(mask, ifTrue.X, ifFalse.X),
            ScalarInt32.Select(mask, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int32 operator +(ScalarVector2Int32 value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int32 operator +(ScalarVector2Int32 left, ScalarVector2Int32 right) => new(left.X + right.X, left.Y + right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int32 operator -(ScalarVector2Int32 left, ScalarVector2Int32 right) => new(left.X - right.X, left.Y - right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int32 operator *(ScalarVector2Int32 left, ScalarVector2Int32 right) => new(left.X * right.X, left.Y * right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int32 operator -(ScalarVector2Int32 value) => new(-value.X, -value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int32Mask operator ==(ScalarVector2Int32 left, ScalarVector2Int32 right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int32Mask operator !=(ScalarVector2Int32 left, ScalarVector2Int32 right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector2Int32 other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}

public readonly struct ScalarVector2Int32Mask :
    ISimdVector2Mask<ScalarVector2Int32Mask, ScalarInt32Mask>
{
    public ScalarVector2Int32Mask(ScalarInt32Mask x, ScalarInt32Mask y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => ScalarInt32Mask.LaneCount;

    public ScalarInt32Mask X { get; }

    public ScalarInt32Mask Y { get; }

    public static ScalarVector2Int32Mask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarInt32Mask.True, ScalarInt32Mask.True);
    }

    public static ScalarVector2Int32Mask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarInt32Mask.False, ScalarInt32Mask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int32Mask Create(ScalarInt32Mask x, ScalarInt32Mask y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int32Mask Broadcast(ScalarInt32Mask value) => new(value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarInt32Mask All(ScalarVector2Int32Mask value) => value.X & value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarInt32Mask Any(ScalarVector2Int32Mask value) => value.X | value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarInt32Mask None(ScalarVector2Int32Mask value) => ~(value.X | value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int32Mask AndNot(ScalarVector2Int32Mask left, ScalarVector2Int32Mask right)
    {
        return new(
            ScalarInt32Mask.AndNot(left.X, right.X),
            ScalarInt32Mask.AndNot(left.Y, right.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int32Mask Select(ScalarVector2Int32Mask mask, ScalarVector2Int32Mask ifTrue, ScalarVector2Int32Mask ifFalse)
    {
        return new(
            ScalarInt32Mask.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarInt32Mask.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int32Mask And(ScalarVector2Int32Mask left, ScalarVector2Int32Mask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int32Mask Or(ScalarVector2Int32Mask left, ScalarVector2Int32Mask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int32Mask Xor(ScalarVector2Int32Mask left, ScalarVector2Int32Mask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int32Mask Not(ScalarVector2Int32Mask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int32Mask operator &(ScalarVector2Int32Mask left, ScalarVector2Int32Mask right) => new(left.X & right.X, left.Y & right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int32Mask operator |(ScalarVector2Int32Mask left, ScalarVector2Int32Mask right) => new(left.X | right.X, left.Y | right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int32Mask operator ^(ScalarVector2Int32Mask left, ScalarVector2Int32Mask right) => new(left.X ^ right.X, left.Y ^ right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int32Mask operator ~(ScalarVector2Int32Mask value) => new(~value.X, ~value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int32Mask operator ==(ScalarVector2Int32Mask left, ScalarVector2Int32Mask right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2Int32Mask operator !=(ScalarVector2Int32Mask left, ScalarVector2Int32Mask right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector2Int32Mask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}