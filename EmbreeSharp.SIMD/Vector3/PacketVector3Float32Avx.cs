using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly partial struct PacketVector3Float32Avx :
    ISimdFloatingPointVector3<PacketVector3Float32Avx, PacketFloat32Avx, float, PacketVector3Float32AvxMask, PacketFloat32AvxMask>
{
    public PacketVector3Float32Avx(PacketFloat32Avx x, PacketFloat32Avx y, PacketFloat32Avx z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketFloat32Avx.LaneCount;

    public PacketFloat32Avx X { get; }

    public PacketFloat32Avx Y { get; }

    public PacketFloat32Avx Z { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Avx Create(PacketFloat32Avx x, PacketFloat32Avx y, PacketFloat32Avx z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Avx Broadcast(float value)
    {
        PacketFloat32Avx packet = PacketFloat32Avx.Broadcast(value);
        return new(packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Avx Select(PacketVector3Float32AvxMask mask, PacketVector3Float32Avx ifTrue, PacketVector3Float32Avx ifFalse)
    {
        return new(
            PacketFloat32Avx.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32Avx.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat32Avx.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Avx Select(PacketFloat32AvxMask mask, PacketVector3Float32Avx ifTrue, PacketVector3Float32Avx ifFalse)
    {
        return new(
            PacketFloat32Avx.Select(mask, ifTrue.X, ifFalse.X),
            PacketFloat32Avx.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketFloat32Avx.Select(mask, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Avx Dot(PacketVector3Float32Avx left, PacketVector3Float32Avx right)
    {
        return PacketFloat32Avx.FusedMultiplyAdd(
            left.Z,
            right.Z,
            PacketFloat32Avx.FusedMultiplyAdd(left.Y, right.Y, left.X * right.X));
    }

    public static partial PacketVector3Float32Avx Cross(PacketVector3Float32Avx left, PacketVector3Float32Avx right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Avx operator +(PacketVector3Float32Avx value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Avx operator +(PacketVector3Float32Avx left, PacketVector3Float32Avx right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Avx operator -(PacketVector3Float32Avx left, PacketVector3Float32Avx right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Avx operator *(PacketVector3Float32Avx left, PacketVector3Float32Avx right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Avx operator -(PacketVector3Float32Avx value) => new(-value.X, -value.Y, -value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32AvxMask operator ==(PacketVector3Float32Avx left, PacketVector3Float32Avx right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32AvxMask operator !=(PacketVector3Float32Avx left, PacketVector3Float32Avx right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3Float32Avx other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}

public readonly struct PacketVector3Float32AvxMask :
    ISimdVector3Mask<PacketVector3Float32AvxMask, PacketFloat32AvxMask>
{
    public PacketVector3Float32AvxMask(PacketFloat32AvxMask x, PacketFloat32AvxMask y, PacketFloat32AvxMask z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketFloat32AvxMask.LaneCount;

    public PacketFloat32AvxMask X { get; }

    public PacketFloat32AvxMask Y { get; }

    public PacketFloat32AvxMask Z { get; }

    public static PacketVector3Float32AvxMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat32AvxMask.True, PacketFloat32AvxMask.True, PacketFloat32AvxMask.True);
    }

    public static PacketVector3Float32AvxMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat32AvxMask.False, PacketFloat32AvxMask.False, PacketFloat32AvxMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32AvxMask Create(PacketFloat32AvxMask x, PacketFloat32AvxMask y, PacketFloat32AvxMask z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32AvxMask Broadcast(PacketFloat32AvxMask value) => new(value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask All(PacketVector3Float32AvxMask value) => value.X & value.Y & value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask Any(PacketVector3Float32AvxMask value) => value.X | value.Y | value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask None(PacketVector3Float32AvxMask value) => ~(value.X | value.Y | value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32AvxMask AndNot(PacketVector3Float32AvxMask left, PacketVector3Float32AvxMask right)
    {
        return new(
            PacketFloat32AvxMask.AndNot(left.X, right.X),
            PacketFloat32AvxMask.AndNot(left.Y, right.Y),
            PacketFloat32AvxMask.AndNot(left.Z, right.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32AvxMask Select(PacketVector3Float32AvxMask mask, PacketVector3Float32AvxMask ifTrue, PacketVector3Float32AvxMask ifFalse)
    {
        return new(
            PacketFloat32AvxMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32AvxMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat32AvxMask.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32AvxMask And(PacketVector3Float32AvxMask left, PacketVector3Float32AvxMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32AvxMask Or(PacketVector3Float32AvxMask left, PacketVector3Float32AvxMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32AvxMask Xor(PacketVector3Float32AvxMask left, PacketVector3Float32AvxMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32AvxMask Not(PacketVector3Float32AvxMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32AvxMask operator &(PacketVector3Float32AvxMask left, PacketVector3Float32AvxMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32AvxMask operator |(PacketVector3Float32AvxMask left, PacketVector3Float32AvxMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32AvxMask operator ^(PacketVector3Float32AvxMask left, PacketVector3Float32AvxMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32AvxMask operator ~(PacketVector3Float32AvxMask value) => new(~value.X, ~value.Y, ~value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32AvxMask operator ==(PacketVector3Float32AvxMask left, PacketVector3Float32AvxMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32AvxMask operator !=(PacketVector3Float32AvxMask left, PacketVector3Float32AvxMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3Float32AvxMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}
