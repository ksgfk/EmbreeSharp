using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct ScalarVector4Int64 :
    ISimdIntegerVector4<ScalarVector4Int64, ScalarInt64, long, ScalarVector4Int64Mask, ScalarInt64Mask>
{
    public ScalarVector4Int64(ScalarInt64 x, ScalarInt64 y, ScalarInt64 z, ScalarInt64 w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => ScalarInt64.LaneCount;

    public ScalarInt64 X { get; }

    public ScalarInt64 Y { get; }

    public ScalarInt64 Z { get; }

    public ScalarInt64 W { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int64 Create(ScalarInt64 x, ScalarInt64 y, ScalarInt64 z, ScalarInt64 w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int64 Broadcast(long value)
    {
        ScalarInt64 packet = ScalarInt64.Broadcast(value);
        return new(packet, packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int64 Select(ScalarVector4Int64Mask mask, ScalarVector4Int64 ifTrue, ScalarVector4Int64 ifFalse)
    {
        return new(
            ScalarInt64.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarInt64.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarInt64.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            ScalarInt64.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int64 Select(ScalarInt64Mask mask, ScalarVector4Int64 ifTrue, ScalarVector4Int64 ifFalse)
    {
        return new(
            ScalarInt64.Select(mask, ifTrue.X, ifFalse.X),
            ScalarInt64.Select(mask, ifTrue.Y, ifFalse.Y),
            ScalarInt64.Select(mask, ifTrue.Z, ifFalse.Z),
            ScalarInt64.Select(mask, ifTrue.W, ifFalse.W));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int64 operator +(ScalarVector4Int64 value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int64 operator +(ScalarVector4Int64 left, ScalarVector4Int64 right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int64 operator -(ScalarVector4Int64 left, ScalarVector4Int64 right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int64 operator *(ScalarVector4Int64 left, ScalarVector4Int64 right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int64 operator -(ScalarVector4Int64 value) => new(-value.X, -value.Y, -value.Z, -value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int64Mask operator ==(ScalarVector4Int64 left, ScalarVector4Int64 right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int64Mask operator !=(ScalarVector4Int64 left, ScalarVector4Int64 right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector4Int64 other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct ScalarVector4Int64Mask :
    ISimdVector4Mask<ScalarVector4Int64Mask, ScalarInt64Mask>
{
    public ScalarVector4Int64Mask(ScalarInt64Mask x, ScalarInt64Mask y, ScalarInt64Mask z, ScalarInt64Mask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => ScalarInt64Mask.LaneCount;

    public ScalarInt64Mask X { get; }

    public ScalarInt64Mask Y { get; }

    public ScalarInt64Mask Z { get; }

    public ScalarInt64Mask W { get; }

    public static ScalarVector4Int64Mask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarInt64Mask.True, ScalarInt64Mask.True, ScalarInt64Mask.True, ScalarInt64Mask.True);
    }

    public static ScalarVector4Int64Mask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarInt64Mask.False, ScalarInt64Mask.False, ScalarInt64Mask.False, ScalarInt64Mask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int64Mask Create(ScalarInt64Mask x, ScalarInt64Mask y, ScalarInt64Mask z, ScalarInt64Mask w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int64Mask Broadcast(ScalarInt64Mask value) => new(value, value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarInt64Mask All(ScalarVector4Int64Mask value) => value.X & value.Y & value.Z & value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarInt64Mask Any(ScalarVector4Int64Mask value) => value.X | value.Y | value.Z | value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarInt64Mask None(ScalarVector4Int64Mask value) => ~(value.X | value.Y | value.Z | value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int64Mask AndNot(ScalarVector4Int64Mask left, ScalarVector4Int64Mask right)
    {
        return new(
            ScalarInt64Mask.AndNot(left.X, right.X),
            ScalarInt64Mask.AndNot(left.Y, right.Y),
            ScalarInt64Mask.AndNot(left.Z, right.Z),
            ScalarInt64Mask.AndNot(left.W, right.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int64Mask Select(ScalarVector4Int64Mask mask, ScalarVector4Int64Mask ifTrue, ScalarVector4Int64Mask ifFalse)
    {
        return new(
            ScalarInt64Mask.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarInt64Mask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarInt64Mask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            ScalarInt64Mask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int64Mask And(ScalarVector4Int64Mask left, ScalarVector4Int64Mask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int64Mask Or(ScalarVector4Int64Mask left, ScalarVector4Int64Mask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int64Mask Xor(ScalarVector4Int64Mask left, ScalarVector4Int64Mask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int64Mask Not(ScalarVector4Int64Mask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int64Mask operator &(ScalarVector4Int64Mask left, ScalarVector4Int64Mask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int64Mask operator |(ScalarVector4Int64Mask left, ScalarVector4Int64Mask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int64Mask operator ^(ScalarVector4Int64Mask left, ScalarVector4Int64Mask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int64Mask operator ~(ScalarVector4Int64Mask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int64Mask operator ==(ScalarVector4Int64Mask left, ScalarVector4Int64Mask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int64Mask operator !=(ScalarVector4Int64Mask left, ScalarVector4Int64Mask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector4Int64Mask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}