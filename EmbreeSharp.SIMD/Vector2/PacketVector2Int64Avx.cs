using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector2Int64Avx :
    ISimdIntegerVector2<PacketVector2Int64Avx, PacketInt64Avx, long, PacketVector2Int64AvxMask, PacketInt64AvxMask>
{
    public PacketVector2Int64Avx(PacketInt64Avx x, PacketInt64Avx y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketInt64Avx.LaneCount;

    public PacketInt64Avx X { get; }

    public PacketInt64Avx Y { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Avx Create(PacketInt64Avx x, PacketInt64Avx y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Avx Broadcast(long value)
    {
        PacketInt64Avx packet = PacketInt64Avx.Broadcast(value);
        return new(packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Avx Select(PacketVector2Int64AvxMask mask, PacketVector2Int64Avx ifTrue, PacketVector2Int64Avx ifFalse)
    {
        return new(
            PacketInt64Avx.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt64Avx.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Avx Select(PacketInt64AvxMask mask, PacketVector2Int64Avx ifTrue, PacketVector2Int64Avx ifFalse)
    {
        return new(
            PacketInt64Avx.Select(mask, ifTrue.X, ifFalse.X),
            PacketInt64Avx.Select(mask, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Avx operator +(PacketVector2Int64Avx value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Avx operator +(PacketVector2Int64Avx left, PacketVector2Int64Avx right) => new(left.X + right.X, left.Y + right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Avx operator -(PacketVector2Int64Avx left, PacketVector2Int64Avx right) => new(left.X - right.X, left.Y - right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Avx operator *(PacketVector2Int64Avx left, PacketVector2Int64Avx right) => new(left.X * right.X, left.Y * right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Avx operator -(PacketVector2Int64Avx value) => new(-value.X, -value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64AvxMask operator ==(PacketVector2Int64Avx left, PacketVector2Int64Avx right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64AvxMask operator !=(PacketVector2Int64Avx left, PacketVector2Int64Avx right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2Int64Avx other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}

public readonly struct PacketVector2Int64AvxMask :
    ISimdVector2Mask<PacketVector2Int64AvxMask, PacketInt64AvxMask>
{
    public PacketVector2Int64AvxMask(PacketInt64AvxMask x, PacketInt64AvxMask y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketInt64AvxMask.LaneCount;

    public PacketInt64AvxMask X { get; }

    public PacketInt64AvxMask Y { get; }

    public static PacketVector2Int64AvxMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt64AvxMask.True, PacketInt64AvxMask.True);
    }

    public static PacketVector2Int64AvxMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt64AvxMask.False, PacketInt64AvxMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64AvxMask Create(PacketInt64AvxMask x, PacketInt64AvxMask y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64AvxMask Broadcast(PacketInt64AvxMask value) => new(value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask All(PacketVector2Int64AvxMask value) => value.X & value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask Any(PacketVector2Int64AvxMask value) => value.X | value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask None(PacketVector2Int64AvxMask value) => ~(value.X | value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64AvxMask AndNot(PacketVector2Int64AvxMask left, PacketVector2Int64AvxMask right)
    {
        return new(
            PacketInt64AvxMask.AndNot(left.X, right.X),
            PacketInt64AvxMask.AndNot(left.Y, right.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64AvxMask Select(PacketVector2Int64AvxMask mask, PacketVector2Int64AvxMask ifTrue, PacketVector2Int64AvxMask ifFalse)
    {
        return new(
            PacketInt64AvxMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt64AvxMask.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64AvxMask And(PacketVector2Int64AvxMask left, PacketVector2Int64AvxMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64AvxMask Or(PacketVector2Int64AvxMask left, PacketVector2Int64AvxMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64AvxMask Xor(PacketVector2Int64AvxMask left, PacketVector2Int64AvxMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64AvxMask Not(PacketVector2Int64AvxMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64AvxMask operator &(PacketVector2Int64AvxMask left, PacketVector2Int64AvxMask right) => new(left.X & right.X, left.Y & right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64AvxMask operator |(PacketVector2Int64AvxMask left, PacketVector2Int64AvxMask right) => new(left.X | right.X, left.Y | right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64AvxMask operator ^(PacketVector2Int64AvxMask left, PacketVector2Int64AvxMask right) => new(left.X ^ right.X, left.Y ^ right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64AvxMask operator ~(PacketVector2Int64AvxMask value) => new(~value.X, ~value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64AvxMask operator ==(PacketVector2Int64AvxMask left, PacketVector2Int64AvxMask right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64AvxMask operator !=(PacketVector2Int64AvxMask left, PacketVector2Int64AvxMask right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2Int64AvxMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}