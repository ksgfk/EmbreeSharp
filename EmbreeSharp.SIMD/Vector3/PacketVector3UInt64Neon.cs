using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector3UInt64Neon :
    ISimdIntegerVector3<PacketVector3UInt64Neon, PacketUInt64Neon, ulong, PacketVector3UInt64NeonMask, PacketUInt64NeonMask>
{
    public PacketVector3UInt64Neon(PacketUInt64Neon x, PacketUInt64Neon y, PacketUInt64Neon z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketUInt64Neon.LaneCount;

    public PacketUInt64Neon X { get; }

    public PacketUInt64Neon Y { get; }

    public PacketUInt64Neon Z { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Neon Create(PacketUInt64Neon x, PacketUInt64Neon y, PacketUInt64Neon z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Neon Broadcast(ulong value)
    {
        PacketUInt64Neon packet = PacketUInt64Neon.Broadcast(value);
        return new(packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Neon Select(PacketVector3UInt64NeonMask mask, PacketVector3UInt64Neon ifTrue, PacketVector3UInt64Neon ifFalse)
    {
        return new(
            PacketUInt64Neon.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt64Neon.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketUInt64Neon.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Neon Select(PacketUInt64NeonMask mask, PacketVector3UInt64Neon ifTrue, PacketVector3UInt64Neon ifFalse)
    {
        return new(
            PacketUInt64Neon.Select(mask, ifTrue.X, ifFalse.X),
            PacketUInt64Neon.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketUInt64Neon.Select(mask, ifTrue.Z, ifFalse.Z));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Neon operator +(PacketVector3UInt64Neon value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Neon operator +(PacketVector3UInt64Neon left, PacketVector3UInt64Neon right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Neon operator -(PacketVector3UInt64Neon left, PacketVector3UInt64Neon right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Neon operator *(PacketVector3UInt64Neon left, PacketVector3UInt64Neon right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Neon operator -(PacketVector3UInt64Neon value) => new(-value.X, -value.Y, -value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64NeonMask operator ==(PacketVector3UInt64Neon left, PacketVector3UInt64Neon right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64NeonMask operator !=(PacketVector3UInt64Neon left, PacketVector3UInt64Neon right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3UInt64Neon other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}

public readonly struct PacketVector3UInt64NeonMask :
    ISimdVector3Mask<PacketVector3UInt64NeonMask, PacketUInt64NeonMask>
{
    public PacketVector3UInt64NeonMask(PacketUInt64NeonMask x, PacketUInt64NeonMask y, PacketUInt64NeonMask z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketUInt64NeonMask.LaneCount;

    public PacketUInt64NeonMask X { get; }

    public PacketUInt64NeonMask Y { get; }

    public PacketUInt64NeonMask Z { get; }

    public static PacketVector3UInt64NeonMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt64NeonMask.True, PacketUInt64NeonMask.True, PacketUInt64NeonMask.True);
    }

    public static PacketVector3UInt64NeonMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt64NeonMask.False, PacketUInt64NeonMask.False, PacketUInt64NeonMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64NeonMask Create(PacketUInt64NeonMask x, PacketUInt64NeonMask y, PacketUInt64NeonMask z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64NeonMask Broadcast(PacketUInt64NeonMask value) => new(value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask All(PacketVector3UInt64NeonMask value) => value.X & value.Y & value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask Any(PacketVector3UInt64NeonMask value) => value.X | value.Y | value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask None(PacketVector3UInt64NeonMask value) => ~(value.X | value.Y | value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64NeonMask AndNot(PacketVector3UInt64NeonMask left, PacketVector3UInt64NeonMask right)
    {
        return new(
            PacketUInt64NeonMask.AndNot(left.X, right.X),
            PacketUInt64NeonMask.AndNot(left.Y, right.Y),
            PacketUInt64NeonMask.AndNot(left.Z, right.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64NeonMask Select(PacketVector3UInt64NeonMask mask, PacketVector3UInt64NeonMask ifTrue, PacketVector3UInt64NeonMask ifFalse)
    {
        return new(
            PacketUInt64NeonMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt64NeonMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketUInt64NeonMask.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64NeonMask And(PacketVector3UInt64NeonMask left, PacketVector3UInt64NeonMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64NeonMask Or(PacketVector3UInt64NeonMask left, PacketVector3UInt64NeonMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64NeonMask Xor(PacketVector3UInt64NeonMask left, PacketVector3UInt64NeonMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64NeonMask Not(PacketVector3UInt64NeonMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64NeonMask operator &(PacketVector3UInt64NeonMask left, PacketVector3UInt64NeonMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64NeonMask operator |(PacketVector3UInt64NeonMask left, PacketVector3UInt64NeonMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64NeonMask operator ^(PacketVector3UInt64NeonMask left, PacketVector3UInt64NeonMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64NeonMask operator ~(PacketVector3UInt64NeonMask value) => new(~value.X, ~value.Y, ~value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64NeonMask operator ==(PacketVector3UInt64NeonMask left, PacketVector3UInt64NeonMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64NeonMask operator !=(PacketVector3UInt64NeonMask left, PacketVector3UInt64NeonMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3UInt64NeonMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}