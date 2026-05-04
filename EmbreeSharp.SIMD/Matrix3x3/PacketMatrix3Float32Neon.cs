namespace EmbreeSharp.SIMD;

public readonly struct PacketMatrix3Float32Neon :
    ISimdFloatingPointMatrix3<PacketMatrix3Float32Neon, PacketVector3Float32Neon, PacketFloat32Neon, float, PacketMatrix3Float32NeonMask, PacketVector3Float32NeonMask, PacketFloat32NeonMask>
{
    public PacketMatrix3Float32Neon(PacketVector3Float32Neon row0, PacketVector3Float32Neon row1, PacketVector3Float32Neon row2)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
    }

    public static int LaneCount => PacketVector3Float32Neon.LaneCount;

    public PacketVector3Float32Neon Row0 { get; }

    public PacketVector3Float32Neon Row1 { get; }

    public PacketVector3Float32Neon Row2 { get; }

    public static PacketMatrix3Float32Neon Identity
    {
        get
        {
            PacketFloat32Neon zero = PacketFloat32Neon.Broadcast(0f);
            PacketFloat32Neon one = PacketFloat32Neon.Broadcast(1f);
            return new(
                new PacketVector3Float32Neon(one, zero, zero),
                new PacketVector3Float32Neon(zero, one, zero),
                new PacketVector3Float32Neon(zero, zero, one));
        }
    }

    public static PacketMatrix3Float32Neon Create(PacketVector3Float32Neon row0, PacketVector3Float32Neon row1, PacketVector3Float32Neon row2) => new(row0, row1, row2);

    public static PacketMatrix3Float32Neon Broadcast(float value)
    {
        PacketVector3Float32Neon row = PacketVector3Float32Neon.Broadcast(value);
        return new(row, row, row);
    }

    public static PacketMatrix3Float32Neon Select(PacketMatrix3Float32NeonMask mask, PacketMatrix3Float32Neon ifTrue, PacketMatrix3Float32Neon ifFalse)
    {
        return new(
            PacketVector3Float32Neon.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Float32Neon.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Float32Neon.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketMatrix3Float32Neon Select(PacketVector3Float32NeonMask mask, PacketMatrix3Float32Neon ifTrue, PacketMatrix3Float32Neon ifFalse)
    {
        return new(
            PacketVector3Float32Neon.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Float32Neon.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Float32Neon.Select(mask, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketMatrix3Float32Neon Select(PacketFloat32NeonMask mask, PacketMatrix3Float32Neon ifTrue, PacketMatrix3Float32Neon ifFalse)
    {
        return new(
            PacketVector3Float32Neon.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Float32Neon.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Float32Neon.Select(mask, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketVector3Float32Neon Transform(PacketMatrix3Float32Neon matrix, PacketVector3Float32Neon vector)
    {
        return new(
            PacketVector3Float32Neon.Dot(matrix.Row0, vector),
            PacketVector3Float32Neon.Dot(matrix.Row1, vector),
            PacketVector3Float32Neon.Dot(matrix.Row2, vector));
    }

    public static PacketMatrix3Float32Neon Multiply(PacketMatrix3Float32Neon left, PacketMatrix3Float32Neon right) => left * right;

    public static PacketMatrix3Float32Neon Multiply(PacketMatrix3Float32Neon matrix, PacketFloat32Neon scalar) => matrix * scalar;

    public static PacketMatrix3Float32Neon Multiply(PacketFloat32Neon scalar, PacketMatrix3Float32Neon matrix) => scalar * matrix;

    public static PacketMatrix3Float32Neon FusedMultiplyAdd(PacketMatrix3Float32Neon left, PacketMatrix3Float32Neon right, PacketMatrix3Float32Neon addend)
    {
        return new(
            new PacketVector3Float32Neon(
                PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.X, right.Row0.X, addend.Row0.X))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.X, right.Row0.Y, addend.Row0.Y))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.X, right.Row0.Z, addend.Row0.Z)))),
            new PacketVector3Float32Neon(
                PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.X, right.Row0.X, addend.Row1.X))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.X, right.Row0.Y, addend.Row1.Y))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.X, right.Row0.Z, addend.Row1.Z)))),
            new PacketVector3Float32Neon(
                PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.X, right.Row0.X, addend.Row2.X))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.X, right.Row0.Y, addend.Row2.Y))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.X, right.Row0.Z, addend.Row2.Z)))));
    }

    public static PacketVector3Float32Neon FusedMultiplyAdd(PacketMatrix3Float32Neon matrix, PacketVector3Float32Neon vector, PacketVector3Float32Neon addend)
    {
        return new(
            PacketFloat32Neon.FusedMultiplyAdd(matrix.Row0.Z, vector.Z, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row0.X, vector.X, addend.X))),
            PacketFloat32Neon.FusedMultiplyAdd(matrix.Row1.Z, vector.Z, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row1.X, vector.X, addend.Y))),
            PacketFloat32Neon.FusedMultiplyAdd(matrix.Row2.Z, vector.Z, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row2.Y, vector.Y, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row2.X, vector.X, addend.Z))));
    }

    public static PacketMatrix3Float32Neon Transpose(PacketMatrix3Float32Neon matrix)
    {
        return new(
            new(matrix.Row0.X, matrix.Row1.X, matrix.Row2.X),
            new(matrix.Row0.Y, matrix.Row1.Y, matrix.Row2.Y),
            new(matrix.Row0.Z, matrix.Row1.Z, matrix.Row2.Z));
    }

    public static PacketMatrix3Float32Neon Scale(PacketVector3Float32Neon scale) => Scale(scale.X, scale.Y);

    public static PacketMatrix3Float32Neon Scale(PacketFloat32Neon x, PacketFloat32Neon y)
    {
        PacketFloat32Neon zero = PacketFloat32Neon.Broadcast(0f);
        PacketFloat32Neon one = PacketFloat32Neon.Broadcast(1f);
        return new(
            new(x, zero, zero),
            new(zero, y, zero),
            new(zero, zero, one));
    }

    public static PacketMatrix3Float32Neon Translate(PacketVector3Float32Neon translation) => Translate(translation.X, translation.Y);

    public static PacketMatrix3Float32Neon Translate(PacketFloat32Neon x, PacketFloat32Neon y)
    {
        PacketFloat32Neon zero = PacketFloat32Neon.Broadcast(0f);
        PacketFloat32Neon one = PacketFloat32Neon.Broadcast(1f);
        return new(
            new(one, zero, x),
            new(zero, one, y),
            new(zero, zero, one));
    }

    public static PacketMatrix3Float32Neon operator *(PacketMatrix3Float32Neon left, PacketMatrix3Float32Neon right)
    {
        return new(
            new PacketVector3Float32Neon(
                PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, left.Row0.X * right.Row0.X)),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, left.Row0.X * right.Row0.Y)),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, left.Row0.X * right.Row0.Z))),
            new PacketVector3Float32Neon(
                PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, left.Row1.X * right.Row0.X)),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, left.Row1.X * right.Row0.Y)),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, left.Row1.X * right.Row0.Z))),
            new PacketVector3Float32Neon(
                PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, left.Row2.X * right.Row0.X)),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, left.Row2.X * right.Row0.Y)),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, left.Row2.X * right.Row0.Z))));
    }

    public static PacketVector3Float32Neon operator *(PacketMatrix3Float32Neon matrix, PacketVector3Float32Neon vector) => Transform(matrix, vector);

    public static PacketMatrix3Float32Neon operator *(PacketMatrix3Float32Neon matrix, PacketFloat32Neon scalar)
    {
        PacketVector3Float32Neon scalarVector = new(scalar, scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector, matrix.Row2 * scalarVector);
    }

    public static PacketMatrix3Float32Neon operator *(PacketFloat32Neon scalar, PacketMatrix3Float32Neon matrix) => matrix * scalar;

    public static PacketMatrix3Float32Neon Divide(PacketMatrix3Float32Neon matrix, PacketFloat32Neon scalar) => matrix / scalar;

    public static PacketMatrix3Float32Neon operator /(PacketMatrix3Float32Neon matrix, PacketFloat32Neon scalar) => matrix * PacketFloat32Neon.Reciprocal(scalar);

    public static PacketMatrix3Float32Neon InverseTranspose(PacketMatrix3Float32Neon matrix)
    {
        PacketVector3Float32Neon col0 = PacketVector3Float32Neon.Cross(matrix.Row1, matrix.Row2);
        PacketVector3Float32Neon col1 = PacketVector3Float32Neon.Cross(matrix.Row2, matrix.Row0);
        PacketVector3Float32Neon col2 = PacketVector3Float32Neon.Cross(matrix.Row0, matrix.Row1);
        PacketFloat32Neon invDet = PacketFloat32Neon.Reciprocal(PacketVector3Float32Neon.Dot(matrix.Row0, col0));
        PacketVector3Float32Neon invDetVector = new(invDet, invDet, invDet);
        return new(col0 * invDetVector, col1 * invDetVector, col2 * invDetVector);
    }

    public static PacketMatrix3Float32Neon Inverse(PacketMatrix3Float32Neon matrix) => Transpose(InverseTranspose(matrix));

    public static PacketMatrix3Float32Neon Rotate(PacketFloat32Neon angle)
    {
        var (sin, cos) = PacketFloat32Neon.SinCos(angle);
        PacketFloat32Neon zero = PacketFloat32Neon.Broadcast(0f);
        PacketFloat32Neon one = PacketFloat32Neon.Broadcast(1f);
        return new(
            new(cos, -sin, zero),
            new(sin, cos, zero),
            new(zero, zero, one));
    }

    public static PacketMatrix3Float32NeonMask operator ==(PacketMatrix3Float32Neon left, PacketMatrix3Float32Neon right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2);

    public static PacketMatrix3Float32NeonMask operator !=(PacketMatrix3Float32Neon left, PacketMatrix3Float32Neon right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix3Float32Neon other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2);
}

public readonly struct PacketMatrix3Float32NeonMask :
    ISimdMatrix3Mask<PacketMatrix3Float32NeonMask, PacketVector3Float32NeonMask, PacketFloat32NeonMask>
{
    public PacketMatrix3Float32NeonMask(PacketVector3Float32NeonMask row0, PacketVector3Float32NeonMask row1, PacketVector3Float32NeonMask row2)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
    }

    public static int LaneCount => PacketVector3Float32NeonMask.LaneCount;

    public PacketVector3Float32NeonMask Row0 { get; }

    public PacketVector3Float32NeonMask Row1 { get; }

    public PacketVector3Float32NeonMask Row2 { get; }

    public static PacketMatrix3Float32NeonMask True => new(PacketVector3Float32NeonMask.True, PacketVector3Float32NeonMask.True, PacketVector3Float32NeonMask.True);

    public static PacketMatrix3Float32NeonMask False => new(PacketVector3Float32NeonMask.False, PacketVector3Float32NeonMask.False, PacketVector3Float32NeonMask.False);

    public static PacketMatrix3Float32NeonMask Create(PacketVector3Float32NeonMask row0, PacketVector3Float32NeonMask row1, PacketVector3Float32NeonMask row2) => new(row0, row1, row2);

    public static PacketMatrix3Float32NeonMask Broadcast(PacketVector3Float32NeonMask value) => new(value, value, value);

    public static PacketVector3Float32NeonMask All(PacketMatrix3Float32NeonMask value) => value.Row0 & value.Row1 & value.Row2;

    public static PacketVector3Float32NeonMask Any(PacketMatrix3Float32NeonMask value) => value.Row0 | value.Row1 | value.Row2;

    public static PacketVector3Float32NeonMask None(PacketMatrix3Float32NeonMask value) => ~(value.Row0 | value.Row1 | value.Row2);

    public static PacketMatrix3Float32NeonMask AndNot(PacketMatrix3Float32NeonMask left, PacketMatrix3Float32NeonMask right)
    {
        return new(
            PacketVector3Float32NeonMask.AndNot(left.Row0, right.Row0),
            PacketVector3Float32NeonMask.AndNot(left.Row1, right.Row1),
            PacketVector3Float32NeonMask.AndNot(left.Row2, right.Row2));
    }

    public static PacketMatrix3Float32NeonMask Select(PacketMatrix3Float32NeonMask mask, PacketMatrix3Float32NeonMask ifTrue, PacketMatrix3Float32NeonMask ifFalse)
    {
        return new(
            PacketVector3Float32NeonMask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Float32NeonMask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Float32NeonMask.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2));
    }
    public static PacketMatrix3Float32NeonMask And(PacketMatrix3Float32NeonMask left, PacketMatrix3Float32NeonMask right) => left & right;

    public static PacketMatrix3Float32NeonMask Or(PacketMatrix3Float32NeonMask left, PacketMatrix3Float32NeonMask right) => left | right;

    public static PacketMatrix3Float32NeonMask Xor(PacketMatrix3Float32NeonMask left, PacketMatrix3Float32NeonMask right) => left ^ right;

    public static PacketMatrix3Float32NeonMask Not(PacketMatrix3Float32NeonMask value) => ~value;


    public static PacketMatrix3Float32NeonMask operator &(PacketMatrix3Float32NeonMask left, PacketMatrix3Float32NeonMask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1, left.Row2 & right.Row2);

    public static PacketMatrix3Float32NeonMask operator |(PacketMatrix3Float32NeonMask left, PacketMatrix3Float32NeonMask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1, left.Row2 | right.Row2);

    public static PacketMatrix3Float32NeonMask operator ^(PacketMatrix3Float32NeonMask left, PacketMatrix3Float32NeonMask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1, left.Row2 ^ right.Row2);

    public static PacketMatrix3Float32NeonMask operator ~(PacketMatrix3Float32NeonMask value) => new(~value.Row0, ~value.Row1, ~value.Row2);

    public static PacketMatrix3Float32NeonMask operator ==(PacketMatrix3Float32NeonMask left, PacketMatrix3Float32NeonMask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2);

    public static PacketMatrix3Float32NeonMask operator !=(PacketMatrix3Float32NeonMask left, PacketMatrix3Float32NeonMask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix3Float32NeonMask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2);
}
