namespace EmbreeSharp.SIMD;

public readonly struct PacketQuaternionFloat32Neon :
    ISimdQuaternion<PacketQuaternionFloat32Neon, PacketVector3Float32Neon, PacketVector4Float32Neon, PacketFloat32Neon, float, PacketQuaternionFloat32NeonMask, PacketVector3Float32NeonMask, PacketVector4Float32NeonMask, PacketFloat32NeonMask>
{
    public PacketQuaternionFloat32Neon(PacketFloat32Neon x, PacketFloat32Neon y, PacketFloat32Neon z, PacketFloat32Neon w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketFloat32Neon.LaneCount;

    public PacketFloat32Neon X { get; }

    public PacketFloat32Neon Y { get; }

    public PacketFloat32Neon Z { get; }

    public PacketFloat32Neon W { get; }

    public PacketVector3Float32Neon Imag => new(X, Y, Z);

    public PacketFloat32Neon Real => W;

    public PacketVector4Float32Neon Vector => new(X, Y, Z, W);

    public static PacketQuaternionFloat32Neon Identity
    {
        get
        {
            PacketFloat32Neon zero = PacketFloat32Neon.Broadcast(0f);
            PacketFloat32Neon one = PacketFloat32Neon.Broadcast(1f);
            return new(zero, zero, zero, one);
        }
    }

    public static PacketQuaternionFloat32Neon Create(PacketFloat32Neon x, PacketFloat32Neon y, PacketFloat32Neon z, PacketFloat32Neon w) => new(x, y, z, w);

    public static PacketQuaternionFloat32Neon Create(PacketVector3Float32Neon imag, PacketFloat32Neon real) => new(imag.X, imag.Y, imag.Z, real);

    public static PacketQuaternionFloat32Neon Create(PacketVector4Float32Neon vector) => new(vector.X, vector.Y, vector.Z, vector.W);

    public static PacketQuaternionFloat32Neon Broadcast(float value)
    {
        PacketFloat32Neon zero = PacketFloat32Neon.Broadcast(0f);
        return new(zero, zero, zero, PacketFloat32Neon.Broadcast(value));
    }

    public static PacketQuaternionFloat32Neon Select(PacketQuaternionFloat32NeonMask mask, PacketQuaternionFloat32Neon ifTrue, PacketQuaternionFloat32Neon ifFalse)
    {
        return new(
            PacketFloat32Neon.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32Neon.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat32Neon.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat32Neon.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    public static PacketQuaternionFloat32Neon Select(PacketVector4Float32NeonMask mask, PacketQuaternionFloat32Neon ifTrue, PacketQuaternionFloat32Neon ifFalse)
    {
        return new(
            PacketFloat32Neon.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32Neon.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat32Neon.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat32Neon.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    public static PacketQuaternionFloat32Neon Select(PacketFloat32NeonMask mask, PacketQuaternionFloat32Neon ifTrue, PacketQuaternionFloat32Neon ifFalse)
    {
        return new(
            PacketFloat32Neon.Select(mask, ifTrue.X, ifFalse.X),
            PacketFloat32Neon.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketFloat32Neon.Select(mask, ifTrue.Z, ifFalse.Z),
            PacketFloat32Neon.Select(mask, ifTrue.W, ifFalse.W));
    }

    public static PacketQuaternionFloat32Neon Conjugate(PacketQuaternionFloat32Neon value) => new(-value.X, -value.Y, -value.Z, value.W);

    public static PacketFloat32Neon Dot(PacketQuaternionFloat32Neon left, PacketQuaternionFloat32Neon right) => PacketVector4Float32Neon.Dot(left.Vector, right.Vector);

    public static PacketFloat32Neon SquaredNorm(PacketQuaternionFloat32Neon value) => Dot(value, value);

    public static PacketFloat32Neon Norm(PacketQuaternionFloat32Neon value) => PacketFloat32Neon.Sqrt(SquaredNorm(value));

    public static PacketQuaternionFloat32Neon Normalize(PacketQuaternionFloat32Neon value) => value * PacketFloat32Neon.ReciprocalSqrt(SquaredNorm(value));

    public static PacketQuaternionFloat32Neon Reciprocal(PacketQuaternionFloat32Neon value) => Conjugate(value) * PacketFloat32Neon.Reciprocal(SquaredNorm(value));

    public static PacketQuaternionFloat32Neon Multiply(PacketQuaternionFloat32Neon left, PacketQuaternionFloat32Neon right) => left * right;

    public static PacketQuaternionFloat32Neon Multiply(PacketQuaternionFloat32Neon quaternion, PacketFloat32Neon scalar) => quaternion * scalar;

    public static PacketQuaternionFloat32Neon Multiply(PacketFloat32Neon scalar, PacketQuaternionFloat32Neon quaternion) => quaternion * scalar;

    public static PacketQuaternionFloat32Neon Divide(PacketQuaternionFloat32Neon left, PacketQuaternionFloat32Neon right) => left / right;

    public static PacketQuaternionFloat32Neon Divide(PacketQuaternionFloat32Neon quaternion, PacketFloat32Neon scalar) => quaternion / scalar;

    public static PacketQuaternionFloat32Neon FusedMultiplyAdd(PacketQuaternionFloat32Neon left, PacketQuaternionFloat32Neon right, PacketQuaternionFloat32Neon addend)
    {
        PacketFloat32Neon t1X = PacketFloat32Neon.FusedMultiplyAdd(left.X, right.W, left.Y * right.Z);
        PacketFloat32Neon t1Y = PacketFloat32Neon.FusedMultiplyAdd(left.Y, right.W, left.Z * right.X);
        PacketFloat32Neon t1Z = PacketFloat32Neon.FusedMultiplyAdd(left.Z, right.W, left.X * right.Y);
        PacketFloat32Neon t1W = PacketFloat32Neon.FusedMultiplyAdd(left.X, right.X, left.Y * right.Y);

        PacketFloat32Neon t2X = PacketFloat32Neon.FusedMultiplyAdd(left.W, right.X, PacketFloat32Neon.FusedMultiplyAdd(-left.Z, right.Y, addend.X));
        PacketFloat32Neon t2Y = PacketFloat32Neon.FusedMultiplyAdd(left.W, right.Y, PacketFloat32Neon.FusedMultiplyAdd(-left.X, right.Z, addend.Y));
        PacketFloat32Neon t2Z = PacketFloat32Neon.FusedMultiplyAdd(left.W, right.Z, PacketFloat32Neon.FusedMultiplyAdd(-left.Y, right.X, addend.Z));
        PacketFloat32Neon t2W = PacketFloat32Neon.FusedMultiplyAdd(left.W, right.W, PacketFloat32Neon.FusedMultiplyAdd(-left.Z, right.Z, addend.W));

        return new(t1X + t2X, t1Y + t2Y, t1Z + t2Z, -t1W + t2W);
    }

    public static PacketQuaternionFloat32Neon Rotate(PacketVector3Float32Neon axis, PacketFloat32Neon angle)
    {
        var (sin, cos) = PacketFloat32Neon.SinCos(angle * PacketFloat32Neon.Broadcast(0.5f));
        PacketVector3Float32Neon imag = axis * new PacketVector3Float32Neon(sin, sin, sin);
        return new(imag.X, imag.Y, imag.Z, cos);
    }

    public static PacketVector3Float32Neon Apply(PacketQuaternionFloat32Neon quaternion, PacketVector3Float32Neon vector)
    {
        PacketVector3Float32Neon imag = quaternion.Imag;
        PacketFloat32Neon real = quaternion.Real;
        PacketFloat32Neon two = PacketFloat32Neon.Broadcast(2f);

        PacketFloat32Neon imagDotVector = PacketVector3Float32Neon.Dot(imag, vector);
        PacketFloat32Neon realScale = real * real - PacketVector3Float32Neon.Dot(imag, imag);
        PacketFloat32Neon crossScale = two * real;

        return new PacketVector3Float32Neon(two * imagDotVector, two * imagDotVector, two * imagDotVector) * imag +
               new PacketVector3Float32Neon(realScale, realScale, realScale) * vector +
               new PacketVector3Float32Neon(crossScale, crossScale, crossScale) * PacketVector3Float32Neon.Cross(imag, vector);
    }

    public static PacketQuaternionFloat32Neon operator +(PacketQuaternionFloat32Neon value) => value;

    public static PacketQuaternionFloat32Neon operator +(PacketQuaternionFloat32Neon left, PacketQuaternionFloat32Neon right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    public static PacketQuaternionFloat32Neon operator -(PacketQuaternionFloat32Neon left, PacketQuaternionFloat32Neon right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    public static PacketQuaternionFloat32Neon operator *(PacketQuaternionFloat32Neon left, PacketQuaternionFloat32Neon right)
    {
        PacketFloat32Neon t1X = PacketFloat32Neon.FusedMultiplyAdd(left.X, right.W, left.Y * right.Z);
        PacketFloat32Neon t1Y = PacketFloat32Neon.FusedMultiplyAdd(left.Y, right.W, left.Z * right.X);
        PacketFloat32Neon t1Z = PacketFloat32Neon.FusedMultiplyAdd(left.Z, right.W, left.X * right.Y);
        PacketFloat32Neon t1W = PacketFloat32Neon.FusedMultiplyAdd(left.X, right.X, left.Y * right.Y);

        PacketFloat32Neon t2X = PacketFloat32Neon.FusedMultiplyAdd(left.W, right.X, -(left.Z * right.Y));
        PacketFloat32Neon t2Y = PacketFloat32Neon.FusedMultiplyAdd(left.W, right.Y, -(left.X * right.Z));
        PacketFloat32Neon t2Z = PacketFloat32Neon.FusedMultiplyAdd(left.W, right.Z, -(left.Y * right.X));
        PacketFloat32Neon t2W = PacketFloat32Neon.FusedMultiplyAdd(left.W, right.W, -(left.Z * right.Z));

        return new(t1X + t2X, t1Y + t2Y, t1Z + t2Z, -t1W + t2W);
    }

    public static PacketQuaternionFloat32Neon operator *(PacketQuaternionFloat32Neon quaternion, PacketFloat32Neon scalar)
    {
        PacketVector4Float32Neon scalarVector = new(scalar, scalar, scalar, scalar);
        return Create(quaternion.Vector * scalarVector);
    }

    public static PacketQuaternionFloat32Neon operator *(PacketFloat32Neon scalar, PacketQuaternionFloat32Neon quaternion) => quaternion * scalar;

    public static PacketQuaternionFloat32Neon operator /(PacketQuaternionFloat32Neon left, PacketQuaternionFloat32Neon right) => left * Reciprocal(right);

    public static PacketQuaternionFloat32Neon operator /(PacketQuaternionFloat32Neon quaternion, PacketFloat32Neon scalar) => quaternion * PacketFloat32Neon.Reciprocal(scalar);

    public static PacketQuaternionFloat32Neon operator -(PacketQuaternionFloat32Neon value) => new(-value.X, -value.Y, -value.Z, -value.W);

    public static PacketQuaternionFloat32NeonMask operator ==(PacketQuaternionFloat32Neon left, PacketQuaternionFloat32Neon right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    public static PacketQuaternionFloat32NeonMask operator !=(PacketQuaternionFloat32Neon left, PacketQuaternionFloat32Neon right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketQuaternionFloat32Neon other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct PacketQuaternionFloat32NeonMask :
    ISimdQuaternionMask<PacketQuaternionFloat32NeonMask, PacketFloat32NeonMask>
{
    public PacketQuaternionFloat32NeonMask(PacketFloat32NeonMask x, PacketFloat32NeonMask y, PacketFloat32NeonMask z, PacketFloat32NeonMask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketFloat32NeonMask.LaneCount;

    public PacketFloat32NeonMask X { get; }

    public PacketFloat32NeonMask Y { get; }

    public PacketFloat32NeonMask Z { get; }

    public PacketFloat32NeonMask W { get; }

    public static PacketQuaternionFloat32NeonMask True => new(PacketFloat32NeonMask.True, PacketFloat32NeonMask.True, PacketFloat32NeonMask.True, PacketFloat32NeonMask.True);

    public static PacketQuaternionFloat32NeonMask False => new(PacketFloat32NeonMask.False, PacketFloat32NeonMask.False, PacketFloat32NeonMask.False, PacketFloat32NeonMask.False);

    public static PacketQuaternionFloat32NeonMask Create(PacketFloat32NeonMask x, PacketFloat32NeonMask y, PacketFloat32NeonMask z, PacketFloat32NeonMask w) => new(x, y, z, w);

    public static PacketQuaternionFloat32NeonMask Broadcast(PacketFloat32NeonMask value) => new(value, value, value, value);

    public static PacketFloat32NeonMask All(PacketQuaternionFloat32NeonMask value) => value.X & value.Y & value.Z & value.W;

    public static PacketFloat32NeonMask Any(PacketQuaternionFloat32NeonMask value) => value.X | value.Y | value.Z | value.W;

    public static PacketFloat32NeonMask None(PacketQuaternionFloat32NeonMask value) => ~(value.X | value.Y | value.Z | value.W);

    public static PacketQuaternionFloat32NeonMask AndNot(PacketQuaternionFloat32NeonMask left, PacketQuaternionFloat32NeonMask right)
    {
        return new(
            PacketFloat32NeonMask.AndNot(left.X, right.X),
            PacketFloat32NeonMask.AndNot(left.Y, right.Y),
            PacketFloat32NeonMask.AndNot(left.Z, right.Z),
            PacketFloat32NeonMask.AndNot(left.W, right.W));
    }

    public static PacketQuaternionFloat32NeonMask Select(PacketQuaternionFloat32NeonMask mask, PacketQuaternionFloat32NeonMask ifTrue, PacketQuaternionFloat32NeonMask ifFalse)
    {
        return new(
            PacketFloat32NeonMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32NeonMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat32NeonMask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat32NeonMask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    public static PacketQuaternionFloat32NeonMask And(PacketQuaternionFloat32NeonMask left, PacketQuaternionFloat32NeonMask right) => left & right;

    public static PacketQuaternionFloat32NeonMask Or(PacketQuaternionFloat32NeonMask left, PacketQuaternionFloat32NeonMask right) => left | right;

    public static PacketQuaternionFloat32NeonMask Xor(PacketQuaternionFloat32NeonMask left, PacketQuaternionFloat32NeonMask right) => left ^ right;

    public static PacketQuaternionFloat32NeonMask Not(PacketQuaternionFloat32NeonMask value) => ~value;

    public static PacketQuaternionFloat32NeonMask operator &(PacketQuaternionFloat32NeonMask left, PacketQuaternionFloat32NeonMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    public static PacketQuaternionFloat32NeonMask operator |(PacketQuaternionFloat32NeonMask left, PacketQuaternionFloat32NeonMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    public static PacketQuaternionFloat32NeonMask operator ^(PacketQuaternionFloat32NeonMask left, PacketQuaternionFloat32NeonMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    public static PacketQuaternionFloat32NeonMask operator ~(PacketQuaternionFloat32NeonMask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    public static PacketQuaternionFloat32NeonMask operator ==(PacketQuaternionFloat32NeonMask left, PacketQuaternionFloat32NeonMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    public static PacketQuaternionFloat32NeonMask operator !=(PacketQuaternionFloat32NeonMask left, PacketQuaternionFloat32NeonMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketQuaternionFloat32NeonMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}
