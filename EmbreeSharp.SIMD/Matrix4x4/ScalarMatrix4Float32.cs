using System.Numerics;

namespace EmbreeSharp.SIMD;

public readonly struct ScalarMatrix4Float32 :
    ISimdFloatingPointMatrix4<ScalarMatrix4Float32, ScalarVector4Float32, ScalarFloat32, float, ScalarMatrix4Float32Mask, ScalarVector4Float32Mask, ScalarFloat32Mask>
{
    internal readonly Matrix4x4 _value;

    public ScalarMatrix4Float32(ScalarVector4Float32 row0, ScalarVector4Float32 row1, ScalarVector4Float32 row2, ScalarVector4Float32 row3)
    {
        _value = new Matrix4x4(
            row0._value.X, row0._value.Y, row0._value.Z, row0._value.W,
            row1._value.X, row1._value.Y, row1._value.Z, row1._value.W,
            row2._value.X, row2._value.Y, row2._value.Z, row2._value.W,
            row3._value.X, row3._value.Y, row3._value.Z, row3._value.W);
    }

    internal ScalarMatrix4Float32(Matrix4x4 value) => _value = value;

    public static int LaneCount => ScalarVector4Float32.LaneCount;

    public ScalarVector4Float32 Row0 => new(new Vector4(_value.M11, _value.M12, _value.M13, _value.M14));

    public ScalarVector4Float32 Row1 => new(new Vector4(_value.M21, _value.M22, _value.M23, _value.M24));

    public ScalarVector4Float32 Row2 => new(new Vector4(_value.M31, _value.M32, _value.M33, _value.M34));

    public ScalarVector4Float32 Row3 => new(new Vector4(_value.M41, _value.M42, _value.M43, _value.M44));

    public static ScalarMatrix4Float32 Identity => new(Matrix4x4.Identity);

    public static ScalarMatrix4Float32 Create(ScalarVector4Float32 row0, ScalarVector4Float32 row1, ScalarVector4Float32 row2, ScalarVector4Float32 row3) => new(row0, row1, row2, row3);

    public static ScalarMatrix4Float32 Broadcast(float value)
    {
        return new(new Matrix4x4(
            value, value, value, value,
            value, value, value, value,
            value, value, value, value,
            value, value, value, value));
    }

    public static ScalarMatrix4Float32 Select(ScalarMatrix4Float32Mask mask, ScalarMatrix4Float32 ifTrue, ScalarMatrix4Float32 ifFalse)
    {
        return new(
            ScalarVector4Float32.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            ScalarVector4Float32.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            ScalarVector4Float32.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2),
            ScalarVector4Float32.Select(mask.Row3, ifTrue.Row3, ifFalse.Row3));
    }

    public static ScalarMatrix4Float32 Select(ScalarVector4Float32Mask mask, ScalarMatrix4Float32 ifTrue, ScalarMatrix4Float32 ifFalse)
    {
        return new(
            ScalarVector4Float32.Select(mask, ifTrue.Row0, ifFalse.Row0),
            ScalarVector4Float32.Select(mask, ifTrue.Row1, ifFalse.Row1),
            ScalarVector4Float32.Select(mask, ifTrue.Row2, ifFalse.Row2),
            ScalarVector4Float32.Select(mask, ifTrue.Row3, ifFalse.Row3));
    }

    public static ScalarMatrix4Float32 Select(ScalarFloat32Mask mask, ScalarMatrix4Float32 ifTrue, ScalarMatrix4Float32 ifFalse)
    {
        return new(
            ScalarVector4Float32.Select(mask, ifTrue.Row0, ifFalse.Row0),
            ScalarVector4Float32.Select(mask, ifTrue.Row1, ifFalse.Row1),
            ScalarVector4Float32.Select(mask, ifTrue.Row2, ifFalse.Row2),
            ScalarVector4Float32.Select(mask, ifTrue.Row3, ifFalse.Row3));
    }

    public static ScalarVector4Float32 Transform(ScalarMatrix4Float32 matrix, ScalarVector4Float32 vector)
    {
        return new(
            ScalarVector4Float32.Dot(matrix.Row0, vector),
            ScalarVector4Float32.Dot(matrix.Row1, vector),
            ScalarVector4Float32.Dot(matrix.Row2, vector),
            ScalarVector4Float32.Dot(matrix.Row3, vector));
    }

    public static ScalarMatrix4Float32 Multiply(ScalarMatrix4Float32 left, ScalarMatrix4Float32 right) => left * right;

    public static ScalarMatrix4Float32 Multiply(ScalarMatrix4Float32 matrix, ScalarFloat32 scalar) => matrix * scalar;

    public static ScalarMatrix4Float32 Multiply(ScalarFloat32 scalar, ScalarMatrix4Float32 matrix) => scalar * matrix;

    public static ScalarMatrix4Float32 FusedMultiplyAdd(ScalarMatrix4Float32 left, ScalarMatrix4Float32 right, ScalarMatrix4Float32 addend)
    {
        return new(
            new ScalarVector4Float32(
                ScalarFloat32.FusedMultiplyAdd(left.Row0.W, right.Row3.X, ScalarFloat32.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, ScalarFloat32.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, ScalarFloat32.FusedMultiplyAdd(left.Row0.X, right.Row0.X, addend.Row0.X)))),
                ScalarFloat32.FusedMultiplyAdd(left.Row0.W, right.Row3.Y, ScalarFloat32.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, ScalarFloat32.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, ScalarFloat32.FusedMultiplyAdd(left.Row0.X, right.Row0.Y, addend.Row0.Y)))),
                ScalarFloat32.FusedMultiplyAdd(left.Row0.W, right.Row3.Z, ScalarFloat32.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, ScalarFloat32.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, ScalarFloat32.FusedMultiplyAdd(left.Row0.X, right.Row0.Z, addend.Row0.Z)))),
                ScalarFloat32.FusedMultiplyAdd(left.Row0.W, right.Row3.W, ScalarFloat32.FusedMultiplyAdd(left.Row0.Z, right.Row2.W, ScalarFloat32.FusedMultiplyAdd(left.Row0.Y, right.Row1.W, ScalarFloat32.FusedMultiplyAdd(left.Row0.X, right.Row0.W, addend.Row0.W))))),
            new ScalarVector4Float32(
                ScalarFloat32.FusedMultiplyAdd(left.Row1.W, right.Row3.X, ScalarFloat32.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, ScalarFloat32.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, ScalarFloat32.FusedMultiplyAdd(left.Row1.X, right.Row0.X, addend.Row1.X)))),
                ScalarFloat32.FusedMultiplyAdd(left.Row1.W, right.Row3.Y, ScalarFloat32.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, ScalarFloat32.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, ScalarFloat32.FusedMultiplyAdd(left.Row1.X, right.Row0.Y, addend.Row1.Y)))),
                ScalarFloat32.FusedMultiplyAdd(left.Row1.W, right.Row3.Z, ScalarFloat32.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, ScalarFloat32.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, ScalarFloat32.FusedMultiplyAdd(left.Row1.X, right.Row0.Z, addend.Row1.Z)))),
                ScalarFloat32.FusedMultiplyAdd(left.Row1.W, right.Row3.W, ScalarFloat32.FusedMultiplyAdd(left.Row1.Z, right.Row2.W, ScalarFloat32.FusedMultiplyAdd(left.Row1.Y, right.Row1.W, ScalarFloat32.FusedMultiplyAdd(left.Row1.X, right.Row0.W, addend.Row1.W))))),
            new ScalarVector4Float32(
                ScalarFloat32.FusedMultiplyAdd(left.Row2.W, right.Row3.X, ScalarFloat32.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, ScalarFloat32.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, ScalarFloat32.FusedMultiplyAdd(left.Row2.X, right.Row0.X, addend.Row2.X)))),
                ScalarFloat32.FusedMultiplyAdd(left.Row2.W, right.Row3.Y, ScalarFloat32.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, ScalarFloat32.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, ScalarFloat32.FusedMultiplyAdd(left.Row2.X, right.Row0.Y, addend.Row2.Y)))),
                ScalarFloat32.FusedMultiplyAdd(left.Row2.W, right.Row3.Z, ScalarFloat32.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, ScalarFloat32.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, ScalarFloat32.FusedMultiplyAdd(left.Row2.X, right.Row0.Z, addend.Row2.Z)))),
                ScalarFloat32.FusedMultiplyAdd(left.Row2.W, right.Row3.W, ScalarFloat32.FusedMultiplyAdd(left.Row2.Z, right.Row2.W, ScalarFloat32.FusedMultiplyAdd(left.Row2.Y, right.Row1.W, ScalarFloat32.FusedMultiplyAdd(left.Row2.X, right.Row0.W, addend.Row2.W))))),
            new ScalarVector4Float32(
                ScalarFloat32.FusedMultiplyAdd(left.Row3.W, right.Row3.X, ScalarFloat32.FusedMultiplyAdd(left.Row3.Z, right.Row2.X, ScalarFloat32.FusedMultiplyAdd(left.Row3.Y, right.Row1.X, ScalarFloat32.FusedMultiplyAdd(left.Row3.X, right.Row0.X, addend.Row3.X)))),
                ScalarFloat32.FusedMultiplyAdd(left.Row3.W, right.Row3.Y, ScalarFloat32.FusedMultiplyAdd(left.Row3.Z, right.Row2.Y, ScalarFloat32.FusedMultiplyAdd(left.Row3.Y, right.Row1.Y, ScalarFloat32.FusedMultiplyAdd(left.Row3.X, right.Row0.Y, addend.Row3.Y)))),
                ScalarFloat32.FusedMultiplyAdd(left.Row3.W, right.Row3.Z, ScalarFloat32.FusedMultiplyAdd(left.Row3.Z, right.Row2.Z, ScalarFloat32.FusedMultiplyAdd(left.Row3.Y, right.Row1.Z, ScalarFloat32.FusedMultiplyAdd(left.Row3.X, right.Row0.Z, addend.Row3.Z)))),
                ScalarFloat32.FusedMultiplyAdd(left.Row3.W, right.Row3.W, ScalarFloat32.FusedMultiplyAdd(left.Row3.Z, right.Row2.W, ScalarFloat32.FusedMultiplyAdd(left.Row3.Y, right.Row1.W, ScalarFloat32.FusedMultiplyAdd(left.Row3.X, right.Row0.W, addend.Row3.W))))));
    }

    public static ScalarVector4Float32 FusedMultiplyAdd(ScalarMatrix4Float32 matrix, ScalarVector4Float32 vector, ScalarVector4Float32 addend)
    {
        return new(
            ScalarFloat32.FusedMultiplyAdd(matrix.Row0.W, vector.W, ScalarFloat32.FusedMultiplyAdd(matrix.Row0.Z, vector.Z, ScalarFloat32.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, ScalarFloat32.FusedMultiplyAdd(matrix.Row0.X, vector.X, addend.X)))),
            ScalarFloat32.FusedMultiplyAdd(matrix.Row1.W, vector.W, ScalarFloat32.FusedMultiplyAdd(matrix.Row1.Z, vector.Z, ScalarFloat32.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, ScalarFloat32.FusedMultiplyAdd(matrix.Row1.X, vector.X, addend.Y)))),
            ScalarFloat32.FusedMultiplyAdd(matrix.Row2.W, vector.W, ScalarFloat32.FusedMultiplyAdd(matrix.Row2.Z, vector.Z, ScalarFloat32.FusedMultiplyAdd(matrix.Row2.Y, vector.Y, ScalarFloat32.FusedMultiplyAdd(matrix.Row2.X, vector.X, addend.Z)))),
            ScalarFloat32.FusedMultiplyAdd(matrix.Row3.W, vector.W, ScalarFloat32.FusedMultiplyAdd(matrix.Row3.Z, vector.Z, ScalarFloat32.FusedMultiplyAdd(matrix.Row3.Y, vector.Y, ScalarFloat32.FusedMultiplyAdd(matrix.Row3.X, vector.X, addend.W)))));
    }

    public static ScalarMatrix4Float32 Transpose(ScalarMatrix4Float32 matrix) => new(Matrix4x4.Transpose(matrix._value));

    public static ScalarMatrix4Float32 Scale(ScalarVector4Float32 scale) => Scale(scale.X, scale.Y, scale.Z);

    public static ScalarMatrix4Float32 Scale(ScalarFloat32 x, ScalarFloat32 y, ScalarFloat32 z) => new(Matrix4x4.CreateScale(x._value, y._value, z._value));

    public static ScalarMatrix4Float32 Translate(ScalarVector4Float32 translation) => Translate(translation.X, translation.Y, translation.Z);

    public static ScalarMatrix4Float32 Translate(ScalarFloat32 x, ScalarFloat32 y, ScalarFloat32 z)
    {
        return new(Matrix4x4.Transpose(Matrix4x4.CreateTranslation(x._value, y._value, z._value)));
    }

    public static ScalarMatrix4Float32 operator *(ScalarMatrix4Float32 left, ScalarMatrix4Float32 right)
    {
        return new(
            new ScalarVector4Float32(
                ScalarFloat32.FusedMultiplyAdd(left.Row0.W, right.Row3.X, ScalarFloat32.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, ScalarFloat32.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, left.Row0.X * right.Row0.X))),
                ScalarFloat32.FusedMultiplyAdd(left.Row0.W, right.Row3.Y, ScalarFloat32.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, ScalarFloat32.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, left.Row0.X * right.Row0.Y))),
                ScalarFloat32.FusedMultiplyAdd(left.Row0.W, right.Row3.Z, ScalarFloat32.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, ScalarFloat32.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, left.Row0.X * right.Row0.Z))),
                ScalarFloat32.FusedMultiplyAdd(left.Row0.W, right.Row3.W, ScalarFloat32.FusedMultiplyAdd(left.Row0.Z, right.Row2.W, ScalarFloat32.FusedMultiplyAdd(left.Row0.Y, right.Row1.W, left.Row0.X * right.Row0.W)))),
            new ScalarVector4Float32(
                ScalarFloat32.FusedMultiplyAdd(left.Row1.W, right.Row3.X, ScalarFloat32.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, ScalarFloat32.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, left.Row1.X * right.Row0.X))),
                ScalarFloat32.FusedMultiplyAdd(left.Row1.W, right.Row3.Y, ScalarFloat32.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, ScalarFloat32.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, left.Row1.X * right.Row0.Y))),
                ScalarFloat32.FusedMultiplyAdd(left.Row1.W, right.Row3.Z, ScalarFloat32.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, ScalarFloat32.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, left.Row1.X * right.Row0.Z))),
                ScalarFloat32.FusedMultiplyAdd(left.Row1.W, right.Row3.W, ScalarFloat32.FusedMultiplyAdd(left.Row1.Z, right.Row2.W, ScalarFloat32.FusedMultiplyAdd(left.Row1.Y, right.Row1.W, left.Row1.X * right.Row0.W)))),
            new ScalarVector4Float32(
                ScalarFloat32.FusedMultiplyAdd(left.Row2.W, right.Row3.X, ScalarFloat32.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, ScalarFloat32.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, left.Row2.X * right.Row0.X))),
                ScalarFloat32.FusedMultiplyAdd(left.Row2.W, right.Row3.Y, ScalarFloat32.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, ScalarFloat32.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, left.Row2.X * right.Row0.Y))),
                ScalarFloat32.FusedMultiplyAdd(left.Row2.W, right.Row3.Z, ScalarFloat32.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, ScalarFloat32.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, left.Row2.X * right.Row0.Z))),
                ScalarFloat32.FusedMultiplyAdd(left.Row2.W, right.Row3.W, ScalarFloat32.FusedMultiplyAdd(left.Row2.Z, right.Row2.W, ScalarFloat32.FusedMultiplyAdd(left.Row2.Y, right.Row1.W, left.Row2.X * right.Row0.W)))),
            new ScalarVector4Float32(
                ScalarFloat32.FusedMultiplyAdd(left.Row3.W, right.Row3.X, ScalarFloat32.FusedMultiplyAdd(left.Row3.Z, right.Row2.X, ScalarFloat32.FusedMultiplyAdd(left.Row3.Y, right.Row1.X, left.Row3.X * right.Row0.X))),
                ScalarFloat32.FusedMultiplyAdd(left.Row3.W, right.Row3.Y, ScalarFloat32.FusedMultiplyAdd(left.Row3.Z, right.Row2.Y, ScalarFloat32.FusedMultiplyAdd(left.Row3.Y, right.Row1.Y, left.Row3.X * right.Row0.Y))),
                ScalarFloat32.FusedMultiplyAdd(left.Row3.W, right.Row3.Z, ScalarFloat32.FusedMultiplyAdd(left.Row3.Z, right.Row2.Z, ScalarFloat32.FusedMultiplyAdd(left.Row3.Y, right.Row1.Z, left.Row3.X * right.Row0.Z))),
                ScalarFloat32.FusedMultiplyAdd(left.Row3.W, right.Row3.W, ScalarFloat32.FusedMultiplyAdd(left.Row3.Z, right.Row2.W, ScalarFloat32.FusedMultiplyAdd(left.Row3.Y, right.Row1.W, left.Row3.X * right.Row0.W)))));
    }

    public static ScalarVector4Float32 operator *(ScalarMatrix4Float32 matrix, ScalarVector4Float32 vector) => Transform(matrix, vector);

    public static ScalarMatrix4Float32 operator *(ScalarMatrix4Float32 matrix, ScalarFloat32 scalar) => new(Matrix4x4.Multiply(matrix._value, scalar._value));

    public static ScalarMatrix4Float32 operator *(ScalarFloat32 scalar, ScalarMatrix4Float32 matrix) => matrix * scalar;

    public static ScalarMatrix4Float32 Divide(ScalarMatrix4Float32 matrix, ScalarFloat32 scalar) => matrix / scalar;

    public static ScalarMatrix4Float32 operator /(ScalarMatrix4Float32 matrix, ScalarFloat32 scalar) => matrix * ScalarFloat32.Reciprocal(scalar);

    public static ScalarMatrix4Float32 Inverse(ScalarMatrix4Float32 matrix)
    {
        Matrix4x4.Invert(matrix._value, out Matrix4x4 inverted);
        return new(inverted);
    }

    public static ScalarMatrix4Float32 InverseTranspose(ScalarMatrix4Float32 matrix)
    {
        Matrix4x4.Invert(matrix._value, out Matrix4x4 inverted);
        return new(Matrix4x4.Transpose(inverted));
    }

    public static ScalarMatrix4Float32 Rotate(ScalarVector4Float32 axis, ScalarFloat32 angle)
    {
        Vector3 axis3 = new(axis._value.X, axis._value.Y, axis._value.Z);
        return new(Matrix4x4.Transpose(Matrix4x4.CreateFromAxisAngle(axis3, angle._value)));
    }

    public static ScalarMatrix4Float32Mask operator ==(ScalarMatrix4Float32 left, ScalarMatrix4Float32 right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2, left.Row3 == right.Row3);

    public static ScalarMatrix4Float32Mask operator !=(ScalarMatrix4Float32 left, ScalarMatrix4Float32 right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2, left.Row3 != right.Row3);

    public override bool Equals(object? obj) => obj is ScalarMatrix4Float32 other && _value.Equals(other._value);

    public override int GetHashCode() => _value.GetHashCode();
}

public readonly struct ScalarMatrix4Float32Mask :
    ISimdMatrix4Mask<ScalarMatrix4Float32Mask, ScalarVector4Float32Mask, ScalarFloat32Mask>
{
    public ScalarMatrix4Float32Mask(ScalarVector4Float32Mask row0, ScalarVector4Float32Mask row1, ScalarVector4Float32Mask row2, ScalarVector4Float32Mask row3)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
        Row3 = row3;
    }

    public static int LaneCount => ScalarVector4Float32Mask.LaneCount;

    public ScalarVector4Float32Mask Row0 { get; }

    public ScalarVector4Float32Mask Row1 { get; }

    public ScalarVector4Float32Mask Row2 { get; }

    public ScalarVector4Float32Mask Row3 { get; }

    public static ScalarMatrix4Float32Mask True => new(ScalarVector4Float32Mask.True, ScalarVector4Float32Mask.True, ScalarVector4Float32Mask.True, ScalarVector4Float32Mask.True);

    public static ScalarMatrix4Float32Mask False => new(ScalarVector4Float32Mask.False, ScalarVector4Float32Mask.False, ScalarVector4Float32Mask.False, ScalarVector4Float32Mask.False);

    public static ScalarMatrix4Float32Mask Create(ScalarVector4Float32Mask row0, ScalarVector4Float32Mask row1, ScalarVector4Float32Mask row2, ScalarVector4Float32Mask row3) => new(row0, row1, row2, row3);

    public static ScalarMatrix4Float32Mask Broadcast(ScalarVector4Float32Mask value) => new(value, value, value, value);

    public static ScalarVector4Float32Mask All(ScalarMatrix4Float32Mask value) => value.Row0 & value.Row1 & value.Row2 & value.Row3;

    public static ScalarVector4Float32Mask Any(ScalarMatrix4Float32Mask value) => value.Row0 | value.Row1 | value.Row2 | value.Row3;

    public static ScalarVector4Float32Mask None(ScalarMatrix4Float32Mask value) => ~(value.Row0 | value.Row1 | value.Row2 | value.Row3);

    public static ScalarMatrix4Float32Mask AndNot(ScalarMatrix4Float32Mask left, ScalarMatrix4Float32Mask right)
    {
        return new(
            ScalarVector4Float32Mask.AndNot(left.Row0, right.Row0),
            ScalarVector4Float32Mask.AndNot(left.Row1, right.Row1),
            ScalarVector4Float32Mask.AndNot(left.Row2, right.Row2),
            ScalarVector4Float32Mask.AndNot(left.Row3, right.Row3));
    }

    public static ScalarMatrix4Float32Mask Select(ScalarMatrix4Float32Mask mask, ScalarMatrix4Float32Mask ifTrue, ScalarMatrix4Float32Mask ifFalse)
    {
        return new(
            ScalarVector4Float32Mask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            ScalarVector4Float32Mask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            ScalarVector4Float32Mask.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2),
            ScalarVector4Float32Mask.Select(mask.Row3, ifTrue.Row3, ifFalse.Row3));
    }

    public static ScalarMatrix4Float32Mask And(ScalarMatrix4Float32Mask left, ScalarMatrix4Float32Mask right) => left & right;

    public static ScalarMatrix4Float32Mask Or(ScalarMatrix4Float32Mask left, ScalarMatrix4Float32Mask right) => left | right;

    public static ScalarMatrix4Float32Mask Xor(ScalarMatrix4Float32Mask left, ScalarMatrix4Float32Mask right) => left ^ right;

    public static ScalarMatrix4Float32Mask Not(ScalarMatrix4Float32Mask value) => ~value;

    public static ScalarMatrix4Float32Mask operator &(ScalarMatrix4Float32Mask left, ScalarMatrix4Float32Mask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1, left.Row2 & right.Row2, left.Row3 & right.Row3);

    public static ScalarMatrix4Float32Mask operator |(ScalarMatrix4Float32Mask left, ScalarMatrix4Float32Mask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1, left.Row2 | right.Row2, left.Row3 | right.Row3);

    public static ScalarMatrix4Float32Mask operator ^(ScalarMatrix4Float32Mask left, ScalarMatrix4Float32Mask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1, left.Row2 ^ right.Row2, left.Row3 ^ right.Row3);

    public static ScalarMatrix4Float32Mask operator ~(ScalarMatrix4Float32Mask value) => new(~value.Row0, ~value.Row1, ~value.Row2, ~value.Row3);

    public static ScalarMatrix4Float32Mask operator ==(ScalarMatrix4Float32Mask left, ScalarMatrix4Float32Mask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2, left.Row3 == right.Row3);

    public static ScalarMatrix4Float32Mask operator !=(ScalarMatrix4Float32Mask left, ScalarMatrix4Float32Mask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2, left.Row3 != right.Row3);

    public override bool Equals(object? obj) => obj is ScalarMatrix4Float32Mask other && Row0.Equals(other.Row0) && Row1.Equals(other.Row1) && Row2.Equals(other.Row2) && Row3.Equals(other.Row3);

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2, Row3);
}
