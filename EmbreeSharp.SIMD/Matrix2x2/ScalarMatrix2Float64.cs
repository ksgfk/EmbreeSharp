namespace EmbreeSharp.SIMD;

public readonly struct ScalarMatrix2Float64 :
    ISimdFloatingPointMatrix2<ScalarMatrix2Float64, ScalarVector2Float64, ScalarFloat64, double, ScalarMatrix2Float64Mask, ScalarVector2Float64Mask, ScalarFloat64Mask>
{
    public ScalarMatrix2Float64(ScalarVector2Float64 row0, ScalarVector2Float64 row1)
    {
        Row0 = row0;
        Row1 = row1;
    }

    public static int LaneCount => ScalarVector2Float64.LaneCount;

    public ScalarVector2Float64 Row0 { get; }

    public ScalarVector2Float64 Row1 { get; }

    public static ScalarMatrix2Float64 Identity
    {
        get
        {
            ScalarFloat64 zero = ScalarFloat64.Broadcast(0.0);
            ScalarFloat64 one = ScalarFloat64.Broadcast(1.0);
            return new(
                new ScalarVector2Float64(one, zero),
                new ScalarVector2Float64(zero, one));
        }
    }

    public static ScalarMatrix2Float64 Create(ScalarVector2Float64 row0, ScalarVector2Float64 row1) => new(row0, row1);

    public static ScalarMatrix2Float64 Broadcast(double value)
    {
        ScalarVector2Float64 row = ScalarVector2Float64.Broadcast(value);
        return new(row, row);
    }

    public static ScalarMatrix2Float64 Select(ScalarMatrix2Float64Mask mask, ScalarMatrix2Float64 ifTrue, ScalarMatrix2Float64 ifFalse)
    {
        return new(
            ScalarVector2Float64.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            ScalarVector2Float64.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1));
    }

    public static ScalarMatrix2Float64 Select(ScalarVector2Float64Mask mask, ScalarMatrix2Float64 ifTrue, ScalarMatrix2Float64 ifFalse)
    {
        return new(
            ScalarVector2Float64.Select(mask, ifTrue.Row0, ifFalse.Row0),
            ScalarVector2Float64.Select(mask, ifTrue.Row1, ifFalse.Row1));
    }

    public static ScalarMatrix2Float64 Select(ScalarFloat64Mask mask, ScalarMatrix2Float64 ifTrue, ScalarMatrix2Float64 ifFalse)
    {
        return new(
            ScalarVector2Float64.Select(mask, ifTrue.Row0, ifFalse.Row0),
            ScalarVector2Float64.Select(mask, ifTrue.Row1, ifFalse.Row1));
    }

    public static ScalarVector2Float64 Transform(ScalarMatrix2Float64 matrix, ScalarVector2Float64 vector)
    {
        return new(
            ScalarFloat64.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, matrix.Row0.X * vector.X),
            ScalarFloat64.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, matrix.Row1.X * vector.X));
    }

    public static ScalarMatrix2Float64 Multiply(ScalarMatrix2Float64 left, ScalarMatrix2Float64 right) => left * right;

    public static ScalarMatrix2Float64 Multiply(ScalarMatrix2Float64 matrix, ScalarFloat64 scalar) => matrix * scalar;

    public static ScalarMatrix2Float64 Multiply(ScalarFloat64 scalar, ScalarMatrix2Float64 matrix) => scalar * matrix;

    public static ScalarMatrix2Float64 FusedMultiplyAdd(ScalarMatrix2Float64 left, ScalarMatrix2Float64 right, ScalarMatrix2Float64 addend)
    {
        return new(
            new ScalarVector2Float64(
                ScalarFloat64.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, ScalarFloat64.FusedMultiplyAdd(left.Row0.X, right.Row0.X, addend.Row0.X)),
                ScalarFloat64.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, ScalarFloat64.FusedMultiplyAdd(left.Row0.X, right.Row0.Y, addend.Row0.Y))),
            new ScalarVector2Float64(
                ScalarFloat64.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, ScalarFloat64.FusedMultiplyAdd(left.Row1.X, right.Row0.X, addend.Row1.X)),
                ScalarFloat64.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, ScalarFloat64.FusedMultiplyAdd(left.Row1.X, right.Row0.Y, addend.Row1.Y))));
    }

    public static ScalarVector2Float64 FusedMultiplyAdd(ScalarMatrix2Float64 matrix, ScalarVector2Float64 vector, ScalarVector2Float64 addend)
    {
        return new(
            ScalarFloat64.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, ScalarFloat64.FusedMultiplyAdd(matrix.Row0.X, vector.X, addend.X)),
            ScalarFloat64.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, ScalarFloat64.FusedMultiplyAdd(matrix.Row1.X, vector.X, addend.Y)));
    }

    public static ScalarMatrix2Float64 Transpose(ScalarMatrix2Float64 matrix)
    {
        return new(
            new(matrix.Row0.X, matrix.Row1.X),
            new(matrix.Row0.Y, matrix.Row1.Y));
    }

    public static ScalarMatrix2Float64 Scale(ScalarVector2Float64 scale) => Scale(scale.X, scale.Y);

    public static ScalarMatrix2Float64 Scale(ScalarFloat64 x, ScalarFloat64 y)
    {
        ScalarFloat64 zero = ScalarFloat64.Broadcast(0.0);
        return new(
            new(x, zero),
            new(zero, y));
    }

    public static ScalarMatrix2Float64 operator *(ScalarMatrix2Float64 left, ScalarMatrix2Float64 right)
    {
        return new(
            new ScalarVector2Float64(
                ScalarFloat64.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, left.Row0.X * right.Row0.X),
                ScalarFloat64.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, left.Row0.X * right.Row0.Y)),
            new ScalarVector2Float64(
                ScalarFloat64.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, left.Row1.X * right.Row0.X),
                ScalarFloat64.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, left.Row1.X * right.Row0.Y)));
    }

    public static ScalarVector2Float64 operator *(ScalarMatrix2Float64 matrix, ScalarVector2Float64 vector) => Transform(matrix, vector);

    public static ScalarMatrix2Float64 operator *(ScalarMatrix2Float64 matrix, ScalarFloat64 scalar)
    {
        ScalarVector2Float64 scalarVector = new(scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector);
    }

    public static ScalarMatrix2Float64 operator *(ScalarFloat64 scalar, ScalarMatrix2Float64 matrix) => matrix * scalar;
    public static ScalarMatrix2Float64 Divide(ScalarMatrix2Float64 matrix, ScalarFloat64 scalar) => matrix / scalar;

    public static ScalarMatrix2Float64 operator /(ScalarMatrix2Float64 matrix, ScalarFloat64 scalar) => matrix * ScalarFloat64.Reciprocal(scalar);

    public static ScalarMatrix2Float64 Inverse(ScalarMatrix2Float64 matrix)
    {
        ScalarFloat64 invDet = ScalarFloat64.Reciprocal(ScalarFloat64.FusedMultiplyAdd(matrix.Row0.X, matrix.Row1.Y, -(matrix.Row0.Y * matrix.Row1.X)));
        return new(
            new(matrix.Row1.Y * invDet, -matrix.Row0.Y * invDet),
            new(-matrix.Row1.X * invDet, matrix.Row0.X * invDet));
    }

    public static ScalarMatrix2Float64 InverseTranspose(ScalarMatrix2Float64 matrix)
    {
        ScalarFloat64 invDet = ScalarFloat64.Reciprocal(ScalarFloat64.FusedMultiplyAdd(matrix.Row0.X, matrix.Row1.Y, -(matrix.Row0.Y * matrix.Row1.X)));
        return new(
            new(matrix.Row1.Y * invDet, -matrix.Row1.X * invDet),
            new(-matrix.Row0.Y * invDet, matrix.Row0.X * invDet));
    }

    public static ScalarMatrix2Float64 Rotate(ScalarFloat64 angle)
    {
        var (sin, cos) = ScalarFloat64.SinCos(angle);
        return new(
            new(cos, -sin),
            new(sin, cos));
    }

    public static ScalarMatrix2Float64Mask operator ==(ScalarMatrix2Float64 left, ScalarMatrix2Float64 right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1);

    public static ScalarMatrix2Float64Mask operator !=(ScalarMatrix2Float64 left, ScalarMatrix2Float64 right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1);

    public static ScalarMatrix2Float64Mask operator <(ScalarMatrix2Float64 left, ScalarMatrix2Float64 right) => new(
        new ScalarVector2Float64Mask(left.Row0.X < right.Row0.X, left.Row0.Y < right.Row0.Y),
        new ScalarVector2Float64Mask(left.Row1.X < right.Row1.X, left.Row1.Y < right.Row1.Y));

    public static ScalarMatrix2Float64Mask operator >(ScalarMatrix2Float64 left, ScalarMatrix2Float64 right) => new(
        new ScalarVector2Float64Mask(left.Row0.X > right.Row0.X, left.Row0.Y > right.Row0.Y),
        new ScalarVector2Float64Mask(left.Row1.X > right.Row1.X, left.Row1.Y > right.Row1.Y));

    public static ScalarMatrix2Float64Mask operator <=(ScalarMatrix2Float64 left, ScalarMatrix2Float64 right) => new(
        new ScalarVector2Float64Mask(left.Row0.X <= right.Row0.X, left.Row0.Y <= right.Row0.Y),
        new ScalarVector2Float64Mask(left.Row1.X <= right.Row1.X, left.Row1.Y <= right.Row1.Y));

    public static ScalarMatrix2Float64Mask operator >=(ScalarMatrix2Float64 left, ScalarMatrix2Float64 right) => new(
        new ScalarVector2Float64Mask(left.Row0.X >= right.Row0.X, left.Row0.Y >= right.Row0.Y),
        new ScalarVector2Float64Mask(left.Row1.X >= right.Row1.X, left.Row1.Y >= right.Row1.Y));

    public override bool Equals(object? obj)
    {
        return obj is ScalarMatrix2Float64 other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1);
}

public readonly struct ScalarMatrix2Float64Mask :
    ISimdMatrix2Mask<ScalarMatrix2Float64Mask, ScalarVector2Float64Mask, ScalarFloat64Mask>
{
    public ScalarMatrix2Float64Mask(ScalarVector2Float64Mask row0, ScalarVector2Float64Mask row1)
    {
        Row0 = row0;
        Row1 = row1;
    }

    public static int LaneCount => ScalarVector2Float64Mask.LaneCount;

    public ScalarVector2Float64Mask Row0 { get; }

    public ScalarVector2Float64Mask Row1 { get; }

    public static ScalarMatrix2Float64Mask True => new(ScalarVector2Float64Mask.True, ScalarVector2Float64Mask.True);

    public static ScalarMatrix2Float64Mask False => new(ScalarVector2Float64Mask.False, ScalarVector2Float64Mask.False);

    public static ScalarMatrix2Float64Mask Create(ScalarVector2Float64Mask row0, ScalarVector2Float64Mask row1) => new(row0, row1);

    public static ScalarMatrix2Float64Mask Broadcast(ScalarVector2Float64Mask value) => new(value, value);

    public static ScalarVector2Float64Mask All(ScalarMatrix2Float64Mask value) => value.Row0 & value.Row1;

    public static ScalarVector2Float64Mask Any(ScalarMatrix2Float64Mask value) => value.Row0 | value.Row1;

    public static ScalarVector2Float64Mask None(ScalarMatrix2Float64Mask value) => ~(value.Row0 | value.Row1);

    public static ScalarMatrix2Float64Mask AndNot(ScalarMatrix2Float64Mask left, ScalarMatrix2Float64Mask right)
    {
        return new(
            ScalarVector2Float64Mask.AndNot(left.Row0, right.Row0),
            ScalarVector2Float64Mask.AndNot(left.Row1, right.Row1));
    }

    public static ScalarMatrix2Float64Mask Select(ScalarMatrix2Float64Mask mask, ScalarMatrix2Float64Mask ifTrue, ScalarMatrix2Float64Mask ifFalse)
    {
        return new(
            ScalarVector2Float64Mask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            ScalarVector2Float64Mask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1));
    }
    public static ScalarMatrix2Float64Mask And(ScalarMatrix2Float64Mask left, ScalarMatrix2Float64Mask right) => left & right;

    public static ScalarMatrix2Float64Mask Or(ScalarMatrix2Float64Mask left, ScalarMatrix2Float64Mask right) => left | right;

    public static ScalarMatrix2Float64Mask Xor(ScalarMatrix2Float64Mask left, ScalarMatrix2Float64Mask right) => left ^ right;

    public static ScalarMatrix2Float64Mask Not(ScalarMatrix2Float64Mask value) => ~value;


    public static ScalarMatrix2Float64Mask operator &(ScalarMatrix2Float64Mask left, ScalarMatrix2Float64Mask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1);

    public static ScalarMatrix2Float64Mask operator |(ScalarMatrix2Float64Mask left, ScalarMatrix2Float64Mask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1);

    public static ScalarMatrix2Float64Mask operator ^(ScalarMatrix2Float64Mask left, ScalarMatrix2Float64Mask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1);

    public static ScalarMatrix2Float64Mask operator ~(ScalarMatrix2Float64Mask value) => new(~value.Row0, ~value.Row1);

    public static ScalarMatrix2Float64Mask operator ==(ScalarMatrix2Float64Mask left, ScalarMatrix2Float64Mask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1);

    public static ScalarMatrix2Float64Mask operator !=(ScalarMatrix2Float64Mask left, ScalarMatrix2Float64Mask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1);

    public override bool Equals(object? obj)
    {
        return obj is ScalarMatrix2Float64Mask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1);
}
