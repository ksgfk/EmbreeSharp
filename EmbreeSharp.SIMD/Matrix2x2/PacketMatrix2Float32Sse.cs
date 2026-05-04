namespace EmbreeSharp.SIMD;

public readonly struct PacketMatrix2Float32Sse :
    ISimdFloatingPointMatrix2<PacketMatrix2Float32Sse, PacketVector2Float32Sse, PacketFloat32Sse, float, PacketMatrix2Float32SseMask, PacketVector2Float32SseMask, PacketFloat32SseMask>
{
    public PacketMatrix2Float32Sse(PacketVector2Float32Sse row0, PacketVector2Float32Sse row1)
    {
        Row0 = row0;
        Row1 = row1;
    }

    public static int LaneCount => PacketVector2Float32Sse.LaneCount;

    public PacketVector2Float32Sse Row0 { get; }

    public PacketVector2Float32Sse Row1 { get; }

    public static PacketMatrix2Float32Sse Identity
    {
        get
        {
            PacketFloat32Sse zero = PacketFloat32Sse.Broadcast(0f);
            PacketFloat32Sse one = PacketFloat32Sse.Broadcast(1f);
            return new(
                new PacketVector2Float32Sse(one, zero),
                new PacketVector2Float32Sse(zero, one));
        }
    }

    public static PacketMatrix2Float32Sse Create(PacketVector2Float32Sse row0, PacketVector2Float32Sse row1) => new(row0, row1);

    public static PacketMatrix2Float32Sse Broadcast(float value)
    {
        PacketVector2Float32Sse row = PacketVector2Float32Sse.Broadcast(value);
        return new(row, row);
    }

    public static PacketMatrix2Float32Sse Select(PacketMatrix2Float32SseMask mask, PacketMatrix2Float32Sse ifTrue, PacketMatrix2Float32Sse ifFalse)
    {
        return new(
            PacketVector2Float32Sse.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Float32Sse.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1));
    }

    public static PacketMatrix2Float32Sse Select(PacketVector2Float32SseMask mask, PacketMatrix2Float32Sse ifTrue, PacketMatrix2Float32Sse ifFalse)
    {
        return new(
            PacketVector2Float32Sse.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Float32Sse.Select(mask, ifTrue.Row1, ifFalse.Row1));
    }

    public static PacketMatrix2Float32Sse Select(PacketFloat32SseMask mask, PacketMatrix2Float32Sse ifTrue, PacketMatrix2Float32Sse ifFalse)
    {
        return new(
            PacketVector2Float32Sse.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Float32Sse.Select(mask, ifTrue.Row1, ifFalse.Row1));
    }

    public static PacketVector2Float32Sse Transform(PacketMatrix2Float32Sse matrix, PacketVector2Float32Sse vector)
    {
        return new(
            PacketFloat32Sse.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, matrix.Row0.X * vector.X),
            PacketFloat32Sse.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, matrix.Row1.X * vector.X));
    }

    public static PacketMatrix2Float32Sse Multiply(PacketMatrix2Float32Sse left, PacketMatrix2Float32Sse right) => left * right;

    public static PacketMatrix2Float32Sse Multiply(PacketMatrix2Float32Sse matrix, PacketFloat32Sse scalar) => matrix * scalar;

    public static PacketMatrix2Float32Sse Multiply(PacketFloat32Sse scalar, PacketMatrix2Float32Sse matrix) => scalar * matrix;

    public static PacketMatrix2Float32Sse FusedMultiplyAdd(PacketMatrix2Float32Sse left, PacketMatrix2Float32Sse right, PacketMatrix2Float32Sse addend)
    {
        return new(
            new PacketVector2Float32Sse(
                PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.X, right.Row0.X, addend.Row0.X)),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.X, right.Row0.Y, addend.Row0.Y))),
            new PacketVector2Float32Sse(
                PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.X, right.Row0.X, addend.Row1.X)),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.X, right.Row0.Y, addend.Row1.Y))));
    }

    public static PacketVector2Float32Sse FusedMultiplyAdd(PacketMatrix2Float32Sse matrix, PacketVector2Float32Sse vector, PacketVector2Float32Sse addend)
    {
        return new(
            PacketFloat32Sse.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row0.X, vector.X, addend.X)),
            PacketFloat32Sse.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row1.X, vector.X, addend.Y)));
    }

    public static PacketMatrix2Float32Sse Transpose(PacketMatrix2Float32Sse matrix)
    {
        return new(
            new(matrix.Row0.X, matrix.Row1.X),
            new(matrix.Row0.Y, matrix.Row1.Y));
    }

    public static PacketMatrix2Float32Sse Scale(PacketVector2Float32Sse scale) => Scale(scale.X, scale.Y);

    public static PacketMatrix2Float32Sse Scale(PacketFloat32Sse x, PacketFloat32Sse y)
    {
        PacketFloat32Sse zero = PacketFloat32Sse.Broadcast(0f);
        return new(
            new(x, zero),
            new(zero, y));
    }

    public static PacketMatrix2Float32Sse operator *(PacketMatrix2Float32Sse left, PacketMatrix2Float32Sse right)
    {
        return new(
            new PacketVector2Float32Sse(
                PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, left.Row0.X * right.Row0.X),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, left.Row0.X * right.Row0.Y)),
            new PacketVector2Float32Sse(
                PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, left.Row1.X * right.Row0.X),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, left.Row1.X * right.Row0.Y)));
    }

    public static PacketVector2Float32Sse operator *(PacketMatrix2Float32Sse matrix, PacketVector2Float32Sse vector) => Transform(matrix, vector);

    public static PacketMatrix2Float32Sse operator *(PacketMatrix2Float32Sse matrix, PacketFloat32Sse scalar)
    {
        PacketVector2Float32Sse scalarVector = new(scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector);
    }

    public static PacketMatrix2Float32Sse operator *(PacketFloat32Sse scalar, PacketMatrix2Float32Sse matrix) => matrix * scalar;
    public static PacketMatrix2Float32Sse Divide(PacketMatrix2Float32Sse matrix, PacketFloat32Sse scalar) => matrix / scalar;

    public static PacketMatrix2Float32Sse operator /(PacketMatrix2Float32Sse matrix, PacketFloat32Sse scalar) => matrix * PacketFloat32Sse.Reciprocal(scalar);

    public static PacketMatrix2Float32Sse Inverse(PacketMatrix2Float32Sse matrix)
    {
        PacketFloat32Sse invDet = PacketFloat32Sse.Reciprocal(PacketFloat32Sse.FusedMultiplyAdd(matrix.Row0.X, matrix.Row1.Y, -(matrix.Row0.Y * matrix.Row1.X)));
        return new(
            new(matrix.Row1.Y * invDet, -matrix.Row0.Y * invDet),
            new(-matrix.Row1.X * invDet, matrix.Row0.X * invDet));
    }

    public static PacketMatrix2Float32Sse InverseTranspose(PacketMatrix2Float32Sse matrix)
    {
        PacketFloat32Sse invDet = PacketFloat32Sse.Reciprocal(PacketFloat32Sse.FusedMultiplyAdd(matrix.Row0.X, matrix.Row1.Y, -(matrix.Row0.Y * matrix.Row1.X)));
        return new(
            new(matrix.Row1.Y * invDet, -matrix.Row1.X * invDet),
            new(-matrix.Row0.Y * invDet, matrix.Row0.X * invDet));
    }

    public static PacketMatrix2Float32Sse Rotate(PacketFloat32Sse angle)
    {
        var (sin, cos) = PacketFloat32Sse.SinCos(angle);
        return new(
            new(cos, -sin),
            new(sin, cos));
    }

    public static PacketMatrix2Float32SseMask operator ==(PacketMatrix2Float32Sse left, PacketMatrix2Float32Sse right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1);

    public static PacketMatrix2Float32SseMask operator !=(PacketMatrix2Float32Sse left, PacketMatrix2Float32Sse right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1);

    public static PacketMatrix2Float32SseMask operator <(PacketMatrix2Float32Sse left, PacketMatrix2Float32Sse right) => new(
        new PacketVector2Float32SseMask(left.Row0.X < right.Row0.X, left.Row0.Y < right.Row0.Y),
        new PacketVector2Float32SseMask(left.Row1.X < right.Row1.X, left.Row1.Y < right.Row1.Y));

    public static PacketMatrix2Float32SseMask operator >(PacketMatrix2Float32Sse left, PacketMatrix2Float32Sse right) => new(
        new PacketVector2Float32SseMask(left.Row0.X > right.Row0.X, left.Row0.Y > right.Row0.Y),
        new PacketVector2Float32SseMask(left.Row1.X > right.Row1.X, left.Row1.Y > right.Row1.Y));

    public static PacketMatrix2Float32SseMask operator <=(PacketMatrix2Float32Sse left, PacketMatrix2Float32Sse right) => new(
        new PacketVector2Float32SseMask(left.Row0.X <= right.Row0.X, left.Row0.Y <= right.Row0.Y),
        new PacketVector2Float32SseMask(left.Row1.X <= right.Row1.X, left.Row1.Y <= right.Row1.Y));

    public static PacketMatrix2Float32SseMask operator >=(PacketMatrix2Float32Sse left, PacketMatrix2Float32Sse right) => new(
        new PacketVector2Float32SseMask(left.Row0.X >= right.Row0.X, left.Row0.Y >= right.Row0.Y),
        new PacketVector2Float32SseMask(left.Row1.X >= right.Row1.X, left.Row1.Y >= right.Row1.Y));

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix2Float32Sse other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1);
}

public readonly struct PacketMatrix2Float32SseMask :
    ISimdMatrix2Mask<PacketMatrix2Float32SseMask, PacketVector2Float32SseMask, PacketFloat32SseMask>
{
    public PacketMatrix2Float32SseMask(PacketVector2Float32SseMask row0, PacketVector2Float32SseMask row1)
    {
        Row0 = row0;
        Row1 = row1;
    }

    public static int LaneCount => PacketVector2Float32SseMask.LaneCount;

    public PacketVector2Float32SseMask Row0 { get; }

    public PacketVector2Float32SseMask Row1 { get; }

    public static PacketMatrix2Float32SseMask True => new(PacketVector2Float32SseMask.True, PacketVector2Float32SseMask.True);

    public static PacketMatrix2Float32SseMask False => new(PacketVector2Float32SseMask.False, PacketVector2Float32SseMask.False);

    public static PacketMatrix2Float32SseMask Create(PacketVector2Float32SseMask row0, PacketVector2Float32SseMask row1) => new(row0, row1);

    public static PacketMatrix2Float32SseMask Broadcast(PacketVector2Float32SseMask value) => new(value, value);

    public static PacketVector2Float32SseMask All(PacketMatrix2Float32SseMask value) => value.Row0 & value.Row1;

    public static PacketVector2Float32SseMask Any(PacketMatrix2Float32SseMask value) => value.Row0 | value.Row1;

    public static PacketVector2Float32SseMask None(PacketMatrix2Float32SseMask value) => ~(value.Row0 | value.Row1);

    public static PacketMatrix2Float32SseMask AndNot(PacketMatrix2Float32SseMask left, PacketMatrix2Float32SseMask right)
    {
        return new(
            PacketVector2Float32SseMask.AndNot(left.Row0, right.Row0),
            PacketVector2Float32SseMask.AndNot(left.Row1, right.Row1));
    }

    public static PacketMatrix2Float32SseMask Select(PacketMatrix2Float32SseMask mask, PacketMatrix2Float32SseMask ifTrue, PacketMatrix2Float32SseMask ifFalse)
    {
        return new(
            PacketVector2Float32SseMask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Float32SseMask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1));
    }
    public static PacketMatrix2Float32SseMask And(PacketMatrix2Float32SseMask left, PacketMatrix2Float32SseMask right) => left & right;

    public static PacketMatrix2Float32SseMask Or(PacketMatrix2Float32SseMask left, PacketMatrix2Float32SseMask right) => left | right;

    public static PacketMatrix2Float32SseMask Xor(PacketMatrix2Float32SseMask left, PacketMatrix2Float32SseMask right) => left ^ right;

    public static PacketMatrix2Float32SseMask Not(PacketMatrix2Float32SseMask value) => ~value;


    public static PacketMatrix2Float32SseMask operator &(PacketMatrix2Float32SseMask left, PacketMatrix2Float32SseMask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1);

    public static PacketMatrix2Float32SseMask operator |(PacketMatrix2Float32SseMask left, PacketMatrix2Float32SseMask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1);

    public static PacketMatrix2Float32SseMask operator ^(PacketMatrix2Float32SseMask left, PacketMatrix2Float32SseMask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1);

    public static PacketMatrix2Float32SseMask operator ~(PacketMatrix2Float32SseMask value) => new(~value.Row0, ~value.Row1);

    public static PacketMatrix2Float32SseMask operator ==(PacketMatrix2Float32SseMask left, PacketMatrix2Float32SseMask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1);

    public static PacketMatrix2Float32SseMask operator !=(PacketMatrix2Float32SseMask left, PacketMatrix2Float32SseMask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix2Float32SseMask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1);
}
