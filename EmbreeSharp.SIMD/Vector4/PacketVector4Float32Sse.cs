using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector4Float32Sse :
    ISimdFloatingPointVector4<PacketVector4Float32Sse, PacketFloat32Sse, float, PacketVector4Float32SseMask, PacketFloat32SseMask>
{
    public PacketVector4Float32Sse(PacketFloat32Sse x, PacketFloat32Sse y, PacketFloat32Sse z, PacketFloat32Sse w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketFloat32Sse.LaneCount;

    public PacketFloat32Sse X { get; }

    public PacketFloat32Sse Y { get; }

    public PacketFloat32Sse Z { get; }

    public PacketFloat32Sse W { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Sse Create(PacketFloat32Sse x, PacketFloat32Sse y, PacketFloat32Sse z, PacketFloat32Sse w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Sse Broadcast(float value)
    {
        PacketFloat32Sse packet = PacketFloat32Sse.Broadcast(value);
        return new(packet, packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Sse Select(PacketVector4Float32SseMask mask, PacketVector4Float32Sse ifTrue, PacketVector4Float32Sse ifFalse)
    {
        return new(
            PacketFloat32Sse.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32Sse.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat32Sse.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat32Sse.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Sse Select(PacketFloat32SseMask mask, PacketVector4Float32Sse ifTrue, PacketVector4Float32Sse ifFalse)
    {
        return new(
            PacketFloat32Sse.Select(mask, ifTrue.X, ifFalse.X),
            PacketFloat32Sse.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketFloat32Sse.Select(mask, ifTrue.Z, ifFalse.Z),
            PacketFloat32Sse.Select(mask, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Sse Dot(PacketVector4Float32Sse left, PacketVector4Float32Sse right)
    {
        return PacketFloat32Sse.FusedMultiplyAdd(
            left.W,
            right.W,
            PacketFloat32Sse.FusedMultiplyAdd(left.Z, right.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Y, right.Y, left.X * right.X)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Sse operator +(PacketVector4Float32Sse value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Sse operator +(PacketVector4Float32Sse left, PacketVector4Float32Sse right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Sse operator -(PacketVector4Float32Sse left, PacketVector4Float32Sse right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Sse operator *(PacketVector4Float32Sse left, PacketVector4Float32Sse right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32Sse operator -(PacketVector4Float32Sse value) => new(-value.X, -value.Y, -value.Z, -value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32SseMask operator ==(PacketVector4Float32Sse left, PacketVector4Float32Sse right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32SseMask operator !=(PacketVector4Float32Sse left, PacketVector4Float32Sse right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4Float32Sse other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct PacketVector4Float32SseMask :
    ISimdVector4Mask<PacketVector4Float32SseMask, PacketFloat32SseMask>
{
    public PacketVector4Float32SseMask(PacketFloat32SseMask x, PacketFloat32SseMask y, PacketFloat32SseMask z, PacketFloat32SseMask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketFloat32SseMask.LaneCount;

    public PacketFloat32SseMask X { get; }

    public PacketFloat32SseMask Y { get; }

    public PacketFloat32SseMask Z { get; }

    public PacketFloat32SseMask W { get; }

    public static PacketVector4Float32SseMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat32SseMask.True, PacketFloat32SseMask.True, PacketFloat32SseMask.True, PacketFloat32SseMask.True);
    }

    public static PacketVector4Float32SseMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat32SseMask.False, PacketFloat32SseMask.False, PacketFloat32SseMask.False, PacketFloat32SseMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32SseMask Create(PacketFloat32SseMask x, PacketFloat32SseMask y, PacketFloat32SseMask z, PacketFloat32SseMask w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32SseMask Broadcast(PacketFloat32SseMask value) => new(value, value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask All(PacketVector4Float32SseMask value) => value.X & value.Y & value.Z & value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask Any(PacketVector4Float32SseMask value) => value.X | value.Y | value.Z | value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask None(PacketVector4Float32SseMask value) => ~(value.X | value.Y | value.Z | value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32SseMask AndNot(PacketVector4Float32SseMask left, PacketVector4Float32SseMask right)
    {
        return new(
            PacketFloat32SseMask.AndNot(left.X, right.X),
            PacketFloat32SseMask.AndNot(left.Y, right.Y),
            PacketFloat32SseMask.AndNot(left.Z, right.Z),
            PacketFloat32SseMask.AndNot(left.W, right.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32SseMask Select(PacketVector4Float32SseMask mask, PacketVector4Float32SseMask ifTrue, PacketVector4Float32SseMask ifFalse)
    {
        return new(
            PacketFloat32SseMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32SseMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat32SseMask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat32SseMask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32SseMask And(PacketVector4Float32SseMask left, PacketVector4Float32SseMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32SseMask Or(PacketVector4Float32SseMask left, PacketVector4Float32SseMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32SseMask Xor(PacketVector4Float32SseMask left, PacketVector4Float32SseMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32SseMask Not(PacketVector4Float32SseMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32SseMask operator &(PacketVector4Float32SseMask left, PacketVector4Float32SseMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32SseMask operator |(PacketVector4Float32SseMask left, PacketVector4Float32SseMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32SseMask operator ^(PacketVector4Float32SseMask left, PacketVector4Float32SseMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32SseMask operator ~(PacketVector4Float32SseMask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32SseMask operator ==(PacketVector4Float32SseMask left, PacketVector4Float32SseMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float32SseMask operator !=(PacketVector4Float32SseMask left, PacketVector4Float32SseMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4Float32SseMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}