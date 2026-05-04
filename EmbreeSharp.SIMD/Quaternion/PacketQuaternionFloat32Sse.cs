namespace EmbreeSharp.SIMD;

public readonly struct PacketQuaternionFloat32Sse :
    ISimdQuaternion<PacketQuaternionFloat32Sse, PacketVector3Float32Sse, PacketVector4Float32Sse, PacketFloat32Sse, float, PacketQuaternionFloat32SseMask, PacketVector3Float32SseMask, PacketVector4Float32SseMask, PacketFloat32SseMask>
{
    public PacketQuaternionFloat32Sse(PacketFloat32Sse x, PacketFloat32Sse y, PacketFloat32Sse z, PacketFloat32Sse w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketFloat32Sse.LaneCount;

    public PacketFloat32Sse X { get; }

    public PacketFloat32Sse Y { get; }

    public PacketFloat32Sse Z { get; }

    public PacketFloat32Sse W { get; }

    public PacketVector3Float32Sse Imag => new(X, Y, Z);

    public PacketFloat32Sse Real => W;

    public PacketVector4Float32Sse Vector => new(X, Y, Z, W);

    public static PacketQuaternionFloat32Sse Identity
    {
        get
        {
            PacketFloat32Sse zero = PacketFloat32Sse.Broadcast(0f);
            PacketFloat32Sse one = PacketFloat32Sse.Broadcast(1f);
            return new(zero, zero, zero, one);
        }
    }

    public static PacketQuaternionFloat32Sse Create(PacketFloat32Sse x, PacketFloat32Sse y, PacketFloat32Sse z, PacketFloat32Sse w) => new(x, y, z, w);

    public static PacketQuaternionFloat32Sse Create(PacketVector3Float32Sse imag, PacketFloat32Sse real) => new(imag.X, imag.Y, imag.Z, real);

    public static PacketQuaternionFloat32Sse Create(PacketVector4Float32Sse vector) => new(vector.X, vector.Y, vector.Z, vector.W);

    public static PacketQuaternionFloat32Sse Broadcast(float value)
    {
        PacketFloat32Sse zero = PacketFloat32Sse.Broadcast(0f);
        return new(zero, zero, zero, PacketFloat32Sse.Broadcast(value));
    }

    public static PacketQuaternionFloat32Sse Select(PacketQuaternionFloat32SseMask mask, PacketQuaternionFloat32Sse ifTrue, PacketQuaternionFloat32Sse ifFalse)
    {
        return new(
            PacketFloat32Sse.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32Sse.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat32Sse.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat32Sse.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    public static PacketQuaternionFloat32Sse Select(PacketVector4Float32SseMask mask, PacketQuaternionFloat32Sse ifTrue, PacketQuaternionFloat32Sse ifFalse)
    {
        return new(
            PacketFloat32Sse.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32Sse.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat32Sse.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat32Sse.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    public static PacketQuaternionFloat32Sse Select(PacketFloat32SseMask mask, PacketQuaternionFloat32Sse ifTrue, PacketQuaternionFloat32Sse ifFalse)
    {
        return new(
            PacketFloat32Sse.Select(mask, ifTrue.X, ifFalse.X),
            PacketFloat32Sse.Select(mask, ifTrue.Y, ifFalse.Y),
            PacketFloat32Sse.Select(mask, ifTrue.Z, ifFalse.Z),
            PacketFloat32Sse.Select(mask, ifTrue.W, ifFalse.W));
    }

    public static PacketQuaternionFloat32Sse Conjugate(PacketQuaternionFloat32Sse value) => new(-value.X, -value.Y, -value.Z, value.W);

    public static PacketFloat32Sse Dot(PacketQuaternionFloat32Sse left, PacketQuaternionFloat32Sse right) => PacketVector4Float32Sse.Dot(left.Vector, right.Vector);

    public static PacketFloat32Sse SquaredNorm(PacketQuaternionFloat32Sse value) => Dot(value, value);

    public static PacketFloat32Sse Norm(PacketQuaternionFloat32Sse value) => PacketFloat32Sse.Sqrt(SquaredNorm(value));

    public static PacketQuaternionFloat32Sse Normalize(PacketQuaternionFloat32Sse value) => value * PacketFloat32Sse.ReciprocalSqrt(SquaredNorm(value));

    public static PacketQuaternionFloat32Sse Reciprocal(PacketQuaternionFloat32Sse value) => Conjugate(value) * PacketFloat32Sse.Reciprocal(SquaredNorm(value));

    public static PacketQuaternionFloat32Sse Multiply(PacketQuaternionFloat32Sse left, PacketQuaternionFloat32Sse right) => left * right;

    public static PacketQuaternionFloat32Sse Multiply(PacketQuaternionFloat32Sse quaternion, PacketFloat32Sse scalar) => quaternion * scalar;

    public static PacketQuaternionFloat32Sse Multiply(PacketFloat32Sse scalar, PacketQuaternionFloat32Sse quaternion) => quaternion * scalar;

    public static PacketQuaternionFloat32Sse Divide(PacketQuaternionFloat32Sse left, PacketQuaternionFloat32Sse right) => left / right;

    public static PacketQuaternionFloat32Sse Divide(PacketQuaternionFloat32Sse quaternion, PacketFloat32Sse scalar) => quaternion / scalar;

    public static PacketQuaternionFloat32Sse FusedMultiplyAdd(PacketQuaternionFloat32Sse left, PacketQuaternionFloat32Sse right, PacketQuaternionFloat32Sse addend)
    {
        PacketFloat32Sse t1X = PacketFloat32Sse.FusedMultiplyAdd(left.X, right.W, left.Y * right.Z);
        PacketFloat32Sse t1Y = PacketFloat32Sse.FusedMultiplyAdd(left.Y, right.W, left.Z * right.X);
        PacketFloat32Sse t1Z = PacketFloat32Sse.FusedMultiplyAdd(left.Z, right.W, left.X * right.Y);
        PacketFloat32Sse t1W = PacketFloat32Sse.FusedMultiplyAdd(left.X, right.X, left.Y * right.Y);

        PacketFloat32Sse t2X = PacketFloat32Sse.FusedMultiplyAdd(left.W, right.X, PacketFloat32Sse.FusedMultiplyAdd(-left.Z, right.Y, addend.X));
        PacketFloat32Sse t2Y = PacketFloat32Sse.FusedMultiplyAdd(left.W, right.Y, PacketFloat32Sse.FusedMultiplyAdd(-left.X, right.Z, addend.Y));
        PacketFloat32Sse t2Z = PacketFloat32Sse.FusedMultiplyAdd(left.W, right.Z, PacketFloat32Sse.FusedMultiplyAdd(-left.Y, right.X, addend.Z));
        PacketFloat32Sse t2W = PacketFloat32Sse.FusedMultiplyAdd(left.W, right.W, PacketFloat32Sse.FusedMultiplyAdd(-left.Z, right.Z, addend.W));

        return new(t1X + t2X, t1Y + t2Y, t1Z + t2Z, -t1W + t2W);
    }

    public static PacketQuaternionFloat32Sse Rotate(PacketVector3Float32Sse axis, PacketFloat32Sse angle)
    {
        var (sin, cos) = PacketFloat32Sse.SinCos(angle * PacketFloat32Sse.Broadcast(0.5f));
        PacketVector3Float32Sse imag = axis * new PacketVector3Float32Sse(sin, sin, sin);
        return new(imag.X, imag.Y, imag.Z, cos);
    }

    public static PacketVector3Float32Sse Apply(PacketQuaternionFloat32Sse quaternion, PacketVector3Float32Sse vector)
    {
        PacketVector3Float32Sse imag = quaternion.Imag;
        PacketFloat32Sse real = quaternion.Real;
        PacketFloat32Sse two = PacketFloat32Sse.Broadcast(2f);

        PacketFloat32Sse imagDotVector = PacketVector3Float32Sse.Dot(imag, vector);
        PacketFloat32Sse realScale = real * real - PacketVector3Float32Sse.Dot(imag, imag);
        PacketFloat32Sse crossScale = two * real;

        return new PacketVector3Float32Sse(two * imagDotVector, two * imagDotVector, two * imagDotVector) * imag +
               new PacketVector3Float32Sse(realScale, realScale, realScale) * vector +
               new PacketVector3Float32Sse(crossScale, crossScale, crossScale) * PacketVector3Float32Sse.Cross(imag, vector);
    }

    public static PacketQuaternionFloat32Sse operator +(PacketQuaternionFloat32Sse value) => value;

    public static PacketQuaternionFloat32Sse operator +(PacketQuaternionFloat32Sse left, PacketQuaternionFloat32Sse right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    public static PacketQuaternionFloat32Sse operator -(PacketQuaternionFloat32Sse left, PacketQuaternionFloat32Sse right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    public static PacketQuaternionFloat32Sse operator *(PacketQuaternionFloat32Sse left, PacketQuaternionFloat32Sse right)
    {
        PacketFloat32Sse t1X = PacketFloat32Sse.FusedMultiplyAdd(left.X, right.W, left.Y * right.Z);
        PacketFloat32Sse t1Y = PacketFloat32Sse.FusedMultiplyAdd(left.Y, right.W, left.Z * right.X);
        PacketFloat32Sse t1Z = PacketFloat32Sse.FusedMultiplyAdd(left.Z, right.W, left.X * right.Y);
        PacketFloat32Sse t1W = PacketFloat32Sse.FusedMultiplyAdd(left.X, right.X, left.Y * right.Y);

        PacketFloat32Sse t2X = PacketFloat32Sse.FusedMultiplyAdd(left.W, right.X, -(left.Z * right.Y));
        PacketFloat32Sse t2Y = PacketFloat32Sse.FusedMultiplyAdd(left.W, right.Y, -(left.X * right.Z));
        PacketFloat32Sse t2Z = PacketFloat32Sse.FusedMultiplyAdd(left.W, right.Z, -(left.Y * right.X));
        PacketFloat32Sse t2W = PacketFloat32Sse.FusedMultiplyAdd(left.W, right.W, -(left.Z * right.Z));

        return new(t1X + t2X, t1Y + t2Y, t1Z + t2Z, -t1W + t2W);
    }

    public static PacketQuaternionFloat32Sse operator *(PacketQuaternionFloat32Sse quaternion, PacketFloat32Sse scalar)
    {
        PacketVector4Float32Sse scalarVector = new(scalar, scalar, scalar, scalar);
        return Create(quaternion.Vector * scalarVector);
    }

    public static PacketQuaternionFloat32Sse operator *(PacketFloat32Sse scalar, PacketQuaternionFloat32Sse quaternion) => quaternion * scalar;

    public static PacketQuaternionFloat32Sse operator /(PacketQuaternionFloat32Sse left, PacketQuaternionFloat32Sse right) => left * Reciprocal(right);

    public static PacketQuaternionFloat32Sse operator /(PacketQuaternionFloat32Sse quaternion, PacketFloat32Sse scalar) => quaternion * PacketFloat32Sse.Reciprocal(scalar);

    public static PacketQuaternionFloat32Sse operator -(PacketQuaternionFloat32Sse value) => new(-value.X, -value.Y, -value.Z, -value.W);

    public static PacketQuaternionFloat32SseMask operator ==(PacketQuaternionFloat32Sse left, PacketQuaternionFloat32Sse right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    public static PacketQuaternionFloat32SseMask operator !=(PacketQuaternionFloat32Sse left, PacketQuaternionFloat32Sse right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketQuaternionFloat32Sse other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct PacketQuaternionFloat32SseMask :
    ISimdQuaternionMask<PacketQuaternionFloat32SseMask, PacketFloat32SseMask>
{
    public PacketQuaternionFloat32SseMask(PacketFloat32SseMask x, PacketFloat32SseMask y, PacketFloat32SseMask z, PacketFloat32SseMask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => PacketFloat32SseMask.LaneCount;

    public PacketFloat32SseMask X { get; }

    public PacketFloat32SseMask Y { get; }

    public PacketFloat32SseMask Z { get; }

    public PacketFloat32SseMask W { get; }

    public static PacketQuaternionFloat32SseMask True => new(PacketFloat32SseMask.True, PacketFloat32SseMask.True, PacketFloat32SseMask.True, PacketFloat32SseMask.True);

    public static PacketQuaternionFloat32SseMask False => new(PacketFloat32SseMask.False, PacketFloat32SseMask.False, PacketFloat32SseMask.False, PacketFloat32SseMask.False);

    public static PacketQuaternionFloat32SseMask Create(PacketFloat32SseMask x, PacketFloat32SseMask y, PacketFloat32SseMask z, PacketFloat32SseMask w) => new(x, y, z, w);

    public static PacketQuaternionFloat32SseMask Broadcast(PacketFloat32SseMask value) => new(value, value, value, value);

    public static PacketFloat32SseMask All(PacketQuaternionFloat32SseMask value) => value.X & value.Y & value.Z & value.W;

    public static PacketFloat32SseMask Any(PacketQuaternionFloat32SseMask value) => value.X | value.Y | value.Z | value.W;

    public static PacketFloat32SseMask None(PacketQuaternionFloat32SseMask value) => ~(value.X | value.Y | value.Z | value.W);

    public static PacketQuaternionFloat32SseMask AndNot(PacketQuaternionFloat32SseMask left, PacketQuaternionFloat32SseMask right)
    {
        return new(
            PacketFloat32SseMask.AndNot(left.X, right.X),
            PacketFloat32SseMask.AndNot(left.Y, right.Y),
            PacketFloat32SseMask.AndNot(left.Z, right.Z),
            PacketFloat32SseMask.AndNot(left.W, right.W));
    }

    public static PacketQuaternionFloat32SseMask Select(PacketQuaternionFloat32SseMask mask, PacketQuaternionFloat32SseMask ifTrue, PacketQuaternionFloat32SseMask ifFalse)
    {
        return new(
            PacketFloat32SseMask.Select(mask.X, ifTrue.X, ifFalse.X),
            PacketFloat32SseMask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            PacketFloat32SseMask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            PacketFloat32SseMask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    public static PacketQuaternionFloat32SseMask And(PacketQuaternionFloat32SseMask left, PacketQuaternionFloat32SseMask right) => left & right;

    public static PacketQuaternionFloat32SseMask Or(PacketQuaternionFloat32SseMask left, PacketQuaternionFloat32SseMask right) => left | right;

    public static PacketQuaternionFloat32SseMask Xor(PacketQuaternionFloat32SseMask left, PacketQuaternionFloat32SseMask right) => left ^ right;

    public static PacketQuaternionFloat32SseMask Not(PacketQuaternionFloat32SseMask value) => ~value;

    public static PacketQuaternionFloat32SseMask operator &(PacketQuaternionFloat32SseMask left, PacketQuaternionFloat32SseMask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    public static PacketQuaternionFloat32SseMask operator |(PacketQuaternionFloat32SseMask left, PacketQuaternionFloat32SseMask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    public static PacketQuaternionFloat32SseMask operator ^(PacketQuaternionFloat32SseMask left, PacketQuaternionFloat32SseMask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    public static PacketQuaternionFloat32SseMask operator ~(PacketQuaternionFloat32SseMask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    public static PacketQuaternionFloat32SseMask operator ==(PacketQuaternionFloat32SseMask left, PacketQuaternionFloat32SseMask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    public static PacketQuaternionFloat32SseMask operator !=(PacketQuaternionFloat32SseMask left, PacketQuaternionFloat32SseMask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is PacketQuaternionFloat32SseMask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}
