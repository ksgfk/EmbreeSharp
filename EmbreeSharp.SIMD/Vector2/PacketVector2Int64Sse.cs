using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector2Int64Sse :
    ISimdIntegerVector2<PacketVector2Int64Sse, PacketInt64Sse, long, PacketVector2Int64SseMask, PacketInt64SseMask>
{
    public PacketVector2Int64Sse(PacketInt64Sse x, PacketInt64Sse y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketInt64Sse.LaneCount;

    public PacketInt64Sse X { get; }

    public PacketInt64Sse Y { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Sse Create(PacketInt64Sse x, PacketInt64Sse y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Sse Broadcast(long value)
    {
        PacketInt64Sse packet = PacketInt64Sse.Broadcast(value);
        return new(packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Sse Select(PacketVector2Int64SseMask mask, PacketVector2Int64Sse ifTrue, PacketVector2Int64Sse ifFalse)
    {
        return new(
            PacketInt64Sse.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt64Sse.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Sse Select(PacketInt64SseMask mask, PacketVector2Int64Sse ifTrue, PacketVector2Int64Sse ifFalse)
    {
        return new(
            PacketInt64Sse.Select(mask, ifTrue.X, ifFalse.X),
            PacketInt64Sse.Select(mask, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Sse operator +(PacketVector2Int64Sse value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Sse operator +(PacketVector2Int64Sse left, PacketVector2Int64Sse right) => new(left.X + right.X, left.Y + right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Sse operator -(PacketVector2Int64Sse left, PacketVector2Int64Sse right) => new(left.X - right.X, left.Y - right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Sse operator *(PacketVector2Int64Sse left, PacketVector2Int64Sse right) => new(left.X * right.X, left.Y * right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64Sse operator -(PacketVector2Int64Sse value) => new(-value.X, -value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64SseMask operator ==(PacketVector2Int64Sse left, PacketVector2Int64Sse right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64SseMask operator !=(PacketVector2Int64Sse left, PacketVector2Int64Sse right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2Int64Sse other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}

public readonly struct PacketVector2Int64SseMask :
    ISimdVector2Mask<PacketVector2Int64SseMask, PacketInt64SseMask>
{
    public PacketVector2Int64SseMask(PacketInt64SseMask x, PacketInt64SseMask y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketInt64SseMask.LaneCount;

    public PacketInt64SseMask X { get; }

    public PacketInt64SseMask Y { get; }

    public static PacketVector2Int64SseMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt64SseMask.True, PacketInt64SseMask.True);
    }

    public static PacketVector2Int64SseMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt64SseMask.False, PacketInt64SseMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64SseMask Create(PacketInt64SseMask x, PacketInt64SseMask y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64SseMask Broadcast(PacketInt64SseMask value) => new(value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask All(PacketVector2Int64SseMask value) => value.X & value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask Any(PacketVector2Int64SseMask value) => value.X | value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask None(PacketVector2Int64SseMask value) => ~(value.X | value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64SseMask AndNot(PacketVector2Int64SseMask left, PacketVector2Int64SseMask right)
    {
        return new(
            PacketInt64SseMask.AndNot(left.X, right.X),
            PacketInt64SseMask.AndNot(left.Y, right.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64SseMask Select(PacketVector2Int64SseMask mask, PacketVector2Int64SseMask ifTrue, PacketVector2Int64SseMask ifFalse)
    {
        return new(
            PacketInt64SseMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt64SseMask.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64SseMask And(PacketVector2Int64SseMask left, PacketVector2Int64SseMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64SseMask Or(PacketVector2Int64SseMask left, PacketVector2Int64SseMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64SseMask Xor(PacketVector2Int64SseMask left, PacketVector2Int64SseMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64SseMask Not(PacketVector2Int64SseMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64SseMask operator &(PacketVector2Int64SseMask left, PacketVector2Int64SseMask right) => new(left.X & right.X, left.Y & right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64SseMask operator |(PacketVector2Int64SseMask left, PacketVector2Int64SseMask right) => new(left.X | right.X, left.Y | right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64SseMask operator ^(PacketVector2Int64SseMask left, PacketVector2Int64SseMask right) => new(left.X ^ right.X, left.Y ^ right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64SseMask operator ~(PacketVector2Int64SseMask value) => new(~value.X, ~value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64SseMask operator ==(PacketVector2Int64SseMask left, PacketVector2Int64SseMask right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int64SseMask operator !=(PacketVector2Int64SseMask left, PacketVector2Int64SseMask right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2Int64SseMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}