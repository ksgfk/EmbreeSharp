using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector3Int32Neon :
    ISimdIntegerVector3<PacketVector3Int32Neon, PacketInt32Neon, int, PacketVector3Int32NeonMask, PacketInt32NeonMask>
{
    public PacketVector3Int32Neon(PacketInt32Neon x, PacketInt32Neon y, PacketInt32Neon z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketInt32Neon.LaneCount;

    public PacketInt32Neon X { get; }

    public PacketInt32Neon Y { get; }

    public PacketInt32Neon Z { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Neon Create(PacketInt32Neon x, PacketInt32Neon y, PacketInt32Neon z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Neon Broadcast(int value)
    {
        PacketInt32Neon packet = PacketInt32Neon.Broadcast(value);
        return new(packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Neon Select(PacketVector3Int32NeonMask mask, PacketVector3Int32Neon ifTrue, PacketVector3Int32Neon ifFalse)
    {
        return new(
            PacketInt32Neon.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt32Neon.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketInt32Neon.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Neon Select(PacketInt32NeonMask mask, PacketVector3Int32Neon ifTrue, PacketVector3Int32Neon ifFalse)
    {
        return new(
            PacketInt32Neon.Select(mask, ifTrue.X, ifFalse.X),
            PacketInt32Neon.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketInt32Neon.Select(mask, ifTrue.Z, ifFalse.Z));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Neon operator +(PacketVector3Int32Neon value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Neon operator +(PacketVector3Int32Neon left, PacketVector3Int32Neon right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Neon operator -(PacketVector3Int32Neon left, PacketVector3Int32Neon right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Neon operator *(PacketVector3Int32Neon left, PacketVector3Int32Neon right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Neon operator -(PacketVector3Int32Neon value) => new(-value.X, -value.Y, -value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32NeonMask operator ==(PacketVector3Int32Neon left, PacketVector3Int32Neon right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32NeonMask operator !=(PacketVector3Int32Neon left, PacketVector3Int32Neon right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3Int32Neon other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}

public readonly struct PacketVector3Int32NeonMask :
    ISimdVector3Mask<PacketVector3Int32NeonMask, PacketInt32NeonMask>
{
    public PacketVector3Int32NeonMask(PacketInt32NeonMask x, PacketInt32NeonMask y, PacketInt32NeonMask z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketInt32NeonMask.LaneCount;

    public PacketInt32NeonMask X { get; }

    public PacketInt32NeonMask Y { get; }

    public PacketInt32NeonMask Z { get; }

    public static PacketVector3Int32NeonMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt32NeonMask.True, PacketInt32NeonMask.True, PacketInt32NeonMask.True);
    }

    public static PacketVector3Int32NeonMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt32NeonMask.False, PacketInt32NeonMask.False, PacketInt32NeonMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32NeonMask Create(PacketInt32NeonMask x, PacketInt32NeonMask y, PacketInt32NeonMask z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32NeonMask Broadcast(PacketInt32NeonMask value) => new(value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32NeonMask All(PacketVector3Int32NeonMask value) => value.X & value.Y & value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32NeonMask Any(PacketVector3Int32NeonMask value) => value.X | value.Y | value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32NeonMask None(PacketVector3Int32NeonMask value) => ~(value.X | value.Y | value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32NeonMask AndNot(PacketVector3Int32NeonMask left, PacketVector3Int32NeonMask right)
    {
        return new(
            PacketInt32NeonMask.AndNot(left.X, right.X),
            PacketInt32NeonMask.AndNot(left.Y, right.Y),
            PacketInt32NeonMask.AndNot(left.Z, right.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32NeonMask Select(PacketVector3Int32NeonMask mask, PacketVector3Int32NeonMask ifTrue, PacketVector3Int32NeonMask ifFalse)
    {
        return new(
            PacketInt32NeonMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt32NeonMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketInt32NeonMask.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32NeonMask And(PacketVector3Int32NeonMask left, PacketVector3Int32NeonMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32NeonMask Or(PacketVector3Int32NeonMask left, PacketVector3Int32NeonMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32NeonMask Xor(PacketVector3Int32NeonMask left, PacketVector3Int32NeonMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32NeonMask Not(PacketVector3Int32NeonMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32NeonMask operator &(PacketVector3Int32NeonMask left, PacketVector3Int32NeonMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32NeonMask operator |(PacketVector3Int32NeonMask left, PacketVector3Int32NeonMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32NeonMask operator ^(PacketVector3Int32NeonMask left, PacketVector3Int32NeonMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32NeonMask operator ~(PacketVector3Int32NeonMask value) => new(~value.X, ~value.Y, ~value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32NeonMask operator ==(PacketVector3Int32NeonMask left, PacketVector3Int32NeonMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32NeonMask operator !=(PacketVector3Int32NeonMask left, PacketVector3Int32NeonMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3Int32NeonMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}