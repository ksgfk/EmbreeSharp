using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector3UInt32Neon :
    ISimdIntegerVector3<PacketVector3UInt32Neon, PacketUInt32Neon, uint, PacketVector3UInt32NeonMask, PacketUInt32NeonMask>
{
    public PacketVector3UInt32Neon(PacketUInt32Neon x, PacketUInt32Neon y, PacketUInt32Neon z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketUInt32Neon.LaneCount;

    public PacketUInt32Neon X { get; }

    public PacketUInt32Neon Y { get; }

    public PacketUInt32Neon Z { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Neon Create(PacketUInt32Neon x, PacketUInt32Neon y, PacketUInt32Neon z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Neon Broadcast(uint value)
    {
        PacketUInt32Neon packet = PacketUInt32Neon.Broadcast(value);
        return new(packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Neon Select(PacketVector3UInt32NeonMask mask, PacketVector3UInt32Neon ifTrue, PacketVector3UInt32Neon ifFalse)
    {
        return new(
            PacketUInt32Neon.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt32Neon.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketUInt32Neon.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Neon Select(PacketUInt32NeonMask mask, PacketVector3UInt32Neon ifTrue, PacketVector3UInt32Neon ifFalse)
    {
        return new(
            PacketUInt32Neon.Select(mask, ifTrue.X, ifFalse.X),
            PacketUInt32Neon.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketUInt32Neon.Select(mask, ifTrue.Z, ifFalse.Z));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Neon operator +(PacketVector3UInt32Neon value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Neon operator +(PacketVector3UInt32Neon left, PacketVector3UInt32Neon right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Neon operator -(PacketVector3UInt32Neon left, PacketVector3UInt32Neon right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Neon operator *(PacketVector3UInt32Neon left, PacketVector3UInt32Neon right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Neon operator -(PacketVector3UInt32Neon value) => new(-value.X, -value.Y, -value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32NeonMask operator ==(PacketVector3UInt32Neon left, PacketVector3UInt32Neon right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32NeonMask operator !=(PacketVector3UInt32Neon left, PacketVector3UInt32Neon right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3UInt32Neon other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}

public readonly struct PacketVector3UInt32NeonMask :
    ISimdVector3Mask<PacketVector3UInt32NeonMask, PacketUInt32NeonMask>
{
    public PacketVector3UInt32NeonMask(PacketUInt32NeonMask x, PacketUInt32NeonMask y, PacketUInt32NeonMask z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketUInt32NeonMask.LaneCount;

    public PacketUInt32NeonMask X { get; }

    public PacketUInt32NeonMask Y { get; }

    public PacketUInt32NeonMask Z { get; }

    public static PacketVector3UInt32NeonMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt32NeonMask.True, PacketUInt32NeonMask.True, PacketUInt32NeonMask.True);
    }

    public static PacketVector3UInt32NeonMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt32NeonMask.False, PacketUInt32NeonMask.False, PacketUInt32NeonMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32NeonMask Create(PacketUInt32NeonMask x, PacketUInt32NeonMask y, PacketUInt32NeonMask z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32NeonMask Broadcast(PacketUInt32NeonMask value) => new(value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask All(PacketVector3UInt32NeonMask value) => value.X & value.Y & value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask Any(PacketVector3UInt32NeonMask value) => value.X | value.Y | value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask None(PacketVector3UInt32NeonMask value) => ~(value.X | value.Y | value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32NeonMask AndNot(PacketVector3UInt32NeonMask left, PacketVector3UInt32NeonMask right)
    {
        return new(
            PacketUInt32NeonMask.AndNot(left.X, right.X),
            PacketUInt32NeonMask.AndNot(left.Y, right.Y),
            PacketUInt32NeonMask.AndNot(left.Z, right.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32NeonMask Select(PacketVector3UInt32NeonMask mask, PacketVector3UInt32NeonMask ifTrue, PacketVector3UInt32NeonMask ifFalse)
    {
        return new(
            PacketUInt32NeonMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt32NeonMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketUInt32NeonMask.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32NeonMask And(PacketVector3UInt32NeonMask left, PacketVector3UInt32NeonMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32NeonMask Or(PacketVector3UInt32NeonMask left, PacketVector3UInt32NeonMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32NeonMask Xor(PacketVector3UInt32NeonMask left, PacketVector3UInt32NeonMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32NeonMask Not(PacketVector3UInt32NeonMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32NeonMask operator &(PacketVector3UInt32NeonMask left, PacketVector3UInt32NeonMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32NeonMask operator |(PacketVector3UInt32NeonMask left, PacketVector3UInt32NeonMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32NeonMask operator ^(PacketVector3UInt32NeonMask left, PacketVector3UInt32NeonMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32NeonMask operator ~(PacketVector3UInt32NeonMask value) => new(~value.X, ~value.Y, ~value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32NeonMask operator ==(PacketVector3UInt32NeonMask left, PacketVector3UInt32NeonMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32NeonMask operator !=(PacketVector3UInt32NeonMask left, PacketVector3UInt32NeonMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3UInt32NeonMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}