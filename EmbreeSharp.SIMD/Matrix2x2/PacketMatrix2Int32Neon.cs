namespace EmbreeSharp.SIMD;

public readonly struct PacketMatrix2Int32Neon :
    ISimdIntegerMatrix2<PacketMatrix2Int32Neon, PacketVector2Int32Neon, PacketInt32Neon, int, PacketMatrix2Int32NeonMask, PacketVector2Int32NeonMask, PacketInt32NeonMask>
{
    public PacketMatrix2Int32Neon(PacketVector2Int32Neon row0, PacketVector2Int32Neon row1)
    {
        Row0 = row0;
        Row1 = row1;
    }

    public static int LaneCount => PacketVector2Int32Neon.LaneCount;

    public PacketVector2Int32Neon Row0 { get; }

    public PacketVector2Int32Neon Row1 { get; }

    public static PacketMatrix2Int32Neon Identity
    {
        get
        {
            PacketInt32Neon zero = PacketInt32Neon.Broadcast(0);
            PacketInt32Neon one = PacketInt32Neon.Broadcast(1);
            return new(
                new PacketVector2Int32Neon(one, zero),
                new PacketVector2Int32Neon(zero, one));
        }
    }

    public static PacketMatrix2Int32Neon Create(PacketVector2Int32Neon row0, PacketVector2Int32Neon row1) => new(row0, row1);

    public static PacketMatrix2Int32Neon Broadcast(int value)
    {
        PacketVector2Int32Neon row = PacketVector2Int32Neon.Broadcast(value);
        return new(row, row);
    }

    public static PacketMatrix2Int32Neon Select(PacketMatrix2Int32NeonMask mask, PacketMatrix2Int32Neon ifTrue, PacketMatrix2Int32Neon ifFalse)
    {
        return new(
            PacketVector2Int32Neon.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Int32Neon.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1));
    }

    public static PacketMatrix2Int32Neon Select(PacketVector2Int32NeonMask mask, PacketMatrix2Int32Neon ifTrue, PacketMatrix2Int32Neon ifFalse)
    {
        return new(
            PacketVector2Int32Neon.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Int32Neon.Select(mask, ifTrue.Row1, ifFalse.Row1));
    }

    public static PacketMatrix2Int32Neon Select(PacketInt32NeonMask mask, PacketMatrix2Int32Neon ifTrue, PacketMatrix2Int32Neon ifFalse)
    {
        return new(
            PacketVector2Int32Neon.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Int32Neon.Select(mask, ifTrue.Row1, ifFalse.Row1));
    }

    public static PacketMatrix2Int32Neon Multiply(PacketMatrix2Int32Neon left, PacketMatrix2Int32Neon right) => left * right;

    public static PacketMatrix2Int32Neon Multiply(PacketMatrix2Int32Neon matrix, PacketInt32Neon scalar) => matrix * scalar;

    public static PacketMatrix2Int32Neon Multiply(PacketInt32Neon scalar, PacketMatrix2Int32Neon matrix) => scalar * matrix;

    public static PacketMatrix2Int32Neon operator *(PacketMatrix2Int32Neon left, PacketMatrix2Int32Neon right)
    {
        return new(
            new PacketVector2Int32Neon(
                left.Row0.X * right.Row0.X + left.Row0.Y * right.Row1.X,
                left.Row0.X * right.Row0.Y + left.Row0.Y * right.Row1.Y),
            new PacketVector2Int32Neon(
                left.Row1.X * right.Row0.X + left.Row1.Y * right.Row1.X,
                left.Row1.X * right.Row0.Y + left.Row1.Y * right.Row1.Y));
    }

    public static PacketVector2Int32Neon operator *(PacketMatrix2Int32Neon matrix, PacketVector2Int32Neon vector)
    {
        return new(
            matrix.Row0.X * vector.X + matrix.Row0.Y * vector.Y,
            matrix.Row1.X * vector.X + matrix.Row1.Y * vector.Y);
    }

    public static PacketMatrix2Int32Neon operator *(PacketMatrix2Int32Neon matrix, PacketInt32Neon scalar)
    {
        PacketVector2Int32Neon scalarVector = new(scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector);
    }

    public static PacketMatrix2Int32Neon operator *(PacketInt32Neon scalar, PacketMatrix2Int32Neon matrix) => matrix * scalar;

    public static PacketMatrix2Int32NeonMask operator ==(PacketMatrix2Int32Neon left, PacketMatrix2Int32Neon right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1);

    public static PacketMatrix2Int32NeonMask operator !=(PacketMatrix2Int32Neon left, PacketMatrix2Int32Neon right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1);

    public static PacketMatrix2Int32NeonMask operator <(PacketMatrix2Int32Neon left, PacketMatrix2Int32Neon right) => new(
        new PacketVector2Int32NeonMask(left.Row0.X < right.Row0.X, left.Row0.Y < right.Row0.Y),
        new PacketVector2Int32NeonMask(left.Row1.X < right.Row1.X, left.Row1.Y < right.Row1.Y));

    public static PacketMatrix2Int32NeonMask operator >(PacketMatrix2Int32Neon left, PacketMatrix2Int32Neon right) => new(
        new PacketVector2Int32NeonMask(left.Row0.X > right.Row0.X, left.Row0.Y > right.Row0.Y),
        new PacketVector2Int32NeonMask(left.Row1.X > right.Row1.X, left.Row1.Y > right.Row1.Y));

    public static PacketMatrix2Int32NeonMask operator <=(PacketMatrix2Int32Neon left, PacketMatrix2Int32Neon right) => new(
        new PacketVector2Int32NeonMask(left.Row0.X <= right.Row0.X, left.Row0.Y <= right.Row0.Y),
        new PacketVector2Int32NeonMask(left.Row1.X <= right.Row1.X, left.Row1.Y <= right.Row1.Y));

    public static PacketMatrix2Int32NeonMask operator >=(PacketMatrix2Int32Neon left, PacketMatrix2Int32Neon right) => new(
        new PacketVector2Int32NeonMask(left.Row0.X >= right.Row0.X, left.Row0.Y >= right.Row0.Y),
        new PacketVector2Int32NeonMask(left.Row1.X >= right.Row1.X, left.Row1.Y >= right.Row1.Y));

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix2Int32Neon other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1);
}

public readonly struct PacketMatrix2Int32NeonMask :
    ISimdMatrix2Mask<PacketMatrix2Int32NeonMask, PacketVector2Int32NeonMask, PacketInt32NeonMask>
{
    public PacketMatrix2Int32NeonMask(PacketVector2Int32NeonMask row0, PacketVector2Int32NeonMask row1)
    {
        Row0 = row0;
        Row1 = row1;
    }

    public static int LaneCount => PacketVector2Int32NeonMask.LaneCount;

    public PacketVector2Int32NeonMask Row0 { get; }

    public PacketVector2Int32NeonMask Row1 { get; }

    public static PacketMatrix2Int32NeonMask True => new(PacketVector2Int32NeonMask.True, PacketVector2Int32NeonMask.True);

    public static PacketMatrix2Int32NeonMask False => new(PacketVector2Int32NeonMask.False, PacketVector2Int32NeonMask.False);

    public static PacketMatrix2Int32NeonMask Create(PacketVector2Int32NeonMask row0, PacketVector2Int32NeonMask row1) => new(row0, row1);

    public static PacketMatrix2Int32NeonMask Broadcast(PacketVector2Int32NeonMask value) => new(value, value);

    public static PacketVector2Int32NeonMask All(PacketMatrix2Int32NeonMask value) => value.Row0 & value.Row1;

    public static PacketVector2Int32NeonMask Any(PacketMatrix2Int32NeonMask value) => value.Row0 | value.Row1;

    public static PacketVector2Int32NeonMask None(PacketMatrix2Int32NeonMask value) => ~(value.Row0 | value.Row1);

    public static PacketMatrix2Int32NeonMask AndNot(PacketMatrix2Int32NeonMask left, PacketMatrix2Int32NeonMask right)
    {
        return new(
            PacketVector2Int32NeonMask.AndNot(left.Row0, right.Row0),
            PacketVector2Int32NeonMask.AndNot(left.Row1, right.Row1));
    }

    public static PacketMatrix2Int32NeonMask Select(PacketMatrix2Int32NeonMask mask, PacketMatrix2Int32NeonMask ifTrue, PacketMatrix2Int32NeonMask ifFalse)
    {
        return new(
            PacketVector2Int32NeonMask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Int32NeonMask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1));
    }
    public static PacketMatrix2Int32NeonMask And(PacketMatrix2Int32NeonMask left, PacketMatrix2Int32NeonMask right) => left & right;

    public static PacketMatrix2Int32NeonMask Or(PacketMatrix2Int32NeonMask left, PacketMatrix2Int32NeonMask right) => left | right;

    public static PacketMatrix2Int32NeonMask Xor(PacketMatrix2Int32NeonMask left, PacketMatrix2Int32NeonMask right) => left ^ right;

    public static PacketMatrix2Int32NeonMask Not(PacketMatrix2Int32NeonMask value) => ~value;


    public static PacketMatrix2Int32NeonMask operator &(PacketMatrix2Int32NeonMask left, PacketMatrix2Int32NeonMask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1);

    public static PacketMatrix2Int32NeonMask operator |(PacketMatrix2Int32NeonMask left, PacketMatrix2Int32NeonMask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1);

    public static PacketMatrix2Int32NeonMask operator ^(PacketMatrix2Int32NeonMask left, PacketMatrix2Int32NeonMask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1);

    public static PacketMatrix2Int32NeonMask operator ~(PacketMatrix2Int32NeonMask value) => new(~value.Row0, ~value.Row1);

    public static PacketMatrix2Int32NeonMask operator ==(PacketMatrix2Int32NeonMask left, PacketMatrix2Int32NeonMask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1);

    public static PacketMatrix2Int32NeonMask operator !=(PacketMatrix2Int32NeonMask left, PacketMatrix2Int32NeonMask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix2Int32NeonMask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1);
}
