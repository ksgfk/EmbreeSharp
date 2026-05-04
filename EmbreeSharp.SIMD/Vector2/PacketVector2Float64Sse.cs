using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector2Float64Sse :
    ISimdFloatingPointVector2<PacketVector2Float64Sse, PacketFloat64Sse, double, PacketVector2Float64SseMask, PacketFloat64SseMask>
{
    public PacketVector2Float64Sse(PacketFloat64Sse x, PacketFloat64Sse y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketFloat64Sse.LaneCount;

    public PacketFloat64Sse X { get; }

    public PacketFloat64Sse Y { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Sse Create(PacketFloat64Sse x, PacketFloat64Sse y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Sse Broadcast(double value)
    {
        PacketFloat64Sse packet = PacketFloat64Sse.Broadcast(value);
        return new(packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Sse Select(PacketVector2Float64SseMask mask, PacketVector2Float64Sse ifTrue, PacketVector2Float64Sse ifFalse)
    {
        return new(
            PacketFloat64Sse.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64Sse.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Sse Select(PacketFloat64SseMask mask, PacketVector2Float64Sse ifTrue, PacketVector2Float64Sse ifFalse)
    {
        return new(
            PacketFloat64Sse.Select(mask, ifTrue.X, ifFalse.X),
            PacketFloat64Sse.Select(mask, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Sse Dot(PacketVector2Float64Sse left, PacketVector2Float64Sse right)
    {
        return PacketFloat64Sse.FusedMultiplyAdd(left.Y, right.Y, left.X * right.X);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Sse operator +(PacketVector2Float64Sse value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Sse operator +(PacketVector2Float64Sse left, PacketVector2Float64Sse right) => new(left.X + right.X, left.Y + right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Sse operator -(PacketVector2Float64Sse left, PacketVector2Float64Sse right) => new(left.X - right.X, left.Y - right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Sse operator *(PacketVector2Float64Sse left, PacketVector2Float64Sse right) => new(left.X * right.X, left.Y * right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64Sse operator -(PacketVector2Float64Sse value) => new(-value.X, -value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64SseMask operator ==(PacketVector2Float64Sse left, PacketVector2Float64Sse right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64SseMask operator !=(PacketVector2Float64Sse left, PacketVector2Float64Sse right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2Float64Sse other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}

public readonly struct PacketVector2Float64SseMask :
    ISimdVector2Mask<PacketVector2Float64SseMask, PacketFloat64SseMask>
{
    public PacketVector2Float64SseMask(PacketFloat64SseMask x, PacketFloat64SseMask y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketFloat64SseMask.LaneCount;

    public PacketFloat64SseMask X { get; }

    public PacketFloat64SseMask Y { get; }

    public static PacketVector2Float64SseMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat64SseMask.True, PacketFloat64SseMask.True);
    }

    public static PacketVector2Float64SseMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat64SseMask.False, PacketFloat64SseMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64SseMask Create(PacketFloat64SseMask x, PacketFloat64SseMask y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64SseMask Broadcast(PacketFloat64SseMask value) => new(value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask All(PacketVector2Float64SseMask value) => value.X & value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask Any(PacketVector2Float64SseMask value) => value.X | value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask None(PacketVector2Float64SseMask value) => ~(value.X | value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64SseMask AndNot(PacketVector2Float64SseMask left, PacketVector2Float64SseMask right)
    {
        return new(
            PacketFloat64SseMask.AndNot(left.X, right.X),
            PacketFloat64SseMask.AndNot(left.Y, right.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64SseMask Select(PacketVector2Float64SseMask mask, PacketVector2Float64SseMask ifTrue, PacketVector2Float64SseMask ifFalse)
    {
        return new(
            PacketFloat64SseMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64SseMask.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64SseMask And(PacketVector2Float64SseMask left, PacketVector2Float64SseMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64SseMask Or(PacketVector2Float64SseMask left, PacketVector2Float64SseMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64SseMask Xor(PacketVector2Float64SseMask left, PacketVector2Float64SseMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64SseMask Not(PacketVector2Float64SseMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64SseMask operator &(PacketVector2Float64SseMask left, PacketVector2Float64SseMask right) => new(left.X & right.X, left.Y & right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64SseMask operator |(PacketVector2Float64SseMask left, PacketVector2Float64SseMask right) => new(left.X | right.X, left.Y | right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64SseMask operator ^(PacketVector2Float64SseMask left, PacketVector2Float64SseMask right) => new(left.X ^ right.X, left.Y ^ right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64SseMask operator ~(PacketVector2Float64SseMask value) => new(~value.X, ~value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64SseMask operator ==(PacketVector2Float64SseMask left, PacketVector2Float64SseMask right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float64SseMask operator !=(PacketVector2Float64SseMask left, PacketVector2Float64SseMask right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2Float64SseMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}