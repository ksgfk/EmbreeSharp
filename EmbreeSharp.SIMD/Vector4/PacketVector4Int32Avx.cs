using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector4Int32Avx :
    ISimdIntegerVector4<PacketVector4Int32Avx, PacketInt32Avx, int, PacketVector4Int32AvxMask, PacketInt32AvxMask>
{
    public PacketVector4Int32Avx(PacketInt32Avx x, PacketInt32Avx y, PacketInt32Avx z, PacketInt32Avx w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketInt32Avx.LaneCount;

    public PacketInt32Avx X { get; }

    public PacketInt32Avx Y { get; }

    public PacketInt32Avx Z { get; }

    public PacketInt32Avx W { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Avx Create(PacketInt32Avx x, PacketInt32Avx y, PacketInt32Avx z, PacketInt32Avx w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Avx Broadcast(int value)
    {
        PacketInt32Avx packet = PacketInt32Avx.Broadcast(value);
        return new(packet, packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Avx Select(PacketVector4Int32AvxMask mask, PacketVector4Int32Avx ifTrue, PacketVector4Int32Avx ifFalse)
    {
        return new(
            PacketInt32Avx.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt32Avx.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketInt32Avx.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketInt32Avx.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Avx Select(PacketInt32AvxMask mask, PacketVector4Int32Avx ifTrue, PacketVector4Int32Avx ifFalse)
    {
        return new(
            PacketInt32Avx.Select(mask, ifTrue.X, ifFalse.X),
            PacketInt32Avx.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketInt32Avx.Select(mask, ifTrue.Z, ifFalse.Z),
            PacketInt32Avx.Select(mask, ifTrue.W, ifFalse.W));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Avx operator +(PacketVector4Int32Avx value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Avx operator +(PacketVector4Int32Avx left, PacketVector4Int32Avx right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Avx operator -(PacketVector4Int32Avx left, PacketVector4Int32Avx right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Avx operator *(PacketVector4Int32Avx left, PacketVector4Int32Avx right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Avx operator -(PacketVector4Int32Avx value) => new(-value.X, -value.Y, -value.Z, -value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32AvxMask operator ==(PacketVector4Int32Avx left, PacketVector4Int32Avx right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32AvxMask operator !=(PacketVector4Int32Avx left, PacketVector4Int32Avx right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4Int32Avx other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct PacketVector4Int32AvxMask :
    ISimdVector4Mask<PacketVector4Int32AvxMask, PacketInt32AvxMask>
{
    public PacketVector4Int32AvxMask(PacketInt32AvxMask x, PacketInt32AvxMask y, PacketInt32AvxMask z, PacketInt32AvxMask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketInt32AvxMask.LaneCount;

    public PacketInt32AvxMask X { get; }

    public PacketInt32AvxMask Y { get; }

    public PacketInt32AvxMask Z { get; }

    public PacketInt32AvxMask W { get; }

    public static PacketVector4Int32AvxMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt32AvxMask.True, PacketInt32AvxMask.True, PacketInt32AvxMask.True, PacketInt32AvxMask.True);
    }

    public static PacketVector4Int32AvxMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt32AvxMask.False, PacketInt32AvxMask.False, PacketInt32AvxMask.False, PacketInt32AvxMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32AvxMask Create(PacketInt32AvxMask x, PacketInt32AvxMask y, PacketInt32AvxMask z, PacketInt32AvxMask w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32AvxMask Broadcast(PacketInt32AvxMask value) => new(value, value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask All(PacketVector4Int32AvxMask value) => value.X & value.Y & value.Z & value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask Any(PacketVector4Int32AvxMask value) => value.X | value.Y | value.Z | value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32AvxMask None(PacketVector4Int32AvxMask value) => ~(value.X | value.Y | value.Z | value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32AvxMask AndNot(PacketVector4Int32AvxMask left, PacketVector4Int32AvxMask right)
    {
        return new(
            PacketInt32AvxMask.AndNot(left.X, right.X),
            PacketInt32AvxMask.AndNot(left.Y, right.Y),
            PacketInt32AvxMask.AndNot(left.Z, right.Z),
            PacketInt32AvxMask.AndNot(left.W, right.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32AvxMask Select(PacketVector4Int32AvxMask mask, PacketVector4Int32AvxMask ifTrue, PacketVector4Int32AvxMask ifFalse)
    {
        return new(
            PacketInt32AvxMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt32AvxMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketInt32AvxMask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketInt32AvxMask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32AvxMask And(PacketVector4Int32AvxMask left, PacketVector4Int32AvxMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32AvxMask Or(PacketVector4Int32AvxMask left, PacketVector4Int32AvxMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32AvxMask Xor(PacketVector4Int32AvxMask left, PacketVector4Int32AvxMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32AvxMask Not(PacketVector4Int32AvxMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32AvxMask operator &(PacketVector4Int32AvxMask left, PacketVector4Int32AvxMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32AvxMask operator |(PacketVector4Int32AvxMask left, PacketVector4Int32AvxMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32AvxMask operator ^(PacketVector4Int32AvxMask left, PacketVector4Int32AvxMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32AvxMask operator ~(PacketVector4Int32AvxMask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32AvxMask operator ==(PacketVector4Int32AvxMask left, PacketVector4Int32AvxMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32AvxMask operator !=(PacketVector4Int32AvxMask left, PacketVector4Int32AvxMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4Int32AvxMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}