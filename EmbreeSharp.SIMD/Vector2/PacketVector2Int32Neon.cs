using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector2Int32Neon :
    ISimdIntegerVector2<PacketVector2Int32Neon, PacketInt32Neon, int, PacketVector2Int32NeonMask, PacketInt32NeonMask>
{
    public PacketVector2Int32Neon(PacketInt32Neon x, PacketInt32Neon y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketInt32Neon.LaneCount;

    public PacketInt32Neon X { get; }

    public PacketInt32Neon Y { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Neon Create(PacketInt32Neon x, PacketInt32Neon y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Neon Broadcast(int value)
    {
        PacketInt32Neon packet = PacketInt32Neon.Broadcast(value);
        return new(packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Neon Select(PacketVector2Int32NeonMask mask, PacketVector2Int32Neon ifTrue, PacketVector2Int32Neon ifFalse)
    {
        return new(
            PacketInt32Neon.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt32Neon.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Neon Select(PacketInt32NeonMask mask, PacketVector2Int32Neon ifTrue, PacketVector2Int32Neon ifFalse)
    {
        return new(
            PacketInt32Neon.Select(mask, ifTrue.X, ifFalse.X),
            PacketInt32Neon.Select(mask, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Neon operator +(PacketVector2Int32Neon value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Neon operator +(PacketVector2Int32Neon left, PacketVector2Int32Neon right) => new(left.X + right.X, left.Y + right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Neon operator -(PacketVector2Int32Neon left, PacketVector2Int32Neon right) => new(left.X - right.X, left.Y - right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Neon operator *(PacketVector2Int32Neon left, PacketVector2Int32Neon right) => new(left.X * right.X, left.Y * right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Neon operator -(PacketVector2Int32Neon value) => new(-value.X, -value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32NeonMask operator ==(PacketVector2Int32Neon left, PacketVector2Int32Neon right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32NeonMask operator !=(PacketVector2Int32Neon left, PacketVector2Int32Neon right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2Int32Neon other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}

public readonly struct PacketVector2Int32NeonMask :
    ISimdVector2Mask<PacketVector2Int32NeonMask, PacketInt32NeonMask>
{
    public PacketVector2Int32NeonMask(PacketInt32NeonMask x, PacketInt32NeonMask y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketInt32NeonMask.LaneCount;

    public PacketInt32NeonMask X { get; }

    public PacketInt32NeonMask Y { get; }

    public static PacketVector2Int32NeonMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt32NeonMask.True, PacketInt32NeonMask.True);
    }

    public static PacketVector2Int32NeonMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt32NeonMask.False, PacketInt32NeonMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32NeonMask Create(PacketInt32NeonMask x, PacketInt32NeonMask y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32NeonMask Broadcast(PacketInt32NeonMask value) => new(value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32NeonMask All(PacketVector2Int32NeonMask value) => value.X & value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32NeonMask Any(PacketVector2Int32NeonMask value) => value.X | value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32NeonMask None(PacketVector2Int32NeonMask value) => ~(value.X | value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32NeonMask AndNot(PacketVector2Int32NeonMask left, PacketVector2Int32NeonMask right)
    {
        return new(
            PacketInt32NeonMask.AndNot(left.X, right.X),
            PacketInt32NeonMask.AndNot(left.Y, right.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32NeonMask Select(PacketVector2Int32NeonMask mask, PacketVector2Int32NeonMask ifTrue, PacketVector2Int32NeonMask ifFalse)
    {
        return new(
            PacketInt32NeonMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt32NeonMask.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32NeonMask And(PacketVector2Int32NeonMask left, PacketVector2Int32NeonMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32NeonMask Or(PacketVector2Int32NeonMask left, PacketVector2Int32NeonMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32NeonMask Xor(PacketVector2Int32NeonMask left, PacketVector2Int32NeonMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32NeonMask Not(PacketVector2Int32NeonMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32NeonMask operator &(PacketVector2Int32NeonMask left, PacketVector2Int32NeonMask right) => new(left.X & right.X, left.Y & right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32NeonMask operator |(PacketVector2Int32NeonMask left, PacketVector2Int32NeonMask right) => new(left.X | right.X, left.Y | right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32NeonMask operator ^(PacketVector2Int32NeonMask left, PacketVector2Int32NeonMask right) => new(left.X ^ right.X, left.Y ^ right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32NeonMask operator ~(PacketVector2Int32NeonMask value) => new(~value.X, ~value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32NeonMask operator ==(PacketVector2Int32NeonMask left, PacketVector2Int32NeonMask right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32NeonMask operator !=(PacketVector2Int32NeonMask left, PacketVector2Int32NeonMask right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2Int32NeonMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}