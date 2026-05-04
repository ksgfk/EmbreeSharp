using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector4UInt64Avx :
    ISimdIntegerVector4<PacketVector4UInt64Avx, PacketUInt64Avx, ulong, PacketVector4UInt64AvxMask, PacketUInt64AvxMask>
{
    public PacketVector4UInt64Avx(PacketUInt64Avx x, PacketUInt64Avx y, PacketUInt64Avx z, PacketUInt64Avx w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketUInt64Avx.LaneCount;

    public PacketUInt64Avx X { get; }

    public PacketUInt64Avx Y { get; }

    public PacketUInt64Avx Z { get; }

    public PacketUInt64Avx W { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Avx Create(PacketUInt64Avx x, PacketUInt64Avx y, PacketUInt64Avx z, PacketUInt64Avx w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Avx Broadcast(ulong value)
    {
        PacketUInt64Avx packet = PacketUInt64Avx.Broadcast(value);
        return new(packet, packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Avx Select(PacketVector4UInt64AvxMask mask, PacketVector4UInt64Avx ifTrue, PacketVector4UInt64Avx ifFalse)
    {
        return new(
            PacketUInt64Avx.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt64Avx.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketUInt64Avx.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketUInt64Avx.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Avx Select(PacketUInt64AvxMask mask, PacketVector4UInt64Avx ifTrue, PacketVector4UInt64Avx ifFalse)
    {
        return new(
            PacketUInt64Avx.Select(mask, ifTrue.X, ifFalse.X),
            PacketUInt64Avx.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketUInt64Avx.Select(mask, ifTrue.Z, ifFalse.Z),
            PacketUInt64Avx.Select(mask, ifTrue.W, ifFalse.W));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Avx operator +(PacketVector4UInt64Avx value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Avx operator +(PacketVector4UInt64Avx left, PacketVector4UInt64Avx right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Avx operator -(PacketVector4UInt64Avx left, PacketVector4UInt64Avx right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Avx operator *(PacketVector4UInt64Avx left, PacketVector4UInt64Avx right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Avx operator -(PacketVector4UInt64Avx value) => new(-value.X, -value.Y, -value.Z, -value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64AvxMask operator ==(PacketVector4UInt64Avx left, PacketVector4UInt64Avx right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64AvxMask operator !=(PacketVector4UInt64Avx left, PacketVector4UInt64Avx right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4UInt64Avx other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct PacketVector4UInt64AvxMask :
    ISimdVector4Mask<PacketVector4UInt64AvxMask, PacketUInt64AvxMask>
{
    public PacketVector4UInt64AvxMask(PacketUInt64AvxMask x, PacketUInt64AvxMask y, PacketUInt64AvxMask z, PacketUInt64AvxMask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketUInt64AvxMask.LaneCount;

    public PacketUInt64AvxMask X { get; }

    public PacketUInt64AvxMask Y { get; }

    public PacketUInt64AvxMask Z { get; }

    public PacketUInt64AvxMask W { get; }

    public static PacketVector4UInt64AvxMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt64AvxMask.True, PacketUInt64AvxMask.True, PacketUInt64AvxMask.True, PacketUInt64AvxMask.True);
    }

    public static PacketVector4UInt64AvxMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt64AvxMask.False, PacketUInt64AvxMask.False, PacketUInt64AvxMask.False, PacketUInt64AvxMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64AvxMask Create(PacketUInt64AvxMask x, PacketUInt64AvxMask y, PacketUInt64AvxMask z, PacketUInt64AvxMask w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64AvxMask Broadcast(PacketUInt64AvxMask value) => new(value, value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask All(PacketVector4UInt64AvxMask value) => value.X & value.Y & value.Z & value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask Any(PacketVector4UInt64AvxMask value) => value.X | value.Y | value.Z | value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask None(PacketVector4UInt64AvxMask value) => ~(value.X | value.Y | value.Z | value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64AvxMask AndNot(PacketVector4UInt64AvxMask left, PacketVector4UInt64AvxMask right)
    {
        return new(
            PacketUInt64AvxMask.AndNot(left.X, right.X),
            PacketUInt64AvxMask.AndNot(left.Y, right.Y),
            PacketUInt64AvxMask.AndNot(left.Z, right.Z),
            PacketUInt64AvxMask.AndNot(left.W, right.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64AvxMask Select(PacketVector4UInt64AvxMask mask, PacketVector4UInt64AvxMask ifTrue, PacketVector4UInt64AvxMask ifFalse)
    {
        return new(
            PacketUInt64AvxMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt64AvxMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketUInt64AvxMask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketUInt64AvxMask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64AvxMask And(PacketVector4UInt64AvxMask left, PacketVector4UInt64AvxMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64AvxMask Or(PacketVector4UInt64AvxMask left, PacketVector4UInt64AvxMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64AvxMask Xor(PacketVector4UInt64AvxMask left, PacketVector4UInt64AvxMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64AvxMask Not(PacketVector4UInt64AvxMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64AvxMask operator &(PacketVector4UInt64AvxMask left, PacketVector4UInt64AvxMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64AvxMask operator |(PacketVector4UInt64AvxMask left, PacketVector4UInt64AvxMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64AvxMask operator ^(PacketVector4UInt64AvxMask left, PacketVector4UInt64AvxMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64AvxMask operator ~(PacketVector4UInt64AvxMask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64AvxMask operator ==(PacketVector4UInt64AvxMask left, PacketVector4UInt64AvxMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64AvxMask operator !=(PacketVector4UInt64AvxMask left, PacketVector4UInt64AvxMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4UInt64AvxMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}