namespace EmbreeSharp.SIMD;

public readonly struct PacketMatrix3Float64Neon :
    ISimdFloatingPointMatrix3<PacketMatrix3Float64Neon, PacketVector3Float64Neon, PacketFloat64Neon, double, PacketMatrix3Float64NeonMask, PacketVector3Float64NeonMask, PacketFloat64NeonMask>
{
    public PacketMatrix3Float64Neon(PacketVector3Float64Neon row0, PacketVector3Float64Neon row1, PacketVector3Float64Neon row2)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
    }

    public static int LaneCount => PacketVector3Float64Neon.LaneCount;

    public PacketVector3Float64Neon Row0 { get; }

    public PacketVector3Float64Neon Row1 { get; }

    public PacketVector3Float64Neon Row2 { get; }

    public static PacketMatrix3Float64Neon Identity
    {
        get
        {
            PacketFloat64Neon zero = PacketFloat64Neon.Broadcast(0d);
            PacketFloat64Neon one = PacketFloat64Neon.Broadcast(1d);
            return new(
                new PacketVector3Float64Neon(one, zero, zero),
                new PacketVector3Float64Neon(zero, one, zero),
                new PacketVector3Float64Neon(zero, zero, one));
        }
    }

    public static PacketMatrix3Float64Neon Create(PacketVector3Float64Neon row0, PacketVector3Float64Neon row1, PacketVector3Float64Neon row2) => new(row0, row1, row2);

    public static PacketMatrix3Float64Neon Broadcast(double value)
    {
        PacketVector3Float64Neon row = PacketVector3Float64Neon.Broadcast(value);
        return new(row, row, row);
    }

    public static PacketMatrix3Float64Neon Select(PacketMatrix3Float64NeonMask mask, PacketMatrix3Float64Neon ifTrue, PacketMatrix3Float64Neon ifFalse)
    {
        return new(
            PacketVector3Float64Neon.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Float64Neon.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Float64Neon.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketMatrix3Float64Neon Select(PacketVector3Float64NeonMask mask, PacketMatrix3Float64Neon ifTrue, PacketMatrix3Float64Neon ifFalse)
    {
        return new(
            PacketVector3Float64Neon.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Float64Neon.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Float64Neon.Select(mask, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketMatrix3Float64Neon Select(PacketFloat64NeonMask mask, PacketMatrix3Float64Neon ifTrue, PacketMatrix3Float64Neon ifFalse)
    {
        return new(
            PacketVector3Float64Neon.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Float64Neon.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Float64Neon.Select(mask, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketVector3Float64Neon Transform(PacketMatrix3Float64Neon matrix, PacketVector3Float64Neon vector)
    {
        return new(
            PacketVector3Float64Neon.Dot(matrix.Row0, vector),
            PacketVector3Float64Neon.Dot(matrix.Row1, vector),
            PacketVector3Float64Neon.Dot(matrix.Row2, vector));
    }

    public static PacketMatrix3Float64Neon Multiply(PacketMatrix3Float64Neon left, PacketMatrix3Float64Neon right) => left * right;

    public static PacketMatrix3Float64Neon Multiply(PacketMatrix3Float64Neon matrix, PacketFloat64Neon scalar) => matrix * scalar;

    public static PacketMatrix3Float64Neon Multiply(PacketFloat64Neon scalar, PacketMatrix3Float64Neon matrix) => scalar * matrix;

    public static PacketMatrix3Float64Neon FusedMultiplyAdd(PacketMatrix3Float64Neon left, PacketMatrix3Float64Neon right, PacketMatrix3Float64Neon addend)
    {
        return new(
            new PacketVector3Float64Neon(
                PacketFloat64Neon.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, PacketFloat64Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, PacketFloat64Neon.FusedMultiplyAdd(left.Row0.X, right.Row0.X, addend.Row0.X))),
                PacketFloat64Neon.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, PacketFloat64Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, PacketFloat64Neon.FusedMultiplyAdd(left.Row0.X, right.Row0.Y, addend.Row0.Y))),
                PacketFloat64Neon.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, PacketFloat64Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, PacketFloat64Neon.FusedMultiplyAdd(left.Row0.X, right.Row0.Z, addend.Row0.Z)))),
            new PacketVector3Float64Neon(
                PacketFloat64Neon.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, PacketFloat64Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, PacketFloat64Neon.FusedMultiplyAdd(left.Row1.X, right.Row0.X, addend.Row1.X))),
                PacketFloat64Neon.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, PacketFloat64Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, PacketFloat64Neon.FusedMultiplyAdd(left.Row1.X, right.Row0.Y, addend.Row1.Y))),
                PacketFloat64Neon.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, PacketFloat64Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, PacketFloat64Neon.FusedMultiplyAdd(left.Row1.X, right.Row0.Z, addend.Row1.Z)))),
            new PacketVector3Float64Neon(
                PacketFloat64Neon.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, PacketFloat64Neon.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, PacketFloat64Neon.FusedMultiplyAdd(left.Row2.X, right.Row0.X, addend.Row2.X))),
                PacketFloat64Neon.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, PacketFloat64Neon.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, PacketFloat64Neon.FusedMultiplyAdd(left.Row2.X, right.Row0.Y, addend.Row2.Y))),
                PacketFloat64Neon.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, PacketFloat64Neon.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, PacketFloat64Neon.FusedMultiplyAdd(left.Row2.X, right.Row0.Z, addend.Row2.Z)))));
    }

    public static PacketVector3Float64Neon FusedMultiplyAdd(PacketMatrix3Float64Neon matrix, PacketVector3Float64Neon vector, PacketVector3Float64Neon addend)
    {
        return new(
            PacketFloat64Neon.FusedMultiplyAdd(matrix.Row0.Z, vector.Z, PacketFloat64Neon.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, PacketFloat64Neon.FusedMultiplyAdd(matrix.Row0.X, vector.X, addend.X))),
            PacketFloat64Neon.FusedMultiplyAdd(matrix.Row1.Z, vector.Z, PacketFloat64Neon.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, PacketFloat64Neon.FusedMultiplyAdd(matrix.Row1.X, vector.X, addend.Y))),
            PacketFloat64Neon.FusedMultiplyAdd(matrix.Row2.Z, vector.Z, PacketFloat64Neon.FusedMultiplyAdd(matrix.Row2.Y, vector.Y, PacketFloat64Neon.FusedMultiplyAdd(matrix.Row2.X, vector.X, addend.Z))));
    }

    public static PacketMatrix3Float64Neon Transpose(PacketMatrix3Float64Neon matrix)
    {
        return new(
            new(matrix.Row0.X, matrix.Row1.X, matrix.Row2.X),
            new(matrix.Row0.Y, matrix.Row1.Y, matrix.Row2.Y),
            new(matrix.Row0.Z, matrix.Row1.Z, matrix.Row2.Z));
    }

    public static PacketMatrix3Float64Neon Scale(PacketVector3Float64Neon scale) => Scale(scale.X, scale.Y);

    public static PacketMatrix3Float64Neon Scale(PacketFloat64Neon x, PacketFloat64Neon y)
    {
        PacketFloat64Neon zero = PacketFloat64Neon.Broadcast(0d);
        PacketFloat64Neon one = PacketFloat64Neon.Broadcast(1d);
        return new(
            new(x, zero, zero),
            new(zero, y, zero),
            new(zero, zero, one));
    }

    public static PacketMatrix3Float64Neon Translate(PacketVector3Float64Neon translation) => Translate(translation.X, translation.Y);

    public static PacketMatrix3Float64Neon Translate(PacketFloat64Neon x, PacketFloat64Neon y)
    {
        PacketFloat64Neon zero = PacketFloat64Neon.Broadcast(0d);
        PacketFloat64Neon one = PacketFloat64Neon.Broadcast(1d);
        return new(
            new(one, zero, x),
            new(zero, one, y),
            new(zero, zero, one));
    }

    public static PacketMatrix3Float64Neon operator *(PacketMatrix3Float64Neon left, PacketMatrix3Float64Neon right)
    {
        return new(
            new PacketVector3Float64Neon(
                PacketFloat64Neon.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, PacketFloat64Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, left.Row0.X * right.Row0.X)),
                PacketFloat64Neon.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, PacketFloat64Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, left.Row0.X * right.Row0.Y)),
                PacketFloat64Neon.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, PacketFloat64Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, left.Row0.X * right.Row0.Z))),
            new PacketVector3Float64Neon(
                PacketFloat64Neon.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, PacketFloat64Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, left.Row1.X * right.Row0.X)),
                PacketFloat64Neon.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, PacketFloat64Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, left.Row1.X * right.Row0.Y)),
                PacketFloat64Neon.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, PacketFloat64Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, left.Row1.X * right.Row0.Z))),
            new PacketVector3Float64Neon(
                PacketFloat64Neon.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, PacketFloat64Neon.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, left.Row2.X * right.Row0.X)),
                PacketFloat64Neon.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, PacketFloat64Neon.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, left.Row2.X * right.Row0.Y)),
                PacketFloat64Neon.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, PacketFloat64Neon.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, left.Row2.X * right.Row0.Z))));
    }

    public static PacketVector3Float64Neon operator *(PacketMatrix3Float64Neon matrix, PacketVector3Float64Neon vector) => Transform(matrix, vector);

    public static PacketMatrix3Float64Neon operator *(PacketMatrix3Float64Neon matrix, PacketFloat64Neon scalar)
    {
        PacketVector3Float64Neon scalarVector = new(scalar, scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector, matrix.Row2 * scalarVector);
    }

    public static PacketMatrix3Float64Neon operator *(PacketFloat64Neon scalar, PacketMatrix3Float64Neon matrix) => matrix * scalar;

    public static PacketMatrix3Float64Neon Divide(PacketMatrix3Float64Neon matrix, PacketFloat64Neon scalar) => matrix / scalar;

    public static PacketMatrix3Float64Neon operator /(PacketMatrix3Float64Neon matrix, PacketFloat64Neon scalar) => matrix * PacketFloat64Neon.Reciprocal(scalar);

    public static PacketMatrix3Float64Neon InverseTranspose(PacketMatrix3Float64Neon matrix)
    {
        PacketVector3Float64Neon col0 = PacketVector3Float64Neon.Cross(matrix.Row1, matrix.Row2);
        PacketVector3Float64Neon col1 = PacketVector3Float64Neon.Cross(matrix.Row2, matrix.Row0);
        PacketVector3Float64Neon col2 = PacketVector3Float64Neon.Cross(matrix.Row0, matrix.Row1);
        PacketFloat64Neon invDet = PacketFloat64Neon.Reciprocal(PacketVector3Float64Neon.Dot(matrix.Row0, col0));
        PacketVector3Float64Neon invDetVector = new(invDet, invDet, invDet);
        return new(col0 * invDetVector, col1 * invDetVector, col2 * invDetVector);
    }

    public static PacketMatrix3Float64Neon Inverse(PacketMatrix3Float64Neon matrix) => Transpose(InverseTranspose(matrix));

    public static PacketMatrix3Float64Neon Rotate(PacketFloat64Neon angle)
    {
        var (sin, cos) = PacketFloat64Neon.SinCos(angle);
        PacketFloat64Neon zero = PacketFloat64Neon.Broadcast(0d);
        PacketFloat64Neon one = PacketFloat64Neon.Broadcast(1d);
        return new(
            new(cos, -sin, zero),
            new(sin, cos, zero),
            new(zero, zero, one));
    }

    public static PacketMatrix3Float64NeonMask operator ==(PacketMatrix3Float64Neon left, PacketMatrix3Float64Neon right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2);

    public static PacketMatrix3Float64NeonMask operator !=(PacketMatrix3Float64Neon left, PacketMatrix3Float64Neon right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix3Float64Neon other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2);
}

public readonly struct PacketMatrix3Float64NeonMask :
    ISimdMatrix3Mask<PacketMatrix3Float64NeonMask, PacketVector3Float64NeonMask, PacketFloat64NeonMask>
{
    public PacketMatrix3Float64NeonMask(PacketVector3Float64NeonMask row0, PacketVector3Float64NeonMask row1, PacketVector3Float64NeonMask row2)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
    }

    public static int LaneCount => PacketVector3Float64NeonMask.LaneCount;

    public PacketVector3Float64NeonMask Row0 { get; }

    public PacketVector3Float64NeonMask Row1 { get; }

    public PacketVector3Float64NeonMask Row2 { get; }

    public static PacketMatrix3Float64NeonMask True => new(PacketVector3Float64NeonMask.True, PacketVector3Float64NeonMask.True, PacketVector3Float64NeonMask.True);

    public static PacketMatrix3Float64NeonMask False => new(PacketVector3Float64NeonMask.False, PacketVector3Float64NeonMask.False, PacketVector3Float64NeonMask.False);

    public static PacketMatrix3Float64NeonMask Create(PacketVector3Float64NeonMask row0, PacketVector3Float64NeonMask row1, PacketVector3Float64NeonMask row2) => new(row0, row1, row2);

    public static PacketMatrix3Float64NeonMask Broadcast(PacketVector3Float64NeonMask value) => new(value, value, value);

    public static PacketVector3Float64NeonMask All(PacketMatrix3Float64NeonMask value) => value.Row0 & value.Row1 & value.Row2;

    public static PacketVector3Float64NeonMask Any(PacketMatrix3Float64NeonMask value) => value.Row0 | value.Row1 | value.Row2;

    public static PacketVector3Float64NeonMask None(PacketMatrix3Float64NeonMask value) => ~(value.Row0 | value.Row1 | value.Row2);

    public static PacketMatrix3Float64NeonMask AndNot(PacketMatrix3Float64NeonMask left, PacketMatrix3Float64NeonMask right)
    {
        return new(
            PacketVector3Float64NeonMask.AndNot(left.Row0, right.Row0),
            PacketVector3Float64NeonMask.AndNot(left.Row1, right.Row1),
            PacketVector3Float64NeonMask.AndNot(left.Row2, right.Row2));
    }

    public static PacketMatrix3Float64NeonMask Select(PacketMatrix3Float64NeonMask mask, PacketMatrix3Float64NeonMask ifTrue, PacketMatrix3Float64NeonMask ifFalse)
    {
        return new(
            PacketVector3Float64NeonMask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Float64NeonMask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Float64NeonMask.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2));
    }
    public static PacketMatrix3Float64NeonMask And(PacketMatrix3Float64NeonMask left, PacketMatrix3Float64NeonMask right) => left & right;

    public static PacketMatrix3Float64NeonMask Or(PacketMatrix3Float64NeonMask left, PacketMatrix3Float64NeonMask right) => left | right;

    public static PacketMatrix3Float64NeonMask Xor(PacketMatrix3Float64NeonMask left, PacketMatrix3Float64NeonMask right) => left ^ right;

    public static PacketMatrix3Float64NeonMask Not(PacketMatrix3Float64NeonMask value) => ~value;


    public static PacketMatrix3Float64NeonMask operator &(PacketMatrix3Float64NeonMask left, PacketMatrix3Float64NeonMask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1, left.Row2 & right.Row2);

    public static PacketMatrix3Float64NeonMask operator |(PacketMatrix3Float64NeonMask left, PacketMatrix3Float64NeonMask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1, left.Row2 | right.Row2);

    public static PacketMatrix3Float64NeonMask operator ^(PacketMatrix3Float64NeonMask left, PacketMatrix3Float64NeonMask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1, left.Row2 ^ right.Row2);

    public static PacketMatrix3Float64NeonMask operator ~(PacketMatrix3Float64NeonMask value) => new(~value.Row0, ~value.Row1, ~value.Row2);

    public static PacketMatrix3Float64NeonMask operator ==(PacketMatrix3Float64NeonMask left, PacketMatrix3Float64NeonMask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2);

    public static PacketMatrix3Float64NeonMask operator !=(PacketMatrix3Float64NeonMask left, PacketMatrix3Float64NeonMask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix3Float64NeonMask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2);
}
