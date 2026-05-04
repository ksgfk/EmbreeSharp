using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector3Int64Neon :
    ISimdIntegerVector3<PacketVector3Int64Neon, PacketInt64Neon, long, PacketVector3Int64NeonMask, PacketInt64NeonMask>
{
    public PacketVector3Int64Neon(PacketInt64Neon x, PacketInt64Neon y, PacketInt64Neon z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketInt64Neon.LaneCount;

    public PacketInt64Neon X { get; }

    public PacketInt64Neon Y { get; }

    public PacketInt64Neon Z { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Neon Create(PacketInt64Neon x, PacketInt64Neon y, PacketInt64Neon z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Neon Broadcast(long value)
    {
        PacketInt64Neon packet = PacketInt64Neon.Broadcast(value);
        return new(packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Neon Select(PacketVector3Int64NeonMask mask, PacketVector3Int64Neon ifTrue, PacketVector3Int64Neon ifFalse)
    {
        return new(
            PacketInt64Neon.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt64Neon.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketInt64Neon.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Neon Select(PacketInt64NeonMask mask, PacketVector3Int64Neon ifTrue, PacketVector3Int64Neon ifFalse)
    {
        return new(
            PacketInt64Neon.Select(mask, ifTrue.X, ifFalse.X),
            PacketInt64Neon.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketInt64Neon.Select(mask, ifTrue.Z, ifFalse.Z));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Neon operator +(PacketVector3Int64Neon value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Neon operator +(PacketVector3Int64Neon left, PacketVector3Int64Neon right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Neon operator -(PacketVector3Int64Neon left, PacketVector3Int64Neon right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Neon operator *(PacketVector3Int64Neon left, PacketVector3Int64Neon right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Neon operator -(PacketVector3Int64Neon value) => new(-value.X, -value.Y, -value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64NeonMask operator ==(PacketVector3Int64Neon left, PacketVector3Int64Neon right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64NeonMask operator !=(PacketVector3Int64Neon left, PacketVector3Int64Neon right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3Int64Neon other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}

public readonly struct PacketVector3Int64NeonMask :
    ISimdVector3Mask<PacketVector3Int64NeonMask, PacketInt64NeonMask>
{
    public PacketVector3Int64NeonMask(PacketInt64NeonMask x, PacketInt64NeonMask y, PacketInt64NeonMask z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketInt64NeonMask.LaneCount;

    public PacketInt64NeonMask X { get; }

    public PacketInt64NeonMask Y { get; }

    public PacketInt64NeonMask Z { get; }

    public static PacketVector3Int64NeonMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt64NeonMask.True, PacketInt64NeonMask.True, PacketInt64NeonMask.True);
    }

    public static PacketVector3Int64NeonMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt64NeonMask.False, PacketInt64NeonMask.False, PacketInt64NeonMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64NeonMask Create(PacketInt64NeonMask x, PacketInt64NeonMask y, PacketInt64NeonMask z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64NeonMask Broadcast(PacketInt64NeonMask value) => new(value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask All(PacketVector3Int64NeonMask value) => value.X & value.Y & value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask Any(PacketVector3Int64NeonMask value) => value.X | value.Y | value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask None(PacketVector3Int64NeonMask value) => ~(value.X | value.Y | value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64NeonMask AndNot(PacketVector3Int64NeonMask left, PacketVector3Int64NeonMask right)
    {
        return new(
            PacketInt64NeonMask.AndNot(left.X, right.X),
            PacketInt64NeonMask.AndNot(left.Y, right.Y),
            PacketInt64NeonMask.AndNot(left.Z, right.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64NeonMask Select(PacketVector3Int64NeonMask mask, PacketVector3Int64NeonMask ifTrue, PacketVector3Int64NeonMask ifFalse)
    {
        return new(
            PacketInt64NeonMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt64NeonMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketInt64NeonMask.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64NeonMask And(PacketVector3Int64NeonMask left, PacketVector3Int64NeonMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64NeonMask Or(PacketVector3Int64NeonMask left, PacketVector3Int64NeonMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64NeonMask Xor(PacketVector3Int64NeonMask left, PacketVector3Int64NeonMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64NeonMask Not(PacketVector3Int64NeonMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64NeonMask operator &(PacketVector3Int64NeonMask left, PacketVector3Int64NeonMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64NeonMask operator |(PacketVector3Int64NeonMask left, PacketVector3Int64NeonMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64NeonMask operator ^(PacketVector3Int64NeonMask left, PacketVector3Int64NeonMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64NeonMask operator ~(PacketVector3Int64NeonMask value) => new(~value.X, ~value.Y, ~value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64NeonMask operator ==(PacketVector3Int64NeonMask left, PacketVector3Int64NeonMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64NeonMask operator !=(PacketVector3Int64NeonMask left, PacketVector3Int64NeonMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3Int64NeonMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}