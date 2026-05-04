using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector4UInt64Sse :
    ISimdIntegerVector4<PacketVector4UInt64Sse, PacketUInt64Sse, ulong, PacketVector4UInt64SseMask, PacketUInt64SseMask>
{
    public PacketVector4UInt64Sse(PacketUInt64Sse x, PacketUInt64Sse y, PacketUInt64Sse z, PacketUInt64Sse w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketUInt64Sse.LaneCount;

    public PacketUInt64Sse X { get; }

    public PacketUInt64Sse Y { get; }

    public PacketUInt64Sse Z { get; }

    public PacketUInt64Sse W { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Sse Create(PacketUInt64Sse x, PacketUInt64Sse y, PacketUInt64Sse z, PacketUInt64Sse w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Sse Broadcast(ulong value)
    {
        PacketUInt64Sse packet = PacketUInt64Sse.Broadcast(value);
        return new(packet, packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Sse Select(PacketVector4UInt64SseMask mask, PacketVector4UInt64Sse ifTrue, PacketVector4UInt64Sse ifFalse)
    {
        return new(
            PacketUInt64Sse.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt64Sse.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketUInt64Sse.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketUInt64Sse.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Sse Select(PacketUInt64SseMask mask, PacketVector4UInt64Sse ifTrue, PacketVector4UInt64Sse ifFalse)
    {
        return new(
            PacketUInt64Sse.Select(mask, ifTrue.X, ifFalse.X),
            PacketUInt64Sse.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketUInt64Sse.Select(mask, ifTrue.Z, ifFalse.Z),
            PacketUInt64Sse.Select(mask, ifTrue.W, ifFalse.W));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Sse operator +(PacketVector4UInt64Sse value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Sse operator +(PacketVector4UInt64Sse left, PacketVector4UInt64Sse right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Sse operator -(PacketVector4UInt64Sse left, PacketVector4UInt64Sse right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Sse operator *(PacketVector4UInt64Sse left, PacketVector4UInt64Sse right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z, left.W * right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64Sse operator -(PacketVector4UInt64Sse value) => new(-value.X, -value.Y, -value.Z, -value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64SseMask operator ==(PacketVector4UInt64Sse left, PacketVector4UInt64Sse right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64SseMask operator !=(PacketVector4UInt64Sse left, PacketVector4UInt64Sse right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4UInt64Sse other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct PacketVector4UInt64SseMask :
    ISimdVector4Mask<PacketVector4UInt64SseMask, PacketUInt64SseMask>
{
    public PacketVector4UInt64SseMask(PacketUInt64SseMask x, PacketUInt64SseMask y, PacketUInt64SseMask z, PacketUInt64SseMask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketUInt64SseMask.LaneCount;

    public PacketUInt64SseMask X { get; }

    public PacketUInt64SseMask Y { get; }

    public PacketUInt64SseMask Z { get; }

    public PacketUInt64SseMask W { get; }

    public static PacketVector4UInt64SseMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt64SseMask.True, PacketUInt64SseMask.True, PacketUInt64SseMask.True, PacketUInt64SseMask.True);
    }

    public static PacketVector4UInt64SseMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt64SseMask.False, PacketUInt64SseMask.False, PacketUInt64SseMask.False, PacketUInt64SseMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64SseMask Create(PacketUInt64SseMask x, PacketUInt64SseMask y, PacketUInt64SseMask z, PacketUInt64SseMask w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64SseMask Broadcast(PacketUInt64SseMask value) => new(value, value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask All(PacketVector4UInt64SseMask value) => value.X & value.Y & value.Z & value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask Any(PacketVector4UInt64SseMask value) => value.X | value.Y | value.Z | value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask None(PacketVector4UInt64SseMask value) => ~(value.X | value.Y | value.Z | value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64SseMask AndNot(PacketVector4UInt64SseMask left, PacketVector4UInt64SseMask right)
    {
        return new(
            PacketUInt64SseMask.AndNot(left.X, right.X),
            PacketUInt64SseMask.AndNot(left.Y, right.Y),
            PacketUInt64SseMask.AndNot(left.Z, right.Z),
            PacketUInt64SseMask.AndNot(left.W, right.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64SseMask Select(PacketVector4UInt64SseMask mask, PacketVector4UInt64SseMask ifTrue, PacketVector4UInt64SseMask ifFalse)
    {
        return new(
            PacketUInt64SseMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt64SseMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketUInt64SseMask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketUInt64SseMask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64SseMask And(PacketVector4UInt64SseMask left, PacketVector4UInt64SseMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64SseMask Or(PacketVector4UInt64SseMask left, PacketVector4UInt64SseMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64SseMask Xor(PacketVector4UInt64SseMask left, PacketVector4UInt64SseMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64SseMask Not(PacketVector4UInt64SseMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64SseMask operator &(PacketVector4UInt64SseMask left, PacketVector4UInt64SseMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64SseMask operator |(PacketVector4UInt64SseMask left, PacketVector4UInt64SseMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64SseMask operator ^(PacketVector4UInt64SseMask left, PacketVector4UInt64SseMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64SseMask operator ~(PacketVector4UInt64SseMask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64SseMask operator ==(PacketVector4UInt64SseMask left, PacketVector4UInt64SseMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector4UInt64SseMask operator !=(PacketVector4UInt64SseMask left, PacketVector4UInt64SseMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector4UInt64SseMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}