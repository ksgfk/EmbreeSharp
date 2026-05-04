namespace EmbreeSharp.SIMD;

public readonly struct ScalarMatrix3Float64 :
    ISimdFloatingPointMatrix3<ScalarMatrix3Float64, ScalarVector3Float64, ScalarFloat64, double, ScalarMatrix3Float64Mask, ScalarVector3Float64Mask, ScalarFloat64Mask>
{
    public ScalarMatrix3Float64(ScalarVector3Float64 row0, ScalarVector3Float64 row1, ScalarVector3Float64 row2)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
    }

    public static int LaneCount => ScalarVector3Float64.LaneCount;

    public ScalarVector3Float64 Row0 { get; }

    public ScalarVector3Float64 Row1 { get; }

    public ScalarVector3Float64 Row2 { get; }

    public static ScalarMatrix3Float64 Identity
    {
        get
        {
            ScalarFloat64 zero = ScalarFloat64.Broadcast(0d);
            ScalarFloat64 one = ScalarFloat64.Broadcast(1d);
            return new(
                new ScalarVector3Float64(one, zero, zero),
                new ScalarVector3Float64(zero, one, zero),
                new ScalarVector3Float64(zero, zero, one));
        }
    }

    public static ScalarMatrix3Float64 Create(ScalarVector3Float64 row0, ScalarVector3Float64 row1, ScalarVector3Float64 row2) => new(row0, row1, row2);

    public static ScalarMatrix3Float64 Broadcast(double value)
    {
        ScalarVector3Float64 row = ScalarVector3Float64.Broadcast(value);
        return new(row, row, row);
    }

    public static ScalarMatrix3Float64 Select(ScalarMatrix3Float64Mask mask, ScalarMatrix3Float64 ifTrue, ScalarMatrix3Float64 ifFalse)
    {
        return new(
            ScalarVector3Float64.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            ScalarVector3Float64.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            ScalarVector3Float64.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2));
    }

    public static ScalarMatrix3Float64 Select(ScalarVector3Float64Mask mask, ScalarMatrix3Float64 ifTrue, ScalarMatrix3Float64 ifFalse)
    {
        return new(
            ScalarVector3Float64.Select(mask, ifTrue.Row0, ifFalse.Row0),
            ScalarVector3Float64.Select(mask, ifTrue.Row1, ifFalse.Row1),
            ScalarVector3Float64.Select(mask, ifTrue.Row2, ifFalse.Row2));
    }

    public static ScalarMatrix3Float64 Select(ScalarFloat64Mask mask, ScalarMatrix3Float64 ifTrue, ScalarMatrix3Float64 ifFalse)
    {
        return new(
            ScalarVector3Float64.Select(mask, ifTrue.Row0, ifFalse.Row0),
            ScalarVector3Float64.Select(mask, ifTrue.Row1, ifFalse.Row1),
            ScalarVector3Float64.Select(mask, ifTrue.Row2, ifFalse.Row2));
    }

    public static ScalarVector3Float64 Transform(ScalarMatrix3Float64 matrix, ScalarVector3Float64 vector)
    {
        return new(
            ScalarVector3Float64.Dot(matrix.Row0, vector),
            ScalarVector3Float64.Dot(matrix.Row1, vector),
            ScalarVector3Float64.Dot(matrix.Row2, vector));
    }

    public static ScalarMatrix3Float64 Multiply(ScalarMatrix3Float64 left, ScalarMatrix3Float64 right) => left * right;

    public static ScalarMatrix3Float64 Multiply(ScalarMatrix3Float64 matrix, ScalarFloat64 scalar) => matrix * scalar;

    public static ScalarMatrix3Float64 Multiply(ScalarFloat64 scalar, ScalarMatrix3Float64 matrix) => scalar * matrix;

    public static ScalarMatrix3Float64 FusedMultiplyAdd(ScalarMatrix3Float64 left, ScalarMatrix3Float64 right, ScalarMatrix3Float64 addend)
    {
        return new(
            new ScalarVector3Float64(
                ScalarFloat64.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, ScalarFloat64.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, ScalarFloat64.FusedMultiplyAdd(left.Row0.X, right.Row0.X, addend.Row0.X))),
                ScalarFloat64.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, ScalarFloat64.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, ScalarFloat64.FusedMultiplyAdd(left.Row0.X, right.Row0.Y, addend.Row0.Y))),
                ScalarFloat64.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, ScalarFloat64.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, ScalarFloat64.FusedMultiplyAdd(left.Row0.X, right.Row0.Z, addend.Row0.Z)))),
            new ScalarVector3Float64(
                ScalarFloat64.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, ScalarFloat64.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, ScalarFloat64.FusedMultiplyAdd(left.Row1.X, right.Row0.X, addend.Row1.X))),
                ScalarFloat64.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, ScalarFloat64.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, ScalarFloat64.FusedMultiplyAdd(left.Row1.X, right.Row0.Y, addend.Row1.Y))),
                ScalarFloat64.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, ScalarFloat64.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, ScalarFloat64.FusedMultiplyAdd(left.Row1.X, right.Row0.Z, addend.Row1.Z)))),
            new ScalarVector3Float64(
                ScalarFloat64.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, ScalarFloat64.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, ScalarFloat64.FusedMultiplyAdd(left.Row2.X, right.Row0.X, addend.Row2.X))),
                ScalarFloat64.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, ScalarFloat64.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, ScalarFloat64.FusedMultiplyAdd(left.Row2.X, right.Row0.Y, addend.Row2.Y))),
                ScalarFloat64.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, ScalarFloat64.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, ScalarFloat64.FusedMultiplyAdd(left.Row2.X, right.Row0.Z, addend.Row2.Z)))));
    }

    public static ScalarVector3Float64 FusedMultiplyAdd(ScalarMatrix3Float64 matrix, ScalarVector3Float64 vector, ScalarVector3Float64 addend)
    {
        return new(
            ScalarFloat64.FusedMultiplyAdd(matrix.Row0.Z, vector.Z, ScalarFloat64.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, ScalarFloat64.FusedMultiplyAdd(matrix.Row0.X, vector.X, addend.X))),
            ScalarFloat64.FusedMultiplyAdd(matrix.Row1.Z, vector.Z, ScalarFloat64.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, ScalarFloat64.FusedMultiplyAdd(matrix.Row1.X, vector.X, addend.Y))),
            ScalarFloat64.FusedMultiplyAdd(matrix.Row2.Z, vector.Z, ScalarFloat64.FusedMultiplyAdd(matrix.Row2.Y, vector.Y, ScalarFloat64.FusedMultiplyAdd(matrix.Row2.X, vector.X, addend.Z))));
    }

    public static ScalarMatrix3Float64 Transpose(ScalarMatrix3Float64 matrix)
    {
        return new(
            new(matrix.Row0.X, matrix.Row1.X, matrix.Row2.X),
            new(matrix.Row0.Y, matrix.Row1.Y, matrix.Row2.Y),
            new(matrix.Row0.Z, matrix.Row1.Z, matrix.Row2.Z));
    }

    public static ScalarMatrix3Float64 Scale(ScalarVector3Float64 scale) => Scale(scale.X, scale.Y);

    public static ScalarMatrix3Float64 Scale(ScalarFloat64 x, ScalarFloat64 y)
    {
        ScalarFloat64 zero = ScalarFloat64.Broadcast(0d);
        ScalarFloat64 one = ScalarFloat64.Broadcast(1d);
        return new(
            new(x, zero, zero),
            new(zero, y, zero),
            new(zero, zero, one));
    }

    public static ScalarMatrix3Float64 Translate(ScalarVector3Float64 translation) => Translate(translation.X, translation.Y);

    public static ScalarMatrix3Float64 Translate(ScalarFloat64 x, ScalarFloat64 y)
    {
        ScalarFloat64 zero = ScalarFloat64.Broadcast(0d);
        ScalarFloat64 one = ScalarFloat64.Broadcast(1d);
        return new(
            new(one, zero, x),
            new(zero, one, y),
            new(zero, zero, one));
    }

    public static ScalarMatrix3Float64 operator *(ScalarMatrix3Float64 left, ScalarMatrix3Float64 right)
    {
        return new(
            new ScalarVector3Float64(
                ScalarFloat64.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, ScalarFloat64.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, left.Row0.X * right.Row0.X)),
                ScalarFloat64.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, ScalarFloat64.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, left.Row0.X * right.Row0.Y)),
                ScalarFloat64.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, ScalarFloat64.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, left.Row0.X * right.Row0.Z))),
            new ScalarVector3Float64(
                ScalarFloat64.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, ScalarFloat64.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, left.Row1.X * right.Row0.X)),
                ScalarFloat64.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, ScalarFloat64.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, left.Row1.X * right.Row0.Y)),
                ScalarFloat64.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, ScalarFloat64.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, left.Row1.X * right.Row0.Z))),
            new ScalarVector3Float64(
                ScalarFloat64.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, ScalarFloat64.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, left.Row2.X * right.Row0.X)),
                ScalarFloat64.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, ScalarFloat64.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, left.Row2.X * right.Row0.Y)),
                ScalarFloat64.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, ScalarFloat64.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, left.Row2.X * right.Row0.Z))));
    }

    public static ScalarVector3Float64 operator *(ScalarMatrix3Float64 matrix, ScalarVector3Float64 vector) => Transform(matrix, vector);

    public static ScalarMatrix3Float64 operator *(ScalarMatrix3Float64 matrix, ScalarFloat64 scalar)
    {
        ScalarVector3Float64 scalarVector = new(scalar, scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector, matrix.Row2 * scalarVector);
    }

    public static ScalarMatrix3Float64 operator *(ScalarFloat64 scalar, ScalarMatrix3Float64 matrix) => matrix * scalar;

    public static ScalarMatrix3Float64 Divide(ScalarMatrix3Float64 matrix, ScalarFloat64 scalar) => matrix / scalar;

    public static ScalarMatrix3Float64 operator /(ScalarMatrix3Float64 matrix, ScalarFloat64 scalar) => matrix * ScalarFloat64.Reciprocal(scalar);

    public static ScalarMatrix3Float64 InverseTranspose(ScalarMatrix3Float64 matrix)
    {
        ScalarVector3Float64 col0 = ScalarVector3Float64.Cross(matrix.Row1, matrix.Row2);
        ScalarVector3Float64 col1 = ScalarVector3Float64.Cross(matrix.Row2, matrix.Row0);
        ScalarVector3Float64 col2 = ScalarVector3Float64.Cross(matrix.Row0, matrix.Row1);
        ScalarFloat64 invDet = ScalarFloat64.Reciprocal(ScalarVector3Float64.Dot(matrix.Row0, col0));
        ScalarVector3Float64 invDetVector = new(invDet, invDet, invDet);
        return new(col0 * invDetVector, col1 * invDetVector, col2 * invDetVector);
    }

    public static ScalarMatrix3Float64 Inverse(ScalarMatrix3Float64 matrix) => Transpose(InverseTranspose(matrix));

    public static ScalarMatrix3Float64 Rotate(ScalarFloat64 angle)
    {
        var (sin, cos) = ScalarFloat64.SinCos(angle);
        ScalarFloat64 zero = ScalarFloat64.Broadcast(0d);
        ScalarFloat64 one = ScalarFloat64.Broadcast(1d);
        return new(
            new(cos, -sin, zero),
            new(sin, cos, zero),
            new(zero, zero, one));
    }

    public static ScalarMatrix3Float64Mask operator ==(ScalarMatrix3Float64 left, ScalarMatrix3Float64 right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2);

    public static ScalarMatrix3Float64Mask operator !=(ScalarMatrix3Float64 left, ScalarMatrix3Float64 right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2);

    public override bool Equals(object? obj)
    {
        return obj is ScalarMatrix3Float64 other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2);
}

public readonly struct ScalarMatrix3Float64Mask :
    ISimdMatrix3Mask<ScalarMatrix3Float64Mask, ScalarVector3Float64Mask, ScalarFloat64Mask>
{
    public ScalarMatrix3Float64Mask(ScalarVector3Float64Mask row0, ScalarVector3Float64Mask row1, ScalarVector3Float64Mask row2)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
    }

    public static int LaneCount => ScalarVector3Float64Mask.LaneCount;

    public ScalarVector3Float64Mask Row0 { get; }

    public ScalarVector3Float64Mask Row1 { get; }

    public ScalarVector3Float64Mask Row2 { get; }

    public static ScalarMatrix3Float64Mask True => new(ScalarVector3Float64Mask.True, ScalarVector3Float64Mask.True, ScalarVector3Float64Mask.True);

    public static ScalarMatrix3Float64Mask False => new(ScalarVector3Float64Mask.False, ScalarVector3Float64Mask.False, ScalarVector3Float64Mask.False);

    public static ScalarMatrix3Float64Mask Create(ScalarVector3Float64Mask row0, ScalarVector3Float64Mask row1, ScalarVector3Float64Mask row2) => new(row0, row1, row2);

    public static ScalarMatrix3Float64Mask Broadcast(ScalarVector3Float64Mask value) => new(value, value, value);

    public static ScalarVector3Float64Mask All(ScalarMatrix3Float64Mask value) => value.Row0 & value.Row1 & value.Row2;

    public static ScalarVector3Float64Mask Any(ScalarMatrix3Float64Mask value) => value.Row0 | value.Row1 | value.Row2;

    public static ScalarVector3Float64Mask None(ScalarMatrix3Float64Mask value) => ~(value.Row0 | value.Row1 | value.Row2);

    public static ScalarMatrix3Float64Mask AndNot(ScalarMatrix3Float64Mask left, ScalarMatrix3Float64Mask right)
    {
        return new(
            ScalarVector3Float64Mask.AndNot(left.Row0, right.Row0),
            ScalarVector3Float64Mask.AndNot(left.Row1, right.Row1),
            ScalarVector3Float64Mask.AndNot(left.Row2, right.Row2));
    }

    public static ScalarMatrix3Float64Mask Select(ScalarMatrix3Float64Mask mask, ScalarMatrix3Float64Mask ifTrue, ScalarMatrix3Float64Mask ifFalse)
    {
        return new(
            ScalarVector3Float64Mask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            ScalarVector3Float64Mask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            ScalarVector3Float64Mask.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2));
    }
    public static ScalarMatrix3Float64Mask And(ScalarMatrix3Float64Mask left, ScalarMatrix3Float64Mask right) => left & right;

    public static ScalarMatrix3Float64Mask Or(ScalarMatrix3Float64Mask left, ScalarMatrix3Float64Mask right) => left | right;

    public static ScalarMatrix3Float64Mask Xor(ScalarMatrix3Float64Mask left, ScalarMatrix3Float64Mask right) => left ^ right;

    public static ScalarMatrix3Float64Mask Not(ScalarMatrix3Float64Mask value) => ~value;


    public static ScalarMatrix3Float64Mask operator &(ScalarMatrix3Float64Mask left, ScalarMatrix3Float64Mask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1, left.Row2 & right.Row2);

    public static ScalarMatrix3Float64Mask operator |(ScalarMatrix3Float64Mask left, ScalarMatrix3Float64Mask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1, left.Row2 | right.Row2);

    public static ScalarMatrix3Float64Mask operator ^(ScalarMatrix3Float64Mask left, ScalarMatrix3Float64Mask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1, left.Row2 ^ right.Row2);

    public static ScalarMatrix3Float64Mask operator ~(ScalarMatrix3Float64Mask value) => new(~value.Row0, ~value.Row1, ~value.Row2);

    public static ScalarMatrix3Float64Mask operator ==(ScalarMatrix3Float64Mask left, ScalarMatrix3Float64Mask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2);

    public static ScalarMatrix3Float64Mask operator !=(ScalarMatrix3Float64Mask left, ScalarMatrix3Float64Mask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2);

    public override bool Equals(object? obj)
    {
        return obj is ScalarMatrix3Float64Mask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2);
}
