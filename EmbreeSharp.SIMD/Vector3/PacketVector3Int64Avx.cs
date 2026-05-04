using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector3Int64Avx :
    ISimdIntegerVector3<PacketVector3Int64Avx, PacketInt64Avx, long, PacketVector3Int64AvxMask, PacketInt64AvxMask>
{
    public PacketVector3Int64Avx(PacketInt64Avx x, PacketInt64Avx y, PacketInt64Avx z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketInt64Avx.LaneCount;

    public PacketInt64Avx X { get; }

    public PacketInt64Avx Y { get; }

    public PacketInt64Avx Z { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Avx Create(PacketInt64Avx x, PacketInt64Avx y, PacketInt64Avx z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Avx Broadcast(long value)
    {
        PacketInt64Avx packet = PacketInt64Avx.Broadcast(value);
        return new(packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Avx Select(PacketVector3Int64AvxMask mask, PacketVector3Int64Avx ifTrue, PacketVector3Int64Avx ifFalse)
    {
        return new(
            PacketInt64Avx.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt64Avx.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketInt64Avx.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Avx Select(PacketInt64AvxMask mask, PacketVector3Int64Avx ifTrue, PacketVector3Int64Avx ifFalse)
    {
        return new(
            PacketInt64Avx.Select(mask, ifTrue.X, ifFalse.X),
            PacketInt64Avx.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketInt64Avx.Select(mask, ifTrue.Z, ifFalse.Z));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Avx operator +(PacketVector3Int64Avx value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Avx operator +(PacketVector3Int64Avx left, PacketVector3Int64Avx right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Avx operator -(PacketVector3Int64Avx left, PacketVector3Int64Avx right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Avx operator *(PacketVector3Int64Avx left, PacketVector3Int64Avx right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Avx operator -(PacketVector3Int64Avx value) => new(-value.X, -value.Y, -value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64AvxMask operator ==(PacketVector3Int64Avx left, PacketVector3Int64Avx right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64AvxMask operator !=(PacketVector3Int64Avx left, PacketVector3Int64Avx right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3Int64Avx other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}

public readonly struct PacketVector3Int64AvxMask :
    ISimdVector3Mask<PacketVector3Int64AvxMask, PacketInt64AvxMask>
{
    public PacketVector3Int64AvxMask(PacketInt64AvxMask x, PacketInt64AvxMask y, PacketInt64AvxMask z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketInt64AvxMask.LaneCount;

    public PacketInt64AvxMask X { get; }

    public PacketInt64AvxMask Y { get; }

    public PacketInt64AvxMask Z { get; }

    public static PacketVector3Int64AvxMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt64AvxMask.True, PacketInt64AvxMask.True, PacketInt64AvxMask.True);
    }

    public static PacketVector3Int64AvxMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt64AvxMask.False, PacketInt64AvxMask.False, PacketInt64AvxMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64AvxMask Create(PacketInt64AvxMask x, PacketInt64AvxMask y, PacketInt64AvxMask z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64AvxMask Broadcast(PacketInt64AvxMask value) => new(value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask All(PacketVector3Int64AvxMask value) => value.X & value.Y & value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask Any(PacketVector3Int64AvxMask value) => value.X | value.Y | value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask None(PacketVector3Int64AvxMask value) => ~(value.X | value.Y | value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64AvxMask AndNot(PacketVector3Int64AvxMask left, PacketVector3Int64AvxMask right)
    {
        return new(
            PacketInt64AvxMask.AndNot(left.X, right.X),
            PacketInt64AvxMask.AndNot(left.Y, right.Y),
            PacketInt64AvxMask.AndNot(left.Z, right.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64AvxMask Select(PacketVector3Int64AvxMask mask, PacketVector3Int64AvxMask ifTrue, PacketVector3Int64AvxMask ifFalse)
    {
        return new(
            PacketInt64AvxMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt64AvxMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketInt64AvxMask.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64AvxMask And(PacketVector3Int64AvxMask left, PacketVector3Int64AvxMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64AvxMask Or(PacketVector3Int64AvxMask left, PacketVector3Int64AvxMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64AvxMask Xor(PacketVector3Int64AvxMask left, PacketVector3Int64AvxMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64AvxMask Not(PacketVector3Int64AvxMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64AvxMask operator &(PacketVector3Int64AvxMask left, PacketVector3Int64AvxMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64AvxMask operator |(PacketVector3Int64AvxMask left, PacketVector3Int64AvxMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64AvxMask operator ^(PacketVector3Int64AvxMask left, PacketVector3Int64AvxMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64AvxMask operator ~(PacketVector3Int64AvxMask value) => new(~value.X, ~value.Y, ~value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64AvxMask operator ==(PacketVector3Int64AvxMask left, PacketVector3Int64AvxMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64AvxMask operator !=(PacketVector3Int64AvxMask left, PacketVector3Int64AvxMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3Int64AvxMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}