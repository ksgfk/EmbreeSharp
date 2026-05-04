using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector2Float32Sse :
    ISimdFloatingPointVector2<PacketVector2Float32Sse, PacketFloat32Sse, float, PacketVector2Float32SseMask, PacketFloat32SseMask>
{
    public PacketVector2Float32Sse(PacketFloat32Sse x, PacketFloat32Sse y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketFloat32Sse.LaneCount;

    public PacketFloat32Sse X { get; }

    public PacketFloat32Sse Y { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Sse Create(PacketFloat32Sse x, PacketFloat32Sse y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Sse Broadcast(float value)
    {
        PacketFloat32Sse packet = PacketFloat32Sse.Broadcast(value);
        return new(packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Sse Select(PacketVector2Float32SseMask mask, PacketVector2Float32Sse ifTrue, PacketVector2Float32Sse ifFalse)
    {
        return new(
            PacketFloat32Sse.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32Sse.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Sse Select(PacketFloat32SseMask mask, PacketVector2Float32Sse ifTrue, PacketVector2Float32Sse ifFalse)
    {
        return new(
            PacketFloat32Sse.Select(mask, ifTrue.X, ifFalse.X),
            PacketFloat32Sse.Select(mask, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Sse Dot(PacketVector2Float32Sse left, PacketVector2Float32Sse right)
    {
        return PacketFloat32Sse.FusedMultiplyAdd(left.Y, right.Y, left.X * right.X);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Sse operator +(PacketVector2Float32Sse value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Sse operator +(PacketVector2Float32Sse left, PacketVector2Float32Sse right) => new(left.X + right.X, left.Y + right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Sse operator -(PacketVector2Float32Sse left, PacketVector2Float32Sse right) => new(left.X - right.X, left.Y - right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Sse operator *(PacketVector2Float32Sse left, PacketVector2Float32Sse right) => new(left.X * right.X, left.Y * right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Sse operator -(PacketVector2Float32Sse value) => new(-value.X, -value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32SseMask operator ==(PacketVector2Float32Sse left, PacketVector2Float32Sse right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32SseMask operator !=(PacketVector2Float32Sse left, PacketVector2Float32Sse right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2Float32Sse other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}

public readonly struct PacketVector2Float32SseMask :
    ISimdVector2Mask<PacketVector2Float32SseMask, PacketFloat32SseMask>
{
    public PacketVector2Float32SseMask(PacketFloat32SseMask x, PacketFloat32SseMask y)
    {
        X = x;
        Y = y;
    }

    public static int LaneCount => PacketFloat32SseMask.LaneCount;

    public PacketFloat32SseMask X { get; }

    public PacketFloat32SseMask Y { get; }

    public static PacketVector2Float32SseMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat32SseMask.True, PacketFloat32SseMask.True);
    }

    public static PacketVector2Float32SseMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat32SseMask.False, PacketFloat32SseMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32SseMask Create(PacketFloat32SseMask x, PacketFloat32SseMask y) => new(x, y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32SseMask Broadcast(PacketFloat32SseMask value) => new(value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask All(PacketVector2Float32SseMask value) => value.X & value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask Any(PacketVector2Float32SseMask value) => value.X | value.Y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask None(PacketVector2Float32SseMask value) => ~(value.X | value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32SseMask AndNot(PacketVector2Float32SseMask left, PacketVector2Float32SseMask right)
    {
        return new(
            PacketFloat32SseMask.AndNot(left.X, right.X),
            PacketFloat32SseMask.AndNot(left.Y, right.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32SseMask Select(PacketVector2Float32SseMask mask, PacketVector2Float32SseMask ifTrue, PacketVector2Float32SseMask ifFalse)
    {
        return new(
            PacketFloat32SseMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32SseMask.Select(mask.Y, ifTrue.Y, ifFalse.Y));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32SseMask And(PacketVector2Float32SseMask left, PacketVector2Float32SseMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32SseMask Or(PacketVector2Float32SseMask left, PacketVector2Float32SseMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32SseMask Xor(PacketVector2Float32SseMask left, PacketVector2Float32SseMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32SseMask Not(PacketVector2Float32SseMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32SseMask operator &(PacketVector2Float32SseMask left, PacketVector2Float32SseMask right) => new(left.X & right.X, left.Y & right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32SseMask operator |(PacketVector2Float32SseMask left, PacketVector2Float32SseMask right) => new(left.X | right.X, left.Y | right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32SseMask operator ^(PacketVector2Float32SseMask left, PacketVector2Float32SseMask right) => new(left.X ^ right.X, left.Y ^ right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32SseMask operator ~(PacketVector2Float32SseMask value) => new(~value.X, ~value.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32SseMask operator ==(PacketVector2Float32SseMask left, PacketVector2Float32SseMask right) => new(left.X == right.X, left.Y == right.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32SseMask operator !=(PacketVector2Float32SseMask left, PacketVector2Float32SseMask right) => new(left.X != right.X, left.Y != right.Y);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector2Float32SseMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y);
}