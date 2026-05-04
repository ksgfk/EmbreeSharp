using System.Numerics;

namespace EmbreeSharp.SIMD;

public readonly struct ScalarQuaternionFloat32 :
    ISimdQuaternion<ScalarQuaternionFloat32, ScalarVector3Float32, ScalarVector4Float32, ScalarFloat32, float, ScalarQuaternionFloat32Mask, ScalarVector3Float32Mask, ScalarVector4Float32Mask, ScalarFloat32Mask>
{
    internal readonly Quaternion _value;

    public ScalarQuaternionFloat32(ScalarFloat32 x, ScalarFloat32 y, ScalarFloat32 z, ScalarFloat32 w) => _value = new Quaternion(x._value, y._value, z._value, w._value);

    internal ScalarQuaternionFloat32(Quaternion value) => _value = value;

    public static int LaneCount => ScalarFloat32.LaneCount;

    public ScalarFloat32 X => new(_value.X);

    public ScalarFloat32 Y => new(_value.Y);

    public ScalarFloat32 Z => new(_value.Z);

    public ScalarFloat32 W => new(_value.W);

    public ScalarVector3Float32 Imag => new(new Vector3(_value.X, _value.Y, _value.Z));

    public ScalarFloat32 Real => W;

    public ScalarVector4Float32 Vector => new(new Vector4(_value.X, _value.Y, _value.Z, _value.W));

    public static ScalarQuaternionFloat32 Identity => new(Quaternion.Identity);

    public static ScalarQuaternionFloat32 Create(ScalarFloat32 x, ScalarFloat32 y, ScalarFloat32 z, ScalarFloat32 w) => new(x, y, z, w);

    public static ScalarQuaternionFloat32 Create(ScalarVector3Float32 imag, ScalarFloat32 real) => new(new Quaternion(imag._value, real._value));

    public static ScalarQuaternionFloat32 Create(ScalarVector4Float32 vector) => new(new Quaternion(vector._value.X, vector._value.Y, vector._value.Z, vector._value.W));

    public static ScalarQuaternionFloat32 Broadcast(float value) => new(new Quaternion(0f, 0f, 0f, value));

    public static ScalarQuaternionFloat32 Select(ScalarQuaternionFloat32Mask mask, ScalarQuaternionFloat32 ifTrue, ScalarQuaternionFloat32 ifFalse)
    {
        return new(
            ScalarFloat32.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarFloat32.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarFloat32.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            ScalarFloat32.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    public static ScalarQuaternionFloat32 Select(ScalarVector4Float32Mask mask, ScalarQuaternionFloat32 ifTrue, ScalarQuaternionFloat32 ifFalse)
    {
        return new(
            ScalarFloat32.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarFloat32.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarFloat32.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            ScalarFloat32.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    public static ScalarQuaternionFloat32 Select(ScalarFloat32Mask mask, ScalarQuaternionFloat32 ifTrue, ScalarQuaternionFloat32 ifFalse)
    {
        return new(
            ScalarFloat32.Select(mask, ifTrue.X, ifFalse.X),
            ScalarFloat32.Select(mask, ifTrue.Y, ifFalse.Y),
            ScalarFloat32.Select(mask, ifTrue.Z, ifFalse.Z),
            ScalarFloat32.Select(mask, ifTrue.W, ifFalse.W));
    }

    public static ScalarQuaternionFloat32 Conjugate(ScalarQuaternionFloat32 value) => new(Quaternion.Conjugate(value._value));

    public static ScalarFloat32 Dot(ScalarQuaternionFloat32 left, ScalarQuaternionFloat32 right) => ScalarVector4Float32.Dot(left.Vector, right.Vector);

    public static ScalarFloat32 SquaredNorm(ScalarQuaternionFloat32 value) => Dot(value, value);

    public static ScalarFloat32 Norm(ScalarQuaternionFloat32 value) => ScalarFloat32.Sqrt(SquaredNorm(value));

    public static ScalarQuaternionFloat32 Normalize(ScalarQuaternionFloat32 value) => new(Quaternion.Normalize(value._value));

    public static ScalarQuaternionFloat32 Reciprocal(ScalarQuaternionFloat32 value) => new(Quaternion.Inverse(value._value));

    public static ScalarQuaternionFloat32 Multiply(ScalarQuaternionFloat32 left, ScalarQuaternionFloat32 right) => left * right;

    public static ScalarQuaternionFloat32 Multiply(ScalarQuaternionFloat32 quaternion, ScalarFloat32 scalar) => quaternion * scalar;

    public static ScalarQuaternionFloat32 Multiply(ScalarFloat32 scalar, ScalarQuaternionFloat32 quaternion) => quaternion * scalar;

    public static ScalarQuaternionFloat32 Divide(ScalarQuaternionFloat32 left, ScalarQuaternionFloat32 right) => left / right;

    public static ScalarQuaternionFloat32 Divide(ScalarQuaternionFloat32 quaternion, ScalarFloat32 scalar) => quaternion / scalar;

    public static ScalarQuaternionFloat32 FusedMultiplyAdd(ScalarQuaternionFloat32 left, ScalarQuaternionFloat32 right, ScalarQuaternionFloat32 addend)
    {
        ScalarFloat32 t1X = ScalarFloat32.FusedMultiplyAdd(left.X, right.W, left.Y * right.Z);
        ScalarFloat32 t1Y = ScalarFloat32.FusedMultiplyAdd(left.Y, right.W, left.Z * right.X);
        ScalarFloat32 t1Z = ScalarFloat32.FusedMultiplyAdd(left.Z, right.W, left.X * right.Y);
        ScalarFloat32 t1W = ScalarFloat32.FusedMultiplyAdd(left.X, right.X, left.Y * right.Y);

        ScalarFloat32 t2X = ScalarFloat32.FusedMultiplyAdd(left.W, right.X, ScalarFloat32.FusedMultiplyAdd(-left.Z, right.Y, addend.X));
        ScalarFloat32 t2Y = ScalarFloat32.FusedMultiplyAdd(left.W, right.Y, ScalarFloat32.FusedMultiplyAdd(-left.X, right.Z, addend.Y));
        ScalarFloat32 t2Z = ScalarFloat32.FusedMultiplyAdd(left.W, right.Z, ScalarFloat32.FusedMultiplyAdd(-left.Y, right.X, addend.Z));
        ScalarFloat32 t2W = ScalarFloat32.FusedMultiplyAdd(left.W, right.W, ScalarFloat32.FusedMultiplyAdd(-left.Z, right.Z, addend.W));

        return new(t1X + t2X, t1Y + t2Y, t1Z + t2Z, -t1W + t2W);
    }

    public static ScalarQuaternionFloat32 Rotate(ScalarVector3Float32 axis, ScalarFloat32 angle) => new(Quaternion.CreateFromAxisAngle(axis._value, angle._value));

    public static ScalarVector3Float32 Apply(ScalarQuaternionFloat32 quaternion, ScalarVector3Float32 vector) => new(Vector3.Transform(vector._value, quaternion._value));

    public static ScalarQuaternionFloat32 operator +(ScalarQuaternionFloat32 value) => value;

    public static ScalarQuaternionFloat32 operator +(ScalarQuaternionFloat32 left, ScalarQuaternionFloat32 right)
    {
        return new(new Quaternion(
            left._value.X + right._value.X,
            left._value.Y + right._value.Y,
            left._value.Z + right._value.Z,
            left._value.W + right._value.W));
    }

    public static ScalarQuaternionFloat32 operator -(ScalarQuaternionFloat32 left, ScalarQuaternionFloat32 right)
    {
        return new(new Quaternion(
            left._value.X - right._value.X,
            left._value.Y - right._value.Y,
            left._value.Z - right._value.Z,
            left._value.W - right._value.W));
    }

    public static ScalarQuaternionFloat32 operator *(ScalarQuaternionFloat32 left, ScalarQuaternionFloat32 right)
    {
        ScalarFloat32 t1X = ScalarFloat32.FusedMultiplyAdd(left.X, right.W, left.Y * right.Z);
        ScalarFloat32 t1Y = ScalarFloat32.FusedMultiplyAdd(left.Y, right.W, left.Z * right.X);
        ScalarFloat32 t1Z = ScalarFloat32.FusedMultiplyAdd(left.Z, right.W, left.X * right.Y);
        ScalarFloat32 t1W = ScalarFloat32.FusedMultiplyAdd(left.X, right.X, left.Y * right.Y);

        ScalarFloat32 t2X = ScalarFloat32.FusedMultiplyAdd(left.W, right.X, -(left.Z * right.Y));
        ScalarFloat32 t2Y = ScalarFloat32.FusedMultiplyAdd(left.W, right.Y, -(left.X * right.Z));
        ScalarFloat32 t2Z = ScalarFloat32.FusedMultiplyAdd(left.W, right.Z, -(left.Y * right.X));
        ScalarFloat32 t2W = ScalarFloat32.FusedMultiplyAdd(left.W, right.W, -(left.Z * right.Z));

        return new(t1X + t2X, t1Y + t2Y, t1Z + t2Z, -t1W + t2W);
    }

    public static ScalarQuaternionFloat32 operator *(ScalarQuaternionFloat32 quaternion, ScalarFloat32 scalar)
    {
        return new(new Quaternion(
            quaternion._value.X * scalar._value,
            quaternion._value.Y * scalar._value,
            quaternion._value.Z * scalar._value,
            quaternion._value.W * scalar._value));
    }

    public static ScalarQuaternionFloat32 operator *(ScalarFloat32 scalar, ScalarQuaternionFloat32 quaternion) => quaternion * scalar;

    public static ScalarQuaternionFloat32 operator /(ScalarQuaternionFloat32 left, ScalarQuaternionFloat32 right) => left * Reciprocal(right);

    public static ScalarQuaternionFloat32 operator /(ScalarQuaternionFloat32 quaternion, ScalarFloat32 scalar) => quaternion * ScalarFloat32.Reciprocal(scalar);

    public static ScalarQuaternionFloat32 operator -(ScalarQuaternionFloat32 value)
    {
        return new(new Quaternion(-value._value.X, -value._value.Y, -value._value.Z, -value._value.W));
    }

    public static ScalarQuaternionFloat32Mask operator ==(ScalarQuaternionFloat32 left, ScalarQuaternionFloat32 right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    public static ScalarQuaternionFloat32Mask operator !=(ScalarQuaternionFloat32 left, ScalarQuaternionFloat32 right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj) => obj is ScalarQuaternionFloat32 other && _value.Equals(other._value);

    public override int GetHashCode() => _value.GetHashCode();
}

public readonly struct ScalarQuaternionFloat32Mask :
    ISimdQuaternionMask<ScalarQuaternionFloat32Mask, ScalarFloat32Mask>
{
    public ScalarQuaternionFloat32Mask(ScalarFloat32Mask x, ScalarFloat32Mask y, ScalarFloat32Mask z, ScalarFloat32Mask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => ScalarFloat32Mask.LaneCount;

    public ScalarFloat32Mask X { get; }

    public ScalarFloat32Mask Y { get; }

    public ScalarFloat32Mask Z { get; }

    public ScalarFloat32Mask W { get; }

    public static ScalarQuaternionFloat32Mask True => new(ScalarFloat32Mask.True, ScalarFloat32Mask.True, ScalarFloat32Mask.True, ScalarFloat32Mask.True);

    public static ScalarQuaternionFloat32Mask False => new(ScalarFloat32Mask.False, ScalarFloat32Mask.False, ScalarFloat32Mask.False, ScalarFloat32Mask.False);

    public static ScalarQuaternionFloat32Mask Create(ScalarFloat32Mask x, ScalarFloat32Mask y, ScalarFloat32Mask z, ScalarFloat32Mask w) => new(x, y, z, w);

    public static ScalarQuaternionFloat32Mask Broadcast(ScalarFloat32Mask value) => new(value, value, value, value);

    public static ScalarFloat32Mask All(ScalarQuaternionFloat32Mask value) => value.X & value.Y & value.Z & value.W;

    public static ScalarFloat32Mask Any(ScalarQuaternionFloat32Mask value) => value.X | value.Y | value.Z | value.W;

    public static ScalarFloat32Mask None(ScalarQuaternionFloat32Mask value) => ~(value.X | value.Y | value.Z | value.W);

    public static ScalarQuaternionFloat32Mask AndNot(ScalarQuaternionFloat32Mask left, ScalarQuaternionFloat32Mask right)
    {
        return new(
            ScalarFloat32Mask.AndNot(left.X, right.X),
            ScalarFloat32Mask.AndNot(left.Y, right.Y),
            ScalarFloat32Mask.AndNot(left.Z, right.Z),
            ScalarFloat32Mask.AndNot(left.W, right.W));
    }

    public static ScalarQuaternionFloat32Mask Select(ScalarQuaternionFloat32Mask mask, ScalarQuaternionFloat32Mask ifTrue, ScalarQuaternionFloat32Mask ifFalse)
    {
        return new(
            ScalarFloat32Mask.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarFloat32Mask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarFloat32Mask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            ScalarFloat32Mask.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    public static ScalarQuaternionFloat32Mask And(ScalarQuaternionFloat32Mask left, ScalarQuaternionFloat32Mask right) => left & right;

    public static ScalarQuaternionFloat32Mask Or(ScalarQuaternionFloat32Mask left, ScalarQuaternionFloat32Mask right) => left | right;

    public static ScalarQuaternionFloat32Mask Xor(ScalarQuaternionFloat32Mask left, ScalarQuaternionFloat32Mask right) => left ^ right;

    public static ScalarQuaternionFloat32Mask Not(ScalarQuaternionFloat32Mask value) => ~value;

    public static ScalarQuaternionFloat32Mask operator &(ScalarQuaternionFloat32Mask left, ScalarQuaternionFloat32Mask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    public static ScalarQuaternionFloat32Mask operator |(ScalarQuaternionFloat32Mask left, ScalarQuaternionFloat32Mask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    public static ScalarQuaternionFloat32Mask operator ^(ScalarQuaternionFloat32Mask left, ScalarQuaternionFloat32Mask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    public static ScalarQuaternionFloat32Mask operator ~(ScalarQuaternionFloat32Mask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    public static ScalarQuaternionFloat32Mask operator ==(ScalarQuaternionFloat32Mask left, ScalarQuaternionFloat32Mask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    public static ScalarQuaternionFloat32Mask operator !=(ScalarQuaternionFloat32Mask left, ScalarQuaternionFloat32Mask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj) => obj is ScalarQuaternionFloat32Mask other && X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && W.Equals(other.W);

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}
