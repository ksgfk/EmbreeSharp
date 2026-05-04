using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector3UInt32Avx :
    ISimdIntegerVector3<PacketVector3UInt32Avx, PacketUInt32Avx, uint, PacketVector3UInt32AvxMask, PacketUInt32AvxMask>
{
    public PacketVector3UInt32Avx(PacketUInt32Avx x, PacketUInt32Avx y, PacketUInt32Avx z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketUInt32Avx.LaneCount;

    public PacketUInt32Avx X { get; }

    public PacketUInt32Avx Y { get; }

    public PacketUInt32Avx Z { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Avx Create(PacketUInt32Avx x, PacketUInt32Avx y, PacketUInt32Avx z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Avx Broadcast(uint value)
    {
        PacketUInt32Avx packet = PacketUInt32Avx.Broadcast(value);
        return new(packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Avx Select(PacketVector3UInt32AvxMask mask, PacketVector3UInt32Avx ifTrue, PacketVector3UInt32Avx ifFalse)
    {
        return new(
            PacketUInt32Avx.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt32Avx.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketUInt32Avx.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Avx Select(PacketUInt32AvxMask mask, PacketVector3UInt32Avx ifTrue, PacketVector3UInt32Avx ifFalse)
    {
        return new(
            PacketUInt32Avx.Select(mask, ifTrue.X, ifFalse.X),
            PacketUInt32Avx.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketUInt32Avx.Select(mask, ifTrue.Z, ifFalse.Z));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Avx operator +(PacketVector3UInt32Avx value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Avx operator +(PacketVector3UInt32Avx left, PacketVector3UInt32Avx right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Avx operator -(PacketVector3UInt32Avx left, PacketVector3UInt32Avx right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Avx operator *(PacketVector3UInt32Avx left, PacketVector3UInt32Avx right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Avx operator -(PacketVector3UInt32Avx value) => new(-value.X, -value.Y, -value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32AvxMask operator ==(PacketVector3UInt32Avx left, PacketVector3UInt32Avx right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32AvxMask operator !=(PacketVector3UInt32Avx left, PacketVector3UInt32Avx right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3UInt32Avx other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}

public readonly struct PacketVector3UInt32AvxMask :
    ISimdVector3Mask<PacketVector3UInt32AvxMask, PacketUInt32AvxMask>
{
    public PacketVector3UInt32AvxMask(PacketUInt32AvxMask x, PacketUInt32AvxMask y, PacketUInt32AvxMask z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketUInt32AvxMask.LaneCount;

    public PacketUInt32AvxMask X { get; }

    public PacketUInt32AvxMask Y { get; }

    public PacketUInt32AvxMask Z { get; }

    public static PacketVector3UInt32AvxMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt32AvxMask.True, PacketUInt32AvxMask.True, PacketUInt32AvxMask.True);
    }

    public static PacketVector3UInt32AvxMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt32AvxMask.False, PacketUInt32AvxMask.False, PacketUInt32AvxMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32AvxMask Create(PacketUInt32AvxMask x, PacketUInt32AvxMask y, PacketUInt32AvxMask z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32AvxMask Broadcast(PacketUInt32AvxMask value) => new(value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask All(PacketVector3UInt32AvxMask value) => value.X & value.Y & value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask Any(PacketVector3UInt32AvxMask value) => value.X | value.Y | value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask None(PacketVector3UInt32AvxMask value) => ~(value.X | value.Y | value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32AvxMask AndNot(PacketVector3UInt32AvxMask left, PacketVector3UInt32AvxMask right)
    {
        return new(
            PacketUInt32AvxMask.AndNot(left.X, right.X),
            PacketUInt32AvxMask.AndNot(left.Y, right.Y),
            PacketUInt32AvxMask.AndNot(left.Z, right.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32AvxMask Select(PacketVector3UInt32AvxMask mask, PacketVector3UInt32AvxMask ifTrue, PacketVector3UInt32AvxMask ifFalse)
    {
        return new(
            PacketUInt32AvxMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt32AvxMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketUInt32AvxMask.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32AvxMask And(PacketVector3UInt32AvxMask left, PacketVector3UInt32AvxMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32AvxMask Or(PacketVector3UInt32AvxMask left, PacketVector3UInt32AvxMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32AvxMask Xor(PacketVector3UInt32AvxMask left, PacketVector3UInt32AvxMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32AvxMask Not(PacketVector3UInt32AvxMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32AvxMask operator &(PacketVector3UInt32AvxMask left, PacketVector3UInt32AvxMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32AvxMask operator |(PacketVector3UInt32AvxMask left, PacketVector3UInt32AvxMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32AvxMask operator ^(PacketVector3UInt32AvxMask left, PacketVector3UInt32AvxMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32AvxMask operator ~(PacketVector3UInt32AvxMask value) => new(~value.X, ~value.Y, ~value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32AvxMask operator ==(PacketVector3UInt32AvxMask left, PacketVector3UInt32AvxMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32AvxMask operator !=(PacketVector3UInt32AvxMask left, PacketVector3UInt32AvxMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3UInt32AvxMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}