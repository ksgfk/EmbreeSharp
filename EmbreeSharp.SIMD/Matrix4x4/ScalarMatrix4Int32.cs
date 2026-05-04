namespace EmbreeSharp.SIMD;

public readonly struct ScalarMatrix4Int32 :
    ISimdIntegerMatrix4<ScalarMatrix4Int32, ScalarVector4Int32, ScalarInt32, int, ScalarMatrix4Int32Mask, ScalarVector4Int32Mask, ScalarInt32Mask>
{
    public ScalarMatrix4Int32(ScalarVector4Int32 row0, ScalarVector4Int32 row1, ScalarVector4Int32 row2, ScalarVector4Int32 row3)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
        Row3 = row3;
    }

    public static int LaneCount => ScalarVector4Int32.LaneCount;

    public ScalarVector4Int32 Row0 { get; }

    public ScalarVector4Int32 Row1 { get; }

    public ScalarVector4Int32 Row2 { get; }

    public ScalarVector4Int32 Row3 { get; }

    public static ScalarMatrix4Int32 Identity
    {
        get
        {
            ScalarInt32 zero = ScalarInt32.Broadcast(0);
            ScalarInt32 one = ScalarInt32.Broadcast(1);
            return new(
                new ScalarVector4Int32(one, zero, zero, zero),
                new ScalarVector4Int32(zero, one, zero, zero),
                new ScalarVector4Int32(zero, zero, one, zero),
                new ScalarVector4Int32(zero, zero, zero, one));
        }
    }

    public static ScalarMatrix4Int32 Create(ScalarVector4Int32 row0, ScalarVector4Int32 row1, ScalarVector4Int32 row2, ScalarVector4Int32 row3) => new(row0, row1, row2, row3);

    public static ScalarMatrix4Int32 Broadcast(int value)
    {
        ScalarVector4Int32 row = ScalarVector4Int32.Broadcast(value);
        return new(row, row, row, row);
    }

    public static ScalarMatrix4Int32 Select(ScalarMatrix4Int32Mask mask, ScalarMatrix4Int32 ifTrue, ScalarMatrix4Int32 ifFalse)
    {
        return new(
            ScalarVector4Int32.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            ScalarVector4Int32.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            ScalarVector4Int32.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2),
            ScalarVector4Int32.Select(mask.Row3, ifTrue.Row3, ifFalse.Row3));
    }

    public static ScalarMatrix4Int32 Select(ScalarVector4Int32Mask mask, ScalarMatrix4Int32 ifTrue, ScalarMatrix4Int32 ifFalse)
    {
        return new(
            ScalarVector4Int32.Select(mask, ifTrue.Row0, ifFalse.Row0),
            ScalarVector4Int32.Select(mask, ifTrue.Row1, ifFalse.Row1),
            ScalarVector4Int32.Select(mask, ifTrue.Row2, ifFalse.Row2),
            ScalarVector4Int32.Select(mask, ifTrue.Row3, ifFalse.Row3));
    }

    public static ScalarMatrix4Int32 Select(ScalarInt32Mask mask, ScalarMatrix4Int32 ifTrue, ScalarMatrix4Int32 ifFalse)
    {
        return new(
            ScalarVector4Int32.Select(mask, ifTrue.Row0, ifFalse.Row0),
            ScalarVector4Int32.Select(mask, ifTrue.Row1, ifFalse.Row1),
            ScalarVector4Int32.Select(mask, ifTrue.Row2, ifFalse.Row2),
            ScalarVector4Int32.Select(mask, ifTrue.Row3, ifFalse.Row3));
    }

    public static ScalarMatrix4Int32 Multiply(ScalarMatrix4Int32 left, ScalarMatrix4Int32 right) => left * right;

    public static ScalarMatrix4Int32 Multiply(ScalarMatrix4Int32 matrix, ScalarInt32 scalar) => matrix * scalar;

    public static ScalarMatrix4Int32 Multiply(ScalarInt32 scalar, ScalarMatrix4Int32 matrix) => scalar * matrix;

    public static ScalarMatrix4Int32 operator *(ScalarMatrix4Int32 left, ScalarMatrix4Int32 right)
    {
        return new(
            new ScalarVector4Int32(
                left.Row0.X * right.Row0.X + left.Row0.Y * right.Row1.X + left.Row0.Z * right.Row2.X + left.Row0.W * right.Row3.X,
                left.Row0.X * right.Row0.Y + left.Row0.Y * right.Row1.Y + left.Row0.Z * right.Row2.Y + left.Row0.W * right.Row3.Y,
                left.Row0.X * right.Row0.Z + left.Row0.Y * right.Row1.Z + left.Row0.Z * right.Row2.Z + left.Row0.W * right.Row3.Z,
                left.Row0.X * right.Row0.W + left.Row0.Y * right.Row1.W + left.Row0.Z * right.Row2.W + left.Row0.W * right.Row3.W),
            new ScalarVector4Int32(
                left.Row1.X * right.Row0.X + left.Row1.Y * right.Row1.X + left.Row1.Z * right.Row2.X + left.Row1.W * right.Row3.X,
                left.Row1.X * right.Row0.Y + left.Row1.Y * right.Row1.Y + left.Row1.Z * right.Row2.Y + left.Row1.W * right.Row3.Y,
                left.Row1.X * right.Row0.Z + left.Row1.Y * right.Row1.Z + left.Row1.Z * right.Row2.Z + left.Row1.W * right.Row3.Z,
                left.Row1.X * right.Row0.W + left.Row1.Y * right.Row1.W + left.Row1.Z * right.Row2.W + left.Row1.W * right.Row3.W),
            new ScalarVector4Int32(
                left.Row2.X * right.Row0.X + left.Row2.Y * right.Row1.X + left.Row2.Z * right.Row2.X + left.Row2.W * right.Row3.X,
                left.Row2.X * right.Row0.Y + left.Row2.Y * right.Row1.Y + left.Row2.Z * right.Row2.Y + left.Row2.W * right.Row3.Y,
                left.Row2.X * right.Row0.Z + left.Row2.Y * right.Row1.Z + left.Row2.Z * right.Row2.Z + left.Row2.W * right.Row3.Z,
                left.Row2.X * right.Row0.W + left.Row2.Y * right.Row1.W + left.Row2.Z * right.Row2.W + left.Row2.W * right.Row3.W),
            new ScalarVector4Int32(
                left.Row3.X * right.Row0.X + left.Row3.Y * right.Row1.X + left.Row3.Z * right.Row2.X + left.Row3.W * right.Row3.X,
                left.Row3.X * right.Row0.Y + left.Row3.Y * right.Row1.Y + left.Row3.Z * right.Row2.Y + left.Row3.W * right.Row3.Y,
                left.Row3.X * right.Row0.Z + left.Row3.Y * right.Row1.Z + left.Row3.Z * right.Row2.Z + left.Row3.W * right.Row3.Z,
                left.Row3.X * right.Row0.W + left.Row3.Y * right.Row1.W + left.Row3.Z * right.Row2.W + left.Row3.W * right.Row3.W));
    }

    public static ScalarVector4Int32 operator *(ScalarMatrix4Int32 matrix, ScalarVector4Int32 vector)
    {
        return new(
            matrix.Row0.X * vector.X + matrix.Row0.Y * vector.Y + matrix.Row0.Z * vector.Z + matrix.Row0.W * vector.W,
            matrix.Row1.X * vector.X + matrix.Row1.Y * vector.Y + matrix.Row1.Z * vector.Z + matrix.Row1.W * vector.W,
            matrix.Row2.X * vector.X + matrix.Row2.Y * vector.Y + matrix.Row2.Z * vector.Z + matrix.Row2.W * vector.W,
            matrix.Row3.X * vector.X + matrix.Row3.Y * vector.Y + matrix.Row3.Z * vector.Z + matrix.Row3.W * vector.W);
    }

    public static ScalarMatrix4Int32 operator *(ScalarMatrix4Int32 matrix, ScalarInt32 scalar)
    {
        ScalarVector4Int32 scalarVector = new(scalar, scalar, scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector, matrix.Row2 * scalarVector, matrix.Row3 * scalarVector);
    }

    public static ScalarMatrix4Int32 operator *(ScalarInt32 scalar, ScalarMatrix4Int32 matrix) => matrix * scalar;

    public static ScalarMatrix4Int32Mask operator ==(ScalarMatrix4Int32 left, ScalarMatrix4Int32 right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2, left.Row3 == right.Row3);

    public static ScalarMatrix4Int32Mask operator !=(ScalarMatrix4Int32 left, ScalarMatrix4Int32 right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2, left.Row3 != right.Row3);

    public override bool Equals(object? obj)
    {
        return obj is ScalarMatrix4Int32 other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2) &&
               Row3.Equals(other.Row3);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2, Row3);
}

public readonly struct ScalarMatrix4Int32Mask :
    ISimdMatrix4Mask<ScalarMatrix4Int32Mask, ScalarVector4Int32Mask, ScalarInt32Mask>
{
    public ScalarMatrix4Int32Mask(ScalarVector4Int32Mask row0, ScalarVector4Int32Mask row1, ScalarVector4Int32Mask row2, ScalarVector4Int32Mask row3)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
        Row3 = row3;
    }

    public static int LaneCount => ScalarVector4Int32Mask.LaneCount;

    public ScalarVector4Int32Mask Row0 { get; }

    public ScalarVector4Int32Mask Row1 { get; }

    public ScalarVector4Int32Mask Row2 { get; }

    public ScalarVector4Int32Mask Row3 { get; }

    public static ScalarMatrix4Int32Mask True => new(ScalarVector4Int32Mask.True, ScalarVector4Int32Mask.True, ScalarVector4Int32Mask.True, ScalarVector4Int32Mask.True);

    public static ScalarMatrix4Int32Mask False => new(ScalarVector4Int32Mask.False, ScalarVector4Int32Mask.False, ScalarVector4Int32Mask.False, ScalarVector4Int32Mask.False);

    public static ScalarMatrix4Int32Mask Create(ScalarVector4Int32Mask row0, ScalarVector4Int32Mask row1, ScalarVector4Int32Mask row2, ScalarVector4Int32Mask row3) => new(row0, row1, row2, row3);

    public static ScalarMatrix4Int32Mask Broadcast(ScalarVector4Int32Mask value) => new(value, value, value, value);

    public static ScalarVector4Int32Mask All(ScalarMatrix4Int32Mask value) => value.Row0 & value.Row1 & value.Row2 & value.Row3;

    public static ScalarVector4Int32Mask Any(ScalarMatrix4Int32Mask value) => value.Row0 | value.Row1 | value.Row2 | value.Row3;

    public static ScalarVector4Int32Mask None(ScalarMatrix4Int32Mask value) => ~(value.Row0 | value.Row1 | value.Row2 | value.Row3);

    public static ScalarMatrix4Int32Mask AndNot(ScalarMatrix4Int32Mask left, ScalarMatrix4Int32Mask right)
    {
        return new(
            ScalarVector4Int32Mask.AndNot(left.Row0, right.Row0),
            ScalarVector4Int32Mask.AndNot(left.Row1, right.Row1),
            ScalarVector4Int32Mask.AndNot(left.Row2, right.Row2),
            ScalarVector4Int32Mask.AndNot(left.Row3, right.Row3));
    }

    public static ScalarMatrix4Int32Mask Select(ScalarMatrix4Int32Mask mask, ScalarMatrix4Int32Mask ifTrue, ScalarMatrix4Int32Mask ifFalse)
    {
        return new(
            ScalarVector4Int32Mask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            ScalarVector4Int32Mask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            ScalarVector4Int32Mask.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2),
            ScalarVector4Int32Mask.Select(mask.Row3, ifTrue.Row3, ifFalse.Row3));
    }
    public static ScalarMatrix4Int32Mask And(ScalarMatrix4Int32Mask left, ScalarMatrix4Int32Mask right) => left & right;

    public static ScalarMatrix4Int32Mask Or(ScalarMatrix4Int32Mask left, ScalarMatrix4Int32Mask right) => left | right;

    public static ScalarMatrix4Int32Mask Xor(ScalarMatrix4Int32Mask left, ScalarMatrix4Int32Mask right) => left ^ right;

    public static ScalarMatrix4Int32Mask Not(ScalarMatrix4Int32Mask value) => ~value;


    public static ScalarMatrix4Int32Mask operator &(ScalarMatrix4Int32Mask left, ScalarMatrix4Int32Mask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1, left.Row2 & right.Row2, left.Row3 & right.Row3);

    public static ScalarMatrix4Int32Mask operator |(ScalarMatrix4Int32Mask left, ScalarMatrix4Int32Mask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1, left.Row2 | right.Row2, left.Row3 | right.Row3);

    public static ScalarMatrix4Int32Mask operator ^(ScalarMatrix4Int32Mask left, ScalarMatrix4Int32Mask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1, left.Row2 ^ right.Row2, left.Row3 ^ right.Row3);

    public static ScalarMatrix4Int32Mask operator ~(ScalarMatrix4Int32Mask value) => new(~value.Row0, ~value.Row1, ~value.Row2, ~value.Row3);

    public static ScalarMatrix4Int32Mask operator ==(ScalarMatrix4Int32Mask left, ScalarMatrix4Int32Mask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2, left.Row3 == right.Row3);

    public static ScalarMatrix4Int32Mask operator !=(ScalarMatrix4Int32Mask left, ScalarMatrix4Int32Mask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2, left.Row3 != right.Row3);

    public override bool Equals(object? obj)
    {
        return obj is ScalarMatrix4Int32Mask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2) &&
               Row3.Equals(other.Row3);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2, Row3);
}
