namespace EmbreeSharp.SIMD;

public readonly struct ScalarQuaternionFloat64 :
    ISimdQuaternion<ScalarQuaternionFloat64, ScalarVector3Float64, ScalarVector4Float64, ScalarFloat64, double, ScalarQuaternionFloat64Mask, ScalarVector3Float64Mask, ScalarVector4Float64Mask, ScalarFloat64Mask>
{
    public ScalarQuaternionFloat64(ScalarFloat64 x, ScalarFloat64 y, ScalarFloat64 z, ScalarFloat64 w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => ScalarFloat64.LaneCount;

    public ScalarFloat64 X { get; }

    public ScalarFloat64 Y { get; }

    public ScalarFloat64 Z { get; }

    public ScalarFloat64 W { get; }

    public ScalarVector3Float64 Imag => new(X, Y, Z);

    public ScalarFloat64 Real => W;

    public ScalarVector4Float64 Vector => new(X, Y, Z, W);

    public static ScalarQuaternionFloat64 Identity
    {
        get
        {
            ScalarFloat64 zero = ScalarFloat64.Broadcast(0d);
            ScalarFloat64 one = ScalarFloat64.Broadcast(1d);
            return new(zero, zero, zero, one);
        }
    }

    public static ScalarQuaternionFloat64 Create(ScalarFloat64 x, ScalarFloat64 y, ScalarFloat64 z, ScalarFloat64 w) => new(x, y, z, w);

    public static ScalarQuaternionFloat64 Create(ScalarVector3Float64 imag, ScalarFloat64 real) => new(imag.X, imag.Y, imag.Z, real);

    public static ScalarQuaternionFloat64 Create(ScalarVector4Float64 vector) => new(vector.X, vector.Y, vector.Z, vector.W);

    public static ScalarQuaternionFloat64 Broadcast(double value)
    {
        ScalarFloat64 zero = ScalarFloat64.Broadcast(0d);
        return new(zero, zero, zero, ScalarFloat64.Broadcast(value));
    }

    public static ScalarQuaternionFloat64 Select(ScalarQuaternionFloat64Mask mask, ScalarQuaternionFloat64 ifTrue, ScalarQuaternionFloat64 ifFalse)
    {
        return new(
            ScalarFloat64.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarFloat64.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarFloat64.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            ScalarFloat64.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    public static ScalarQuaternionFloat64 Select(ScalarVector4Float64Mask mask, ScalarQuaternionFloat64 ifTrue, ScalarQuaternionFloat64 ifFalse)
    {
        return new(
            ScalarFloat64.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarFloat64.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarFloat64.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            ScalarFloat64.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    public static ScalarQuaternionFloat64 Select(ScalarFloat64Mask mask, ScalarQuaternionFloat64 ifTrue, ScalarQuaternionFloat64 ifFalse)
    {
        return new(
            ScalarFloat64.Select(mask, ifTrue.X, ifFalse.X),
            ScalarFloat64.Select(mask, ifTrue.Y, ifFalse.Y),
            ScalarFloat64.Select(mask, ifTrue.Z, ifFalse.Z),
            ScalarFloat64.Select(mask, ifTrue.W, ifFalse.W));
    }

    public static ScalarQuaternionFloat64 Conjugate(ScalarQuaternionFloat64 value) => new(-value.X, -value.Y, -value.Z, value.W);

    public static ScalarFloat64 Dot(ScalarQuaternionFloat64 left, ScalarQuaternionFloat64 right) => ScalarVector4Float64.Dot(left.Vector, right.Vector);

    public static ScalarFloat64 SquaredNorm(ScalarQuaternionFloat64 value) => Dot(value, value);

    public static ScalarFloat64 Norm(ScalarQuaternionFloat64 value) => ScalarFloat64.Sqrt(SquaredNorm(value));

    public static ScalarQuaternionFloat64 Normalize(ScalarQuaternionFloat64 value) => value * ScalarFloat64.ReciprocalSqrt(SquaredNorm(value));

    public static ScalarQuaternionFloat64 Reciprocal(ScalarQuaternionFloat64 value) => Conjugate(value) * ScalarFloat64.Reciprocal(SquaredNorm(value));

    public static ScalarQuaternionFloat64 Multiply(ScalarQuaternionFloat64 left, ScalarQuaternionFloat64 right) => left * right;

    public static ScalarQuaternionFloat64 Multiply(ScalarQuaternionFloat64 quaternion, ScalarFloat64 scalar) => quaternion * scalar;

    public static ScalarQuaternionFloat64 Multiply(ScalarFloat64 scalar, ScalarQuaternionFloat64 quaternion) => quaternion * scalar;

    public static ScalarQuaternionFloat64 Divide(ScalarQuaternionFloat64 left, ScalarQuaternionFloat64 right) => left / right;

    public static ScalarQuaternionFloat64 Divide(ScalarQuaternionFloat64 quaternion, ScalarFloat64 scalar) => quaternion / scalar;

    public static ScalarQuaternionFloat64 FusedMultiplyAdd(ScalarQuaternionFloat64 left, ScalarQuaternionFloat64 right, ScalarQuaternionFloat64 addend)
    {
        ScalarFloat64 t1X = ScalarFloat64.FusedMultiplyAdd(left.X, right.W, left.Y * right.Z);
        ScalarFloat64 t1Y = ScalarFloat64.FusedMultiplyAdd(left.Y, right.W, left.Z * right.X);
        ScalarFloat64 t1Z = ScalarFloat64.FusedMultiplyAdd(left.Z, right.W, left.X * right.Y);
        ScalarFloat64 t1W = ScalarFloat64.FusedMultiplyAdd(left.X, right.X, left.Y * right.Y);

        ScalarFloat64 t2X = ScalarFloat64.FusedMultiplyAdd(left.W, right.X, ScalarFloat64.FusedMultiplyAdd(-left.Z, right.Y, addend.X));
        ScalarFloat64 t2Y = ScalarFloat64.FusedMultiplyAdd(left.W, right.Y, ScalarFloat64.FusedMultiplyAdd(-left.X, right.Z, addend.Y));
        ScalarFloat64 t2Z = ScalarFloat64.FusedMultiplyAdd(left.W, right.Z, ScalarFloat64.FusedMultiplyAdd(-left.Y, right.X, addend.Z));
        ScalarFloat64 t2W = ScalarFloat64.FusedMultiplyAdd(left.W, right.W, ScalarFloat64.FusedMultiplyAdd(-left.Z, right.Z, addend.W));

        return new(t1X + t2X, t1Y + t2Y, t1Z + t2Z, -t1W + t2W);
    }

    public static ScalarQuaternionFloat64 Rotate(ScalarVector3Float64 axis, ScalarFloat64 angle)
    {
        var (sin, cos) = ScalarFloat64.SinCos(angle * ScalarFloat64.Broadcast(0.5d));
        ScalarVector3Float64 imag = axis * new ScalarVector3Float64(sin, sin, sin);
        return new(imag.X, imag.Y, imag.Z, cos);
    }

    public static ScalarVector3Float64 Apply(ScalarQuaternionFloat64 quaternion, ScalarVector3Float64 vector)
    {
        ScalarVector3Float64 imag = quaternion.Imag;
        ScalarFloat64 real = quaternion.Real;
        ScalarFloat64 two = ScalarFloat64.Broadcast(2d);

        ScalarFloat64 imagDotVector = ScalarVector3Float64.Dot(imag, vector);
        ScalarFloat64 realScale = real * real - ScalarVector3Float64.Dot(imag, imag);
        ScalarFloat64 crossScale = two * real;

        return new ScalarVector3Float64(two * imagDotVector, two * imagDotVector, two * imagDotVector) * imag +
               new ScalarVector3Float64(realScale, realScale, realScale) * vector +
               new ScalarVector3Float64(crossScale, crossScale, crossScale) * ScalarVector3Float64.Cross(imag, vector);
    }

    public static ScalarQuaternionFloat64 operator +(ScalarQuaternionFloat64 value) => value;

    public static ScalarQuaternionFloat64 operator +(ScalarQuaternionFloat64 left, ScalarQuaternionFloat64 right) => new(left.X + right.X, left.Y + right.Y, left.Z + right.Z, left.W + right.W);

    public static ScalarQuaternionFloat64 operator -(ScalarQuaternionFloat64 left, ScalarQuaternionFloat64 right) => new(left.X - right.X, left.Y - right.Y, left.Z - right.Z, left.W - right.W);

    public static ScalarQuaternionFloat64 operator *(ScalarQuaternionFloat64 left, ScalarQuaternionFloat64 right)
    {
        ScalarFloat64 t1X = ScalarFloat64.FusedMultiplyAdd(left.X, right.W, left.Y * right.Z);
        ScalarFloat64 t1Y = ScalarFloat64.FusedMultiplyAdd(left.Y, right.W, left.Z * right.X);
        ScalarFloat64 t1Z = ScalarFloat64.FusedMultiplyAdd(left.Z, right.W, left.X * right.Y);
        ScalarFloat64 t1W = ScalarFloat64.FusedMultiplyAdd(left.X, right.X, left.Y * right.Y);

        ScalarFloat64 t2X = ScalarFloat64.FusedMultiplyAdd(left.W, right.X, -(left.Z * right.Y));
        ScalarFloat64 t2Y = ScalarFloat64.FusedMultiplyAdd(left.W, right.Y, -(left.X * right.Z));
        ScalarFloat64 t2Z = ScalarFloat64.FusedMultiplyAdd(left.W, right.Z, -(left.Y * right.X));
        ScalarFloat64 t2W = ScalarFloat64.FusedMultiplyAdd(left.W, right.W, -(left.Z * right.Z));

        return new(t1X + t2X, t1Y + t2Y, t1Z + t2Z, -t1W + t2W);
    }

    public static ScalarQuaternionFloat64 operator *(ScalarQuaternionFloat64 quaternion, ScalarFloat64 scalar)
    {
        ScalarVector4Float64 scalarVector = new(scalar, scalar, scalar, scalar);
        return Create(quaternion.Vector * scalarVector);
    }

    public static ScalarQuaternionFloat64 operator *(ScalarFloat64 scalar, ScalarQuaternionFloat64 quaternion) => quaternion * scalar;

    public static ScalarQuaternionFloat64 operator /(ScalarQuaternionFloat64 left, ScalarQuaternionFloat64 right) => left * Reciprocal(right);

    public static ScalarQuaternionFloat64 operator /(ScalarQuaternionFloat64 quaternion, ScalarFloat64 scalar) => quaternion * ScalarFloat64.Reciprocal(scalar);

    public static ScalarQuaternionFloat64 operator -(ScalarQuaternionFloat64 value) => new(-value.X, -value.Y, -value.Z, -value.W);

    public static ScalarQuaternionFloat64Mask operator ==(ScalarQuaternionFloat64 left, ScalarQuaternionFloat64 right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    public static ScalarQuaternionFloat64Mask operator !=(ScalarQuaternionFloat64 left, ScalarQuaternionFloat64 right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is ScalarQuaternionFloat64 other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}

public readonly struct ScalarQuaternionFloat64Mask :
    ISimdQuaternionMask<ScalarQuaternionFloat64Mask, ScalarFloat64Mask>
{
    public ScalarQuaternionFloat64Mask(ScalarFloat64Mask x, ScalarFloat64Mask y, ScalarFloat64Mask z, ScalarFloat64Mask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => ScalarFloat64Mask.LaneCount;

    public ScalarFloat64Mask X { get; }

    public ScalarFloat64Mask Y { get; }

    public ScalarFloat64Mask Z { get; }

    public ScalarFloat64Mask W { get; }

    public static ScalarQuaternionFloat64Mask True => new(ScalarFloat64Mask.True, ScalarFloat64Mask.True, ScalarFloat64Mask.True, ScalarFloat64Mask.True);

    public static ScalarQuaternionFloat64Mask False => new(ScalarFloat64Mask.False, ScalarFloat64Mask.False, ScalarFloat64Mask.False, ScalarFloat64Mask.False);

    public static ScalarQuaternionFloat64Mask Create(ScalarFloat64Mask x, ScalarFloat64Mask y, ScalarFloat64Mask z, ScalarFloat64Mask w) => new(x, y, z, w);

    public static ScalarQuaternionFloat64Mask Broadcast(ScalarFloat64Mask value) => new(value, value, value, value);

    public static ScalarFloat64Mask All(ScalarQuaternionFloat64Mask value) => value.X & value.Y & value.Z & value.W;

    public static ScalarFloat64Mask Any(ScalarQuaternionFloat64Mask value) => value.X | value.Y | value.Z | value.W;

    public static ScalarFloat64Mask None(ScalarQuaternionFloat64Mask value) => ~(value.X | value.Y | value.Z | value.W);

    public static ScalarQuaternionFloat64Mask AndNot(ScalarQuaternionFloat64Mask left, ScalarQuaternionFloat64Mask right)
    {
        return new(
            ScalarFloat64Mask.AndNot(left.X, right.X),
            ScalarFloat64Mask.AndNot(left.Y, right.Y),
            ScalarFloat64Mask.AndNot(left.Z, right.Z),
            ScalarFloat64Mask.AndNot(left.W, right.W));
    }

    public static ScalarQuaternionFloat64Mask Select(ScalarQuaternionFloat64Mask mask, ScalarQuaternionFloat64Mask ifTrue, ScalarQuaternionFloat64Mask ifFalse)
    {
        return new(
            ScalarFloat64Mask.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarFloat64Mask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarFloat64Mask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            ScalarFloat64Mask.Select(mask.W, ifTrue.W, ifFalse.W));
    }
    public static ScalarQuaternionFloat64Mask And(ScalarQuaternionFloat64Mask left, ScalarQuaternionFloat64Mask right) => left & right;

    public static ScalarQuaternionFloat64Mask Or(ScalarQuaternionFloat64Mask left, ScalarQuaternionFloat64Mask right) => left | right;

    public static ScalarQuaternionFloat64Mask Xor(ScalarQuaternionFloat64Mask left, ScalarQuaternionFloat64Mask right) => left ^ right;

    public static ScalarQuaternionFloat64Mask Not(ScalarQuaternionFloat64Mask value) => ~value;

    public static ScalarQuaternionFloat64Mask operator &(ScalarQuaternionFloat64Mask left, ScalarQuaternionFloat64Mask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    public static ScalarQuaternionFloat64Mask operator |(ScalarQuaternionFloat64Mask left, ScalarQuaternionFloat64Mask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    public static ScalarQuaternionFloat64Mask operator ^(ScalarQuaternionFloat64Mask left, ScalarQuaternionFloat64Mask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    public static ScalarQuaternionFloat64Mask operator ~(ScalarQuaternionFloat64Mask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    public static ScalarQuaternionFloat64Mask operator ==(ScalarQuaternionFloat64Mask left, ScalarQuaternionFloat64Mask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    public static ScalarQuaternionFloat64Mask operator !=(ScalarQuaternionFloat64Mask left, ScalarQuaternionFloat64Mask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj)
    {
        return obj is ScalarQuaternionFloat64Mask other &&
               X.Equals(other.X) &&
               Y.Equals(other.Y) &&
               Z.Equals(other.Z) &&
               W.Equals(other.W);
    }

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}
