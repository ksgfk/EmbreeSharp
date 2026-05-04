using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector4UInt32Neon :
    ISimdIntegerVector4<PacketVector4UInt32Neon, PacketUInt32Neon, uint, PacketVector4UInt32NeonMask, PacketUInt32NeonMask>
{
    public PacketVector4UInt32Neon(PacketUInt32Neon x, PacketUInt32Neon y, PacketUInt32Neon z, PacketUInt32Neon w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketUInt32Neon.LaneCount;

    public PacketUInt32Neon X { get; }

    public PacketUInt32Neon Y { get; }

    public PacketUInt32Neon Z { get; }

    public PacketUInt32Neon W { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Neon Create(PacketUInt32Neon x, PacketUInt32Neon y, PacketUInt32Neon z, PacketUInt32Neon w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Neon Broadcast(uint value)
    {
        PacketUInt32Neon packet = PacketUInt32Neon.Broadcast(value);
        return new(packet, packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Neon Select(PacketVector4UInt32NeonMask mask, PacketVector4UInt32Neon ifTrue, PacketVector4UInt32Neon ifFalse)
    {
        return new(
            PacketUInt32Neon.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt32Neon.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketUInt32Neon.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketUInt32Neon.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Neon Select(PacketUInt32NeonMask mask, PacketVector4UInt32Neon ifTrue, PacketVector4UInt32Neon ifFalse)
    {
        return new(
            PacketUInt32Neon.Select(mask, ifTrue.X, ifFalse.X),
            PacketUInt32Neon.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketUInt32Neon.Select(mask, ifTrue.Z, ifFalse.Z),
            PacketUInt32Neon.Select(mask, ifTrue.W, ifFalse.W));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Neon operator +(PacketVector4UInt32Neon value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Neon operator +(PacketVector4UInt32Neon left, PacketVector4UInt32Neon right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Neon operator -(PacketVector4UInt32Neon left, PacketVector4UInt32Neon right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Neon operator *(PacketVector4UInt32Neon left, PacketVector4UInt32Neon right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Neon operator -(PacketVector4UInt32Neon value) => new(-value.X, -value.Y, -value.Z, -value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32NeonMask operator ==(PacketVector4UInt32Neon left, PacketVector4UInt32Neon right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32NeonMask operator !=(PacketVector4UInt32Neon left, PacketVector4UInt32Neon right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4UInt32Neon other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct PacketVector4UInt32NeonMask :
    ISimdVector4Mask<PacketVector4UInt32NeonMask, PacketUInt32NeonMask>
{
    public PacketVector4UInt32NeonMask(PacketUInt32NeonMask x, PacketUInt32NeonMask y, PacketUInt32NeonMask z, PacketUInt32NeonMask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketUInt32NeonMask.LaneCount;

    public PacketUInt32NeonMask X { get; }

    public PacketUInt32NeonMask Y { get; }

    public PacketUInt32NeonMask Z { get; }

    public PacketUInt32NeonMask W { get; }

    public static PacketVector4UInt32NeonMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt32NeonMask.True, PacketUInt32NeonMask.True, PacketUInt32NeonMask.True, PacketUInt32NeonMask.True);
    }

    public static PacketVector4UInt32NeonMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt32NeonMask.False, PacketUInt32NeonMask.False, PacketUInt32NeonMask.False, PacketUInt32NeonMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32NeonMask Create(PacketUInt32NeonMask x, PacketUInt32NeonMask y, PacketUInt32NeonMask z, PacketUInt32NeonMask w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32NeonMask Broadcast(PacketUInt32NeonMask value) => new(value, value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask All(PacketVector4UInt32NeonMask value) => value.X & value.Y & value.Z & value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask Any(PacketVector4UInt32NeonMask value) => value.X | value.Y | value.Z | value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32NeonMask None(PacketVector4UInt32NeonMask value) => ~(value.X | value.Y | value.Z | value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32NeonMask AndNot(PacketVector4UInt32NeonMask left, PacketVector4UInt32NeonMask right)
    {
        return new(
            PacketUInt32NeonMask.AndNot(left.X, right.X),
            PacketUInt32NeonMask.AndNot(left.Y, right.Y),
            PacketUInt32NeonMask.AndNot(left.Z, right.Z),
            PacketUInt32NeonMask.AndNot(left.W, right.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32NeonMask Select(PacketVector4UInt32NeonMask mask, PacketVector4UInt32NeonMask ifTrue, PacketVector4UInt32NeonMask ifFalse)
    {
        return new(
            PacketUInt32NeonMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt32NeonMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketUInt32NeonMask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketUInt32NeonMask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32NeonMask And(PacketVector4UInt32NeonMask left, PacketVector4UInt32NeonMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32NeonMask Or(PacketVector4UInt32NeonMask left, PacketVector4UInt32NeonMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32NeonMask Xor(PacketVector4UInt32NeonMask left, PacketVector4UInt32NeonMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32NeonMask Not(PacketVector4UInt32NeonMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32NeonMask operator &(PacketVector4UInt32NeonMask left, PacketVector4UInt32NeonMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32NeonMask operator |(PacketVector4UInt32NeonMask left, PacketVector4UInt32NeonMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32NeonMask operator ^(PacketVector4UInt32NeonMask left, PacketVector4UInt32NeonMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32NeonMask operator ~(PacketVector4UInt32NeonMask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32NeonMask operator ==(PacketVector4UInt32NeonMask left, PacketVector4UInt32NeonMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32NeonMask operator !=(PacketVector4UInt32NeonMask left, PacketVector4UInt32NeonMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4UInt32NeonMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}