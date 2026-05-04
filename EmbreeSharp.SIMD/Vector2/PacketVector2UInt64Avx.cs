using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector2UInt64Avx :
    ISimdIntegerVector2<PacketVector2UInt64Avx, PacketUInt64Avx, ulong, PacketVector2UInt64AvxMask, PacketUInt64AvxMask>
{
    public PacketVector2UInt64Avx(PacketUInt64Avx x, PacketUInt64Avx y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketUInt64Avx.LaneCount;

    public PacketUInt64Avx X { get; }

    public PacketUInt64Avx Y { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Avx Create(PacketUInt64Avx x, PacketUInt64Avx y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Avx Broadcast(ulong value)
    {
        PacketUInt64Avx packet = PacketUInt64Avx.Broadcast(value);
        return new(packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Avx Select(PacketVector2UInt64AvxMask mask, PacketVector2UInt64Avx ifTrue, PacketVector2UInt64Avx ifFalse)
    {
        return new(
            PacketUInt64Avx.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt64Avx.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Avx Select(PacketUInt64AvxMask mask, PacketVector2UInt64Avx ifTrue, PacketVector2UInt64Avx ifFalse)
    {
        return new(
            PacketUInt64Avx.Select(mask, ifTrue.X, ifFalse.X),
            PacketUInt64Avx.Select(mask, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Avx operator +(PacketVector2UInt64Avx value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Avx operator +(PacketVector2UInt64Avx left, PacketVector2UInt64Avx right) => new(left.X + right.X, left.Y + right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Avx operator -(PacketVector2UInt64Avx left, PacketVector2UInt64Avx right) => new(left.X - right.X, left.Y - right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Avx operator *(PacketVector2UInt64Avx left, PacketVector2UInt64Avx right) => new(left.X * right.X, left.Y * right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Avx operator -(PacketVector2UInt64Avx value) => new(-value.X, -value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64AvxMask operator ==(PacketVector2UInt64Avx left, PacketVector2UInt64Avx right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64AvxMask operator !=(PacketVector2UInt64Avx left, PacketVector2UInt64Avx right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2UInt64Avx other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}

public readonly struct PacketVector2UInt64AvxMask :
    ISimdVector2Mask<PacketVector2UInt64AvxMask, PacketUInt64AvxMask>
{
    public PacketVector2UInt64AvxMask(PacketUInt64AvxMask x, PacketUInt64AvxMask y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketUInt64AvxMask.LaneCount;

    public PacketUInt64AvxMask X { get; }

    public PacketUInt64AvxMask Y { get; }

    public static PacketVector2UInt64AvxMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt64AvxMask.True, PacketUInt64AvxMask.True);
    }

    public static PacketVector2UInt64AvxMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt64AvxMask.False, PacketUInt64AvxMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64AvxMask Create(PacketUInt64AvxMask x, PacketUInt64AvxMask y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64AvxMask Broadcast(PacketUInt64AvxMask value) => new(value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask All(PacketVector2UInt64AvxMask value) => value.X & value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask Any(PacketVector2UInt64AvxMask value) => value.X | value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask None(PacketVector2UInt64AvxMask value) => ~(value.X | value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64AvxMask AndNot(PacketVector2UInt64AvxMask left, PacketVector2UInt64AvxMask right)
    {
        return new(
            PacketUInt64AvxMask.AndNot(left.X, right.X),
            PacketUInt64AvxMask.AndNot(left.Y, right.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64AvxMask Select(PacketVector2UInt64AvxMask mask, PacketVector2UInt64AvxMask ifTrue, PacketVector2UInt64AvxMask ifFalse)
    {
        return new(
            PacketUInt64AvxMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt64AvxMask.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64AvxMask And(PacketVector2UInt64AvxMask left, PacketVector2UInt64AvxMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64AvxMask Or(PacketVector2UInt64AvxMask left, PacketVector2UInt64AvxMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64AvxMask Xor(PacketVector2UInt64AvxMask left, PacketVector2UInt64AvxMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64AvxMask Not(PacketVector2UInt64AvxMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64AvxMask operator &(PacketVector2UInt64AvxMask left, PacketVector2UInt64AvxMask right) => new(left.X & right.X, left.Y & right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64AvxMask operator |(PacketVector2UInt64AvxMask left, PacketVector2UInt64AvxMask right) => new(left.X | right.X, left.Y | right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64AvxMask operator ^(PacketVector2UInt64AvxMask left, PacketVector2UInt64AvxMask right) => new(left.X ^ right.X, left.Y ^ right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64AvxMask operator ~(PacketVector2UInt64AvxMask value) => new(~value.X, ~value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64AvxMask operator ==(PacketVector2UInt64AvxMask left, PacketVector2UInt64AvxMask right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64AvxMask operator !=(PacketVector2UInt64AvxMask left, PacketVector2UInt64AvxMask right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2UInt64AvxMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}