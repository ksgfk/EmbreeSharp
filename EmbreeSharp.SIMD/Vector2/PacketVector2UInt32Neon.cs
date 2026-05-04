using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector2UInt32Neon :
    ISimdIntegerVector2<PacketVector2UInt32Neon, PacketUInt32Neon, uint, PacketVector2UInt32NeonMask, PacketUInt32NeonMask>
{
    public PacketVector2UInt32Neon(PacketUInt32Neon x, PacketUInt32Neon y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketUInt32Neon.LaneCount;

    public PacketUInt32Neon X { get; }

    public PacketUInt32Neon Y { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Neon Create(PacketUInt32Neon x, PacketUInt32Neon y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Neon Broadcast(uint value)
    {
        PacketUInt32Neon packet = PacketUInt32Neon.Broadcast(value);
        return new(packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Neon Select(PacketVector2UInt32NeonMask mask, PacketVector2UInt32Neon ifTrue, PacketVector2UInt32Neon ifFalse)
    {
        return new(
            PacketUInt32Neon.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt32Neon.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Neon Select(PacketUInt32NeonMask mask, PacketVector2UInt32Neon ifTrue, PacketVector2UInt32Neon ifFalse)
    {
        return new(
            PacketUInt32Neon.Select(mask, ifTrue.X, ifFalse.X),
            PacketUInt32Neon.Select(mask, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Neon operator +(PacketVector2UInt32Neon value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Neon operator +(PacketVector2UInt32Neon left, PacketVector2UInt32Neon right) => new(left.X + right.X, left.Y + right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Neon operator -(PacketVector2UInt32Neon left, PacketVector2UInt32Neon right) => new(left.X - right.X, left.Y - right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Neon operator *(PacketVector2UInt32Neon left, PacketVector2UInt32Neon right) => new(left.X * right.X, left.Y * right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Neon operator -(PacketVector2UInt32Neon value) => new(-value.X, -value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32NeonMask operator ==(PacketVector2UInt32Neon left, PacketVector2UInt32Neon right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32NeonMask operator !=(PacketVector2UInt32Neon left, PacketVector2UInt32Neon right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2UInt32Neon other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}

public readonly struct PacketVector2UInt32NeonMask :
    ISimdVector2Mask<PacketVector2UInt32NeonMask, PacketUInt32NeonMask>
{
    public PacketVector2UInt32NeonMask(PacketUInt32NeonMask x, PacketUInt32NeonMask y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketUInt32NeonMask.LaneCount;

    public PacketUInt32NeonMask X { get; }

    public PacketUInt32NeonMask Y { get; }

    public static PacketVector2UInt32NeonMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt32NeonMask.True, PacketUInt32NeonMask.True);
    }

    public static PacketVector2UInt32NeonMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt32NeonMask.False, PacketUInt32NeonMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32NeonMask Create(PacketUInt32NeonMask x, PacketUInt32NeonMask y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32NeonMask Broadcast(PacketUInt32NeonMask value) => new(value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask All(PacketVector2UInt32NeonMask value) => value.X & value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask Any(PacketVector2UInt32NeonMask value) => value.X | value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask None(PacketVector2UInt32NeonMask value) => ~(value.X | value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32NeonMask AndNot(PacketVector2UInt32NeonMask left, PacketVector2UInt32NeonMask right)
    {
        return new(
            PacketUInt32NeonMask.AndNot(left.X, right.X),
            PacketUInt32NeonMask.AndNot(left.Y, right.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32NeonMask Select(PacketVector2UInt32NeonMask mask, PacketVector2UInt32NeonMask ifTrue, PacketVector2UInt32NeonMask ifFalse)
    {
        return new(
            PacketUInt32NeonMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt32NeonMask.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32NeonMask And(PacketVector2UInt32NeonMask left, PacketVector2UInt32NeonMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32NeonMask Or(PacketVector2UInt32NeonMask left, PacketVector2UInt32NeonMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32NeonMask Xor(PacketVector2UInt32NeonMask left, PacketVector2UInt32NeonMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32NeonMask Not(PacketVector2UInt32NeonMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32NeonMask operator &(PacketVector2UInt32NeonMask left, PacketVector2UInt32NeonMask right) => new(left.X & right.X, left.Y & right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32NeonMask operator |(PacketVector2UInt32NeonMask left, PacketVector2UInt32NeonMask right) => new(left.X | right.X, left.Y | right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32NeonMask operator ^(PacketVector2UInt32NeonMask left, PacketVector2UInt32NeonMask right) => new(left.X ^ right.X, left.Y ^ right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32NeonMask operator ~(PacketVector2UInt32NeonMask value) => new(~value.X, ~value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32NeonMask operator ==(PacketVector2UInt32NeonMask left, PacketVector2UInt32NeonMask right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32NeonMask operator !=(PacketVector2UInt32NeonMask left, PacketVector2UInt32NeonMask right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2UInt32NeonMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}