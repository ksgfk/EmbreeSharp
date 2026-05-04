using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector2Int32Avx :
    ISimdIntegerVector2<PacketVector2Int32Avx, PacketInt32Avx, int, PacketVector2Int32AvxMask, PacketInt32AvxMask>
{
    public PacketVector2Int32Avx(PacketInt32Avx x, PacketInt32Avx y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketInt32Avx.LaneCount;

    public PacketInt32Avx X { get; }

    public PacketInt32Avx Y { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Avx Create(PacketInt32Avx x, PacketInt32Avx y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Avx Broadcast(int value)
    {
        PacketInt32Avx packet = PacketInt32Avx.Broadcast(value);
        return new(packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Avx Select(PacketVector2Int32AvxMask mask, PacketVector2Int32Avx ifTrue, PacketVector2Int32Avx ifFalse)
    {
        return new(
            PacketInt32Avx.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt32Avx.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Avx Select(PacketInt32AvxMask mask, PacketVector2Int32Avx ifTrue, PacketVector2Int32Avx ifFalse)
    {
        return new(
            PacketInt32Avx.Select(mask, ifTrue.X, ifFalse.X),
            PacketInt32Avx.Select(mask, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Avx operator +(PacketVector2Int32Avx value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Avx operator +(PacketVector2Int32Avx left, PacketVector2Int32Avx right) => new(left.X + right.X, left.Y + right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Avx operator -(PacketVector2Int32Avx left, PacketVector2Int32Avx right) => new(left.X - right.X, left.Y - right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Avx operator *(PacketVector2Int32Avx left, PacketVector2Int32Avx right) => new(left.X * right.X, left.Y * right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Avx operator -(PacketVector2Int32Avx value) => new(-value.X, -value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32AvxMask operator ==(PacketVector2Int32Avx left, PacketVector2Int32Avx right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32AvxMask operator !=(PacketVector2Int32Avx left, PacketVector2Int32Avx right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2Int32Avx other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}

public readonly struct PacketVector2Int32AvxMask :
    ISimdVector2Mask<PacketVector2Int32AvxMask, PacketInt32AvxMask>
{
    public PacketVector2Int32AvxMask(PacketInt32AvxMask x, PacketInt32AvxMask y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketInt32AvxMask.LaneCount;

    public PacketInt32AvxMask X { get; }

    public PacketInt32AvxMask Y { get; }

    public static PacketVector2Int32AvxMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt32AvxMask.True, PacketInt32AvxMask.True);
    }

    public static PacketVector2Int32AvxMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt32AvxMask.False, PacketInt32AvxMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32AvxMask Create(PacketInt32AvxMask x, PacketInt32AvxMask y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32AvxMask Broadcast(PacketInt32AvxMask value) => new(value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask All(PacketVector2Int32AvxMask value) => value.X & value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask Any(PacketVector2Int32AvxMask value) => value.X | value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask None(PacketVector2Int32AvxMask value) => ~(value.X | value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32AvxMask AndNot(PacketVector2Int32AvxMask left, PacketVector2Int32AvxMask right)
    {
        return new(
            PacketInt32AvxMask.AndNot(left.X, right.X),
            PacketInt32AvxMask.AndNot(left.Y, right.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32AvxMask Select(PacketVector2Int32AvxMask mask, PacketVector2Int32AvxMask ifTrue, PacketVector2Int32AvxMask ifFalse)
    {
        return new(
            PacketInt32AvxMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt32AvxMask.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32AvxMask And(PacketVector2Int32AvxMask left, PacketVector2Int32AvxMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32AvxMask Or(PacketVector2Int32AvxMask left, PacketVector2Int32AvxMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32AvxMask Xor(PacketVector2Int32AvxMask left, PacketVector2Int32AvxMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32AvxMask Not(PacketVector2Int32AvxMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32AvxMask operator &(PacketVector2Int32AvxMask left, PacketVector2Int32AvxMask right) => new(left.X & right.X, left.Y & right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32AvxMask operator |(PacketVector2Int32AvxMask left, PacketVector2Int32AvxMask right) => new(left.X | right.X, left.Y | right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32AvxMask operator ^(PacketVector2Int32AvxMask left, PacketVector2Int32AvxMask right) => new(left.X ^ right.X, left.Y ^ right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32AvxMask operator ~(PacketVector2Int32AvxMask value) => new(~value.X, ~value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32AvxMask operator ==(PacketVector2Int32AvxMask left, PacketVector2Int32AvxMask right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32AvxMask operator !=(PacketVector2Int32AvxMask left, PacketVector2Int32AvxMask right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2Int32AvxMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}