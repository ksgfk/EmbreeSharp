using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector4UInt32Sse :
    ISimdIntegerVector4<PacketVector4UInt32Sse, PacketUInt32Sse, uint, PacketVector4UInt32SseMask, PacketUInt32SseMask>
{
    public PacketVector4UInt32Sse(PacketUInt32Sse x, PacketUInt32Sse y, PacketUInt32Sse z, PacketUInt32Sse w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketUInt32Sse.LaneCount;

    public PacketUInt32Sse X { get; }

    public PacketUInt32Sse Y { get; }

    public PacketUInt32Sse Z { get; }

    public PacketUInt32Sse W { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Sse Create(PacketUInt32Sse x, PacketUInt32Sse y, PacketUInt32Sse z, PacketUInt32Sse w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Sse Broadcast(uint value)
    {
        PacketUInt32Sse packet = PacketUInt32Sse.Broadcast(value);
        return new(packet, packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Sse Select(PacketVector4UInt32SseMask mask, PacketVector4UInt32Sse ifTrue, PacketVector4UInt32Sse ifFalse)
    {
        return new(
            PacketUInt32Sse.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt32Sse.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketUInt32Sse.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketUInt32Sse.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Sse Select(PacketUInt32SseMask mask, PacketVector4UInt32Sse ifTrue, PacketVector4UInt32Sse ifFalse)
    {
        return new(
            PacketUInt32Sse.Select(mask, ifTrue.X, ifFalse.X),
            PacketUInt32Sse.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketUInt32Sse.Select(mask, ifTrue.Z, ifFalse.Z),
            PacketUInt32Sse.Select(mask, ifTrue.W, ifFalse.W));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Sse operator +(PacketVector4UInt32Sse value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Sse operator +(PacketVector4UInt32Sse left, PacketVector4UInt32Sse right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Sse operator -(PacketVector4UInt32Sse left, PacketVector4UInt32Sse right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Sse operator *(PacketVector4UInt32Sse left, PacketVector4UInt32Sse right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32Sse operator -(PacketVector4UInt32Sse value) => new(-value.X, -value.Y, -value.Z, -value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32SseMask operator ==(PacketVector4UInt32Sse left, PacketVector4UInt32Sse right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32SseMask operator !=(PacketVector4UInt32Sse left, PacketVector4UInt32Sse right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4UInt32Sse other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct PacketVector4UInt32SseMask :
    ISimdVector4Mask<PacketVector4UInt32SseMask, PacketUInt32SseMask>
{
    public PacketVector4UInt32SseMask(PacketUInt32SseMask x, PacketUInt32SseMask y, PacketUInt32SseMask z, PacketUInt32SseMask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketUInt32SseMask.LaneCount;

    public PacketUInt32SseMask X { get; }

    public PacketUInt32SseMask Y { get; }

    public PacketUInt32SseMask Z { get; }

    public PacketUInt32SseMask W { get; }

    public static PacketVector4UInt32SseMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt32SseMask.True, PacketUInt32SseMask.True, PacketUInt32SseMask.True, PacketUInt32SseMask.True);
    }

    public static PacketVector4UInt32SseMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt32SseMask.False, PacketUInt32SseMask.False, PacketUInt32SseMask.False, PacketUInt32SseMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32SseMask Create(PacketUInt32SseMask x, PacketUInt32SseMask y, PacketUInt32SseMask z, PacketUInt32SseMask w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32SseMask Broadcast(PacketUInt32SseMask value) => new(value, value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask All(PacketVector4UInt32SseMask value) => value.X & value.Y & value.Z & value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask Any(PacketVector4UInt32SseMask value) => value.X | value.Y | value.Z | value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask None(PacketVector4UInt32SseMask value) => ~(value.X | value.Y | value.Z | value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32SseMask AndNot(PacketVector4UInt32SseMask left, PacketVector4UInt32SseMask right)
    {
        return new(
            PacketUInt32SseMask.AndNot(left.X, right.X),
            PacketUInt32SseMask.AndNot(left.Y, right.Y),
            PacketUInt32SseMask.AndNot(left.Z, right.Z),
            PacketUInt32SseMask.AndNot(left.W, right.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32SseMask Select(PacketVector4UInt32SseMask mask, PacketVector4UInt32SseMask ifTrue, PacketVector4UInt32SseMask ifFalse)
    {
        return new(
            PacketUInt32SseMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt32SseMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketUInt32SseMask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketUInt32SseMask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32SseMask And(PacketVector4UInt32SseMask left, PacketVector4UInt32SseMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32SseMask Or(PacketVector4UInt32SseMask left, PacketVector4UInt32SseMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32SseMask Xor(PacketVector4UInt32SseMask left, PacketVector4UInt32SseMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32SseMask Not(PacketVector4UInt32SseMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32SseMask operator &(PacketVector4UInt32SseMask left, PacketVector4UInt32SseMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32SseMask operator |(PacketVector4UInt32SseMask left, PacketVector4UInt32SseMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32SseMask operator ^(PacketVector4UInt32SseMask left, PacketVector4UInt32SseMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32SseMask operator ~(PacketVector4UInt32SseMask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32SseMask operator ==(PacketVector4UInt32SseMask left, PacketVector4UInt32SseMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt32SseMask operator !=(PacketVector4UInt32SseMask left, PacketVector4UInt32SseMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4UInt32SseMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}