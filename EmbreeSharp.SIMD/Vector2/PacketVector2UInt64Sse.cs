using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector2UInt64Sse :
    ISimdIntegerVector2<PacketVector2UInt64Sse, PacketUInt64Sse, ulong, PacketVector2UInt64SseMask, PacketUInt64SseMask>
{
    public PacketVector2UInt64Sse(PacketUInt64Sse x, PacketUInt64Sse y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketUInt64Sse.LaneCount;

    public PacketUInt64Sse X { get; }

    public PacketUInt64Sse Y { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Sse Create(PacketUInt64Sse x, PacketUInt64Sse y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Sse Broadcast(ulong value)
    {
        PacketUInt64Sse packet = PacketUInt64Sse.Broadcast(value);
        return new(packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Sse Select(PacketVector2UInt64SseMask mask, PacketVector2UInt64Sse ifTrue, PacketVector2UInt64Sse ifFalse)
    {
        return new(
            PacketUInt64Sse.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt64Sse.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Sse Select(PacketUInt64SseMask mask, PacketVector2UInt64Sse ifTrue, PacketVector2UInt64Sse ifFalse)
    {
        return new(
            PacketUInt64Sse.Select(mask, ifTrue.X, ifFalse.X),
            PacketUInt64Sse.Select(mask, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Sse operator +(PacketVector2UInt64Sse value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Sse operator +(PacketVector2UInt64Sse left, PacketVector2UInt64Sse right) => new(left.X + right.X, left.Y + right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Sse operator -(PacketVector2UInt64Sse left, PacketVector2UInt64Sse right) => new(left.X - right.X, left.Y - right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Sse operator *(PacketVector2UInt64Sse left, PacketVector2UInt64Sse right) => new(left.X * right.X, left.Y * right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64Sse operator -(PacketVector2UInt64Sse value) => new(-value.X, -value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64SseMask operator ==(PacketVector2UInt64Sse left, PacketVector2UInt64Sse right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64SseMask operator !=(PacketVector2UInt64Sse left, PacketVector2UInt64Sse right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2UInt64Sse other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}

public readonly struct PacketVector2UInt64SseMask :
    ISimdVector2Mask<PacketVector2UInt64SseMask, PacketUInt64SseMask>
{
    public PacketVector2UInt64SseMask(PacketUInt64SseMask x, PacketUInt64SseMask y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketUInt64SseMask.LaneCount;

    public PacketUInt64SseMask X { get; }

    public PacketUInt64SseMask Y { get; }

    public static PacketVector2UInt64SseMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt64SseMask.True, PacketUInt64SseMask.True);
    }

    public static PacketVector2UInt64SseMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt64SseMask.False, PacketUInt64SseMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64SseMask Create(PacketUInt64SseMask x, PacketUInt64SseMask y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64SseMask Broadcast(PacketUInt64SseMask value) => new(value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask All(PacketVector2UInt64SseMask value) => value.X & value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask Any(PacketVector2UInt64SseMask value) => value.X | value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask None(PacketVector2UInt64SseMask value) => ~(value.X | value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64SseMask AndNot(PacketVector2UInt64SseMask left, PacketVector2UInt64SseMask right)
    {
        return new(
            PacketUInt64SseMask.AndNot(left.X, right.X),
            PacketUInt64SseMask.AndNot(left.Y, right.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64SseMask Select(PacketVector2UInt64SseMask mask, PacketVector2UInt64SseMask ifTrue, PacketVector2UInt64SseMask ifFalse)
    {
        return new(
            PacketUInt64SseMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt64SseMask.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64SseMask And(PacketVector2UInt64SseMask left, PacketVector2UInt64SseMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64SseMask Or(PacketVector2UInt64SseMask left, PacketVector2UInt64SseMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64SseMask Xor(PacketVector2UInt64SseMask left, PacketVector2UInt64SseMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64SseMask Not(PacketVector2UInt64SseMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64SseMask operator &(PacketVector2UInt64SseMask left, PacketVector2UInt64SseMask right) => new(left.X & right.X, left.Y & right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64SseMask operator |(PacketVector2UInt64SseMask left, PacketVector2UInt64SseMask right) => new(left.X | right.X, left.Y | right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64SseMask operator ^(PacketVector2UInt64SseMask left, PacketVector2UInt64SseMask right) => new(left.X ^ right.X, left.Y ^ right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64SseMask operator ~(PacketVector2UInt64SseMask value) => new(~value.X, ~value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64SseMask operator ==(PacketVector2UInt64SseMask left, PacketVector2UInt64SseMask right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt64SseMask operator !=(PacketVector2UInt64SseMask left, PacketVector2UInt64SseMask right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2UInt64SseMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}