namespace EmbreeSharp.SIMD;

public readonly struct ScalarMatrix2Int32 :
    ISimdIntegerMatrix2<ScalarMatrix2Int32, ScalarVector2Int32, ScalarInt32, int, ScalarMatrix2Int32Mask, ScalarVector2Int32Mask, ScalarInt32Mask>
{
    public ScalarMatrix2Int32(ScalarVector2Int32 row0, ScalarVector2Int32 row1)
    {
        Row0 = row0;
        Row1 = row1;
    }

    public static int LaneCount => ScalarVector2Int32.LaneCount;

    public ScalarVector2Int32 Row0 { get; }

    public ScalarVector2Int32 Row1 { get; }

    public static ScalarMatrix2Int32 Identity
    {
        get
        {
            ScalarInt32 zero = ScalarInt32.Broadcast(0);
            ScalarInt32 one = ScalarInt32.Broadcast(1);
            return new(
                new ScalarVector2Int32(one, zero),
                new ScalarVector2Int32(zero, one));
        }
    }

    public static ScalarMatrix2Int32 Create(ScalarVector2Int32 row0, ScalarVector2Int32 row1) => new(row0, row1);

    public static ScalarMatrix2Int32 Broadcast(int value)
    {
        ScalarVector2Int32 row = ScalarVector2Int32.Broadcast(value);
        return new(row, row);
    }

    public static ScalarMatrix2Int32 Select(ScalarMatrix2Int32Mask mask, ScalarMatrix2Int32 ifTrue, ScalarMatrix2Int32 ifFalse)
    {
        return new(
            ScalarVector2Int32.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            ScalarVector2Int32.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1));
    }

    public static ScalarMatrix2Int32 Select(ScalarVector2Int32Mask mask, ScalarMatrix2Int32 ifTrue, ScalarMatrix2Int32 ifFalse)
    {
        return new(
            ScalarVector2Int32.Select(mask, ifTrue.Row0, ifFalse.Row0),
            ScalarVector2Int32.Select(mask, ifTrue.Row1, ifFalse.Row1));
    }

    public static ScalarMatrix2Int32 Select(ScalarInt32Mask mask, ScalarMatrix2Int32 ifTrue, ScalarMatrix2Int32 ifFalse)
    {
        return new(
            ScalarVector2Int32.Select(mask, ifTrue.Row0, ifFalse.Row0),
            ScalarVector2Int32.Select(mask, ifTrue.Row1, ifFalse.Row1));
    }

    public static ScalarMatrix2Int32 Multiply(ScalarMatrix2Int32 left, ScalarMatrix2Int32 right) => left * right;

    public static ScalarMatrix2Int32 Multiply(ScalarMatrix2Int32 matrix, ScalarInt32 scalar) => matrix * scalar;

    public static ScalarMatrix2Int32 Multiply(ScalarInt32 scalar, ScalarMatrix2Int32 matrix) => scalar * matrix;

    public static ScalarMatrix2Int32 operator *(ScalarMatrix2Int32 left, ScalarMatrix2Int32 right)
    {
        return new(
            new ScalarVector2Int32(
                left.Row0.X * right.Row0.X + left.Row0.Y * right.Row1.X,
                left.Row0.X * right.Row0.Y + left.Row0.Y * right.Row1.Y),
            new ScalarVector2Int32(
                left.Row1.X * right.Row0.X + left.Row1.Y * right.Row1.X,
                left.Row1.X * right.Row0.Y + left.Row1.Y * right.Row1.Y));
    }

    public static ScalarVector2Int32 operator *(ScalarMatrix2Int32 matrix, ScalarVector2Int32 vector)
    {
        return new(
            matrix.Row0.X * vector.X + matrix.Row0.Y * vector.Y,
            matrix.Row1.X * vector.X + matrix.Row1.Y * vector.Y);
    }

    public static ScalarMatrix2Int32 operator *(ScalarMatrix2Int32 matrix, ScalarInt32 scalar)
    {
        ScalarVector2Int32 scalarVector = new(scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector);
    }

    public static ScalarMatrix2Int32 operator *(ScalarInt32 scalar, ScalarMatrix2Int32 matrix) => matrix * scalar;

    public static ScalarMatrix2Int32Mask operator ==(ScalarMatrix2Int32 left, ScalarMatrix2Int32 right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1);

    public static ScalarMatrix2Int32Mask operator !=(ScalarMatrix2Int32 left, ScalarMatrix2Int32 right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1);

    public static ScalarMatrix2Int32Mask operator <(ScalarMatrix2Int32 left, ScalarMatrix2Int32 right) => new(
        new ScalarVector2Int32Mask(left.Row0.X < right.Row0.X, left.Row0.Y < right.Row0.Y),
        new ScalarVector2Int32Mask(left.Row1.X < right.Row1.X, left.Row1.Y < right.Row1.Y));

    public static ScalarMatrix2Int32Mask operator >(ScalarMatrix2Int32 left, ScalarMatrix2Int32 right) => new(
        new ScalarVector2Int32Mask(left.Row0.X > right.Row0.X, left.Row0.Y > right.Row0.Y),
        new ScalarVector2Int32Mask(left.Row1.X > right.Row1.X, left.Row1.Y > right.Row1.Y));

    public static ScalarMatrix2Int32Mask operator <=(ScalarMatrix2Int32 left, ScalarMatrix2Int32 right) => new(
        new ScalarVector2Int32Mask(left.Row0.X <= right.Row0.X, left.Row0.Y <= right.Row0.Y),
        new ScalarVector2Int32Mask(left.Row1.X <= right.Row1.X, left.Row1.Y <= right.Row1.Y));

    public static ScalarMatrix2Int32Mask operator >=(ScalarMatrix2Int32 left, ScalarMatrix2Int32 right) => new(
        new ScalarVector2Int32Mask(left.Row0.X >= right.Row0.X, left.Row0.Y >= right.Row0.Y),
        new ScalarVector2Int32Mask(left.Row1.X >= right.Row1.X, left.Row1.Y >= right.Row1.Y));

    public override bool Equals(object? obj)
    {
        return obj is ScalarMatrix2Int32 other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1);
}

public readonly struct ScalarMatrix2Int32Mask :
    ISimdMatrix2Mask<ScalarMatrix2Int32Mask, ScalarVector2Int32Mask, ScalarInt32Mask>
{
    public ScalarMatrix2Int32Mask(ScalarVector2Int32Mask row0, ScalarVector2Int32Mask row1)
    {
        Row0 = row0;
        Row1 = row1;
    }

    public static int LaneCount => ScalarVector2Int32Mask.LaneCount;

    public ScalarVector2Int32Mask Row0 { get; }

    public ScalarVector2Int32Mask Row1 { get; }

    public static ScalarMatrix2Int32Mask True => new(ScalarVector2Int32Mask.True, ScalarVector2Int32Mask.True);

    public static ScalarMatrix2Int32Mask False => new(ScalarVector2Int32Mask.False, ScalarVector2Int32Mask.False);

    public static ScalarMatrix2Int32Mask Create(ScalarVector2Int32Mask row0, ScalarVector2Int32Mask row1) => new(row0, row1);

    public static ScalarMatrix2Int32Mask Broadcast(ScalarVector2Int32Mask value) => new(value, value);

    public static ScalarVector2Int32Mask All(ScalarMatrix2Int32Mask value) => value.Row0 & value.Row1;

    public static ScalarVector2Int32Mask Any(ScalarMatrix2Int32Mask value) => value.Row0 | value.Row1;

    public static ScalarVector2Int32Mask None(ScalarMatrix2Int32Mask value) => ~(value.Row0 | value.Row1);

    public static ScalarMatrix2Int32Mask AndNot(ScalarMatrix2Int32Mask left, ScalarMatrix2Int32Mask right)
    {
        return new(
            ScalarVector2Int32Mask.AndNot(left.Row0, right.Row0),
            ScalarVector2Int32Mask.AndNot(left.Row1, right.Row1));
    }

    public static ScalarMatrix2Int32Mask Select(ScalarMatrix2Int32Mask mask, ScalarMatrix2Int32Mask ifTrue, ScalarMatrix2Int32Mask ifFalse)
    {
        return new(
            ScalarVector2Int32Mask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            ScalarVector2Int32Mask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1));
    }
    public static ScalarMatrix2Int32Mask And(ScalarMatrix2Int32Mask left, ScalarMatrix2Int32Mask right) => left & right;

    public static ScalarMatrix2Int32Mask Or(ScalarMatrix2Int32Mask left, ScalarMatrix2Int32Mask right) => left | right;

    public static ScalarMatrix2Int32Mask Xor(ScalarMatrix2Int32Mask left, ScalarMatrix2Int32Mask right) => left ^ right;

    public static ScalarMatrix2Int32Mask Not(ScalarMatrix2Int32Mask value) => ~value;


    public static ScalarMatrix2Int32Mask operator &(ScalarMatrix2Int32Mask left, ScalarMatrix2Int32Mask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1);

    public static ScalarMatrix2Int32Mask operator |(ScalarMatrix2Int32Mask left, ScalarMatrix2Int32Mask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1);

    public static ScalarMatrix2Int32Mask operator ^(ScalarMatrix2Int32Mask left, ScalarMatrix2Int32Mask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1);

    public static ScalarMatrix2Int32Mask operator ~(ScalarMatrix2Int32Mask value) => new(~value.Row0, ~value.Row1);

    public static ScalarMatrix2Int32Mask operator ==(ScalarMatrix2Int32Mask left, ScalarMatrix2Int32Mask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1);

    public static ScalarMatrix2Int32Mask operator !=(ScalarMatrix2Int32Mask left, ScalarMatrix2Int32Mask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1);

    public override bool Equals(object? obj)
    {
        return obj is ScalarMatrix2Int32Mask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1);
}
