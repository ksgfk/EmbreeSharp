using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector2UInt32Avx :
    ISimdIntegerVector2<PacketVector2UInt32Avx, PacketUInt32Avx, uint, PacketVector2UInt32AvxMask, PacketUInt32AvxMask>
{
    public PacketVector2UInt32Avx(PacketUInt32Avx x, PacketUInt32Avx y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketUInt32Avx.LaneCount;

    public PacketUInt32Avx X { get; }

    public PacketUInt32Avx Y { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Avx Create(PacketUInt32Avx x, PacketUInt32Avx y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Avx Broadcast(uint value)
    {
        PacketUInt32Avx packet = PacketUInt32Avx.Broadcast(value);
        return new(packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Avx Select(PacketVector2UInt32AvxMask mask, PacketVector2UInt32Avx ifTrue, PacketVector2UInt32Avx ifFalse)
    {
        return new(
            PacketUInt32Avx.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt32Avx.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Avx Select(PacketUInt32AvxMask mask, PacketVector2UInt32Avx ifTrue, PacketVector2UInt32Avx ifFalse)
    {
        return new(
            PacketUInt32Avx.Select(mask, ifTrue.X, ifFalse.X),
            PacketUInt32Avx.Select(mask, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Avx operator +(PacketVector2UInt32Avx value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Avx operator +(PacketVector2UInt32Avx left, PacketVector2UInt32Avx right) => new(left.X + right.X, left.Y + right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Avx operator -(PacketVector2UInt32Avx left, PacketVector2UInt32Avx right) => new(left.X - right.X, left.Y - right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Avx operator *(PacketVector2UInt32Avx left, PacketVector2UInt32Avx right) => new(left.X * right.X, left.Y * right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Avx operator -(PacketVector2UInt32Avx value) => new(-value.X, -value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32AvxMask operator ==(PacketVector2UInt32Avx left, PacketVector2UInt32Avx right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32AvxMask operator !=(PacketVector2UInt32Avx left, PacketVector2UInt32Avx right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2UInt32Avx other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}

public readonly struct PacketVector2UInt32AvxMask :
    ISimdVector2Mask<PacketVector2UInt32AvxMask, PacketUInt32AvxMask>
{
    public PacketVector2UInt32AvxMask(PacketUInt32AvxMask x, PacketUInt32AvxMask y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketUInt32AvxMask.LaneCount;

    public PacketUInt32AvxMask X { get; }

    public PacketUInt32AvxMask Y { get; }

    public static PacketVector2UInt32AvxMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt32AvxMask.True, PacketUInt32AvxMask.True);
    }

    public static PacketVector2UInt32AvxMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt32AvxMask.False, PacketUInt32AvxMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32AvxMask Create(PacketUInt32AvxMask x, PacketUInt32AvxMask y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32AvxMask Broadcast(PacketUInt32AvxMask value) => new(value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask All(PacketVector2UInt32AvxMask value) => value.X & value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask Any(PacketVector2UInt32AvxMask value) => value.X | value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask None(PacketVector2UInt32AvxMask value) => ~(value.X | value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32AvxMask AndNot(PacketVector2UInt32AvxMask left, PacketVector2UInt32AvxMask right)
    {
        return new(
            PacketUInt32AvxMask.AndNot(left.X, right.X),
            PacketUInt32AvxMask.AndNot(left.Y, right.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32AvxMask Select(PacketVector2UInt32AvxMask mask, PacketVector2UInt32AvxMask ifTrue, PacketVector2UInt32AvxMask ifFalse)
    {
        return new(
            PacketUInt32AvxMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt32AvxMask.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32AvxMask And(PacketVector2UInt32AvxMask left, PacketVector2UInt32AvxMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32AvxMask Or(PacketVector2UInt32AvxMask left, PacketVector2UInt32AvxMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32AvxMask Xor(PacketVector2UInt32AvxMask left, PacketVector2UInt32AvxMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32AvxMask Not(PacketVector2UInt32AvxMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32AvxMask operator &(PacketVector2UInt32AvxMask left, PacketVector2UInt32AvxMask right) => new(left.X & right.X, left.Y & right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32AvxMask operator |(PacketVector2UInt32AvxMask left, PacketVector2UInt32AvxMask right) => new(left.X | right.X, left.Y | right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32AvxMask operator ^(PacketVector2UInt32AvxMask left, PacketVector2UInt32AvxMask right) => new(left.X ^ right.X, left.Y ^ right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32AvxMask operator ~(PacketVector2UInt32AvxMask value) => new(~value.X, ~value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32AvxMask operator ==(PacketVector2UInt32AvxMask left, PacketVector2UInt32AvxMask right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32AvxMask operator !=(PacketVector2UInt32AvxMask left, PacketVector2UInt32AvxMask right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2UInt32AvxMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}