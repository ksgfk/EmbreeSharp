using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector4Float64Neon :
    ISimdFloatingPointVector4<PacketVector4Float64Neon, PacketFloat64Neon, double, PacketVector4Float64NeonMask, PacketFloat64NeonMask>
{
    public PacketVector4Float64Neon(PacketFloat64Neon x, PacketFloat64Neon y, PacketFloat64Neon z, PacketFloat64Neon w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketFloat64Neon.LaneCount;

    public PacketFloat64Neon X { get; }

    public PacketFloat64Neon Y { get; }

    public PacketFloat64Neon Z { get; }

    public PacketFloat64Neon W { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Neon Create(PacketFloat64Neon x, PacketFloat64Neon y, PacketFloat64Neon z, PacketFloat64Neon w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Neon Broadcast(double value)
    {
        PacketFloat64Neon packet = PacketFloat64Neon.Broadcast(value);
        return new(packet, packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Neon Select(PacketVector4Float64NeonMask mask, PacketVector4Float64Neon ifTrue, PacketVector4Float64Neon ifFalse)
    {
        return new(
            PacketFloat64Neon.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64Neon.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat64Neon.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat64Neon.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Neon Select(PacketFloat64NeonMask mask, PacketVector4Float64Neon ifTrue, PacketVector4Float64Neon ifFalse)
    {
        return new(
            PacketFloat64Neon.Select(mask, ifTrue.X, ifFalse.X),
            PacketFloat64Neon.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketFloat64Neon.Select(mask, ifTrue.Z, ifFalse.Z),
            PacketFloat64Neon.Select(mask, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Neon Dot(PacketVector4Float64Neon left, PacketVector4Float64Neon right)
    {
        return PacketFloat64Neon.FusedMultiplyAdd(
            left.W,
            right.W,
            PacketFloat64Neon.FusedMultiplyAdd(left.Z, right.Z, PacketFloat64Neon.FusedMultiplyAdd(left.Y, right.Y, left.X * right.X)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Neon operator +(PacketVector4Float64Neon value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Neon operator +(PacketVector4Float64Neon left, PacketVector4Float64Neon right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Neon operator -(PacketVector4Float64Neon left, PacketVector4Float64Neon right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Neon operator *(PacketVector4Float64Neon left, PacketVector4Float64Neon right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Neon operator -(PacketVector4Float64Neon value) => new(-value.X, -value.Y, -value.Z, -value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64NeonMask operator ==(PacketVector4Float64Neon left, PacketVector4Float64Neon right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64NeonMask operator !=(PacketVector4Float64Neon left, PacketVector4Float64Neon right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4Float64Neon other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct PacketVector4Float64NeonMask :
    ISimdVector4Mask<PacketVector4Float64NeonMask, PacketFloat64NeonMask>
{
    public PacketVector4Float64NeonMask(PacketFloat64NeonMask x, PacketFloat64NeonMask y, PacketFloat64NeonMask z, PacketFloat64NeonMask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketFloat64NeonMask.LaneCount;

    public PacketFloat64NeonMask X { get; }

    public PacketFloat64NeonMask Y { get; }

    public PacketFloat64NeonMask Z { get; }

    public PacketFloat64NeonMask W { get; }

    public static PacketVector4Float64NeonMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat64NeonMask.True, PacketFloat64NeonMask.True, PacketFloat64NeonMask.True, PacketFloat64NeonMask.True);
    }

    public static PacketVector4Float64NeonMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat64NeonMask.False, PacketFloat64NeonMask.False, PacketFloat64NeonMask.False, PacketFloat64NeonMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64NeonMask Create(PacketFloat64NeonMask x, PacketFloat64NeonMask y, PacketFloat64NeonMask z, PacketFloat64NeonMask w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64NeonMask Broadcast(PacketFloat64NeonMask value) => new(value, value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask All(PacketVector4Float64NeonMask value) => value.X & value.Y & value.Z & value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask Any(PacketVector4Float64NeonMask value) => value.X | value.Y | value.Z | value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask None(PacketVector4Float64NeonMask value) => ~(value.X | value.Y | value.Z | value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64NeonMask AndNot(PacketVector4Float64NeonMask left, PacketVector4Float64NeonMask right)
    {
        return new(
            PacketFloat64NeonMask.AndNot(left.X, right.X),
            PacketFloat64NeonMask.AndNot(left.Y, right.Y),
            PacketFloat64NeonMask.AndNot(left.Z, right.Z),
            PacketFloat64NeonMask.AndNot(left.W, right.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64NeonMask Select(PacketVector4Float64NeonMask mask, PacketVector4Float64NeonMask ifTrue, PacketVector4Float64NeonMask ifFalse)
    {
        return new(
            PacketFloat64NeonMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64NeonMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat64NeonMask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat64NeonMask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64NeonMask And(PacketVector4Float64NeonMask left, PacketVector4Float64NeonMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64NeonMask Or(PacketVector4Float64NeonMask left, PacketVector4Float64NeonMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64NeonMask Xor(PacketVector4Float64NeonMask left, PacketVector4Float64NeonMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64NeonMask Not(PacketVector4Float64NeonMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64NeonMask operator &(PacketVector4Float64NeonMask left, PacketVector4Float64NeonMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64NeonMask operator |(PacketVector4Float64NeonMask left, PacketVector4Float64NeonMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64NeonMask operator ^(PacketVector4Float64NeonMask left, PacketVector4Float64NeonMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64NeonMask operator ~(PacketVector4Float64NeonMask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64NeonMask operator ==(PacketVector4Float64NeonMask left, PacketVector4Float64NeonMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64NeonMask operator !=(PacketVector4Float64NeonMask left, PacketVector4Float64NeonMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4Float64NeonMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}