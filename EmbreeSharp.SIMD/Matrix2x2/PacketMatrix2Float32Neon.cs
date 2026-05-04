namespace EmbreeSharp.SIMD;

public readonly struct PacketMatrix2Float32Neon :
    ISimdFloatingPointMatrix2<PacketMatrix2Float32Neon, PacketVector2Float32Neon, PacketFloat32Neon, float, PacketMatrix2Float32NeonMask, PacketVector2Float32NeonMask, PacketFloat32NeonMask>
{
    public PacketMatrix2Float32Neon(PacketVector2Float32Neon row0, PacketVector2Float32Neon row1)
    {
        Row0 = row0;
        Row1 = row1;
    }

    public static int LaneCount => PacketVector2Float32Neon.LaneCount;

    public PacketVector2Float32Neon Row0 { get; }

    public PacketVector2Float32Neon Row1 { get; }

    public static PacketMatrix2Float32Neon Identity
    {
        get
        {
            PacketFloat32Neon zero = PacketFloat32Neon.Broadcast(0f);
            PacketFloat32Neon one = PacketFloat32Neon.Broadcast(1f);
            return new(
                new PacketVector2Float32Neon(one, zero),
                new PacketVector2Float32Neon(zero, one));
        }
    }

    public static PacketMatrix2Float32Neon Create(PacketVector2Float32Neon row0, PacketVector2Float32Neon row1) => new(row0, row1);

    public static PacketMatrix2Float32Neon Broadcast(float value)
    {
        PacketVector2Float32Neon row = PacketVector2Float32Neon.Broadcast(value);
        return new(row, row);
    }

    public static PacketMatrix2Float32Neon Select(PacketMatrix2Float32NeonMask mask, PacketMatrix2Float32Neon ifTrue, PacketMatrix2Float32Neon ifFalse)
    {
        return new(
            PacketVector2Float32Neon.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Float32Neon.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1));
    }

    public static PacketMatrix2Float32Neon Select(PacketVector2Float32NeonMask mask, PacketMatrix2Float32Neon ifTrue, PacketMatrix2Float32Neon ifFalse)
    {
        return new(
            PacketVector2Float32Neon.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Float32Neon.Select(mask, ifTrue.Row1, ifFalse.Row1));
    }

    public static PacketMatrix2Float32Neon Select(PacketFloat32NeonMask mask, PacketMatrix2Float32Neon ifTrue, PacketMatrix2Float32Neon ifFalse)
    {
        return new(
            PacketVector2Float32Neon.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Float32Neon.Select(mask, ifTrue.Row1, ifFalse.Row1));
    }

    public static PacketVector2Float32Neon Transform(PacketMatrix2Float32Neon matrix, PacketVector2Float32Neon vector)
    {
        return new(
            PacketFloat32Neon.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, matrix.Row0.X * vector.X),
            PacketFloat32Neon.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, matrix.Row1.X * vector.X));
    }

    public static PacketMatrix2Float32Neon Multiply(PacketMatrix2Float32Neon left, PacketMatrix2Float32Neon right) => left * right;

    public static PacketMatrix2Float32Neon Multiply(PacketMatrix2Float32Neon matrix, PacketFloat32Neon scalar) => matrix * scalar;

    public static PacketMatrix2Float32Neon Multiply(PacketFloat32Neon scalar, PacketMatrix2Float32Neon matrix) => scalar * matrix;

    public static PacketMatrix2Float32Neon FusedMultiplyAdd(PacketMatrix2Float32Neon left, PacketMatrix2Float32Neon right, PacketMatrix2Float32Neon addend)
    {
        return new(
            new PacketVector2Float32Neon(
                PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.X, right.Row0.X, addend.Row0.X)),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.X, right.Row0.Y, addend.Row0.Y))),
            new PacketVector2Float32Neon(
                PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.X, right.Row0.X, addend.Row1.X)),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.X, right.Row0.Y, addend.Row1.Y))));
    }

    public static PacketVector2Float32Neon FusedMultiplyAdd(PacketMatrix2Float32Neon matrix, PacketVector2Float32Neon vector, PacketVector2Float32Neon addend)
    {
        return new(
            PacketFloat32Neon.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row0.X, vector.X, addend.X)),
            PacketFloat32Neon.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row1.X, vector.X, addend.Y)));
    }

    public static PacketMatrix2Float32Neon Transpose(PacketMatrix2Float32Neon matrix)
    {
        return new(
            new(matrix.Row0.X, matrix.Row1.X),
            new(matrix.Row0.Y, matrix.Row1.Y));
    }

    public static PacketMatrix2Float32Neon Scale(PacketVector2Float32Neon scale) => Scale(scale.X, scale.Y);

    public static PacketMatrix2Float32Neon Scale(PacketFloat32Neon x, PacketFloat32Neon y)
    {
        PacketFloat32Neon zero = PacketFloat32Neon.Broadcast(0f);
        return new(
            new(x, zero),
            new(zero, y));
    }

    public static PacketMatrix2Float32Neon operator *(PacketMatrix2Float32Neon left, PacketMatrix2Float32Neon right)
    {
        return new(
            new PacketVector2Float32Neon(
                PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, left.Row0.X * right.Row0.X),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, left.Row0.X * right.Row0.Y)),
            new PacketVector2Float32Neon(
                PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, left.Row1.X * right.Row0.X),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, left.Row1.X * right.Row0.Y)));
    }

    public static PacketVector2Float32Neon operator *(PacketMatrix2Float32Neon matrix, PacketVector2Float32Neon vector) => Transform(matrix, vector);

    public static PacketMatrix2Float32Neon operator *(PacketMatrix2Float32Neon matrix, PacketFloat32Neon scalar)
    {
        PacketVector2Float32Neon scalarVector = new(scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector);
    }

    public static PacketMatrix2Float32Neon operator *(PacketFloat32Neon scalar, PacketMatrix2Float32Neon matrix) => matrix * scalar;
    public static PacketMatrix2Float32Neon Divide(PacketMatrix2Float32Neon matrix, PacketFloat32Neon scalar) => matrix / scalar;

    public static PacketMatrix2Float32Neon operator /(PacketMatrix2Float32Neon matrix, PacketFloat32Neon scalar) => matrix * PacketFloat32Neon.Reciprocal(scalar);

    public static PacketMatrix2Float32Neon Inverse(PacketMatrix2Float32Neon matrix)
    {
        PacketFloat32Neon invDet = PacketFloat32Neon.Reciprocal(PacketFloat32Neon.FusedMultiplyAdd(matrix.Row0.X, matrix.Row1.Y, -(matrix.Row0.Y * matrix.Row1.X)));
        return new(
            new(matrix.Row1.Y * invDet, -matrix.Row0.Y * invDet),
            new(-matrix.Row1.X * invDet, matrix.Row0.X * invDet));
    }

    public static PacketMatrix2Float32Neon InverseTranspose(PacketMatrix2Float32Neon matrix)
    {
        PacketFloat32Neon invDet = PacketFloat32Neon.Reciprocal(PacketFloat32Neon.FusedMultiplyAdd(matrix.Row0.X, matrix.Row1.Y, -(matrix.Row0.Y * matrix.Row1.X)));
        return new(
            new(matrix.Row1.Y * invDet, -matrix.Row1.X * invDet),
            new(-matrix.Row0.Y * invDet, matrix.Row0.X * invDet));
    }

    public static PacketMatrix2Float32Neon Rotate(PacketFloat32Neon angle)
    {
        var (sin, cos) = PacketFloat32Neon.SinCos(angle);
        return new(
            new(cos, -sin),
            new(sin, cos));
    }

    public static PacketMatrix2Float32NeonMask operator ==(PacketMatrix2Float32Neon left, PacketMatrix2Float32Neon right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1);

    public static PacketMatrix2Float32NeonMask operator !=(PacketMatrix2Float32Neon left, PacketMatrix2Float32Neon right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1);

    public static PacketMatrix2Float32NeonMask operator <(PacketMatrix2Float32Neon left, PacketMatrix2Float32Neon right) => new(
        new PacketVector2Float32NeonMask(left.Row0.X < right.Row0.X, left.Row0.Y < right.Row0.Y),
        new PacketVector2Float32NeonMask(left.Row1.X < right.Row1.X, left.Row1.Y < right.Row1.Y));

    public static PacketMatrix2Float32NeonMask operator >(PacketMatrix2Float32Neon left, PacketMatrix2Float32Neon right) => new(
        new PacketVector2Float32NeonMask(left.Row0.X > right.Row0.X, left.Row0.Y > right.Row0.Y),
        new PacketVector2Float32NeonMask(left.Row1.X > right.Row1.X, left.Row1.Y > right.Row1.Y));

    public static PacketMatrix2Float32NeonMask operator <=(PacketMatrix2Float32Neon left, PacketMatrix2Float32Neon right) => new(
        new PacketVector2Float32NeonMask(left.Row0.X <= right.Row0.X, left.Row0.Y <= right.Row0.Y),
        new PacketVector2Float32NeonMask(left.Row1.X <= right.Row1.X, left.Row1.Y <= right.Row1.Y));

    public static PacketMatrix2Float32NeonMask operator >=(PacketMatrix2Float32Neon left, PacketMatrix2Float32Neon right) => new(
        new PacketVector2Float32NeonMask(left.Row0.X >= right.Row0.X, left.Row0.Y >= right.Row0.Y),
        new PacketVector2Float32NeonMask(left.Row1.X >= right.Row1.X, left.Row1.Y >= right.Row1.Y));

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix2Float32Neon other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1);
}

public readonly struct PacketMatrix2Float32NeonMask :
    ISimdMatrix2Mask<PacketMatrix2Float32NeonMask, PacketVector2Float32NeonMask, PacketFloat32NeonMask>
{
    public PacketMatrix2Float32NeonMask(PacketVector2Float32NeonMask row0, PacketVector2Float32NeonMask row1)
    {
        Row0 = row0;
        Row1 = row1;
    }

    public static int LaneCount => PacketVector2Float32NeonMask.LaneCount;

    public PacketVector2Float32NeonMask Row0 { get; }

    public PacketVector2Float32NeonMask Row1 { get; }

    public static PacketMatrix2Float32NeonMask True => new(PacketVector2Float32NeonMask.True, PacketVector2Float32NeonMask.True);

    public static PacketMatrix2Float32NeonMask False => new(PacketVector2Float32NeonMask.False, PacketVector2Float32NeonMask.False);

    public static PacketMatrix2Float32NeonMask Create(PacketVector2Float32NeonMask row0, PacketVector2Float32NeonMask row1) => new(row0, row1);

    public static PacketMatrix2Float32NeonMask Broadcast(PacketVector2Float32NeonMask value) => new(value, value);

    public static PacketVector2Float32NeonMask All(PacketMatrix2Float32NeonMask value) => value.Row0 & value.Row1;

    public static PacketVector2Float32NeonMask Any(PacketMatrix2Float32NeonMask value) => value.Row0 | value.Row1;

    public static PacketVector2Float32NeonMask None(PacketMatrix2Float32NeonMask value) => ~(value.Row0 | value.Row1);

    public static PacketMatrix2Float32NeonMask AndNot(PacketMatrix2Float32NeonMask left, PacketMatrix2Float32NeonMask right)
    {
        return new(
            PacketVector2Float32NeonMask.AndNot(left.Row0, right.Row0),
            PacketVector2Float32NeonMask.AndNot(left.Row1, right.Row1));
    }

    public static PacketMatrix2Float32NeonMask Select(PacketMatrix2Float32NeonMask mask, PacketMatrix2Float32NeonMask ifTrue, PacketMatrix2Float32NeonMask ifFalse)
    {
        return new(
            PacketVector2Float32NeonMask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Float32NeonMask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1));
    }
    public static PacketMatrix2Float32NeonMask And(PacketMatrix2Float32NeonMask left, PacketMatrix2Float32NeonMask right) => left & right;

    public static PacketMatrix2Float32NeonMask Or(PacketMatrix2Float32NeonMask left, PacketMatrix2Float32NeonMask right) => left | right;

    public static PacketMatrix2Float32NeonMask Xor(PacketMatrix2Float32NeonMask left, PacketMatrix2Float32NeonMask right) => left ^ right;

    public static PacketMatrix2Float32NeonMask Not(PacketMatrix2Float32NeonMask value) => ~value;


    public static PacketMatrix2Float32NeonMask operator &(PacketMatrix2Float32NeonMask left, PacketMatrix2Float32NeonMask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1);

    public static PacketMatrix2Float32NeonMask operator |(PacketMatrix2Float32NeonMask left, PacketMatrix2Float32NeonMask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1);

    public static PacketMatrix2Float32NeonMask operator ^(PacketMatrix2Float32NeonMask left, PacketMatrix2Float32NeonMask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1);

    public static PacketMatrix2Float32NeonMask operator ~(PacketMatrix2Float32NeonMask value) => new(~value.Row0, ~value.Row1);

    public static PacketMatrix2Float32NeonMask operator ==(PacketMatrix2Float32NeonMask left, PacketMatrix2Float32NeonMask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1);

    public static PacketMatrix2Float32NeonMask operator !=(PacketMatrix2Float32NeonMask left, PacketMatrix2Float32NeonMask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix2Float32NeonMask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1);
}
