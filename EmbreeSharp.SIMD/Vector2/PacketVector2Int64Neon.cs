using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector2Int64Neon :
    ISimdIntegerVector2<PacketVector2Int64Neon, PacketInt64Neon, long, PacketVector2Int64NeonMask, PacketInt64NeonMask>
{
    public PacketVector2Int64Neon(PacketInt64Neon x, PacketInt64Neon y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketInt64Neon.LaneCount;

    public PacketInt64Neon X { get; }

    public PacketInt64Neon Y { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Neon Create(PacketInt64Neon x, PacketInt64Neon y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Neon Broadcast(long value)
    {
        PacketInt64Neon packet = PacketInt64Neon.Broadcast(value);
        return new(packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Neon Select(PacketVector2Int64NeonMask mask, PacketVector2Int64Neon ifTrue, PacketVector2Int64Neon ifFalse)
    {
        return new(
            PacketInt64Neon.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt64Neon.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Neon Select(PacketInt64NeonMask mask, PacketVector2Int64Neon ifTrue, PacketVector2Int64Neon ifFalse)
    {
        return new(
            PacketInt64Neon.Select(mask, ifTrue.X, ifFalse.X),
            PacketInt64Neon.Select(mask, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Neon operator +(PacketVector2Int64Neon value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Neon operator +(PacketVector2Int64Neon left, PacketVector2Int64Neon right) => new(left.X + right.X, left.Y + right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Neon operator -(PacketVector2Int64Neon left, PacketVector2Int64Neon right) => new(left.X - right.X, left.Y - right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Neon operator *(PacketVector2Int64Neon left, PacketVector2Int64Neon right) => new(left.X * right.X, left.Y * right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Neon operator -(PacketVector2Int64Neon value) => new(-value.X, -value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64NeonMask operator ==(PacketVector2Int64Neon left, PacketVector2Int64Neon right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64NeonMask operator !=(PacketVector2Int64Neon left, PacketVector2Int64Neon right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2Int64Neon other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}

public readonly struct PacketVector2Int64NeonMask :
    ISimdVector2Mask<PacketVector2Int64NeonMask, PacketInt64NeonMask>
{
    public PacketVector2Int64NeonMask(PacketInt64NeonMask x, PacketInt64NeonMask y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketInt64NeonMask.LaneCount;

    public PacketInt64NeonMask X { get; }

    public PacketInt64NeonMask Y { get; }

    public static PacketVector2Int64NeonMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt64NeonMask.True, PacketInt64NeonMask.True);
    }

    public static PacketVector2Int64NeonMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt64NeonMask.False, PacketInt64NeonMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64NeonMask Create(PacketInt64NeonMask x, PacketInt64NeonMask y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64NeonMask Broadcast(PacketInt64NeonMask value) => new(value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask All(PacketVector2Int64NeonMask value) => value.X & value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask Any(PacketVector2Int64NeonMask value) => value.X | value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask None(PacketVector2Int64NeonMask value) => ~(value.X | value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64NeonMask AndNot(PacketVector2Int64NeonMask left, PacketVector2Int64NeonMask right)
    {
        return new(
            PacketInt64NeonMask.AndNot(left.X, right.X),
            PacketInt64NeonMask.AndNot(left.Y, right.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64NeonMask Select(PacketVector2Int64NeonMask mask, PacketVector2Int64NeonMask ifTrue, PacketVector2Int64NeonMask ifFalse)
    {
        return new(
            PacketInt64NeonMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt64NeonMask.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64NeonMask And(PacketVector2Int64NeonMask left, PacketVector2Int64NeonMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64NeonMask Or(PacketVector2Int64NeonMask left, PacketVector2Int64NeonMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64NeonMask Xor(PacketVector2Int64NeonMask left, PacketVector2Int64NeonMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64NeonMask Not(PacketVector2Int64NeonMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64NeonMask operator &(PacketVector2Int64NeonMask left, PacketVector2Int64NeonMask right) => new(left.X & right.X, left.Y & right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64NeonMask operator |(PacketVector2Int64NeonMask left, PacketVector2Int64NeonMask right) => new(left.X | right.X, left.Y | right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64NeonMask operator ^(PacketVector2Int64NeonMask left, PacketVector2Int64NeonMask right) => new(left.X ^ right.X, left.Y ^ right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64NeonMask operator ~(PacketVector2Int64NeonMask value) => new(~value.X, ~value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64NeonMask operator ==(PacketVector2Int64NeonMask left, PacketVector2Int64NeonMask right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64NeonMask operator !=(PacketVector2Int64NeonMask left, PacketVector2Int64NeonMask right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2Int64NeonMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}