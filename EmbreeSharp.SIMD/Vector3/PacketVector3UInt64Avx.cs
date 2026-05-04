using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector3UInt64Avx :
    ISimdIntegerVector3<PacketVector3UInt64Avx, PacketUInt64Avx, ulong, PacketVector3UInt64AvxMask, PacketUInt64AvxMask>
{
    public PacketVector3UInt64Avx(PacketUInt64Avx x, PacketUInt64Avx y, PacketUInt64Avx z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketUInt64Avx.LaneCount;

    public PacketUInt64Avx X { get; }

    public PacketUInt64Avx Y { get; }

    public PacketUInt64Avx Z { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Avx Create(PacketUInt64Avx x, PacketUInt64Avx y, PacketUInt64Avx z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Avx Broadcast(ulong value)
    {
        PacketUInt64Avx packet = PacketUInt64Avx.Broadcast(value);
        return new(packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Avx Select(PacketVector3UInt64AvxMask mask, PacketVector3UInt64Avx ifTrue, PacketVector3UInt64Avx ifFalse)
    {
        return new(
            PacketUInt64Avx.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt64Avx.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketUInt64Avx.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Avx Select(PacketUInt64AvxMask mask, PacketVector3UInt64Avx ifTrue, PacketVector3UInt64Avx ifFalse)
    {
        return new(
            PacketUInt64Avx.Select(mask, ifTrue.X, ifFalse.X),
            PacketUInt64Avx.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketUInt64Avx.Select(mask, ifTrue.Z, ifFalse.Z));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Avx operator +(PacketVector3UInt64Avx value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Avx operator +(PacketVector3UInt64Avx left, PacketVector3UInt64Avx right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Avx operator -(PacketVector3UInt64Avx left, PacketVector3UInt64Avx right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Avx operator *(PacketVector3UInt64Avx left, PacketVector3UInt64Avx right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Avx operator -(PacketVector3UInt64Avx value) => new(-value.X, -value.Y, -value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64AvxMask operator ==(PacketVector3UInt64Avx left, PacketVector3UInt64Avx right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64AvxMask operator !=(PacketVector3UInt64Avx left, PacketVector3UInt64Avx right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3UInt64Avx other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}

public readonly struct PacketVector3UInt64AvxMask :
    ISimdVector3Mask<PacketVector3UInt64AvxMask, PacketUInt64AvxMask>
{
    public PacketVector3UInt64AvxMask(PacketUInt64AvxMask x, PacketUInt64AvxMask y, PacketUInt64AvxMask z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketUInt64AvxMask.LaneCount;

    public PacketUInt64AvxMask X { get; }

    public PacketUInt64AvxMask Y { get; }

    public PacketUInt64AvxMask Z { get; }

    public static PacketVector3UInt64AvxMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt64AvxMask.True, PacketUInt64AvxMask.True, PacketUInt64AvxMask.True);
    }

    public static PacketVector3UInt64AvxMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt64AvxMask.False, PacketUInt64AvxMask.False, PacketUInt64AvxMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64AvxMask Create(PacketUInt64AvxMask x, PacketUInt64AvxMask y, PacketUInt64AvxMask z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64AvxMask Broadcast(PacketUInt64AvxMask value) => new(value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask All(PacketVector3UInt64AvxMask value) => value.X & value.Y & value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask Any(PacketVector3UInt64AvxMask value) => value.X | value.Y | value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64AvxMask None(PacketVector3UInt64AvxMask value) => ~(value.X | value.Y | value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64AvxMask AndNot(PacketVector3UInt64AvxMask left, PacketVector3UInt64AvxMask right)
    {
        return new(
            PacketUInt64AvxMask.AndNot(left.X, right.X),
            PacketUInt64AvxMask.AndNot(left.Y, right.Y),
            PacketUInt64AvxMask.AndNot(left.Z, right.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64AvxMask Select(PacketVector3UInt64AvxMask mask, PacketVector3UInt64AvxMask ifTrue, PacketVector3UInt64AvxMask ifFalse)
    {
        return new(
            PacketUInt64AvxMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt64AvxMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketUInt64AvxMask.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64AvxMask And(PacketVector3UInt64AvxMask left, PacketVector3UInt64AvxMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64AvxMask Or(PacketVector3UInt64AvxMask left, PacketVector3UInt64AvxMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64AvxMask Xor(PacketVector3UInt64AvxMask left, PacketVector3UInt64AvxMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64AvxMask Not(PacketVector3UInt64AvxMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64AvxMask operator &(PacketVector3UInt64AvxMask left, PacketVector3UInt64AvxMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64AvxMask operator |(PacketVector3UInt64AvxMask left, PacketVector3UInt64AvxMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64AvxMask operator ^(PacketVector3UInt64AvxMask left, PacketVector3UInt64AvxMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64AvxMask operator ~(PacketVector3UInt64AvxMask value) => new(~value.X, ~value.Y, ~value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64AvxMask operator ==(PacketVector3UInt64AvxMask left, PacketVector3UInt64AvxMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64AvxMask operator !=(PacketVector3UInt64AvxMask left, PacketVector3UInt64AvxMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3UInt64AvxMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}