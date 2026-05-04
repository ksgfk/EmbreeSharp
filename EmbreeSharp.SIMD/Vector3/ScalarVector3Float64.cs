using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct ScalarVector3Float64 :
    ISimdFloatingPointVector3<ScalarVector3Float64, ScalarFloat64, double, ScalarVector3Float64Mask, ScalarFloat64Mask>
{
    public ScalarVector3Float64(ScalarFloat64 x, ScalarFloat64 y, ScalarFloat64 z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => ScalarFloat64.LaneCount;

    public ScalarFloat64 X { get; }

    public ScalarFloat64 Y { get; }

    public ScalarFloat64 Z { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float64 Create(ScalarFloat64 x, ScalarFloat64 y, ScalarFloat64 z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float64 Broadcast(double value)
    {
        ScalarFloat64 packet = ScalarFloat64.Broadcast(value);
        return new(packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float64 Select(ScalarVector3Float64Mask mask, ScalarVector3Float64 ifTrue, ScalarVector3Float64 ifFalse)
    {
        return new(
            ScalarFloat64.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarFloat64.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarFloat64.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float64 Select(ScalarFloat64Mask mask, ScalarVector3Float64 ifTrue, ScalarVector3Float64 ifFalse)
    {
        return new(
            ScalarFloat64.Select(mask, ifTrue.X, ifFalse.X),
            ScalarFloat64.Select(mask, ifTrue.Y, ifFalse.Y),
            ScalarFloat64.Select(mask, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFloat64 Dot(ScalarVector3Float64 left, ScalarVector3Float64 right)
    {
        return ScalarFloat64.FusedMultiplyAdd(
            left.Z,
            right.Z,
            ScalarFloat64.FusedMultiplyAdd(left.Y, right.Y, left.X * right.X));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float64 Cross(ScalarVector3Float64 left, ScalarVector3Float64 right)
    {
        return new(
            left.Y * right.Z - left.Z * right.Y,
            left.Z * right.X - left.X * right.Z,
            left.X * right.Y - left.Y * right.X);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float64 operator +(ScalarVector3Float64 value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float64 operator +(ScalarVector3Float64 left, ScalarVector3Float64 right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float64 operator -(ScalarVector3Float64 left, ScalarVector3Float64 right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float64 operator *(ScalarVector3Float64 left, ScalarVector3Float64 right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float64 operator -(ScalarVector3Float64 value) => new(-value.X, -value.Y, -value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float64Mask operator ==(ScalarVector3Float64 left, ScalarVector3Float64 right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float64Mask operator !=(ScalarVector3Float64 left, ScalarVector3Float64 right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector3Float64 other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}

public readonly struct ScalarVector3Float64Mask :
    ISimdVector3Mask<ScalarVector3Float64Mask, ScalarFloat64Mask>
{
    public ScalarVector3Float64Mask(ScalarFloat64Mask x, ScalarFloat64Mask y, ScalarFloat64Mask z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => ScalarFloat64Mask.LaneCount;

    public ScalarFloat64Mask X { get; }

    public ScalarFloat64Mask Y { get; }

    public ScalarFloat64Mask Z { get; }

    public static ScalarVector3Float64Mask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarFloat64Mask.True, ScalarFloat64Mask.True, ScalarFloat64Mask.True);
    }

    public static ScalarVector3Float64Mask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarFloat64Mask.False, ScalarFloat64Mask.False, ScalarFloat64Mask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float64Mask Create(ScalarFloat64Mask x, ScalarFloat64Mask y, ScalarFloat64Mask z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float64Mask Broadcast(ScalarFloat64Mask value) => new(value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFloat64Mask All(ScalarVector3Float64Mask value) => value.X & value.Y & value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFloat64Mask Any(ScalarVector3Float64Mask value) => value.X | value.Y | value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFloat64Mask None(ScalarVector3Float64Mask value) => ~(value.X | value.Y | value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float64Mask AndNot(ScalarVector3Float64Mask left, ScalarVector3Float64Mask right)
    {
        return new(
            ScalarFloat64Mask.AndNot(left.X, right.X),
            ScalarFloat64Mask.AndNot(left.Y, right.Y),
            ScalarFloat64Mask.AndNot(left.Z, right.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float64Mask Select(ScalarVector3Float64Mask mask, ScalarVector3Float64Mask ifTrue, ScalarVector3Float64Mask ifFalse)
    {
        return new(
            ScalarFloat64Mask.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarFloat64Mask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarFloat64Mask.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float64Mask And(ScalarVector3Float64Mask left, ScalarVector3Float64Mask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float64Mask Or(ScalarVector3Float64Mask left, ScalarVector3Float64Mask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float64Mask Xor(ScalarVector3Float64Mask left, ScalarVector3Float64Mask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float64Mask Not(ScalarVector3Float64Mask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float64Mask operator &(ScalarVector3Float64Mask left, ScalarVector3Float64Mask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float64Mask operator |(ScalarVector3Float64Mask left, ScalarVector3Float64Mask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float64Mask operator ^(ScalarVector3Float64Mask left, ScalarVector3Float64Mask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float64Mask operator ~(ScalarVector3Float64Mask value) => new(~value.X, ~value.Y, ~value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float64Mask operator ==(ScalarVector3Float64Mask left, ScalarVector3Float64Mask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float64Mask operator !=(ScalarVector3Float64Mask left, ScalarVector3Float64Mask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector3Float64Mask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}