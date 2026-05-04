using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector4Float32Neon :
    ISimdFloatingPointVector4<PacketVector4Float32Neon, PacketFloat32Neon, float, PacketVector4Float32NeonMask, PacketFloat32NeonMask>
{
    public PacketVector4Float32Neon(PacketFloat32Neon x, PacketFloat32Neon y, PacketFloat32Neon z, PacketFloat32Neon w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketFloat32Neon.LaneCount;

    public PacketFloat32Neon X { get; }

    public PacketFloat32Neon Y { get; }

    public PacketFloat32Neon Z { get; }

    public PacketFloat32Neon W { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Neon Create(PacketFloat32Neon x, PacketFloat32Neon y, PacketFloat32Neon z, PacketFloat32Neon w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Neon Broadcast(float value)
    {
        PacketFloat32Neon packet = PacketFloat32Neon.Broadcast(value);
        return new(packet, packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Neon Select(PacketVector4Float32NeonMask mask, PacketVector4Float32Neon ifTrue, PacketVector4Float32Neon ifFalse)
    {
        return new(
            PacketFloat32Neon.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32Neon.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat32Neon.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat32Neon.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Neon Select(PacketFloat32NeonMask mask, PacketVector4Float32Neon ifTrue, PacketVector4Float32Neon ifFalse)
    {
        return new(
            PacketFloat32Neon.Select(mask, ifTrue.X, ifFalse.X),
            PacketFloat32Neon.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketFloat32Neon.Select(mask, ifTrue.Z, ifFalse.Z),
            PacketFloat32Neon.Select(mask, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Neon Dot(PacketVector4Float32Neon left, PacketVector4Float32Neon right)
    {
        return PacketFloat32Neon.FusedMultiplyAdd(
            left.W,
            right.W,
            PacketFloat32Neon.FusedMultiplyAdd(left.Z, right.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Y, right.Y, left.X * right.X)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Neon operator +(PacketVector4Float32Neon value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Neon operator +(PacketVector4Float32Neon left, PacketVector4Float32Neon right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Neon operator -(PacketVector4Float32Neon left, PacketVector4Float32Neon right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Neon operator *(PacketVector4Float32Neon left, PacketVector4Float32Neon right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Neon operator -(PacketVector4Float32Neon value) => new(-value.X, -value.Y, -value.Z, -value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32NeonMask operator ==(PacketVector4Float32Neon left, PacketVector4Float32Neon right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32NeonMask operator !=(PacketVector4Float32Neon left, PacketVector4Float32Neon right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4Float32Neon other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct PacketVector4Float32NeonMask :
    ISimdVector4Mask<PacketVector4Float32NeonMask, PacketFloat32NeonMask>
{
    public PacketVector4Float32NeonMask(PacketFloat32NeonMask x, PacketFloat32NeonMask y, PacketFloat32NeonMask z, PacketFloat32NeonMask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketFloat32NeonMask.LaneCount;

    public PacketFloat32NeonMask X { get; }

    public PacketFloat32NeonMask Y { get; }

    public PacketFloat32NeonMask Z { get; }

    public PacketFloat32NeonMask W { get; }

    public static PacketVector4Float32NeonMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat32NeonMask.True, PacketFloat32NeonMask.True, PacketFloat32NeonMask.True, PacketFloat32NeonMask.True);
    }

    public static PacketVector4Float32NeonMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat32NeonMask.False, PacketFloat32NeonMask.False, PacketFloat32NeonMask.False, PacketFloat32NeonMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32NeonMask Create(PacketFloat32NeonMask x, PacketFloat32NeonMask y, PacketFloat32NeonMask z, PacketFloat32NeonMask w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32NeonMask Broadcast(PacketFloat32NeonMask value) => new(value, value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask All(PacketVector4Float32NeonMask value) => value.X & value.Y & value.Z & value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask Any(PacketVector4Float32NeonMask value) => value.X | value.Y | value.Z | value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask None(PacketVector4Float32NeonMask value) => ~(value.X | value.Y | value.Z | value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32NeonMask AndNot(PacketVector4Float32NeonMask left, PacketVector4Float32NeonMask right)
    {
        return new(
            PacketFloat32NeonMask.AndNot(left.X, right.X),
            PacketFloat32NeonMask.AndNot(left.Y, right.Y),
            PacketFloat32NeonMask.AndNot(left.Z, right.Z),
            PacketFloat32NeonMask.AndNot(left.W, right.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32NeonMask Select(PacketVector4Float32NeonMask mask, PacketVector4Float32NeonMask ifTrue, PacketVector4Float32NeonMask ifFalse)
    {
        return new(
            PacketFloat32NeonMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32NeonMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat32NeonMask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat32NeonMask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32NeonMask And(PacketVector4Float32NeonMask left, PacketVector4Float32NeonMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32NeonMask Or(PacketVector4Float32NeonMask left, PacketVector4Float32NeonMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32NeonMask Xor(PacketVector4Float32NeonMask left, PacketVector4Float32NeonMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32NeonMask Not(PacketVector4Float32NeonMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32NeonMask operator &(PacketVector4Float32NeonMask left, PacketVector4Float32NeonMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32NeonMask operator |(PacketVector4Float32NeonMask left, PacketVector4Float32NeonMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32NeonMask operator ^(PacketVector4Float32NeonMask left, PacketVector4Float32NeonMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32NeonMask operator ~(PacketVector4Float32NeonMask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32NeonMask operator ==(PacketVector4Float32NeonMask left, PacketVector4Float32NeonMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32NeonMask operator !=(PacketVector4Float32NeonMask left, PacketVector4Float32NeonMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4Float32NeonMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}