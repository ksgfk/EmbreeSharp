using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct ScalarVector4UInt64 :
    ISimdIntegerVector4<ScalarVector4UInt64, ScalarUInt64, ulong, ScalarVector4UInt64Mask, ScalarUInt64Mask>
{
    public ScalarVector4UInt64(ScalarUInt64 x, ScalarUInt64 y, ScalarUInt64 z, ScalarUInt64 w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => ScalarUInt64.LaneCount;

    public ScalarUInt64 X { get; }

    public ScalarUInt64 Y { get; }

    public ScalarUInt64 Z { get; }

    public ScalarUInt64 W { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt64 Create(ScalarUInt64 x, ScalarUInt64 y, ScalarUInt64 z, ScalarUInt64 w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt64 Broadcast(ulong value)
    {
        ScalarUInt64 packet = ScalarUInt64.Broadcast(value);
        return new(packet, packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt64 Select(ScalarVector4UInt64Mask mask, ScalarVector4UInt64 ifTrue, ScalarVector4UInt64 ifFalse)
    {
        return new(
            ScalarUInt64.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarUInt64.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarUInt64.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            ScalarUInt64.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt64 Select(ScalarUInt64Mask mask, ScalarVector4UInt64 ifTrue, ScalarVector4UInt64 ifFalse)
    {
        return new(
            ScalarUInt64.Select(mask, ifTrue.X, ifFalse.X),
            ScalarUInt64.Select(mask, ifTrue.Y, ifFalse.Y),
            ScalarUInt64.Select(mask, ifTrue.Z, ifFalse.Z),
            ScalarUInt64.Select(mask, ifTrue.W, ifFalse.W));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt64 operator +(ScalarVector4UInt64 value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt64 operator +(ScalarVector4UInt64 left, ScalarVector4UInt64 right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt64 operator -(ScalarVector4UInt64 left, ScalarVector4UInt64 right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt64 operator *(ScalarVector4UInt64 left, ScalarVector4UInt64 right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt64 operator -(ScalarVector4UInt64 value) => new(-value.X, -value.Y, -value.Z, -value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt64Mask operator ==(ScalarVector4UInt64 left, ScalarVector4UInt64 right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt64Mask operator !=(ScalarVector4UInt64 left, ScalarVector4UInt64 right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector4UInt64 other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct ScalarVector4UInt64Mask :
    ISimdVector4Mask<ScalarVector4UInt64Mask, ScalarUInt64Mask>
{
    public ScalarVector4UInt64Mask(ScalarUInt64Mask x, ScalarUInt64Mask y, ScalarUInt64Mask z, ScalarUInt64Mask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => ScalarUInt64Mask.LaneCount;

    public ScalarUInt64Mask X { get; }

    public ScalarUInt64Mask Y { get; }

    public ScalarUInt64Mask Z { get; }

    public ScalarUInt64Mask W { get; }

    public static ScalarVector4UInt64Mask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarUInt64Mask.True, ScalarUInt64Mask.True, ScalarUInt64Mask.True, ScalarUInt64Mask.True);
    }

    public static ScalarVector4UInt64Mask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarUInt64Mask.False, ScalarUInt64Mask.False, ScalarUInt64Mask.False, ScalarUInt64Mask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt64Mask Create(ScalarUInt64Mask x, ScalarUInt64Mask y, ScalarUInt64Mask z, ScalarUInt64Mask w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt64Mask Broadcast(ScalarUInt64Mask value) => new(value, value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarUInt64Mask All(ScalarVector4UInt64Mask value) => value.X & value.Y & value.Z & value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarUInt64Mask Any(ScalarVector4UInt64Mask value) => value.X | value.Y | value.Z | value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarUInt64Mask None(ScalarVector4UInt64Mask value) => ~(value.X | value.Y | value.Z | value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt64Mask AndNot(ScalarVector4UInt64Mask left, ScalarVector4UInt64Mask right)
    {
        return new(
            ScalarUInt64Mask.AndNot(left.X, right.X),
            ScalarUInt64Mask.AndNot(left.Y, right.Y),
            ScalarUInt64Mask.AndNot(left.Z, right.Z),
            ScalarUInt64Mask.AndNot(left.W, right.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt64Mask Select(ScalarVector4UInt64Mask mask, ScalarVector4UInt64Mask ifTrue, ScalarVector4UInt64Mask ifFalse)
    {
        return new(
            ScalarUInt64Mask.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarUInt64Mask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarUInt64Mask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            ScalarUInt64Mask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt64Mask And(ScalarVector4UInt64Mask left, ScalarVector4UInt64Mask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt64Mask Or(ScalarVector4UInt64Mask left, ScalarVector4UInt64Mask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt64Mask Xor(ScalarVector4UInt64Mask left, ScalarVector4UInt64Mask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt64Mask Not(ScalarVector4UInt64Mask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt64Mask operator &(ScalarVector4UInt64Mask left, ScalarVector4UInt64Mask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt64Mask operator |(ScalarVector4UInt64Mask left, ScalarVector4UInt64Mask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt64Mask operator ^(ScalarVector4UInt64Mask left, ScalarVector4UInt64Mask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt64Mask operator ~(ScalarVector4UInt64Mask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt64Mask operator ==(ScalarVector4UInt64Mask left, ScalarVector4UInt64Mask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4UInt64Mask operator !=(ScalarVector4UInt64Mask left, ScalarVector4UInt64Mask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector4UInt64Mask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}