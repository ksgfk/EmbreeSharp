using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector4Float32Avx :
    ISimdFloatingPointVector4<PacketVector4Float32Avx, PacketFloat32Avx, float, PacketVector4Float32AvxMask, PacketFloat32AvxMask>
{
    public PacketVector4Float32Avx(PacketFloat32Avx x, PacketFloat32Avx y, PacketFloat32Avx z, PacketFloat32Avx w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketFloat32Avx.LaneCount;

    public PacketFloat32Avx X { get; }

    public PacketFloat32Avx Y { get; }

    public PacketFloat32Avx Z { get; }

    public PacketFloat32Avx W { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Avx Create(PacketFloat32Avx x, PacketFloat32Avx y, PacketFloat32Avx z, PacketFloat32Avx w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Avx Broadcast(float value)
    {
        PacketFloat32Avx packet = PacketFloat32Avx.Broadcast(value);
        return new(packet, packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Avx Select(PacketVector4Float32AvxMask mask, PacketVector4Float32Avx ifTrue, PacketVector4Float32Avx ifFalse)
    {
        return new(
            PacketFloat32Avx.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32Avx.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat32Avx.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat32Avx.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Avx Select(PacketFloat32AvxMask mask, PacketVector4Float32Avx ifTrue, PacketVector4Float32Avx ifFalse)
    {
        return new(
            PacketFloat32Avx.Select(mask, ifTrue.X, ifFalse.X),
            PacketFloat32Avx.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketFloat32Avx.Select(mask, ifTrue.Z, ifFalse.Z),
            PacketFloat32Avx.Select(mask, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Avx Dot(PacketVector4Float32Avx left, PacketVector4Float32Avx right)
    {
        return PacketFloat32Avx.FusedMultiplyAdd(
            left.W,
            right.W,
            PacketFloat32Avx.FusedMultiplyAdd(left.Z, right.Z, PacketFloat32Avx.FusedMultiplyAdd(left.Y, right.Y, left.X * right.X)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Avx operator +(PacketVector4Float32Avx value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Avx operator +(PacketVector4Float32Avx left, PacketVector4Float32Avx right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Avx operator -(PacketVector4Float32Avx left, PacketVector4Float32Avx right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Avx operator *(PacketVector4Float32Avx left, PacketVector4Float32Avx right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Avx operator -(PacketVector4Float32Avx value) => new(-value.X, -value.Y, -value.Z, -value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32AvxMask operator ==(PacketVector4Float32Avx left, PacketVector4Float32Avx right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32AvxMask operator !=(PacketVector4Float32Avx left, PacketVector4Float32Avx right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4Float32Avx other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct PacketVector4Float32AvxMask :
    ISimdVector4Mask<PacketVector4Float32AvxMask, PacketFloat32AvxMask>
{
    public PacketVector4Float32AvxMask(PacketFloat32AvxMask x, PacketFloat32AvxMask y, PacketFloat32AvxMask z, PacketFloat32AvxMask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketFloat32AvxMask.LaneCount;

    public PacketFloat32AvxMask X { get; }

    public PacketFloat32AvxMask Y { get; }

    public PacketFloat32AvxMask Z { get; }

    public PacketFloat32AvxMask W { get; }

    public static PacketVector4Float32AvxMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat32AvxMask.True, PacketFloat32AvxMask.True, PacketFloat32AvxMask.True, PacketFloat32AvxMask.True);
    }

    public static PacketVector4Float32AvxMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat32AvxMask.False, PacketFloat32AvxMask.False, PacketFloat32AvxMask.False, PacketFloat32AvxMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32AvxMask Create(PacketFloat32AvxMask x, PacketFloat32AvxMask y, PacketFloat32AvxMask z, PacketFloat32AvxMask w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32AvxMask Broadcast(PacketFloat32AvxMask value) => new(value, value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask All(PacketVector4Float32AvxMask value) => value.X & value.Y & value.Z & value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask Any(PacketVector4Float32AvxMask value) => value.X | value.Y | value.Z | value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32AvxMask None(PacketVector4Float32AvxMask value) => ~(value.X | value.Y | value.Z | value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32AvxMask AndNot(PacketVector4Float32AvxMask left, PacketVector4Float32AvxMask right)
    {
        return new(
            PacketFloat32AvxMask.AndNot(left.X, right.X),
            PacketFloat32AvxMask.AndNot(left.Y, right.Y),
            PacketFloat32AvxMask.AndNot(left.Z, right.Z),
            PacketFloat32AvxMask.AndNot(left.W, right.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32AvxMask Select(PacketVector4Float32AvxMask mask, PacketVector4Float32AvxMask ifTrue, PacketVector4Float32AvxMask ifFalse)
    {
        return new(
            PacketFloat32AvxMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32AvxMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat32AvxMask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat32AvxMask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32AvxMask And(PacketVector4Float32AvxMask left, PacketVector4Float32AvxMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32AvxMask Or(PacketVector4Float32AvxMask left, PacketVector4Float32AvxMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32AvxMask Xor(PacketVector4Float32AvxMask left, PacketVector4Float32AvxMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32AvxMask Not(PacketVector4Float32AvxMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32AvxMask operator &(PacketVector4Float32AvxMask left, PacketVector4Float32AvxMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32AvxMask operator |(PacketVector4Float32AvxMask left, PacketVector4Float32AvxMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32AvxMask operator ^(PacketVector4Float32AvxMask left, PacketVector4Float32AvxMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32AvxMask operator ~(PacketVector4Float32AvxMask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32AvxMask operator ==(PacketVector4Float32AvxMask left, PacketVector4Float32AvxMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32AvxMask operator !=(PacketVector4Float32AvxMask left, PacketVector4Float32AvxMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4Float32AvxMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}