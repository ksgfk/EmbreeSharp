namespace EmbreeSharp.SIMD;

public readonly struct PacketMatrix3Float32Sse :
    ISimdFloatingPointMatrix3<PacketMatrix3Float32Sse, PacketVector3Float32Sse, PacketFloat32Sse, float, PacketMatrix3Float32SseMask, PacketVector3Float32SseMask, PacketFloat32SseMask>
{
    public PacketMatrix3Float32Sse(PacketVector3Float32Sse row0, PacketVector3Float32Sse row1, PacketVector3Float32Sse row2)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
    }

    public static int LaneCount => PacketVector3Float32Sse.LaneCount;

    public PacketVector3Float32Sse Row0 { get; }

    public PacketVector3Float32Sse Row1 { get; }

    public PacketVector3Float32Sse Row2 { get; }

    public static PacketMatrix3Float32Sse Identity
    {
        get
        {
            PacketFloat32Sse zero = PacketFloat32Sse.Broadcast(0f);
            PacketFloat32Sse one = PacketFloat32Sse.Broadcast(1f);
            return new(
                new PacketVector3Float32Sse(one, zero, zero),
                new PacketVector3Float32Sse(zero, one, zero),
                new PacketVector3Float32Sse(zero, zero, one));
        }
    }

    public static PacketMatrix3Float32Sse Create(PacketVector3Float32Sse row0, PacketVector3Float32Sse row1, PacketVector3Float32Sse row2) => new(row0, row1, row2);

    public static PacketMatrix3Float32Sse Broadcast(float value)
    {
        PacketVector3Float32Sse row = PacketVector3Float32Sse.Broadcast(value);
        return new(row, row, row);
    }

    public static PacketMatrix3Float32Sse Select(PacketMatrix3Float32SseMask mask, PacketMatrix3Float32Sse ifTrue, PacketMatrix3Float32Sse ifFalse)
    {
        return new(
            PacketVector3Float32Sse.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Float32Sse.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Float32Sse.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketMatrix3Float32Sse Select(PacketVector3Float32SseMask mask, PacketMatrix3Float32Sse ifTrue, PacketMatrix3Float32Sse ifFalse)
    {
        return new(
            PacketVector3Float32Sse.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Float32Sse.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Float32Sse.Select(mask, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketMatrix3Float32Sse Select(PacketFloat32SseMask mask, PacketMatrix3Float32Sse ifTrue, PacketMatrix3Float32Sse ifFalse)
    {
        return new(
            PacketVector3Float32Sse.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Float32Sse.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Float32Sse.Select(mask, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketVector3Float32Sse Transform(PacketMatrix3Float32Sse matrix, PacketVector3Float32Sse vector)
    {
        return new(
            PacketVector3Float32Sse.Dot(matrix.Row0, vector),
            PacketVector3Float32Sse.Dot(matrix.Row1, vector),
            PacketVector3Float32Sse.Dot(matrix.Row2, vector));
    }

    public static PacketMatrix3Float32Sse Multiply(PacketMatrix3Float32Sse left, PacketMatrix3Float32Sse right) => left * right;

    public static PacketMatrix3Float32Sse Multiply(PacketMatrix3Float32Sse matrix, PacketFloat32Sse scalar) => matrix * scalar;

    public static PacketMatrix3Float32Sse Multiply(PacketFloat32Sse scalar, PacketMatrix3Float32Sse matrix) => scalar * matrix;

    public static PacketMatrix3Float32Sse FusedMultiplyAdd(PacketMatrix3Float32Sse left, PacketMatrix3Float32Sse right, PacketMatrix3Float32Sse addend)
    {
        return new(
            new PacketVector3Float32Sse(
                PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.X, right.Row0.X, addend.Row0.X))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.X, right.Row0.Y, addend.Row0.Y))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.X, right.Row0.Z, addend.Row0.Z)))),
            new PacketVector3Float32Sse(
                PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.X, right.Row0.X, addend.Row1.X))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.X, right.Row0.Y, addend.Row1.Y))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.X, right.Row0.Z, addend.Row1.Z)))),
            new PacketVector3Float32Sse(
                PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.X, right.Row0.X, addend.Row2.X))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.X, right.Row0.Y, addend.Row2.Y))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.X, right.Row0.Z, addend.Row2.Z)))));
    }

    public static PacketVector3Float32Sse FusedMultiplyAdd(PacketMatrix3Float32Sse matrix, PacketVector3Float32Sse vector, PacketVector3Float32Sse addend)
    {
        return new(
            PacketFloat32Sse.FusedMultiplyAdd(matrix.Row0.Z, vector.Z, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row0.X, vector.X, addend.X))),
            PacketFloat32Sse.FusedMultiplyAdd(matrix.Row1.Z, vector.Z, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row1.X, vector.X, addend.Y))),
            PacketFloat32Sse.FusedMultiplyAdd(matrix.Row2.Z, vector.Z, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row2.Y, vector.Y, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row2.X, vector.X, addend.Z))));
    }

    public static PacketMatrix3Float32Sse Transpose(PacketMatrix3Float32Sse matrix)
    {
        return new(
            new(matrix.Row0.X, matrix.Row1.X, matrix.Row2.X),
            new(matrix.Row0.Y, matrix.Row1.Y, matrix.Row2.Y),
            new(matrix.Row0.Z, matrix.Row1.Z, matrix.Row2.Z));
    }

    public static PacketMatrix3Float32Sse Scale(PacketVector3Float32Sse scale) => Scale(scale.X, scale.Y);

    public static PacketMatrix3Float32Sse Scale(PacketFloat32Sse x, PacketFloat32Sse y)
    {
        PacketFloat32Sse zero = PacketFloat32Sse.Broadcast(0f);
        PacketFloat32Sse one = PacketFloat32Sse.Broadcast(1f);
        return new(
            new(x, zero, zero),
            new(zero, y, zero),
            new(zero, zero, one));
    }

    public static PacketMatrix3Float32Sse Translate(PacketVector3Float32Sse translation) => Translate(translation.X, translation.Y);

    public static PacketMatrix3Float32Sse Translate(PacketFloat32Sse x, PacketFloat32Sse y)
    {
        PacketFloat32Sse zero = PacketFloat32Sse.Broadcast(0f);
        PacketFloat32Sse one = PacketFloat32Sse.Broadcast(1f);
        return new(
            new(one, zero, x),
            new(zero, one, y),
            new(zero, zero, one));
    }

    public static PacketMatrix3Float32Sse operator *(PacketMatrix3Float32Sse left, PacketMatrix3Float32Sse right)
    {
        return new(
            new PacketVector3Float32Sse(
                PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, left.Row0.X * right.Row0.X)),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, left.Row0.X * right.Row0.Y)),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, left.Row0.X * right.Row0.Z))),
            new PacketVector3Float32Sse(
                PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, left.Row1.X * right.Row0.X)),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, left.Row1.X * right.Row0.Y)),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, left.Row1.X * right.Row0.Z))),
            new PacketVector3Float32Sse(
                PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, left.Row2.X * right.Row0.X)),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, left.Row2.X * right.Row0.Y)),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, left.Row2.X * right.Row0.Z))));
    }

    public static PacketVector3Float32Sse operator *(PacketMatrix3Float32Sse matrix, PacketVector3Float32Sse vector) => Transform(matrix, vector);

    public static PacketMatrix3Float32Sse operator *(PacketMatrix3Float32Sse matrix, PacketFloat32Sse scalar)
    {
        PacketVector3Float32Sse scalarVector = new(scalar, scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector, matrix.Row2 * scalarVector);
    }

    public static PacketMatrix3Float32Sse operator *(PacketFloat32Sse scalar, PacketMatrix3Float32Sse matrix) => matrix * scalar;

    public static PacketMatrix3Float32Sse Divide(PacketMatrix3Float32Sse matrix, PacketFloat32Sse scalar) => matrix / scalar;

    public static PacketMatrix3Float32Sse operator /(PacketMatrix3Float32Sse matrix, PacketFloat32Sse scalar) => matrix * PacketFloat32Sse.Reciprocal(scalar);

    public static PacketMatrix3Float32Sse InverseTranspose(PacketMatrix3Float32Sse matrix)
    {
        PacketVector3Float32Sse col0 = PacketVector3Float32Sse.Cross(matrix.Row1, matrix.Row2);
        PacketVector3Float32Sse col1 = PacketVector3Float32Sse.Cross(matrix.Row2, matrix.Row0);
        PacketVector3Float32Sse col2 = PacketVector3Float32Sse.Cross(matrix.Row0, matrix.Row1);
        PacketFloat32Sse invDet = PacketFloat32Sse.Reciprocal(PacketVector3Float32Sse.Dot(matrix.Row0, col0));
        PacketVector3Float32Sse invDetVector = new(invDet, invDet, invDet);
        return new(col0 * invDetVector, col1 * invDetVector, col2 * invDetVector);
    }

    public static PacketMatrix3Float32Sse Inverse(PacketMatrix3Float32Sse matrix) => Transpose(InverseTranspose(matrix));

    public static PacketMatrix3Float32Sse Rotate(PacketFloat32Sse angle)
    {
        var (sin, cos) = PacketFloat32Sse.SinCos(angle);
        PacketFloat32Sse zero = PacketFloat32Sse.Broadcast(0f);
        PacketFloat32Sse one = PacketFloat32Sse.Broadcast(1f);
        return new(
            new(cos, -sin, zero),
            new(sin, cos, zero),
            new(zero, zero, one));
    }

    public static PacketMatrix3Float32SseMask operator ==(PacketMatrix3Float32Sse left, PacketMatrix3Float32Sse right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2);

    public static PacketMatrix3Float32SseMask operator !=(PacketMatrix3Float32Sse left, PacketMatrix3Float32Sse right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix3Float32Sse other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2);
}

public readonly struct PacketMatrix3Float32SseMask :
    ISimdMatrix3Mask<PacketMatrix3Float32SseMask, PacketVector3Float32SseMask, PacketFloat32SseMask>
{
    public PacketMatrix3Float32SseMask(PacketVector3Float32SseMask row0, PacketVector3Float32SseMask row1, PacketVector3Float32SseMask row2)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
    }

    public static int LaneCount => PacketVector3Float32SseMask.LaneCount;

    public PacketVector3Float32SseMask Row0 { get; }

    public PacketVector3Float32SseMask Row1 { get; }

    public PacketVector3Float32SseMask Row2 { get; }

    public static PacketMatrix3Float32SseMask True => new(PacketVector3Float32SseMask.True, PacketVector3Float32SseMask.True, PacketVector3Float32SseMask.True);

    public static PacketMatrix3Float32SseMask False => new(PacketVector3Float32SseMask.False, PacketVector3Float32SseMask.False, PacketVector3Float32SseMask.False);

    public static PacketMatrix3Float32SseMask Create(PacketVector3Float32SseMask row0, PacketVector3Float32SseMask row1, PacketVector3Float32SseMask row2) => new(row0, row1, row2);

    public static PacketMatrix3Float32SseMask Broadcast(PacketVector3Float32SseMask value) => new(value, value, value);

    public static PacketVector3Float32SseMask All(PacketMatrix3Float32SseMask value) => value.Row0 & value.Row1 & value.Row2;

    public static PacketVector3Float32SseMask Any(PacketMatrix3Float32SseMask value) => value.Row0 | value.Row1 | value.Row2;

    public static PacketVector3Float32SseMask None(PacketMatrix3Float32SseMask value) => ~(value.Row0 | value.Row1 | value.Row2);

    public static PacketMatrix3Float32SseMask AndNot(PacketMatrix3Float32SseMask left, PacketMatrix3Float32SseMask right)
    {
        return new(
            PacketVector3Float32SseMask.AndNot(left.Row0, right.Row0),
            PacketVector3Float32SseMask.AndNot(left.Row1, right.Row1),
            PacketVector3Float32SseMask.AndNot(left.Row2, right.Row2));
    }

    public static PacketMatrix3Float32SseMask Select(PacketMatrix3Float32SseMask mask, PacketMatrix3Float32SseMask ifTrue, PacketMatrix3Float32SseMask ifFalse)
    {
        return new(
            PacketVector3Float32SseMask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Float32SseMask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Float32SseMask.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2));
    }
    public static PacketMatrix3Float32SseMask And(PacketMatrix3Float32SseMask left, PacketMatrix3Float32SseMask right) => left & right;

    public static PacketMatrix3Float32SseMask Or(PacketMatrix3Float32SseMask left, PacketMatrix3Float32SseMask right) => left | right;

    public static PacketMatrix3Float32SseMask Xor(PacketMatrix3Float32SseMask left, PacketMatrix3Float32SseMask right) => left ^ right;

    public static PacketMatrix3Float32SseMask Not(PacketMatrix3Float32SseMask value) => ~value;


    public static PacketMatrix3Float32SseMask operator &(PacketMatrix3Float32SseMask left, PacketMatrix3Float32SseMask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1, left.Row2 & right.Row2);

    public static PacketMatrix3Float32SseMask operator |(PacketMatrix3Float32SseMask left, PacketMatrix3Float32SseMask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1, left.Row2 | right.Row2);

    public static PacketMatrix3Float32SseMask operator ^(PacketMatrix3Float32SseMask left, PacketMatrix3Float32SseMask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1, left.Row2 ^ right.Row2);

    public static PacketMatrix3Float32SseMask operator ~(PacketMatrix3Float32SseMask value) => new(~value.Row0, ~value.Row1, ~value.Row2);

    public static PacketMatrix3Float32SseMask operator ==(PacketMatrix3Float32SseMask left, PacketMatrix3Float32SseMask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2);

    public static PacketMatrix3Float32SseMask operator !=(PacketMatrix3Float32SseMask left, PacketMatrix3Float32SseMask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix3Float32SseMask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2);
}
