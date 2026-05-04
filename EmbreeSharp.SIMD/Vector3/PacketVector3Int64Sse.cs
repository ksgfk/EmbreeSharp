using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector3Int64Sse :
    ISimdIntegerVector3<PacketVector3Int64Sse, PacketInt64Sse, long, PacketVector3Int64SseMask, PacketInt64SseMask>
{
    public PacketVector3Int64Sse(PacketInt64Sse x, PacketInt64Sse y, PacketInt64Sse z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketInt64Sse.LaneCount;

    public PacketInt64Sse X { get; }

    public PacketInt64Sse Y { get; }

    public PacketInt64Sse Z { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Sse Create(PacketInt64Sse x, PacketInt64Sse y, PacketInt64Sse z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Sse Broadcast(long value)
    {
        PacketInt64Sse packet = PacketInt64Sse.Broadcast(value);
        return new(packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Sse Select(PacketVector3Int64SseMask mask, PacketVector3Int64Sse ifTrue, PacketVector3Int64Sse ifFalse)
    {
        return new(
            PacketInt64Sse.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt64Sse.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketInt64Sse.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Sse Select(PacketInt64SseMask mask, PacketVector3Int64Sse ifTrue, PacketVector3Int64Sse ifFalse)
    {
        return new(
            PacketInt64Sse.Select(mask, ifTrue.X, ifFalse.X),
            PacketInt64Sse.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketInt64Sse.Select(mask, ifTrue.Z, ifFalse.Z));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Sse operator +(PacketVector3Int64Sse value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Sse operator +(PacketVector3Int64Sse left, PacketVector3Int64Sse right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Sse operator -(PacketVector3Int64Sse left, PacketVector3Int64Sse right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Sse operator *(PacketVector3Int64Sse left, PacketVector3Int64Sse right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64Sse operator -(PacketVector3Int64Sse value) => new(-value.X, -value.Y, -value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64SseMask operator ==(PacketVector3Int64Sse left, PacketVector3Int64Sse right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64SseMask operator !=(PacketVector3Int64Sse left, PacketVector3Int64Sse right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3Int64Sse other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}

public readonly struct PacketVector3Int64SseMask :
    ISimdVector3Mask<PacketVector3Int64SseMask, PacketInt64SseMask>
{
    public PacketVector3Int64SseMask(PacketInt64SseMask x, PacketInt64SseMask y, PacketInt64SseMask z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketInt64SseMask.LaneCount;

    public PacketInt64SseMask X { get; }

    public PacketInt64SseMask Y { get; }

    public PacketInt64SseMask Z { get; }

    public static PacketVector3Int64SseMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt64SseMask.True, PacketInt64SseMask.True, PacketInt64SseMask.True);
    }

    public static PacketVector3Int64SseMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt64SseMask.False, PacketInt64SseMask.False, PacketInt64SseMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64SseMask Create(PacketInt64SseMask x, PacketInt64SseMask y, PacketInt64SseMask z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64SseMask Broadcast(PacketInt64SseMask value) => new(value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask All(PacketVector3Int64SseMask value) => value.X & value.Y & value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask Any(PacketVector3Int64SseMask value) => value.X | value.Y | value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt64SseMask None(PacketVector3Int64SseMask value) => ~(value.X | value.Y | value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64SseMask AndNot(PacketVector3Int64SseMask left, PacketVector3Int64SseMask right)
    {
        return new(
            PacketInt64SseMask.AndNot(left.X, right.X),
            PacketInt64SseMask.AndNot(left.Y, right.Y),
            PacketInt64SseMask.AndNot(left.Z, right.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64SseMask Select(PacketVector3Int64SseMask mask, PacketVector3Int64SseMask ifTrue, PacketVector3Int64SseMask ifFalse)
    {
        return new(
            PacketInt64SseMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt64SseMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketInt64SseMask.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64SseMask And(PacketVector3Int64SseMask left, PacketVector3Int64SseMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64SseMask Or(PacketVector3Int64SseMask left, PacketVector3Int64SseMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64SseMask Xor(PacketVector3Int64SseMask left, PacketVector3Int64SseMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64SseMask Not(PacketVector3Int64SseMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64SseMask operator &(PacketVector3Int64SseMask left, PacketVector3Int64SseMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64SseMask operator |(PacketVector3Int64SseMask left, PacketVector3Int64SseMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64SseMask operator ^(PacketVector3Int64SseMask left, PacketVector3Int64SseMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64SseMask operator ~(PacketVector3Int64SseMask value) => new(~value.X, ~value.Y, ~value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64SseMask operator ==(PacketVector3Int64SseMask left, PacketVector3Int64SseMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int64SseMask operator !=(PacketVector3Int64SseMask left, PacketVector3Int64SseMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3Int64SseMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}