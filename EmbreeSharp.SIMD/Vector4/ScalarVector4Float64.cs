using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct ScalarVector4Float64 :
    ISimdFloatingPointVector4<ScalarVector4Float64, ScalarFloat64, double, ScalarVector4Float64Mask, ScalarFloat64Mask>
{
    public ScalarVector4Float64(ScalarFloat64 x, ScalarFloat64 y, ScalarFloat64 z, ScalarFloat64 w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => ScalarFloat64.LaneCount;

    public ScalarFloat64 X { get; }

    public ScalarFloat64 Y { get; }

    public ScalarFloat64 Z { get; }

    public ScalarFloat64 W { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float64 Create(ScalarFloat64 x, ScalarFloat64 y, ScalarFloat64 z, ScalarFloat64 w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float64 Broadcast(double value)
    {
        ScalarFloat64 packet = ScalarFloat64.Broadcast(value);
        return new(packet, packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float64 Select(ScalarVector4Float64Mask mask, ScalarVector4Float64 ifTrue, ScalarVector4Float64 ifFalse)
    {
        return new(
            ScalarFloat64.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarFloat64.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarFloat64.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            ScalarFloat64.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float64 Select(ScalarFloat64Mask mask, ScalarVector4Float64 ifTrue, ScalarVector4Float64 ifFalse)
    {
        return new(
            ScalarFloat64.Select(mask, ifTrue.X, ifFalse.X),
            ScalarFloat64.Select(mask, ifTrue.Y, ifFalse.Y),
            ScalarFloat64.Select(mask, ifTrue.Z, ifFalse.Z),
            ScalarFloat64.Select(mask, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFloat64 Dot(ScalarVector4Float64 left, ScalarVector4Float64 right)
    {
        return ScalarFloat64.FusedMultiplyAdd(
            left.W,
            right.W,
            ScalarFloat64.FusedMultiplyAdd(left.Z, right.Z, ScalarFloat64.FusedMultiplyAdd(left.Y, right.Y, left.X * right.X)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float64 operator +(ScalarVector4Float64 value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float64 operator +(ScalarVector4Float64 left, ScalarVector4Float64 right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float64 operator -(ScalarVector4Float64 left, ScalarVector4Float64 right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float64 operator *(ScalarVector4Float64 left, ScalarVector4Float64 right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float64 operator -(ScalarVector4Float64 value) => new(-value.X, -value.Y, -value.Z, -value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float64Mask operator ==(ScalarVector4Float64 left, ScalarVector4Float64 right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float64Mask operator !=(ScalarVector4Float64 left, ScalarVector4Float64 right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector4Float64 other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct ScalarVector4Float64Mask :
    ISimdVector4Mask<ScalarVector4Float64Mask, ScalarFloat64Mask>
{
    public ScalarVector4Float64Mask(ScalarFloat64Mask x, ScalarFloat64Mask y, ScalarFloat64Mask z, ScalarFloat64Mask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => ScalarFloat64Mask.LaneCount;

    public ScalarFloat64Mask X { get; }

    public ScalarFloat64Mask Y { get; }

    public ScalarFloat64Mask Z { get; }

    public ScalarFloat64Mask W { get; }

    public static ScalarVector4Float64Mask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarFloat64Mask.True, ScalarFloat64Mask.True, ScalarFloat64Mask.True, ScalarFloat64Mask.True);
    }

    public static ScalarVector4Float64Mask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarFloat64Mask.False, ScalarFloat64Mask.False, ScalarFloat64Mask.False, ScalarFloat64Mask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float64Mask Create(ScalarFloat64Mask x, ScalarFloat64Mask y, ScalarFloat64Mask z, ScalarFloat64Mask w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float64Mask Broadcast(ScalarFloat64Mask value) => new(value, value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFloat64Mask All(ScalarVector4Float64Mask value) => value.X & value.Y & value.Z & value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFloat64Mask Any(ScalarVector4Float64Mask value) => value.X | value.Y | value.Z | value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFloat64Mask None(ScalarVector4Float64Mask value) => ~(value.X | value.Y | value.Z | value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float64Mask AndNot(ScalarVector4Float64Mask left, ScalarVector4Float64Mask right)
    {
        return new(
            ScalarFloat64Mask.AndNot(left.X, right.X),
            ScalarFloat64Mask.AndNot(left.Y, right.Y),
            ScalarFloat64Mask.AndNot(left.Z, right.Z),
            ScalarFloat64Mask.AndNot(left.W, right.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float64Mask Select(ScalarVector4Float64Mask mask, ScalarVector4Float64Mask ifTrue, ScalarVector4Float64Mask ifFalse)
    {
        return new(
            ScalarFloat64Mask.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarFloat64Mask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarFloat64Mask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            ScalarFloat64Mask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float64Mask And(ScalarVector4Float64Mask left, ScalarVector4Float64Mask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float64Mask Or(ScalarVector4Float64Mask left, ScalarVector4Float64Mask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float64Mask Xor(ScalarVector4Float64Mask left, ScalarVector4Float64Mask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float64Mask Not(ScalarVector4Float64Mask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float64Mask operator &(ScalarVector4Float64Mask left, ScalarVector4Float64Mask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float64Mask operator |(ScalarVector4Float64Mask left, ScalarVector4Float64Mask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float64Mask operator ^(ScalarVector4Float64Mask left, ScalarVector4Float64Mask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float64Mask operator ~(ScalarVector4Float64Mask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float64Mask operator ==(ScalarVector4Float64Mask left, ScalarVector4Float64Mask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float64Mask operator !=(ScalarVector4Float64Mask left, ScalarVector4Float64Mask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector4Float64Mask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}