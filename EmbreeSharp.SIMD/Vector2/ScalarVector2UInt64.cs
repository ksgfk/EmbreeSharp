using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct ScalarVector2UInt64 :
    ISimdIntegerVector2<ScalarVector2UInt64, ScalarUInt64, ulong, ScalarVector2UInt64Mask, ScalarUInt64Mask>
{
    public ScalarVector2UInt64(ScalarUInt64 x, ScalarUInt64 y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => ScalarUInt64.LaneCount;

    public ScalarUInt64 X { get; }

    public ScalarUInt64 Y { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt64 Create(ScalarUInt64 x, ScalarUInt64 y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt64 Broadcast(ulong value)
    {
        ScalarUInt64 packet = ScalarUInt64.Broadcast(value);
        return new(packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt64 Select(ScalarVector2UInt64Mask mask, ScalarVector2UInt64 ifTrue, ScalarVector2UInt64 ifFalse)
    {
        return new(
            ScalarUInt64.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarUInt64.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt64 Select(ScalarUInt64Mask mask, ScalarVector2UInt64 ifTrue, ScalarVector2UInt64 ifFalse)
    {
        return new(
            ScalarUInt64.Select(mask, ifTrue.X, ifFalse.X),
            ScalarUInt64.Select(mask, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt64 operator +(ScalarVector2UInt64 value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt64 operator +(ScalarVector2UInt64 left, ScalarVector2UInt64 right) => new(left.X + right.X, left.Y + right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt64 operator -(ScalarVector2UInt64 left, ScalarVector2UInt64 right) => new(left.X - right.X, left.Y - right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt64 operator *(ScalarVector2UInt64 left, ScalarVector2UInt64 right) => new(left.X * right.X, left.Y * right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt64 operator -(ScalarVector2UInt64 value) => new(-value.X, -value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt64Mask operator ==(ScalarVector2UInt64 left, ScalarVector2UInt64 right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt64Mask operator !=(ScalarVector2UInt64 left, ScalarVector2UInt64 right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector2UInt64 other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}

public readonly struct ScalarVector2UInt64Mask :
    ISimdVector2Mask<ScalarVector2UInt64Mask, ScalarUInt64Mask>
{
    public ScalarVector2UInt64Mask(ScalarUInt64Mask x, ScalarUInt64Mask y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => ScalarUInt64Mask.LaneCount;

    public ScalarUInt64Mask X { get; }

    public ScalarUInt64Mask Y { get; }

    public static ScalarVector2UInt64Mask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarUInt64Mask.True, ScalarUInt64Mask.True);
    }

    public static ScalarVector2UInt64Mask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarUInt64Mask.False, ScalarUInt64Mask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt64Mask Create(ScalarUInt64Mask x, ScalarUInt64Mask y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt64Mask Broadcast(ScalarUInt64Mask value) => new(value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarUInt64Mask All(ScalarVector2UInt64Mask value) => value.X & value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarUInt64Mask Any(ScalarVector2UInt64Mask value) => value.X | value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarUInt64Mask None(ScalarVector2UInt64Mask value) => ~(value.X | value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt64Mask AndNot(ScalarVector2UInt64Mask left, ScalarVector2UInt64Mask right)
    {
        return new(
            ScalarUInt64Mask.AndNot(left.X, right.X),
            ScalarUInt64Mask.AndNot(left.Y, right.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt64Mask Select(ScalarVector2UInt64Mask mask, ScalarVector2UInt64Mask ifTrue, ScalarVector2UInt64Mask ifFalse)
    {
        return new(
            ScalarUInt64Mask.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarUInt64Mask.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt64Mask And(ScalarVector2UInt64Mask left, ScalarVector2UInt64Mask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt64Mask Or(ScalarVector2UInt64Mask left, ScalarVector2UInt64Mask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt64Mask Xor(ScalarVector2UInt64Mask left, ScalarVector2UInt64Mask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt64Mask Not(ScalarVector2UInt64Mask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt64Mask operator &(ScalarVector2UInt64Mask left, ScalarVector2UInt64Mask right) => new(left.X & right.X, left.Y & right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt64Mask operator |(ScalarVector2UInt64Mask left, ScalarVector2UInt64Mask right) => new(left.X | right.X, left.Y | right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt64Mask operator ^(ScalarVector2UInt64Mask left, ScalarVector2UInt64Mask right) => new(left.X ^ right.X, left.Y ^ right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt64Mask operator ~(ScalarVector2UInt64Mask value) => new(~value.X, ~value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt64Mask operator ==(ScalarVector2UInt64Mask left, ScalarVector2UInt64Mask right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt64Mask operator !=(ScalarVector2UInt64Mask left, ScalarVector2UInt64Mask right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector2UInt64Mask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}