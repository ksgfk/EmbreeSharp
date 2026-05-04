namespace EmbreeSharp.SIMD;

public readonly struct PacketMatrix2Float64Neon :
    ISimdFloatingPointMatrix2<PacketMatrix2Float64Neon, PacketVector2Float64Neon, PacketFloat64Neon, double, PacketMatrix2Float64NeonMask, PacketVector2Float64NeonMask, PacketFloat64NeonMask>
{
    public PacketMatrix2Float64Neon(PacketVector2Float64Neon row0, PacketVector2Float64Neon row1)
    {
        Row0 = row0;
        Row1 = row1;
    }

    public static int LaneCount => PacketVector2Float64Neon.LaneCount;

    public PacketVector2Float64Neon Row0 { get; }

    public PacketVector2Float64Neon Row1 { get; }

    public static PacketMatrix2Float64Neon Identity
    {
        get
        {
            PacketFloat64Neon zero = PacketFloat64Neon.Broadcast(0.0);
            PacketFloat64Neon one = PacketFloat64Neon.Broadcast(1.0);
            return new(
                new PacketVector2Float64Neon(one, zero),
                new PacketVector2Float64Neon(zero, one));
        }
    }

    public static PacketMatrix2Float64Neon Create(PacketVector2Float64Neon row0, PacketVector2Float64Neon row1) => new(row0, row1);

    public static PacketMatrix2Float64Neon Broadcast(double value)
    {
        PacketVector2Float64Neon row = PacketVector2Float64Neon.Broadcast(value);
        return new(row, row);
    }

    public static PacketMatrix2Float64Neon Select(PacketMatrix2Float64NeonMask mask, PacketMatrix2Float64Neon ifTrue, PacketMatrix2Float64Neon ifFalse)
    {
        return new(
            PacketVector2Float64Neon.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Float64Neon.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1));
    }

    public static PacketMatrix2Float64Neon Select(PacketVector2Float64NeonMask mask, PacketMatrix2Float64Neon ifTrue, PacketMatrix2Float64Neon ifFalse)
    {
        return new(
            PacketVector2Float64Neon.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Float64Neon.Select(mask, ifTrue.Row1, ifFalse.Row1));
    }

    public static PacketMatrix2Float64Neon Select(PacketFloat64NeonMask mask, PacketMatrix2Float64Neon ifTrue, PacketMatrix2Float64Neon ifFalse)
    {
        return new(
            PacketVector2Float64Neon.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Float64Neon.Select(mask, ifTrue.Row1, ifFalse.Row1));
    }

    public static PacketVector2Float64Neon Transform(PacketMatrix2Float64Neon matrix, PacketVector2Float64Neon vector)
    {
        return new(
            PacketFloat64Neon.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, matrix.Row0.X * vector.X),
            PacketFloat64Neon.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, matrix.Row1.X * vector.X));
    }

    public static PacketMatrix2Float64Neon Multiply(PacketMatrix2Float64Neon left, PacketMatrix2Float64Neon right) => left * right;

    public static PacketMatrix2Float64Neon Multiply(PacketMatrix2Float64Neon matrix, PacketFloat64Neon scalar) => matrix * scalar;

    public static PacketMatrix2Float64Neon Multiply(PacketFloat64Neon scalar, PacketMatrix2Float64Neon matrix) => scalar * matrix;

    public static PacketMatrix2Float64Neon FusedMultiplyAdd(PacketMatrix2Float64Neon left, PacketMatrix2Float64Neon right, PacketMatrix2Float64Neon addend)
    {
        return new(
            new PacketVector2Float64Neon(
                PacketFloat64Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, PacketFloat64Neon.FusedMultiplyAdd(left.Row0.X, right.Row0.X, addend.Row0.X)),
                PacketFloat64Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, PacketFloat64Neon.FusedMultiplyAdd(left.Row0.X, right.Row0.Y, addend.Row0.Y))),
            new PacketVector2Float64Neon(
                PacketFloat64Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, PacketFloat64Neon.FusedMultiplyAdd(left.Row1.X, right.Row0.X, addend.Row1.X)),
                PacketFloat64Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, PacketFloat64Neon.FusedMultiplyAdd(left.Row1.X, right.Row0.Y, addend.Row1.Y))));
    }

    public static PacketVector2Float64Neon FusedMultiplyAdd(PacketMatrix2Float64Neon matrix, PacketVector2Float64Neon vector, PacketVector2Float64Neon addend)
    {
        return new(
            PacketFloat64Neon.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, PacketFloat64Neon.FusedMultiplyAdd(matrix.Row0.X, vector.X, addend.X)),
            PacketFloat64Neon.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, PacketFloat64Neon.FusedMultiplyAdd(matrix.Row1.X, vector.X, addend.Y)));
    }

    public static PacketMatrix2Float64Neon Transpose(PacketMatrix2Float64Neon matrix)
    {
        return new(
            new(matrix.Row0.X, matrix.Row1.X),
            new(matrix.Row0.Y, matrix.Row1.Y));
    }

    public static PacketMatrix2Float64Neon Scale(PacketVector2Float64Neon scale) => Scale(scale.X, scale.Y);

    public static PacketMatrix2Float64Neon Scale(PacketFloat64Neon x, PacketFloat64Neon y)
    {
        PacketFloat64Neon zero = PacketFloat64Neon.Broadcast(0.0);
        return new(
            new(x, zero),
            new(zero, y));
    }

    public static PacketMatrix2Float64Neon operator *(PacketMatrix2Float64Neon left, PacketMatrix2Float64Neon right)
    {
        return new(
            new PacketVector2Float64Neon(
                PacketFloat64Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, left.Row0.X * right.Row0.X),
                PacketFloat64Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, left.Row0.X * right.Row0.Y)),
            new PacketVector2Float64Neon(
                PacketFloat64Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, left.Row1.X * right.Row0.X),
                PacketFloat64Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, left.Row1.X * right.Row0.Y)));
    }

    public static PacketVector2Float64Neon operator *(PacketMatrix2Float64Neon matrix, PacketVector2Float64Neon vector) => Transform(matrix, vector);

    public static PacketMatrix2Float64Neon operator *(PacketMatrix2Float64Neon matrix, PacketFloat64Neon scalar)
    {
        PacketVector2Float64Neon scalarVector = new(scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector);
    }

    public static PacketMatrix2Float64Neon operator *(PacketFloat64Neon scalar, PacketMatrix2Float64Neon matrix) => matrix * scalar;
    public static PacketMatrix2Float64Neon Divide(PacketMatrix2Float64Neon matrix, PacketFloat64Neon scalar) => matrix / scalar;

    public static PacketMatrix2Float64Neon operator /(PacketMatrix2Float64Neon matrix, PacketFloat64Neon scalar) => matrix * PacketFloat64Neon.Reciprocal(scalar);

    public static PacketMatrix2Float64Neon Inverse(PacketMatrix2Float64Neon matrix)
    {
        PacketFloat64Neon invDet = PacketFloat64Neon.Reciprocal(PacketFloat64Neon.FusedMultiplyAdd(matrix.Row0.X, matrix.Row1.Y, -(matrix.Row0.Y * matrix.Row1.X)));
        return new(
            new(matrix.Row1.Y * invDet, -matrix.Row0.Y * invDet),
            new(-matrix.Row1.X * invDet, matrix.Row0.X * invDet));
    }

    public static PacketMatrix2Float64Neon InverseTranspose(PacketMatrix2Float64Neon matrix)
    {
        PacketFloat64Neon invDet = PacketFloat64Neon.Reciprocal(PacketFloat64Neon.FusedMultiplyAdd(matrix.Row0.X, matrix.Row1.Y, -(matrix.Row0.Y * matrix.Row1.X)));
        return new(
            new(matrix.Row1.Y * invDet, -matrix.Row1.X * invDet),
            new(-matrix.Row0.Y * invDet, matrix.Row0.X * invDet));
    }

    public static PacketMatrix2Float64Neon Rotate(PacketFloat64Neon angle)
    {
        var (sin, cos) = PacketFloat64Neon.SinCos(angle);
        return new(
            new(cos, -sin),
            new(sin, cos));
    }

    public static PacketMatrix2Float64NeonMask operator ==(PacketMatrix2Float64Neon left, PacketMatrix2Float64Neon right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1);

    public static PacketMatrix2Float64NeonMask operator !=(PacketMatrix2Float64Neon left, PacketMatrix2Float64Neon right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1);

    public static PacketMatrix2Float64NeonMask operator <(PacketMatrix2Float64Neon left, PacketMatrix2Float64Neon right) => new(
        new PacketVector2Float64NeonMask(left.Row0.X < right.Row0.X, left.Row0.Y < right.Row0.Y),
        new PacketVector2Float64NeonMask(left.Row1.X < right.Row1.X, left.Row1.Y < right.Row1.Y));

    public static PacketMatrix2Float64NeonMask operator >(PacketMatrix2Float64Neon left, PacketMatrix2Float64Neon right) => new(
        new PacketVector2Float64NeonMask(left.Row0.X > right.Row0.X, left.Row0.Y > right.Row0.Y),
        new PacketVector2Float64NeonMask(left.Row1.X > right.Row1.X, left.Row1.Y > right.Row1.Y));

    public static PacketMatrix2Float64NeonMask operator <=(PacketMatrix2Float64Neon left, PacketMatrix2Float64Neon right) => new(
        new PacketVector2Float64NeonMask(left.Row0.X <= right.Row0.X, left.Row0.Y <= right.Row0.Y),
        new PacketVector2Float64NeonMask(left.Row1.X <= right.Row1.X, left.Row1.Y <= right.Row1.Y));

    public static PacketMatrix2Float64NeonMask operator >=(PacketMatrix2Float64Neon left, PacketMatrix2Float64Neon right) => new(
        new PacketVector2Float64NeonMask(left.Row0.X >= right.Row0.X, left.Row0.Y >= right.Row0.Y),
        new PacketVector2Float64NeonMask(left.Row1.X >= right.Row1.X, left.Row1.Y >= right.Row1.Y));

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix2Float64Neon other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1);
}

public readonly struct PacketMatrix2Float64NeonMask :
    ISimdMatrix2Mask<PacketMatrix2Float64NeonMask, PacketVector2Float64NeonMask, PacketFloat64NeonMask>
{
    public PacketMatrix2Float64NeonMask(PacketVector2Float64NeonMask row0, PacketVector2Float64NeonMask row1)
    {
        Row0 = row0;
        Row1 = row1;
    }

    public static int LaneCount => PacketVector2Float64NeonMask.LaneCount;

    public PacketVector2Float64NeonMask Row0 { get; }

    public PacketVector2Float64NeonMask Row1 { get; }

    public static PacketMatrix2Float64NeonMask True => new(PacketVector2Float64NeonMask.True, PacketVector2Float64NeonMask.True);

    public static PacketMatrix2Float64NeonMask False => new(PacketVector2Float64NeonMask.False, PacketVector2Float64NeonMask.False);

    public static PacketMatrix2Float64NeonMask Create(PacketVector2Float64NeonMask row0, PacketVector2Float64NeonMask row1) => new(row0, row1);

    public static PacketMatrix2Float64NeonMask Broadcast(PacketVector2Float64NeonMask value) => new(value, value);

    public static PacketVector2Float64NeonMask All(PacketMatrix2Float64NeonMask value) => value.Row0 & value.Row1;

    public static PacketVector2Float64NeonMask Any(PacketMatrix2Float64NeonMask value) => value.Row0 | value.Row1;

    public static PacketVector2Float64NeonMask None(PacketMatrix2Float64NeonMask value) => ~(value.Row0 | value.Row1);

    public static PacketMatrix2Float64NeonMask AndNot(PacketMatrix2Float64NeonMask left, PacketMatrix2Float64NeonMask right)
    {
        return new(
            PacketVector2Float64NeonMask.AndNot(left.Row0, right.Row0),
            PacketVector2Float64NeonMask.AndNot(left.Row1, right.Row1));
    }

    public static PacketMatrix2Float64NeonMask Select(PacketMatrix2Float64NeonMask mask, PacketMatrix2Float64NeonMask ifTrue, PacketMatrix2Float64NeonMask ifFalse)
    {
        return new(
            PacketVector2Float64NeonMask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Float64NeonMask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1));
    }
    public static PacketMatrix2Float64NeonMask And(PacketMatrix2Float64NeonMask left, PacketMatrix2Float64NeonMask right) => left & right;

    public static PacketMatrix2Float64NeonMask Or(PacketMatrix2Float64NeonMask left, PacketMatrix2Float64NeonMask right) => left | right;

    public static PacketMatrix2Float64NeonMask Xor(PacketMatrix2Float64NeonMask left, PacketMatrix2Float64NeonMask right) => left ^ right;

    public static PacketMatrix2Float64NeonMask Not(PacketMatrix2Float64NeonMask value) => ~value;


    public static PacketMatrix2Float64NeonMask operator &(PacketMatrix2Float64NeonMask left, PacketMatrix2Float64NeonMask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1);

    public static PacketMatrix2Float64NeonMask operator |(PacketMatrix2Float64NeonMask left, PacketMatrix2Float64NeonMask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1);

    public static PacketMatrix2Float64NeonMask operator ^(PacketMatrix2Float64NeonMask left, PacketMatrix2Float64NeonMask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1);

    public static PacketMatrix2Float64NeonMask operator ~(PacketMatrix2Float64NeonMask value) => new(~value.Row0, ~value.Row1);

    public static PacketMatrix2Float64NeonMask operator ==(PacketMatrix2Float64NeonMask left, PacketMatrix2Float64NeonMask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1);

    public static PacketMatrix2Float64NeonMask operator !=(PacketMatrix2Float64NeonMask left, PacketMatrix2Float64NeonMask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix2Float64NeonMask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1);
}
