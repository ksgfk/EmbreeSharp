namespace EmbreeSharp.SIMD;

public readonly struct PacketQuaternionFloat64Neon :
    ISimdQuaternion<PacketQuaternionFloat64Neon, PacketVector3Float64Neon, PacketVector4Float64Neon, PacketFloat64Neon, double, PacketQuaternionFloat64NeonMask, PacketVector3Float64NeonMask, PacketVector4Float64NeonMask, PacketFloat64NeonMask>
{
    public PacketQuaternionFloat64Neon(PacketFloat64Neon x, PacketFloat64Neon y, PacketFloat64Neon z, PacketFloat64Neon w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketFloat64Neon.LaneCount;

    public PacketFloat64Neon X { get; }

    public PacketFloat64Neon Y { get; }

    public PacketFloat64Neon Z { get; }

    public PacketFloat64Neon W { get; }

    public PacketVector3Float64Neon Imag => new(X, Y, Z);

    public PacketFloat64Neon Real => W;

    public PacketVector4Float64Neon Vector => new(X, Y, Z, W);

    public static PacketQuaternionFloat64Neon Identity
    {
        get
        {
            PacketFloat64Neon zero = PacketFloat64Neon.Broadcast(0d);
            PacketFloat64Neon one = PacketFloat64Neon.Broadcast(1d);
            return new(zero, zero, zero, one);
        }
    }

    public static PacketQuaternionFloat64Neon Create(PacketFloat64Neon x, PacketFloat64Neon y, PacketFloat64Neon z, PacketFloat64Neon w) => new(x, y, z, w);

    public static PacketQuaternionFloat64Neon Create(PacketVector3Float64Neon imag, PacketFloat64Neon real) => new(imag.X, imag.Y, imag.Z, real);

    public static PacketQuaternionFloat64Neon Create(PacketVector4Float64Neon vector) => new(vector.X, vector.Y, vector.Z, vector.W);

    public static PacketQuaternionFloat64Neon Broadcast(double value)
    {
        PacketFloat64Neon zero = PacketFloat64Neon.Broadcast(0d);
        return new(zero, zero, zero, PacketFloat64Neon.Broadcast(value));
    }

    public static PacketQuaternionFloat64Neon Select(PacketQuaternionFloat64NeonMask mask, PacketQuaternionFloat64Neon ifTrue, PacketQuaternionFloat64Neon ifFalse)
    {
        return new(
            PacketFloat64Neon.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64Neon.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat64Neon.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat64Neon.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    public static PacketQuaternionFloat64Neon Select(PacketVector4Float64NeonMask mask, PacketQuaternionFloat64Neon ifTrue, PacketQuaternionFloat64Neon ifFalse)
    {
        return new(
            PacketFloat64Neon.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64Neon.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat64Neon.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat64Neon.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    public static PacketQuaternionFloat64Neon Select(PacketFloat64NeonMask mask, PacketQuaternionFloat64Neon ifTrue, PacketQuaternionFloat64Neon ifFalse)
    {
        return new(
            PacketFloat64Neon.Select(mask, ifTrue.X, ifFalse.X),
            PacketFloat64Neon.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketFloat64Neon.Select(mask, ifTrue.Z, ifFalse.Z),
            PacketFloat64Neon.Select(mask, ifTrue.W, ifFalse.W));
    }

    public static PacketQuaternionFloat64Neon Conjugate(PacketQuaternionFloat64Neon value) => new(-value.X, -value.Y, -value.Z, value.W);

    public static PacketFloat64Neon Dot(PacketQuaternionFloat64Neon left, PacketQuaternionFloat64Neon right) => PacketVector4Float64Neon.Dot(left.Vector, right.Vector);

    public static PacketFloat64Neon SquaredNorm(PacketQuaternionFloat64Neon value) => Dot(value, value);

    public static PacketFloat64Neon Norm(PacketQuaternionFloat64Neon value) => PacketFloat64Neon.Sqrt(SquaredNorm(value));

    public static PacketQuaternionFloat64Neon Normalize(PacketQuaternionFloat64Neon value) => value * PacketFloat64Neon.ReciprocalSqrt(SquaredNorm(value));

    public static PacketQuaternionFloat64Neon Reciprocal(PacketQuaternionFloat64Neon value) => Conjugate(value) * PacketFloat64Neon.Reciprocal(SquaredNorm(value));

    public static PacketQuaternionFloat64Neon Multiply(PacketQuaternionFloat64Neon left, PacketQuaternionFloat64Neon right) => left * right;

    public static PacketQuaternionFloat64Neon Multiply(PacketQuaternionFloat64Neon quaternion, PacketFloat64Neon scalar) => quaternion * scalar;

    public static PacketQuaternionFloat64Neon Multiply(PacketFloat64Neon scalar, PacketQuaternionFloat64Neon quaternion) => quaternion * scalar;

    public static PacketQuaternionFloat64Neon Divide(PacketQuaternionFloat64Neon left, PacketQuaternionFloat64Neon right) => left / right;

    public static PacketQuaternionFloat64Neon Divide(PacketQuaternionFloat64Neon quaternion, PacketFloat64Neon scalar) => quaternion / scalar;

    public static PacketQuaternionFloat64Neon FusedMultiplyAdd(PacketQuaternionFloat64Neon left, PacketQuaternionFloat64Neon right, PacketQuaternionFloat64Neon addend)
    {
        PacketFloat64Neon t1X = PacketFloat64Neon.FusedMultiplyAdd(left.X, right.W, left.Y * right.Z);
        PacketFloat64Neon t1Y = PacketFloat64Neon.FusedMultiplyAdd(left.Y, right.W, left.Z * right.X);
        PacketFloat64Neon t1Z = PacketFloat64Neon.FusedMultiplyAdd(left.Z, right.W, left.X * right.Y);
        PacketFloat64Neon t1W = PacketFloat64Neon.FusedMultiplyAdd(left.X, right.X, left.Y * right.Y);

        PacketFloat64Neon t2X = PacketFloat64Neon.FusedMultiplyAdd(left.W, right.X, PacketFloat64Neon.FusedMultiplyAdd(-left.Z, right.Y, addend.X));
        PacketFloat64Neon t2Y = PacketFloat64Neon.FusedMultiplyAdd(left.W, right.Y, PacketFloat64Neon.FusedMultiplyAdd(-left.X, right.Z, addend.Y));
        PacketFloat64Neon t2Z = PacketFloat64Neon.FusedMultiplyAdd(left.W, right.Z, PacketFloat64Neon.FusedMultiplyAdd(-left.Y, right.X, addend.Z));
        PacketFloat64Neon t2W = PacketFloat64Neon.FusedMultiplyAdd(left.W, right.W, PacketFloat64Neon.FusedMultiplyAdd(-left.Z, right.Z, addend.W));

        return new(t1X + t2X, t1Y + t2Y, t1Z + t2Z, -t1W + t2W);
    }

    public static PacketQuaternionFloat64Neon Rotate(PacketVector3Float64Neon axis, PacketFloat64Neon angle)
    {
        var (sin, cos) = PacketFloat64Neon.SinCos(angle * PacketFloat64Neon.Broadcast(0.5d));
        PacketVector3Float64Neon imag = axis * new PacketVector3Float64Neon(sin, sin, sin);
        return new(imag.X, imag.Y, imag.Z, cos);
    }

    public static PacketVector3Float64Neon Apply(PacketQuaternionFloat64Neon quaternion, PacketVector3Float64Neon vector)
    {
        PacketVector3Float64Neon imag = quaternion.Imag;
        PacketFloat64Neon real = quaternion.Real;
        PacketFloat64Neon two = PacketFloat64Neon.Broadcast(2d);

        PacketFloat64Neon imagDotVector = PacketVector3Float64Neon.Dot(imag, vector);
        PacketFloat64Neon realScale = real * real - PacketVector3Float64Neon.Dot(imag, imag);
        PacketFloat64Neon crossScale = two * real;

        return new PacketVector3Float64Neon(two * imagDotVector, two * imagDotVector, two * imagDotVector) * imag +
               new PacketVector3Float64Neon(realScale, realScale, realScale) * vector +
               new PacketVector3Float64Neon(crossScale, crossScale, crossScale) * PacketVector3Float64Neon.Cross(imag, vector);
    }

    public static PacketQuaternionFloat64Neon operator +(PacketQuaternionFloat64Neon value) => value;

    public static PacketQuaternionFloat64Neon operator +(PacketQuaternionFloat64Neon left, PacketQuaternionFloat64Neon right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    public static PacketQuaternionFloat64Neon operator -(PacketQuaternionFloat64Neon left, PacketQuaternionFloat64Neon right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    public static PacketQuaternionFloat64Neon operator *(PacketQuaternionFloat64Neon left, PacketQuaternionFloat64Neon right)
    {
        PacketFloat64Neon t1X = PacketFloat64Neon.FusedMultiplyAdd(left.X, right.W, left.Y * right.Z);
        PacketFloat64Neon t1Y = PacketFloat64Neon.FusedMultiplyAdd(left.Y, right.W, left.Z * right.X);
        PacketFloat64Neon t1Z = PacketFloat64Neon.FusedMultiplyAdd(left.Z, right.W, left.X * right.Y);
        PacketFloat64Neon t1W = PacketFloat64Neon.FusedMultiplyAdd(left.X, right.X, left.Y * right.Y);

        PacketFloat64Neon t2X = PacketFloat64Neon.FusedMultiplyAdd(left.W, right.X, -(left.Z * right.Y));
        PacketFloat64Neon t2Y = PacketFloat64Neon.FusedMultiplyAdd(left.W, right.Y, -(left.X * right.Z));
        PacketFloat64Neon t2Z = PacketFloat64Neon.FusedMultiplyAdd(left.W, right.Z, -(left.Y * right.X));
        PacketFloat64Neon t2W = PacketFloat64Neon.FusedMultiplyAdd(left.W, right.W, -(left.Z * right.Z));

        return new(t1X + t2X, t1Y + t2Y, t1Z + t2Z, -t1W + t2W);
    }

    public static PacketQuaternionFloat64Neon operator *(PacketQuaternionFloat64Neon quaternion, PacketFloat64Neon scalar)
    {
        PacketVector4Float64Neon scalarVector = new(scalar, scalar, scalar, scalar);
        return Create(quaternion.Vector * scalarVector);
    }

    public static PacketQuaternionFloat64Neon operator *(PacketFloat64Neon scalar, PacketQuaternionFloat64Neon quaternion) => quaternion * scalar;

    public static PacketQuaternionFloat64Neon operator /(PacketQuaternionFloat64Neon left, PacketQuaternionFloat64Neon right) => left * Reciprocal(right);

    public static PacketQuaternionFloat64Neon operator /(PacketQuaternionFloat64Neon quaternion, PacketFloat64Neon scalar) => quaternion * PacketFloat64Neon.Reciprocal(scalar);

    public static PacketQuaternionFloat64Neon operator -(PacketQuaternionFloat64Neon value) => new(-value.X, -value.Y, -value.Z, -value.W);

    public static PacketQuaternionFloat64NeonMask operator ==(PacketQuaternionFloat64Neon left, PacketQuaternionFloat64Neon right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    public static PacketQuaternionFloat64NeonMask operator !=(PacketQuaternionFloat64Neon left, PacketQuaternionFloat64Neon right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketQuaternionFloat64Neon other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct PacketQuaternionFloat64NeonMask :
    ISimdQuaternionMask<PacketQuaternionFloat64NeonMask, PacketFloat64NeonMask>
{
    public PacketQuaternionFloat64NeonMask(PacketFloat64NeonMask x, PacketFloat64NeonMask y, PacketFloat64NeonMask z, PacketFloat64NeonMask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketFloat64NeonMask.LaneCount;

    public PacketFloat64NeonMask X { get; }

    public PacketFloat64NeonMask Y { get; }

    public PacketFloat64NeonMask Z { get; }

    public PacketFloat64NeonMask W { get; }

    public static PacketQuaternionFloat64NeonMask True => new(PacketFloat64NeonMask.True, PacketFloat64NeonMask.True, PacketFloat64NeonMask.True, PacketFloat64NeonMask.True);

    public static PacketQuaternionFloat64NeonMask False => new(PacketFloat64NeonMask.False, PacketFloat64NeonMask.False, PacketFloat64NeonMask.False, PacketFloat64NeonMask.False);

    public static PacketQuaternionFloat64NeonMask Create(PacketFloat64NeonMask x, PacketFloat64NeonMask y, PacketFloat64NeonMask z, PacketFloat64NeonMask w) => new(x, y, z, w);

    public static PacketQuaternionFloat64NeonMask Broadcast(PacketFloat64NeonMask value) => new(value, value, value, value);

    public static PacketFloat64NeonMask All(PacketQuaternionFloat64NeonMask value) => value.X & value.Y & value.Z & value.W;

    public static PacketFloat64NeonMask Any(PacketQuaternionFloat64NeonMask value) => value.X | value.Y | value.Z | value.W;

    public static PacketFloat64NeonMask None(PacketQuaternionFloat64NeonMask value) => ~(value.X | value.Y | value.Z | value.W);

    public static PacketQuaternionFloat64NeonMask AndNot(PacketQuaternionFloat64NeonMask left, PacketQuaternionFloat64NeonMask right)
    {
        return new(
            PacketFloat64NeonMask.AndNot(left.X, right.X),
            PacketFloat64NeonMask.AndNot(left.Y, right.Y),
            PacketFloat64NeonMask.AndNot(left.Z, right.Z),
            PacketFloat64NeonMask.AndNot(left.W, right.W));
    }

    public static PacketQuaternionFloat64NeonMask Select(PacketQuaternionFloat64NeonMask mask, PacketQuaternionFloat64NeonMask ifTrue, PacketQuaternionFloat64NeonMask ifFalse)
    {
        return new(
            PacketFloat64NeonMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64NeonMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat64NeonMask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat64NeonMask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    public static PacketQuaternionFloat64NeonMask And(PacketQuaternionFloat64NeonMask left, PacketQuaternionFloat64NeonMask right) => left & right;

    public static PacketQuaternionFloat64NeonMask Or(PacketQuaternionFloat64NeonMask left, PacketQuaternionFloat64NeonMask right) => left | right;

    public static PacketQuaternionFloat64NeonMask Xor(PacketQuaternionFloat64NeonMask left, PacketQuaternionFloat64NeonMask right) => left ^ right;

    public static PacketQuaternionFloat64NeonMask Not(PacketQuaternionFloat64NeonMask value) => ~value;

    public static PacketQuaternionFloat64NeonMask operator &(PacketQuaternionFloat64NeonMask left, PacketQuaternionFloat64NeonMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    public static PacketQuaternionFloat64NeonMask operator |(PacketQuaternionFloat64NeonMask left, PacketQuaternionFloat64NeonMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    public static PacketQuaternionFloat64NeonMask operator ^(PacketQuaternionFloat64NeonMask left, PacketQuaternionFloat64NeonMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    public static PacketQuaternionFloat64NeonMask operator ~(PacketQuaternionFloat64NeonMask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    public static PacketQuaternionFloat64NeonMask operator ==(PacketQuaternionFloat64NeonMask left, PacketQuaternionFloat64NeonMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    public static PacketQuaternionFloat64NeonMask operator !=(PacketQuaternionFloat64NeonMask left, PacketQuaternionFloat64NeonMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketQuaternionFloat64NeonMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}
