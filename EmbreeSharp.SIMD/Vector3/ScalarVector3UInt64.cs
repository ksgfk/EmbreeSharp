using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct ScalarVector3UInt64 :
    ISimdIntegerVector3<ScalarVector3UInt64, ScalarUInt64, ulong, ScalarVector3UInt64Mask, ScalarUInt64Mask>
{
    public ScalarVector3UInt64(ScalarUInt64 x, ScalarUInt64 y, ScalarUInt64 z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => ScalarUInt64.LaneCount;

    public ScalarUInt64 X { get; }

    public ScalarUInt64 Y { get; }

    public ScalarUInt64 Z { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt64 Create(ScalarUInt64 x, ScalarUInt64 y, ScalarUInt64 z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt64 Broadcast(ulong value)
    {
        ScalarUInt64 packet = ScalarUInt64.Broadcast(value);
        return new(packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt64 Select(ScalarVector3UInt64Mask mask, ScalarVector3UInt64 ifTrue, ScalarVector3UInt64 ifFalse)
    {
        return new(
            ScalarUInt64.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarUInt64.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarUInt64.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt64 Select(ScalarUInt64Mask mask, ScalarVector3UInt64 ifTrue, ScalarVector3UInt64 ifFalse)
    {
        return new(
            ScalarUInt64.Select(mask, ifTrue.X, ifFalse.X),
            ScalarUInt64.Select(mask, ifTrue.Y, ifFalse.Y),
            ScalarUInt64.Select(mask, ifTrue.Z, ifFalse.Z));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt64 operator +(ScalarVector3UInt64 value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt64 operator +(ScalarVector3UInt64 left, ScalarVector3UInt64 right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt64 operator -(ScalarVector3UInt64 left, ScalarVector3UInt64 right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt64 operator *(ScalarVector3UInt64 left, ScalarVector3UInt64 right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt64 operator -(ScalarVector3UInt64 value) => new(-value.X, -value.Y, -value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt64Mask operator ==(ScalarVector3UInt64 left, ScalarVector3UInt64 right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt64Mask operator !=(ScalarVector3UInt64 left, ScalarVector3UInt64 right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector3UInt64 other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}

public readonly struct ScalarVector3UInt64Mask :
    ISimdVector3Mask<ScalarVector3UInt64Mask, ScalarUInt64Mask>
{
    public ScalarVector3UInt64Mask(ScalarUInt64Mask x, ScalarUInt64Mask y, ScalarUInt64Mask z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => ScalarUInt64Mask.LaneCount;

    public ScalarUInt64Mask X { get; }

    public ScalarUInt64Mask Y { get; }

    public ScalarUInt64Mask Z { get; }

    public static ScalarVector3UInt64Mask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarUInt64Mask.True, ScalarUInt64Mask.True, ScalarUInt64Mask.True);
    }

    public static ScalarVector3UInt64Mask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarUInt64Mask.False, ScalarUInt64Mask.False, ScalarUInt64Mask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt64Mask Create(ScalarUInt64Mask x, ScalarUInt64Mask y, ScalarUInt64Mask z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt64Mask Broadcast(ScalarUInt64Mask value) => new(value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarUInt64Mask All(ScalarVector3UInt64Mask value) => value.X & value.Y & value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarUInt64Mask Any(ScalarVector3UInt64Mask value) => value.X | value.Y | value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarUInt64Mask None(ScalarVector3UInt64Mask value) => ~(value.X | value.Y | value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt64Mask AndNot(ScalarVector3UInt64Mask left, ScalarVector3UInt64Mask right)
    {
        return new(
            ScalarUInt64Mask.AndNot(left.X, right.X),
            ScalarUInt64Mask.AndNot(left.Y, right.Y),
            ScalarUInt64Mask.AndNot(left.Z, right.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt64Mask Select(ScalarVector3UInt64Mask mask, ScalarVector3UInt64Mask ifTrue, ScalarVector3UInt64Mask ifFalse)
    {
        return new(
            ScalarUInt64Mask.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarUInt64Mask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarUInt64Mask.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt64Mask And(ScalarVector3UInt64Mask left, ScalarVector3UInt64Mask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt64Mask Or(ScalarVector3UInt64Mask left, ScalarVector3UInt64Mask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt64Mask Xor(ScalarVector3UInt64Mask left, ScalarVector3UInt64Mask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt64Mask Not(ScalarVector3UInt64Mask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt64Mask operator &(ScalarVector3UInt64Mask left, ScalarVector3UInt64Mask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt64Mask operator |(ScalarVector3UInt64Mask left, ScalarVector3UInt64Mask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt64Mask operator ^(ScalarVector3UInt64Mask left, ScalarVector3UInt64Mask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt64Mask operator ~(ScalarVector3UInt64Mask value) => new(~value.X, ~value.Y, ~value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt64Mask operator ==(ScalarVector3UInt64Mask left, ScalarVector3UInt64Mask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt64Mask operator !=(ScalarVector3UInt64Mask left, ScalarVector3UInt64Mask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector3UInt64Mask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}