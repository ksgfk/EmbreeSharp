using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector2UInt64Neon :
    ISimdIntegerVector2<PacketVector2UInt64Neon, PacketUInt64Neon, ulong, PacketVector2UInt64NeonMask, PacketUInt64NeonMask>
{
    public PacketVector2UInt64Neon(PacketUInt64Neon x, PacketUInt64Neon y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketUInt64Neon.LaneCount;

    public PacketUInt64Neon X { get; }

    public PacketUInt64Neon Y { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Neon Create(PacketUInt64Neon x, PacketUInt64Neon y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Neon Broadcast(ulong value)
    {
        PacketUInt64Neon packet = PacketUInt64Neon.Broadcast(value);
        return new(packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Neon Select(PacketVector2UInt64NeonMask mask, PacketVector2UInt64Neon ifTrue, PacketVector2UInt64Neon ifFalse)
    {
        return new(
            PacketUInt64Neon.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt64Neon.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Neon Select(PacketUInt64NeonMask mask, PacketVector2UInt64Neon ifTrue, PacketVector2UInt64Neon ifFalse)
    {
        return new(
            PacketUInt64Neon.Select(mask, ifTrue.X, ifFalse.X),
            PacketUInt64Neon.Select(mask, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Neon operator +(PacketVector2UInt64Neon value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Neon operator +(PacketVector2UInt64Neon left, PacketVector2UInt64Neon right) => new(left.X + right.X, left.Y + right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Neon operator -(PacketVector2UInt64Neon left, PacketVector2UInt64Neon right) => new(left.X - right.X, left.Y - right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Neon operator *(PacketVector2UInt64Neon left, PacketVector2UInt64Neon right) => new(left.X * right.X, left.Y * right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Neon operator -(PacketVector2UInt64Neon value) => new(-value.X, -value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64NeonMask operator ==(PacketVector2UInt64Neon left, PacketVector2UInt64Neon right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64NeonMask operator !=(PacketVector2UInt64Neon left, PacketVector2UInt64Neon right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2UInt64Neon other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}

public readonly struct PacketVector2UInt64NeonMask :
    ISimdVector2Mask<PacketVector2UInt64NeonMask, PacketUInt64NeonMask>
{
    public PacketVector2UInt64NeonMask(PacketUInt64NeonMask x, PacketUInt64NeonMask y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketUInt64NeonMask.LaneCount;

    public PacketUInt64NeonMask X { get; }

    public PacketUInt64NeonMask Y { get; }

    public static PacketVector2UInt64NeonMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt64NeonMask.True, PacketUInt64NeonMask.True);
    }

    public static PacketVector2UInt64NeonMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt64NeonMask.False, PacketUInt64NeonMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64NeonMask Create(PacketUInt64NeonMask x, PacketUInt64NeonMask y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64NeonMask Broadcast(PacketUInt64NeonMask value) => new(value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask All(PacketVector2UInt64NeonMask value) => value.X & value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask Any(PacketVector2UInt64NeonMask value) => value.X | value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask None(PacketVector2UInt64NeonMask value) => ~(value.X | value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64NeonMask AndNot(PacketVector2UInt64NeonMask left, PacketVector2UInt64NeonMask right)
    {
        return new(
            PacketUInt64NeonMask.AndNot(left.X, right.X),
            PacketUInt64NeonMask.AndNot(left.Y, right.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64NeonMask Select(PacketVector2UInt64NeonMask mask, PacketVector2UInt64NeonMask ifTrue, PacketVector2UInt64NeonMask ifFalse)
    {
        return new(
            PacketUInt64NeonMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt64NeonMask.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64NeonMask And(PacketVector2UInt64NeonMask left, PacketVector2UInt64NeonMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64NeonMask Or(PacketVector2UInt64NeonMask left, PacketVector2UInt64NeonMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64NeonMask Xor(PacketVector2UInt64NeonMask left, PacketVector2UInt64NeonMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64NeonMask Not(PacketVector2UInt64NeonMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64NeonMask operator &(PacketVector2UInt64NeonMask left, PacketVector2UInt64NeonMask right) => new(left.X & right.X, left.Y & right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64NeonMask operator |(PacketVector2UInt64NeonMask left, PacketVector2UInt64NeonMask right) => new(left.X | right.X, left.Y | right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64NeonMask operator ^(PacketVector2UInt64NeonMask left, PacketVector2UInt64NeonMask right) => new(left.X ^ right.X, left.Y ^ right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64NeonMask operator ~(PacketVector2UInt64NeonMask value) => new(~value.X, ~value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64NeonMask operator ==(PacketVector2UInt64NeonMask left, PacketVector2UInt64NeonMask right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64NeonMask operator !=(PacketVector2UInt64NeonMask left, PacketVector2UInt64NeonMask right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2UInt64NeonMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}