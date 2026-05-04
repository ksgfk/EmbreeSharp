namespace EmbreeSharp.SIMD;

public readonly struct ScalarMatrix2Float32 :
    ISimdFloatingPointMatrix2<ScalarMatrix2Float32, ScalarVector2Float32, ScalarFloat32, float, ScalarMatrix2Float32Mask, ScalarVector2Float32Mask, ScalarFloat32Mask>
{
    public ScalarMatrix2Float32(ScalarVector2Float32 row0, ScalarVector2Float32 row1)
    {
        Row0 = row0;
        Row1 = row1;
    }

    public static int LaneCount => ScalarVector2Float32.LaneCount;

    public ScalarVector2Float32 Row0 { get; }

    public ScalarVector2Float32 Row1 { get; }

    public static ScalarMatrix2Float32 Identity
    {
        get
        {
            ScalarFloat32 zero = ScalarFloat32.Broadcast(0f);
            ScalarFloat32 one = ScalarFloat32.Broadcast(1f);
            return new(
                new ScalarVector2Float32(one, zero),
                new ScalarVector2Float32(zero, one));
        }
    }

    public static ScalarMatrix2Float32 Create(ScalarVector2Float32 row0, ScalarVector2Float32 row1) => new(row0, row1);

    public static ScalarMatrix2Float32 Broadcast(float value)
    {
        ScalarVector2Float32 row = ScalarVector2Float32.Broadcast(value);
        return new(row, row);
    }

    public static ScalarMatrix2Float32 Select(ScalarMatrix2Float32Mask mask, ScalarMatrix2Float32 ifTrue, ScalarMatrix2Float32 ifFalse)
    {
        return new(
            ScalarVector2Float32.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            ScalarVector2Float32.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1));
    }

    public static ScalarMatrix2Float32 Select(ScalarVector2Float32Mask mask, ScalarMatrix2Float32 ifTrue, ScalarMatrix2Float32 ifFalse)
    {
        return new(
            ScalarVector2Float32.Select(mask, ifTrue.Row0, ifFalse.Row0),
            ScalarVector2Float32.Select(mask, ifTrue.Row1, ifFalse.Row1));
    }

    public static ScalarMatrix2Float32 Select(ScalarFloat32Mask mask, ScalarMatrix2Float32 ifTrue, ScalarMatrix2Float32 ifFalse)
    {
        return new(
            ScalarVector2Float32.Select(mask, ifTrue.Row0, ifFalse.Row0),
            ScalarVector2Float32.Select(mask, ifTrue.Row1, ifFalse.Row1));
    }

    public static ScalarVector2Float32 Transform(ScalarMatrix2Float32 matrix, ScalarVector2Float32 vector)
    {
        return new(
            ScalarFloat32.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, matrix.Row0.X * vector.X),
            ScalarFloat32.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, matrix.Row1.X * vector.X));
    }

    public static ScalarMatrix2Float32 Multiply(ScalarMatrix2Float32 left, ScalarMatrix2Float32 right) => left * right;

    public static ScalarMatrix2Float32 Multiply(ScalarMatrix2Float32 matrix, ScalarFloat32 scalar) => matrix * scalar;

    public static ScalarMatrix2Float32 Multiply(ScalarFloat32 scalar, ScalarMatrix2Float32 matrix) => scalar * matrix;

    public static ScalarMatrix2Float32 FusedMultiplyAdd(ScalarMatrix2Float32 left, ScalarMatrix2Float32 right, ScalarMatrix2Float32 addend)
    {
        return new(
            new ScalarVector2Float32(
                ScalarFloat32.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, ScalarFloat32.FusedMultiplyAdd(left.Row0.X, right.Row0.X, addend.Row0.X)),
                ScalarFloat32.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, ScalarFloat32.FusedMultiplyAdd(left.Row0.X, right.Row0.Y, addend.Row0.Y))),
            new ScalarVector2Float32(
                ScalarFloat32.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, ScalarFloat32.FusedMultiplyAdd(left.Row1.X, right.Row0.X, addend.Row1.X)),
                ScalarFloat32.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, ScalarFloat32.FusedMultiplyAdd(left.Row1.X, right.Row0.Y, addend.Row1.Y))));
    }

    public static ScalarVector2Float32 FusedMultiplyAdd(ScalarMatrix2Float32 matrix, ScalarVector2Float32 vector, ScalarVector2Float32 addend)
    {
        return new(
            ScalarFloat32.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, ScalarFloat32.FusedMultiplyAdd(matrix.Row0.X, vector.X, addend.X)),
            ScalarFloat32.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, ScalarFloat32.FusedMultiplyAdd(matrix.Row1.X, vector.X, addend.Y)));
    }

    public static ScalarMatrix2Float32 Transpose(ScalarMatrix2Float32 matrix)
    {
        return new(
            new(matrix.Row0.X, matrix.Row1.X),
            new(matrix.Row0.Y, matrix.Row1.Y));
    }

    public static ScalarMatrix2Float32 Scale(ScalarVector2Float32 scale) => Scale(scale.X, scale.Y);

    public static ScalarMatrix2Float32 Scale(ScalarFloat32 x, ScalarFloat32 y)
    {
        ScalarFloat32 zero = ScalarFloat32.Broadcast(0f);
        return new(
            new(x, zero),
            new(zero, y));
    }

    public static ScalarMatrix2Float32 operator *(ScalarMatrix2Float32 left, ScalarMatrix2Float32 right)
    {
        return new(
            new ScalarVector2Float32(
                ScalarFloat32.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, left.Row0.X * right.Row0.X),
                ScalarFloat32.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, left.Row0.X * right.Row0.Y)),
            new ScalarVector2Float32(
                ScalarFloat32.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, left.Row1.X * right.Row0.X),
                ScalarFloat32.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, left.Row1.X * right.Row0.Y)));
    }

    public static ScalarVector2Float32 operator *(ScalarMatrix2Float32 matrix, ScalarVector2Float32 vector) => Transform(matrix, vector);

    public static ScalarMatrix2Float32 operator *(ScalarMatrix2Float32 matrix, ScalarFloat32 scalar)
    {
        ScalarVector2Float32 scalarVector = new(scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector);
    }

    public static ScalarMatrix2Float32 operator *(ScalarFloat32 scalar, ScalarMatrix2Float32 matrix) => matrix * scalar;
    public static ScalarMatrix2Float32 Divide(ScalarMatrix2Float32 matrix, ScalarFloat32 scalar) => matrix / scalar;

    public static ScalarMatrix2Float32 operator /(ScalarMatrix2Float32 matrix, ScalarFloat32 scalar) => matrix * ScalarFloat32.Reciprocal(scalar);

    public static ScalarMatrix2Float32 Inverse(ScalarMatrix2Float32 matrix)
    {
        ScalarFloat32 invDet = ScalarFloat32.Reciprocal(ScalarFloat32.FusedMultiplyAdd(matrix.Row0.X, matrix.Row1.Y, -(matrix.Row0.Y * matrix.Row1.X)));
        return new(
            new(matrix.Row1.Y * invDet, -matrix.Row0.Y * invDet),
            new(-matrix.Row1.X * invDet, matrix.Row0.X * invDet));
    }

    public static ScalarMatrix2Float32 InverseTranspose(ScalarMatrix2Float32 matrix)
    {
        ScalarFloat32 invDet = ScalarFloat32.Reciprocal(ScalarFloat32.FusedMultiplyAdd(matrix.Row0.X, matrix.Row1.Y, -(matrix.Row0.Y * matrix.Row1.X)));
        return new(
            new(matrix.Row1.Y * invDet, -matrix.Row1.X * invDet),
            new(-matrix.Row0.Y * invDet, matrix.Row0.X * invDet));
    }

    public static ScalarMatrix2Float32 Rotate(ScalarFloat32 angle)
    {
        var (sin, cos) = ScalarFloat32.SinCos(angle);
        return new(
            new(cos, -sin),
            new(sin, cos));
    }

    public static ScalarMatrix2Float32Mask operator ==(ScalarMatrix2Float32 left, ScalarMatrix2Float32 right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1);

    public static ScalarMatrix2Float32Mask operator !=(ScalarMatrix2Float32 left, ScalarMatrix2Float32 right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1);

    public static ScalarMatrix2Float32Mask operator <(ScalarMatrix2Float32 left, ScalarMatrix2Float32 right) => new(
        new ScalarVector2Float32Mask(left.Row0.X < right.Row0.X, left.Row0.Y < right.Row0.Y),
        new ScalarVector2Float32Mask(left.Row1.X < right.Row1.X, left.Row1.Y < right.Row1.Y));

    public static ScalarMatrix2Float32Mask operator >(ScalarMatrix2Float32 left, ScalarMatrix2Float32 right) => new(
        new ScalarVector2Float32Mask(left.Row0.X > right.Row0.X, left.Row0.Y > right.Row0.Y),
        new ScalarVector2Float32Mask(left.Row1.X > right.Row1.X, left.Row1.Y > right.Row1.Y));

    public static ScalarMatrix2Float32Mask operator <=(ScalarMatrix2Float32 left, ScalarMatrix2Float32 right) => new(
        new ScalarVector2Float32Mask(left.Row0.X <= right.Row0.X, left.Row0.Y <= right.Row0.Y),
        new ScalarVector2Float32Mask(left.Row1.X <= right.Row1.X, left.Row1.Y <= right.Row1.Y));

    public static ScalarMatrix2Float32Mask operator >=(ScalarMatrix2Float32 left, ScalarMatrix2Float32 right) => new(
        new ScalarVector2Float32Mask(left.Row0.X >= right.Row0.X, left.Row0.Y >= right.Row0.Y),
        new ScalarVector2Float32Mask(left.Row1.X >= right.Row1.X, left.Row1.Y >= right.Row1.Y));

    public override bool Equals(object? obj)
    {
        return obj is ScalarMatrix2Float32 other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1);
}

public readonly struct ScalarMatrix2Float32Mask :
    ISimdMatrix2Mask<ScalarMatrix2Float32Mask, ScalarVector2Float32Mask, ScalarFloat32Mask>
{
    public ScalarMatrix2Float32Mask(ScalarVector2Float32Mask row0, ScalarVector2Float32Mask row1)
    {
        Row0 = row0;
        Row1 = row1;
    }

    public static int LaneCount => ScalarVector2Float32Mask.LaneCount;

    public ScalarVector2Float32Mask Row0 { get; }

    public ScalarVector2Float32Mask Row1 { get; }

    public static ScalarMatrix2Float32Mask True => new(ScalarVector2Float32Mask.True, ScalarVector2Float32Mask.True);

    public static ScalarMatrix2Float32Mask False => new(ScalarVector2Float32Mask.False, ScalarVector2Float32Mask.False);

    public static ScalarMatrix2Float32Mask Create(ScalarVector2Float32Mask row0, ScalarVector2Float32Mask row1) => new(row0, row1);

    public static ScalarMatrix2Float32Mask Broadcast(ScalarVector2Float32Mask value) => new(value, value);

    public static ScalarVector2Float32Mask All(ScalarMatrix2Float32Mask value) => value.Row0 & value.Row1;

    public static ScalarVector2Float32Mask Any(ScalarMatrix2Float32Mask value) => value.Row0 | value.Row1;

    public static ScalarVector2Float32Mask None(ScalarMatrix2Float32Mask value) => ~(value.Row0 | value.Row1);

    public static ScalarMatrix2Float32Mask AndNot(ScalarMatrix2Float32Mask left, ScalarMatrix2Float32Mask right)
    {
        return new(
            ScalarVector2Float32Mask.AndNot(left.Row0, right.Row0),
            ScalarVector2Float32Mask.AndNot(left.Row1, right.Row1));
    }

    public static ScalarMatrix2Float32Mask Select(ScalarMatrix2Float32Mask mask, ScalarMatrix2Float32Mask ifTrue, ScalarMatrix2Float32Mask ifFalse)
    {
        return new(
            ScalarVector2Float32Mask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            ScalarVector2Float32Mask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1));
    }
    public static ScalarMatrix2Float32Mask And(ScalarMatrix2Float32Mask left, ScalarMatrix2Float32Mask right) => left & right;

    public static ScalarMatrix2Float32Mask Or(ScalarMatrix2Float32Mask left, ScalarMatrix2Float32Mask right) => left | right;

    public static ScalarMatrix2Float32Mask Xor(ScalarMatrix2Float32Mask left, ScalarMatrix2Float32Mask right) => left ^ right;

    public static ScalarMatrix2Float32Mask Not(ScalarMatrix2Float32Mask value) => ~value;


    public static ScalarMatrix2Float32Mask operator &(ScalarMatrix2Float32Mask left, ScalarMatrix2Float32Mask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1);

    public static ScalarMatrix2Float32Mask operator |(ScalarMatrix2Float32Mask left, ScalarMatrix2Float32Mask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1);

    public static ScalarMatrix2Float32Mask operator ^(ScalarMatrix2Float32Mask left, ScalarMatrix2Float32Mask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1);

    public static ScalarMatrix2Float32Mask operator ~(ScalarMatrix2Float32Mask value) => new(~value.Row0, ~value.Row1);

    public static ScalarMatrix2Float32Mask operator ==(ScalarMatrix2Float32Mask left, ScalarMatrix2Float32Mask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1);

    public static ScalarMatrix2Float32Mask operator !=(ScalarMatrix2Float32Mask left, ScalarMatrix2Float32Mask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1);

    public override bool Equals(object? obj)
    {
        return obj is ScalarMatrix2Float32Mask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1);
}
