using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct ScalarVector3Int32 :
    ISimdIntegerVector3<ScalarVector3Int32, ScalarInt32, int, ScalarVector3Int32Mask, ScalarInt32Mask>
{
    public ScalarVector3Int32(ScalarInt32 x, ScalarInt32 y, ScalarInt32 z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => ScalarInt32.LaneCount;

    public ScalarInt32 X { get; }

    public ScalarInt32 Y { get; }

    public ScalarInt32 Z { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int32 Create(ScalarInt32 x, ScalarInt32 y, ScalarInt32 z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int32 Broadcast(int value)
    {
        ScalarInt32 packet = ScalarInt32.Broadcast(value);
        return new(packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int32 Select(ScalarVector3Int32Mask mask, ScalarVector3Int32 ifTrue, ScalarVector3Int32 ifFalse)
    {
        return new(
            ScalarInt32.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarInt32.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarInt32.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int32 Select(ScalarInt32Mask mask, ScalarVector3Int32 ifTrue, ScalarVector3Int32 ifFalse)
    {
        return new(
            ScalarInt32.Select(mask, ifTrue.X, ifFalse.X),
            ScalarInt32.Select(mask, ifTrue.Y, ifFalse.Y),
            ScalarInt32.Select(mask, ifTrue.Z, ifFalse.Z));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int32 operator +(ScalarVector3Int32 value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int32 operator +(ScalarVector3Int32 left, ScalarVector3Int32 right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int32 operator -(ScalarVector3Int32 left, ScalarVector3Int32 right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int32 operator *(ScalarVector3Int32 left, ScalarVector3Int32 right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int32 operator -(ScalarVector3Int32 value) => new(-value.X, -value.Y, -value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int32Mask operator ==(ScalarVector3Int32 left, ScalarVector3Int32 right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int32Mask operator !=(ScalarVector3Int32 left, ScalarVector3Int32 right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector3Int32 other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}

public readonly struct ScalarVector3Int32Mask :
    ISimdVector3Mask<ScalarVector3Int32Mask, ScalarInt32Mask>
{
    public ScalarVector3Int32Mask(ScalarInt32Mask x, ScalarInt32Mask y, ScalarInt32Mask z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => ScalarInt32Mask.LaneCount;

    public ScalarInt32Mask X { get; }

    public ScalarInt32Mask Y { get; }

    public ScalarInt32Mask Z { get; }

    public static ScalarVector3Int32Mask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarInt32Mask.True, ScalarInt32Mask.True, ScalarInt32Mask.True);
    }

    public static ScalarVector3Int32Mask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarInt32Mask.False, ScalarInt32Mask.False, ScalarInt32Mask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int32Mask Create(ScalarInt32Mask x, ScalarInt32Mask y, ScalarInt32Mask z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int32Mask Broadcast(ScalarInt32Mask value) => new(value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarInt32Mask All(ScalarVector3Int32Mask value) => value.X & value.Y & value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarInt32Mask Any(ScalarVector3Int32Mask value) => value.X | value.Y | value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarInt32Mask None(ScalarVector3Int32Mask value) => ~(value.X | value.Y | value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int32Mask AndNot(ScalarVector3Int32Mask left, ScalarVector3Int32Mask right)
    {
        return new(
            ScalarInt32Mask.AndNot(left.X, right.X),
            ScalarInt32Mask.AndNot(left.Y, right.Y),
            ScalarInt32Mask.AndNot(left.Z, right.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int32Mask Select(ScalarVector3Int32Mask mask, ScalarVector3Int32Mask ifTrue, ScalarVector3Int32Mask ifFalse)
    {
        return new(
            ScalarInt32Mask.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarInt32Mask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarInt32Mask.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int32Mask And(ScalarVector3Int32Mask left, ScalarVector3Int32Mask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int32Mask Or(ScalarVector3Int32Mask left, ScalarVector3Int32Mask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int32Mask Xor(ScalarVector3Int32Mask left, ScalarVector3Int32Mask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int32Mask Not(ScalarVector3Int32Mask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int32Mask operator &(ScalarVector3Int32Mask left, ScalarVector3Int32Mask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int32Mask operator |(ScalarVector3Int32Mask left, ScalarVector3Int32Mask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int32Mask operator ^(ScalarVector3Int32Mask left, ScalarVector3Int32Mask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int32Mask operator ~(ScalarVector3Int32Mask value) => new(~value.X, ~value.Y, ~value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int32Mask operator ==(ScalarVector3Int32Mask left, ScalarVector3Int32Mask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Int32Mask operator !=(ScalarVector3Int32Mask left, ScalarVector3Int32Mask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector3Int32Mask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}