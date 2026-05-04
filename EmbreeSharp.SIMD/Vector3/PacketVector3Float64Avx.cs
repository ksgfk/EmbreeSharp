using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly partial struct PacketVector3Float64Avx :
    ISimdFloatingPointVector3<PacketVector3Float64Avx, PacketFloat64Avx, double, PacketVector3Float64AvxMask, PacketFloat64AvxMask>
{
    public PacketVector3Float64Avx(PacketFloat64Avx x, PacketFloat64Avx y, PacketFloat64Avx z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketFloat64Avx.LaneCount;

    public PacketFloat64Avx X { get; }

    public PacketFloat64Avx Y { get; }

    public PacketFloat64Avx Z { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Avx Create(PacketFloat64Avx x, PacketFloat64Avx y, PacketFloat64Avx z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Avx Broadcast(double value)
    {
        PacketFloat64Avx packet = PacketFloat64Avx.Broadcast(value);
        return new(packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Avx Select(PacketVector3Float64AvxMask mask, PacketVector3Float64Avx ifTrue, PacketVector3Float64Avx ifFalse)
    {
        return new(
            PacketFloat64Avx.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64Avx.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat64Avx.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Avx Select(PacketFloat64AvxMask mask, PacketVector3Float64Avx ifTrue, PacketVector3Float64Avx ifFalse)
    {
        return new(
            PacketFloat64Avx.Select(mask, ifTrue.X, ifFalse.X),
            PacketFloat64Avx.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketFloat64Avx.Select(mask, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Avx Dot(PacketVector3Float64Avx left, PacketVector3Float64Avx right)
    {
        return PacketFloat64Avx.FusedMultiplyAdd(
            left.Z,
            right.Z,
            PacketFloat64Avx.FusedMultiplyAdd(left.Y, right.Y, left.X * right.X));
    }

    public static partial PacketVector3Float64Avx Cross(PacketVector3Float64Avx left, PacketVector3Float64Avx right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Avx operator +(PacketVector3Float64Avx value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Avx operator +(PacketVector3Float64Avx left, PacketVector3Float64Avx right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Avx operator -(PacketVector3Float64Avx left, PacketVector3Float64Avx right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Avx operator *(PacketVector3Float64Avx left, PacketVector3Float64Avx right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Avx operator -(PacketVector3Float64Avx value) => new(-value.X, -value.Y, -value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64AvxMask operator ==(PacketVector3Float64Avx left, PacketVector3Float64Avx right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64AvxMask operator !=(PacketVector3Float64Avx left, PacketVector3Float64Avx right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3Float64Avx other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}

public readonly struct PacketVector3Float64AvxMask :
    ISimdVector3Mask<PacketVector3Float64AvxMask, PacketFloat64AvxMask>
{
    public PacketVector3Float64AvxMask(PacketFloat64AvxMask x, PacketFloat64AvxMask y, PacketFloat64AvxMask z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketFloat64AvxMask.LaneCount;

    public PacketFloat64AvxMask X { get; }

    public PacketFloat64AvxMask Y { get; }

    public PacketFloat64AvxMask Z { get; }

    public static PacketVector3Float64AvxMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat64AvxMask.True, PacketFloat64AvxMask.True, PacketFloat64AvxMask.True);
    }

    public static PacketVector3Float64AvxMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat64AvxMask.False, PacketFloat64AvxMask.False, PacketFloat64AvxMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64AvxMask Create(PacketFloat64AvxMask x, PacketFloat64AvxMask y, PacketFloat64AvxMask z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64AvxMask Broadcast(PacketFloat64AvxMask value) => new(value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask All(PacketVector3Float64AvxMask value) => value.X & value.Y & value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask Any(PacketVector3Float64AvxMask value) => value.X | value.Y | value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask None(PacketVector3Float64AvxMask value) => ~(value.X | value.Y | value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64AvxMask AndNot(PacketVector3Float64AvxMask left, PacketVector3Float64AvxMask right)
    {
        return new(
            PacketFloat64AvxMask.AndNot(left.X, right.X),
            PacketFloat64AvxMask.AndNot(left.Y, right.Y),
            PacketFloat64AvxMask.AndNot(left.Z, right.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64AvxMask Select(PacketVector3Float64AvxMask mask, PacketVector3Float64AvxMask ifTrue, PacketVector3Float64AvxMask ifFalse)
    {
        return new(
            PacketFloat64AvxMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64AvxMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat64AvxMask.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64AvxMask And(PacketVector3Float64AvxMask left, PacketVector3Float64AvxMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64AvxMask Or(PacketVector3Float64AvxMask left, PacketVector3Float64AvxMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64AvxMask Xor(PacketVector3Float64AvxMask left, PacketVector3Float64AvxMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64AvxMask Not(PacketVector3Float64AvxMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64AvxMask operator &(PacketVector3Float64AvxMask left, PacketVector3Float64AvxMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64AvxMask operator |(PacketVector3Float64AvxMask left, PacketVector3Float64AvxMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64AvxMask operator ^(PacketVector3Float64AvxMask left, PacketVector3Float64AvxMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64AvxMask operator ~(PacketVector3Float64AvxMask value) => new(~value.X, ~value.Y, ~value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64AvxMask operator ==(PacketVector3Float64AvxMask left, PacketVector3Float64AvxMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64AvxMask operator !=(PacketVector3Float64AvxMask left, PacketVector3Float64AvxMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3Float64AvxMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}
