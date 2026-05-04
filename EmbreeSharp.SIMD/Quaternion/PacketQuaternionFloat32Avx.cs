namespace EmbreeSharp.SIMD;

public readonly struct PacketQuaternionFloat32Avx :
    ISimdQuaternion<PacketQuaternionFloat32Avx, PacketVector3Float32Avx, PacketVector4Float32Avx, PacketFloat32Avx, float, PacketQuaternionFloat32AvxMask, PacketVector3Float32AvxMask, PacketVector4Float32AvxMask, PacketFloat32AvxMask>
{
    public PacketQuaternionFloat32Avx(PacketFloat32Avx x, PacketFloat32Avx y, PacketFloat32Avx z, PacketFloat32Avx w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketFloat32Avx.LaneCount;

    public PacketFloat32Avx X { get; }

    public PacketFloat32Avx Y { get; }

    public PacketFloat32Avx Z { get; }

    public PacketFloat32Avx W { get; }

    public PacketVector3Float32Avx Imag => new(X, Y, Z);

    public PacketFloat32Avx Real => W;

    public PacketVector4Float32Avx Vector => new(X, Y, Z, W);

    public static PacketQuaternionFloat32Avx Identity
    {
        get
        {
            PacketFloat32Avx zero = PacketFloat32Avx.Broadcast(0f);
            PacketFloat32Avx one = PacketFloat32Avx.Broadcast(1f);
            return new(zero, zero, zero, one);
        }
    }

    public static PacketQuaternionFloat32Avx Create(PacketFloat32Avx x, PacketFloat32Avx y, PacketFloat32Avx z, PacketFloat32Avx w) => new(x, y, z, w);

    public static PacketQuaternionFloat32Avx Create(PacketVector3Float32Avx imag, PacketFloat32Avx real) => new(imag.X, imag.Y, imag.Z, real);

    public static PacketQuaternionFloat32Avx Create(PacketVector4Float32Avx vector) => new(vector.X, vector.Y, vector.Z, vector.W);

    public static PacketQuaternionFloat32Avx Broadcast(float value)
    {
        PacketFloat32Avx zero = PacketFloat32Avx.Broadcast(0f);
        return new(zero, zero, zero, PacketFloat32Avx.Broadcast(value));
    }

    public static PacketQuaternionFloat32Avx Select(PacketQuaternionFloat32AvxMask mask, PacketQuaternionFloat32Avx ifTrue, PacketQuaternionFloat32Avx ifFalse)
    {
        return new(
            PacketFloat32Avx.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32Avx.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat32Avx.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat32Avx.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    public static PacketQuaternionFloat32Avx Select(PacketVector4Float32AvxMask mask, PacketQuaternionFloat32Avx ifTrue, PacketQuaternionFloat32Avx ifFalse)
    {
        return new(
            PacketFloat32Avx.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32Avx.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat32Avx.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat32Avx.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    public static PacketQuaternionFloat32Avx Select(PacketFloat32AvxMask mask, PacketQuaternionFloat32Avx ifTrue, PacketQuaternionFloat32Avx ifFalse)
    {
        return new(
            PacketFloat32Avx.Select(mask, ifTrue.X, ifFalse.X),
            PacketFloat32Avx.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketFloat32Avx.Select(mask, ifTrue.Z, ifFalse.Z),
            PacketFloat32Avx.Select(mask, ifTrue.W, ifFalse.W));
    }

    public static PacketQuaternionFloat32Avx Conjugate(PacketQuaternionFloat32Avx value) => new(-value.X, -value.Y, -value.Z, value.W);

    public static PacketFloat32Avx Dot(PacketQuaternionFloat32Avx left, PacketQuaternionFloat32Avx right) => PacketVector4Float32Avx.Dot(left.Vector, right.Vector);

    public static PacketFloat32Avx SquaredNorm(PacketQuaternionFloat32Avx value) => Dot(value, value);

    public static PacketFloat32Avx Norm(PacketQuaternionFloat32Avx value) => PacketFloat32Avx.Sqrt(SquaredNorm(value));

    public static PacketQuaternionFloat32Avx Normalize(PacketQuaternionFloat32Avx value) => value * PacketFloat32Avx.ReciprocalSqrt(SquaredNorm(value));

    public static PacketQuaternionFloat32Avx Reciprocal(PacketQuaternionFloat32Avx value) => Conjugate(value) * PacketFloat32Avx.Reciprocal(SquaredNorm(value));

    public static PacketQuaternionFloat32Avx Multiply(PacketQuaternionFloat32Avx left, PacketQuaternionFloat32Avx right) => left * right;

    public static PacketQuaternionFloat32Avx Multiply(PacketQuaternionFloat32Avx quaternion, PacketFloat32Avx scalar) => quaternion * scalar;

    public static PacketQuaternionFloat32Avx Multiply(PacketFloat32Avx scalar, PacketQuaternionFloat32Avx quaternion) => quaternion * scalar;

    public static PacketQuaternionFloat32Avx Divide(PacketQuaternionFloat32Avx left, PacketQuaternionFloat32Avx right) => left / right;

    public static PacketQuaternionFloat32Avx Divide(PacketQuaternionFloat32Avx quaternion, PacketFloat32Avx scalar) => quaternion / scalar;

    public static PacketQuaternionFloat32Avx FusedMultiplyAdd(PacketQuaternionFloat32Avx left, PacketQuaternionFloat32Avx right, PacketQuaternionFloat32Avx addend)
    {
        PacketFloat32Avx t1X = PacketFloat32Avx.FusedMultiplyAdd(left.X, right.W, left.Y * right.Z);
        PacketFloat32Avx t1Y = PacketFloat32Avx.FusedMultiplyAdd(left.Y, right.W, left.Z * right.X);
        PacketFloat32Avx t1Z = PacketFloat32Avx.FusedMultiplyAdd(left.Z, right.W, left.X * right.Y);
        PacketFloat32Avx t1W = PacketFloat32Avx.FusedMultiplyAdd(left.X, right.X, left.Y * right.Y);

        PacketFloat32Avx t2X = PacketFloat32Avx.FusedMultiplyAdd(left.W, right.X, PacketFloat32Avx.FusedMultiplyAdd(-left.Z, right.Y, addend.X));
        PacketFloat32Avx t2Y = PacketFloat32Avx.FusedMultiplyAdd(left.W, right.Y, PacketFloat32Avx.FusedMultiplyAdd(-left.X, right.Z, addend.Y));
        PacketFloat32Avx t2Z = PacketFloat32Avx.FusedMultiplyAdd(left.W, right.Z, PacketFloat32Avx.FusedMultiplyAdd(-left.Y, right.X, addend.Z));
        PacketFloat32Avx t2W = PacketFloat32Avx.FusedMultiplyAdd(left.W, right.W, PacketFloat32Avx.FusedMultiplyAdd(-left.Z, right.Z, addend.W));

        return new(t1X + t2X, t1Y + t2Y, t1Z + t2Z, -t1W + t2W);
    }

    public static PacketQuaternionFloat32Avx Rotate(PacketVector3Float32Avx axis, PacketFloat32Avx angle)
    {
        var (sin, cos) = PacketFloat32Avx.SinCos(angle * PacketFloat32Avx.Broadcast(0.5f));
        PacketVector3Float32Avx imag = axis * new PacketVector3Float32Avx(sin, sin, sin);
        return new(imag.X, imag.Y, imag.Z, cos);
    }

    public static PacketVector3Float32Avx Apply(PacketQuaternionFloat32Avx quaternion, PacketVector3Float32Avx vector)
    {
        PacketVector3Float32Avx imag = quaternion.Imag;
        PacketFloat32Avx real = quaternion.Real;
        PacketFloat32Avx two = PacketFloat32Avx.Broadcast(2f);

        PacketFloat32Avx imagDotVector = PacketVector3Float32Avx.Dot(imag, vector);
        PacketFloat32Avx realScale = real * real - PacketVector3Float32Avx.Dot(imag, imag);
        PacketFloat32Avx crossScale = two * real;

        return new PacketVector3Float32Avx(two * imagDotVector, two * imagDotVector, two * imagDotVector) * imag +
               new PacketVector3Float32Avx(realScale, realScale, realScale) * vector +
               new PacketVector3Float32Avx(crossScale, crossScale, crossScale) * PacketVector3Float32Avx.Cross(imag, vector);
    }

    public static PacketQuaternionFloat32Avx operator +(PacketQuaternionFloat32Avx value) => value;

    public static PacketQuaternionFloat32Avx operator +(PacketQuaternionFloat32Avx left, PacketQuaternionFloat32Avx right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    public static PacketQuaternionFloat32Avx operator -(PacketQuaternionFloat32Avx left, PacketQuaternionFloat32Avx right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    public static PacketQuaternionFloat32Avx operator *(PacketQuaternionFloat32Avx left, PacketQuaternionFloat32Avx right)
    {
        PacketFloat32Avx t1X = PacketFloat32Avx.FusedMultiplyAdd(left.X, right.W, left.Y * right.Z);
        PacketFloat32Avx t1Y = PacketFloat32Avx.FusedMultiplyAdd(left.Y, right.W, left.Z * right.X);
        PacketFloat32Avx t1Z = PacketFloat32Avx.FusedMultiplyAdd(left.Z, right.W, left.X * right.Y);
        PacketFloat32Avx t1W = PacketFloat32Avx.FusedMultiplyAdd(left.X, right.X, left.Y * right.Y);

        PacketFloat32Avx t2X = PacketFloat32Avx.FusedMultiplyAdd(left.W, right.X, -(left.Z * right.Y));
        PacketFloat32Avx t2Y = PacketFloat32Avx.FusedMultiplyAdd(left.W, right.Y, -(left.X * right.Z));
        PacketFloat32Avx t2Z = PacketFloat32Avx.FusedMultiplyAdd(left.W, right.Z, -(left.Y * right.X));
        PacketFloat32Avx t2W = PacketFloat32Avx.FusedMultiplyAdd(left.W, right.W, -(left.Z * right.Z));

        return new(t1X + t2X, t1Y + t2Y, t1Z + t2Z, -t1W + t2W);
    }

    public static PacketQuaternionFloat32Avx operator *(PacketQuaternionFloat32Avx quaternion, PacketFloat32Avx scalar)
    {
        PacketVector4Float32Avx scalarVector = new(scalar, scalar, scalar, scalar);
        return Create(quaternion.Vector * scalarVector);
    }

    public static PacketQuaternionFloat32Avx operator *(PacketFloat32Avx scalar, PacketQuaternionFloat32Avx quaternion) => quaternion * scalar;

    public static PacketQuaternionFloat32Avx operator /(PacketQuaternionFloat32Avx left, PacketQuaternionFloat32Avx right) => left * Reciprocal(right);

    public static PacketQuaternionFloat32Avx operator /(PacketQuaternionFloat32Avx quaternion, PacketFloat32Avx scalar) => quaternion * PacketFloat32Avx.Reciprocal(scalar);

    public static PacketQuaternionFloat32Avx operator -(PacketQuaternionFloat32Avx value) => new(-value.X, -value.Y, -value.Z, -value.W);

    public static PacketQuaternionFloat32AvxMask operator ==(PacketQuaternionFloat32Avx left, PacketQuaternionFloat32Avx right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    public static PacketQuaternionFloat32AvxMask operator !=(PacketQuaternionFloat32Avx left, PacketQuaternionFloat32Avx right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketQuaternionFloat32Avx other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct PacketQuaternionFloat32AvxMask :
    ISimdQuaternionMask<PacketQuaternionFloat32AvxMask, PacketFloat32AvxMask>
{
    public PacketQuaternionFloat32AvxMask(PacketFloat32AvxMask x, PacketFloat32AvxMask y, PacketFloat32AvxMask z, PacketFloat32AvxMask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketFloat32AvxMask.LaneCount;

    public PacketFloat32AvxMask X { get; }

    public PacketFloat32AvxMask Y { get; }

    public PacketFloat32AvxMask Z { get; }

    public PacketFloat32AvxMask W { get; }

    public static PacketQuaternionFloat32AvxMask True => new(PacketFloat32AvxMask.True, PacketFloat32AvxMask.True, PacketFloat32AvxMask.True, PacketFloat32AvxMask.True);

    public static PacketQuaternionFloat32AvxMask False => new(PacketFloat32AvxMask.False, PacketFloat32AvxMask.False, PacketFloat32AvxMask.False, PacketFloat32AvxMask.False);

    public static PacketQuaternionFloat32AvxMask Create(PacketFloat32AvxMask x, PacketFloat32AvxMask y, PacketFloat32AvxMask z, PacketFloat32AvxMask w) => new(x, y, z, w);

    public static PacketQuaternionFloat32AvxMask Broadcast(PacketFloat32AvxMask value) => new(value, value, value, value);

    public static PacketFloat32AvxMask All(PacketQuaternionFloat32AvxMask value) => value.X & value.Y & value.Z & value.W;

    public static PacketFloat32AvxMask Any(PacketQuaternionFloat32AvxMask value) => value.X | value.Y | value.Z | value.W;

    public static PacketFloat32AvxMask None(PacketQuaternionFloat32AvxMask value) => ~(value.X | value.Y | value.Z | value.W);

    public static PacketQuaternionFloat32AvxMask AndNot(PacketQuaternionFloat32AvxMask left, PacketQuaternionFloat32AvxMask right)
    {
        return new(
            PacketFloat32AvxMask.AndNot(left.X, right.X),
            PacketFloat32AvxMask.AndNot(left.Y, right.Y),
            PacketFloat32AvxMask.AndNot(left.Z, right.Z),
            PacketFloat32AvxMask.AndNot(left.W, right.W));
    }

    public static PacketQuaternionFloat32AvxMask Select(PacketQuaternionFloat32AvxMask mask, PacketQuaternionFloat32AvxMask ifTrue, PacketQuaternionFloat32AvxMask ifFalse)
    {
        return new(
            PacketFloat32AvxMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32AvxMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat32AvxMask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat32AvxMask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    public static PacketQuaternionFloat32AvxMask And(PacketQuaternionFloat32AvxMask left, PacketQuaternionFloat32AvxMask right) => left & right;

    public static PacketQuaternionFloat32AvxMask Or(PacketQuaternionFloat32AvxMask left, PacketQuaternionFloat32AvxMask right) => left | right;

    public static PacketQuaternionFloat32AvxMask Xor(PacketQuaternionFloat32AvxMask left, PacketQuaternionFloat32AvxMask right) => left ^ right;

    public static PacketQuaternionFloat32AvxMask Not(PacketQuaternionFloat32AvxMask value) => ~value;

    public static PacketQuaternionFloat32AvxMask operator &(PacketQuaternionFloat32AvxMask left, PacketQuaternionFloat32AvxMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    public static PacketQuaternionFloat32AvxMask operator |(PacketQuaternionFloat32AvxMask left, PacketQuaternionFloat32AvxMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    public static PacketQuaternionFloat32AvxMask operator ^(PacketQuaternionFloat32AvxMask left, PacketQuaternionFloat32AvxMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    public static PacketQuaternionFloat32AvxMask operator ~(PacketQuaternionFloat32AvxMask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    public static PacketQuaternionFloat32AvxMask operator ==(PacketQuaternionFloat32AvxMask left, PacketQuaternionFloat32AvxMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    public static PacketQuaternionFloat32AvxMask operator !=(PacketQuaternionFloat32AvxMask left, PacketQuaternionFloat32AvxMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketQuaternionFloat32AvxMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}
