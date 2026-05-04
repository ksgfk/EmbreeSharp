using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly partial struct PacketVector3Float64Sse :
    ISimdFloatingPointVector3<PacketVector3Float64Sse, PacketFloat64Sse, double, PacketVector3Float64SseMask, PacketFloat64SseMask>
{
    public PacketVector3Float64Sse(PacketFloat64Sse x, PacketFloat64Sse y, PacketFloat64Sse z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketFloat64Sse.LaneCount;

    public PacketFloat64Sse X { get; }

    public PacketFloat64Sse Y { get; }

    public PacketFloat64Sse Z { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Sse Create(PacketFloat64Sse x, PacketFloat64Sse y, PacketFloat64Sse z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Sse Broadcast(double value)
    {
        PacketFloat64Sse packet = PacketFloat64Sse.Broadcast(value);
        return new(packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Sse Select(PacketVector3Float64SseMask mask, PacketVector3Float64Sse ifTrue, PacketVector3Float64Sse ifFalse)
    {
        return new(
            PacketFloat64Sse.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64Sse.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat64Sse.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Sse Select(PacketFloat64SseMask mask, PacketVector3Float64Sse ifTrue, PacketVector3Float64Sse ifFalse)
    {
        return new(
            PacketFloat64Sse.Select(mask, ifTrue.X, ifFalse.X),
            PacketFloat64Sse.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketFloat64Sse.Select(mask, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Sse Dot(PacketVector3Float64Sse left, PacketVector3Float64Sse right)
    {
        return PacketFloat64Sse.FusedMultiplyAdd(
            left.Z,
            right.Z,
            PacketFloat64Sse.FusedMultiplyAdd(left.Y, right.Y, left.X * right.X));
    }

    public static partial PacketVector3Float64Sse Cross(PacketVector3Float64Sse left, PacketVector3Float64Sse right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Sse operator +(PacketVector3Float64Sse value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Sse operator +(PacketVector3Float64Sse left, PacketVector3Float64Sse right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Sse operator -(PacketVector3Float64Sse left, PacketVector3Float64Sse right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Sse operator *(PacketVector3Float64Sse left, PacketVector3Float64Sse right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Sse operator -(PacketVector3Float64Sse value) => new(-value.X, -value.Y, -value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64SseMask operator ==(PacketVector3Float64Sse left, PacketVector3Float64Sse right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64SseMask operator !=(PacketVector3Float64Sse left, PacketVector3Float64Sse right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3Float64Sse other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}

public readonly struct PacketVector3Float64SseMask :
    ISimdVector3Mask<PacketVector3Float64SseMask, PacketFloat64SseMask>
{
    public PacketVector3Float64SseMask(PacketFloat64SseMask x, PacketFloat64SseMask y, PacketFloat64SseMask z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketFloat64SseMask.LaneCount;

    public PacketFloat64SseMask X { get; }

    public PacketFloat64SseMask Y { get; }

    public PacketFloat64SseMask Z { get; }

    public static PacketVector3Float64SseMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat64SseMask.True, PacketFloat64SseMask.True, PacketFloat64SseMask.True);
    }

    public static PacketVector3Float64SseMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat64SseMask.False, PacketFloat64SseMask.False, PacketFloat64SseMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64SseMask Create(PacketFloat64SseMask x, PacketFloat64SseMask y, PacketFloat64SseMask z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64SseMask Broadcast(PacketFloat64SseMask value) => new(value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask All(PacketVector3Float64SseMask value) => value.X & value.Y & value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask Any(PacketVector3Float64SseMask value) => value.X | value.Y | value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64SseMask None(PacketVector3Float64SseMask value) => ~(value.X | value.Y | value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64SseMask AndNot(PacketVector3Float64SseMask left, PacketVector3Float64SseMask right)
    {
        return new(
            PacketFloat64SseMask.AndNot(left.X, right.X),
            PacketFloat64SseMask.AndNot(left.Y, right.Y),
            PacketFloat64SseMask.AndNot(left.Z, right.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64SseMask Select(PacketVector3Float64SseMask mask, PacketVector3Float64SseMask ifTrue, PacketVector3Float64SseMask ifFalse)
    {
        return new(
            PacketFloat64SseMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64SseMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat64SseMask.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64SseMask And(PacketVector3Float64SseMask left, PacketVector3Float64SseMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64SseMask Or(PacketVector3Float64SseMask left, PacketVector3Float64SseMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64SseMask Xor(PacketVector3Float64SseMask left, PacketVector3Float64SseMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64SseMask Not(PacketVector3Float64SseMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64SseMask operator &(PacketVector3Float64SseMask left, PacketVector3Float64SseMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64SseMask operator |(PacketVector3Float64SseMask left, PacketVector3Float64SseMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64SseMask operator ^(PacketVector3Float64SseMask left, PacketVector3Float64SseMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64SseMask operator ~(PacketVector3Float64SseMask value) => new(~value.X, ~value.Y, ~value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64SseMask operator ==(PacketVector3Float64SseMask left, PacketVector3Float64SseMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64SseMask operator !=(PacketVector3Float64SseMask left, PacketVector3Float64SseMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3Float64SseMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}
