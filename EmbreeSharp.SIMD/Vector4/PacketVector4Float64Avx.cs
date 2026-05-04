using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector4Float64Avx :
    ISimdFloatingPointVector4<PacketVector4Float64Avx, PacketFloat64Avx, double, PacketVector4Float64AvxMask, PacketFloat64AvxMask>
{
    public PacketVector4Float64Avx(PacketFloat64Avx x, PacketFloat64Avx y, PacketFloat64Avx z, PacketFloat64Avx w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketFloat64Avx.LaneCount;

    public PacketFloat64Avx X { get; }

    public PacketFloat64Avx Y { get; }

    public PacketFloat64Avx Z { get; }

    public PacketFloat64Avx W { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Avx Create(PacketFloat64Avx x, PacketFloat64Avx y, PacketFloat64Avx z, PacketFloat64Avx w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Avx Broadcast(double value)
    {
        PacketFloat64Avx packet = PacketFloat64Avx.Broadcast(value);
        return new(packet, packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Avx Select(PacketVector4Float64AvxMask mask, PacketVector4Float64Avx ifTrue, PacketVector4Float64Avx ifFalse)
    {
        return new(
            PacketFloat64Avx.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64Avx.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat64Avx.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat64Avx.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Avx Select(PacketFloat64AvxMask mask, PacketVector4Float64Avx ifTrue, PacketVector4Float64Avx ifFalse)
    {
        return new(
            PacketFloat64Avx.Select(mask, ifTrue.X, ifFalse.X),
            PacketFloat64Avx.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketFloat64Avx.Select(mask, ifTrue.Z, ifFalse.Z),
            PacketFloat64Avx.Select(mask, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Avx Dot(PacketVector4Float64Avx left, PacketVector4Float64Avx right)
    {
        return PacketFloat64Avx.FusedMultiplyAdd(
            left.W,
            right.W,
            PacketFloat64Avx.FusedMultiplyAdd(left.Z, right.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Y, right.Y, left.X * right.X)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Avx operator +(PacketVector4Float64Avx value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Avx operator +(PacketVector4Float64Avx left, PacketVector4Float64Avx right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Avx operator -(PacketVector4Float64Avx left, PacketVector4Float64Avx right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Avx operator *(PacketVector4Float64Avx left, PacketVector4Float64Avx right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Avx operator -(PacketVector4Float64Avx value) => new(-value.X, -value.Y, -value.Z, -value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64AvxMask operator ==(PacketVector4Float64Avx left, PacketVector4Float64Avx right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64AvxMask operator !=(PacketVector4Float64Avx left, PacketVector4Float64Avx right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4Float64Avx other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct PacketVector4Float64AvxMask :
    ISimdVector4Mask<PacketVector4Float64AvxMask, PacketFloat64AvxMask>
{
    public PacketVector4Float64AvxMask(PacketFloat64AvxMask x, PacketFloat64AvxMask y, PacketFloat64AvxMask z, PacketFloat64AvxMask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketFloat64AvxMask.LaneCount;

    public PacketFloat64AvxMask X { get; }

    public PacketFloat64AvxMask Y { get; }

    public PacketFloat64AvxMask Z { get; }

    public PacketFloat64AvxMask W { get; }

    public static PacketVector4Float64AvxMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat64AvxMask.True, PacketFloat64AvxMask.True, PacketFloat64AvxMask.True, PacketFloat64AvxMask.True);
    }

    public static PacketVector4Float64AvxMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat64AvxMask.False, PacketFloat64AvxMask.False, PacketFloat64AvxMask.False, PacketFloat64AvxMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64AvxMask Create(PacketFloat64AvxMask x, PacketFloat64AvxMask y, PacketFloat64AvxMask z, PacketFloat64AvxMask w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64AvxMask Broadcast(PacketFloat64AvxMask value) => new(value, value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask All(PacketVector4Float64AvxMask value) => value.X & value.Y & value.Z & value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask Any(PacketVector4Float64AvxMask value) => value.X | value.Y | value.Z | value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64AvxMask None(PacketVector4Float64AvxMask value) => ~(value.X | value.Y | value.Z | value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64AvxMask AndNot(PacketVector4Float64AvxMask left, PacketVector4Float64AvxMask right)
    {
        return new(
            PacketFloat64AvxMask.AndNot(left.X, right.X),
            PacketFloat64AvxMask.AndNot(left.Y, right.Y),
            PacketFloat64AvxMask.AndNot(left.Z, right.Z),
            PacketFloat64AvxMask.AndNot(left.W, right.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64AvxMask Select(PacketVector4Float64AvxMask mask, PacketVector4Float64AvxMask ifTrue, PacketVector4Float64AvxMask ifFalse)
    {
        return new(
            PacketFloat64AvxMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64AvxMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat64AvxMask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat64AvxMask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64AvxMask And(PacketVector4Float64AvxMask left, PacketVector4Float64AvxMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64AvxMask Or(PacketVector4Float64AvxMask left, PacketVector4Float64AvxMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64AvxMask Xor(PacketVector4Float64AvxMask left, PacketVector4Float64AvxMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64AvxMask Not(PacketVector4Float64AvxMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64AvxMask operator &(PacketVector4Float64AvxMask left, PacketVector4Float64AvxMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64AvxMask operator |(PacketVector4Float64AvxMask left, PacketVector4Float64AvxMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64AvxMask operator ^(PacketVector4Float64AvxMask left, PacketVector4Float64AvxMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64AvxMask operator ~(PacketVector4Float64AvxMask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64AvxMask operator ==(PacketVector4Float64AvxMask left, PacketVector4Float64AvxMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64AvxMask operator !=(PacketVector4Float64AvxMask left, PacketVector4Float64AvxMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4Float64AvxMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}