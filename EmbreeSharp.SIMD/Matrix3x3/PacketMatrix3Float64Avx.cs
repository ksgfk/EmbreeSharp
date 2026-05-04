namespace EmbreeSharp.SIMD;

public readonly struct PacketMatrix3Float64Avx :
    ISimdFloatingPointMatrix3<PacketMatrix3Float64Avx, PacketVector3Float64Avx, PacketFloat64Avx, double, PacketMatrix3Float64AvxMask, PacketVector3Float64AvxMask, PacketFloat64AvxMask>
{
    public PacketMatrix3Float64Avx(PacketVector3Float64Avx row0, PacketVector3Float64Avx row1, PacketVector3Float64Avx row2)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
    }

    public static int LaneCount => PacketVector3Float64Avx.LaneCount;

    public PacketVector3Float64Avx Row0 { get; }

    public PacketVector3Float64Avx Row1 { get; }

    public PacketVector3Float64Avx Row2 { get; }

    public static PacketMatrix3Float64Avx Identity
    {
        get
        {
            PacketFloat64Avx zero = PacketFloat64Avx.Broadcast(0d);
            PacketFloat64Avx one = PacketFloat64Avx.Broadcast(1d);
            return new(
                new PacketVector3Float64Avx(one, zero, zero),
                new PacketVector3Float64Avx(zero, one, zero),
                new PacketVector3Float64Avx(zero, zero, one));
        }
    }

    public static PacketMatrix3Float64Avx Create(PacketVector3Float64Avx row0, PacketVector3Float64Avx row1, PacketVector3Float64Avx row2) => new(row0, row1, row2);

    public static PacketMatrix3Float64Avx Broadcast(double value)
    {
        PacketVector3Float64Avx row = PacketVector3Float64Avx.Broadcast(value);
        return new(row, row, row);
    }

    public static PacketMatrix3Float64Avx Select(PacketMatrix3Float64AvxMask mask, PacketMatrix3Float64Avx ifTrue, PacketMatrix3Float64Avx ifFalse)
    {
        return new(
            PacketVector3Float64Avx.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Float64Avx.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Float64Avx.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketMatrix3Float64Avx Select(PacketVector3Float64AvxMask mask, PacketMatrix3Float64Avx ifTrue, PacketMatrix3Float64Avx ifFalse)
    {
        return new(
            PacketVector3Float64Avx.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Float64Avx.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Float64Avx.Select(mask, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketMatrix3Float64Avx Select(PacketFloat64AvxMask mask, PacketMatrix3Float64Avx ifTrue, PacketMatrix3Float64Avx ifFalse)
    {
        return new(
            PacketVector3Float64Avx.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Float64Avx.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Float64Avx.Select(mask, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketVector3Float64Avx Transform(PacketMatrix3Float64Avx matrix, PacketVector3Float64Avx vector)
    {
        return new(
            PacketVector3Float64Avx.Dot(matrix.Row0, vector),
            PacketVector3Float64Avx.Dot(matrix.Row1, vector),
            PacketVector3Float64Avx.Dot(matrix.Row2, vector));
    }

    public static PacketMatrix3Float64Avx Multiply(PacketMatrix3Float64Avx left, PacketMatrix3Float64Avx right) => left * right;

    public static PacketMatrix3Float64Avx Multiply(PacketMatrix3Float64Avx matrix, PacketFloat64Avx scalar) => matrix * scalar;

    public static PacketMatrix3Float64Avx Multiply(PacketFloat64Avx scalar, PacketMatrix3Float64Avx matrix) => scalar * matrix;

    public static PacketMatrix3Float64Avx FusedMultiplyAdd(PacketMatrix3Float64Avx left, PacketMatrix3Float64Avx right, PacketMatrix3Float64Avx addend)
    {
        return new(
            new PacketVector3Float64Avx(
                PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.X, right.Row0.X, addend.Row0.X))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.X, right.Row0.Y, addend.Row0.Y))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.X, right.Row0.Z, addend.Row0.Z)))),
            new PacketVector3Float64Avx(
                PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.X, right.Row0.X, addend.Row1.X))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.X, right.Row0.Y, addend.Row1.Y))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.X, right.Row0.Z, addend.Row1.Z)))),
            new PacketVector3Float64Avx(
                PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.X, right.Row0.X, addend.Row2.X))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.X, right.Row0.Y, addend.Row2.Y))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.X, right.Row0.Z, addend.Row2.Z)))));
    }

    public static PacketVector3Float64Avx FusedMultiplyAdd(PacketMatrix3Float64Avx matrix, PacketVector3Float64Avx vector, PacketVector3Float64Avx addend)
    {
        return new(
            PacketFloat64Avx.FusedMultiplyAdd(matrix.Row0.Z, vector.Z, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row0.X, vector.X, addend.X))),
            PacketFloat64Avx.FusedMultiplyAdd(matrix.Row1.Z, vector.Z, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row1.X, vector.X, addend.Y))),
            PacketFloat64Avx.FusedMultiplyAdd(matrix.Row2.Z, vector.Z, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row2.Y, vector.Y, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row2.X, vector.X, addend.Z))));
    }

    public static PacketMatrix3Float64Avx Transpose(PacketMatrix3Float64Avx matrix)
    {
        return new(
            new(matrix.Row0.X, matrix.Row1.X, matrix.Row2.X),
            new(matrix.Row0.Y, matrix.Row1.Y, matrix.Row2.Y),
            new(matrix.Row0.Z, matrix.Row1.Z, matrix.Row2.Z));
    }

    public static PacketMatrix3Float64Avx Scale(PacketVector3Float64Avx scale) => Scale(scale.X, scale.Y);

    public static PacketMatrix3Float64Avx Scale(PacketFloat64Avx x, PacketFloat64Avx y)
    {
        PacketFloat64Avx zero = PacketFloat64Avx.Broadcast(0d);
        PacketFloat64Avx one = PacketFloat64Avx.Broadcast(1d);
        return new(
            new(x, zero, zero),
            new(zero, y, zero),
            new(zero, zero, one));
    }

    public static PacketMatrix3Float64Avx Translate(PacketVector3Float64Avx translation) => Translate(translation.X, translation.Y);

    public static PacketMatrix3Float64Avx Translate(PacketFloat64Avx x, PacketFloat64Avx y)
    {
        PacketFloat64Avx zero = PacketFloat64Avx.Broadcast(0d);
        PacketFloat64Avx one = PacketFloat64Avx.Broadcast(1d);
        return new(
            new(one, zero, x),
            new(zero, one, y),
            new(zero, zero, one));
    }

    public static PacketMatrix3Float64Avx operator *(PacketMatrix3Float64Avx left, PacketMatrix3Float64Avx right)
    {
        return new(
            new PacketVector3Float64Avx(
                PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, left.Row0.X * right.Row0.X)),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, left.Row0.X * right.Row0.Y)),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, left.Row0.X * right.Row0.Z))),
            new PacketVector3Float64Avx(
                PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, left.Row1.X * right.Row0.X)),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, left.Row1.X * right.Row0.Y)),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, left.Row1.X * right.Row0.Z))),
            new PacketVector3Float64Avx(
                PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, left.Row2.X * right.Row0.X)),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, left.Row2.X * right.Row0.Y)),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, left.Row2.X * right.Row0.Z))));
    }

    public static PacketVector3Float64Avx operator *(PacketMatrix3Float64Avx matrix, PacketVector3Float64Avx vector) => Transform(matrix, vector);

    public static PacketMatrix3Float64Avx operator *(PacketMatrix3Float64Avx matrix, PacketFloat64Avx scalar)
    {
        PacketVector3Float64Avx scalarVector = new(scalar, scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector, matrix.Row2 * scalarVector);
    }

    public static PacketMatrix3Float64Avx operator *(PacketFloat64Avx scalar, PacketMatrix3Float64Avx matrix) => matrix * scalar;

    public static PacketMatrix3Float64Avx Divide(PacketMatrix3Float64Avx matrix, PacketFloat64Avx scalar) => matrix / scalar;

    public static PacketMatrix3Float64Avx operator /(PacketMatrix3Float64Avx matrix, PacketFloat64Avx scalar) => matrix * PacketFloat64Avx.Reciprocal(scalar);

    public static PacketMatrix3Float64Avx InverseTranspose(PacketMatrix3Float64Avx matrix)
    {
        PacketVector3Float64Avx col0 = PacketVector3Float64Avx.Cross(matrix.Row1, matrix.Row2);
        PacketVector3Float64Avx col1 = PacketVector3Float64Avx.Cross(matrix.Row2, matrix.Row0);
        PacketVector3Float64Avx col2 = PacketVector3Float64Avx.Cross(matrix.Row0, matrix.Row1);
        PacketFloat64Avx invDet = PacketFloat64Avx.Reciprocal(PacketVector3Float64Avx.Dot(matrix.Row0, col0));
        PacketVector3Float64Avx invDetVector = new(invDet, invDet, invDet);
        return new(col0 * invDetVector, col1 * invDetVector, col2 * invDetVector);
    }

    public static PacketMatrix3Float64Avx Inverse(PacketMatrix3Float64Avx matrix) => Transpose(InverseTranspose(matrix));

    public static PacketMatrix3Float64Avx Rotate(PacketFloat64Avx angle)
    {
        var (sin, cos) = PacketFloat64Avx.SinCos(angle);
        PacketFloat64Avx zero = PacketFloat64Avx.Broadcast(0d);
        PacketFloat64Avx one = PacketFloat64Avx.Broadcast(1d);
        return new(
            new(cos, -sin, zero),
            new(sin, cos, zero),
            new(zero, zero, one));
    }

    public static PacketMatrix3Float64AvxMask operator ==(PacketMatrix3Float64Avx left, PacketMatrix3Float64Avx right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2);

    public static PacketMatrix3Float64AvxMask operator !=(PacketMatrix3Float64Avx left, PacketMatrix3Float64Avx right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix3Float64Avx other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2);
}

public readonly struct PacketMatrix3Float64AvxMask :
    ISimdMatrix3Mask<PacketMatrix3Float64AvxMask, PacketVector3Float64AvxMask, PacketFloat64AvxMask>
{
    public PacketMatrix3Float64AvxMask(PacketVector3Float64AvxMask row0, PacketVector3Float64AvxMask row1, PacketVector3Float64AvxMask row2)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
    }

    public static int LaneCount => PacketVector3Float64AvxMask.LaneCount;

    public PacketVector3Float64AvxMask Row0 { get; }

    public PacketVector3Float64AvxMask Row1 { get; }

    public PacketVector3Float64AvxMask Row2 { get; }

    public static PacketMatrix3Float64AvxMask True => new(PacketVector3Float64AvxMask.True, PacketVector3Float64AvxMask.True, PacketVector3Float64AvxMask.True);

    public static PacketMatrix3Float64AvxMask False => new(PacketVector3Float64AvxMask.False, PacketVector3Float64AvxMask.False, PacketVector3Float64AvxMask.False);

    public static PacketMatrix3Float64AvxMask Create(PacketVector3Float64AvxMask row0, PacketVector3Float64AvxMask row1, PacketVector3Float64AvxMask row2) => new(row0, row1, row2);

    public static PacketMatrix3Float64AvxMask Broadcast(PacketVector3Float64AvxMask value) => new(value, value, value);

    public static PacketVector3Float64AvxMask All(PacketMatrix3Float64AvxMask value) => value.Row0 & value.Row1 & value.Row2;

    public static PacketVector3Float64AvxMask Any(PacketMatrix3Float64AvxMask value) => value.Row0 | value.Row1 | value.Row2;

    public static PacketVector3Float64AvxMask None(PacketMatrix3Float64AvxMask value) => ~(value.Row0 | value.Row1 | value.Row2);

    public static PacketMatrix3Float64AvxMask AndNot(PacketMatrix3Float64AvxMask left, PacketMatrix3Float64AvxMask right)
    {
        return new(
            PacketVector3Float64AvxMask.AndNot(left.Row0, right.Row0),
            PacketVector3Float64AvxMask.AndNot(left.Row1, right.Row1),
            PacketVector3Float64AvxMask.AndNot(left.Row2, right.Row2));
    }

    public static PacketMatrix3Float64AvxMask Select(PacketMatrix3Float64AvxMask mask, PacketMatrix3Float64AvxMask ifTrue, PacketMatrix3Float64AvxMask ifFalse)
    {
        return new(
            PacketVector3Float64AvxMask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Float64AvxMask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Float64AvxMask.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2));
    }
    public static PacketMatrix3Float64AvxMask And(PacketMatrix3Float64AvxMask left, PacketMatrix3Float64AvxMask right) => left & right;

    public static PacketMatrix3Float64AvxMask Or(PacketMatrix3Float64AvxMask left, PacketMatrix3Float64AvxMask right) => left | right;

    public static PacketMatrix3Float64AvxMask Xor(PacketMatrix3Float64AvxMask left, PacketMatrix3Float64AvxMask right) => left ^ right;

    public static PacketMatrix3Float64AvxMask Not(PacketMatrix3Float64AvxMask value) => ~value;


    public static PacketMatrix3Float64AvxMask operator &(PacketMatrix3Float64AvxMask left, PacketMatrix3Float64AvxMask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1, left.Row2 & right.Row2);

    public static PacketMatrix3Float64AvxMask operator |(PacketMatrix3Float64AvxMask left, PacketMatrix3Float64AvxMask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1, left.Row2 | right.Row2);

    public static PacketMatrix3Float64AvxMask operator ^(PacketMatrix3Float64AvxMask left, PacketMatrix3Float64AvxMask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1, left.Row2 ^ right.Row2);

    public static PacketMatrix3Float64AvxMask operator ~(PacketMatrix3Float64AvxMask value) => new(~value.Row0, ~value.Row1, ~value.Row2);

    public static PacketMatrix3Float64AvxMask operator ==(PacketMatrix3Float64AvxMask left, PacketMatrix3Float64AvxMask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2);

    public static PacketMatrix3Float64AvxMask operator !=(PacketMatrix3Float64AvxMask left, PacketMatrix3Float64AvxMask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix3Float64AvxMask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2);
}
