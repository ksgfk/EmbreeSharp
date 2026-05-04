namespace EmbreeSharp.SIMD;

public readonly struct PacketQuaternionFloat64Avx :
    ISimdQuaternion<PacketQuaternionFloat64Avx, PacketVector3Float64Avx, PacketVector4Float64Avx, PacketFloat64Avx, double, PacketQuaternionFloat64AvxMask, PacketVector3Float64AvxMask, PacketVector4Float64AvxMask, PacketFloat64AvxMask>
{
    public PacketQuaternionFloat64Avx(PacketFloat64Avx x, PacketFloat64Avx y, PacketFloat64Avx z, PacketFloat64Avx w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketFloat64Avx.LaneCount;

    public PacketFloat64Avx X { get; }

    public PacketFloat64Avx Y { get; }

    public PacketFloat64Avx Z { get; }

    public PacketFloat64Avx W { get; }

    public PacketVector3Float64Avx Imag => new(X, Y, Z);

    public PacketFloat64Avx Real => W;

    public PacketVector4Float64Avx Vector => new(X, Y, Z, W);

    public static PacketQuaternionFloat64Avx Identity
    {
        get
        {
            PacketFloat64Avx zero = PacketFloat64Avx.Broadcast(0d);
            PacketFloat64Avx one = PacketFloat64Avx.Broadcast(1d);
            return new(zero, zero, zero, one);
        }
    }

    public static PacketQuaternionFloat64Avx Create(PacketFloat64Avx x, PacketFloat64Avx y, PacketFloat64Avx z, PacketFloat64Avx w) => new(x, y, z, w);

    public static PacketQuaternionFloat64Avx Create(PacketVector3Float64Avx imag, PacketFloat64Avx real) => new(imag.X, imag.Y, imag.Z, real);

    public static PacketQuaternionFloat64Avx Create(PacketVector4Float64Avx vector) => new(vector.X, vector.Y, vector.Z, vector.W);

    public static PacketQuaternionFloat64Avx Broadcast(double value)
    {
        PacketFloat64Avx zero = PacketFloat64Avx.Broadcast(0d);
        return new(zero, zero, zero, PacketFloat64Avx.Broadcast(value));
    }

    public static PacketQuaternionFloat64Avx Select(PacketQuaternionFloat64AvxMask mask, PacketQuaternionFloat64Avx ifTrue, PacketQuaternionFloat64Avx ifFalse)
    {
        return new(
            PacketFloat64Avx.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64Avx.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat64Avx.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat64Avx.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    public static PacketQuaternionFloat64Avx Select(PacketVector4Float64AvxMask mask, PacketQuaternionFloat64Avx ifTrue, PacketQuaternionFloat64Avx ifFalse)
    {
        return new(
            PacketFloat64Avx.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64Avx.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat64Avx.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat64Avx.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    public static PacketQuaternionFloat64Avx Select(PacketFloat64AvxMask mask, PacketQuaternionFloat64Avx ifTrue, PacketQuaternionFloat64Avx ifFalse)
    {
        return new(
            PacketFloat64Avx.Select(mask, ifTrue.X, ifFalse.X),
            PacketFloat64Avx.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketFloat64Avx.Select(mask, ifTrue.Z, ifFalse.Z),
            PacketFloat64Avx.Select(mask, ifTrue.W, ifFalse.W));
    }

    public static PacketQuaternionFloat64Avx Conjugate(PacketQuaternionFloat64Avx value) => new(-value.X, -value.Y, -value.Z, value.W);

    public static PacketFloat64Avx Dot(PacketQuaternionFloat64Avx left, PacketQuaternionFloat64Avx right) => PacketVector4Float64Avx.Dot(left.Vector, right.Vector);

    public static PacketFloat64Avx SquaredNorm(PacketQuaternionFloat64Avx value) => Dot(value, value);

    public static PacketFloat64Avx Norm(PacketQuaternionFloat64Avx value) => PacketFloat64Avx.Sqrt(SquaredNorm(value));

    public static PacketQuaternionFloat64Avx Normalize(PacketQuaternionFloat64Avx value) => value * PacketFloat64Avx.ReciprocalSqrt(SquaredNorm(value));

    public static PacketQuaternionFloat64Avx Reciprocal(PacketQuaternionFloat64Avx value) => Conjugate(value) * PacketFloat64Avx.Reciprocal(SquaredNorm(value));

    public static PacketQuaternionFloat64Avx Multiply(PacketQuaternionFloat64Avx left, PacketQuaternionFloat64Avx right) => left * right;

    public static PacketQuaternionFloat64Avx Multiply(PacketQuaternionFloat64Avx quaternion, PacketFloat64Avx scalar) => quaternion * scalar;

    public static PacketQuaternionFloat64Avx Multiply(PacketFloat64Avx scalar, PacketQuaternionFloat64Avx quaternion) => quaternion * scalar;

    public static PacketQuaternionFloat64Avx Divide(PacketQuaternionFloat64Avx left, PacketQuaternionFloat64Avx right) => left / right;

    public static PacketQuaternionFloat64Avx Divide(PacketQuaternionFloat64Avx quaternion, PacketFloat64Avx scalar) => quaternion / scalar;

    public static PacketQuaternionFloat64Avx FusedMultiplyAdd(PacketQuaternionFloat64Avx left, PacketQuaternionFloat64Avx right, PacketQuaternionFloat64Avx addend)
    {
        PacketFloat64Avx t1X = PacketFloat64Avx.FusedMultiplyAdd(left.X, right.W, left.Y * right.Z);
        PacketFloat64Avx t1Y = PacketFloat64Avx.FusedMultiplyAdd(left.Y, right.W, left.Z * right.X);
        PacketFloat64Avx t1Z = PacketFloat64Avx.FusedMultiplyAdd(left.Z, right.W, left.X * right.Y);
        PacketFloat64Avx t1W = PacketFloat64Avx.FusedMultiplyAdd(left.X, right.X, left.Y * right.Y);

        PacketFloat64Avx t2X = PacketFloat64Avx.FusedMultiplyAdd(left.W, right.X, PacketFloat64Avx.FusedMultiplyAdd(-left.Z, right.Y, addend.X));
        PacketFloat64Avx t2Y = PacketFloat64Avx.FusedMultiplyAdd(left.W, right.Y, PacketFloat64Avx.FusedMultiplyAdd(-left.X, right.Z, addend.Y));
        PacketFloat64Avx t2Z = PacketFloat64Avx.FusedMultiplyAdd(left.W, right.Z, PacketFloat64Avx.FusedMultiplyAdd(-left.Y, right.X, addend.Z));
        PacketFloat64Avx t2W = PacketFloat64Avx.FusedMultiplyAdd(left.W, right.W, PacketFloat64Avx.FusedMultiplyAdd(-left.Z, right.Z, addend.W));

        return new(t1X + t2X, t1Y + t2Y, t1Z + t2Z, -t1W + t2W);
    }

    public static PacketQuaternionFloat64Avx Rotate(PacketVector3Float64Avx axis, PacketFloat64Avx angle)
    {
        var (sin, cos) = PacketFloat64Avx.SinCos(angle * PacketFloat64Avx.Broadcast(0.5d));
        PacketVector3Float64Avx imag = axis * new PacketVector3Float64Avx(sin, sin, sin);
        return new(imag.X, imag.Y, imag.Z, cos);
    }

    public static PacketVector3Float64Avx Apply(PacketQuaternionFloat64Avx quaternion, PacketVector3Float64Avx vector)
    {
        PacketVector3Float64Avx imag = quaternion.Imag;
        PacketFloat64Avx real = quaternion.Real;
        PacketFloat64Avx two = PacketFloat64Avx.Broadcast(2d);

        PacketFloat64Avx imagDotVector = PacketVector3Float64Avx.Dot(imag, vector);
        PacketFloat64Avx realScale = real * real - PacketVector3Float64Avx.Dot(imag, imag);
        PacketFloat64Avx crossScale = two * real;

        return new PacketVector3Float64Avx(two * imagDotVector, two * imagDotVector, two * imagDotVector) * imag +
               new PacketVector3Float64Avx(realScale, realScale, realScale) * vector +
               new PacketVector3Float64Avx(crossScale, crossScale, crossScale) * PacketVector3Float64Avx.Cross(imag, vector);
    }

    public static PacketQuaternionFloat64Avx operator +(PacketQuaternionFloat64Avx value) => value;

    public static PacketQuaternionFloat64Avx operator +(PacketQuaternionFloat64Avx left, PacketQuaternionFloat64Avx right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    public static PacketQuaternionFloat64Avx operator -(PacketQuaternionFloat64Avx left, PacketQuaternionFloat64Avx right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    public static PacketQuaternionFloat64Avx operator *(PacketQuaternionFloat64Avx left, PacketQuaternionFloat64Avx right)
    {
        PacketFloat64Avx t1X = PacketFloat64Avx.FusedMultiplyAdd(left.X, right.W, left.Y * right.Z);
        PacketFloat64Avx t1Y = PacketFloat64Avx.FusedMultiplyAdd(left.Y, right.W, left.Z * right.X);
        PacketFloat64Avx t1Z = PacketFloat64Avx.FusedMultiplyAdd(left.Z, right.W, left.X * right.Y);
        PacketFloat64Avx t1W = PacketFloat64Avx.FusedMultiplyAdd(left.X, right.X, left.Y * right.Y);

        PacketFloat64Avx t2X = PacketFloat64Avx.FusedMultiplyAdd(left.W, right.X, -(left.Z * right.Y));
        PacketFloat64Avx t2Y = PacketFloat64Avx.FusedMultiplyAdd(left.W, right.Y, -(left.X * right.Z));
        PacketFloat64Avx t2Z = PacketFloat64Avx.FusedMultiplyAdd(left.W, right.Z, -(left.Y * right.X));
        PacketFloat64Avx t2W = PacketFloat64Avx.FusedMultiplyAdd(left.W, right.W, -(left.Z * right.Z));

        return new(t1X + t2X, t1Y + t2Y, t1Z + t2Z, -t1W + t2W);
    }

    public static PacketQuaternionFloat64Avx operator *(PacketQuaternionFloat64Avx quaternion, PacketFloat64Avx scalar)
    {
        PacketVector4Float64Avx scalarVector = new(scalar, scalar, scalar, scalar);
        return Create(quaternion.Vector * scalarVector);
    }

    public static PacketQuaternionFloat64Avx operator *(PacketFloat64Avx scalar, PacketQuaternionFloat64Avx quaternion) => quaternion * scalar;

    public static PacketQuaternionFloat64Avx operator /(PacketQuaternionFloat64Avx left, PacketQuaternionFloat64Avx right) => left * Reciprocal(right);

    public static PacketQuaternionFloat64Avx operator /(PacketQuaternionFloat64Avx quaternion, PacketFloat64Avx scalar) => quaternion * PacketFloat64Avx.Reciprocal(scalar);

    public static PacketQuaternionFloat64Avx operator -(PacketQuaternionFloat64Avx value) => new(-value.X, -value.Y, -value.Z, -value.W);

    public static PacketQuaternionFloat64AvxMask operator ==(PacketQuaternionFloat64Avx left, PacketQuaternionFloat64Avx right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    public static PacketQuaternionFloat64AvxMask operator !=(PacketQuaternionFloat64Avx left, PacketQuaternionFloat64Avx right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketQuaternionFloat64Avx other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct PacketQuaternionFloat64AvxMask :
    ISimdQuaternionMask<PacketQuaternionFloat64AvxMask, PacketFloat64AvxMask>
{
    public PacketQuaternionFloat64AvxMask(PacketFloat64AvxMask x, PacketFloat64AvxMask y, PacketFloat64AvxMask z, PacketFloat64AvxMask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketFloat64AvxMask.LaneCount;

    public PacketFloat64AvxMask X { get; }

    public PacketFloat64AvxMask Y { get; }

    public PacketFloat64AvxMask Z { get; }

    public PacketFloat64AvxMask W { get; }

    public static PacketQuaternionFloat64AvxMask True => new(PacketFloat64AvxMask.True, PacketFloat64AvxMask.True, PacketFloat64AvxMask.True, PacketFloat64AvxMask.True);

    public static PacketQuaternionFloat64AvxMask False => new(PacketFloat64AvxMask.False, PacketFloat64AvxMask.False, PacketFloat64AvxMask.False, PacketFloat64AvxMask.False);

    public static PacketQuaternionFloat64AvxMask Create(PacketFloat64AvxMask x, PacketFloat64AvxMask y, PacketFloat64AvxMask z, PacketFloat64AvxMask w) => new(x, y, z, w);

    public static PacketQuaternionFloat64AvxMask Broadcast(PacketFloat64AvxMask value) => new(value, value, value, value);

    public static PacketFloat64AvxMask All(PacketQuaternionFloat64AvxMask value) => value.X & value.Y & value.Z & value.W;

    public static PacketFloat64AvxMask Any(PacketQuaternionFloat64AvxMask value) => value.X | value.Y | value.Z | value.W;

    public static PacketFloat64AvxMask None(PacketQuaternionFloat64AvxMask value) => ~(value.X | value.Y | value.Z | value.W);

    public static PacketQuaternionFloat64AvxMask AndNot(PacketQuaternionFloat64AvxMask left, PacketQuaternionFloat64AvxMask right)
    {
        return new(
            PacketFloat64AvxMask.AndNot(left.X, right.X),
            PacketFloat64AvxMask.AndNot(left.Y, right.Y),
            PacketFloat64AvxMask.AndNot(left.Z, right.Z),
            PacketFloat64AvxMask.AndNot(left.W, right.W));
    }

    public static PacketQuaternionFloat64AvxMask Select(PacketQuaternionFloat64AvxMask mask, PacketQuaternionFloat64AvxMask ifTrue, PacketQuaternionFloat64AvxMask ifFalse)
    {
        return new(
            PacketFloat64AvxMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64AvxMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat64AvxMask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat64AvxMask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    public static PacketQuaternionFloat64AvxMask And(PacketQuaternionFloat64AvxMask left, PacketQuaternionFloat64AvxMask right) => left & right;

    public static PacketQuaternionFloat64AvxMask Or(PacketQuaternionFloat64AvxMask left, PacketQuaternionFloat64AvxMask right) => left | right;

    public static PacketQuaternionFloat64AvxMask Xor(PacketQuaternionFloat64AvxMask left, PacketQuaternionFloat64AvxMask right) => left ^ right;

    public static PacketQuaternionFloat64AvxMask Not(PacketQuaternionFloat64AvxMask value) => ~value;

    public static PacketQuaternionFloat64AvxMask operator &(PacketQuaternionFloat64AvxMask left, PacketQuaternionFloat64AvxMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    public static PacketQuaternionFloat64AvxMask operator |(PacketQuaternionFloat64AvxMask left, PacketQuaternionFloat64AvxMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    public static PacketQuaternionFloat64AvxMask operator ^(PacketQuaternionFloat64AvxMask left, PacketQuaternionFloat64AvxMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    public static PacketQuaternionFloat64AvxMask operator ~(PacketQuaternionFloat64AvxMask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    public static PacketQuaternionFloat64AvxMask operator ==(PacketQuaternionFloat64AvxMask left, PacketQuaternionFloat64AvxMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    public static PacketQuaternionFloat64AvxMask operator !=(PacketQuaternionFloat64AvxMask left, PacketQuaternionFloat64AvxMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketQuaternionFloat64AvxMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}
