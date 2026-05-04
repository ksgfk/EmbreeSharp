using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector4UInt64Neon :
    ISimdIntegerVector4<PacketVector4UInt64Neon, PacketUInt64Neon, ulong, PacketVector4UInt64NeonMask, PacketUInt64NeonMask>
{
    public PacketVector4UInt64Neon(PacketUInt64Neon x, PacketUInt64Neon y, PacketUInt64Neon z, PacketUInt64Neon w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketUInt64Neon.LaneCount;

    public PacketUInt64Neon X { get; }

    public PacketUInt64Neon Y { get; }

    public PacketUInt64Neon Z { get; }

    public PacketUInt64Neon W { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Neon Create(PacketUInt64Neon x, PacketUInt64Neon y, PacketUInt64Neon z, PacketUInt64Neon w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Neon Broadcast(ulong value)
    {
        PacketUInt64Neon packet = PacketUInt64Neon.Broadcast(value);
        return new(packet, packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Neon Select(PacketVector4UInt64NeonMask mask, PacketVector4UInt64Neon ifTrue, PacketVector4UInt64Neon ifFalse)
    {
        return new(
            PacketUInt64Neon.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt64Neon.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketUInt64Neon.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketUInt64Neon.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Neon Select(PacketUInt64NeonMask mask, PacketVector4UInt64Neon ifTrue, PacketVector4UInt64Neon ifFalse)
    {
        return new(
            PacketUInt64Neon.Select(mask, ifTrue.X, ifFalse.X),
            PacketUInt64Neon.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketUInt64Neon.Select(mask, ifTrue.Z, ifFalse.Z),
            PacketUInt64Neon.Select(mask, ifTrue.W, ifFalse.W));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Neon operator +(PacketVector4UInt64Neon value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Neon operator +(PacketVector4UInt64Neon left, PacketVector4UInt64Neon right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Neon operator -(PacketVector4UInt64Neon left, PacketVector4UInt64Neon right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Neon operator *(PacketVector4UInt64Neon left, PacketVector4UInt64Neon right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Neon operator -(PacketVector4UInt64Neon value) => new(-value.X, -value.Y, -value.Z, -value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64NeonMask operator ==(PacketVector4UInt64Neon left, PacketVector4UInt64Neon right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64NeonMask operator !=(PacketVector4UInt64Neon left, PacketVector4UInt64Neon right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4UInt64Neon other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct PacketVector4UInt64NeonMask :
    ISimdVector4Mask<PacketVector4UInt64NeonMask, PacketUInt64NeonMask>
{
    public PacketVector4UInt64NeonMask(PacketUInt64NeonMask x, PacketUInt64NeonMask y, PacketUInt64NeonMask z, PacketUInt64NeonMask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketUInt64NeonMask.LaneCount;

    public PacketUInt64NeonMask X { get; }

    public PacketUInt64NeonMask Y { get; }

    public PacketUInt64NeonMask Z { get; }

    public PacketUInt64NeonMask W { get; }

    public static PacketVector4UInt64NeonMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt64NeonMask.True, PacketUInt64NeonMask.True, PacketUInt64NeonMask.True, PacketUInt64NeonMask.True);
    }

    public static PacketVector4UInt64NeonMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt64NeonMask.False, PacketUInt64NeonMask.False, PacketUInt64NeonMask.False, PacketUInt64NeonMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64NeonMask Create(PacketUInt64NeonMask x, PacketUInt64NeonMask y, PacketUInt64NeonMask z, PacketUInt64NeonMask w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64NeonMask Broadcast(PacketUInt64NeonMask value) => new(value, value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask All(PacketVector4UInt64NeonMask value) => value.X & value.Y & value.Z & value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask Any(PacketVector4UInt64NeonMask value) => value.X | value.Y | value.Z | value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64NeonMask None(PacketVector4UInt64NeonMask value) => ~(value.X | value.Y | value.Z | value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64NeonMask AndNot(PacketVector4UInt64NeonMask left, PacketVector4UInt64NeonMask right)
    {
        return new(
            PacketUInt64NeonMask.AndNot(left.X, right.X),
            PacketUInt64NeonMask.AndNot(left.Y, right.Y),
            PacketUInt64NeonMask.AndNot(left.Z, right.Z),
            PacketUInt64NeonMask.AndNot(left.W, right.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64NeonMask Select(PacketVector4UInt64NeonMask mask, PacketVector4UInt64NeonMask ifTrue, PacketVector4UInt64NeonMask ifFalse)
    {
        return new(
            PacketUInt64NeonMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt64NeonMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketUInt64NeonMask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketUInt64NeonMask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64NeonMask And(PacketVector4UInt64NeonMask left, PacketVector4UInt64NeonMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64NeonMask Or(PacketVector4UInt64NeonMask left, PacketVector4UInt64NeonMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64NeonMask Xor(PacketVector4UInt64NeonMask left, PacketVector4UInt64NeonMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64NeonMask Not(PacketVector4UInt64NeonMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64NeonMask operator &(PacketVector4UInt64NeonMask left, PacketVector4UInt64NeonMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64NeonMask operator |(PacketVector4UInt64NeonMask left, PacketVector4UInt64NeonMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64NeonMask operator ^(PacketVector4UInt64NeonMask left, PacketVector4UInt64NeonMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64NeonMask operator ~(PacketVector4UInt64NeonMask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64NeonMask operator ==(PacketVector4UInt64NeonMask left, PacketVector4UInt64NeonMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64NeonMask operator !=(PacketVector4UInt64NeonMask left, PacketVector4UInt64NeonMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4UInt64NeonMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}