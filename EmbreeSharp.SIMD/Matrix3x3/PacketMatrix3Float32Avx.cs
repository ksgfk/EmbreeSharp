namespace EmbreeSharp.SIMD;

public readonly struct PacketMatrix3Float32Avx :
    ISimdFloatingPointMatrix3<PacketMatrix3Float32Avx, PacketVector3Float32Avx, PacketFloat32Avx, float, PacketMatrix3Float32AvxMask, PacketVector3Float32AvxMask, PacketFloat32AvxMask>
{
    public PacketMatrix3Float32Avx(PacketVector3Float32Avx row0, PacketVector3Float32Avx row1, PacketVector3Float32Avx row2)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
    }

    public static int LaneCount => PacketVector3Float32Avx.LaneCount;

    public PacketVector3Float32Avx Row0 { get; }

    public PacketVector3Float32Avx Row1 { get; }

    public PacketVector3Float32Avx Row2 { get; }

    public static PacketMatrix3Float32Avx Identity
    {
        get
        {
            PacketFloat32Avx zero = PacketFloat32Avx.Broadcast(0f);
            PacketFloat32Avx one = PacketFloat32Avx.Broadcast(1f);
            return new(
                new PacketVector3Float32Avx(one, zero, zero),
                new PacketVector3Float32Avx(zero, one, zero),
                new PacketVector3Float32Avx(zero, zero, one));
        }
    }

    public static PacketMatrix3Float32Avx Create(PacketVector3Float32Avx row0, PacketVector3Float32Avx row1, PacketVector3Float32Avx row2) => new(row0, row1, row2);

    public static PacketMatrix3Float32Avx Broadcast(float value)
    {
        PacketVector3Float32Avx row = PacketVector3Float32Avx.Broadcast(value);
        return new(row, row, row);
    }

    public static PacketMatrix3Float32Avx Select(PacketMatrix3Float32AvxMask mask, PacketMatrix3Float32Avx ifTrue, PacketMatrix3Float32Avx ifFalse)
    {
        return new(
            PacketVector3Float32Avx.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Float32Avx.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Float32Avx.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketMatrix3Float32Avx Select(PacketVector3Float32AvxMask mask, PacketMatrix3Float32Avx ifTrue, PacketMatrix3Float32Avx ifFalse)
    {
        return new(
            PacketVector3Float32Avx.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Float32Avx.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Float32Avx.Select(mask, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketMatrix3Float32Avx Select(PacketFloat32AvxMask mask, PacketMatrix3Float32Avx ifTrue, PacketMatrix3Float32Avx ifFalse)
    {
        return new(
            PacketVector3Float32Avx.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Float32Avx.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Float32Avx.Select(mask, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketVector3Float32Avx Transform(PacketMatrix3Float32Avx matrix, PacketVector3Float32Avx vector)
    {
        return new(
            PacketVector3Float32Avx.Dot(matrix.Row0, vector),
            PacketVector3Float32Avx.Dot(matrix.Row1, vector),
            PacketVector3Float32Avx.Dot(matrix.Row2, vector));
    }

    public static PacketMatrix3Float32Avx Multiply(PacketMatrix3Float32Avx left, PacketMatrix3Float32Avx right) => left * right;

    public static PacketMatrix3Float32Avx Multiply(PacketMatrix3Float32Avx matrix, PacketFloat32Avx scalar) => matrix * scalar;

    public static PacketMatrix3Float32Avx Multiply(PacketFloat32Avx scalar, PacketMatrix3Float32Avx matrix) => scalar * matrix;

    public static PacketMatrix3Float32Avx FusedMultiplyAdd(PacketMatrix3Float32Avx left, PacketMatrix3Float32Avx right, PacketMatrix3Float32Avx addend)
    {
        return new(
            new PacketVector3Float32Avx(
                PacketFloat32Avx.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, PacketFloat32Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, PacketFloat32Avx.FusedMultiplyAdd(left.Row0.X, right.Row0.X, addend.Row0.X))),
                PacketFloat32Avx.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, PacketFloat32Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, PacketFloat32Avx.FusedMultiplyAdd(left.Row0.X, right.Row0.Y, addend.Row0.Y))),
                PacketFloat32Avx.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, PacketFloat32Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, PacketFloat32Avx.FusedMultiplyAdd(left.Row0.X, right.Row0.Z, addend.Row0.Z)))),
            new PacketVector3Float32Avx(
                PacketFloat32Avx.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, PacketFloat32Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, PacketFloat32Avx.FusedMultiplyAdd(left.Row1.X, right.Row0.X, addend.Row1.X))),
                PacketFloat32Avx.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, PacketFloat32Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, PacketFloat32Avx.FusedMultiplyAdd(left.Row1.X, right.Row0.Y, addend.Row1.Y))),
                PacketFloat32Avx.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, PacketFloat32Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, PacketFloat32Avx.FusedMultiplyAdd(left.Row1.X, right.Row0.Z, addend.Row1.Z)))),
            new PacketVector3Float32Avx(
                PacketFloat32Avx.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, PacketFloat32Avx.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, PacketFloat32Avx.FusedMultiplyAdd(left.Row2.X, right.Row0.X, addend.Row2.X))),
                PacketFloat32Avx.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, PacketFloat32Avx.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, PacketFloat32Avx.FusedMultiplyAdd(left.Row2.X, right.Row0.Y, addend.Row2.Y))),
                PacketFloat32Avx.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, PacketFloat32Avx.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, PacketFloat32Avx.FusedMultiplyAdd(left.Row2.X, right.Row0.Z, addend.Row2.Z)))));
    }

    public static PacketVector3Float32Avx FusedMultiplyAdd(PacketMatrix3Float32Avx matrix, PacketVector3Float32Avx vector, PacketVector3Float32Avx addend)
    {
        return new(
            PacketFloat32Avx.FusedMultiplyAdd(matrix.Row0.Z, vector.Z, PacketFloat32Avx.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, PacketFloat32Avx.FusedMultiplyAdd(matrix.Row0.X, vector.X, addend.X))),
            PacketFloat32Avx.FusedMultiplyAdd(matrix.Row1.Z, vector.Z, PacketFloat32Avx.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, PacketFloat32Avx.FusedMultiplyAdd(matrix.Row1.X, vector.X, addend.Y))),
            PacketFloat32Avx.FusedMultiplyAdd(matrix.Row2.Z, vector.Z, PacketFloat32Avx.FusedMultiplyAdd(matrix.Row2.Y, vector.Y, PacketFloat32Avx.FusedMultiplyAdd(matrix.Row2.X, vector.X, addend.Z))));
    }

    public static PacketMatrix3Float32Avx Transpose(PacketMatrix3Float32Avx matrix)
    {
        return new(
            new(matrix.Row0.X, matrix.Row1.X, matrix.Row2.X),
            new(matrix.Row0.Y, matrix.Row1.Y, matrix.Row2.Y),
            new(matrix.Row0.Z, matrix.Row1.Z, matrix.Row2.Z));
    }

    public static PacketMatrix3Float32Avx Scale(PacketVector3Float32Avx scale) => Scale(scale.X, scale.Y);

    public static PacketMatrix3Float32Avx Scale(PacketFloat32Avx x, PacketFloat32Avx y)
    {
        PacketFloat32Avx zero = PacketFloat32Avx.Broadcast(0f);
        PacketFloat32Avx one = PacketFloat32Avx.Broadcast(1f);
        return new(
            new(x, zero, zero),
            new(zero, y, zero),
            new(zero, zero, one));
    }

    public static PacketMatrix3Float32Avx Translate(PacketVector3Float32Avx translation) => Translate(translation.X, translation.Y);

    public static PacketMatrix3Float32Avx Translate(PacketFloat32Avx x, PacketFloat32Avx y)
    {
        PacketFloat32Avx zero = PacketFloat32Avx.Broadcast(0f);
        PacketFloat32Avx one = PacketFloat32Avx.Broadcast(1f);
        return new(
            new(one, zero, x),
            new(zero, one, y),
            new(zero, zero, one));
    }

    public static PacketMatrix3Float32Avx operator *(PacketMatrix3Float32Avx left, PacketMatrix3Float32Avx right)
    {
        return new(
            new PacketVector3Float32Avx(
                PacketFloat32Avx.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, PacketFloat32Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, left.Row0.X * right.Row0.X)),
                PacketFloat32Avx.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, PacketFloat32Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, left.Row0.X * right.Row0.Y)),
                PacketFloat32Avx.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, PacketFloat32Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, left.Row0.X * right.Row0.Z))),
            new PacketVector3Float32Avx(
                PacketFloat32Avx.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, PacketFloat32Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, left.Row1.X * right.Row0.X)),
                PacketFloat32Avx.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, PacketFloat32Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, left.Row1.X * right.Row0.Y)),
                PacketFloat32Avx.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, PacketFloat32Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, left.Row1.X * right.Row0.Z))),
            new PacketVector3Float32Avx(
                PacketFloat32Avx.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, PacketFloat32Avx.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, left.Row2.X * right.Row0.X)),
                PacketFloat32Avx.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, PacketFloat32Avx.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, left.Row2.X * right.Row0.Y)),
                PacketFloat32Avx.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, PacketFloat32Avx.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, left.Row2.X * right.Row0.Z))));
    }

    public static PacketVector3Float32Avx operator *(PacketMatrix3Float32Avx matrix, PacketVector3Float32Avx vector) => Transform(matrix, vector);

    public static PacketMatrix3Float32Avx operator *(PacketMatrix3Float32Avx matrix, PacketFloat32Avx scalar)
    {
        PacketVector3Float32Avx scalarVector = new(scalar, scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector, matrix.Row2 * scalarVector);
    }

    public static PacketMatrix3Float32Avx operator *(PacketFloat32Avx scalar, PacketMatrix3Float32Avx matrix) => matrix * scalar;

    public static PacketMatrix3Float32Avx Divide(PacketMatrix3Float32Avx matrix, PacketFloat32Avx scalar) => matrix / scalar;

    public static PacketMatrix3Float32Avx operator /(PacketMatrix3Float32Avx matrix, PacketFloat32Avx scalar) => matrix * PacketFloat32Avx.Reciprocal(scalar);

    public static PacketMatrix3Float32Avx InverseTranspose(PacketMatrix3Float32Avx matrix)
    {
        PacketVector3Float32Avx col0 = PacketVector3Float32Avx.Cross(matrix.Row1, matrix.Row2);
        PacketVector3Float32Avx col1 = PacketVector3Float32Avx.Cross(matrix.Row2, matrix.Row0);
        PacketVector3Float32Avx col2 = PacketVector3Float32Avx.Cross(matrix.Row0, matrix.Row1);
        PacketFloat32Avx invDet = PacketFloat32Avx.Reciprocal(PacketVector3Float32Avx.Dot(matrix.Row0, col0));
        PacketVector3Float32Avx invDetVector = new(invDet, invDet, invDet);
        return new(col0 * invDetVector, col1 * invDetVector, col2 * invDetVector);
    }

    public static PacketMatrix3Float32Avx Inverse(PacketMatrix3Float32Avx matrix) => Transpose(InverseTranspose(matrix));

    public static PacketMatrix3Float32Avx Rotate(PacketFloat32Avx angle)
    {
        var (sin, cos) = PacketFloat32Avx.SinCos(angle);
        PacketFloat32Avx zero = PacketFloat32Avx.Broadcast(0f);
        PacketFloat32Avx one = PacketFloat32Avx.Broadcast(1f);
        return new(
            new(cos, -sin, zero),
            new(sin, cos, zero),
            new(zero, zero, one));
    }

    public static PacketMatrix3Float32AvxMask operator ==(PacketMatrix3Float32Avx left, PacketMatrix3Float32Avx right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2);

    public static PacketMatrix3Float32AvxMask operator !=(PacketMatrix3Float32Avx left, PacketMatrix3Float32Avx right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix3Float32Avx other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2);
}

public readonly struct PacketMatrix3Float32AvxMask :
    ISimdMatrix3Mask<PacketMatrix3Float32AvxMask, PacketVector3Float32AvxMask, PacketFloat32AvxMask>
{
    public PacketMatrix3Float32AvxMask(PacketVector3Float32AvxMask row0, PacketVector3Float32AvxMask row1, PacketVector3Float32AvxMask row2)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
    }

    public static int LaneCount => PacketVector3Float32AvxMask.LaneCount;

    public PacketVector3Float32AvxMask Row0 { get; }

    public PacketVector3Float32AvxMask Row1 { get; }

    public PacketVector3Float32AvxMask Row2 { get; }

    public static PacketMatrix3Float32AvxMask True => new(PacketVector3Float32AvxMask.True, PacketVector3Float32AvxMask.True, PacketVector3Float32AvxMask.True);

    public static PacketMatrix3Float32AvxMask False => new(PacketVector3Float32AvxMask.False, PacketVector3Float32AvxMask.False, PacketVector3Float32AvxMask.False);

    public static PacketMatrix3Float32AvxMask Create(PacketVector3Float32AvxMask row0, PacketVector3Float32AvxMask row1, PacketVector3Float32AvxMask row2) => new(row0, row1, row2);

    public static PacketMatrix3Float32AvxMask Broadcast(PacketVector3Float32AvxMask value) => new(value, value, value);

    public static PacketVector3Float32AvxMask All(PacketMatrix3Float32AvxMask value) => value.Row0 & value.Row1 & value.Row2;

    public static PacketVector3Float32AvxMask Any(PacketMatrix3Float32AvxMask value) => value.Row0 | value.Row1 | value.Row2;

    public static PacketVector3Float32AvxMask None(PacketMatrix3Float32AvxMask value) => ~(value.Row0 | value.Row1 | value.Row2);

    public static PacketMatrix3Float32AvxMask AndNot(PacketMatrix3Float32AvxMask left, PacketMatrix3Float32AvxMask right)
    {
        return new(
            PacketVector3Float32AvxMask.AndNot(left.Row0, right.Row0),
            PacketVector3Float32AvxMask.AndNot(left.Row1, right.Row1),
            PacketVector3Float32AvxMask.AndNot(left.Row2, right.Row2));
    }

    public static PacketMatrix3Float32AvxMask Select(PacketMatrix3Float32AvxMask mask, PacketMatrix3Float32AvxMask ifTrue, PacketMatrix3Float32AvxMask ifFalse)
    {
        return new(
            PacketVector3Float32AvxMask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Float32AvxMask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Float32AvxMask.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2));
    }
    public static PacketMatrix3Float32AvxMask And(PacketMatrix3Float32AvxMask left, PacketMatrix3Float32AvxMask right) => left & right;

    public static PacketMatrix3Float32AvxMask Or(PacketMatrix3Float32AvxMask left, PacketMatrix3Float32AvxMask right) => left | right;

    public static PacketMatrix3Float32AvxMask Xor(PacketMatrix3Float32AvxMask left, PacketMatrix3Float32AvxMask right) => left ^ right;

    public static PacketMatrix3Float32AvxMask Not(PacketMatrix3Float32AvxMask value) => ~value;


    public static PacketMatrix3Float32AvxMask operator &(PacketMatrix3Float32AvxMask left, PacketMatrix3Float32AvxMask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1, left.Row2 & right.Row2);

    public static PacketMatrix3Float32AvxMask operator |(PacketMatrix3Float32AvxMask left, PacketMatrix3Float32AvxMask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1, left.Row2 | right.Row2);

    public static PacketMatrix3Float32AvxMask operator ^(PacketMatrix3Float32AvxMask left, PacketMatrix3Float32AvxMask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1, left.Row2 ^ right.Row2);

    public static PacketMatrix3Float32AvxMask operator ~(PacketMatrix3Float32AvxMask value) => new(~value.Row0, ~value.Row1, ~value.Row2);

    public static PacketMatrix3Float32AvxMask operator ==(PacketMatrix3Float32AvxMask left, PacketMatrix3Float32AvxMask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2);

    public static PacketMatrix3Float32AvxMask operator !=(PacketMatrix3Float32AvxMask left, PacketMatrix3Float32AvxMask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix3Float32AvxMask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2);
}
