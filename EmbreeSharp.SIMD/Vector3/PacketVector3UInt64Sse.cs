using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector3UInt64Sse :
    ISimdIntegerVector3<PacketVector3UInt64Sse, PacketUInt64Sse, ulong, PacketVector3UInt64SseMask, PacketUInt64SseMask>
{
    public PacketVector3UInt64Sse(PacketUInt64Sse x, PacketUInt64Sse y, PacketUInt64Sse z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketUInt64Sse.LaneCount;

    public PacketUInt64Sse X { get; }

    public PacketUInt64Sse Y { get; }

    public PacketUInt64Sse Z { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Sse Create(PacketUInt64Sse x, PacketUInt64Sse y, PacketUInt64Sse z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Sse Broadcast(ulong value)
    {
        PacketUInt64Sse packet = PacketUInt64Sse.Broadcast(value);
        return new(packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Sse Select(PacketVector3UInt64SseMask mask, PacketVector3UInt64Sse ifTrue, PacketVector3UInt64Sse ifFalse)
    {
        return new(
            PacketUInt64Sse.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt64Sse.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketUInt64Sse.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Sse Select(PacketUInt64SseMask mask, PacketVector3UInt64Sse ifTrue, PacketVector3UInt64Sse ifFalse)
    {
        return new(
            PacketUInt64Sse.Select(mask, ifTrue.X, ifFalse.X),
            PacketUInt64Sse.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketUInt64Sse.Select(mask, ifTrue.Z, ifFalse.Z));
    }


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Sse operator +(PacketVector3UInt64Sse value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Sse operator +(PacketVector3UInt64Sse left, PacketVector3UInt64Sse right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Sse operator -(PacketVector3UInt64Sse left, PacketVector3UInt64Sse right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Sse operator *(PacketVector3UInt64Sse left, PacketVector3UInt64Sse right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64Sse operator -(PacketVector3UInt64Sse value) => new(-value.X, -value.Y, -value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64SseMask operator ==(PacketVector3UInt64Sse left, PacketVector3UInt64Sse right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64SseMask operator !=(PacketVector3UInt64Sse left, PacketVector3UInt64Sse right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3UInt64Sse other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}

public readonly struct PacketVector3UInt64SseMask :
    ISimdVector3Mask<PacketVector3UInt64SseMask, PacketUInt64SseMask>
{
    public PacketVector3UInt64SseMask(PacketUInt64SseMask x, PacketUInt64SseMask y, PacketUInt64SseMask z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketUInt64SseMask.LaneCount;

    public PacketUInt64SseMask X { get; }

    public PacketUInt64SseMask Y { get; }

    public PacketUInt64SseMask Z { get; }

    public static PacketVector3UInt64SseMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt64SseMask.True, PacketUInt64SseMask.True, PacketUInt64SseMask.True);
    }

    public static PacketVector3UInt64SseMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketUInt64SseMask.False, PacketUInt64SseMask.False, PacketUInt64SseMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64SseMask Create(PacketUInt64SseMask x, PacketUInt64SseMask y, PacketUInt64SseMask z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64SseMask Broadcast(PacketUInt64SseMask value) => new(value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask All(PacketVector3UInt64SseMask value) => value.X & value.Y & value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask Any(PacketVector3UInt64SseMask value) => value.X | value.Y | value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketUInt64SseMask None(PacketVector3UInt64SseMask value) => ~(value.X | value.Y | value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64SseMask AndNot(PacketVector3UInt64SseMask left, PacketVector3UInt64SseMask right)
    {
        return new(
            PacketUInt64SseMask.AndNot(left.X, right.X),
            PacketUInt64SseMask.AndNot(left.Y, right.Y),
            PacketUInt64SseMask.AndNot(left.Z, right.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64SseMask Select(PacketVector3UInt64SseMask mask, PacketVector3UInt64SseMask ifTrue, PacketVector3UInt64SseMask ifFalse)
    {
        return new(
            PacketUInt64SseMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketUInt64SseMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketUInt64SseMask.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64SseMask And(PacketVector3UInt64SseMask left, PacketVector3UInt64SseMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64SseMask Or(PacketVector3UInt64SseMask left, PacketVector3UInt64SseMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64SseMask Xor(PacketVector3UInt64SseMask left, PacketVector3UInt64SseMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64SseMask Not(PacketVector3UInt64SseMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64SseMask operator &(PacketVector3UInt64SseMask left, PacketVector3UInt64SseMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64SseMask operator |(PacketVector3UInt64SseMask left, PacketVector3UInt64SseMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64SseMask operator ^(PacketVector3UInt64SseMask left, PacketVector3UInt64SseMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64SseMask operator ~(PacketVector3UInt64SseMask value) => new(~value.X, ~value.Y, ~value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64SseMask operator ==(PacketVector3UInt64SseMask left, PacketVector3UInt64SseMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3UInt64SseMask operator !=(PacketVector3UInt64SseMask left, PacketVector3UInt64SseMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3UInt64SseMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}