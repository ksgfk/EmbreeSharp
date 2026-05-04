using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector2Int32Sse :
    ISimdIntegerVector2<PacketVector2Int32Sse, PacketInt32Sse, int, PacketVector2Int32SseMask, PacketInt32SseMask>
{
    public PacketVector2Int32Sse(PacketInt32Sse x, PacketInt32Sse y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketInt32Sse.LaneCount;

    public PacketInt32Sse X { get; }

    public PacketInt32Sse Y { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Sse Create(PacketInt32Sse x, PacketInt32Sse y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Sse Broadcast(int value)
    {
        PacketInt32Sse packet = PacketInt32Sse.Broadcast(value);
        return new(packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Sse Select(PacketVector2Int32SseMask mask, PacketVector2Int32Sse ifTrue, PacketVector2Int32Sse ifFalse)
    {
        return new(
            PacketInt32Sse.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt32Sse.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Sse Select(PacketInt32SseMask mask, PacketVector2Int32Sse ifTrue, PacketVector2Int32Sse ifFalse)
    {
        return new(
            PacketInt32Sse.Select(mask, ifTrue.X, ifFalse.X),
            PacketInt32Sse.Select(mask, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Sse operator +(PacketVector2Int32Sse value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Sse operator +(PacketVector2Int32Sse left, PacketVector2Int32Sse right) => new(left.X + right.X, left.Y + right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Sse operator -(PacketVector2Int32Sse left, PacketVector2Int32Sse right) => new(left.X - right.X, left.Y - right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Sse operator *(PacketVector2Int32Sse left, PacketVector2Int32Sse right) => new(left.X * right.X, left.Y * right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32Sse operator -(PacketVector2Int32Sse value) => new(-value.X, -value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32SseMask operator ==(PacketVector2Int32Sse left, PacketVector2Int32Sse right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32SseMask operator !=(PacketVector2Int32Sse left, PacketVector2Int32Sse right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2Int32Sse other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}

public readonly struct PacketVector2Int32SseMask :
    ISimdVector2Mask<PacketVector2Int32SseMask, PacketInt32SseMask>
{
    public PacketVector2Int32SseMask(PacketInt32SseMask x, PacketInt32SseMask y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketInt32SseMask.LaneCount;

    public PacketInt32SseMask X { get; }

    public PacketInt32SseMask Y { get; }

    public static PacketVector2Int32SseMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt32SseMask.True, PacketInt32SseMask.True);
    }

    public static PacketVector2Int32SseMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt32SseMask.False, PacketInt32SseMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32SseMask Create(PacketInt32SseMask x, PacketInt32SseMask y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32SseMask Broadcast(PacketInt32SseMask value) => new(value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask All(PacketVector2Int32SseMask value) => value.X & value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask Any(PacketVector2Int32SseMask value) => value.X | value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask None(PacketVector2Int32SseMask value) => ~(value.X | value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32SseMask AndNot(PacketVector2Int32SseMask left, PacketVector2Int32SseMask right)
    {
        return new(
            PacketInt32SseMask.AndNot(left.X, right.X),
            PacketInt32SseMask.AndNot(left.Y, right.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32SseMask Select(PacketVector2Int32SseMask mask, PacketVector2Int32SseMask ifTrue, PacketVector2Int32SseMask ifFalse)
    {
        return new(
            PacketInt32SseMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt32SseMask.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32SseMask And(PacketVector2Int32SseMask left, PacketVector2Int32SseMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32SseMask Or(PacketVector2Int32SseMask left, PacketVector2Int32SseMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32SseMask Xor(PacketVector2Int32SseMask left, PacketVector2Int32SseMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32SseMask Not(PacketVector2Int32SseMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32SseMask operator &(PacketVector2Int32SseMask left, PacketVector2Int32SseMask right) => new(left.X & right.X, left.Y & right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32SseMask operator |(PacketVector2Int32SseMask left, PacketVector2Int32SseMask right) => new(left.X | right.X, left.Y | right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32SseMask operator ^(PacketVector2Int32SseMask left, PacketVector2Int32SseMask right) => new(left.X ^ right.X, left.Y ^ right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32SseMask operator ~(PacketVector2Int32SseMask value) => new(~value.X, ~value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32SseMask operator ==(PacketVector2Int32SseMask left, PacketVector2Int32SseMask right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Int32SseMask operator !=(PacketVector2Int32SseMask left, PacketVector2Int32SseMask right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2Int32SseMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}