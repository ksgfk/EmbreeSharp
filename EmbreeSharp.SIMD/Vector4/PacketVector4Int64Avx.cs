using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector4Int64Avx :
    ISimdIntegerVector4<PacketVector4Int64Avx, PacketInt64Avx, long, PacketVector4Int64AvxMask, PacketInt64AvxMask>
{
    public PacketVector4Int64Avx(PacketInt64Avx x, PacketInt64Avx y, PacketInt64Avx z, PacketInt64Avx w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketInt64Avx.LaneCount;

    public PacketInt64Avx X { get; }

    public PacketInt64Avx Y { get; }

    public PacketInt64Avx Z { get; }

    public PacketInt64Avx W { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Avx Create(PacketInt64Avx x, PacketInt64Avx y, PacketInt64Avx z, PacketInt64Avx w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Avx Broadcast(long value)
    {
        PacketInt64Avx packet = PacketInt64Avx.Broadcast(value);
        return new(packet, packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Avx Select(PacketVector4Int64AvxMask mask, PacketVector4Int64Avx ifTrue, PacketVector4Int64Avx ifFalse)
    {
        return new(
            PacketInt64Avx.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt64Avx.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketInt64Avx.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketInt64Avx.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Avx Select(PacketInt64AvxMask mask, PacketVector4Int64Avx ifTrue, PacketVector4Int64Avx ifFalse)
    {
        return new(
            PacketInt64Avx.Select(mask, ifTrue.X, ifFalse.X),
            PacketInt64Avx.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketInt64Avx.Select(mask, ifTrue.Z, ifFalse.Z),
            PacketInt64Avx.Select(mask, ifTrue.W, ifFalse.W));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Avx operator +(PacketVector4Int64Avx value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Avx operator +(PacketVector4Int64Avx left, PacketVector4Int64Avx right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Avx operator -(PacketVector4Int64Avx left, PacketVector4Int64Avx right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Avx operator *(PacketVector4Int64Avx left, PacketVector4Int64Avx right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Avx operator -(PacketVector4Int64Avx value) => new(-value.X, -value.Y, -value.Z, -value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64AvxMask operator ==(PacketVector4Int64Avx left, PacketVector4Int64Avx right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64AvxMask operator !=(PacketVector4Int64Avx left, PacketVector4Int64Avx right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4Int64Avx other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct PacketVector4Int64AvxMask :
    ISimdVector4Mask<PacketVector4Int64AvxMask, PacketInt64AvxMask>
{
    public PacketVector4Int64AvxMask(PacketInt64AvxMask x, PacketInt64AvxMask y, PacketInt64AvxMask z, PacketInt64AvxMask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketInt64AvxMask.LaneCount;

    public PacketInt64AvxMask X { get; }

    public PacketInt64AvxMask Y { get; }

    public PacketInt64AvxMask Z { get; }

    public PacketInt64AvxMask W { get; }

    public static PacketVector4Int64AvxMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt64AvxMask.True, PacketInt64AvxMask.True, PacketInt64AvxMask.True, PacketInt64AvxMask.True);
    }

    public static PacketVector4Int64AvxMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt64AvxMask.False, PacketInt64AvxMask.False, PacketInt64AvxMask.False, PacketInt64AvxMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64AvxMask Create(PacketInt64AvxMask x, PacketInt64AvxMask y, PacketInt64AvxMask z, PacketInt64AvxMask w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64AvxMask Broadcast(PacketInt64AvxMask value) => new(value, value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask All(PacketVector4Int64AvxMask value) => value.X & value.Y & value.Z & value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask Any(PacketVector4Int64AvxMask value) => value.X | value.Y | value.Z | value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64AvxMask None(PacketVector4Int64AvxMask value) => ~(value.X | value.Y | value.Z | value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64AvxMask AndNot(PacketVector4Int64AvxMask left, PacketVector4Int64AvxMask right)
    {
        return new(
            PacketInt64AvxMask.AndNot(left.X, right.X),
            PacketInt64AvxMask.AndNot(left.Y, right.Y),
            PacketInt64AvxMask.AndNot(left.Z, right.Z),
            PacketInt64AvxMask.AndNot(left.W, right.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64AvxMask Select(PacketVector4Int64AvxMask mask, PacketVector4Int64AvxMask ifTrue, PacketVector4Int64AvxMask ifFalse)
    {
        return new(
            PacketInt64AvxMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt64AvxMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketInt64AvxMask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketInt64AvxMask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64AvxMask And(PacketVector4Int64AvxMask left, PacketVector4Int64AvxMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64AvxMask Or(PacketVector4Int64AvxMask left, PacketVector4Int64AvxMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64AvxMask Xor(PacketVector4Int64AvxMask left, PacketVector4Int64AvxMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64AvxMask Not(PacketVector4Int64AvxMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64AvxMask operator &(PacketVector4Int64AvxMask left, PacketVector4Int64AvxMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64AvxMask operator |(PacketVector4Int64AvxMask left, PacketVector4Int64AvxMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64AvxMask operator ^(PacketVector4Int64AvxMask left, PacketVector4Int64AvxMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64AvxMask operator ~(PacketVector4Int64AvxMask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64AvxMask operator ==(PacketVector4Int64AvxMask left, PacketVector4Int64AvxMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64AvxMask operator !=(PacketVector4Int64AvxMask left, PacketVector4Int64AvxMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4Int64AvxMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}