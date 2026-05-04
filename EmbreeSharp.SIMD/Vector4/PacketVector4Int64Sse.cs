using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector4Int64Sse :
    ISimdIntegerVector4<PacketVector4Int64Sse, PacketInt64Sse, long, PacketVector4Int64SseMask, PacketInt64SseMask>
{
    public PacketVector4Int64Sse(PacketInt64Sse x, PacketInt64Sse y, PacketInt64Sse z, PacketInt64Sse w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketInt64Sse.LaneCount;

    public PacketInt64Sse X { get; }

    public PacketInt64Sse Y { get; }

    public PacketInt64Sse Z { get; }

    public PacketInt64Sse W { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Sse Create(PacketInt64Sse x, PacketInt64Sse y, PacketInt64Sse z, PacketInt64Sse w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Sse Broadcast(long value)
    {
        PacketInt64Sse packet = PacketInt64Sse.Broadcast(value);
        return new(packet, packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Sse Select(PacketVector4Int64SseMask mask, PacketVector4Int64Sse ifTrue, PacketVector4Int64Sse ifFalse)
    {
        return new(
            PacketInt64Sse.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt64Sse.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketInt64Sse.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketInt64Sse.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Sse Select(PacketInt64SseMask mask, PacketVector4Int64Sse ifTrue, PacketVector4Int64Sse ifFalse)
    {
        return new(
            PacketInt64Sse.Select(mask, ifTrue.X, ifFalse.X),
            PacketInt64Sse.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketInt64Sse.Select(mask, ifTrue.Z, ifFalse.Z),
            PacketInt64Sse.Select(mask, ifTrue.W, ifFalse.W));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Sse operator +(PacketVector4Int64Sse value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Sse operator +(PacketVector4Int64Sse left, PacketVector4Int64Sse right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Sse operator -(PacketVector4Int64Sse left, PacketVector4Int64Sse right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Sse operator *(PacketVector4Int64Sse left, PacketVector4Int64Sse right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64Sse operator -(PacketVector4Int64Sse value) => new(-value.X, -value.Y, -value.Z, -value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64SseMask operator ==(PacketVector4Int64Sse left, PacketVector4Int64Sse right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64SseMask operator !=(PacketVector4Int64Sse left, PacketVector4Int64Sse right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4Int64Sse other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct PacketVector4Int64SseMask :
    ISimdVector4Mask<PacketVector4Int64SseMask, PacketInt64SseMask>
{
    public PacketVector4Int64SseMask(PacketInt64SseMask x, PacketInt64SseMask y, PacketInt64SseMask z, PacketInt64SseMask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketInt64SseMask.LaneCount;

    public PacketInt64SseMask X { get; }

    public PacketInt64SseMask Y { get; }

    public PacketInt64SseMask Z { get; }

    public PacketInt64SseMask W { get; }

    public static PacketVector4Int64SseMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt64SseMask.True, PacketInt64SseMask.True, PacketInt64SseMask.True, PacketInt64SseMask.True);
    }

    public static PacketVector4Int64SseMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt64SseMask.False, PacketInt64SseMask.False, PacketInt64SseMask.False, PacketInt64SseMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64SseMask Create(PacketInt64SseMask x, PacketInt64SseMask y, PacketInt64SseMask z, PacketInt64SseMask w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64SseMask Broadcast(PacketInt64SseMask value) => new(value, value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask All(PacketVector4Int64SseMask value) => value.X & value.Y & value.Z & value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask Any(PacketVector4Int64SseMask value) => value.X | value.Y | value.Z | value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask None(PacketVector4Int64SseMask value) => ~(value.X | value.Y | value.Z | value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64SseMask AndNot(PacketVector4Int64SseMask left, PacketVector4Int64SseMask right)
    {
        return new(
            PacketInt64SseMask.AndNot(left.X, right.X),
            PacketInt64SseMask.AndNot(left.Y, right.Y),
            PacketInt64SseMask.AndNot(left.Z, right.Z),
            PacketInt64SseMask.AndNot(left.W, right.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64SseMask Select(PacketVector4Int64SseMask mask, PacketVector4Int64SseMask ifTrue, PacketVector4Int64SseMask ifFalse)
    {
        return new(
            PacketInt64SseMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt64SseMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketInt64SseMask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketInt64SseMask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64SseMask And(PacketVector4Int64SseMask left, PacketVector4Int64SseMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64SseMask Or(PacketVector4Int64SseMask left, PacketVector4Int64SseMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64SseMask Xor(PacketVector4Int64SseMask left, PacketVector4Int64SseMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64SseMask Not(PacketVector4Int64SseMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64SseMask operator &(PacketVector4Int64SseMask left, PacketVector4Int64SseMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64SseMask operator |(PacketVector4Int64SseMask left, PacketVector4Int64SseMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64SseMask operator ^(PacketVector4Int64SseMask left, PacketVector4Int64SseMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64SseMask operator ~(PacketVector4Int64SseMask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64SseMask operator ==(PacketVector4Int64SseMask left, PacketVector4Int64SseMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int64SseMask operator !=(PacketVector4Int64SseMask left, PacketVector4Int64SseMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4Int64SseMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}