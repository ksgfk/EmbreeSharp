namespace EmbreeSharp.SIMD;

public readonly struct ScalarMatrix3Float32 :
    ISimdFloatingPointMatrix3<ScalarMatrix3Float32, ScalarVector3Float32, ScalarFloat32, float, ScalarMatrix3Float32Mask, ScalarVector3Float32Mask, ScalarFloat32Mask>
{
    public ScalarMatrix3Float32(ScalarVector3Float32 row0, ScalarVector3Float32 row1, ScalarVector3Float32 row2)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
    }

    public static int LaneCount => ScalarVector3Float32.LaneCount;

    public ScalarVector3Float32 Row0 { get; }

    public ScalarVector3Float32 Row1 { get; }

    public ScalarVector3Float32 Row2 { get; }

    public static ScalarMatrix3Float32 Identity
    {
        get
        {
            ScalarFloat32 zero = ScalarFloat32.Broadcast(0f);
            ScalarFloat32 one = ScalarFloat32.Broadcast(1f);
            return new(
                new ScalarVector3Float32(one, zero, zero),
                new ScalarVector3Float32(zero, one, zero),
                new ScalarVector3Float32(zero, zero, one));
        }
    }

    public static ScalarMatrix3Float32 Create(ScalarVector3Float32 row0, ScalarVector3Float32 row1, ScalarVector3Float32 row2) => new(row0, row1, row2);

    public static ScalarMatrix3Float32 Broadcast(float value)
    {
        ScalarVector3Float32 row = ScalarVector3Float32.Broadcast(value);
        return new(row, row, row);
    }

    public static ScalarMatrix3Float32 Select(ScalarMatrix3Float32Mask mask, ScalarMatrix3Float32 ifTrue, ScalarMatrix3Float32 ifFalse)
    {
        return new(
            ScalarVector3Float32.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            ScalarVector3Float32.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            ScalarVector3Float32.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2));
    }

    public static ScalarMatrix3Float32 Select(ScalarVector3Float32Mask mask, ScalarMatrix3Float32 ifTrue, ScalarMatrix3Float32 ifFalse)
    {
        return new(
            ScalarVector3Float32.Select(mask, ifTrue.Row0, ifFalse.Row0),
            ScalarVector3Float32.Select(mask, ifTrue.Row1, ifFalse.Row1),
            ScalarVector3Float32.Select(mask, ifTrue.Row2, ifFalse.Row2));
    }

    public static ScalarMatrix3Float32 Select(ScalarFloat32Mask mask, ScalarMatrix3Float32 ifTrue, ScalarMatrix3Float32 ifFalse)
    {
        return new(
            ScalarVector3Float32.Select(mask, ifTrue.Row0, ifFalse.Row0),
            ScalarVector3Float32.Select(mask, ifTrue.Row1, ifFalse.Row1),
            ScalarVector3Float32.Select(mask, ifTrue.Row2, ifFalse.Row2));
    }

    public static ScalarVector3Float32 Transform(ScalarMatrix3Float32 matrix, ScalarVector3Float32 vector)
    {
        return new(
            ScalarVector3Float32.Dot(matrix.Row0, vector),
            ScalarVector3Float32.Dot(matrix.Row1, vector),
            ScalarVector3Float32.Dot(matrix.Row2, vector));
    }

    public static ScalarMatrix3Float32 Multiply(ScalarMatrix3Float32 left, ScalarMatrix3Float32 right) => left * right;

    public static ScalarMatrix3Float32 Multiply(ScalarMatrix3Float32 matrix, ScalarFloat32 scalar) => matrix * scalar;

    public static ScalarMatrix3Float32 Multiply(ScalarFloat32 scalar, ScalarMatrix3Float32 matrix) => scalar * matrix;

    public static ScalarMatrix3Float32 FusedMultiplyAdd(ScalarMatrix3Float32 left, ScalarMatrix3Float32 right, ScalarMatrix3Float32 addend)
    {
        return new(
            new ScalarVector3Float32(
                ScalarFloat32.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, ScalarFloat32.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, ScalarFloat32.FusedMultiplyAdd(left.Row0.X, right.Row0.X, addend.Row0.X))),
                ScalarFloat32.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, ScalarFloat32.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, ScalarFloat32.FusedMultiplyAdd(left.Row0.X, right.Row0.Y, addend.Row0.Y))),
                ScalarFloat32.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, ScalarFloat32.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, ScalarFloat32.FusedMultiplyAdd(left.Row0.X, right.Row0.Z, addend.Row0.Z)))),
            new ScalarVector3Float32(
                ScalarFloat32.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, ScalarFloat32.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, ScalarFloat32.FusedMultiplyAdd(left.Row1.X, right.Row0.X, addend.Row1.X))),
                ScalarFloat32.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, ScalarFloat32.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, ScalarFloat32.FusedMultiplyAdd(left.Row1.X, right.Row0.Y, addend.Row1.Y))),
                ScalarFloat32.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, ScalarFloat32.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, ScalarFloat32.FusedMultiplyAdd(left.Row1.X, right.Row0.Z, addend.Row1.Z)))),
            new ScalarVector3Float32(
                ScalarFloat32.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, ScalarFloat32.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, ScalarFloat32.FusedMultiplyAdd(left.Row2.X, right.Row0.X, addend.Row2.X))),
                ScalarFloat32.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, ScalarFloat32.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, ScalarFloat32.FusedMultiplyAdd(left.Row2.X, right.Row0.Y, addend.Row2.Y))),
                ScalarFloat32.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, ScalarFloat32.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, ScalarFloat32.FusedMultiplyAdd(left.Row2.X, right.Row0.Z, addend.Row2.Z)))));
    }

    public static ScalarVector3Float32 FusedMultiplyAdd(ScalarMatrix3Float32 matrix, ScalarVector3Float32 vector, ScalarVector3Float32 addend)
    {
        return new(
            ScalarFloat32.FusedMultiplyAdd(matrix.Row0.Z, vector.Z, ScalarFloat32.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, ScalarFloat32.FusedMultiplyAdd(matrix.Row0.X, vector.X, addend.X))),
            ScalarFloat32.FusedMultiplyAdd(matrix.Row1.Z, vector.Z, ScalarFloat32.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, ScalarFloat32.FusedMultiplyAdd(matrix.Row1.X, vector.X, addend.Y))),
            ScalarFloat32.FusedMultiplyAdd(matrix.Row2.Z, vector.Z, ScalarFloat32.FusedMultiplyAdd(matrix.Row2.Y, vector.Y, ScalarFloat32.FusedMultiplyAdd(matrix.Row2.X, vector.X, addend.Z))));
    }

    public static ScalarMatrix3Float32 Transpose(ScalarMatrix3Float32 matrix)
    {
        return new(
            new(matrix.Row0.X, matrix.Row1.X, matrix.Row2.X),
            new(matrix.Row0.Y, matrix.Row1.Y, matrix.Row2.Y),
            new(matrix.Row0.Z, matrix.Row1.Z, matrix.Row2.Z));
    }

    public static ScalarMatrix3Float32 Scale(ScalarVector3Float32 scale) => Scale(scale.X, scale.Y);

    public static ScalarMatrix3Float32 Scale(ScalarFloat32 x, ScalarFloat32 y)
    {
        ScalarFloat32 zero = ScalarFloat32.Broadcast(0f);
        ScalarFloat32 one = ScalarFloat32.Broadcast(1f);
        return new(
            new(x, zero, zero),
            new(zero, y, zero),
            new(zero, zero, one));
    }

    public static ScalarMatrix3Float32 Translate(ScalarVector3Float32 translation) => Translate(translation.X, translation.Y);

    public static ScalarMatrix3Float32 Translate(ScalarFloat32 x, ScalarFloat32 y)
    {
        ScalarFloat32 zero = ScalarFloat32.Broadcast(0f);
        ScalarFloat32 one = ScalarFloat32.Broadcast(1f);
        return new(
            new(one, zero, x),
            new(zero, one, y),
            new(zero, zero, one));
    }

    public static ScalarMatrix3Float32 operator *(ScalarMatrix3Float32 left, ScalarMatrix3Float32 right)
    {
        return new(
            new ScalarVector3Float32(
                ScalarFloat32.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, ScalarFloat32.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, left.Row0.X * right.Row0.X)),
                ScalarFloat32.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, ScalarFloat32.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, left.Row0.X * right.Row0.Y)),
                ScalarFloat32.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, ScalarFloat32.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, left.Row0.X * right.Row0.Z))),
            new ScalarVector3Float32(
                ScalarFloat32.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, ScalarFloat32.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, left.Row1.X * right.Row0.X)),
                ScalarFloat32.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, ScalarFloat32.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, left.Row1.X * right.Row0.Y)),
                ScalarFloat32.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, ScalarFloat32.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, left.Row1.X * right.Row0.Z))),
            new ScalarVector3Float32(
                ScalarFloat32.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, ScalarFloat32.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, left.Row2.X * right.Row0.X)),
                ScalarFloat32.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, ScalarFloat32.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, left.Row2.X * right.Row0.Y)),
                ScalarFloat32.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, ScalarFloat32.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, left.Row2.X * right.Row0.Z))));
    }

    public static ScalarVector3Float32 operator *(ScalarMatrix3Float32 matrix, ScalarVector3Float32 vector) => Transform(matrix, vector);

    public static ScalarMatrix3Float32 operator *(ScalarMatrix3Float32 matrix, ScalarFloat32 scalar)
    {
        ScalarVector3Float32 scalarVector = new(scalar, scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector, matrix.Row2 * scalarVector);
    }

    public static ScalarMatrix3Float32 operator *(ScalarFloat32 scalar, ScalarMatrix3Float32 matrix) => matrix * scalar;

    public static ScalarMatrix3Float32 Divide(ScalarMatrix3Float32 matrix, ScalarFloat32 scalar) => matrix / scalar;

    public static ScalarMatrix3Float32 operator /(ScalarMatrix3Float32 matrix, ScalarFloat32 scalar) => matrix * ScalarFloat32.Reciprocal(scalar);

    public static ScalarMatrix3Float32 InverseTranspose(ScalarMatrix3Float32 matrix)
    {
        ScalarVector3Float32 col0 = ScalarVector3Float32.Cross(matrix.Row1, matrix.Row2);
        ScalarVector3Float32 col1 = ScalarVector3Float32.Cross(matrix.Row2, matrix.Row0);
        ScalarVector3Float32 col2 = ScalarVector3Float32.Cross(matrix.Row0, matrix.Row1);
        ScalarFloat32 invDet = ScalarFloat32.Reciprocal(ScalarVector3Float32.Dot(matrix.Row0, col0));
        ScalarVector3Float32 invDetVector = new(invDet, invDet, invDet);
        return new(col0 * invDetVector, col1 * invDetVector, col2 * invDetVector);
    }

    public static ScalarMatrix3Float32 Inverse(ScalarMatrix3Float32 matrix) => Transpose(InverseTranspose(matrix));

    public static ScalarMatrix3Float32 Rotate(ScalarFloat32 angle)
    {
        var (sin, cos) = ScalarFloat32.SinCos(angle);
        ScalarFloat32 zero = ScalarFloat32.Broadcast(0f);
        ScalarFloat32 one = ScalarFloat32.Broadcast(1f);
        return new(
            new(cos, -sin, zero),
            new(sin, cos, zero),
            new(zero, zero, one));
    }

    public static ScalarMatrix3Float32Mask operator ==(ScalarMatrix3Float32 left, ScalarMatrix3Float32 right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2);

    public static ScalarMatrix3Float32Mask operator !=(ScalarMatrix3Float32 left, ScalarMatrix3Float32 right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2);

    public override bool Equals(object? obj)
    {
        return obj is ScalarMatrix3Float32 other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2);
}

public readonly struct ScalarMatrix3Float32Mask :
    ISimdMatrix3Mask<ScalarMatrix3Float32Mask, ScalarVector3Float32Mask, ScalarFloat32Mask>
{
    public ScalarMatrix3Float32Mask(ScalarVector3Float32Mask row0, ScalarVector3Float32Mask row1, ScalarVector3Float32Mask row2)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
    }

    public static int LaneCount => ScalarVector3Float32Mask.LaneCount;

    public ScalarVector3Float32Mask Row0 { get; }

    public ScalarVector3Float32Mask Row1 { get; }

    public ScalarVector3Float32Mask Row2 { get; }

    public static ScalarMatrix3Float32Mask True => new(ScalarVector3Float32Mask.True, ScalarVector3Float32Mask.True, ScalarVector3Float32Mask.True);

    public static ScalarMatrix3Float32Mask False => new(ScalarVector3Float32Mask.False, ScalarVector3Float32Mask.False, ScalarVector3Float32Mask.False);

    public static ScalarMatrix3Float32Mask Create(ScalarVector3Float32Mask row0, ScalarVector3Float32Mask row1, ScalarVector3Float32Mask row2) => new(row0, row1, row2);

    public static ScalarMatrix3Float32Mask Broadcast(ScalarVector3Float32Mask value) => new(value, value, value);

    public static ScalarVector3Float32Mask All(ScalarMatrix3Float32Mask value) => value.Row0 & value.Row1 & value.Row2;

    public static ScalarVector3Float32Mask Any(ScalarMatrix3Float32Mask value) => value.Row0 | value.Row1 | value.Row2;

    public static ScalarVector3Float32Mask None(ScalarMatrix3Float32Mask value) => ~(value.Row0 | value.Row1 | value.Row2);

    public static ScalarMatrix3Float32Mask AndNot(ScalarMatrix3Float32Mask left, ScalarMatrix3Float32Mask right)
    {
        return new(
            ScalarVector3Float32Mask.AndNot(left.Row0, right.Row0),
            ScalarVector3Float32Mask.AndNot(left.Row1, right.Row1),
            ScalarVector3Float32Mask.AndNot(left.Row2, right.Row2));
    }

    public static ScalarMatrix3Float32Mask Select(ScalarMatrix3Float32Mask mask, ScalarMatrix3Float32Mask ifTrue, ScalarMatrix3Float32Mask ifFalse)
    {
        return new(
            ScalarVector3Float32Mask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            ScalarVector3Float32Mask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            ScalarVector3Float32Mask.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2));
    }
    public static ScalarMatrix3Float32Mask And(ScalarMatrix3Float32Mask left, ScalarMatrix3Float32Mask right) => left & right;

    public static ScalarMatrix3Float32Mask Or(ScalarMatrix3Float32Mask left, ScalarMatrix3Float32Mask right) => left | right;

    public static ScalarMatrix3Float32Mask Xor(ScalarMatrix3Float32Mask left, ScalarMatrix3Float32Mask right) => left ^ right;

    public static ScalarMatrix3Float32Mask Not(ScalarMatrix3Float32Mask value) => ~value;


    public static ScalarMatrix3Float32Mask operator &(ScalarMatrix3Float32Mask left, ScalarMatrix3Float32Mask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1, left.Row2 & right.Row2);

    public static ScalarMatrix3Float32Mask operator |(ScalarMatrix3Float32Mask left, ScalarMatrix3Float32Mask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1, left.Row2 | right.Row2);

    public static ScalarMatrix3Float32Mask operator ^(ScalarMatrix3Float32Mask left, ScalarMatrix3Float32Mask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1, left.Row2 ^ right.Row2);

    public static ScalarMatrix3Float32Mask operator ~(ScalarMatrix3Float32Mask value) => new(~value.Row0, ~value.Row1, ~value.Row2);

    public static ScalarMatrix3Float32Mask operator ==(ScalarMatrix3Float32Mask left, ScalarMatrix3Float32Mask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2);

    public static ScalarMatrix3Float32Mask operator !=(ScalarMatrix3Float32Mask left, ScalarMatrix3Float32Mask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2);

    public override bool Equals(object? obj)
    {
        return obj is ScalarMatrix3Float32Mask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2);
}
