using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector2Float64Neon :
    ISimdFloatingPointVector2<PacketVector2Float64Neon, PacketFloat64Neon, double, PacketVector2Float64NeonMask, PacketFloat64NeonMask>
{
    public PacketVector2Float64Neon(PacketFloat64Neon x, PacketFloat64Neon y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketFloat64Neon.LaneCount;

    public PacketFloat64Neon X { get; }

    public PacketFloat64Neon Y { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Neon Create(PacketFloat64Neon x, PacketFloat64Neon y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Neon Broadcast(double value)
    {
        PacketFloat64Neon packet = PacketFloat64Neon.Broadcast(value);
        return new(packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Neon Select(PacketVector2Float64NeonMask mask, PacketVector2Float64Neon ifTrue, PacketVector2Float64Neon ifFalse)
    {
        return new(
            PacketFloat64Neon.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64Neon.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Neon Select(PacketFloat64NeonMask mask, PacketVector2Float64Neon ifTrue, PacketVector2Float64Neon ifFalse)
    {
        return new(
            PacketFloat64Neon.Select(mask, ifTrue.X, ifFalse.X),
            PacketFloat64Neon.Select(mask, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Neon Dot(PacketVector2Float64Neon left, PacketVector2Float64Neon right)
    {
        return PacketFloat64Neon.FusedMultiplyAdd(left.Y, right.Y, left.X * right.X);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Neon operator +(PacketVector2Float64Neon value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Neon operator +(PacketVector2Float64Neon left, PacketVector2Float64Neon right) => new(left.X + right.X, left.Y + right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Neon operator -(PacketVector2Float64Neon left, PacketVector2Float64Neon right) => new(left.X - right.X, left.Y - right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Neon operator *(PacketVector2Float64Neon left, PacketVector2Float64Neon right) => new(left.X * right.X, left.Y * right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Neon operator -(PacketVector2Float64Neon value) => new(-value.X, -value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64NeonMask operator ==(PacketVector2Float64Neon left, PacketVector2Float64Neon right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64NeonMask operator !=(PacketVector2Float64Neon left, PacketVector2Float64Neon right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2Float64Neon other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}

public readonly struct PacketVector2Float64NeonMask :
    ISimdVector2Mask<PacketVector2Float64NeonMask, PacketFloat64NeonMask>
{
    public PacketVector2Float64NeonMask(PacketFloat64NeonMask x, PacketFloat64NeonMask y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketFloat64NeonMask.LaneCount;

    public PacketFloat64NeonMask X { get; }

    public PacketFloat64NeonMask Y { get; }

    public static PacketVector2Float64NeonMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat64NeonMask.True, PacketFloat64NeonMask.True);
    }

    public static PacketVector2Float64NeonMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat64NeonMask.False, PacketFloat64NeonMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64NeonMask Create(PacketFloat64NeonMask x, PacketFloat64NeonMask y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64NeonMask Broadcast(PacketFloat64NeonMask value) => new(value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask All(PacketVector2Float64NeonMask value) => value.X & value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask Any(PacketVector2Float64NeonMask value) => value.X | value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask None(PacketVector2Float64NeonMask value) => ~(value.X | value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64NeonMask AndNot(PacketVector2Float64NeonMask left, PacketVector2Float64NeonMask right)
    {
        return new(
            PacketFloat64NeonMask.AndNot(left.X, right.X),
            PacketFloat64NeonMask.AndNot(left.Y, right.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64NeonMask Select(PacketVector2Float64NeonMask mask, PacketVector2Float64NeonMask ifTrue, PacketVector2Float64NeonMask ifFalse)
    {
        return new(
            PacketFloat64NeonMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64NeonMask.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64NeonMask And(PacketVector2Float64NeonMask left, PacketVector2Float64NeonMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64NeonMask Or(PacketVector2Float64NeonMask left, PacketVector2Float64NeonMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64NeonMask Xor(PacketVector2Float64NeonMask left, PacketVector2Float64NeonMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64NeonMask Not(PacketVector2Float64NeonMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64NeonMask operator &(PacketVector2Float64NeonMask left, PacketVector2Float64NeonMask right) => new(left.X & right.X, left.Y & right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64NeonMask operator |(PacketVector2Float64NeonMask left, PacketVector2Float64NeonMask right) => new(left.X | right.X, left.Y | right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64NeonMask operator ^(PacketVector2Float64NeonMask left, PacketVector2Float64NeonMask right) => new(left.X ^ right.X, left.Y ^ right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64NeonMask operator ~(PacketVector2Float64NeonMask value) => new(~value.X, ~value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64NeonMask operator ==(PacketVector2Float64NeonMask left, PacketVector2Float64NeonMask right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64NeonMask operator !=(PacketVector2Float64NeonMask left, PacketVector2Float64NeonMask right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2Float64NeonMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}