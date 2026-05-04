using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector2UInt32Sse :
    ISimdIntegerVector2<PacketVector2UInt32Sse, PacketUInt32Sse, uint, PacketVector2UInt32SseMask, PacketUInt32SseMask>
{
    public PacketVector2UInt32Sse(PacketUInt32Sse x, PacketUInt32Sse y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketUInt32Sse.LaneCount;

    public PacketUInt32Sse X { get; }

    public PacketUInt32Sse Y { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Sse Create(PacketUInt32Sse x, PacketUInt32Sse y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Sse Broadcast(uint value)
    {
        PacketUInt32Sse packet = PacketUInt32Sse.Broadcast(value);
        return new(packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Sse Select(PacketVector2UInt32SseMask mask, PacketVector2UInt32Sse ifTrue, PacketVector2UInt32Sse ifFalse)
    {
        return new(
            PacketUInt32Sse.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt32Sse.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Sse Select(PacketUInt32SseMask mask, PacketVector2UInt32Sse ifTrue, PacketVector2UInt32Sse ifFalse)
    {
        return new(
            PacketUInt32Sse.Select(mask, ifTrue.X, ifFalse.X),
            PacketUInt32Sse.Select(mask, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Sse operator +(PacketVector2UInt32Sse value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Sse operator +(PacketVector2UInt32Sse left, PacketVector2UInt32Sse right) => new(left.X + right.X, left.Y + right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Sse operator -(PacketVector2UInt32Sse left, PacketVector2UInt32Sse right) => new(left.X - right.X, left.Y - right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Sse operator *(PacketVector2UInt32Sse left, PacketVector2UInt32Sse right) => new(left.X * right.X, left.Y * right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32Sse operator -(PacketVector2UInt32Sse value) => new(-value.X, -value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32SseMask operator ==(PacketVector2UInt32Sse left, PacketVector2UInt32Sse right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32SseMask operator !=(PacketVector2UInt32Sse left, PacketVector2UInt32Sse right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2UInt32Sse other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}

public readonly struct PacketVector2UInt32SseMask :
    ISimdVector2Mask<PacketVector2UInt32SseMask, PacketUInt32SseMask>
{
    public PacketVector2UInt32SseMask(PacketUInt32SseMask x, PacketUInt32SseMask y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketUInt32SseMask.LaneCount;

    public PacketUInt32SseMask X { get; }

    public PacketUInt32SseMask Y { get; }

    public static PacketVector2UInt32SseMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt32SseMask.True, PacketUInt32SseMask.True);
    }

    public static PacketVector2UInt32SseMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt32SseMask.False, PacketUInt32SseMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32SseMask Create(PacketUInt32SseMask x, PacketUInt32SseMask y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32SseMask Broadcast(PacketUInt32SseMask value) => new(value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask All(PacketVector2UInt32SseMask value) => value.X & value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask Any(PacketVector2UInt32SseMask value) => value.X | value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask None(PacketVector2UInt32SseMask value) => ~(value.X | value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32SseMask AndNot(PacketVector2UInt32SseMask left, PacketVector2UInt32SseMask right)
    {
        return new(
            PacketUInt32SseMask.AndNot(left.X, right.X),
            PacketUInt32SseMask.AndNot(left.Y, right.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32SseMask Select(PacketVector2UInt32SseMask mask, PacketVector2UInt32SseMask ifTrue, PacketVector2UInt32SseMask ifFalse)
    {
        return new(
            PacketUInt32SseMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt32SseMask.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32SseMask And(PacketVector2UInt32SseMask left, PacketVector2UInt32SseMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32SseMask Or(PacketVector2UInt32SseMask left, PacketVector2UInt32SseMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32SseMask Xor(PacketVector2UInt32SseMask left, PacketVector2UInt32SseMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32SseMask Not(PacketVector2UInt32SseMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32SseMask operator &(PacketVector2UInt32SseMask left, PacketVector2UInt32SseMask right) => new(left.X & right.X, left.Y & right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32SseMask operator |(PacketVector2UInt32SseMask left, PacketVector2UInt32SseMask right) => new(left.X | right.X, left.Y | right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32SseMask operator ^(PacketVector2UInt32SseMask left, PacketVector2UInt32SseMask right) => new(left.X ^ right.X, left.Y ^ right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32SseMask operator ~(PacketVector2UInt32SseMask value) => new(~value.X, ~value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32SseMask operator ==(PacketVector2UInt32SseMask left, PacketVector2UInt32SseMask right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2UInt32SseMask operator !=(PacketVector2UInt32SseMask left, PacketVector2UInt32SseMask right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2UInt32SseMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}