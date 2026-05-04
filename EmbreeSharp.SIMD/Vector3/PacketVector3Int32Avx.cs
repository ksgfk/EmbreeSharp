using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector3Int32Avx :
    ISimdIntegerVector3<PacketVector3Int32Avx, PacketInt32Avx, int, PacketVector3Int32AvxMask, PacketInt32AvxMask>
{
    public PacketVector3Int32Avx(PacketInt32Avx x, PacketInt32Avx y, PacketInt32Avx z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketInt32Avx.LaneCount;

    public PacketInt32Avx X { get; }

    public PacketInt32Avx Y { get; }

    public PacketInt32Avx Z { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Avx Create(PacketInt32Avx x, PacketInt32Avx y, PacketInt32Avx z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Avx Broadcast(int value)
    {
        PacketInt32Avx packet = PacketInt32Avx.Broadcast(value);
        return new(packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Avx Select(PacketVector3Int32AvxMask mask, PacketVector3Int32Avx ifTrue, PacketVector3Int32Avx ifFalse)
    {
        return new(
            PacketInt32Avx.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt32Avx.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketInt32Avx.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Avx Select(PacketInt32AvxMask mask, PacketVector3Int32Avx ifTrue, PacketVector3Int32Avx ifFalse)
    {
        return new(
            PacketInt32Avx.Select(mask, ifTrue.X, ifFalse.X),
            PacketInt32Avx.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketInt32Avx.Select(mask, ifTrue.Z, ifFalse.Z));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Avx operator +(PacketVector3Int32Avx value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Avx operator +(PacketVector3Int32Avx left, PacketVector3Int32Avx right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Avx operator -(PacketVector3Int32Avx left, PacketVector3Int32Avx right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Avx operator *(PacketVector3Int32Avx left, PacketVector3Int32Avx right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Avx operator -(PacketVector3Int32Avx value) => new(-value.X, -value.Y, -value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32AvxMask operator ==(PacketVector3Int32Avx left, PacketVector3Int32Avx right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32AvxMask operator !=(PacketVector3Int32Avx left, PacketVector3Int32Avx right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3Int32Avx other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}

public readonly struct PacketVector3Int32AvxMask :
    ISimdVector3Mask<PacketVector3Int32AvxMask, PacketInt32AvxMask>
{
    public PacketVector3Int32AvxMask(PacketInt32AvxMask x, PacketInt32AvxMask y, PacketInt32AvxMask z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketInt32AvxMask.LaneCount;

    public PacketInt32AvxMask X { get; }

    public PacketInt32AvxMask Y { get; }

    public PacketInt32AvxMask Z { get; }

    public static PacketVector3Int32AvxMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt32AvxMask.True, PacketInt32AvxMask.True, PacketInt32AvxMask.True);
    }

    public static PacketVector3Int32AvxMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt32AvxMask.False, PacketInt32AvxMask.False, PacketInt32AvxMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32AvxMask Create(PacketInt32AvxMask x, PacketInt32AvxMask y, PacketInt32AvxMask z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32AvxMask Broadcast(PacketInt32AvxMask value) => new(value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask All(PacketVector3Int32AvxMask value) => value.X & value.Y & value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask Any(PacketVector3Int32AvxMask value) => value.X | value.Y | value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask None(PacketVector3Int32AvxMask value) => ~(value.X | value.Y | value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32AvxMask AndNot(PacketVector3Int32AvxMask left, PacketVector3Int32AvxMask right)
    {
        return new(
            PacketInt32AvxMask.AndNot(left.X, right.X),
            PacketInt32AvxMask.AndNot(left.Y, right.Y),
            PacketInt32AvxMask.AndNot(left.Z, right.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32AvxMask Select(PacketVector3Int32AvxMask mask, PacketVector3Int32AvxMask ifTrue, PacketVector3Int32AvxMask ifFalse)
    {
        return new(
            PacketInt32AvxMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt32AvxMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketInt32AvxMask.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32AvxMask And(PacketVector3Int32AvxMask left, PacketVector3Int32AvxMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32AvxMask Or(PacketVector3Int32AvxMask left, PacketVector3Int32AvxMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32AvxMask Xor(PacketVector3Int32AvxMask left, PacketVector3Int32AvxMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32AvxMask Not(PacketVector3Int32AvxMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32AvxMask operator &(PacketVector3Int32AvxMask left, PacketVector3Int32AvxMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32AvxMask operator |(PacketVector3Int32AvxMask left, PacketVector3Int32AvxMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32AvxMask operator ^(PacketVector3Int32AvxMask left, PacketVector3Int32AvxMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32AvxMask operator ~(PacketVector3Int32AvxMask value) => new(~value.X, ~value.Y, ~value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32AvxMask operator ==(PacketVector3Int32AvxMask left, PacketVector3Int32AvxMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32AvxMask operator !=(PacketVector3Int32AvxMask left, PacketVector3Int32AvxMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3Int32AvxMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}