namespace EmbreeSharp.SIMD;

public readonly struct PacketMatrix2Float64Sse :
    ISimdFloatingPointMatrix2<PacketMatrix2Float64Sse, PacketVector2Float64Sse, PacketFloat64Sse, double, PacketMatrix2Float64SseMask, PacketVector2Float64SseMask, PacketFloat64SseMask>
{
    public PacketMatrix2Float64Sse(PacketVector2Float64Sse row0, PacketVector2Float64Sse row1)
    {
        Row0 = row0;
        Row1 = row1;
    }

    public static int LaneCount => PacketVector2Float64Sse.LaneCount;

    public PacketVector2Float64Sse Row0 { get; }

    public PacketVector2Float64Sse Row1 { get; }

    public static PacketMatrix2Float64Sse Identity
    {
        get
        {
            PacketFloat64Sse zero = PacketFloat64Sse.Broadcast(0.0);
            PacketFloat64Sse one = PacketFloat64Sse.Broadcast(1.0);
            return new(
                new PacketVector2Float64Sse(one, zero),
                new PacketVector2Float64Sse(zero, one));
        }
    }

    public static PacketMatrix2Float64Sse Create(PacketVector2Float64Sse row0, PacketVector2Float64Sse row1) => new(row0, row1);

    public static PacketMatrix2Float64Sse Broadcast(double value)
    {
        PacketVector2Float64Sse row = PacketVector2Float64Sse.Broadcast(value);
        return new(row, row);
    }

    public static PacketMatrix2Float64Sse Select(PacketMatrix2Float64SseMask mask, PacketMatrix2Float64Sse ifTrue, PacketMatrix2Float64Sse ifFalse)
    {
        return new(
            PacketVector2Float64Sse.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Float64Sse.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1));
    }

    public static PacketMatrix2Float64Sse Select(PacketVector2Float64SseMask mask, PacketMatrix2Float64Sse ifTrue, PacketMatrix2Float64Sse ifFalse)
    {
        return new(
            PacketVector2Float64Sse.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Float64Sse.Select(mask, ifTrue.Row1, ifFalse.Row1));
    }

    public static PacketMatrix2Float64Sse Select(PacketFloat64SseMask mask, PacketMatrix2Float64Sse ifTrue, PacketMatrix2Float64Sse ifFalse)
    {
        return new(
            PacketVector2Float64Sse.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Float64Sse.Select(mask, ifTrue.Row1, ifFalse.Row1));
    }

    public static PacketVector2Float64Sse Transform(PacketMatrix2Float64Sse matrix, PacketVector2Float64Sse vector)
    {
        return new(
            PacketFloat64Sse.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, matrix.Row0.X * vector.X),
            PacketFloat64Sse.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, matrix.Row1.X * vector.X));
    }

    public static PacketMatrix2Float64Sse Multiply(PacketMatrix2Float64Sse left, PacketMatrix2Float64Sse right) => left * right;

    public static PacketMatrix2Float64Sse Multiply(PacketMatrix2Float64Sse matrix, PacketFloat64Sse scalar) => matrix * scalar;

    public static PacketMatrix2Float64Sse Multiply(PacketFloat64Sse scalar, PacketMatrix2Float64Sse matrix) => scalar * matrix;

    public static PacketMatrix2Float64Sse FusedMultiplyAdd(PacketMatrix2Float64Sse left, PacketMatrix2Float64Sse right, PacketMatrix2Float64Sse addend)
    {
        return new(
            new PacketVector2Float64Sse(
                PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.X, right.Row0.X, addend.Row0.X)),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.X, right.Row0.Y, addend.Row0.Y))),
            new PacketVector2Float64Sse(
                PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.X, right.Row0.X, addend.Row1.X)),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.X, right.Row0.Y, addend.Row1.Y))));
    }

    public static PacketVector2Float64Sse FusedMultiplyAdd(PacketMatrix2Float64Sse matrix, PacketVector2Float64Sse vector, PacketVector2Float64Sse addend)
    {
        return new(
            PacketFloat64Sse.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row0.X, vector.X, addend.X)),
            PacketFloat64Sse.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row1.X, vector.X, addend.Y)));
    }

    public static PacketMatrix2Float64Sse Transpose(PacketMatrix2Float64Sse matrix)
    {
        return new(
            new(matrix.Row0.X, matrix.Row1.X),
            new(matrix.Row0.Y, matrix.Row1.Y));
    }

    public static PacketMatrix2Float64Sse Scale(PacketVector2Float64Sse scale) => Scale(scale.X, scale.Y);

    public static PacketMatrix2Float64Sse Scale(PacketFloat64Sse x, PacketFloat64Sse y)
    {
        PacketFloat64Sse zero = PacketFloat64Sse.Broadcast(0.0);
        return new(
            new(x, zero),
            new(zero, y));
    }

    public static PacketMatrix2Float64Sse operator *(PacketMatrix2Float64Sse left, PacketMatrix2Float64Sse right)
    {
        return new(
            new PacketVector2Float64Sse(
                PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, left.Row0.X * right.Row0.X),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, left.Row0.X * right.Row0.Y)),
            new PacketVector2Float64Sse(
                PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, left.Row1.X * right.Row0.X),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, left.Row1.X * right.Row0.Y)));
    }

    public static PacketVector2Float64Sse operator *(PacketMatrix2Float64Sse matrix, PacketVector2Float64Sse vector) => Transform(matrix, vector);

    public static PacketMatrix2Float64Sse operator *(PacketMatrix2Float64Sse matrix, PacketFloat64Sse scalar)
    {
        PacketVector2Float64Sse scalarVector = new(scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector);
    }

    public static PacketMatrix2Float64Sse operator *(PacketFloat64Sse scalar, PacketMatrix2Float64Sse matrix) => matrix * scalar;
    public static PacketMatrix2Float64Sse Divide(PacketMatrix2Float64Sse matrix, PacketFloat64Sse scalar) => matrix / scalar;

    public static PacketMatrix2Float64Sse operator /(PacketMatrix2Float64Sse matrix, PacketFloat64Sse scalar) => matrix * PacketFloat64Sse.Reciprocal(scalar);

    public static PacketMatrix2Float64Sse Inverse(PacketMatrix2Float64Sse matrix)
    {
        PacketFloat64Sse invDet = PacketFloat64Sse.Reciprocal(PacketFloat64Sse.FusedMultiplyAdd(matrix.Row0.X, matrix.Row1.Y, -(matrix.Row0.Y * matrix.Row1.X)));
        return new(
            new(matrix.Row1.Y * invDet, -matrix.Row0.Y * invDet),
            new(-matrix.Row1.X * invDet, matrix.Row0.X * invDet));
    }

    public static PacketMatrix2Float64Sse InverseTranspose(PacketMatrix2Float64Sse matrix)
    {
        PacketFloat64Sse invDet = PacketFloat64Sse.Reciprocal(PacketFloat64Sse.FusedMultiplyAdd(matrix.Row0.X, matrix.Row1.Y, -(matrix.Row0.Y * matrix.Row1.X)));
        return new(
            new(matrix.Row1.Y * invDet, -matrix.Row1.X * invDet),
            new(-matrix.Row0.Y * invDet, matrix.Row0.X * invDet));
    }

    public static PacketMatrix2Float64Sse Rotate(PacketFloat64Sse angle)
    {
        var (sin, cos) = PacketFloat64Sse.SinCos(angle);
        return new(
            new(cos, -sin),
            new(sin, cos));
    }

    public static PacketMatrix2Float64SseMask operator ==(PacketMatrix2Float64Sse left, PacketMatrix2Float64Sse right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1);

    public static PacketMatrix2Float64SseMask operator !=(PacketMatrix2Float64Sse left, PacketMatrix2Float64Sse right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1);

    public static PacketMatrix2Float64SseMask operator <(PacketMatrix2Float64Sse left, PacketMatrix2Float64Sse right) => new(
        new PacketVector2Float64SseMask(left.Row0.X < right.Row0.X, left.Row0.Y < right.Row0.Y),
        new PacketVector2Float64SseMask(left.Row1.X < right.Row1.X, left.Row1.Y < right.Row1.Y));

    public static PacketMatrix2Float64SseMask operator >(PacketMatrix2Float64Sse left, PacketMatrix2Float64Sse right) => new(
        new PacketVector2Float64SseMask(left.Row0.X > right.Row0.X, left.Row0.Y > right.Row0.Y),
        new PacketVector2Float64SseMask(left.Row1.X > right.Row1.X, left.Row1.Y > right.Row1.Y));

    public static PacketMatrix2Float64SseMask operator <=(PacketMatrix2Float64Sse left, PacketMatrix2Float64Sse right) => new(
        new PacketVector2Float64SseMask(left.Row0.X <= right.Row0.X, left.Row0.Y <= right.Row0.Y),
        new PacketVector2Float64SseMask(left.Row1.X <= right.Row1.X, left.Row1.Y <= right.Row1.Y));

    public static PacketMatrix2Float64SseMask operator >=(PacketMatrix2Float64Sse left, PacketMatrix2Float64Sse right) => new(
        new PacketVector2Float64SseMask(left.Row0.X >= right.Row0.X, left.Row0.Y >= right.Row0.Y),
        new PacketVector2Float64SseMask(left.Row1.X >= right.Row1.X, left.Row1.Y >= right.Row1.Y));

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix2Float64Sse other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1);
}

public readonly struct PacketMatrix2Float64SseMask :
    ISimdMatrix2Mask<PacketMatrix2Float64SseMask, PacketVector2Float64SseMask, PacketFloat64SseMask>
{
    public PacketMatrix2Float64SseMask(PacketVector2Float64SseMask row0, PacketVector2Float64SseMask row1)
    {
        Row0 = row0;
        Row1 = row1;
    }

    public static int LaneCount => PacketVector2Float64SseMask.LaneCount;

    public PacketVector2Float64SseMask Row0 { get; }

    public PacketVector2Float64SseMask Row1 { get; }

    public static PacketMatrix2Float64SseMask True => new(PacketVector2Float64SseMask.True, PacketVector2Float64SseMask.True);

    public static PacketMatrix2Float64SseMask False => new(PacketVector2Float64SseMask.False, PacketVector2Float64SseMask.False);

    public static PacketMatrix2Float64SseMask Create(PacketVector2Float64SseMask row0, PacketVector2Float64SseMask row1) => new(row0, row1);

    public static PacketMatrix2Float64SseMask Broadcast(PacketVector2Float64SseMask value) => new(value, value);

    public static PacketVector2Float64SseMask All(PacketMatrix2Float64SseMask value) => value.Row0 & value.Row1;

    public static PacketVector2Float64SseMask Any(PacketMatrix2Float64SseMask value) => value.Row0 | value.Row1;

    public static PacketVector2Float64SseMask None(PacketMatrix2Float64SseMask value) => ~(value.Row0 | value.Row1);

    public static PacketMatrix2Float64SseMask AndNot(PacketMatrix2Float64SseMask left, PacketMatrix2Float64SseMask right)
    {
        return new(
            PacketVector2Float64SseMask.AndNot(left.Row0, right.Row0),
            PacketVector2Float64SseMask.AndNot(left.Row1, right.Row1));
    }

    public static PacketMatrix2Float64SseMask Select(PacketMatrix2Float64SseMask mask, PacketMatrix2Float64SseMask ifTrue, PacketMatrix2Float64SseMask ifFalse)
    {
        return new(
            PacketVector2Float64SseMask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Float64SseMask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1));
    }
    public static PacketMatrix2Float64SseMask And(PacketMatrix2Float64SseMask left, PacketMatrix2Float64SseMask right) => left & right;

    public static PacketMatrix2Float64SseMask Or(PacketMatrix2Float64SseMask left, PacketMatrix2Float64SseMask right) => left | right;

    public static PacketMatrix2Float64SseMask Xor(PacketMatrix2Float64SseMask left, PacketMatrix2Float64SseMask right) => left ^ right;

    public static PacketMatrix2Float64SseMask Not(PacketMatrix2Float64SseMask value) => ~value;


    public static PacketMatrix2Float64SseMask operator &(PacketMatrix2Float64SseMask left, PacketMatrix2Float64SseMask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1);

    public static PacketMatrix2Float64SseMask operator |(PacketMatrix2Float64SseMask left, PacketMatrix2Float64SseMask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1);

    public static PacketMatrix2Float64SseMask operator ^(PacketMatrix2Float64SseMask left, PacketMatrix2Float64SseMask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1);

    public static PacketMatrix2Float64SseMask operator ~(PacketMatrix2Float64SseMask value) => new(~value.Row0, ~value.Row1);

    public static PacketMatrix2Float64SseMask operator ==(PacketMatrix2Float64SseMask left, PacketMatrix2Float64SseMask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1);

    public static PacketMatrix2Float64SseMask operator !=(PacketMatrix2Float64SseMask left, PacketMatrix2Float64SseMask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix2Float64SseMask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1);
}
