using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector3UInt32Sse :
    ISimdIntegerVector3<PacketVector3UInt32Sse, PacketUInt32Sse, uint, PacketVector3UInt32SseMask, PacketUInt32SseMask>
{
    public PacketVector3UInt32Sse(PacketUInt32Sse x, PacketUInt32Sse y, PacketUInt32Sse z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketUInt32Sse.LaneCount;

    public PacketUInt32Sse X { get; }

    public PacketUInt32Sse Y { get; }

    public PacketUInt32Sse Z { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Sse Create(PacketUInt32Sse x, PacketUInt32Sse y, PacketUInt32Sse z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Sse Broadcast(uint value)
    {
        PacketUInt32Sse packet = PacketUInt32Sse.Broadcast(value);
        return new(packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Sse Select(PacketVector3UInt32SseMask mask, PacketVector3UInt32Sse ifTrue, PacketVector3UInt32Sse ifFalse)
    {
        return new(
            PacketUInt32Sse.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt32Sse.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketUInt32Sse.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Sse Select(PacketUInt32SseMask mask, PacketVector3UInt32Sse ifTrue, PacketVector3UInt32Sse ifFalse)
    {
        return new(
            PacketUInt32Sse.Select(mask, ifTrue.X, ifFalse.X),
            PacketUInt32Sse.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketUInt32Sse.Select(mask, ifTrue.Z, ifFalse.Z));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Sse operator +(PacketVector3UInt32Sse value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Sse operator +(PacketVector3UInt32Sse left, PacketVector3UInt32Sse right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Sse operator -(PacketVector3UInt32Sse left, PacketVector3UInt32Sse right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Sse operator *(PacketVector3UInt32Sse left, PacketVector3UInt32Sse right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32Sse operator -(PacketVector3UInt32Sse value) => new(-value.X, -value.Y, -value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32SseMask operator ==(PacketVector3UInt32Sse left, PacketVector3UInt32Sse right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32SseMask operator !=(PacketVector3UInt32Sse left, PacketVector3UInt32Sse right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3UInt32Sse other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}

public readonly struct PacketVector3UInt32SseMask :
    ISimdVector3Mask<PacketVector3UInt32SseMask, PacketUInt32SseMask>
{
    public PacketVector3UInt32SseMask(PacketUInt32SseMask x, PacketUInt32SseMask y, PacketUInt32SseMask z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketUInt32SseMask.LaneCount;

    public PacketUInt32SseMask X { get; }

    public PacketUInt32SseMask Y { get; }

    public PacketUInt32SseMask Z { get; }

    public static PacketVector3UInt32SseMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt32SseMask.True, PacketUInt32SseMask.True, PacketUInt32SseMask.True);
    }

    public static PacketVector3UInt32SseMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt32SseMask.False, PacketUInt32SseMask.False, PacketUInt32SseMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32SseMask Create(PacketUInt32SseMask x, PacketUInt32SseMask y, PacketUInt32SseMask z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32SseMask Broadcast(PacketUInt32SseMask value) => new(value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask All(PacketVector3UInt32SseMask value) => value.X & value.Y & value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask Any(PacketVector3UInt32SseMask value) => value.X | value.Y | value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt32SseMask None(PacketVector3UInt32SseMask value) => ~(value.X | value.Y | value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32SseMask AndNot(PacketVector3UInt32SseMask left, PacketVector3UInt32SseMask right)
    {
        return new(
            PacketUInt32SseMask.AndNot(left.X, right.X),
            PacketUInt32SseMask.AndNot(left.Y, right.Y),
            PacketUInt32SseMask.AndNot(left.Z, right.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32SseMask Select(PacketVector3UInt32SseMask mask, PacketVector3UInt32SseMask ifTrue, PacketVector3UInt32SseMask ifFalse)
    {
        return new(
            PacketUInt32SseMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt32SseMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketUInt32SseMask.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32SseMask And(PacketVector3UInt32SseMask left, PacketVector3UInt32SseMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32SseMask Or(PacketVector3UInt32SseMask left, PacketVector3UInt32SseMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32SseMask Xor(PacketVector3UInt32SseMask left, PacketVector3UInt32SseMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32SseMask Not(PacketVector3UInt32SseMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32SseMask operator &(PacketVector3UInt32SseMask left, PacketVector3UInt32SseMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32SseMask operator |(PacketVector3UInt32SseMask left, PacketVector3UInt32SseMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32SseMask operator ^(PacketVector3UInt32SseMask left, PacketVector3UInt32SseMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32SseMask operator ~(PacketVector3UInt32SseMask value) => new(~value.X, ~value.Y, ~value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32SseMask operator ==(PacketVector3UInt32SseMask left, PacketVector3UInt32SseMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt32SseMask operator !=(PacketVector3UInt32SseMask left, PacketVector3UInt32SseMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3UInt32SseMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}