using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
namespace EmbreeSharp.SIMD;

public readonly struct PacketVector3Float32Neon :
    ISimdFloatingPointVector3<PacketVector3Float32Neon, PacketFloat32Neon, float, PacketVector3Float32NeonMask, PacketFloat32NeonMask>
{
    public PacketVector3Float32Neon(PacketFloat32Neon x, PacketFloat32Neon y, PacketFloat32Neon z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketFloat32Neon.LaneCount;

    public PacketFloat32Neon X { get; }

    public PacketFloat32Neon Y { get; }

    public PacketFloat32Neon Z { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Neon Create(PacketFloat32Neon x, PacketFloat32Neon y, PacketFloat32Neon z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Neon Broadcast(float value)
    {
        PacketFloat32Neon packet = PacketFloat32Neon.Broadcast(value);
        return new(packet, packet, packet);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Neon Select(PacketVector3Float32NeonMask mask, PacketVector3Float32Neon ifTrue, PacketVector3Float32Neon ifFalse)
    {
        return new(
            PacketFloat32Neon.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32Neon.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat32Neon.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Neon Select(PacketFloat32NeonMask mask, PacketVector3Float32Neon ifTrue, PacketVector3Float32Neon ifFalse)
    {
        return new(
            PacketFloat32Neon.Select(mask, ifTrue.X, ifFalse.X),
            PacketFloat32Neon.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketFloat32Neon.Select(mask, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32Neon Dot(PacketVector3Float32Neon left, PacketVector3Float32Neon right)
    {
        return PacketFloat32Neon.FusedMultiplyAdd(
            left.Z,
            right.Z,
            PacketFloat32Neon.FusedMultiplyAdd(left.Y, right.Y, left.X * right.X));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Neon Cross(PacketVector3Float32Neon left, PacketVector3Float32Neon right)
    {
        PacketVector3Float32Neon leftZxy = new(left.Z, left.X, left.Y);
        PacketVector3Float32Neon rightYzx = new(right.Y, right.Z, right.X);
        PacketVector3Float32Neon leftYzx = new(left.Y, left.Z, left.X);
        PacketVector3Float32Neon rightZxy = new(right.Z, right.X, right.Y);

        return new(
            new(AdvSimd.FusedMultiplySubtract(
                (leftYzx.X * rightZxy.X)._value,
                leftZxy.X._value,
                rightYzx.X._value)),
            new(AdvSimd.FusedMultiplySubtract(
                (leftYzx.Y * rightZxy.Y)._value,
                leftZxy.Y._value,
                rightYzx.Y._value)),
            new(AdvSimd.FusedMultiplySubtract(
                (leftYzx.Z * rightZxy.Z)._value,
                leftZxy.Z._value,
                rightYzx.Z._value)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Neon operator +(PacketVector3Float32Neon value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Neon operator +(PacketVector3Float32Neon left, PacketVector3Float32Neon right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Neon operator -(PacketVector3Float32Neon left, PacketVector3Float32Neon right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Neon operator *(PacketVector3Float32Neon left, PacketVector3Float32Neon right) => new(left.X * right.X, left.Y * right.Y, left.Z * right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32Neon operator -(PacketVector3Float32Neon value) => new(-value.X, -value.Y, -value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32NeonMask operator ==(PacketVector3Float32Neon left, PacketVector3Float32Neon right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32NeonMask operator !=(PacketVector3Float32Neon left, PacketVector3Float32Neon right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3Float32Neon other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}

public readonly struct PacketVector3Float32NeonMask :
    ISimdVector3Mask<PacketVector3Float32NeonMask, PacketFloat32NeonMask>
{
    public PacketVector3Float32NeonMask(PacketFloat32NeonMask x, PacketFloat32NeonMask y, PacketFloat32NeonMask z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => PacketFloat32NeonMask.LaneCount;

    public PacketFloat32NeonMask X { get; }

    public PacketFloat32NeonMask Y { get; }

    public PacketFloat32NeonMask Z { get; }

    public static PacketVector3Float32NeonMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat32NeonMask.True, PacketFloat32NeonMask.True, PacketFloat32NeonMask.True);
    }

    public static PacketVector3Float32NeonMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketFloat32NeonMask.False, PacketFloat32NeonMask.False, PacketFloat32NeonMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32NeonMask Create(PacketFloat32NeonMask x, PacketFloat32NeonMask y, PacketFloat32NeonMask z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32NeonMask Broadcast(PacketFloat32NeonMask value) => new(value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask All(PacketVector3Float32NeonMask value) => value.X & value.Y & value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask Any(PacketVector3Float32NeonMask value) => value.X | value.Y | value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketFloat32NeonMask None(PacketVector3Float32NeonMask value) => ~(value.X | value.Y | value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32NeonMask AndNot(PacketVector3Float32NeonMask left, PacketVector3Float32NeonMask right)
    {
        return new(
            PacketFloat32NeonMask.AndNot(left.X, right.X),
            PacketFloat32NeonMask.AndNot(left.Y, right.Y),
            PacketFloat32NeonMask.AndNot(left.Z, right.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32NeonMask Select(PacketVector3Float32NeonMask mask, PacketVector3Float32NeonMask ifTrue, PacketVector3Float32NeonMask ifFalse)
    {
        return new(
            PacketFloat32NeonMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32NeonMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat32NeonMask.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32NeonMask And(PacketVector3Float32NeonMask left, PacketVector3Float32NeonMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32NeonMask Or(PacketVector3Float32NeonMask left, PacketVector3Float32NeonMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32NeonMask Xor(PacketVector3Float32NeonMask left, PacketVector3Float32NeonMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32NeonMask Not(PacketVector3Float32NeonMask value) => ~value;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32NeonMask operator &(PacketVector3Float32NeonMask left, PacketVector3Float32NeonMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32NeonMask operator |(PacketVector3Float32NeonMask left, PacketVector3Float32NeonMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32NeonMask operator ^(PacketVector3Float32NeonMask left, PacketVector3Float32NeonMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32NeonMask operator ~(PacketVector3Float32NeonMask value) => new(~value.X, ~value.Y, ~value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32NeonMask operator ==(PacketVector3Float32NeonMask left, PacketVector3Float32NeonMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector3Float32NeonMask operator !=(PacketVector3Float32NeonMask left, PacketVector3Float32NeonMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj)
    {
        return obj is PacketVector3Float32NeonMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}
