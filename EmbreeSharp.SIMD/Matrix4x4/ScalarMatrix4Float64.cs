namespace EmbreeSharp.SIMD;

public readonly struct ScalarMatrix4Float64 :
    ISimdFloatingPointMatrix4<ScalarMatrix4Float64, ScalarVector4Float64, ScalarFloat64, double, ScalarMatrix4Float64Mask, ScalarVector4Float64Mask, ScalarFloat64Mask>
{
    public ScalarMatrix4Float64(ScalarVector4Float64 row0, ScalarVector4Float64 row1, ScalarVector4Float64 row2, ScalarVector4Float64 row3)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
        Row3 = row3;
    }

    public static int LaneCount => ScalarVector4Float64.LaneCount;

    public ScalarVector4Float64 Row0 { get; }

    public ScalarVector4Float64 Row1 { get; }

    public ScalarVector4Float64 Row2 { get; }

    public ScalarVector4Float64 Row3 { get; }

    public static ScalarMatrix4Float64 Identity
    {
        get
        {
            ScalarFloat64 zero = ScalarFloat64.Broadcast(0d);
            ScalarFloat64 one = ScalarFloat64.Broadcast(1d);
            return new(
                new ScalarVector4Float64(one, zero, zero, zero),
                new ScalarVector4Float64(zero, one, zero, zero),
                new ScalarVector4Float64(zero, zero, one, zero),
                new ScalarVector4Float64(zero, zero, zero, one));
        }
    }

    public static ScalarMatrix4Float64 Create(ScalarVector4Float64 row0, ScalarVector4Float64 row1, ScalarVector4Float64 row2, ScalarVector4Float64 row3) => new(row0, row1, row2, row3);

    public static ScalarMatrix4Float64 Broadcast(double value)
    {
        ScalarVector4Float64 row = ScalarVector4Float64.Broadcast(value);
        return new(row, row, row, row);
    }

    public static ScalarMatrix4Float64 Select(ScalarMatrix4Float64Mask mask, ScalarMatrix4Float64 ifTrue, ScalarMatrix4Float64 ifFalse)
    {
        return new(
            ScalarVector4Float64.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            ScalarVector4Float64.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            ScalarVector4Float64.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2),
            ScalarVector4Float64.Select(mask.Row3, ifTrue.Row3, ifFalse.Row3));
    }

    public static ScalarMatrix4Float64 Select(ScalarVector4Float64Mask mask, ScalarMatrix4Float64 ifTrue, ScalarMatrix4Float64 ifFalse)
    {
        return new(
            ScalarVector4Float64.Select(mask, ifTrue.Row0, ifFalse.Row0),
            ScalarVector4Float64.Select(mask, ifTrue.Row1, ifFalse.Row1),
            ScalarVector4Float64.Select(mask, ifTrue.Row2, ifFalse.Row2),
            ScalarVector4Float64.Select(mask, ifTrue.Row3, ifFalse.Row3));
    }

    public static ScalarMatrix4Float64 Select(ScalarFloat64Mask mask, ScalarMatrix4Float64 ifTrue, ScalarMatrix4Float64 ifFalse)
    {
        return new(
            ScalarVector4Float64.Select(mask, ifTrue.Row0, ifFalse.Row0),
            ScalarVector4Float64.Select(mask, ifTrue.Row1, ifFalse.Row1),
            ScalarVector4Float64.Select(mask, ifTrue.Row2, ifFalse.Row2),
            ScalarVector4Float64.Select(mask, ifTrue.Row3, ifFalse.Row3));
    }

    public static ScalarVector4Float64 Transform(ScalarMatrix4Float64 matrix, ScalarVector4Float64 vector)
    {
        return new(
            ScalarFloat64.FusedMultiplyAdd(matrix.Row0.W, vector.W, ScalarFloat64.FusedMultiplyAdd(matrix.Row0.Z, vector.Z, ScalarFloat64.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, matrix.Row0.X * vector.X))),
            ScalarFloat64.FusedMultiplyAdd(matrix.Row1.W, vector.W, ScalarFloat64.FusedMultiplyAdd(matrix.Row1.Z, vector.Z, ScalarFloat64.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, matrix.Row1.X * vector.X))),
            ScalarFloat64.FusedMultiplyAdd(matrix.Row2.W, vector.W, ScalarFloat64.FusedMultiplyAdd(matrix.Row2.Z, vector.Z, ScalarFloat64.FusedMultiplyAdd(matrix.Row2.Y, vector.Y, matrix.Row2.X * vector.X))),
            ScalarFloat64.FusedMultiplyAdd(matrix.Row3.W, vector.W, ScalarFloat64.FusedMultiplyAdd(matrix.Row3.Z, vector.Z, ScalarFloat64.FusedMultiplyAdd(matrix.Row3.Y, vector.Y, matrix.Row3.X * vector.X))));
    }

    public static ScalarMatrix4Float64 Multiply(ScalarMatrix4Float64 left, ScalarMatrix4Float64 right) => left * right;

    public static ScalarMatrix4Float64 Multiply(ScalarMatrix4Float64 matrix, ScalarFloat64 scalar) => matrix * scalar;

    public static ScalarMatrix4Float64 Multiply(ScalarFloat64 scalar, ScalarMatrix4Float64 matrix) => scalar * matrix;

    public static ScalarMatrix4Float64 FusedMultiplyAdd(ScalarMatrix4Float64 left, ScalarMatrix4Float64 right, ScalarMatrix4Float64 addend)
    {
        return new(
            new ScalarVector4Float64(
                ScalarFloat64.FusedMultiplyAdd(left.Row0.W, right.Row3.X, ScalarFloat64.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, ScalarFloat64.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, ScalarFloat64.FusedMultiplyAdd(left.Row0.X, right.Row0.X, addend.Row0.X)))),
                ScalarFloat64.FusedMultiplyAdd(left.Row0.W, right.Row3.Y, ScalarFloat64.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, ScalarFloat64.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, ScalarFloat64.FusedMultiplyAdd(left.Row0.X, right.Row0.Y, addend.Row0.Y)))),
                ScalarFloat64.FusedMultiplyAdd(left.Row0.W, right.Row3.Z, ScalarFloat64.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, ScalarFloat64.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, ScalarFloat64.FusedMultiplyAdd(left.Row0.X, right.Row0.Z, addend.Row0.Z)))),
                ScalarFloat64.FusedMultiplyAdd(left.Row0.W, right.Row3.W, ScalarFloat64.FusedMultiplyAdd(left.Row0.Z, right.Row2.W, ScalarFloat64.FusedMultiplyAdd(left.Row0.Y, right.Row1.W, ScalarFloat64.FusedMultiplyAdd(left.Row0.X, right.Row0.W, addend.Row0.W))))),
            new ScalarVector4Float64(
                ScalarFloat64.FusedMultiplyAdd(left.Row1.W, right.Row3.X, ScalarFloat64.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, ScalarFloat64.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, ScalarFloat64.FusedMultiplyAdd(left.Row1.X, right.Row0.X, addend.Row1.X)))),
                ScalarFloat64.FusedMultiplyAdd(left.Row1.W, right.Row3.Y, ScalarFloat64.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, ScalarFloat64.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, ScalarFloat64.FusedMultiplyAdd(left.Row1.X, right.Row0.Y, addend.Row1.Y)))),
                ScalarFloat64.FusedMultiplyAdd(left.Row1.W, right.Row3.Z, ScalarFloat64.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, ScalarFloat64.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, ScalarFloat64.FusedMultiplyAdd(left.Row1.X, right.Row0.Z, addend.Row1.Z)))),
                ScalarFloat64.FusedMultiplyAdd(left.Row1.W, right.Row3.W, ScalarFloat64.FusedMultiplyAdd(left.Row1.Z, right.Row2.W, ScalarFloat64.FusedMultiplyAdd(left.Row1.Y, right.Row1.W, ScalarFloat64.FusedMultiplyAdd(left.Row1.X, right.Row0.W, addend.Row1.W))))),
            new ScalarVector4Float64(
                ScalarFloat64.FusedMultiplyAdd(left.Row2.W, right.Row3.X, ScalarFloat64.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, ScalarFloat64.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, ScalarFloat64.FusedMultiplyAdd(left.Row2.X, right.Row0.X, addend.Row2.X)))),
                ScalarFloat64.FusedMultiplyAdd(left.Row2.W, right.Row3.Y, ScalarFloat64.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, ScalarFloat64.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, ScalarFloat64.FusedMultiplyAdd(left.Row2.X, right.Row0.Y, addend.Row2.Y)))),
                ScalarFloat64.FusedMultiplyAdd(left.Row2.W, right.Row3.Z, ScalarFloat64.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, ScalarFloat64.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, ScalarFloat64.FusedMultiplyAdd(left.Row2.X, right.Row0.Z, addend.Row2.Z)))),
                ScalarFloat64.FusedMultiplyAdd(left.Row2.W, right.Row3.W, ScalarFloat64.FusedMultiplyAdd(left.Row2.Z, right.Row2.W, ScalarFloat64.FusedMultiplyAdd(left.Row2.Y, right.Row1.W, ScalarFloat64.FusedMultiplyAdd(left.Row2.X, right.Row0.W, addend.Row2.W))))),
            new ScalarVector4Float64(
                ScalarFloat64.FusedMultiplyAdd(left.Row3.W, right.Row3.X, ScalarFloat64.FusedMultiplyAdd(left.Row3.Z, right.Row2.X, ScalarFloat64.FusedMultiplyAdd(left.Row3.Y, right.Row1.X, ScalarFloat64.FusedMultiplyAdd(left.Row3.X, right.Row0.X, addend.Row3.X)))),
                ScalarFloat64.FusedMultiplyAdd(left.Row3.W, right.Row3.Y, ScalarFloat64.FusedMultiplyAdd(left.Row3.Z, right.Row2.Y, ScalarFloat64.FusedMultiplyAdd(left.Row3.Y, right.Row1.Y, ScalarFloat64.FusedMultiplyAdd(left.Row3.X, right.Row0.Y, addend.Row3.Y)))),
                ScalarFloat64.FusedMultiplyAdd(left.Row3.W, right.Row3.Z, ScalarFloat64.FusedMultiplyAdd(left.Row3.Z, right.Row2.Z, ScalarFloat64.FusedMultiplyAdd(left.Row3.Y, right.Row1.Z, ScalarFloat64.FusedMultiplyAdd(left.Row3.X, right.Row0.Z, addend.Row3.Z)))),
                ScalarFloat64.FusedMultiplyAdd(left.Row3.W, right.Row3.W, ScalarFloat64.FusedMultiplyAdd(left.Row3.Z, right.Row2.W, ScalarFloat64.FusedMultiplyAdd(left.Row3.Y, right.Row1.W, ScalarFloat64.FusedMultiplyAdd(left.Row3.X, right.Row0.W, addend.Row3.W))))));
    }

    public static ScalarVector4Float64 FusedMultiplyAdd(ScalarMatrix4Float64 matrix, ScalarVector4Float64 vector, ScalarVector4Float64 addend)
    {
        return new(
            ScalarFloat64.FusedMultiplyAdd(matrix.Row0.W, vector.W, ScalarFloat64.FusedMultiplyAdd(matrix.Row0.Z, vector.Z, ScalarFloat64.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, ScalarFloat64.FusedMultiplyAdd(matrix.Row0.X, vector.X, addend.X)))),
            ScalarFloat64.FusedMultiplyAdd(matrix.Row1.W, vector.W, ScalarFloat64.FusedMultiplyAdd(matrix.Row1.Z, vector.Z, ScalarFloat64.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, ScalarFloat64.FusedMultiplyAdd(matrix.Row1.X, vector.X, addend.Y)))),
            ScalarFloat64.FusedMultiplyAdd(matrix.Row2.W, vector.W, ScalarFloat64.FusedMultiplyAdd(matrix.Row2.Z, vector.Z, ScalarFloat64.FusedMultiplyAdd(matrix.Row2.Y, vector.Y, ScalarFloat64.FusedMultiplyAdd(matrix.Row2.X, vector.X, addend.Z)))),
            ScalarFloat64.FusedMultiplyAdd(matrix.Row3.W, vector.W, ScalarFloat64.FusedMultiplyAdd(matrix.Row3.Z, vector.Z, ScalarFloat64.FusedMultiplyAdd(matrix.Row3.Y, vector.Y, ScalarFloat64.FusedMultiplyAdd(matrix.Row3.X, vector.X, addend.W)))));
    }

    public static ScalarMatrix4Float64 Transpose(ScalarMatrix4Float64 matrix)
    {
        return new(
            new(matrix.Row0.X, matrix.Row1.X, matrix.Row2.X, matrix.Row3.X),
            new(matrix.Row0.Y, matrix.Row1.Y, matrix.Row2.Y, matrix.Row3.Y),
            new(matrix.Row0.Z, matrix.Row1.Z, matrix.Row2.Z, matrix.Row3.Z),
            new(matrix.Row0.W, matrix.Row1.W, matrix.Row2.W, matrix.Row3.W));
    }

    public static ScalarMatrix4Float64 Scale(ScalarVector4Float64 scale) => Scale(scale.X, scale.Y, scale.Z);

    public static ScalarMatrix4Float64 Scale(ScalarFloat64 x, ScalarFloat64 y, ScalarFloat64 z)
    {
        ScalarFloat64 zero = ScalarFloat64.Broadcast(0d);
        ScalarFloat64 one = ScalarFloat64.Broadcast(1d);
        return new(
            new(x, zero, zero, zero),
            new(zero, y, zero, zero),
            new(zero, zero, z, zero),
            new(zero, zero, zero, one));
    }

    public static ScalarMatrix4Float64 Translate(ScalarVector4Float64 translation) => Translate(translation.X, translation.Y, translation.Z);

    public static ScalarMatrix4Float64 Translate(ScalarFloat64 x, ScalarFloat64 y, ScalarFloat64 z)
    {
        ScalarFloat64 zero = ScalarFloat64.Broadcast(0d);
        ScalarFloat64 one = ScalarFloat64.Broadcast(1d);
        return new(
            new(one, zero, zero, x),
            new(zero, one, zero, y),
            new(zero, zero, one, z),
            new(zero, zero, zero, one));
    }

    public static ScalarMatrix4Float64 operator *(ScalarMatrix4Float64 left, ScalarMatrix4Float64 right)
    {
        return new(
            new ScalarVector4Float64(
                ScalarFloat64.FusedMultiplyAdd(left.Row0.W, right.Row3.X, ScalarFloat64.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, ScalarFloat64.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, left.Row0.X * right.Row0.X))),
                ScalarFloat64.FusedMultiplyAdd(left.Row0.W, right.Row3.Y, ScalarFloat64.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, ScalarFloat64.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, left.Row0.X * right.Row0.Y))),
                ScalarFloat64.FusedMultiplyAdd(left.Row0.W, right.Row3.Z, ScalarFloat64.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, ScalarFloat64.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, left.Row0.X * right.Row0.Z))),
                ScalarFloat64.FusedMultiplyAdd(left.Row0.W, right.Row3.W, ScalarFloat64.FusedMultiplyAdd(left.Row0.Z, right.Row2.W, ScalarFloat64.FusedMultiplyAdd(left.Row0.Y, right.Row1.W, left.Row0.X * right.Row0.W)))),
            new ScalarVector4Float64(
                ScalarFloat64.FusedMultiplyAdd(left.Row1.W, right.Row3.X, ScalarFloat64.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, ScalarFloat64.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, left.Row1.X * right.Row0.X))),
                ScalarFloat64.FusedMultiplyAdd(left.Row1.W, right.Row3.Y, ScalarFloat64.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, ScalarFloat64.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, left.Row1.X * right.Row0.Y))),
                ScalarFloat64.FusedMultiplyAdd(left.Row1.W, right.Row3.Z, ScalarFloat64.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, ScalarFloat64.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, left.Row1.X * right.Row0.Z))),
                ScalarFloat64.FusedMultiplyAdd(left.Row1.W, right.Row3.W, ScalarFloat64.FusedMultiplyAdd(left.Row1.Z, right.Row2.W, ScalarFloat64.FusedMultiplyAdd(left.Row1.Y, right.Row1.W, left.Row1.X * right.Row0.W)))),
            new ScalarVector4Float64(
                ScalarFloat64.FusedMultiplyAdd(left.Row2.W, right.Row3.X, ScalarFloat64.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, ScalarFloat64.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, left.Row2.X * right.Row0.X))),
                ScalarFloat64.FusedMultiplyAdd(left.Row2.W, right.Row3.Y, ScalarFloat64.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, ScalarFloat64.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, left.Row2.X * right.Row0.Y))),
                ScalarFloat64.FusedMultiplyAdd(left.Row2.W, right.Row3.Z, ScalarFloat64.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, ScalarFloat64.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, left.Row2.X * right.Row0.Z))),
                ScalarFloat64.FusedMultiplyAdd(left.Row2.W, right.Row3.W, ScalarFloat64.FusedMultiplyAdd(left.Row2.Z, right.Row2.W, ScalarFloat64.FusedMultiplyAdd(left.Row2.Y, right.Row1.W, left.Row2.X * right.Row0.W)))),
            new ScalarVector4Float64(
                ScalarFloat64.FusedMultiplyAdd(left.Row3.W, right.Row3.X, ScalarFloat64.FusedMultiplyAdd(left.Row3.Z, right.Row2.X, ScalarFloat64.FusedMultiplyAdd(left.Row3.Y, right.Row1.X, left.Row3.X * right.Row0.X))),
                ScalarFloat64.FusedMultiplyAdd(left.Row3.W, right.Row3.Y, ScalarFloat64.FusedMultiplyAdd(left.Row3.Z, right.Row2.Y, ScalarFloat64.FusedMultiplyAdd(left.Row3.Y, right.Row1.Y, left.Row3.X * right.Row0.Y))),
                ScalarFloat64.FusedMultiplyAdd(left.Row3.W, right.Row3.Z, ScalarFloat64.FusedMultiplyAdd(left.Row3.Z, right.Row2.Z, ScalarFloat64.FusedMultiplyAdd(left.Row3.Y, right.Row1.Z, left.Row3.X * right.Row0.Z))),
                ScalarFloat64.FusedMultiplyAdd(left.Row3.W, right.Row3.W, ScalarFloat64.FusedMultiplyAdd(left.Row3.Z, right.Row2.W, ScalarFloat64.FusedMultiplyAdd(left.Row3.Y, right.Row1.W, left.Row3.X * right.Row0.W)))));
    }

    public static ScalarVector4Float64 operator *(ScalarMatrix4Float64 matrix, ScalarVector4Float64 vector) => Transform(matrix, vector);

    public static ScalarMatrix4Float64 operator *(ScalarMatrix4Float64 matrix, ScalarFloat64 scalar)
    {
        ScalarVector4Float64 scalarVector = new(scalar, scalar, scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector, matrix.Row2 * scalarVector, matrix.Row3 * scalarVector);
    }

    public static ScalarMatrix4Float64 operator *(ScalarFloat64 scalar, ScalarMatrix4Float64 matrix) => matrix * scalar;
    public static ScalarMatrix4Float64 Divide(ScalarMatrix4Float64 matrix, ScalarFloat64 scalar) => matrix / scalar;

    public static ScalarMatrix4Float64 operator /(ScalarMatrix4Float64 matrix, ScalarFloat64 scalar) => matrix * ScalarFloat64.Reciprocal(scalar);

    public static ScalarMatrix4Float64 InverseTranspose(ScalarMatrix4Float64 matrix)
    {
        static ScalarVector4Float64 Shuffle2301(ScalarVector4Float64 value) => new(value.Z, value.W, value.X, value.Y);

        static ScalarVector4Float64 Shuffle1032(ScalarVector4Float64 value) => new(value.Y, value.X, value.W, value.Z);

        static ScalarVector4Float64 Fmadd(ScalarVector4Float64 left, ScalarVector4Float64 right, ScalarVector4Float64 addend)
        {
            return new(
                ScalarFloat64.FusedMultiplyAdd(left.X, right.X, addend.X),
                ScalarFloat64.FusedMultiplyAdd(left.Y, right.Y, addend.Y),
                ScalarFloat64.FusedMultiplyAdd(left.Z, right.Z, addend.Z),
                ScalarFloat64.FusedMultiplyAdd(left.W, right.W, addend.W));
        }

        static ScalarVector4Float64 Fmsub(ScalarVector4Float64 left, ScalarVector4Float64 right, ScalarVector4Float64 subtrahend)
        {
            return new(
                ScalarFloat64.FusedMultiplyAdd(left.X, right.X, -subtrahend.X),
                ScalarFloat64.FusedMultiplyAdd(left.Y, right.Y, -subtrahend.Y),
                ScalarFloat64.FusedMultiplyAdd(left.Z, right.Z, -subtrahend.Z),
                ScalarFloat64.FusedMultiplyAdd(left.W, right.W, -subtrahend.W));
        }

        static ScalarVector4Float64 Fnmadd(ScalarVector4Float64 left, ScalarVector4Float64 right, ScalarVector4Float64 addend)
        {
            return new(
                ScalarFloat64.FusedMultiplyAdd(-left.X, right.X, addend.X),
                ScalarFloat64.FusedMultiplyAdd(-left.Y, right.Y, addend.Y),
                ScalarFloat64.FusedMultiplyAdd(-left.Z, right.Z, addend.Z),
                ScalarFloat64.FusedMultiplyAdd(-left.W, right.W, addend.W));
        }

        ScalarVector4Float64 row0 = matrix.Row0;
        ScalarVector4Float64 row1 = matrix.Row1;
        ScalarVector4Float64 row2 = matrix.Row2;
        ScalarVector4Float64 row3 = matrix.Row3;

        row1 = Shuffle2301(row1);
        row3 = Shuffle2301(row3);

        ScalarVector4Float64 temp = Shuffle1032(row2 * row3);
        ScalarVector4Float64 col0 = row1 * temp;
        ScalarVector4Float64 col1 = row0 * temp;
        temp = Shuffle2301(temp);
        col0 = Fmsub(row1, temp, col0);
        col1 = Shuffle2301(Fmsub(row0, temp, col1));

        temp = Shuffle1032(row1 * row2);
        col0 = Fmadd(row3, temp, col0);
        ScalarVector4Float64 col3 = row0 * temp;
        temp = Shuffle2301(temp);
        col0 = Fnmadd(row3, temp, col0);
        col3 = Shuffle2301(Fmsub(row0, temp, col3));

        temp = Shuffle1032(Shuffle2301(row1) * row3);
        row2 = Shuffle2301(row2);
        col0 = Fmadd(row2, temp, col0);
        ScalarVector4Float64 col2 = row0 * temp;
        temp = Shuffle2301(temp);
        col0 = Fnmadd(row2, temp, col0);
        col2 = Shuffle2301(Fmsub(row0, temp, col2));

        temp = Shuffle1032(row0 * row1);
        col2 = Fmadd(row3, temp, col2);
        col3 = Fmsub(row2, temp, col3);
        temp = Shuffle2301(temp);
        col2 = Fmsub(row3, temp, col2);
        col3 = Fnmadd(row2, temp, col3);

        temp = Shuffle1032(row0 * row3);
        col1 = Fnmadd(row2, temp, col1);
        col2 = Fmadd(row1, temp, col2);
        temp = Shuffle2301(temp);
        col1 = Fmadd(row2, temp, col1);
        col2 = Fnmadd(row1, temp, col2);

        temp = Shuffle1032(row0 * row2);
        col1 = Fmadd(row3, temp, col1);
        col3 = Fnmadd(row1, temp, col3);
        temp = Shuffle2301(temp);
        col1 = Fnmadd(row3, temp, col1);
        col3 = Fmadd(row1, temp, col3);

        ScalarFloat64 invDet = ScalarFloat64.Reciprocal(ScalarVector4Float64.Dot(row0, col0));
        ScalarVector4Float64 invDetVector = new(invDet, invDet, invDet, invDet);
        return new(col0 * invDetVector, col1 * invDetVector, col2 * invDetVector, col3 * invDetVector);
    }

    public static ScalarMatrix4Float64 Inverse(ScalarMatrix4Float64 matrix) => Transpose(InverseTranspose(matrix));

    public static ScalarMatrix4Float64 Rotate(ScalarVector4Float64 axis, ScalarFloat64 angle)
    {
        var (sin, cos) = ScalarFloat64.SinCos(angle);
        ScalarFloat64 zero = ScalarFloat64.Broadcast(0d);
        ScalarFloat64 one = ScalarFloat64.Broadcast(1d);
        ScalarFloat64 oneMinusCos = one - cos;

        ScalarFloat64 row0X = ScalarFloat64.FusedMultiplyAdd(axis.X * axis.X, oneMinusCos, cos);
        ScalarFloat64 row1Y = ScalarFloat64.FusedMultiplyAdd(axis.Y * axis.Y, oneMinusCos, cos);
        ScalarFloat64 row2Z = ScalarFloat64.FusedMultiplyAdd(axis.Z * axis.Z, oneMinusCos, cos);
        ScalarFloat64 row0Y = ScalarFloat64.FusedMultiplyAdd(axis.Y * axis.X, oneMinusCos, -(axis.Z * sin));
        ScalarFloat64 row0Z = ScalarFloat64.FusedMultiplyAdd(axis.Z * axis.X, oneMinusCos, axis.Y * sin);
        ScalarFloat64 row1X = ScalarFloat64.FusedMultiplyAdd(axis.X * axis.Y, oneMinusCos, axis.Z * sin);
        ScalarFloat64 row1Z = ScalarFloat64.FusedMultiplyAdd(axis.Z * axis.Y, oneMinusCos, -(axis.X * sin));
        ScalarFloat64 row2X = ScalarFloat64.FusedMultiplyAdd(axis.X * axis.Z, oneMinusCos, -(axis.Y * sin));
        ScalarFloat64 row2Y = ScalarFloat64.FusedMultiplyAdd(axis.Y * axis.Z, oneMinusCos, axis.X * sin);

        return new(
            new(row0X, row0Y, row0Z, zero),
            new(row1X, row1Y, row1Z, zero),
            new(row2X, row2Y, row2Z, zero),
            new(zero, zero, zero, one));
    }

    public static ScalarMatrix4Float64Mask operator ==(ScalarMatrix4Float64 left, ScalarMatrix4Float64 right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2, left.Row3 == right.Row3);

    public static ScalarMatrix4Float64Mask operator !=(ScalarMatrix4Float64 left, ScalarMatrix4Float64 right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2, left.Row3 != right.Row3);

    public override bool Equals(object? obj)
    {
        return obj is ScalarMatrix4Float64 other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2) &&
               Row3.Equals(other.Row3);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2, Row3);
}

public readonly struct ScalarMatrix4Float64Mask :
    ISimdMatrix4Mask<ScalarMatrix4Float64Mask, ScalarVector4Float64Mask, ScalarFloat64Mask>
{
    public ScalarMatrix4Float64Mask(ScalarVector4Float64Mask row0, ScalarVector4Float64Mask row1, ScalarVector4Float64Mask row2, ScalarVector4Float64Mask row3)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
        Row3 = row3;
    }

    public static int LaneCount => ScalarVector4Float64Mask.LaneCount;

    public ScalarVector4Float64Mask Row0 { get; }

    public ScalarVector4Float64Mask Row1 { get; }

    public ScalarVector4Float64Mask Row2 { get; }

    public ScalarVector4Float64Mask Row3 { get; }

    public static ScalarMatrix4Float64Mask True => new(ScalarVector4Float64Mask.True, ScalarVector4Float64Mask.True, ScalarVector4Float64Mask.True, ScalarVector4Float64Mask.True);

    public static ScalarMatrix4Float64Mask False => new(ScalarVector4Float64Mask.False, ScalarVector4Float64Mask.False, ScalarVector4Float64Mask.False, ScalarVector4Float64Mask.False);

    public static ScalarMatrix4Float64Mask Create(ScalarVector4Float64Mask row0, ScalarVector4Float64Mask row1, ScalarVector4Float64Mask row2, ScalarVector4Float64Mask row3) => new(row0, row1, row2, row3);

    public static ScalarMatrix4Float64Mask Broadcast(ScalarVector4Float64Mask value) => new(value, value, value, value);

    public static ScalarVector4Float64Mask All(ScalarMatrix4Float64Mask value) => value.Row0 & value.Row1 & value.Row2 & value.Row3;

    public static ScalarVector4Float64Mask Any(ScalarMatrix4Float64Mask value) => value.Row0 | value.Row1 | value.Row2 | value.Row3;

    public static ScalarVector4Float64Mask None(ScalarMatrix4Float64Mask value) => ~(value.Row0 | value.Row1 | value.Row2 | value.Row3);

    public static ScalarMatrix4Float64Mask AndNot(ScalarMatrix4Float64Mask left, ScalarMatrix4Float64Mask right)
    {
        return new(
            ScalarVector4Float64Mask.AndNot(left.Row0, right.Row0),
            ScalarVector4Float64Mask.AndNot(left.Row1, right.Row1),
            ScalarVector4Float64Mask.AndNot(left.Row2, right.Row2),
            ScalarVector4Float64Mask.AndNot(left.Row3, right.Row3));
    }

    public static ScalarMatrix4Float64Mask Select(ScalarMatrix4Float64Mask mask, ScalarMatrix4Float64Mask ifTrue, ScalarMatrix4Float64Mask ifFalse)
    {
        return new(
            ScalarVector4Float64Mask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            ScalarVector4Float64Mask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            ScalarVector4Float64Mask.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2),
            ScalarVector4Float64Mask.Select(mask.Row3, ifTrue.Row3, ifFalse.Row3));
    }
    public static ScalarMatrix4Float64Mask And(ScalarMatrix4Float64Mask left, ScalarMatrix4Float64Mask right) => left & right;

    public static ScalarMatrix4Float64Mask Or(ScalarMatrix4Float64Mask left, ScalarMatrix4Float64Mask right) => left | right;

    public static ScalarMatrix4Float64Mask Xor(ScalarMatrix4Float64Mask left, ScalarMatrix4Float64Mask right) => left ^ right;

    public static ScalarMatrix4Float64Mask Not(ScalarMatrix4Float64Mask value) => ~value;


    public static ScalarMatrix4Float64Mask operator &(ScalarMatrix4Float64Mask left, ScalarMatrix4Float64Mask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1, left.Row2 & right.Row2, left.Row3 & right.Row3);

    public static ScalarMatrix4Float64Mask operator |(ScalarMatrix4Float64Mask left, ScalarMatrix4Float64Mask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1, left.Row2 | right.Row2, left.Row3 | right.Row3);

    public static ScalarMatrix4Float64Mask operator ^(ScalarMatrix4Float64Mask left, ScalarMatrix4Float64Mask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1, left.Row2 ^ right.Row2, left.Row3 ^ right.Row3);

    public static ScalarMatrix4Float64Mask operator ~(ScalarMatrix4Float64Mask value) => new(~value.Row0, ~value.Row1, ~value.Row2, ~value.Row3);

    public static ScalarMatrix4Float64Mask operator ==(ScalarMatrix4Float64Mask left, ScalarMatrix4Float64Mask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2, left.Row3 == right.Row3);

    public static ScalarMatrix4Float64Mask operator !=(ScalarMatrix4Float64Mask left, ScalarMatrix4Float64Mask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2, left.Row3 != right.Row3);

    public override bool Equals(object? obj)
    {
        return obj is ScalarMatrix4Float64Mask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2) &&
               Row3.Equals(other.Row3);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2, Row3);
}
