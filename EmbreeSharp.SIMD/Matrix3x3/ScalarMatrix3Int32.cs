namespace EmbreeSharp.SIMD;

public readonly struct ScalarMatrix3Int32 :
    ISimdIntegerMatrix3<ScalarMatrix3Int32, ScalarVector3Int32, ScalarInt32, int, ScalarMatrix3Int32Mask, ScalarVector3Int32Mask, ScalarInt32Mask>
{
    public ScalarMatrix3Int32(ScalarVector3Int32 row0, ScalarVector3Int32 row1, ScalarVector3Int32 row2)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
    }

    public static int LaneCount => ScalarVector3Int32.LaneCount;

    public ScalarVector3Int32 Row0 { get; }

    public ScalarVector3Int32 Row1 { get; }

    public ScalarVector3Int32 Row2 { get; }

    public static ScalarMatrix3Int32 Identity
    {
        get
        {
            ScalarInt32 zero = ScalarInt32.Broadcast(0);
            ScalarInt32 one = ScalarInt32.Broadcast(1);
            return new(
                new ScalarVector3Int32(one, zero, zero),
                new ScalarVector3Int32(zero, one, zero),
                new ScalarVector3Int32(zero, zero, one));
        }
    }

    public static ScalarMatrix3Int32 Create(ScalarVector3Int32 row0, ScalarVector3Int32 row1, ScalarVector3Int32 row2) => new(row0, row1, row2);

    public static ScalarMatrix3Int32 Broadcast(int value)
    {
        ScalarVector3Int32 row = ScalarVector3Int32.Broadcast(value);
        return new(row, row, row);
    }

    public static ScalarMatrix3Int32 Select(ScalarMatrix3Int32Mask mask, ScalarMatrix3Int32 ifTrue, ScalarMatrix3Int32 ifFalse)
    {
        return new(
            ScalarVector3Int32.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            ScalarVector3Int32.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            ScalarVector3Int32.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2));
    }

    public static ScalarMatrix3Int32 Select(ScalarVector3Int32Mask mask, ScalarMatrix3Int32 ifTrue, ScalarMatrix3Int32 ifFalse)
    {
        return new(
            ScalarVector3Int32.Select(mask, ifTrue.Row0, ifFalse.Row0),
            ScalarVector3Int32.Select(mask, ifTrue.Row1, ifFalse.Row1),
            ScalarVector3Int32.Select(mask, ifTrue.Row2, ifFalse.Row2));
    }

    public static ScalarMatrix3Int32 Select(ScalarInt32Mask mask, ScalarMatrix3Int32 ifTrue, ScalarMatrix3Int32 ifFalse)
    {
        return new(
            ScalarVector3Int32.Select(mask, ifTrue.Row0, ifFalse.Row0),
            ScalarVector3Int32.Select(mask, ifTrue.Row1, ifFalse.Row1),
            ScalarVector3Int32.Select(mask, ifTrue.Row2, ifFalse.Row2));
    }

    public static ScalarMatrix3Int32 Multiply(ScalarMatrix3Int32 left, ScalarMatrix3Int32 right) => left * right;

    public static ScalarMatrix3Int32 Multiply(ScalarMatrix3Int32 matrix, ScalarInt32 scalar) => matrix * scalar;

    public static ScalarMatrix3Int32 Multiply(ScalarInt32 scalar, ScalarMatrix3Int32 matrix) => scalar * matrix;

    public static ScalarMatrix3Int32 operator *(ScalarMatrix3Int32 left, ScalarMatrix3Int32 right)
    {
        return new(
            new ScalarVector3Int32(
                left.Row0.X * right.Row0.X + left.Row0.Y * right.Row1.X + left.Row0.Z * right.Row2.X,
                left.Row0.X * right.Row0.Y + left.Row0.Y * right.Row1.Y + left.Row0.Z * right.Row2.Y,
                left.Row0.X * right.Row0.Z + left.Row0.Y * right.Row1.Z + left.Row0.Z * right.Row2.Z),
            new ScalarVector3Int32(
                left.Row1.X * right.Row0.X + left.Row1.Y * right.Row1.X + left.Row1.Z * right.Row2.X,
                left.Row1.X * right.Row0.Y + left.Row1.Y * right.Row1.Y + left.Row1.Z * right.Row2.Y,
                left.Row1.X * right.Row0.Z + left.Row1.Y * right.Row1.Z + left.Row1.Z * right.Row2.Z),
            new ScalarVector3Int32(
                left.Row2.X * right.Row0.X + left.Row2.Y * right.Row1.X + left.Row2.Z * right.Row2.X,
                left.Row2.X * right.Row0.Y + left.Row2.Y * right.Row1.Y + left.Row2.Z * right.Row2.Y,
                left.Row2.X * right.Row0.Z + left.Row2.Y * right.Row1.Z + left.Row2.Z * right.Row2.Z));
    }

    public static ScalarVector3Int32 operator *(ScalarMatrix3Int32 matrix, ScalarVector3Int32 vector)
    {
        return new(
            matrix.Row0.X * vector.X + matrix.Row0.Y * vector.Y + matrix.Row0.Z * vector.Z,
            matrix.Row1.X * vector.X + matrix.Row1.Y * vector.Y + matrix.Row1.Z * vector.Z,
            matrix.Row2.X * vector.X + matrix.Row2.Y * vector.Y + matrix.Row2.Z * vector.Z);
    }

    public static ScalarMatrix3Int32 operator *(ScalarMatrix3Int32 matrix, ScalarInt32 scalar)
    {
        ScalarVector3Int32 scalarVector = new(scalar, scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector, matrix.Row2 * scalarVector);
    }

    public static ScalarMatrix3Int32 operator *(ScalarInt32 scalar, ScalarMatrix3Int32 matrix) => matrix * scalar;

    public static ScalarMatrix3Int32Mask operator ==(ScalarMatrix3Int32 left, ScalarMatrix3Int32 right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2);

    public static ScalarMatrix3Int32Mask operator !=(ScalarMatrix3Int32 left, ScalarMatrix3Int32 right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2);

    public override bool Equals(object? obj)
    {
        return obj is ScalarMatrix3Int32 other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2);
}

public readonly struct ScalarMatrix3Int32Mask :
    ISimdMatrix3Mask<ScalarMatrix3Int32Mask, ScalarVector3Int32Mask, ScalarInt32Mask>
{
    public ScalarMatrix3Int32Mask(ScalarVector3Int32Mask row0, ScalarVector3Int32Mask row1, ScalarVector3Int32Mask row2)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
    }

    public static int LaneCount => ScalarVector3Int32Mask.LaneCount;

    public ScalarVector3Int32Mask Row0 { get; }

    public ScalarVector3Int32Mask Row1 { get; }

    public ScalarVector3Int32Mask Row2 { get; }

    public static ScalarMatrix3Int32Mask True => new(ScalarVector3Int32Mask.True, ScalarVector3Int32Mask.True, ScalarVector3Int32Mask.True);

    public static ScalarMatrix3Int32Mask False => new(ScalarVector3Int32Mask.False, ScalarVector3Int32Mask.False, ScalarVector3Int32Mask.False);

    public static ScalarMatrix3Int32Mask Create(ScalarVector3Int32Mask row0, ScalarVector3Int32Mask row1, ScalarVector3Int32Mask row2) => new(row0, row1, row2);

    public static ScalarMatrix3Int32Mask Broadcast(ScalarVector3Int32Mask value) => new(value, value, value);

    public static ScalarVector3Int32Mask All(ScalarMatrix3Int32Mask value) => value.Row0 & value.Row1 & value.Row2;

    public static ScalarVector3Int32Mask Any(ScalarMatrix3Int32Mask value) => value.Row0 | value.Row1 | value.Row2;

    public static ScalarVector3Int32Mask None(ScalarMatrix3Int32Mask value) => ~(value.Row0 | value.Row1 | value.Row2);

    public static ScalarMatrix3Int32Mask AndNot(ScalarMatrix3Int32Mask left, ScalarMatrix3Int32Mask right)
    {
        return new(
            ScalarVector3Int32Mask.AndNot(left.Row0, right.Row0),
            ScalarVector3Int32Mask.AndNot(left.Row1, right.Row1),
            ScalarVector3Int32Mask.AndNot(left.Row2, right.Row2));
    }

    public static ScalarMatrix3Int32Mask Select(ScalarMatrix3Int32Mask mask, ScalarMatrix3Int32Mask ifTrue, ScalarMatrix3Int32Mask ifFalse)
    {
        return new(
            ScalarVector3Int32Mask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            ScalarVector3Int32Mask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            ScalarVector3Int32Mask.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2));
    }
    public static ScalarMatrix3Int32Mask And(ScalarMatrix3Int32Mask left, ScalarMatrix3Int32Mask right) => left & right;

    public static ScalarMatrix3Int32Mask Or(ScalarMatrix3Int32Mask left, ScalarMatrix3Int32Mask right) => left | right;

    public static ScalarMatrix3Int32Mask Xor(ScalarMatrix3Int32Mask left, ScalarMatrix3Int32Mask right) => left ^ right;

    public static ScalarMatrix3Int32Mask Not(ScalarMatrix3Int32Mask value) => ~value;


    public static ScalarMatrix3Int32Mask operator &(ScalarMatrix3Int32Mask left, ScalarMatrix3Int32Mask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1, left.Row2 & right.Row2);

    public static ScalarMatrix3Int32Mask operator |(ScalarMatrix3Int32Mask left, ScalarMatrix3Int32Mask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1, left.Row2 | right.Row2);

    public static ScalarMatrix3Int32Mask operator ^(ScalarMatrix3Int32Mask left, ScalarMatrix3Int32Mask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1, left.Row2 ^ right.Row2);

    public static ScalarMatrix3Int32Mask operator ~(ScalarMatrix3Int32Mask value) => new(~value.Row0, ~value.Row1, ~value.Row2);

    public static ScalarMatrix3Int32Mask operator ==(ScalarMatrix3Int32Mask left, ScalarMatrix3Int32Mask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2);

    public static ScalarMatrix3Int32Mask operator !=(ScalarMatrix3Int32Mask left, ScalarMatrix3Int32Mask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2);

    public override bool Equals(object? obj)
    {
        return obj is ScalarMatrix3Int32Mask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2);
}
