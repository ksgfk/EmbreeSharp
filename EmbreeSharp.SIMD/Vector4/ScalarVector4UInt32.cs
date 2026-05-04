using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct ScalarVector4UInt32 :
    ISimdIntegerVector4<ScalarVector4UInt32, ScalarUInt32, uint, ScalarVector4UInt32Mask, ScalarUInt32Mask>
{
    public ScalarVector4UInt32(ScalarUInt32 x, ScalarUInt32 y, ScalarUInt32 z, ScalarUInt32 w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => ScalarUInt32.LaneCount;

    public ScalarUInt32 X { get; }

    public ScalarUInt32 Y { get; }

    public ScalarUInt32 Z { get; }

    public ScalarUInt32 W { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt32 Create(ScalarUInt32 x, ScalarUInt32 y, ScalarUInt32 z, ScalarUInt32 w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt32 Broadcast(uint value)
    {
        ScalarUInt32 packet = ScalarUInt32.Broadcast(value);
        return new(packet, packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt32 Select(ScalarVector4UInt32Mask mask, ScalarVector4UInt32 ifTrue, ScalarVector4UInt32 ifFalse)
    {
        return new(
            ScalarUInt32.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarUInt32.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarUInt32.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            ScalarUInt32.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt32 Select(ScalarUInt32Mask mask, ScalarVector4UInt32 ifTrue, ScalarVector4UInt32 ifFalse)
    {
        return new(
            ScalarUInt32.Select(mask, ifTrue.X, ifFalse.X),
            ScalarUInt32.Select(mask, ifTrue.Y, ifFalse.Y),
            ScalarUInt32.Select(mask, ifTrue.Z, ifFalse.Z),
            ScalarUInt32.Select(mask, ifTrue.W, ifFalse.W));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt32 operator +(ScalarVector4UInt32 value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt32 operator +(ScalarVector4UInt32 left, ScalarVector4UInt32 right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt32 operator -(ScalarVector4UInt32 left, ScalarVector4UInt32 right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt32 operator *(ScalarVector4UInt32 left, ScalarVector4UInt32 right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt32 operator -(ScalarVector4UInt32 value) => new(-value.X, -value.Y, -value.Z, -value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt32Mask operator ==(ScalarVector4UInt32 left, ScalarVector4UInt32 right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt32Mask operator !=(ScalarVector4UInt32 left, ScalarVector4UInt32 right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector4UInt32 other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct ScalarVector4UInt32Mask :
    ISimdVector4Mask<ScalarVector4UInt32Mask, ScalarUInt32Mask>
{
    public ScalarVector4UInt32Mask(ScalarUInt32Mask x, ScalarUInt32Mask y, ScalarUInt32Mask z, ScalarUInt32Mask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => ScalarUInt32Mask.LaneCount;

    public ScalarUInt32Mask X { get; }

    public ScalarUInt32Mask Y { get; }

    public ScalarUInt32Mask Z { get; }

    public ScalarUInt32Mask W { get; }

    public static ScalarVector4UInt32Mask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarUInt32Mask.True, ScalarUInt32Mask.True, ScalarUInt32Mask.True, ScalarUInt32Mask.True);
    }

    public static ScalarVector4UInt32Mask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarUInt32Mask.False, ScalarUInt32Mask.False, ScalarUInt32Mask.False, ScalarUInt32Mask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt32Mask Create(ScalarUInt32Mask x, ScalarUInt32Mask y, ScalarUInt32Mask z, ScalarUInt32Mask w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt32Mask Broadcast(ScalarUInt32Mask value) => new(value, value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarUInt32Mask All(ScalarVector4UInt32Mask value) => value.X & value.Y & value.Z & value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarUInt32Mask Any(ScalarVector4UInt32Mask value) => value.X | value.Y | value.Z | value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarUInt32Mask None(ScalarVector4UInt32Mask value) => ~(value.X | value.Y | value.Z | value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt32Mask AndNot(ScalarVector4UInt32Mask left, ScalarVector4UInt32Mask right)
    {
        return new(
            ScalarUInt32Mask.AndNot(left.X, right.X),
            ScalarUInt32Mask.AndNot(left.Y, right.Y),
            ScalarUInt32Mask.AndNot(left.Z, right.Z),
            ScalarUInt32Mask.AndNot(left.W, right.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt32Mask Select(ScalarVector4UInt32Mask mask, ScalarVector4UInt32Mask ifTrue, ScalarVector4UInt32Mask ifFalse)
    {
        return new(
            ScalarUInt32Mask.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarUInt32Mask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarUInt32Mask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            ScalarUInt32Mask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt32Mask And(ScalarVector4UInt32Mask left, ScalarVector4UInt32Mask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt32Mask Or(ScalarVector4UInt32Mask left, ScalarVector4UInt32Mask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt32Mask Xor(ScalarVector4UInt32Mask left, ScalarVector4UInt32Mask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt32Mask Not(ScalarVector4UInt32Mask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt32Mask operator &(ScalarVector4UInt32Mask left, ScalarVector4UInt32Mask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt32Mask operator |(ScalarVector4UInt32Mask left, ScalarVector4UInt32Mask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt32Mask operator ^(ScalarVector4UInt32Mask left, ScalarVector4UInt32Mask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt32Mask operator ~(ScalarVector4UInt32Mask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt32Mask operator ==(ScalarVector4UInt32Mask left, ScalarVector4UInt32Mask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt32Mask operator !=(ScalarVector4UInt32Mask left, ScalarVector4UInt32Mask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector4UInt32Mask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}