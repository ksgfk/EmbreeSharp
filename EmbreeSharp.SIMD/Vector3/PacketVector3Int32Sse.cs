using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector3Int32Sse :
    ISimdIntegerVector3<PacketVector3Int32Sse, PacketInt32Sse, int, PacketVector3Int32SseMask, PacketInt32SseMask>
{
    public PacketVector3Int32Sse(PacketInt32Sse x, PacketInt32Sse y, PacketInt32Sse z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketInt32Sse.LaneCount;

    public PacketInt32Sse X { get; }

    public PacketInt32Sse Y { get; }

    public PacketInt32Sse Z { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Sse Create(PacketInt32Sse x, PacketInt32Sse y, PacketInt32Sse z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Sse Broadcast(int value)
    {
        PacketInt32Sse packet = PacketInt32Sse.Broadcast(value);
        return new(packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Sse Select(PacketVector3Int32SseMask mask, PacketVector3Int32Sse ifTrue, PacketVector3Int32Sse ifFalse)
    {
        return new(
            PacketInt32Sse.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt32Sse.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketInt32Sse.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Sse Select(PacketInt32SseMask mask, PacketVector3Int32Sse ifTrue, PacketVector3Int32Sse ifFalse)
    {
        return new(
            PacketInt32Sse.Select(mask, ifTrue.X, ifFalse.X),
            PacketInt32Sse.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketInt32Sse.Select(mask, ifTrue.Z, ifFalse.Z));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Sse operator +(PacketVector3Int32Sse value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Sse operator +(PacketVector3Int32Sse left, PacketVector3Int32Sse right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Sse operator -(PacketVector3Int32Sse left, PacketVector3Int32Sse right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Sse operator *(PacketVector3Int32Sse left, PacketVector3Int32Sse right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32Sse operator -(PacketVector3Int32Sse value) => new(-value.X, -value.Y, -value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32SseMask operator ==(PacketVector3Int32Sse left, PacketVector3Int32Sse right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32SseMask operator !=(PacketVector3Int32Sse left, PacketVector3Int32Sse right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3Int32Sse other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}

public readonly struct PacketVector3Int32SseMask :
    ISimdVector3Mask<PacketVector3Int32SseMask, PacketInt32SseMask>
{
    public PacketVector3Int32SseMask(PacketInt32SseMask x, PacketInt32SseMask y, PacketInt32SseMask z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketInt32SseMask.LaneCount;

    public PacketInt32SseMask X { get; }

    public PacketInt32SseMask Y { get; }

    public PacketInt32SseMask Z { get; }

    public static PacketVector3Int32SseMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt32SseMask.True, PacketInt32SseMask.True, PacketInt32SseMask.True);
    }

    public static PacketVector3Int32SseMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketInt32SseMask.False, PacketInt32SseMask.False, PacketInt32SseMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32SseMask Create(PacketInt32SseMask x, PacketInt32SseMask y, PacketInt32SseMask z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32SseMask Broadcast(PacketInt32SseMask value) => new(value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask All(PacketVector3Int32SseMask value) => value.X & value.Y & value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask Any(PacketVector3Int32SseMask value) => value.X | value.Y | value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketInt32SseMask None(PacketVector3Int32SseMask value) => ~(value.X | value.Y | value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32SseMask AndNot(PacketVector3Int32SseMask left, PacketVector3Int32SseMask right)
    {
        return new(
            PacketInt32SseMask.AndNot(left.X, right.X),
            PacketInt32SseMask.AndNot(left.Y, right.Y),
            PacketInt32SseMask.AndNot(left.Z, right.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32SseMask Select(PacketVector3Int32SseMask mask, PacketVector3Int32SseMask ifTrue, PacketVector3Int32SseMask ifFalse)
    {
        return new(
            PacketInt32SseMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketInt32SseMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketInt32SseMask.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32SseMask And(PacketVector3Int32SseMask left, PacketVector3Int32SseMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32SseMask Or(PacketVector3Int32SseMask left, PacketVector3Int32SseMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32SseMask Xor(PacketVector3Int32SseMask left, PacketVector3Int32SseMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32SseMask Not(PacketVector3Int32SseMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32SseMask operator &(PacketVector3Int32SseMask left, PacketVector3Int32SseMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32SseMask operator |(PacketVector3Int32SseMask left, PacketVector3Int32SseMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32SseMask operator ^(PacketVector3Int32SseMask left, PacketVector3Int32SseMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32SseMask operator ~(PacketVector3Int32SseMask value) => new(~value.X, ~value.Y, ~value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32SseMask operator ==(PacketVector3Int32SseMask left, PacketVector3Int32SseMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Int32SseMask operator !=(PacketVector3Int32SseMask left, PacketVector3Int32SseMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3Int32SseMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}