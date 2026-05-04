using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector3Float64Neon :
    ISimdFloatingPointVector3<PacketVector3Float64Neon, PacketFloat64Neon, double, PacketVector3Float64NeonMask, PacketFloat64NeonMask>
{
    public PacketVector3Float64Neon(PacketFloat64Neon x, PacketFloat64Neon y, PacketFloat64Neon z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketFloat64Neon.LaneCount;

    public PacketFloat64Neon X { get; }

    public PacketFloat64Neon Y { get; }

    public PacketFloat64Neon Z { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Neon Create(PacketFloat64Neon x, PacketFloat64Neon y, PacketFloat64Neon z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Neon Broadcast(double value)
    {
        PacketFloat64Neon packet = PacketFloat64Neon.Broadcast(value);
        return new(packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Neon Select(PacketVector3Float64NeonMask mask, PacketVector3Float64Neon ifTrue, PacketVector3Float64Neon ifFalse)
    {
        return new(
            PacketFloat64Neon.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64Neon.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat64Neon.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Neon Select(PacketFloat64NeonMask mask, PacketVector3Float64Neon ifTrue, PacketVector3Float64Neon ifFalse)
    {
        return new(
            PacketFloat64Neon.Select(mask, ifTrue.X, ifFalse.X),
            PacketFloat64Neon.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketFloat64Neon.Select(mask, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64Neon Dot(PacketVector3Float64Neon left, PacketVector3Float64Neon right)
    {
        return PacketFloat64Neon.FusedMultiplyAdd(
            left.Z,
            right.Z,
            PacketFloat64Neon.FusedMultiplyAdd(left.Y, right.Y, left.X * right.X));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Neon Cross(PacketVector3Float64Neon left, PacketVector3Float64Neon right)
    {
        PacketVector3Float64Neon leftZxy = new(left.Z, left.X, left.Y);
        PacketVector3Float64Neon rightYzx = new(right.Y, right.Z, right.X);
        PacketVector3Float64Neon leftYzx = new(left.Y, left.Z, left.X);
        PacketVector3Float64Neon rightZxy = new(right.Z, right.X, right.Y);

        return new(
            new(AdvSimd.Arm64.FusedMultiplySubtract(
                (leftYzx.X * rightZxy.X)._value,
                leftZxy.X._value,
                rightYzx.X._value)),
            new(AdvSimd.Arm64.FusedMultiplySubtract(
                (leftYzx.Y * rightZxy.Y)._value,
                leftZxy.Y._value,
                rightYzx.Y._value)),
            new(AdvSimd.Arm64.FusedMultiplySubtract(
                (leftYzx.Z * rightZxy.Z)._value,
                leftZxy.Z._value,
                rightYzx.Z._value)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Neon operator +(PacketVector3Float64Neon value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Neon operator +(PacketVector3Float64Neon left, PacketVector3Float64Neon right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Neon operator -(PacketVector3Float64Neon left, PacketVector3Float64Neon right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Neon operator *(PacketVector3Float64Neon left, PacketVector3Float64Neon right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64Neon operator -(PacketVector3Float64Neon value) => new(-value.X, -value.Y, -value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64NeonMask operator ==(PacketVector3Float64Neon left, PacketVector3Float64Neon right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64NeonMask operator !=(PacketVector3Float64Neon left, PacketVector3Float64Neon right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3Float64Neon other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}

public readonly struct PacketVector3Float64NeonMask :
    ISimdVector3Mask<PacketVector3Float64NeonMask, PacketFloat64NeonMask>
{
    public PacketVector3Float64NeonMask(PacketFloat64NeonMask x, PacketFloat64NeonMask y, PacketFloat64NeonMask z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketFloat64NeonMask.LaneCount;

    public PacketFloat64NeonMask X { get; }

    public PacketFloat64NeonMask Y { get; }

    public PacketFloat64NeonMask Z { get; }

    public static PacketVector3Float64NeonMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat64NeonMask.True, PacketFloat64NeonMask.True, PacketFloat64NeonMask.True);
    }

    public static PacketVector3Float64NeonMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat64NeonMask.False, PacketFloat64NeonMask.False, PacketFloat64NeonMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64NeonMask Create(PacketFloat64NeonMask x, PacketFloat64NeonMask y, PacketFloat64NeonMask z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64NeonMask Broadcast(PacketFloat64NeonMask value) => new(value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask All(PacketVector3Float64NeonMask value) => value.X & value.Y & value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask Any(PacketVector3Float64NeonMask value) => value.X | value.Y | value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat64NeonMask None(PacketVector3Float64NeonMask value) => ~(value.X | value.Y | value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64NeonMask AndNot(PacketVector3Float64NeonMask left, PacketVector3Float64NeonMask right)
    {
        return new(
            PacketFloat64NeonMask.AndNot(left.X, right.X),
            PacketFloat64NeonMask.AndNot(left.Y, right.Y),
            PacketFloat64NeonMask.AndNot(left.Z, right.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64NeonMask Select(PacketVector3Float64NeonMask mask, PacketVector3Float64NeonMask ifTrue, PacketVector3Float64NeonMask ifFalse)
    {
        return new(
            PacketFloat64NeonMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64NeonMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat64NeonMask.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64NeonMask And(PacketVector3Float64NeonMask left, PacketVector3Float64NeonMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64NeonMask Or(PacketVector3Float64NeonMask left, PacketVector3Float64NeonMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64NeonMask Xor(PacketVector3Float64NeonMask left, PacketVector3Float64NeonMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64NeonMask Not(PacketVector3Float64NeonMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64NeonMask operator &(PacketVector3Float64NeonMask left, PacketVector3Float64NeonMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64NeonMask operator |(PacketVector3Float64NeonMask left, PacketVector3Float64NeonMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64NeonMask operator ^(PacketVector3Float64NeonMask left, PacketVector3Float64NeonMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64NeonMask operator ~(PacketVector3Float64NeonMask value) => new(~value.X, ~value.Y, ~value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64NeonMask operator ==(PacketVector3Float64NeonMask left, PacketVector3Float64NeonMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float64NeonMask operator !=(PacketVector3Float64NeonMask left, PacketVector3Float64NeonMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3Float64NeonMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}
