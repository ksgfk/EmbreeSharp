using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct ScalarVector2UInt32 :
    ISimdIntegerVector2<ScalarVector2UInt32, ScalarUInt32, uint, ScalarVector2UInt32Mask, ScalarUInt32Mask>
{
    public ScalarVector2UInt32(ScalarUInt32 x, ScalarUInt32 y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => ScalarUInt32.LaneCount;

    public ScalarUInt32 X { get; }

    public ScalarUInt32 Y { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt32 Create(ScalarUInt32 x, ScalarUInt32 y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt32 Broadcast(uint value)
    {
        ScalarUInt32 packet = ScalarUInt32.Broadcast(value);
        return new(packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt32 Select(ScalarVector2UInt32Mask mask, ScalarVector2UInt32 ifTrue, ScalarVector2UInt32 ifFalse)
    {
        return new(
            ScalarUInt32.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarUInt32.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt32 Select(ScalarUInt32Mask mask, ScalarVector2UInt32 ifTrue, ScalarVector2UInt32 ifFalse)
    {
        return new(
            ScalarUInt32.Select(mask, ifTrue.X, ifFalse.X),
            ScalarUInt32.Select(mask, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt32 operator +(ScalarVector2UInt32 value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt32 operator +(ScalarVector2UInt32 left, ScalarVector2UInt32 right) => new(left.X + right.X, left.Y + right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt32 operator -(ScalarVector2UInt32 left, ScalarVector2UInt32 right) => new(left.X - right.X, left.Y - right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt32 operator *(ScalarVector2UInt32 left, ScalarVector2UInt32 right) => new(left.X * right.X, left.Y * right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt32 operator -(ScalarVector2UInt32 value) => new(-value.X, -value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt32Mask operator ==(ScalarVector2UInt32 left, ScalarVector2UInt32 right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt32Mask operator !=(ScalarVector2UInt32 left, ScalarVector2UInt32 right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector2UInt32 other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}

public readonly struct ScalarVector2UInt32Mask :
    ISimdVector2Mask<ScalarVector2UInt32Mask, ScalarUInt32Mask>
{
    public ScalarVector2UInt32Mask(ScalarUInt32Mask x, ScalarUInt32Mask y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => ScalarUInt32Mask.LaneCount;

    public ScalarUInt32Mask X { get; }

    public ScalarUInt32Mask Y { get; }

    public static ScalarVector2UInt32Mask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarUInt32Mask.True, ScalarUInt32Mask.True);
    }

    public static ScalarVector2UInt32Mask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarUInt32Mask.False, ScalarUInt32Mask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt32Mask Create(ScalarUInt32Mask x, ScalarUInt32Mask y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt32Mask Broadcast(ScalarUInt32Mask value) => new(value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarUInt32Mask All(ScalarVector2UInt32Mask value) => value.X & value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarUInt32Mask Any(ScalarVector2UInt32Mask value) => value.X | value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarUInt32Mask None(ScalarVector2UInt32Mask value) => ~(value.X | value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt32Mask AndNot(ScalarVector2UInt32Mask left, ScalarVector2UInt32Mask right)
    {
        return new(
            ScalarUInt32Mask.AndNot(left.X, right.X),
            ScalarUInt32Mask.AndNot(left.Y, right.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt32Mask Select(ScalarVector2UInt32Mask mask, ScalarVector2UInt32Mask ifTrue, ScalarVector2UInt32Mask ifFalse)
    {
        return new(
            ScalarUInt32Mask.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarUInt32Mask.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt32Mask And(ScalarVector2UInt32Mask left, ScalarVector2UInt32Mask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt32Mask Or(ScalarVector2UInt32Mask left, ScalarVector2UInt32Mask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt32Mask Xor(ScalarVector2UInt32Mask left, ScalarVector2UInt32Mask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt32Mask Not(ScalarVector2UInt32Mask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt32Mask operator &(ScalarVector2UInt32Mask left, ScalarVector2UInt32Mask right) => new(left.X & right.X, left.Y & right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt32Mask operator |(ScalarVector2UInt32Mask left, ScalarVector2UInt32Mask right) => new(left.X | right.X, left.Y | right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt32Mask operator ^(ScalarVector2UInt32Mask left, ScalarVector2UInt32Mask right) => new(left.X ^ right.X, left.Y ^ right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt32Mask operator ~(ScalarVector2UInt32Mask value) => new(~value.X, ~value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt32Mask operator ==(ScalarVector2UInt32Mask left, ScalarVector2UInt32Mask right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector2UInt32Mask operator !=(ScalarVector2UInt32Mask left, ScalarVector2UInt32Mask right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector2UInt32Mask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}