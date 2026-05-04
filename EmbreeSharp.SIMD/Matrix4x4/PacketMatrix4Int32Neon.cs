namespace EmbreeSharp.SIMD;

public readonly struct PacketMatrix4Int32Neon :
    ISimdIntegerMatrix4<PacketMatrix4Int32Neon, PacketVector4Int32Neon, PacketInt32Neon, int, PacketMatrix4Int32NeonMask, PacketVector4Int32NeonMask, PacketInt32NeonMask>
{
    public PacketMatrix4Int32Neon(PacketVector4Int32Neon row0, PacketVector4Int32Neon row1, PacketVector4Int32Neon row2, PacketVector4Int32Neon row3)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
        Row3 = row3;
    }

    public static int LaneCount => PacketVector4Int32Neon.LaneCount;

    public PacketVector4Int32Neon Row0 { get; }

    public PacketVector4Int32Neon Row1 { get; }

    public PacketVector4Int32Neon Row2 { get; }

    public PacketVector4Int32Neon Row3 { get; }

    public static PacketMatrix4Int32Neon Identity
    {
        get
        {
            PacketInt32Neon zero = PacketInt32Neon.Broadcast(0);
            PacketInt32Neon one = PacketInt32Neon.Broadcast(1);
            return new(
                new PacketVector4Int32Neon(one, zero, zero, zero),
                new PacketVector4Int32Neon(zero, one, zero, zero),
                new PacketVector4Int32Neon(zero, zero, one, zero),
                new PacketVector4Int32Neon(zero, zero, zero, one));
        }
    }

    public static PacketMatrix4Int32Neon Create(PacketVector4Int32Neon row0, PacketVector4Int32Neon row1, PacketVector4Int32Neon row2, PacketVector4Int32Neon row3) => new(row0, row1, row2, row3);

    public static PacketMatrix4Int32Neon Broadcast(int value)
    {
        PacketVector4Int32Neon row = PacketVector4Int32Neon.Broadcast(value);
        return new(row, row, row, row);
    }

    public static PacketMatrix4Int32Neon Select(PacketMatrix4Int32NeonMask mask, PacketMatrix4Int32Neon ifTrue, PacketMatrix4Int32Neon ifFalse)
    {
        return new(
            PacketVector4Int32Neon.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Int32Neon.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Int32Neon.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Int32Neon.Select(mask.Row3, ifTrue.Row3, ifFalse.Row3));
    }

    public static PacketMatrix4Int32Neon Select(PacketVector4Int32NeonMask mask, PacketMatrix4Int32Neon ifTrue, PacketMatrix4Int32Neon ifFalse)
    {
        return new(
            PacketVector4Int32Neon.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Int32Neon.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Int32Neon.Select(mask, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Int32Neon.Select(mask, ifTrue.Row3, ifFalse.Row3));
    }

    public static PacketMatrix4Int32Neon Select(PacketInt32NeonMask mask, PacketMatrix4Int32Neon ifTrue, PacketMatrix4Int32Neon ifFalse)
    {
        return new(
            PacketVector4Int32Neon.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Int32Neon.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Int32Neon.Select(mask, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Int32Neon.Select(mask, ifTrue.Row3, ifFalse.Row3));
    }

    public static PacketMatrix4Int32Neon Multiply(PacketMatrix4Int32Neon left, PacketMatrix4Int32Neon right) => left * right;

    public static PacketMatrix4Int32Neon Multiply(PacketMatrix4Int32Neon matrix, PacketInt32Neon scalar) => matrix * scalar;

    public static PacketMatrix4Int32Neon Multiply(PacketInt32Neon scalar, PacketMatrix4Int32Neon matrix) => scalar * matrix;

    public static PacketMatrix4Int32Neon operator *(PacketMatrix4Int32Neon left, PacketMatrix4Int32Neon right)
    {
        return new(
            new PacketVector4Int32Neon(
                left.Row0.X * right.Row0.X + left.Row0.Y * right.Row1.X + left.Row0.Z * right.Row2.X + left.Row0.W * right.Row3.X,
                left.Row0.X * right.Row0.Y + left.Row0.Y * right.Row1.Y + left.Row0.Z * right.Row2.Y + left.Row0.W * right.Row3.Y,
                left.Row0.X * right.Row0.Z + left.Row0.Y * right.Row1.Z + left.Row0.Z * right.Row2.Z + left.Row0.W * right.Row3.Z,
                left.Row0.X * right.Row0.W + left.Row0.Y * right.Row1.W + left.Row0.Z * right.Row2.W + left.Row0.W * right.Row3.W),
            new PacketVector4Int32Neon(
                left.Row1.X * right.Row0.X + left.Row1.Y * right.Row1.X + left.Row1.Z * right.Row2.X + left.Row1.W * right.Row3.X,
                left.Row1.X * right.Row0.Y + left.Row1.Y * right.Row1.Y + left.Row1.Z * right.Row2.Y + left.Row1.W * right.Row3.Y,
                left.Row1.X * right.Row0.Z + left.Row1.Y * right.Row1.Z + left.Row1.Z * right.Row2.Z + left.Row1.W * right.Row3.Z,
                left.Row1.X * right.Row0.W + left.Row1.Y * right.Row1.W + left.Row1.Z * right.Row2.W + left.Row1.W * right.Row3.W),
            new PacketVector4Int32Neon(
                left.Row2.X * right.Row0.X + left.Row2.Y * right.Row1.X + left.Row2.Z * right.Row2.X + left.Row2.W * right.Row3.X,
                left.Row2.X * right.Row0.Y + left.Row2.Y * right.Row1.Y + left.Row2.Z * right.Row2.Y + left.Row2.W * right.Row3.Y,
                left.Row2.X * right.Row0.Z + left.Row2.Y * right.Row1.Z + left.Row2.Z * right.Row2.Z + left.Row2.W * right.Row3.Z,
                left.Row2.X * right.Row0.W + left.Row2.Y * right.Row1.W + left.Row2.Z * right.Row2.W + left.Row2.W * right.Row3.W),
            new PacketVector4Int32Neon(
                left.Row3.X * right.Row0.X + left.Row3.Y * right.Row1.X + left.Row3.Z * right.Row2.X + left.Row3.W * right.Row3.X,
                left.Row3.X * right.Row0.Y + left.Row3.Y * right.Row1.Y + left.Row3.Z * right.Row2.Y + left.Row3.W * right.Row3.Y,
                left.Row3.X * right.Row0.Z + left.Row3.Y * right.Row1.Z + left.Row3.Z * right.Row2.Z + left.Row3.W * right.Row3.Z,
                left.Row3.X * right.Row0.W + left.Row3.Y * right.Row1.W + left.Row3.Z * right.Row2.W + left.Row3.W * right.Row3.W));
    }

    public static PacketVector4Int32Neon operator *(PacketMatrix4Int32Neon matrix, PacketVector4Int32Neon vector)
    {
        return new(
            matrix.Row0.X * vector.X + matrix.Row0.Y * vector.Y + matrix.Row0.Z * vector.Z + matrix.Row0.W * vector.W,
            matrix.Row1.X * vector.X + matrix.Row1.Y * vector.Y + matrix.Row1.Z * vector.Z + matrix.Row1.W * vector.W,
            matrix.Row2.X * vector.X + matrix.Row2.Y * vector.Y + matrix.Row2.Z * vector.Z + matrix.Row2.W * vector.W,
            matrix.Row3.X * vector.X + matrix.Row3.Y * vector.Y + matrix.Row3.Z * vector.Z + matrix.Row3.W * vector.W);
    }

    public static PacketMatrix4Int32Neon operator *(PacketMatrix4Int32Neon matrix, PacketInt32Neon scalar)
    {
        PacketVector4Int32Neon scalarVector = new(scalar, scalar, scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector, matrix.Row2 * scalarVector, matrix.Row3 * scalarVector);
    }

    public static PacketMatrix4Int32Neon operator *(PacketInt32Neon scalar, PacketMatrix4Int32Neon matrix) => matrix * scalar;

    public static PacketMatrix4Int32NeonMask operator ==(PacketMatrix4Int32Neon left, PacketMatrix4Int32Neon right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2, left.Row3 == right.Row3);

    public static PacketMatrix4Int32NeonMask operator !=(PacketMatrix4Int32Neon left, PacketMatrix4Int32Neon right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2, left.Row3 != right.Row3);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix4Int32Neon other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2) &&
               Row3.Equals(other.Row3);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2, Row3);
}

public readonly struct PacketMatrix4Int32NeonMask :
    ISimdMatrix4Mask<PacketMatrix4Int32NeonMask, PacketVector4Int32NeonMask, PacketInt32NeonMask>
{
    public PacketMatrix4Int32NeonMask(PacketVector4Int32NeonMask row0, PacketVector4Int32NeonMask row1, PacketVector4Int32NeonMask row2, PacketVector4Int32NeonMask row3)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
        Row3 = row3;
    }

    public static int LaneCount => PacketVector4Int32NeonMask.LaneCount;

    public PacketVector4Int32NeonMask Row0 { get; }

    public PacketVector4Int32NeonMask Row1 { get; }

    public PacketVector4Int32NeonMask Row2 { get; }

    public PacketVector4Int32NeonMask Row3 { get; }

    public static PacketMatrix4Int32NeonMask True => new(PacketVector4Int32NeonMask.True, PacketVector4Int32NeonMask.True, PacketVector4Int32NeonMask.True, PacketVector4Int32NeonMask.True);

    public static PacketMatrix4Int32NeonMask False => new(PacketVector4Int32NeonMask.False, PacketVector4Int32NeonMask.False, PacketVector4Int32NeonMask.False, PacketVector4Int32NeonMask.False);

    public static PacketMatrix4Int32NeonMask Create(PacketVector4Int32NeonMask row0, PacketVector4Int32NeonMask row1, PacketVector4Int32NeonMask row2, PacketVector4Int32NeonMask row3) => new(row0, row1, row2, row3);

    public static PacketMatrix4Int32NeonMask Broadcast(PacketVector4Int32NeonMask value) => new(value, value, value, value);

    public static PacketVector4Int32NeonMask All(PacketMatrix4Int32NeonMask value) => value.Row0 & value.Row1 & value.Row2 & value.Row3;

    public static PacketVector4Int32NeonMask Any(PacketMatrix4Int32NeonMask value) => value.Row0 | value.Row1 | value.Row2 | value.Row3;

    public static PacketVector4Int32NeonMask None(PacketMatrix4Int32NeonMask value) => ~(value.Row0 | value.Row1 | value.Row2 | value.Row3);

    public static PacketMatrix4Int32NeonMask AndNot(PacketMatrix4Int32NeonMask left, PacketMatrix4Int32NeonMask right)
    {
        return new(
            PacketVector4Int32NeonMask.AndNot(left.Row0, right.Row0),
            PacketVector4Int32NeonMask.AndNot(left.Row1, right.Row1),
            PacketVector4Int32NeonMask.AndNot(left.Row2, right.Row2),
            PacketVector4Int32NeonMask.AndNot(left.Row3, right.Row3));
    }

    public static PacketMatrix4Int32NeonMask Select(PacketMatrix4Int32NeonMask mask, PacketMatrix4Int32NeonMask ifTrue, PacketMatrix4Int32NeonMask ifFalse)
    {
        return new(
            PacketVector4Int32NeonMask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Int32NeonMask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Int32NeonMask.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Int32NeonMask.Select(mask.Row3, ifTrue.Row3, ifFalse.Row3));
    }
    public static PacketMatrix4Int32NeonMask And(PacketMatrix4Int32NeonMask left, PacketMatrix4Int32NeonMask right) => left & right;

    public static PacketMatrix4Int32NeonMask Or(PacketMatrix4Int32NeonMask left, PacketMatrix4Int32NeonMask right) => left | right;

    public static PacketMatrix4Int32NeonMask Xor(PacketMatrix4Int32NeonMask left, PacketMatrix4Int32NeonMask right) => left ^ right;

    public static PacketMatrix4Int32NeonMask Not(PacketMatrix4Int32NeonMask value) => ~value;


    public static PacketMatrix4Int32NeonMask operator &(PacketMatrix4Int32NeonMask left, PacketMatrix4Int32NeonMask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1, left.Row2 & right.Row2, left.Row3 & right.Row3);

    public static PacketMatrix4Int32NeonMask operator |(PacketMatrix4Int32NeonMask left, PacketMatrix4Int32NeonMask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1, left.Row2 | right.Row2, left.Row3 | right.Row3);

    public static PacketMatrix4Int32NeonMask operator ^(PacketMatrix4Int32NeonMask left, PacketMatrix4Int32NeonMask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1, left.Row2 ^ right.Row2, left.Row3 ^ right.Row3);

    public static PacketMatrix4Int32NeonMask operator ~(PacketMatrix4Int32NeonMask value) => new(~value.Row0, ~value.Row1, ~value.Row2, ~value.Row3);

    public static PacketMatrix4Int32NeonMask operator ==(PacketMatrix4Int32NeonMask left, PacketMatrix4Int32NeonMask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2, left.Row3 == right.Row3);

    public static PacketMatrix4Int32NeonMask operator !=(PacketMatrix4Int32NeonMask left, PacketMatrix4Int32NeonMask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2, left.Row3 != right.Row3);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix4Int32NeonMask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2) &&
               Row3.Equals(other.Row3);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2, Row3);
}
