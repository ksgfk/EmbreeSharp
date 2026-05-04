using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector2Float32Neon :
    ISimdFloatingPointVector2<PacketVector2Float32Neon, PacketFloat32Neon, float, PacketVector2Float32NeonMask, PacketFloat32NeonMask>
{
    public PacketVector2Float32Neon(PacketFloat32Neon x, PacketFloat32Neon y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketFloat32Neon.LaneCount;

    public PacketFloat32Neon X { get; }

    public PacketFloat32Neon Y { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Neon Create(PacketFloat32Neon x, PacketFloat32Neon y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Neon Broadcast(float value)
    {
        PacketFloat32Neon packet = PacketFloat32Neon.Broadcast(value);
        return new(packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Neon Select(PacketVector2Float32NeonMask mask, PacketVector2Float32Neon ifTrue, PacketVector2Float32Neon ifFalse)
    {
        return new(
            PacketFloat32Neon.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32Neon.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Neon Select(PacketFloat32NeonMask mask, PacketVector2Float32Neon ifTrue, PacketVector2Float32Neon ifFalse)
    {
        return new(
            PacketFloat32Neon.Select(mask, ifTrue.X, ifFalse.X),
            PacketFloat32Neon.Select(mask, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Neon Dot(PacketVector2Float32Neon left, PacketVector2Float32Neon right)
    {
        return PacketFloat32Neon.FusedMultiplyAdd(left.Y, right.Y, left.X * right.X);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Neon operator +(PacketVector2Float32Neon value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Neon operator +(PacketVector2Float32Neon left, PacketVector2Float32Neon right) => new(left.X + right.X, left.Y + right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Neon operator -(PacketVector2Float32Neon left, PacketVector2Float32Neon right) => new(left.X - right.X, left.Y - right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Neon operator *(PacketVector2Float32Neon left, PacketVector2Float32Neon right) => new(left.X * right.X, left.Y * right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Neon operator -(PacketVector2Float32Neon value) => new(-value.X, -value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32NeonMask operator ==(PacketVector2Float32Neon left, PacketVector2Float32Neon right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32NeonMask operator !=(PacketVector2Float32Neon left, PacketVector2Float32Neon right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2Float32Neon other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}

public readonly struct PacketVector2Float32NeonMask :
    ISimdVector2Mask<PacketVector2Float32NeonMask, PacketFloat32NeonMask>
{
    public PacketVector2Float32NeonMask(PacketFloat32NeonMask x, PacketFloat32NeonMask y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketFloat32NeonMask.LaneCount;

    public PacketFloat32NeonMask X { get; }

    public PacketFloat32NeonMask Y { get; }

    public static PacketVector2Float32NeonMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat32NeonMask.True, PacketFloat32NeonMask.True);
    }

    public static PacketVector2Float32NeonMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat32NeonMask.False, PacketFloat32NeonMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32NeonMask Create(PacketFloat32NeonMask x, PacketFloat32NeonMask y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32NeonMask Broadcast(PacketFloat32NeonMask value) => new(value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask All(PacketVector2Float32NeonMask value) => value.X & value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask Any(PacketVector2Float32NeonMask value) => value.X | value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask None(PacketVector2Float32NeonMask value) => ~(value.X | value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32NeonMask AndNot(PacketVector2Float32NeonMask left, PacketVector2Float32NeonMask right)
    {
        return new(
            PacketFloat32NeonMask.AndNot(left.X, right.X),
            PacketFloat32NeonMask.AndNot(left.Y, right.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32NeonMask Select(PacketVector2Float32NeonMask mask, PacketVector2Float32NeonMask ifTrue, PacketVector2Float32NeonMask ifFalse)
    {
        return new(
            PacketFloat32NeonMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32NeonMask.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32NeonMask And(PacketVector2Float32NeonMask left, PacketVector2Float32NeonMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32NeonMask Or(PacketVector2Float32NeonMask left, PacketVector2Float32NeonMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32NeonMask Xor(PacketVector2Float32NeonMask left, PacketVector2Float32NeonMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32NeonMask Not(PacketVector2Float32NeonMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32NeonMask operator &(PacketVector2Float32NeonMask left, PacketVector2Float32NeonMask right) => new(left.X & right.X, left.Y & right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32NeonMask operator |(PacketVector2Float32NeonMask left, PacketVector2Float32NeonMask right) => new(left.X | right.X, left.Y | right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32NeonMask operator ^(PacketVector2Float32NeonMask left, PacketVector2Float32NeonMask right) => new(left.X ^ right.X, left.Y ^ right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32NeonMask operator ~(PacketVector2Float32NeonMask value) => new(~value.X, ~value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32NeonMask operator ==(PacketVector2Float32NeonMask left, PacketVector2Float32NeonMask right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32NeonMask operator !=(PacketVector2Float32NeonMask left, PacketVector2Float32NeonMask right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2Float32NeonMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}