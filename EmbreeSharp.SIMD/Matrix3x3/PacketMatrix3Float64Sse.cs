namespace EmbreeSharp.SIMD;

public readonly struct PacketMatrix3Float64Sse :
    ISimdFloatingPointMatrix3<PacketMatrix3Float64Sse, PacketVector3Float64Sse, PacketFloat64Sse, double, PacketMatrix3Float64SseMask, PacketVector3Float64SseMask, PacketFloat64SseMask>
{
    public PacketMatrix3Float64Sse(PacketVector3Float64Sse row0, PacketVector3Float64Sse row1, PacketVector3Float64Sse row2)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
    }

    public static int LaneCount => PacketVector3Float64Sse.LaneCount;

    public PacketVector3Float64Sse Row0 { get; }

    public PacketVector3Float64Sse Row1 { get; }

    public PacketVector3Float64Sse Row2 { get; }

    public static PacketMatrix3Float64Sse Identity
    {
        get
        {
            PacketFloat64Sse zero = PacketFloat64Sse.Broadcast(0d);
            PacketFloat64Sse one = PacketFloat64Sse.Broadcast(1d);
            return new(
                new PacketVector3Float64Sse(one, zero, zero),
                new PacketVector3Float64Sse(zero, one, zero),
                new PacketVector3Float64Sse(zero, zero, one));
        }
    }

    public static PacketMatrix3Float64Sse Create(PacketVector3Float64Sse row0, PacketVector3Float64Sse row1, PacketVector3Float64Sse row2) => new(row0, row1, row2);

    public static PacketMatrix3Float64Sse Broadcast(double value)
    {
        PacketVector3Float64Sse row = PacketVector3Float64Sse.Broadcast(value);
        return new(row, row, row);
    }

    public static PacketMatrix3Float64Sse Select(PacketMatrix3Float64SseMask mask, PacketMatrix3Float64Sse ifTrue, PacketMatrix3Float64Sse ifFalse)
    {
        return new(
            PacketVector3Float64Sse.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Float64Sse.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Float64Sse.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketMatrix3Float64Sse Select(PacketVector3Float64SseMask mask, PacketMatrix3Float64Sse ifTrue, PacketMatrix3Float64Sse ifFalse)
    {
        return new(
            PacketVector3Float64Sse.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Float64Sse.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Float64Sse.Select(mask, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketMatrix3Float64Sse Select(PacketFloat64SseMask mask, PacketMatrix3Float64Sse ifTrue, PacketMatrix3Float64Sse ifFalse)
    {
        return new(
            PacketVector3Float64Sse.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Float64Sse.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Float64Sse.Select(mask, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketVector3Float64Sse Transform(PacketMatrix3Float64Sse matrix, PacketVector3Float64Sse vector)
    {
        return new(
            PacketVector3Float64Sse.Dot(matrix.Row0, vector),
            PacketVector3Float64Sse.Dot(matrix.Row1, vector),
            PacketVector3Float64Sse.Dot(matrix.Row2, vector));
    }

    public static PacketMatrix3Float64Sse Multiply(PacketMatrix3Float64Sse left, PacketMatrix3Float64Sse right) => left * right;

    public static PacketMatrix3Float64Sse Multiply(PacketMatrix3Float64Sse matrix, PacketFloat64Sse scalar) => matrix * scalar;

    public static PacketMatrix3Float64Sse Multiply(PacketFloat64Sse scalar, PacketMatrix3Float64Sse matrix) => scalar * matrix;

    public static PacketMatrix3Float64Sse FusedMultiplyAdd(PacketMatrix3Float64Sse left, PacketMatrix3Float64Sse right, PacketMatrix3Float64Sse addend)
    {
        return new(
            new PacketVector3Float64Sse(
                PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.X, right.Row0.X, addend.Row0.X))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.X, right.Row0.Y, addend.Row0.Y))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.X, right.Row0.Z, addend.Row0.Z)))),
            new PacketVector3Float64Sse(
                PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.X, right.Row0.X, addend.Row1.X))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.X, right.Row0.Y, addend.Row1.Y))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.X, right.Row0.Z, addend.Row1.Z)))),
            new PacketVector3Float64Sse(
                PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.X, right.Row0.X, addend.Row2.X))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.X, right.Row0.Y, addend.Row2.Y))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.X, right.Row0.Z, addend.Row2.Z)))));
    }

    public static PacketVector3Float64Sse FusedMultiplyAdd(PacketMatrix3Float64Sse matrix, PacketVector3Float64Sse vector, PacketVector3Float64Sse addend)
    {
        return new(
            PacketFloat64Sse.FusedMultiplyAdd(matrix.Row0.Z, vector.Z, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row0.X, vector.X, addend.X))),
            PacketFloat64Sse.FusedMultiplyAdd(matrix.Row1.Z, vector.Z, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row1.X, vector.X, addend.Y))),
            PacketFloat64Sse.FusedMultiplyAdd(matrix.Row2.Z, vector.Z, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row2.Y, vector.Y, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row2.X, vector.X, addend.Z))));
    }

    public static PacketMatrix3Float64Sse Transpose(PacketMatrix3Float64Sse matrix)
    {
        return new(
            new(matrix.Row0.X, matrix.Row1.X, matrix.Row2.X),
            new(matrix.Row0.Y, matrix.Row1.Y, matrix.Row2.Y),
            new(matrix.Row0.Z, matrix.Row1.Z, matrix.Row2.Z));
    }

    public static PacketMatrix3Float64Sse Scale(PacketVector3Float64Sse scale) => Scale(scale.X, scale.Y);

    public static PacketMatrix3Float64Sse Scale(PacketFloat64Sse x, PacketFloat64Sse y)
    {
        PacketFloat64Sse zero = PacketFloat64Sse.Broadcast(0d);
        PacketFloat64Sse one = PacketFloat64Sse.Broadcast(1d);
        return new(
            new(x, zero, zero),
            new(zero, y, zero),
            new(zero, zero, one));
    }

    public static PacketMatrix3Float64Sse Translate(PacketVector3Float64Sse translation) => Translate(translation.X, translation.Y);

    public static PacketMatrix3Float64Sse Translate(PacketFloat64Sse x, PacketFloat64Sse y)
    {
        PacketFloat64Sse zero = PacketFloat64Sse.Broadcast(0d);
        PacketFloat64Sse one = PacketFloat64Sse.Broadcast(1d);
        return new(
            new(one, zero, x),
            new(zero, one, y),
            new(zero, zero, one));
    }

    public static PacketMatrix3Float64Sse operator *(PacketMatrix3Float64Sse left, PacketMatrix3Float64Sse right)
    {
        return new(
            new PacketVector3Float64Sse(
                PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, left.Row0.X * right.Row0.X)),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, left.Row0.X * right.Row0.Y)),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, left.Row0.X * right.Row0.Z))),
            new PacketVector3Float64Sse(
                PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, left.Row1.X * right.Row0.X)),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, left.Row1.X * right.Row0.Y)),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, left.Row1.X * right.Row0.Z))),
            new PacketVector3Float64Sse(
                PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, left.Row2.X * right.Row0.X)),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, left.Row2.X * right.Row0.Y)),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, left.Row2.X * right.Row0.Z))));
    }

    public static PacketVector3Float64Sse operator *(PacketMatrix3Float64Sse matrix, PacketVector3Float64Sse vector) => Transform(matrix, vector);

    public static PacketMatrix3Float64Sse operator *(PacketMatrix3Float64Sse matrix, PacketFloat64Sse scalar)
    {
        PacketVector3Float64Sse scalarVector = new(scalar, scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector, matrix.Row2 * scalarVector);
    }

    public static PacketMatrix3Float64Sse operator *(PacketFloat64Sse scalar, PacketMatrix3Float64Sse matrix) => matrix * scalar;

    public static PacketMatrix3Float64Sse Divide(PacketMatrix3Float64Sse matrix, PacketFloat64Sse scalar) => matrix / scalar;

    public static PacketMatrix3Float64Sse operator /(PacketMatrix3Float64Sse matrix, PacketFloat64Sse scalar) => matrix * PacketFloat64Sse.Reciprocal(scalar);

    public static PacketMatrix3Float64Sse InverseTranspose(PacketMatrix3Float64Sse matrix)
    {
        PacketVector3Float64Sse col0 = PacketVector3Float64Sse.Cross(matrix.Row1, matrix.Row2);
        PacketVector3Float64Sse col1 = PacketVector3Float64Sse.Cross(matrix.Row2, matrix.Row0);
        PacketVector3Float64Sse col2 = PacketVector3Float64Sse.Cross(matrix.Row0, matrix.Row1);
        PacketFloat64Sse invDet = PacketFloat64Sse.Reciprocal(PacketVector3Float64Sse.Dot(matrix.Row0, col0));
        PacketVector3Float64Sse invDetVector = new(invDet, invDet, invDet);
        return new(col0 * invDetVector, col1 * invDetVector, col2 * invDetVector);
    }

    public static PacketMatrix3Float64Sse Inverse(PacketMatrix3Float64Sse matrix) => Transpose(InverseTranspose(matrix));

    public static PacketMatrix3Float64Sse Rotate(PacketFloat64Sse angle)
    {
        var (sin, cos) = PacketFloat64Sse.SinCos(angle);
        PacketFloat64Sse zero = PacketFloat64Sse.Broadcast(0d);
        PacketFloat64Sse one = PacketFloat64Sse.Broadcast(1d);
        return new(
            new(cos, -sin, zero),
            new(sin, cos, zero),
            new(zero, zero, one));
    }

    public static PacketMatrix3Float64SseMask operator ==(PacketMatrix3Float64Sse left, PacketMatrix3Float64Sse right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2);

    public static PacketMatrix3Float64SseMask operator !=(PacketMatrix3Float64Sse left, PacketMatrix3Float64Sse right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix3Float64Sse other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2);
}

public readonly struct PacketMatrix3Float64SseMask :
    ISimdMatrix3Mask<PacketMatrix3Float64SseMask, PacketVector3Float64SseMask, PacketFloat64SseMask>
{
    public PacketMatrix3Float64SseMask(PacketVector3Float64SseMask row0, PacketVector3Float64SseMask row1, PacketVector3Float64SseMask row2)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
    }

    public static int LaneCount => PacketVector3Float64SseMask.LaneCount;

    public PacketVector3Float64SseMask Row0 { get; }

    public PacketVector3Float64SseMask Row1 { get; }

    public PacketVector3Float64SseMask Row2 { get; }

    public static PacketMatrix3Float64SseMask True => new(PacketVector3Float64SseMask.True, PacketVector3Float64SseMask.True, PacketVector3Float64SseMask.True);

    public static PacketMatrix3Float64SseMask False => new(PacketVector3Float64SseMask.False, PacketVector3Float64SseMask.False, PacketVector3Float64SseMask.False);

    public static PacketMatrix3Float64SseMask Create(PacketVector3Float64SseMask row0, PacketVector3Float64SseMask row1, PacketVector3Float64SseMask row2) => new(row0, row1, row2);

    public static PacketMatrix3Float64SseMask Broadcast(PacketVector3Float64SseMask value) => new(value, value, value);

    public static PacketVector3Float64SseMask All(PacketMatrix3Float64SseMask value) => value.Row0 & value.Row1 & value.Row2;

    public static PacketVector3Float64SseMask Any(PacketMatrix3Float64SseMask value) => value.Row0 | value.Row1 | value.Row2;

    public static PacketVector3Float64SseMask None(PacketMatrix3Float64SseMask value) => ~(value.Row0 | value.Row1 | value.Row2);

    public static PacketMatrix3Float64SseMask AndNot(PacketMatrix3Float64SseMask left, PacketMatrix3Float64SseMask right)
    {
        return new(
            PacketVector3Float64SseMask.AndNot(left.Row0, right.Row0),
            PacketVector3Float64SseMask.AndNot(left.Row1, right.Row1),
            PacketVector3Float64SseMask.AndNot(left.Row2, right.Row2));
    }

    public static PacketMatrix3Float64SseMask Select(PacketMatrix3Float64SseMask mask, PacketMatrix3Float64SseMask ifTrue, PacketMatrix3Float64SseMask ifFalse)
    {
        return new(
            PacketVector3Float64SseMask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Float64SseMask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Float64SseMask.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2));
    }
    public static PacketMatrix3Float64SseMask And(PacketMatrix3Float64SseMask left, PacketMatrix3Float64SseMask right) => left & right;

    public static PacketMatrix3Float64SseMask Or(PacketMatrix3Float64SseMask left, PacketMatrix3Float64SseMask right) => left | right;

    public static PacketMatrix3Float64SseMask Xor(PacketMatrix3Float64SseMask left, PacketMatrix3Float64SseMask right) => left ^ right;

    public static PacketMatrix3Float64SseMask Not(PacketMatrix3Float64SseMask value) => ~value;


    public static PacketMatrix3Float64SseMask operator &(PacketMatrix3Float64SseMask left, PacketMatrix3Float64SseMask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1, left.Row2 & right.Row2);

    public static PacketMatrix3Float64SseMask operator |(PacketMatrix3Float64SseMask left, PacketMatrix3Float64SseMask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1, left.Row2 | right.Row2);

    public static PacketMatrix3Float64SseMask operator ^(PacketMatrix3Float64SseMask left, PacketMatrix3Float64SseMask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1, left.Row2 ^ right.Row2);

    public static PacketMatrix3Float64SseMask operator ~(PacketMatrix3Float64SseMask value) => new(~value.Row0, ~value.Row1, ~value.Row2);

    public static PacketMatrix3Float64SseMask operator ==(PacketMatrix3Float64SseMask left, PacketMatrix3Float64SseMask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2);

    public static PacketMatrix3Float64SseMask operator !=(PacketMatrix3Float64SseMask left, PacketMatrix3Float64SseMask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix3Float64SseMask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2);
}
