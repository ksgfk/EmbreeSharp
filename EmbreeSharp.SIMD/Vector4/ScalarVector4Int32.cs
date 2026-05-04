using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct ScalarVector4Int32 :
    ISimdIntegerVector4<ScalarVector4Int32, ScalarInt32, int, ScalarVector4Int32Mask, ScalarInt32Mask>
{
    public ScalarVector4Int32(ScalarInt32 x, ScalarInt32 y, ScalarInt32 z, ScalarInt32 w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => ScalarInt32.LaneCount;

    public ScalarInt32 X { get; }

    public ScalarInt32 Y { get; }

    public ScalarInt32 Z { get; }

    public ScalarInt32 W { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int32 Create(ScalarInt32 x, ScalarInt32 y, ScalarInt32 z, ScalarInt32 w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int32 Broadcast(int value)
    {
        ScalarInt32 packet = ScalarInt32.Broadcast(value);
        return new(packet, packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int32 Select(ScalarVector4Int32Mask mask, ScalarVector4Int32 ifTrue, ScalarVector4Int32 ifFalse)
    {
        return new(
            ScalarInt32.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarInt32.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarInt32.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            ScalarInt32.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int32 Select(ScalarInt32Mask mask, ScalarVector4Int32 ifTrue, ScalarVector4Int32 ifFalse)
    {
        return new(
            ScalarInt32.Select(mask, ifTrue.X, ifFalse.X),
            ScalarInt32.Select(mask, ifTrue.Y, ifFalse.Y),
            ScalarInt32.Select(mask, ifTrue.Z, ifFalse.Z),
            ScalarInt32.Select(mask, ifTrue.W, ifFalse.W));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int32 operator +(ScalarVector4Int32 value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int32 operator +(ScalarVector4Int32 left, ScalarVector4Int32 right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int32 operator -(ScalarVector4Int32 left, ScalarVector4Int32 right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int32 operator *(ScalarVector4Int32 left, ScalarVector4Int32 right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int32 operator -(ScalarVector4Int32 value) => new(-value.X, -value.Y, -value.Z, -value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int32Mask operator ==(ScalarVector4Int32 left, ScalarVector4Int32 right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int32Mask operator !=(ScalarVector4Int32 left, ScalarVector4Int32 right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector4Int32 other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct ScalarVector4Int32Mask :
    ISimdVector4Mask<ScalarVector4Int32Mask, ScalarInt32Mask>
{
    public ScalarVector4Int32Mask(ScalarInt32Mask x, ScalarInt32Mask y, ScalarInt32Mask z, ScalarInt32Mask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => ScalarInt32Mask.LaneCount;

    public ScalarInt32Mask X { get; }

    public ScalarInt32Mask Y { get; }

    public ScalarInt32Mask Z { get; }

    public ScalarInt32Mask W { get; }

    public static ScalarVector4Int32Mask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarInt32Mask.True, ScalarInt32Mask.True, ScalarInt32Mask.True, ScalarInt32Mask.True);
    }

    public static ScalarVector4Int32Mask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarInt32Mask.False, ScalarInt32Mask.False, ScalarInt32Mask.False, ScalarInt32Mask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int32Mask Create(ScalarInt32Mask x, ScalarInt32Mask y, ScalarInt32Mask z, ScalarInt32Mask w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int32Mask Broadcast(ScalarInt32Mask value) => new(value, value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarInt32Mask All(ScalarVector4Int32Mask value) => value.X & value.Y & value.Z & value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarInt32Mask Any(ScalarVector4Int32Mask value) => value.X | value.Y | value.Z | value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarInt32Mask None(ScalarVector4Int32Mask value) => ~(value.X | value.Y | value.Z | value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int32Mask AndNot(ScalarVector4Int32Mask left, ScalarVector4Int32Mask right)
    {
        return new(
            ScalarInt32Mask.AndNot(left.X, right.X),
            ScalarInt32Mask.AndNot(left.Y, right.Y),
            ScalarInt32Mask.AndNot(left.Z, right.Z),
            ScalarInt32Mask.AndNot(left.W, right.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int32Mask Select(ScalarVector4Int32Mask mask, ScalarVector4Int32Mask ifTrue, ScalarVector4Int32Mask ifFalse)
    {
        return new(
            ScalarInt32Mask.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarInt32Mask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarInt32Mask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            ScalarInt32Mask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int32Mask And(ScalarVector4Int32Mask left, ScalarVector4Int32Mask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int32Mask Or(ScalarVector4Int32Mask left, ScalarVector4Int32Mask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int32Mask Xor(ScalarVector4Int32Mask left, ScalarVector4Int32Mask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int32Mask Not(ScalarVector4Int32Mask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int32Mask operator &(ScalarVector4Int32Mask left, ScalarVector4Int32Mask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int32Mask operator |(ScalarVector4Int32Mask left, ScalarVector4Int32Mask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int32Mask operator ^(ScalarVector4Int32Mask left, ScalarVector4Int32Mask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int32Mask operator ~(ScalarVector4Int32Mask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int32Mask operator ==(ScalarVector4Int32Mask left, ScalarVector4Int32Mask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Int32Mask operator !=(ScalarVector4Int32Mask left, ScalarVector4Int32Mask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector4Int32Mask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}