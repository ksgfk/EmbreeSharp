using System.Runtime.CompilerServices;

namespace EmbreeSharp.SIMD;

public readonly struct PacketVector2Float32Avx :
    ISimdFloatingPointVector2<PacketVector2Float32Avx, PacketFloat32Avx, float, PacketVector2Float32AvxMask, PacketFloat32AvxMask>
{
    public PacketVector2Float32Avx(PacketFloat32Avx x, PacketFloat32Avx y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketFloat32Avx.LaneCount;

    public PacketFloat32Avx X { get; }

    public PacketFloat32Avx Y { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Avx Create(PacketFloat32Avx x, PacketFloat32Avx y) => new(x, y);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Avx Broadcast(float value)
    {
        PacketFloat32Avx packet = PacketFloat32Avx.Broadcast(value);
        return new(packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Avx Select(PacketVector2Float32AvxMask mask, PacketVector2Float32Avx ifTrue, PacketVector2Float32Avx ifFalse)
    {
        return new(
            PacketFloat32Avx.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32Avx.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Avx Select(PacketFloat32AvxMask mask, PacketVector2Float32Avx ifTrue, PacketVector2Float32Avx ifFalse)
    {
        return new(
            PacketFloat32Avx.Select(mask, ifTrue.X, ifFalse.X),
            PacketFloat32Avx.Select(mask, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Avx Dot(PacketVector2Float32Avx left, PacketVector2Float32Avx right)
    {
        return PacketFloat32Avx.FusedMultiplyAdd(left.Y, right.Y, left.X * right.X);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Avx operator +(PacketVector2Float32Avx value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Avx operator +(PacketVector2Float32Avx left, PacketVector2Float32Avx right) => new(left.X + right.X, left.Y + right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Avx operator -(PacketVector2Float32Avx left, PacketVector2Float32Avx right) => new(left.X - right.X, left.Y - right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Avx operator *(PacketVector2Float32Avx left, PacketVector2Float32Avx right) => new(left.X * right.X, left.Y * right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Avx operator -(PacketVector2Float32Avx value) => new(-value.X, -value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32AvxMask operator ==(PacketVector2Float32Avx left, PacketVector2Float32Avx right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32AvxMask operator !=(PacketVector2Float32Avx left, PacketVector2Float32Avx right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2Float32Avx other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}

public readonly struct PacketVector2Float32AvxMask :
    ISimdVector2Mask<PacketVector2Float32AvxMask, PacketFloat32AvxMask>
{
    public PacketVector2Float32AvxMask(PacketFloat32AvxMask x, PacketFloat32AvxMask y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketFloat32AvxMask.LaneCount;

    public PacketFloat32AvxMask X { get; }

    public PacketFloat32AvxMask Y { get; }

    public static PacketVector2Float32AvxMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat32AvxMask.True, PacketFloat32AvxMask.True);
    }

    public static PacketVector2Float32AvxMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat32AvxMask.False, PacketFloat32AvxMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32AvxMask Create(PacketFloat32AvxMask x, PacketFloat32AvxMask y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32AvxMask Broadcast(PacketFloat32AvxMask value) => new(value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask All(PacketVector2Float32AvxMask value) => value.X & value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask Any(PacketVector2Float32AvxMask value) => value.X | value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask None(PacketVector2Float32AvxMask value) => ~(value.X | value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32AvxMask AndNot(PacketVector2Float32AvxMask left, PacketVector2Float32AvxMask right)
    {
        return new(
            PacketFloat32AvxMask.AndNot(left.X, right.X),
            PacketFloat32AvxMask.AndNot(left.Y, right.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32AvxMask Select(PacketVector2Float32AvxMask mask, PacketVector2Float32AvxMask ifTrue, PacketVector2Float32AvxMask ifFalse)
    {
        return new(
            PacketFloat32AvxMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32AvxMask.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32AvxMask And(PacketVector2Float32AvxMask left, PacketVector2Float32AvxMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32AvxMask Or(PacketVector2Float32AvxMask left, PacketVector2Float32AvxMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32AvxMask Xor(PacketVector2Float32AvxMask left, PacketVector2Float32AvxMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32AvxMask Not(PacketVector2Float32AvxMask value) => ~value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32AvxMask operator &(PacketVector2Float32AvxMask left, PacketVector2Float32AvxMask right) => new(left.X & right.X, left.Y & right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32AvxMask operator |(PacketVector2Float32AvxMask left, PacketVector2Float32AvxMask right) => new(left.X | right.X, left.Y | right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32AvxMask operator ^(PacketVector2Float32AvxMask left, PacketVector2Float32AvxMask right) => new(left.X ^ right.X, left.Y ^ right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32AvxMask operator ~(PacketVector2Float32AvxMask value) => new(~value.X, ~value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32AvxMask operator ==(PacketVector2Float32AvxMask left, PacketVector2Float32AvxMask right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32AvxMask operator !=(PacketVector2Float32AvxMask left, PacketVector2Float32AvxMask right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2Float32AvxMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}
