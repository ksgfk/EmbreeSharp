namespace EmbreeSharp.SIMD;

public readonly struct PacketMatrix3Int32Neon :
    ISimdIntegerMatrix3<PacketMatrix3Int32Neon, PacketVector3Int32Neon, PacketInt32Neon, int, PacketMatrix3Int32NeonMask, PacketVector3Int32NeonMask, PacketInt32NeonMask>
{
    public PacketMatrix3Int32Neon(PacketVector3Int32Neon row0, PacketVector3Int32Neon row1, PacketVector3Int32Neon row2)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
    }

    public static int LaneCount => PacketVector3Int32Neon.LaneCount;

    public PacketVector3Int32Neon Row0 { get; }

    public PacketVector3Int32Neon Row1 { get; }

    public PacketVector3Int32Neon Row2 { get; }

    public static PacketMatrix3Int32Neon Identity
    {
        get
        {
            PacketInt32Neon zero = PacketInt32Neon.Broadcast(0);
            PacketInt32Neon one = PacketInt32Neon.Broadcast(1);
            return new(
                new PacketVector3Int32Neon(one, zero, zero),
                new PacketVector3Int32Neon(zero, one, zero),
                new PacketVector3Int32Neon(zero, zero, one));
        }
    }

    public static PacketMatrix3Int32Neon Create(PacketVector3Int32Neon row0, PacketVector3Int32Neon row1, PacketVector3Int32Neon row2) => new(row0, row1, row2);

    public static PacketMatrix3Int32Neon Broadcast(int value)
    {
        PacketVector3Int32Neon row = PacketVector3Int32Neon.Broadcast(value);
        return new(row, row, row);
    }

    public static PacketMatrix3Int32Neon Select(PacketMatrix3Int32NeonMask mask, PacketMatrix3Int32Neon ifTrue, PacketMatrix3Int32Neon ifFalse)
    {
        return new(
            PacketVector3Int32Neon.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Int32Neon.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Int32Neon.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketMatrix3Int32Neon Select(PacketVector3Int32NeonMask mask, PacketMatrix3Int32Neon ifTrue, PacketMatrix3Int32Neon ifFalse)
    {
        return new(
            PacketVector3Int32Neon.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Int32Neon.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Int32Neon.Select(mask, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketMatrix3Int32Neon Select(PacketInt32NeonMask mask, PacketMatrix3Int32Neon ifTrue, PacketMatrix3Int32Neon ifFalse)
    {
        return new(
            PacketVector3Int32Neon.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Int32Neon.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Int32Neon.Select(mask, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketMatrix3Int32Neon Multiply(PacketMatrix3Int32Neon left, PacketMatrix3Int32Neon right) => left * right;

    public static PacketMatrix3Int32Neon Multiply(PacketMatrix3Int32Neon matrix, PacketInt32Neon scalar) => matrix * scalar;

    public static PacketMatrix3Int32Neon Multiply(PacketInt32Neon scalar, PacketMatrix3Int32Neon matrix) => scalar * matrix;

    public static PacketMatrix3Int32Neon operator *(PacketMatrix3Int32Neon left, PacketMatrix3Int32Neon right)
    {
        return new(
            new PacketVector3Int32Neon(
                left.Row0.X * right.Row0.X + left.Row0.Y * right.Row1.X + left.Row0.Z * right.Row2.X,
                left.Row0.X * right.Row0.Y + left.Row0.Y * right.Row1.Y + left.Row0.Z * right.Row2.Y,
                left.Row0.X * right.Row0.Z + left.Row0.Y * right.Row1.Z + left.Row0.Z * right.Row2.Z),
            new PacketVector3Int32Neon(
                left.Row1.X * right.Row0.X + left.Row1.Y * right.Row1.X + left.Row1.Z * right.Row2.X,
                left.Row1.X * right.Row0.Y + left.Row1.Y * right.Row1.Y + left.Row1.Z * right.Row2.Y,
                left.Row1.X * right.Row0.Z + left.Row1.Y * right.Row1.Z + left.Row1.Z * right.Row2.Z),
            new PacketVector3Int32Neon(
                left.Row2.X * right.Row0.X + left.Row2.Y * right.Row1.X + left.Row2.Z * right.Row2.X,
                left.Row2.X * right.Row0.Y + left.Row2.Y * right.Row1.Y + left.Row2.Z * right.Row2.Y,
                left.Row2.X * right.Row0.Z + left.Row2.Y * right.Row1.Z + left.Row2.Z * right.Row2.Z));
    }

    public static PacketVector3Int32Neon operator *(PacketMatrix3Int32Neon matrix, PacketVector3Int32Neon vector)
    {
        return new(
            matrix.Row0.X * vector.X + matrix.Row0.Y * vector.Y + matrix.Row0.Z * vector.Z,
            matrix.Row1.X * vector.X + matrix.Row1.Y * vector.Y + matrix.Row1.Z * vector.Z,
            matrix.Row2.X * vector.X + matrix.Row2.Y * vector.Y + matrix.Row2.Z * vector.Z);
    }

    public static PacketMatrix3Int32Neon operator *(PacketMatrix3Int32Neon matrix, PacketInt32Neon scalar)
    {
        PacketVector3Int32Neon scalarVector = new(scalar, scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector, matrix.Row2 * scalarVector);
    }

    public static PacketMatrix3Int32Neon operator *(PacketInt32Neon scalar, PacketMatrix3Int32Neon matrix) => matrix * scalar;

    public static PacketMatrix3Int32NeonMask operator ==(PacketMatrix3Int32Neon left, PacketMatrix3Int32Neon right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2);

    public static PacketMatrix3Int32NeonMask operator !=(PacketMatrix3Int32Neon left, PacketMatrix3Int32Neon right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix3Int32Neon other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2);
}

public readonly struct PacketMatrix3Int32NeonMask :
    ISimdMatrix3Mask<PacketMatrix3Int32NeonMask, PacketVector3Int32NeonMask, PacketInt32NeonMask>
{
    public PacketMatrix3Int32NeonMask(PacketVector3Int32NeonMask row0, PacketVector3Int32NeonMask row1, PacketVector3Int32NeonMask row2)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
    }

    public static int LaneCount => PacketVector3Int32NeonMask.LaneCount;

    public PacketVector3Int32NeonMask Row0 { get; }

    public PacketVector3Int32NeonMask Row1 { get; }

    public PacketVector3Int32NeonMask Row2 { get; }

    public static PacketMatrix3Int32NeonMask True => new(PacketVector3Int32NeonMask.True, PacketVector3Int32NeonMask.True, PacketVector3Int32NeonMask.True);

    public static PacketMatrix3Int32NeonMask False => new(PacketVector3Int32NeonMask.False, PacketVector3Int32NeonMask.False, PacketVector3Int32NeonMask.False);

    public static PacketMatrix3Int32NeonMask Create(PacketVector3Int32NeonMask row0, PacketVector3Int32NeonMask row1, PacketVector3Int32NeonMask row2) => new(row0, row1, row2);

    public static PacketMatrix3Int32NeonMask Broadcast(PacketVector3Int32NeonMask value) => new(value, value, value);

    public static PacketVector3Int32NeonMask All(PacketMatrix3Int32NeonMask value) => value.Row0 & value.Row1 & value.Row2;

    public static PacketVector3Int32NeonMask Any(PacketMatrix3Int32NeonMask value) => value.Row0 | value.Row1 | value.Row2;

    public static PacketVector3Int32NeonMask None(PacketMatrix3Int32NeonMask value) => ~(value.Row0 | value.Row1 | value.Row2);

    public static PacketMatrix3Int32NeonMask AndNot(PacketMatrix3Int32NeonMask left, PacketMatrix3Int32NeonMask right)
    {
        return new(
            PacketVector3Int32NeonMask.AndNot(left.Row0, right.Row0),
            PacketVector3Int32NeonMask.AndNot(left.Row1, right.Row1),
            PacketVector3Int32NeonMask.AndNot(left.Row2, right.Row2));
    }

    public static PacketMatrix3Int32NeonMask Select(PacketMatrix3Int32NeonMask mask, PacketMatrix3Int32NeonMask ifTrue, PacketMatrix3Int32NeonMask ifFalse)
    {
        return new(
            PacketVector3Int32NeonMask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Int32NeonMask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Int32NeonMask.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2));
    }
    public static PacketMatrix3Int32NeonMask And(PacketMatrix3Int32NeonMask left, PacketMatrix3Int32NeonMask right) => left & right;

    public static PacketMatrix3Int32NeonMask Or(PacketMatrix3Int32NeonMask left, PacketMatrix3Int32NeonMask right) => left | right;

    public static PacketMatrix3Int32NeonMask Xor(PacketMatrix3Int32NeonMask left, PacketMatrix3Int32NeonMask right) => left ^ right;

    public static PacketMatrix3Int32NeonMask Not(PacketMatrix3Int32NeonMask value) => ~value;


    public static PacketMatrix3Int32NeonMask operator &(PacketMatrix3Int32NeonMask left, PacketMatrix3Int32NeonMask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1, left.Row2 & right.Row2);

    public static PacketMatrix3Int32NeonMask operator |(PacketMatrix3Int32NeonMask left, PacketMatrix3Int32NeonMask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1, left.Row2 | right.Row2);

    public static PacketMatrix3Int32NeonMask operator ^(PacketMatrix3Int32NeonMask left, PacketMatrix3Int32NeonMask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1, left.Row2 ^ right.Row2);

    public static PacketMatrix3Int32NeonMask operator ~(PacketMatrix3Int32NeonMask value) => new(~value.Row0, ~value.Row1, ~value.Row2);

    public static PacketMatrix3Int32NeonMask operator ==(PacketMatrix3Int32NeonMask left, PacketMatrix3Int32NeonMask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2);

    public static PacketMatrix3Int32NeonMask operator !=(PacketMatrix3Int32NeonMask left, PacketMatrix3Int32NeonMask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix3Int32NeonMask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2);
}
