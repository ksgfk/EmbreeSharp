namespace EmbreeSharp.SIMD;

public readonly struct PacketMatrix2Float64Avx :
    ISimdFloatingPointMatrix2<PacketMatrix2Float64Avx, PacketVector2Float64Avx, PacketFloat64Avx, double, PacketMatrix2Float64AvxMask, PacketVector2Float64AvxMask, PacketFloat64AvxMask>
{
    public PacketMatrix2Float64Avx(PacketVector2Float64Avx row0, PacketVector2Float64Avx row1)
    {
        Row0 = row0;
        Row1 = row1;
    }

    public static int LaneCount => PacketVector2Float64Avx.LaneCount;

    public PacketVector2Float64Avx Row0 { get; }

    public PacketVector2Float64Avx Row1 { get; }

    public static PacketMatrix2Float64Avx Identity
    {
        get
        {
            PacketFloat64Avx zero = PacketFloat64Avx.Broadcast(0.0);
            PacketFloat64Avx one = PacketFloat64Avx.Broadcast(1.0);
            return new(
                new PacketVector2Float64Avx(one, zero),
                new PacketVector2Float64Avx(zero, one));
        }
    }

    public static PacketMatrix2Float64Avx Create(PacketVector2Float64Avx row0, PacketVector2Float64Avx row1) => new(row0, row1);

    public static PacketMatrix2Float64Avx Broadcast(double value)
    {
        PacketVector2Float64Avx row = PacketVector2Float64Avx.Broadcast(value);
        return new(row, row);
    }

    public static PacketMatrix2Float64Avx Select(PacketMatrix2Float64AvxMask mask, PacketMatrix2Float64Avx ifTrue, PacketMatrix2Float64Avx ifFalse)
    {
        return new(
            PacketVector2Float64Avx.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Float64Avx.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1));
    }

    public static PacketMatrix2Float64Avx Select(PacketVector2Float64AvxMask mask, PacketMatrix2Float64Avx ifTrue, PacketMatrix2Float64Avx ifFalse)
    {
        return new(
            PacketVector2Float64Avx.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Float64Avx.Select(mask, ifTrue.Row1, ifFalse.Row1));
    }

    public static PacketMatrix2Float64Avx Select(PacketFloat64AvxMask mask, PacketMatrix2Float64Avx ifTrue, PacketMatrix2Float64Avx ifFalse)
    {
        return new(
            PacketVector2Float64Avx.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Float64Avx.Select(mask, ifTrue.Row1, ifFalse.Row1));
    }

    public static PacketVector2Float64Avx Transform(PacketMatrix2Float64Avx matrix, PacketVector2Float64Avx vector)
    {
        return new(
            PacketFloat64Avx.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, matrix.Row0.X * vector.X),
            PacketFloat64Avx.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, matrix.Row1.X * vector.X));
    }

    public static PacketMatrix2Float64Avx Multiply(PacketMatrix2Float64Avx left, PacketMatrix2Float64Avx right) => left * right;

    public static PacketMatrix2Float64Avx Multiply(PacketMatrix2Float64Avx matrix, PacketFloat64Avx scalar) => matrix * scalar;

    public static PacketMatrix2Float64Avx Multiply(PacketFloat64Avx scalar, PacketMatrix2Float64Avx matrix) => scalar * matrix;

    public static PacketMatrix2Float64Avx FusedMultiplyAdd(PacketMatrix2Float64Avx left, PacketMatrix2Float64Avx right, PacketMatrix2Float64Avx addend)
    {
        return new(
            new PacketVector2Float64Avx(
                PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.X, right.Row0.X, addend.Row0.X)),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.X, right.Row0.Y, addend.Row0.Y))),
            new PacketVector2Float64Avx(
                PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.X, right.Row0.X, addend.Row1.X)),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.X, right.Row0.Y, addend.Row1.Y))));
    }

    public static PacketVector2Float64Avx FusedMultiplyAdd(PacketMatrix2Float64Avx matrix, PacketVector2Float64Avx vector, PacketVector2Float64Avx addend)
    {
        return new(
            PacketFloat64Avx.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row0.X, vector.X, addend.X)),
            PacketFloat64Avx.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row1.X, vector.X, addend.Y)));
    }

    public static PacketMatrix2Float64Avx Transpose(PacketMatrix2Float64Avx matrix)
    {
        return new(
            new(matrix.Row0.X, matrix.Row1.X),
            new(matrix.Row0.Y, matrix.Row1.Y));
    }

    public static PacketMatrix2Float64Avx Scale(PacketVector2Float64Avx scale) => Scale(scale.X, scale.Y);

    public static PacketMatrix2Float64Avx Scale(PacketFloat64Avx x, PacketFloat64Avx y)
    {
        PacketFloat64Avx zero = PacketFloat64Avx.Broadcast(0.0);
        return new(
            new(x, zero),
            new(zero, y));
    }

    public static PacketMatrix2Float64Avx operator *(PacketMatrix2Float64Avx left, PacketMatrix2Float64Avx right)
    {
        return new(
            new PacketVector2Float64Avx(
                PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, left.Row0.X * right.Row0.X),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, left.Row0.X * right.Row0.Y)),
            new PacketVector2Float64Avx(
                PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, left.Row1.X * right.Row0.X),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, left.Row1.X * right.Row0.Y)));
    }

    public static PacketVector2Float64Avx operator *(PacketMatrix2Float64Avx matrix, PacketVector2Float64Avx vector) => Transform(matrix, vector);

    public static PacketMatrix2Float64Avx operator *(PacketMatrix2Float64Avx matrix, PacketFloat64Avx scalar)
    {
        PacketVector2Float64Avx scalarVector = new(scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector);
    }

    public static PacketMatrix2Float64Avx operator *(PacketFloat64Avx scalar, PacketMatrix2Float64Avx matrix) => matrix * scalar;
    public static PacketMatrix2Float64Avx Divide(PacketMatrix2Float64Avx matrix, PacketFloat64Avx scalar) => matrix / scalar;

    public static PacketMatrix2Float64Avx operator /(PacketMatrix2Float64Avx matrix, PacketFloat64Avx scalar) => matrix * PacketFloat64Avx.Reciprocal(scalar);

    public static PacketMatrix2Float64Avx Inverse(PacketMatrix2Float64Avx matrix)
    {
        PacketFloat64Avx invDet = PacketFloat64Avx.Reciprocal(PacketFloat64Avx.FusedMultiplyAdd(matrix.Row0.X, matrix.Row1.Y, -(matrix.Row0.Y * matrix.Row1.X)));
        return new(
            new(matrix.Row1.Y * invDet, -matrix.Row0.Y * invDet),
            new(-matrix.Row1.X * invDet, matrix.Row0.X * invDet));
    }

    public static PacketMatrix2Float64Avx InverseTranspose(PacketMatrix2Float64Avx matrix)
    {
        PacketFloat64Avx invDet = PacketFloat64Avx.Reciprocal(PacketFloat64Avx.FusedMultiplyAdd(matrix.Row0.X, matrix.Row1.Y, -(matrix.Row0.Y * matrix.Row1.X)));
        return new(
            new(matrix.Row1.Y * invDet, -matrix.Row1.X * invDet),
            new(-matrix.Row0.Y * invDet, matrix.Row0.X * invDet));
    }

    public static PacketMatrix2Float64Avx Rotate(PacketFloat64Avx angle)
    {
        var (sin, cos) = PacketFloat64Avx.SinCos(angle);
        return new(
            new(cos, -sin),
            new(sin, cos));
    }

    public static PacketMatrix2Float64AvxMask operator ==(PacketMatrix2Float64Avx left, PacketMatrix2Float64Avx right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1);

    public static PacketMatrix2Float64AvxMask operator !=(PacketMatrix2Float64Avx left, PacketMatrix2Float64Avx right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1);

    public static PacketMatrix2Float64AvxMask operator <(PacketMatrix2Float64Avx left, PacketMatrix2Float64Avx right) => new(
        new PacketVector2Float64AvxMask(left.Row0.X < right.Row0.X, left.Row0.Y < right.Row0.Y),
        new PacketVector2Float64AvxMask(left.Row1.X < right.Row1.X, left.Row1.Y < right.Row1.Y));

    public static PacketMatrix2Float64AvxMask operator >(PacketMatrix2Float64Avx left, PacketMatrix2Float64Avx right) => new(
        new PacketVector2Float64AvxMask(left.Row0.X > right.Row0.X, left.Row0.Y > right.Row0.Y),
        new PacketVector2Float64AvxMask(left.Row1.X > right.Row1.X, left.Row1.Y > right.Row1.Y));

    public static PacketMatrix2Float64AvxMask operator <=(PacketMatrix2Float64Avx left, PacketMatrix2Float64Avx right) => new(
        new PacketVector2Float64AvxMask(left.Row0.X <= right.Row0.X, left.Row0.Y <= right.Row0.Y),
        new PacketVector2Float64AvxMask(left.Row1.X <= right.Row1.X, left.Row1.Y <= right.Row1.Y));

    public static PacketMatrix2Float64AvxMask operator >=(PacketMatrix2Float64Avx left, PacketMatrix2Float64Avx right) => new(
        new PacketVector2Float64AvxMask(left.Row0.X >= right.Row0.X, left.Row0.Y >= right.Row0.Y),
        new PacketVector2Float64AvxMask(left.Row1.X >= right.Row1.X, left.Row1.Y >= right.Row1.Y));

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix2Float64Avx other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1);
}

public readonly struct PacketMatrix2Float64AvxMask :
    ISimdMatrix2Mask<PacketMatrix2Float64AvxMask, PacketVector2Float64AvxMask, PacketFloat64AvxMask>
{
    public PacketMatrix2Float64AvxMask(PacketVector2Float64AvxMask row0, PacketVector2Float64AvxMask row1)
    {
        Row0 = row0;
        Row1 = row1;
    }

    public static int LaneCount => PacketVector2Float64AvxMask.LaneCount;

    public PacketVector2Float64AvxMask Row0 { get; }

    public PacketVector2Float64AvxMask Row1 { get; }

    public static PacketMatrix2Float64AvxMask True => new(PacketVector2Float64AvxMask.True, PacketVector2Float64AvxMask.True);

    public static PacketMatrix2Float64AvxMask False => new(PacketVector2Float64AvxMask.False, PacketVector2Float64AvxMask.False);

    public static PacketMatrix2Float64AvxMask Create(PacketVector2Float64AvxMask row0, PacketVector2Float64AvxMask row1) => new(row0, row1);

    public static PacketMatrix2Float64AvxMask Broadcast(PacketVector2Float64AvxMask value) => new(value, value);

    public static PacketVector2Float64AvxMask All(PacketMatrix2Float64AvxMask value) => value.Row0 & value.Row1;

    public static PacketVector2Float64AvxMask Any(PacketMatrix2Float64AvxMask value) => value.Row0 | value.Row1;

    public static PacketVector2Float64AvxMask None(PacketMatrix2Float64AvxMask value) => ~(value.Row0 | value.Row1);

    public static PacketMatrix2Float64AvxMask AndNot(PacketMatrix2Float64AvxMask left, PacketMatrix2Float64AvxMask right)
    {
        return new(
            PacketVector2Float64AvxMask.AndNot(left.Row0, right.Row0),
            PacketVector2Float64AvxMask.AndNot(left.Row1, right.Row1));
    }

    public static PacketMatrix2Float64AvxMask Select(PacketMatrix2Float64AvxMask mask, PacketMatrix2Float64AvxMask ifTrue, PacketMatrix2Float64AvxMask ifFalse)
    {
        return new(
            PacketVector2Float64AvxMask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Float64AvxMask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1));
    }
    public static PacketMatrix2Float64AvxMask And(PacketMatrix2Float64AvxMask left, PacketMatrix2Float64AvxMask right) => left & right;

    public static PacketMatrix2Float64AvxMask Or(PacketMatrix2Float64AvxMask left, PacketMatrix2Float64AvxMask right) => left | right;

    public static PacketMatrix2Float64AvxMask Xor(PacketMatrix2Float64AvxMask left, PacketMatrix2Float64AvxMask right) => left ^ right;

    public static PacketMatrix2Float64AvxMask Not(PacketMatrix2Float64AvxMask value) => ~value;


    public static PacketMatrix2Float64AvxMask operator &(PacketMatrix2Float64AvxMask left, PacketMatrix2Float64AvxMask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1);

    public static PacketMatrix2Float64AvxMask operator |(PacketMatrix2Float64AvxMask left, PacketMatrix2Float64AvxMask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1);

    public static PacketMatrix2Float64AvxMask operator ^(PacketMatrix2Float64AvxMask left, PacketMatrix2Float64AvxMask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1);

    public static PacketMatrix2Float64AvxMask operator ~(PacketMatrix2Float64AvxMask value) => new(~value.Row0, ~value.Row1);

    public static PacketMatrix2Float64AvxMask operator ==(PacketMatrix2Float64AvxMask left, PacketMatrix2Float64AvxMask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1);

    public static PacketMatrix2Float64AvxMask operator !=(PacketMatrix2Float64AvxMask left, PacketMatrix2Float64AvxMask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix2Float64AvxMask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1);
}
