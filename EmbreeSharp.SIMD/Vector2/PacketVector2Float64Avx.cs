using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector2Float64Avx :
    ISimdFloatingPointVector2<PacketVector2Float64Avx, PacketFloat64Avx, double, PacketVector2Float64AvxMask, PacketFloat64AvxMask>
{
    public PacketVector2Float64Avx(PacketFloat64Avx x, PacketFloat64Avx y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketFloat64Avx.LaneCount;

    public PacketFloat64Avx X { get; }

    public PacketFloat64Avx Y { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Avx Create(PacketFloat64Avx x, PacketFloat64Avx y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Avx Broadcast(double value)
    {
        PacketFloat64Avx packet = PacketFloat64Avx.Broadcast(value);
        return new(packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Avx Select(PacketVector2Float64AvxMask mask, PacketVector2Float64Avx ifTrue, PacketVector2Float64Avx ifFalse)
    {
        return new(
            PacketFloat64Avx.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64Avx.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Avx Select(PacketFloat64AvxMask mask, PacketVector2Float64Avx ifTrue, PacketVector2Float64Avx ifFalse)
    {
        return new(
            PacketFloat64Avx.Select(mask, ifTrue.X, ifFalse.X),
            PacketFloat64Avx.Select(mask, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Avx Dot(PacketVector2Float64Avx left, PacketVector2Float64Avx right)
    {
        return PacketFloat64Avx.FusedMultiplyAdd(left.Y, right.Y, left.X * right.X);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Avx operator +(PacketVector2Float64Avx value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Avx operator +(PacketVector2Float64Avx left, PacketVector2Float64Avx right) => new(left.X + right.X, left.Y + right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Avx operator -(PacketVector2Float64Avx left, PacketVector2Float64Avx right) => new(left.X - right.X, left.Y - right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Avx operator *(PacketVector2Float64Avx left, PacketVector2Float64Avx right) => new(left.X * right.X, left.Y * right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Avx operator -(PacketVector2Float64Avx value) => new(-value.X, -value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64AvxMask operator ==(PacketVector2Float64Avx left, PacketVector2Float64Avx right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64AvxMask operator !=(PacketVector2Float64Avx left, PacketVector2Float64Avx right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2Float64Avx other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}

public readonly struct PacketVector2Float64AvxMask :
    ISimdVector2Mask<PacketVector2Float64AvxMask, PacketFloat64AvxMask>
{
    public PacketVector2Float64AvxMask(PacketFloat64AvxMask x, PacketFloat64AvxMask y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketFloat64AvxMask.LaneCount;

    public PacketFloat64AvxMask X { get; }

    public PacketFloat64AvxMask Y { get; }

    public static PacketVector2Float64AvxMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat64AvxMask.True, PacketFloat64AvxMask.True);
    }

    public static PacketVector2Float64AvxMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat64AvxMask.False, PacketFloat64AvxMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64AvxMask Create(PacketFloat64AvxMask x, PacketFloat64AvxMask y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64AvxMask Broadcast(PacketFloat64AvxMask value) => new(value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask All(PacketVector2Float64AvxMask value) => value.X & value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask Any(PacketVector2Float64AvxMask value) => value.X | value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask None(PacketVector2Float64AvxMask value) => ~(value.X | value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64AvxMask AndNot(PacketVector2Float64AvxMask left, PacketVector2Float64AvxMask right)
    {
        return new(
            PacketFloat64AvxMask.AndNot(left.X, right.X),
            PacketFloat64AvxMask.AndNot(left.Y, right.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64AvxMask Select(PacketVector2Float64AvxMask mask, PacketVector2Float64AvxMask ifTrue, PacketVector2Float64AvxMask ifFalse)
    {
        return new(
            PacketFloat64AvxMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64AvxMask.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64AvxMask And(PacketVector2Float64AvxMask left, PacketVector2Float64AvxMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64AvxMask Or(PacketVector2Float64AvxMask left, PacketVector2Float64AvxMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64AvxMask Xor(PacketVector2Float64AvxMask left, PacketVector2Float64AvxMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64AvxMask Not(PacketVector2Float64AvxMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64AvxMask operator &(PacketVector2Float64AvxMask left, PacketVector2Float64AvxMask right) => new(left.X & right.X, left.Y & right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64AvxMask operator |(PacketVector2Float64AvxMask left, PacketVector2Float64AvxMask right) => new(left.X | right.X, left.Y | right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64AvxMask operator ^(PacketVector2Float64AvxMask left, PacketVector2Float64AvxMask right) => new(left.X ^ right.X, left.Y ^ right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64AvxMask operator ~(PacketVector2Float64AvxMask value) => new(~value.X, ~value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64AvxMask operator ==(PacketVector2Float64AvxMask left, PacketVector2Float64AvxMask right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64AvxMask operator !=(PacketVector2Float64AvxMask left, PacketVector2Float64AvxMask right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2Float64AvxMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}