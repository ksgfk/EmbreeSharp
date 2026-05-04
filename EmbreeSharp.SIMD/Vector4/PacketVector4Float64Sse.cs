using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector4Float64Sse :
    ISimdFloatingPointVector4<PacketVector4Float64Sse, PacketFloat64Sse, double, PacketVector4Float64SseMask, PacketFloat64SseMask>
{
    public PacketVector4Float64Sse(PacketFloat64Sse x, PacketFloat64Sse y, PacketFloat64Sse z, PacketFloat64Sse w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketFloat64Sse.LaneCount;

    public PacketFloat64Sse X { get; }

    public PacketFloat64Sse Y { get; }

    public PacketFloat64Sse Z { get; }

    public PacketFloat64Sse W { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Sse Create(PacketFloat64Sse x, PacketFloat64Sse y, PacketFloat64Sse z, PacketFloat64Sse w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Sse Broadcast(double value)
    {
        PacketFloat64Sse packet = PacketFloat64Sse.Broadcast(value);
        return new(packet, packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Sse Select(PacketVector4Float64SseMask mask, PacketVector4Float64Sse ifTrue, PacketVector4Float64Sse ifFalse)
    {
        return new(
            PacketFloat64Sse.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64Sse.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat64Sse.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat64Sse.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Sse Select(PacketFloat64SseMask mask, PacketVector4Float64Sse ifTrue, PacketVector4Float64Sse ifFalse)
    {
        return new(
            PacketFloat64Sse.Select(mask, ifTrue.X, ifFalse.X),
            PacketFloat64Sse.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketFloat64Sse.Select(mask, ifTrue.Z, ifFalse.Z),
            PacketFloat64Sse.Select(mask, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Sse Dot(PacketVector4Float64Sse left, PacketVector4Float64Sse right)
    {
        return PacketFloat64Sse.FusedMultiplyAdd(
            left.W,
            right.W,
            PacketFloat64Sse.FusedMultiplyAdd(left.Z, right.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Y, right.Y, left.X * right.X)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Sse operator +(PacketVector4Float64Sse value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Sse operator +(PacketVector4Float64Sse left, PacketVector4Float64Sse right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Sse operator -(PacketVector4Float64Sse left, PacketVector4Float64Sse right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Sse operator *(PacketVector4Float64Sse left, PacketVector4Float64Sse right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64Sse operator -(PacketVector4Float64Sse value) => new(-value.X, -value.Y, -value.Z, -value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64SseMask operator ==(PacketVector4Float64Sse left, PacketVector4Float64Sse right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64SseMask operator !=(PacketVector4Float64Sse left, PacketVector4Float64Sse right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4Float64Sse other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct PacketVector4Float64SseMask :
    ISimdVector4Mask<PacketVector4Float64SseMask, PacketFloat64SseMask>
{
    public PacketVector4Float64SseMask(PacketFloat64SseMask x, PacketFloat64SseMask y, PacketFloat64SseMask z, PacketFloat64SseMask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketFloat64SseMask.LaneCount;

    public PacketFloat64SseMask X { get; }

    public PacketFloat64SseMask Y { get; }

    public PacketFloat64SseMask Z { get; }

    public PacketFloat64SseMask W { get; }

    public static PacketVector4Float64SseMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat64SseMask.True, PacketFloat64SseMask.True, PacketFloat64SseMask.True, PacketFloat64SseMask.True);
    }

    public static PacketVector4Float64SseMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat64SseMask.False, PacketFloat64SseMask.False, PacketFloat64SseMask.False, PacketFloat64SseMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64SseMask Create(PacketFloat64SseMask x, PacketFloat64SseMask y, PacketFloat64SseMask z, PacketFloat64SseMask w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64SseMask Broadcast(PacketFloat64SseMask value) => new(value, value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask All(PacketVector4Float64SseMask value) => value.X & value.Y & value.Z & value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask Any(PacketVector4Float64SseMask value) => value.X | value.Y | value.Z | value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask None(PacketVector4Float64SseMask value) => ~(value.X | value.Y | value.Z | value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64SseMask AndNot(PacketVector4Float64SseMask left, PacketVector4Float64SseMask right)
    {
        return new(
            PacketFloat64SseMask.AndNot(left.X, right.X),
            PacketFloat64SseMask.AndNot(left.Y, right.Y),
            PacketFloat64SseMask.AndNot(left.Z, right.Z),
            PacketFloat64SseMask.AndNot(left.W, right.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64SseMask Select(PacketVector4Float64SseMask mask, PacketVector4Float64SseMask ifTrue, PacketVector4Float64SseMask ifFalse)
    {
        return new(
            PacketFloat64SseMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64SseMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat64SseMask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat64SseMask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64SseMask And(PacketVector4Float64SseMask left, PacketVector4Float64SseMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64SseMask Or(PacketVector4Float64SseMask left, PacketVector4Float64SseMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64SseMask Xor(PacketVector4Float64SseMask left, PacketVector4Float64SseMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64SseMask Not(PacketVector4Float64SseMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64SseMask operator &(PacketVector4Float64SseMask left, PacketVector4Float64SseMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64SseMask operator |(PacketVector4Float64SseMask left, PacketVector4Float64SseMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64SseMask operator ^(PacketVector4Float64SseMask left, PacketVector4Float64SseMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64SseMask operator ~(PacketVector4Float64SseMask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64SseMask operator ==(PacketVector4Float64SseMask left, PacketVector4Float64SseMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Float64SseMask operator !=(PacketVector4Float64SseMask left, PacketVector4Float64SseMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4Float64SseMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}