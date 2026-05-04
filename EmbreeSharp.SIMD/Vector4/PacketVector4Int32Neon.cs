using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector4Int32Neon :
    ISimdIntegerVector4<PacketVector4Int32Neon, PacketInt32Neon, int, PacketVector4Int32NeonMask, PacketInt32NeonMask>
{
    public PacketVector4Int32Neon(PacketInt32Neon x, PacketInt32Neon y, PacketInt32Neon z, PacketInt32Neon w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketInt32Neon.LaneCount;

    public PacketInt32Neon X { get; }

    public PacketInt32Neon Y { get; }

    public PacketInt32Neon Z { get; }

    public PacketInt32Neon W { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Neon Create(PacketInt32Neon x, PacketInt32Neon y, PacketInt32Neon z, PacketInt32Neon w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Neon Broadcast(int value)
    {
        PacketInt32Neon packet = PacketInt32Neon.Broadcast(value);
        return new(packet, packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Neon Select(PacketVector4Int32NeonMask mask, PacketVector4Int32Neon ifTrue, PacketVector4Int32Neon ifFalse)
    {
        return new(
            PacketInt32Neon.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt32Neon.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketInt32Neon.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketInt32Neon.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Neon Select(PacketInt32NeonMask mask, PacketVector4Int32Neon ifTrue, PacketVector4Int32Neon ifFalse)
    {
        return new(
            PacketInt32Neon.Select(mask, ifTrue.X, ifFalse.X),
            PacketInt32Neon.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketInt32Neon.Select(mask, ifTrue.Z, ifFalse.Z),
            PacketInt32Neon.Select(mask, ifTrue.W, ifFalse.W));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Neon operator +(PacketVector4Int32Neon value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Neon operator +(PacketVector4Int32Neon left, PacketVector4Int32Neon right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Neon operator -(PacketVector4Int32Neon left, PacketVector4Int32Neon right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Neon operator *(PacketVector4Int32Neon left, PacketVector4Int32Neon right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Neon operator -(PacketVector4Int32Neon value) => new(-value.X, -value.Y, -value.Z, -value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32NeonMask operator ==(PacketVector4Int32Neon left, PacketVector4Int32Neon right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32NeonMask operator !=(PacketVector4Int32Neon left, PacketVector4Int32Neon right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4Int32Neon other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct PacketVector4Int32NeonMask :
    ISimdVector4Mask<PacketVector4Int32NeonMask, PacketInt32NeonMask>
{
    public PacketVector4Int32NeonMask(PacketInt32NeonMask x, PacketInt32NeonMask y, PacketInt32NeonMask z, PacketInt32NeonMask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketInt32NeonMask.LaneCount;

    public PacketInt32NeonMask X { get; }

    public PacketInt32NeonMask Y { get; }

    public PacketInt32NeonMask Z { get; }

    public PacketInt32NeonMask W { get; }

    public static PacketVector4Int32NeonMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt32NeonMask.True, PacketInt32NeonMask.True, PacketInt32NeonMask.True, PacketInt32NeonMask.True);
    }

    public static PacketVector4Int32NeonMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt32NeonMask.False, PacketInt32NeonMask.False, PacketInt32NeonMask.False, PacketInt32NeonMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32NeonMask Create(PacketInt32NeonMask x, PacketInt32NeonMask y, PacketInt32NeonMask z, PacketInt32NeonMask w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32NeonMask Broadcast(PacketInt32NeonMask value) => new(value, value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32NeonMask All(PacketVector4Int32NeonMask value) => value.X & value.Y & value.Z & value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32NeonMask Any(PacketVector4Int32NeonMask value) => value.X | value.Y | value.Z | value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32NeonMask None(PacketVector4Int32NeonMask value) => ~(value.X | value.Y | value.Z | value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32NeonMask AndNot(PacketVector4Int32NeonMask left, PacketVector4Int32NeonMask right)
    {
        return new(
            PacketInt32NeonMask.AndNot(left.X, right.X),
            PacketInt32NeonMask.AndNot(left.Y, right.Y),
            PacketInt32NeonMask.AndNot(left.Z, right.Z),
            PacketInt32NeonMask.AndNot(left.W, right.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32NeonMask Select(PacketVector4Int32NeonMask mask, PacketVector4Int32NeonMask ifTrue, PacketVector4Int32NeonMask ifFalse)
    {
        return new(
            PacketInt32NeonMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt32NeonMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketInt32NeonMask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketInt32NeonMask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32NeonMask And(PacketVector4Int32NeonMask left, PacketVector4Int32NeonMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32NeonMask Or(PacketVector4Int32NeonMask left, PacketVector4Int32NeonMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32NeonMask Xor(PacketVector4Int32NeonMask left, PacketVector4Int32NeonMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32NeonMask Not(PacketVector4Int32NeonMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32NeonMask operator &(PacketVector4Int32NeonMask left, PacketVector4Int32NeonMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32NeonMask operator |(PacketVector4Int32NeonMask left, PacketVector4Int32NeonMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32NeonMask operator ^(PacketVector4Int32NeonMask left, PacketVector4Int32NeonMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32NeonMask operator ~(PacketVector4Int32NeonMask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32NeonMask operator ==(PacketVector4Int32NeonMask left, PacketVector4Int32NeonMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32NeonMask operator !=(PacketVector4Int32NeonMask left, PacketVector4Int32NeonMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4Int32NeonMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}