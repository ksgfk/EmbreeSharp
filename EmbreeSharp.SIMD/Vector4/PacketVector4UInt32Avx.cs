using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector4UInt32Avx :
    ISimdIntegerVector4<PacketVector4UInt32Avx, PacketUInt32Avx, uint, PacketVector4UInt32AvxMask, PacketUInt32AvxMask>
{
    public PacketVector4UInt32Avx(PacketUInt32Avx x, PacketUInt32Avx y, PacketUInt32Avx z, PacketUInt32Avx w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketUInt32Avx.LaneCount;

    public PacketUInt32Avx X { get; }

    public PacketUInt32Avx Y { get; }

    public PacketUInt32Avx Z { get; }

    public PacketUInt32Avx W { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Avx Create(PacketUInt32Avx x, PacketUInt32Avx y, PacketUInt32Avx z, PacketUInt32Avx w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Avx Broadcast(uint value)
    {
        PacketUInt32Avx packet = PacketUInt32Avx.Broadcast(value);
        return new(packet, packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Avx Select(PacketVector4UInt32AvxMask mask, PacketVector4UInt32Avx ifTrue, PacketVector4UInt32Avx ifFalse)
    {
        return new(
            PacketUInt32Avx.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt32Avx.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketUInt32Avx.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketUInt32Avx.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Avx Select(PacketUInt32AvxMask mask, PacketVector4UInt32Avx ifTrue, PacketVector4UInt32Avx ifFalse)
    {
        return new(
            PacketUInt32Avx.Select(mask, ifTrue.X, ifFalse.X),
            PacketUInt32Avx.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketUInt32Avx.Select(mask, ifTrue.Z, ifFalse.Z),
            PacketUInt32Avx.Select(mask, ifTrue.W, ifFalse.W));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Avx operator +(PacketVector4UInt32Avx value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Avx operator +(PacketVector4UInt32Avx left, PacketVector4UInt32Avx right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Avx operator -(PacketVector4UInt32Avx left, PacketVector4UInt32Avx right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Avx operator *(PacketVector4UInt32Avx left, PacketVector4UInt32Avx right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Avx operator -(PacketVector4UInt32Avx value) => new(-value.X, -value.Y, -value.Z, -value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32AvxMask operator ==(PacketVector4UInt32Avx left, PacketVector4UInt32Avx right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32AvxMask operator !=(PacketVector4UInt32Avx left, PacketVector4UInt32Avx right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4UInt32Avx other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct PacketVector4UInt32AvxMask :
    ISimdVector4Mask<PacketVector4UInt32AvxMask, PacketUInt32AvxMask>
{
    public PacketVector4UInt32AvxMask(PacketUInt32AvxMask x, PacketUInt32AvxMask y, PacketUInt32AvxMask z, PacketUInt32AvxMask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketUInt32AvxMask.LaneCount;

    public PacketUInt32AvxMask X { get; }

    public PacketUInt32AvxMask Y { get; }

    public PacketUInt32AvxMask Z { get; }

    public PacketUInt32AvxMask W { get; }

    public static PacketVector4UInt32AvxMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt32AvxMask.True, PacketUInt32AvxMask.True, PacketUInt32AvxMask.True, PacketUInt32AvxMask.True);
    }

    public static PacketVector4UInt32AvxMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt32AvxMask.False, PacketUInt32AvxMask.False, PacketUInt32AvxMask.False, PacketUInt32AvxMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32AvxMask Create(PacketUInt32AvxMask x, PacketUInt32AvxMask y, PacketUInt32AvxMask z, PacketUInt32AvxMask w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32AvxMask Broadcast(PacketUInt32AvxMask value) => new(value, value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask All(PacketVector4UInt32AvxMask value) => value.X & value.Y & value.Z & value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask Any(PacketVector4UInt32AvxMask value) => value.X | value.Y | value.Z | value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32AvxMask None(PacketVector4UInt32AvxMask value) => ~(value.X | value.Y | value.Z | value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32AvxMask AndNot(PacketVector4UInt32AvxMask left, PacketVector4UInt32AvxMask right)
    {
        return new(
            PacketUInt32AvxMask.AndNot(left.X, right.X),
            PacketUInt32AvxMask.AndNot(left.Y, right.Y),
            PacketUInt32AvxMask.AndNot(left.Z, right.Z),
            PacketUInt32AvxMask.AndNot(left.W, right.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32AvxMask Select(PacketVector4UInt32AvxMask mask, PacketVector4UInt32AvxMask ifTrue, PacketVector4UInt32AvxMask ifFalse)
    {
        return new(
            PacketUInt32AvxMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt32AvxMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketUInt32AvxMask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketUInt32AvxMask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32AvxMask And(PacketVector4UInt32AvxMask left, PacketVector4UInt32AvxMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32AvxMask Or(PacketVector4UInt32AvxMask left, PacketVector4UInt32AvxMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32AvxMask Xor(PacketVector4UInt32AvxMask left, PacketVector4UInt32AvxMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32AvxMask Not(PacketVector4UInt32AvxMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32AvxMask operator &(PacketVector4UInt32AvxMask left, PacketVector4UInt32AvxMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32AvxMask operator |(PacketVector4UInt32AvxMask left, PacketVector4UInt32AvxMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32AvxMask operator ^(PacketVector4UInt32AvxMask left, PacketVector4UInt32AvxMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32AvxMask operator ~(PacketVector4UInt32AvxMask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32AvxMask operator ==(PacketVector4UInt32AvxMask left, PacketVector4UInt32AvxMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32AvxMask operator !=(PacketVector4UInt32AvxMask left, PacketVector4UInt32AvxMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4UInt32AvxMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}