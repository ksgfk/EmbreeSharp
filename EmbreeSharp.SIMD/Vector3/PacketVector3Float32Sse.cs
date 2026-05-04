using System.Runtime.CompilerServices;
namespace EmbreeSharp.SIMD;

public readonly partial struct PacketVector3Float32Sse :
    ISimdFloatingPointVector3<PacketVector3Float32Sse, PacketFloat32Sse, float, PacketVector3Float32SseMask, PacketFloat32SseMask>
{
    public PacketVector3Float32Sse(PacketFloat32Sse x, PacketFloat32Sse y, PacketFloat32Sse z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketFloat32Sse.LaneCount;

    public PacketFloat32Sse X { get; }

    public PacketFloat32Sse Y { get; }

    public PacketFloat32Sse Z { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Sse Create(PacketFloat32Sse x, PacketFloat32Sse y, PacketFloat32Sse z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Sse Broadcast(float value)
    {
        PacketFloat32Sse packet = PacketFloat32Sse.Broadcast(value);
        return new(packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Sse Select(PacketVector3Float32SseMask mask, PacketVector3Float32Sse ifTrue, PacketVector3Float32Sse ifFalse)
    {
        return new(
            PacketFloat32Sse.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32Sse.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat32Sse.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Sse Select(PacketFloat32SseMask mask, PacketVector3Float32Sse ifTrue, PacketVector3Float32Sse ifFalse)
    {
        return new(
            PacketFloat32Sse.Select(mask, ifTrue.X, ifFalse.X),
            PacketFloat32Sse.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketFloat32Sse.Select(mask, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Sse Dot(PacketVector3Float32Sse left, PacketVector3Float32Sse right)
    {
        return PacketFloat32Sse.FusedMultiplyAdd(
            left.Z,
            right.Z,
            PacketFloat32Sse.FusedMultiplyAdd(left.Y, right.Y, left.X * right.X));
    }

    public static partial PacketVector3Float32Sse Cross(PacketVector3Float32Sse left, PacketVector3Float32Sse right);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Sse operator +(PacketVector3Float32Sse value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Sse operator +(PacketVector3Float32Sse left, PacketVector3Float32Sse right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Sse operator -(PacketVector3Float32Sse left, PacketVector3Float32Sse right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Sse operator *(PacketVector3Float32Sse left, PacketVector3Float32Sse right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Sse operator -(PacketVector3Float32Sse value) => new(-value.X, -value.Y, -value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32SseMask operator ==(PacketVector3Float32Sse left, PacketVector3Float32Sse right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32SseMask operator !=(PacketVector3Float32Sse left, PacketVector3Float32Sse right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3Float32Sse other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}

public readonly struct PacketVector3Float32SseMask :
    ISimdVector3Mask<PacketVector3Float32SseMask, PacketFloat32SseMask>
{
    public PacketVector3Float32SseMask(PacketFloat32SseMask x, PacketFloat32SseMask y, PacketFloat32SseMask z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketFloat32SseMask.LaneCount;

    public PacketFloat32SseMask X { get; }

    public PacketFloat32SseMask Y { get; }

    public PacketFloat32SseMask Z { get; }

    public static PacketVector3Float32SseMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat32SseMask.True, PacketFloat32SseMask.True, PacketFloat32SseMask.True);
    }

    public static PacketVector3Float32SseMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat32SseMask.False, PacketFloat32SseMask.False, PacketFloat32SseMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32SseMask Create(PacketFloat32SseMask x, PacketFloat32SseMask y, PacketFloat32SseMask z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32SseMask Broadcast(PacketFloat32SseMask value) => new(value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask All(PacketVector3Float32SseMask value) => value.X & value.Y & value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask Any(PacketVector3Float32SseMask value) => value.X | value.Y | value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32SseMask None(PacketVector3Float32SseMask value) => ~(value.X | value.Y | value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32SseMask AndNot(PacketVector3Float32SseMask left, PacketVector3Float32SseMask right)
    {
        return new(
            PacketFloat32SseMask.AndNot(left.X, right.X),
            PacketFloat32SseMask.AndNot(left.Y, right.Y),
            PacketFloat32SseMask.AndNot(left.Z, right.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32SseMask Select(PacketVector3Float32SseMask mask, PacketVector3Float32SseMask ifTrue, PacketVector3Float32SseMask ifFalse)
    {
        return new(
            PacketFloat32SseMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32SseMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat32SseMask.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32SseMask And(PacketVector3Float32SseMask left, PacketVector3Float32SseMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32SseMask Or(PacketVector3Float32SseMask left, PacketVector3Float32SseMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32SseMask Xor(PacketVector3Float32SseMask left, PacketVector3Float32SseMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32SseMask Not(PacketVector3Float32SseMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32SseMask operator &(PacketVector3Float32SseMask left, PacketVector3Float32SseMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32SseMask operator |(PacketVector3Float32SseMask left, PacketVector3Float32SseMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32SseMask operator ^(PacketVector3Float32SseMask left, PacketVector3Float32SseMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32SseMask operator ~(PacketVector3Float32SseMask value) => new(~value.X, ~value.Y, ~value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32SseMask operator ==(PacketVector3Float32SseMask left, PacketVector3Float32SseMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32SseMask operator !=(PacketVector3Float32SseMask left, PacketVector3Float32SseMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3Float32SseMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}
