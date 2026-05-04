using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector4Int32Sse :
    ISimdIntegerVector4<PacketVector4Int32Sse, PacketInt32Sse, int, PacketVector4Int32SseMask, PacketInt32SseMask>
{
    public PacketVector4Int32Sse(PacketInt32Sse x, PacketInt32Sse y, PacketInt32Sse z, PacketInt32Sse w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketInt32Sse.LaneCount;

    public PacketInt32Sse X { get; }

    public PacketInt32Sse Y { get; }

    public PacketInt32Sse Z { get; }

    public PacketInt32Sse W { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Sse Create(PacketInt32Sse x, PacketInt32Sse y, PacketInt32Sse z, PacketInt32Sse w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Sse Broadcast(int value)
    {
        PacketInt32Sse packet = PacketInt32Sse.Broadcast(value);
        return new(packet, packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Sse Select(PacketVector4Int32SseMask mask, PacketVector4Int32Sse ifTrue, PacketVector4Int32Sse ifFalse)
    {
        return new(
            PacketInt32Sse.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt32Sse.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketInt32Sse.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketInt32Sse.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Sse Select(PacketInt32SseMask mask, PacketVector4Int32Sse ifTrue, PacketVector4Int32Sse ifFalse)
    {
        return new(
            PacketInt32Sse.Select(mask, ifTrue.X, ifFalse.X),
            PacketInt32Sse.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketInt32Sse.Select(mask, ifTrue.Z, ifFalse.Z),
            PacketInt32Sse.Select(mask, ifTrue.W, ifFalse.W));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Sse operator +(PacketVector4Int32Sse value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Sse operator +(PacketVector4Int32Sse left, PacketVector4Int32Sse right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Sse operator -(PacketVector4Int32Sse left, PacketVector4Int32Sse right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Sse operator *(PacketVector4Int32Sse left, PacketVector4Int32Sse right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32Sse operator -(PacketVector4Int32Sse value) => new(-value.X, -value.Y, -value.Z, -value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32SseMask operator ==(PacketVector4Int32Sse left, PacketVector4Int32Sse right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32SseMask operator !=(PacketVector4Int32Sse left, PacketVector4Int32Sse right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4Int32Sse other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct PacketVector4Int32SseMask :
    ISimdVector4Mask<PacketVector4Int32SseMask, PacketInt32SseMask>
{
    public PacketVector4Int32SseMask(PacketInt32SseMask x, PacketInt32SseMask y, PacketInt32SseMask z, PacketInt32SseMask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketInt32SseMask.LaneCount;

    public PacketInt32SseMask X { get; }

    public PacketInt32SseMask Y { get; }

    public PacketInt32SseMask Z { get; }

    public PacketInt32SseMask W { get; }

    public static PacketVector4Int32SseMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt32SseMask.True, PacketInt32SseMask.True, PacketInt32SseMask.True, PacketInt32SseMask.True);
    }

    public static PacketVector4Int32SseMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt32SseMask.False, PacketInt32SseMask.False, PacketInt32SseMask.False, PacketInt32SseMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32SseMask Create(PacketInt32SseMask x, PacketInt32SseMask y, PacketInt32SseMask z, PacketInt32SseMask w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32SseMask Broadcast(PacketInt32SseMask value) => new(value, value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask All(PacketVector4Int32SseMask value) => value.X & value.Y & value.Z & value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask Any(PacketVector4Int32SseMask value) => value.X | value.Y | value.Z | value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask None(PacketVector4Int32SseMask value) => ~(value.X | value.Y | value.Z | value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32SseMask AndNot(PacketVector4Int32SseMask left, PacketVector4Int32SseMask right)
    {
        return new(
            PacketInt32SseMask.AndNot(left.X, right.X),
            PacketInt32SseMask.AndNot(left.Y, right.Y),
            PacketInt32SseMask.AndNot(left.Z, right.Z),
            PacketInt32SseMask.AndNot(left.W, right.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32SseMask Select(PacketVector4Int32SseMask mask, PacketVector4Int32SseMask ifTrue, PacketVector4Int32SseMask ifFalse)
    {
        return new(
            PacketInt32SseMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt32SseMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketInt32SseMask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketInt32SseMask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32SseMask And(PacketVector4Int32SseMask left, PacketVector4Int32SseMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32SseMask Or(PacketVector4Int32SseMask left, PacketVector4Int32SseMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32SseMask Xor(PacketVector4Int32SseMask left, PacketVector4Int32SseMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32SseMask Not(PacketVector4Int32SseMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32SseMask operator &(PacketVector4Int32SseMask left, PacketVector4Int32SseMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32SseMask operator |(PacketVector4Int32SseMask left, PacketVector4Int32SseMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32SseMask operator ^(PacketVector4Int32SseMask left, PacketVector4Int32SseMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32SseMask operator ~(PacketVector4Int32SseMask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32SseMask operator ==(PacketVector4Int32SseMask left, PacketVector4Int32SseMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4Int32SseMask operator !=(PacketVector4Int32SseMask left, PacketVector4Int32SseMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4Int32SseMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}