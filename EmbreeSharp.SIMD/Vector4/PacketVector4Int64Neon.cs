using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector4Int64Neon :
    ISimdIntegerVector4<PacketVector4Int64Neon, PacketInt64Neon, long, PacketVector4Int64NeonMask, PacketInt64NeonMask>
{
    public PacketVector4Int64Neon(PacketInt64Neon x, PacketInt64Neon y, PacketInt64Neon z, PacketInt64Neon w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketInt64Neon.LaneCount;

    public PacketInt64Neon X { get; }

    public PacketInt64Neon Y { get; }

    public PacketInt64Neon Z { get; }

    public PacketInt64Neon W { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Neon Create(PacketInt64Neon x, PacketInt64Neon y, PacketInt64Neon z, PacketInt64Neon w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Neon Broadcast(long value)
    {
        PacketInt64Neon packet = PacketInt64Neon.Broadcast(value);
        return new(packet, packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Neon Select(PacketVector4Int64NeonMask mask, PacketVector4Int64Neon ifTrue, PacketVector4Int64Neon ifFalse)
    {
        return new(
            PacketInt64Neon.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt64Neon.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketInt64Neon.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketInt64Neon.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Neon Select(PacketInt64NeonMask mask, PacketVector4Int64Neon ifTrue, PacketVector4Int64Neon ifFalse)
    {
        return new(
            PacketInt64Neon.Select(mask, ifTrue.X, ifFalse.X),
            PacketInt64Neon.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketInt64Neon.Select(mask, ifTrue.Z, ifFalse.Z),
            PacketInt64Neon.Select(mask, ifTrue.W, ifFalse.W));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Neon operator +(PacketVector4Int64Neon value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Neon operator +(PacketVector4Int64Neon left, PacketVector4Int64Neon right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Neon operator -(PacketVector4Int64Neon left, PacketVector4Int64Neon right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Neon operator *(PacketVector4Int64Neon left, PacketVector4Int64Neon right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Neon operator -(PacketVector4Int64Neon value) => new(-value.X, -value.Y, -value.Z, -value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64NeonMask operator ==(PacketVector4Int64Neon left, PacketVector4Int64Neon right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64NeonMask operator !=(PacketVector4Int64Neon left, PacketVector4Int64Neon right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4Int64Neon other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct PacketVector4Int64NeonMask :
    ISimdVector4Mask<PacketVector4Int64NeonMask, PacketInt64NeonMask>
{
    public PacketVector4Int64NeonMask(PacketInt64NeonMask x, PacketInt64NeonMask y, PacketInt64NeonMask z, PacketInt64NeonMask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketInt64NeonMask.LaneCount;

    public PacketInt64NeonMask X { get; }

    public PacketInt64NeonMask Y { get; }

    public PacketInt64NeonMask Z { get; }

    public PacketInt64NeonMask W { get; }

    public static PacketVector4Int64NeonMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt64NeonMask.True, PacketInt64NeonMask.True, PacketInt64NeonMask.True, PacketInt64NeonMask.True);
    }

    public static PacketVector4Int64NeonMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt64NeonMask.False, PacketInt64NeonMask.False, PacketInt64NeonMask.False, PacketInt64NeonMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64NeonMask Create(PacketInt64NeonMask x, PacketInt64NeonMask y, PacketInt64NeonMask z, PacketInt64NeonMask w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64NeonMask Broadcast(PacketInt64NeonMask value) => new(value, value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask All(PacketVector4Int64NeonMask value) => value.X & value.Y & value.Z & value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask Any(PacketVector4Int64NeonMask value) => value.X | value.Y | value.Z | value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64NeonMask None(PacketVector4Int64NeonMask value) => ~(value.X | value.Y | value.Z | value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64NeonMask AndNot(PacketVector4Int64NeonMask left, PacketVector4Int64NeonMask right)
    {
        return new(
            PacketInt64NeonMask.AndNot(left.X, right.X),
            PacketInt64NeonMask.AndNot(left.Y, right.Y),
            PacketInt64NeonMask.AndNot(left.Z, right.Z),
            PacketInt64NeonMask.AndNot(left.W, right.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64NeonMask Select(PacketVector4Int64NeonMask mask, PacketVector4Int64NeonMask ifTrue, PacketVector4Int64NeonMask ifFalse)
    {
        return new(
            PacketInt64NeonMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt64NeonMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketInt64NeonMask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketInt64NeonMask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64NeonMask And(PacketVector4Int64NeonMask left, PacketVector4Int64NeonMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64NeonMask Or(PacketVector4Int64NeonMask left, PacketVector4Int64NeonMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64NeonMask Xor(PacketVector4Int64NeonMask left, PacketVector4Int64NeonMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64NeonMask Not(PacketVector4Int64NeonMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64NeonMask operator &(PacketVector4Int64NeonMask left, PacketVector4Int64NeonMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64NeonMask operator |(PacketVector4Int64NeonMask left, PacketVector4Int64NeonMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64NeonMask operator ^(PacketVector4Int64NeonMask left, PacketVector4Int64NeonMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64NeonMask operator ~(PacketVector4Int64NeonMask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64NeonMask operator ==(PacketVector4Int64NeonMask left, PacketVector4Int64NeonMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64NeonMask operator !=(PacketVector4Int64NeonMask left, PacketVector4Int64NeonMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4Int64NeonMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}