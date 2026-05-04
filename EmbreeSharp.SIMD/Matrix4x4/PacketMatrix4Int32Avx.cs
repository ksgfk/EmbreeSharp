namespace EmbreeSharp.SIMD;

public readonly struct PacketMatrix4Int32Avx :
    ISimdIntegerMatrix4<PacketMatrix4Int32Avx, PacketVector4Int32Avx, PacketInt32Avx, int, PacketMatrix4Int32AvxMask, PacketVector4Int32AvxMask, PacketInt32AvxMask>
{
    public PacketMatrix4Int32Avx(PacketVector4Int32Avx row0, PacketVector4Int32Avx row1, PacketVector4Int32Avx row2, PacketVector4Int32Avx row3)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
        Row3 = row3;
    }

    public static int LaneCount => PacketVector4Int32Avx.LaneCount;

    public PacketVector4Int32Avx Row0 { get; }

    public PacketVector4Int32Avx Row1 { get; }

    public PacketVector4Int32Avx Row2 { get; }

    public PacketVector4Int32Avx Row3 { get; }

    public static PacketMatrix4Int32Avx Identity
    {
        get
        {
            PacketInt32Avx zero = PacketInt32Avx.Broadcast(0);
            PacketInt32Avx one = PacketInt32Avx.Broadcast(1);
            return new(
                new PacketVector4Int32Avx(one, zero, zero, zero),
                new PacketVector4Int32Avx(zero, one, zero, zero),
                new PacketVector4Int32Avx(zero, zero, one, zero),
                new PacketVector4Int32Avx(zero, zero, zero, one));
        }
    }

    public static PacketMatrix4Int32Avx Create(PacketVector4Int32Avx row0, PacketVector4Int32Avx row1, PacketVector4Int32Avx row2, PacketVector4Int32Avx row3) => new(row0, row1, row2, row3);

    public static PacketMatrix4Int32Avx Broadcast(int value)
    {
        PacketVector4Int32Avx row = PacketVector4Int32Avx.Broadcast(value);
        return new(row, row, row, row);
    }

    public static PacketMatrix4Int32Avx Select(PacketMatrix4Int32AvxMask mask, PacketMatrix4Int32Avx ifTrue, PacketMatrix4Int32Avx ifFalse)
    {
        return new(
            PacketVector4Int32Avx.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Int32Avx.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Int32Avx.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Int32Avx.Select(mask.Row3, ifTrue.Row3, ifFalse.Row3));
    }

    public static PacketMatrix4Int32Avx Select(PacketVector4Int32AvxMask mask, PacketMatrix4Int32Avx ifTrue, PacketMatrix4Int32Avx ifFalse)
    {
        return new(
            PacketVector4Int32Avx.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Int32Avx.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Int32Avx.Select(mask, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Int32Avx.Select(mask, ifTrue.Row3, ifFalse.Row3));
    }

    public static PacketMatrix4Int32Avx Select(PacketInt32AvxMask mask, PacketMatrix4Int32Avx ifTrue, PacketMatrix4Int32Avx ifFalse)
    {
        return new(
            PacketVector4Int32Avx.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Int32Avx.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Int32Avx.Select(mask, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Int32Avx.Select(mask, ifTrue.Row3, ifFalse.Row3));
    }

    public static PacketMatrix4Int32Avx Multiply(PacketMatrix4Int32Avx left, PacketMatrix4Int32Avx right) => left * right;

    public static PacketMatrix4Int32Avx Multiply(PacketMatrix4Int32Avx matrix, PacketInt32Avx scalar) => matrix * scalar;

    public static PacketMatrix4Int32Avx Multiply(PacketInt32Avx scalar, PacketMatrix4Int32Avx matrix) => scalar * matrix;

    public static PacketMatrix4Int32Avx operator *(PacketMatrix4Int32Avx left, PacketMatrix4Int32Avx right)
    {
        return new(
            new PacketVector4Int32Avx(
                left.Row0.X * right.Row0.X + left.Row0.Y * right.Row1.X + left.Row0.Z * right.Row2.X + left.Row0.W * right.Row3.X,
                left.Row0.X * right.Row0.Y + left.Row0.Y * right.Row1.Y + left.Row0.Z * right.Row2.Y + left.Row0.W * right.Row3.Y,
                left.Row0.X * right.Row0.Z + left.Row0.Y * right.Row1.Z + left.Row0.Z * right.Row2.Z + left.Row0.W * right.Row3.Z,
                left.Row0.X * right.Row0.W + left.Row0.Y * right.Row1.W + left.Row0.Z * right.Row2.W + left.Row0.W * right.Row3.W),
            new PacketVector4Int32Avx(
                left.Row1.X * right.Row0.X + left.Row1.Y * right.Row1.X + left.Row1.Z * right.Row2.X + left.Row1.W * right.Row3.X,
                left.Row1.X * right.Row0.Y + left.Row1.Y * right.Row1.Y + left.Row1.Z * right.Row2.Y + left.Row1.W * right.Row3.Y,
                left.Row1.X * right.Row0.Z + left.Row1.Y * right.Row1.Z + left.Row1.Z * right.Row2.Z + left.Row1.W * right.Row3.Z,
                left.Row1.X * right.Row0.W + left.Row1.Y * right.Row1.W + left.Row1.Z * right.Row2.W + left.Row1.W * right.Row3.W),
            new PacketVector4Int32Avx(
                left.Row2.X * right.Row0.X + left.Row2.Y * right.Row1.X + left.Row2.Z * right.Row2.X + left.Row2.W * right.Row3.X,
                left.Row2.X * right.Row0.Y + left.Row2.Y * right.Row1.Y + left.Row2.Z * right.Row2.Y + left.Row2.W * right.Row3.Y,
                left.Row2.X * right.Row0.Z + left.Row2.Y * right.Row1.Z + left.Row2.Z * right.Row2.Z + left.Row2.W * right.Row3.Z,
                left.Row2.X * right.Row0.W + left.Row2.Y * right.Row1.W + left.Row2.Z * right.Row2.W + left.Row2.W * right.Row3.W),
            new PacketVector4Int32Avx(
                left.Row3.X * right.Row0.X + left.Row3.Y * right.Row1.X + left.Row3.Z * right.Row2.X + left.Row3.W * right.Row3.X,
                left.Row3.X * right.Row0.Y + left.Row3.Y * right.Row1.Y + left.Row3.Z * right.Row2.Y + left.Row3.W * right.Row3.Y,
                left.Row3.X * right.Row0.Z + left.Row3.Y * right.Row1.Z + left.Row3.Z * right.Row2.Z + left.Row3.W * right.Row3.Z,
                left.Row3.X * right.Row0.W + left.Row3.Y * right.Row1.W + left.Row3.Z * right.Row2.W + left.Row3.W * right.Row3.W));
    }

    public static PacketVector4Int32Avx operator *(PacketMatrix4Int32Avx matrix, PacketVector4Int32Avx vector)
    {
        return new(
            matrix.Row0.X * vector.X + matrix.Row0.Y * vector.Y + matrix.Row0.Z * vector.Z + matrix.Row0.W * vector.W,
            matrix.Row1.X * vector.X + matrix.Row1.Y * vector.Y + matrix.Row1.Z * vector.Z + matrix.Row1.W * vector.W,
            matrix.Row2.X * vector.X + matrix.Row2.Y * vector.Y + matrix.Row2.Z * vector.Z + matrix.Row2.W * vector.W,
            matrix.Row3.X * vector.X + matrix.Row3.Y * vector.Y + matrix.Row3.Z * vector.Z + matrix.Row3.W * vector.W);
    }

    public static PacketMatrix4Int32Avx operator *(PacketMatrix4Int32Avx matrix, PacketInt32Avx scalar)
    {
        PacketVector4Int32Avx scalarVector = new(scalar, scalar, scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector, matrix.Row2 * scalarVector, matrix.Row3 * scalarVector);
    }

    public static PacketMatrix4Int32Avx operator *(PacketInt32Avx scalar, PacketMatrix4Int32Avx matrix) => matrix * scalar;

    public static PacketMatrix4Int32AvxMask operator ==(PacketMatrix4Int32Avx left, PacketMatrix4Int32Avx right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2, left.Row3 == right.Row3);

    public static PacketMatrix4Int32AvxMask operator !=(PacketMatrix4Int32Avx left, PacketMatrix4Int32Avx right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2, left.Row3 != right.Row3);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix4Int32Avx other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2) &&
               Row3.Equals(other.Row3);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2, Row3);
}

public readonly struct PacketMatrix4Int32AvxMask :
    ISimdMatrix4Mask<PacketMatrix4Int32AvxMask, PacketVector4Int32AvxMask, PacketInt32AvxMask>
{
    public PacketMatrix4Int32AvxMask(PacketVector4Int32AvxMask row0, PacketVector4Int32AvxMask row1, PacketVector4Int32AvxMask row2, PacketVector4Int32AvxMask row3)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
        Row3 = row3;
    }

    public static int LaneCount => PacketVector4Int32AvxMask.LaneCount;

    public PacketVector4Int32AvxMask Row0 { get; }

    public PacketVector4Int32AvxMask Row1 { get; }

    public PacketVector4Int32AvxMask Row2 { get; }

    public PacketVector4Int32AvxMask Row3 { get; }

    public static PacketMatrix4Int32AvxMask True => new(PacketVector4Int32AvxMask.True, PacketVector4Int32AvxMask.True, PacketVector4Int32AvxMask.True, PacketVector4Int32AvxMask.True);

    public static PacketMatrix4Int32AvxMask False => new(PacketVector4Int32AvxMask.False, PacketVector4Int32AvxMask.False, PacketVector4Int32AvxMask.False, PacketVector4Int32AvxMask.False);

    public static PacketMatrix4Int32AvxMask Create(PacketVector4Int32AvxMask row0, PacketVector4Int32AvxMask row1, PacketVector4Int32AvxMask row2, PacketVector4Int32AvxMask row3) => new(row0, row1, row2, row3);

    public static PacketMatrix4Int32AvxMask Broadcast(PacketVector4Int32AvxMask value) => new(value, value, value, value);

    public static PacketVector4Int32AvxMask All(PacketMatrix4Int32AvxMask value) => value.Row0 & value.Row1 & value.Row2 & value.Row3;

    public static PacketVector4Int32AvxMask Any(PacketMatrix4Int32AvxMask value) => value.Row0 | value.Row1 | value.Row2 | value.Row3;

    public static PacketVector4Int32AvxMask None(PacketMatrix4Int32AvxMask value) => ~(value.Row0 | value.Row1 | value.Row2 | value.Row3);

    public static PacketMatrix4Int32AvxMask AndNot(PacketMatrix4Int32AvxMask left, PacketMatrix4Int32AvxMask right)
    {
        return new(
            PacketVector4Int32AvxMask.AndNot(left.Row0, right.Row0),
            PacketVector4Int32AvxMask.AndNot(left.Row1, right.Row1),
            PacketVector4Int32AvxMask.AndNot(left.Row2, right.Row2),
            PacketVector4Int32AvxMask.AndNot(left.Row3, right.Row3));
    }

    public static PacketMatrix4Int32AvxMask Select(PacketMatrix4Int32AvxMask mask, PacketMatrix4Int32AvxMask ifTrue, PacketMatrix4Int32AvxMask ifFalse)
    {
        return new(
            PacketVector4Int32AvxMask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Int32AvxMask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Int32AvxMask.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Int32AvxMask.Select(mask.Row3, ifTrue.Row3, ifFalse.Row3));
    }
    public static PacketMatrix4Int32AvxMask And(PacketMatrix4Int32AvxMask left, PacketMatrix4Int32AvxMask right) => left & right;

    public static PacketMatrix4Int32AvxMask Or(PacketMatrix4Int32AvxMask left, PacketMatrix4Int32AvxMask right) => left | right;

    public static PacketMatrix4Int32AvxMask Xor(PacketMatrix4Int32AvxMask left, PacketMatrix4Int32AvxMask right) => left ^ right;

    public static PacketMatrix4Int32AvxMask Not(PacketMatrix4Int32AvxMask value) => ~value;


    public static PacketMatrix4Int32AvxMask operator &(PacketMatrix4Int32AvxMask left, PacketMatrix4Int32AvxMask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1, left.Row2 & right.Row2, left.Row3 & right.Row3);

    public static PacketMatrix4Int32AvxMask operator |(PacketMatrix4Int32AvxMask left, PacketMatrix4Int32AvxMask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1, left.Row2 | right.Row2, left.Row3 | right.Row3);

    public static PacketMatrix4Int32AvxMask operator ^(PacketMatrix4Int32AvxMask left, PacketMatrix4Int32AvxMask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1, left.Row2 ^ right.Row2, left.Row3 ^ right.Row3);

    public static PacketMatrix4Int32AvxMask operator ~(PacketMatrix4Int32AvxMask value) => new(~value.Row0, ~value.Row1, ~value.Row2, ~value.Row3);

    public static PacketMatrix4Int32AvxMask operator ==(PacketMatrix4Int32AvxMask left, PacketMatrix4Int32AvxMask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2, left.Row3 == right.Row3);

    public static PacketMatrix4Int32AvxMask operator !=(PacketMatrix4Int32AvxMask left, PacketMatrix4Int32AvxMask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2, left.Row3 != right.Row3);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix4Int32AvxMask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2) &&
               Row3.Equals(other.Row3);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2, Row3);
}
