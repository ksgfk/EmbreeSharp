using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct ScalarVector3UInt32 :
    ISimdIntegerVector3<ScalarVector3UInt32, ScalarUInt32, uint, ScalarVector3UInt32Mask, ScalarUInt32Mask>
{
    public ScalarVector3UInt32(ScalarUInt32 x, ScalarUInt32 y, ScalarUInt32 z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => ScalarUInt32.LaneCount;

    public ScalarUInt32 X { get; }

    public ScalarUInt32 Y { get; }

    public ScalarUInt32 Z { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt32 Create(ScalarUInt32 x, ScalarUInt32 y, ScalarUInt32 z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt32 Broadcast(uint value)
    {
        ScalarUInt32 packet = ScalarUInt32.Broadcast(value);
        return new(packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt32 Select(ScalarVector3UInt32Mask mask, ScalarVector3UInt32 ifTrue, ScalarVector3UInt32 ifFalse)
    {
        return new(
            ScalarUInt32.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarUInt32.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarUInt32.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt32 Select(ScalarUInt32Mask mask, ScalarVector3UInt32 ifTrue, ScalarVector3UInt32 ifFalse)
    {
        return new(
            ScalarUInt32.Select(mask, ifTrue.X, ifFalse.X),
            ScalarUInt32.Select(mask, ifTrue.Y, ifFalse.Y),
            ScalarUInt32.Select(mask, ifTrue.Z, ifFalse.Z));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt32 operator +(ScalarVector3UInt32 value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt32 operator +(ScalarVector3UInt32 left, ScalarVector3UInt32 right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt32 operator -(ScalarVector3UInt32 left, ScalarVector3UInt32 right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt32 operator *(ScalarVector3UInt32 left, ScalarVector3UInt32 right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt32 operator -(ScalarVector3UInt32 value) => new(-value.X, -value.Y, -value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt32Mask operator ==(ScalarVector3UInt32 left, ScalarVector3UInt32 right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt32Mask operator !=(ScalarVector3UInt32 left, ScalarVector3UInt32 right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector3UInt32 other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}

public readonly struct ScalarVector3UInt32Mask :
    ISimdVector3Mask<ScalarVector3UInt32Mask, ScalarUInt32Mask>
{
    public ScalarVector3UInt32Mask(ScalarUInt32Mask x, ScalarUInt32Mask y, ScalarUInt32Mask z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => ScalarUInt32Mask.LaneCount;

    public ScalarUInt32Mask X { get; }

    public ScalarUInt32Mask Y { get; }

    public ScalarUInt32Mask Z { get; }

    public static ScalarVector3UInt32Mask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarUInt32Mask.True, ScalarUInt32Mask.True, ScalarUInt32Mask.True);
    }

    public static ScalarVector3UInt32Mask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarUInt32Mask.False, ScalarUInt32Mask.False, ScalarUInt32Mask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt32Mask Create(ScalarUInt32Mask x, ScalarUInt32Mask y, ScalarUInt32Mask z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt32Mask Broadcast(ScalarUInt32Mask value) => new(value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarUInt32Mask All(ScalarVector3UInt32Mask value) => value.X & value.Y & value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarUInt32Mask Any(ScalarVector3UInt32Mask value) => value.X | value.Y | value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarUInt32Mask None(ScalarVector3UInt32Mask value) => ~(value.X | value.Y | value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt32Mask AndNot(ScalarVector3UInt32Mask left, ScalarVector3UInt32Mask right)
    {
        return new(
            ScalarUInt32Mask.AndNot(left.X, right.X),
            ScalarUInt32Mask.AndNot(left.Y, right.Y),
            ScalarUInt32Mask.AndNot(left.Z, right.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt32Mask Select(ScalarVector3UInt32Mask mask, ScalarVector3UInt32Mask ifTrue, ScalarVector3UInt32Mask ifFalse)
    {
        return new(
            ScalarUInt32Mask.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarUInt32Mask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarUInt32Mask.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt32Mask And(ScalarVector3UInt32Mask left, ScalarVector3UInt32Mask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt32Mask Or(ScalarVector3UInt32Mask left, ScalarVector3UInt32Mask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt32Mask Xor(ScalarVector3UInt32Mask left, ScalarVector3UInt32Mask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt32Mask Not(ScalarVector3UInt32Mask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt32Mask operator &(ScalarVector3UInt32Mask left, ScalarVector3UInt32Mask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt32Mask operator |(ScalarVector3UInt32Mask left, ScalarVector3UInt32Mask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt32Mask operator ^(ScalarVector3UInt32Mask left, ScalarVector3UInt32Mask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt32Mask operator ~(ScalarVector3UInt32Mask value) => new(~value.X, ~value.Y, ~value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt32Mask operator ==(ScalarVector3UInt32Mask left, ScalarVector3UInt32Mask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3UInt32Mask operator !=(ScalarVector3UInt32Mask left, ScalarVector3UInt32Mask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is ScalarVector3UInt32Mask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}