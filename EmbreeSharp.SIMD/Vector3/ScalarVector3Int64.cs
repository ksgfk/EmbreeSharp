using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct ScalarVector3Int64 :
    ISimdIntegerVector3<ScalarVector3Int64, ScalarInt64, long, ScalarVector3Int64Mask, ScalarInt64Mask>
{
    public ScalarVector3Int64(ScalarInt64 x, ScalarInt64 y, ScalarInt64 z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => ScalarInt64.LaneCount;

    public ScalarInt64 X { get; }

    public ScalarInt64 Y { get; }

    public ScalarInt64 Z { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int64 Create(ScalarInt64 x, ScalarInt64 y, ScalarInt64 z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int64 Broadcast(long value)
    {
        ScalarInt64 packet = ScalarInt64.Broadcast(value);
        return new(packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int64 Select(ScalarVector3Int64Mask mask, ScalarVector3Int64 ifTrue, ScalarVector3Int64 ifFalse)
    {
        return new(
            ScalarInt64.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarInt64.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarInt64.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int64 Select(ScalarInt64Mask mask, ScalarVector3Int64 ifTrue, ScalarVector3Int64 ifFalse)
    {
        return new(
            ScalarInt64.Select(mask, ifTrue.X, ifFalse.X),
            ScalarInt64.Select(mask, ifTrue.Y, ifFalse.Y),
            ScalarInt64.Select(mask, ifTrue.Z, ifFalse.Z));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int64 operator +(ScalarVector3Int64 value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int64 operator +(ScalarVector3Int64 left, ScalarVector3Int64 right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int64 operator -(ScalarVector3Int64 left, ScalarVector3Int64 right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int64 operator *(ScalarVector3Int64 left, ScalarVector3Int64 right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int64 operator -(ScalarVector3Int64 value) => new(-value.X, -value.Y, -value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int64Mask operator ==(ScalarVector3Int64 left, ScalarVector3Int64 right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int64Mask operator !=(ScalarVector3Int64 left, ScalarVector3Int64 right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector3Int64 other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}

public readonly struct ScalarVector3Int64Mask :
    ISimdVector3Mask<ScalarVector3Int64Mask, ScalarInt64Mask>
{
    public ScalarVector3Int64Mask(ScalarInt64Mask x, ScalarInt64Mask y, ScalarInt64Mask z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => ScalarInt64Mask.LaneCount;

    public ScalarInt64Mask X { get; }

    public ScalarInt64Mask Y { get; }

    public ScalarInt64Mask Z { get; }

    public static ScalarVector3Int64Mask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarInt64Mask.True, ScalarInt64Mask.True, ScalarInt64Mask.True);
    }

    public static ScalarVector3Int64Mask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarInt64Mask.False, ScalarInt64Mask.False, ScalarInt64Mask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int64Mask Create(ScalarInt64Mask x, ScalarInt64Mask y, ScalarInt64Mask z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int64Mask Broadcast(ScalarInt64Mask value) => new(value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarInt64Mask All(ScalarVector3Int64Mask value) => value.X & value.Y & value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarInt64Mask Any(ScalarVector3Int64Mask value) => value.X | value.Y | value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarInt64Mask None(ScalarVector3Int64Mask value) => ~(value.X | value.Y | value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int64Mask AndNot(ScalarVector3Int64Mask left, ScalarVector3Int64Mask right)
    {
        return new(
            ScalarInt64Mask.AndNot(left.X, right.X),
            ScalarInt64Mask.AndNot(left.Y, right.Y),
            ScalarInt64Mask.AndNot(left.Z, right.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int64Mask Select(ScalarVector3Int64Mask mask, ScalarVector3Int64Mask ifTrue, ScalarVector3Int64Mask ifFalse)
    {
        return new(
            ScalarInt64Mask.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarInt64Mask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarInt64Mask.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int64Mask And(ScalarVector3Int64Mask left, ScalarVector3Int64Mask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int64Mask Or(ScalarVector3Int64Mask left, ScalarVector3Int64Mask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int64Mask Xor(ScalarVector3Int64Mask left, ScalarVector3Int64Mask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int64Mask Not(ScalarVector3Int64Mask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int64Mask operator &(ScalarVector3Int64Mask left, ScalarVector3Int64Mask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int64Mask operator |(ScalarVector3Int64Mask left, ScalarVector3Int64Mask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int64Mask operator ^(ScalarVector3Int64Mask left, ScalarVector3Int64Mask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int64Mask operator ~(ScalarVector3Int64Mask value) => new(~value.X, ~value.Y, ~value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int64Mask operator ==(ScalarVector3Int64Mask left, ScalarVector3Int64Mask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int64Mask operator !=(ScalarVector3Int64Mask left, ScalarVector3Int64Mask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector3Int64Mask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}