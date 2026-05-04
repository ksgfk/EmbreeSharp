namespace EmbreeSharp.SIMD;

public readonly struct PacketQuaternionFloat64Sse :
    ISimdQuaternion<PacketQuaternionFloat64Sse, PacketVector3Float64Sse, PacketVector4Float64Sse, PacketFloat64Sse, double, PacketQuaternionFloat64SseMask, PacketVector3Float64SseMask, PacketVector4Float64SseMask, PacketFloat64SseMask>
{
    public PacketQuaternionFloat64Sse(PacketFloat64Sse x, PacketFloat64Sse y, PacketFloat64Sse z, PacketFloat64Sse w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketFloat64Sse.LaneCount;

    public PacketFloat64Sse X { get; }

    public PacketFloat64Sse Y { get; }

    public PacketFloat64Sse Z { get; }

    public PacketFloat64Sse W { get; }

    public PacketVector3Float64Sse Imag => new(X, Y, Z);

    public PacketFloat64Sse Real => W;

    public PacketVector4Float64Sse Vector => new(X, Y, Z, W);

    public static PacketQuaternionFloat64Sse Identity
    {
        get
        {
            PacketFloat64Sse zero = PacketFloat64Sse.Broadcast(0d);
            PacketFloat64Sse one = PacketFloat64Sse.Broadcast(1d);
            return new(zero, zero, zero, one);
        }
    }

    public static PacketQuaternionFloat64Sse Create(PacketFloat64Sse x, PacketFloat64Sse y, PacketFloat64Sse z, PacketFloat64Sse w) => new(x, y, z, w);

    public static PacketQuaternionFloat64Sse Create(PacketVector3Float64Sse imag, PacketFloat64Sse real) => new(imag.X, imag.Y, imag.Z, real);

    public static PacketQuaternionFloat64Sse Create(PacketVector4Float64Sse vector) => new(vector.X, vector.Y, vector.Z, vector.W);

    public static PacketQuaternionFloat64Sse Broadcast(double value)
    {
        PacketFloat64Sse zero = PacketFloat64Sse.Broadcast(0d);
        return new(zero, zero, zero, PacketFloat64Sse.Broadcast(value));
    }

    public static PacketQuaternionFloat64Sse Select(PacketQuaternionFloat64SseMask mask, PacketQuaternionFloat64Sse ifTrue, PacketQuaternionFloat64Sse ifFalse)
    {
        return new(
            PacketFloat64Sse.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64Sse.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat64Sse.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat64Sse.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    public static PacketQuaternionFloat64Sse Select(PacketVector4Float64SseMask mask, PacketQuaternionFloat64Sse ifTrue, PacketQuaternionFloat64Sse ifFalse)
    {
        return new(
            PacketFloat64Sse.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64Sse.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat64Sse.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat64Sse.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    public static PacketQuaternionFloat64Sse Select(PacketFloat64SseMask mask, PacketQuaternionFloat64Sse ifTrue, PacketQuaternionFloat64Sse ifFalse)
    {
        return new(
            PacketFloat64Sse.Select(mask, ifTrue.X, ifFalse.X),
            PacketFloat64Sse.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketFloat64Sse.Select(mask, ifTrue.Z, ifFalse.Z),
            PacketFloat64Sse.Select(mask, ifTrue.W, ifFalse.W));
    }

    public static PacketQuaternionFloat64Sse Conjugate(PacketQuaternionFloat64Sse value) => new(-value.X, -value.Y, -value.Z, value.W);

    public static PacketFloat64Sse Dot(PacketQuaternionFloat64Sse left, PacketQuaternionFloat64Sse right) => PacketVector4Float64Sse.Dot(left.Vector, right.Vector);

    public static PacketFloat64Sse SquaredNorm(PacketQuaternionFloat64Sse value) => Dot(value, value);

    public static PacketFloat64Sse Norm(PacketQuaternionFloat64Sse value) => PacketFloat64Sse.Sqrt(SquaredNorm(value));

    public static PacketQuaternionFloat64Sse Normalize(PacketQuaternionFloat64Sse value) => value * PacketFloat64Sse.ReciprocalSqrt(SquaredNorm(value));

    public static PacketQuaternionFloat64Sse Reciprocal(PacketQuaternionFloat64Sse value) => Conjugate(value) * PacketFloat64Sse.Reciprocal(SquaredNorm(value));

    public static PacketQuaternionFloat64Sse Multiply(PacketQuaternionFloat64Sse left, PacketQuaternionFloat64Sse right) => left * right;

    public static PacketQuaternionFloat64Sse Multiply(PacketQuaternionFloat64Sse quaternion, PacketFloat64Sse scalar) => quaternion * scalar;

    public static PacketQuaternionFloat64Sse Multiply(PacketFloat64Sse scalar, PacketQuaternionFloat64Sse quaternion) => quaternion * scalar;

    public static PacketQuaternionFloat64Sse Divide(PacketQuaternionFloat64Sse left, PacketQuaternionFloat64Sse right) => left / right;

    public static PacketQuaternionFloat64Sse Divide(PacketQuaternionFloat64Sse quaternion, PacketFloat64Sse scalar) => quaternion / scalar;

    public static PacketQuaternionFloat64Sse FusedMultiplyAdd(PacketQuaternionFloat64Sse left, PacketQuaternionFloat64Sse right, PacketQuaternionFloat64Sse addend)
    {
        PacketFloat64Sse t1X = PacketFloat64Sse.FusedMultiplyAdd(left.X, right.W, left.Y * right.Z);
        PacketFloat64Sse t1Y = PacketFloat64Sse.FusedMultiplyAdd(left.Y, right.W, left.Z * right.X);
        PacketFloat64Sse t1Z = PacketFloat64Sse.FusedMultiplyAdd(left.Z, right.W, left.X * right.Y);
        PacketFloat64Sse t1W = PacketFloat64Sse.FusedMultiplyAdd(left.X, right.X, left.Y * right.Y);

        PacketFloat64Sse t2X = PacketFloat64Sse.FusedMultiplyAdd(left.W, right.X, PacketFloat64Sse.FusedMultiplyAdd(-left.Z, right.Y, addend.X));
        PacketFloat64Sse t2Y = PacketFloat64Sse.FusedMultiplyAdd(left.W, right.Y, PacketFloat64Sse.FusedMultiplyAdd(-left.X, right.Z, addend.Y));
        PacketFloat64Sse t2Z = PacketFloat64Sse.FusedMultiplyAdd(left.W, right.Z, PacketFloat64Sse.FusedMultiplyAdd(-left.Y, right.X, addend.Z));
        PacketFloat64Sse t2W = PacketFloat64Sse.FusedMultiplyAdd(left.W, right.W, PacketFloat64Sse.FusedMultiplyAdd(-left.Z, right.Z, addend.W));

        return new(t1X + t2X, t1Y + t2Y, t1Z + t2Z, -t1W + t2W);
    }

    public static PacketQuaternionFloat64Sse Rotate(PacketVector3Float64Sse axis, PacketFloat64Sse angle)
    {
        var (sin, cos) = PacketFloat64Sse.SinCos(angle * PacketFloat64Sse.Broadcast(0.5d));
        PacketVector3Float64Sse imag = axis * new PacketVector3Float64Sse(sin, sin, sin);
        return new(imag.X, imag.Y, imag.Z, cos);
    }

    public static PacketVector3Float64Sse Apply(PacketQuaternionFloat64Sse quaternion, PacketVector3Float64Sse vector)
    {
        PacketVector3Float64Sse imag = quaternion.Imag;
        PacketFloat64Sse real = quaternion.Real;
        PacketFloat64Sse two = PacketFloat64Sse.Broadcast(2d);

        PacketFloat64Sse imagDotVector = PacketVector3Float64Sse.Dot(imag, vector);
        PacketFloat64Sse realScale = real * real - PacketVector3Float64Sse.Dot(imag, imag);
        PacketFloat64Sse crossScale = two * real;

        return new PacketVector3Float64Sse(two * imagDotVector, two * imagDotVector, two * imagDotVector) * imag +
               new PacketVector3Float64Sse(realScale, realScale, realScale) * vector +
               new PacketVector3Float64Sse(crossScale, crossScale, crossScale) * PacketVector3Float64Sse.Cross(imag, vector);
    }

    public static PacketQuaternionFloat64Sse operator +(PacketQuaternionFloat64Sse value) => value;

    public static PacketQuaternionFloat64Sse operator +(PacketQuaternionFloat64Sse left, PacketQuaternionFloat64Sse right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    public static PacketQuaternionFloat64Sse operator -(PacketQuaternionFloat64Sse left, PacketQuaternionFloat64Sse right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    public static PacketQuaternionFloat64Sse operator *(PacketQuaternionFloat64Sse left, PacketQuaternionFloat64Sse right)
    {
        PacketFloat64Sse t1X = PacketFloat64Sse.FusedMultiplyAdd(left.X, right.W, left.Y * right.Z);
        PacketFloat64Sse t1Y = PacketFloat64Sse.FusedMultiplyAdd(left.Y, right.W, left.Z * right.X);
        PacketFloat64Sse t1Z = PacketFloat64Sse.FusedMultiplyAdd(left.Z, right.W, left.X * right.Y);
        PacketFloat64Sse t1W = PacketFloat64Sse.FusedMultiplyAdd(left.X, right.X, left.Y * right.Y);

        PacketFloat64Sse t2X = PacketFloat64Sse.FusedMultiplyAdd(left.W, right.X, -(left.Z * right.Y));
        PacketFloat64Sse t2Y = PacketFloat64Sse.FusedMultiplyAdd(left.W, right.Y, -(left.X * right.Z));
        PacketFloat64Sse t2Z = PacketFloat64Sse.FusedMultiplyAdd(left.W, right.Z, -(left.Y * right.X));
        PacketFloat64Sse t2W = PacketFloat64Sse.FusedMultiplyAdd(left.W, right.W, -(left.Z * right.Z));

        return new(t1X + t2X, t1Y + t2Y, t1Z + t2Z, -t1W + t2W);
    }

    public static PacketQuaternionFloat64Sse operator *(PacketQuaternionFloat64Sse quaternion, PacketFloat64Sse scalar)
    {
        PacketVector4Float64Sse scalarVector = new(scalar, scalar, scalar, scalar);
        return Create(quaternion.Vector * scalarVector);
    }

    public static PacketQuaternionFloat64Sse operator *(PacketFloat64Sse scalar, PacketQuaternionFloat64Sse quaternion) => quaternion * scalar;

    public static PacketQuaternionFloat64Sse operator /(PacketQuaternionFloat64Sse left, PacketQuaternionFloat64Sse right) => left * Reciprocal(right);

    public static PacketQuaternionFloat64Sse operator /(PacketQuaternionFloat64Sse quaternion, PacketFloat64Sse scalar) => quaternion * PacketFloat64Sse.Reciprocal(scalar);

    public static PacketQuaternionFloat64Sse operator -(PacketQuaternionFloat64Sse value) => new(-value.X, -value.Y, -value.Z, -value.W);

    public static PacketQuaternionFloat64SseMask operator ==(PacketQuaternionFloat64Sse left, PacketQuaternionFloat64Sse right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    public static PacketQuaternionFloat64SseMask operator !=(PacketQuaternionFloat64Sse left, PacketQuaternionFloat64Sse right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketQuaternionFloat64Sse other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct PacketQuaternionFloat64SseMask :
    ISimdQuaternionMask<PacketQuaternionFloat64SseMask, PacketFloat64SseMask>
{
    public PacketQuaternionFloat64SseMask(PacketFloat64SseMask x, PacketFloat64SseMask y, PacketFloat64SseMask z, PacketFloat64SseMask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketFloat64SseMask.LaneCount;

    public PacketFloat64SseMask X { get; }

    public PacketFloat64SseMask Y { get; }

    public PacketFloat64SseMask Z { get; }

    public PacketFloat64SseMask W { get; }

    public static PacketQuaternionFloat64SseMask True => new(PacketFloat64SseMask.True, PacketFloat64SseMask.True, PacketFloat64SseMask.True, PacketFloat64SseMask.True);

    public static PacketQuaternionFloat64SseMask False => new(PacketFloat64SseMask.False, PacketFloat64SseMask.False, PacketFloat64SseMask.False, PacketFloat64SseMask.False);

    public static PacketQuaternionFloat64SseMask Create(PacketFloat64SseMask x, PacketFloat64SseMask y, PacketFloat64SseMask z, PacketFloat64SseMask w) => new(x, y, z, w);

    public static PacketQuaternionFloat64SseMask Broadcast(PacketFloat64SseMask value) => new(value, value, value, value);

    public static PacketFloat64SseMask All(PacketQuaternionFloat64SseMask value) => value.X & value.Y & value.Z & value.W;

    public static PacketFloat64SseMask Any(PacketQuaternionFloat64SseMask value) => value.X | value.Y | value.Z | value.W;

    public static PacketFloat64SseMask None(PacketQuaternionFloat64SseMask value) => ~(value.X | value.Y | value.Z | value.W);

    public static PacketQuaternionFloat64SseMask AndNot(PacketQuaternionFloat64SseMask left, PacketQuaternionFloat64SseMask right)
    {
        return new(
            PacketFloat64SseMask.AndNot(left.X, right.X),
            PacketFloat64SseMask.AndNot(left.Y, right.Y),
            PacketFloat64SseMask.AndNot(left.Z, right.Z),
            PacketFloat64SseMask.AndNot(left.W, right.W));
    }

    public static PacketQuaternionFloat64SseMask Select(PacketQuaternionFloat64SseMask mask, PacketQuaternionFloat64SseMask ifTrue, PacketQuaternionFloat64SseMask ifFalse)
    {
        return new(
            PacketFloat64SseMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat64SseMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat64SseMask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat64SseMask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    public static PacketQuaternionFloat64SseMask And(PacketQuaternionFloat64SseMask left, PacketQuaternionFloat64SseMask right) => left & right;

    public static PacketQuaternionFloat64SseMask Or(PacketQuaternionFloat64SseMask left, PacketQuaternionFloat64SseMask right) => left | right;

    public static PacketQuaternionFloat64SseMask Xor(PacketQuaternionFloat64SseMask left, PacketQuaternionFloat64SseMask right) => left ^ right;

    public static PacketQuaternionFloat64SseMask Not(PacketQuaternionFloat64SseMask value) => ~value;

    public static PacketQuaternionFloat64SseMask operator &(PacketQuaternionFloat64SseMask left, PacketQuaternionFloat64SseMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    public static PacketQuaternionFloat64SseMask operator |(PacketQuaternionFloat64SseMask left, PacketQuaternionFloat64SseMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    public static PacketQuaternionFloat64SseMask operator ^(PacketQuaternionFloat64SseMask left, PacketQuaternionFloat64SseMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    public static PacketQuaternionFloat64SseMask operator ~(PacketQuaternionFloat64SseMask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    public static PacketQuaternionFloat64SseMask operator ==(PacketQuaternionFloat64SseMask left, PacketQuaternionFloat64SseMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    public static PacketQuaternionFloat64SseMask operator !=(PacketQuaternionFloat64SseMask left, PacketQuaternionFloat64SseMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketQuaternionFloat64SseMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}
