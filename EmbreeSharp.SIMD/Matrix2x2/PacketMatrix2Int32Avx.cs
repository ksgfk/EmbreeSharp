namespace EmbreeSharp.SIMD;

public readonly struct PacketMatrix2Int32Avx :
    ISimdIntegerMatrix2<PacketMatrix2Int32Avx, PacketVector2Int32Avx, PacketInt32Avx, int, PacketMatrix2Int32AvxMask, PacketVector2Int32AvxMask, PacketInt32AvxMask>
{
    public PacketMatrix2Int32Avx(PacketVector2Int32Avx row0, PacketVector2Int32Avx row1)
    {
        Row0 = row0;
        Row1 = row1;
    }

    public static int LaneCount => PacketVector2Int32Avx.LaneCount;

    public PacketVector2Int32Avx Row0 { get; }

    public PacketVector2Int32Avx Row1 { get; }

    public static PacketMatrix2Int32Avx Identity
    {
        get
        {
            PacketInt32Avx zero = PacketInt32Avx.Broadcast(0);
            PacketInt32Avx one = PacketInt32Avx.Broadcast(1);
            return new(
                new PacketVector2Int32Avx(one, zero),
                new PacketVector2Int32Avx(zero, one));
        }
    }

    public static PacketMatrix2Int32Avx Create(PacketVector2Int32Avx row0, PacketVector2Int32Avx row1) => new(row0, row1);

    public static PacketMatrix2Int32Avx Broadcast(int value)
    {
        PacketVector2Int32Avx row = PacketVector2Int32Avx.Broadcast(value);
        return new(row, row);
    }

    public static PacketMatrix2Int32Avx Select(PacketMatrix2Int32AvxMask mask, PacketMatrix2Int32Avx ifTrue, PacketMatrix2Int32Avx ifFalse)
    {
        return new(
            PacketVector2Int32Avx.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Int32Avx.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1));
    }

    public static PacketMatrix2Int32Avx Select(PacketVector2Int32AvxMask mask, PacketMatrix2Int32Avx ifTrue, PacketMatrix2Int32Avx ifFalse)
    {
        return new(
            PacketVector2Int32Avx.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Int32Avx.Select(mask, ifTrue.Row1, ifFalse.Row1));
    }

    public static PacketMatrix2Int32Avx Select(PacketInt32AvxMask mask, PacketMatrix2Int32Avx ifTrue, PacketMatrix2Int32Avx ifFalse)
    {
        return new(
            PacketVector2Int32Avx.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Int32Avx.Select(mask, ifTrue.Row1, ifFalse.Row1));
    }

    public static PacketMatrix2Int32Avx Multiply(PacketMatrix2Int32Avx left, PacketMatrix2Int32Avx right) => left * right;

    public static PacketMatrix2Int32Avx Multiply(PacketMatrix2Int32Avx matrix, PacketInt32Avx scalar) => matrix * scalar;

    public static PacketMatrix2Int32Avx Multiply(PacketInt32Avx scalar, PacketMatrix2Int32Avx matrix) => scalar * matrix;

    public static PacketMatrix2Int32Avx operator *(PacketMatrix2Int32Avx left, PacketMatrix2Int32Avx right)
    {
        return new(
            new PacketVector2Int32Avx(
                left.Row0.X * right.Row0.X + left.Row0.Y * right.Row1.X,
                left.Row0.X * right.Row0.Y + left.Row0.Y * right.Row1.Y),
            new PacketVector2Int32Avx(
                left.Row1.X * right.Row0.X + left.Row1.Y * right.Row1.X,
                left.Row1.X * right.Row0.Y + left.Row1.Y * right.Row1.Y));
    }

    public static PacketVector2Int32Avx operator *(PacketMatrix2Int32Avx matrix, PacketVector2Int32Avx vector)
    {
        return new(
            matrix.Row0.X * vector.X + matrix.Row0.Y * vector.Y,
            matrix.Row1.X * vector.X + matrix.Row1.Y * vector.Y);
    }

    public static PacketMatrix2Int32Avx operator *(PacketMatrix2Int32Avx matrix, PacketInt32Avx scalar)
    {
        PacketVector2Int32Avx scalarVector = new(scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector);
    }

    public static PacketMatrix2Int32Avx operator *(PacketInt32Avx scalar, PacketMatrix2Int32Avx matrix) => matrix * scalar;

    public static PacketMatrix2Int32AvxMask operator ==(PacketMatrix2Int32Avx left, PacketMatrix2Int32Avx right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1);

    public static PacketMatrix2Int32AvxMask operator !=(PacketMatrix2Int32Avx left, PacketMatrix2Int32Avx right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1);

    public static PacketMatrix2Int32AvxMask operator <(PacketMatrix2Int32Avx left, PacketMatrix2Int32Avx right) => new(
        new PacketVector2Int32AvxMask(left.Row0.X < right.Row0.X, left.Row0.Y < right.Row0.Y),
        new PacketVector2Int32AvxMask(left.Row1.X < right.Row1.X, left.Row1.Y < right.Row1.Y));

    public static PacketMatrix2Int32AvxMask operator >(PacketMatrix2Int32Avx left, PacketMatrix2Int32Avx right) => new(
        new PacketVector2Int32AvxMask(left.Row0.X > right.Row0.X, left.Row0.Y > right.Row0.Y),
        new PacketVector2Int32AvxMask(left.Row1.X > right.Row1.X, left.Row1.Y > right.Row1.Y));

    public static PacketMatrix2Int32AvxMask operator <=(PacketMatrix2Int32Avx left, PacketMatrix2Int32Avx right) => new(
        new PacketVector2Int32AvxMask(left.Row0.X <= right.Row0.X, left.Row0.Y <= right.Row0.Y),
        new PacketVector2Int32AvxMask(left.Row1.X <= right.Row1.X, left.Row1.Y <= right.Row1.Y));

    public static PacketMatrix2Int32AvxMask operator >=(PacketMatrix2Int32Avx left, PacketMatrix2Int32Avx right) => new(
        new PacketVector2Int32AvxMask(left.Row0.X >= right.Row0.X, left.Row0.Y >= right.Row0.Y),
        new PacketVector2Int32AvxMask(left.Row1.X >= right.Row1.X, left.Row1.Y >= right.Row1.Y));

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix2Int32Avx other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1);
}

public readonly struct PacketMatrix2Int32AvxMask :
    ISimdMatrix2Mask<PacketMatrix2Int32AvxMask, PacketVector2Int32AvxMask, PacketInt32AvxMask>
{
    public PacketMatrix2Int32AvxMask(PacketVector2Int32AvxMask row0, PacketVector2Int32AvxMask row1)
    {
        Row0 = row0;
        Row1 = row1;
    }

    public static int LaneCount => PacketVector2Int32AvxMask.LaneCount;

    public PacketVector2Int32AvxMask Row0 { get; }

    public PacketVector2Int32AvxMask Row1 { get; }

    public static PacketMatrix2Int32AvxMask True => new(PacketVector2Int32AvxMask.True, PacketVector2Int32AvxMask.True);

    public static PacketMatrix2Int32AvxMask False => new(PacketVector2Int32AvxMask.False, PacketVector2Int32AvxMask.False);

    public static PacketMatrix2Int32AvxMask Create(PacketVector2Int32AvxMask row0, PacketVector2Int32AvxMask row1) => new(row0, row1);

    public static PacketMatrix2Int32AvxMask Broadcast(PacketVector2Int32AvxMask value) => new(value, value);

    public static PacketVector2Int32AvxMask All(PacketMatrix2Int32AvxMask value) => value.Row0 & value.Row1;

    public static PacketVector2Int32AvxMask Any(PacketMatrix2Int32AvxMask value) => value.Row0 | value.Row1;

    public static PacketVector2Int32AvxMask None(PacketMatrix2Int32AvxMask value) => ~(value.Row0 | value.Row1);

    public static PacketMatrix2Int32AvxMask AndNot(PacketMatrix2Int32AvxMask left, PacketMatrix2Int32AvxMask right)
    {
        return new(
            PacketVector2Int32AvxMask.AndNot(left.Row0, right.Row0),
            PacketVector2Int32AvxMask.AndNot(left.Row1, right.Row1));
    }

    public static PacketMatrix2Int32AvxMask Select(PacketMatrix2Int32AvxMask mask, PacketMatrix2Int32AvxMask ifTrue, PacketMatrix2Int32AvxMask ifFalse)
    {
        return new(
            PacketVector2Int32AvxMask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Int32AvxMask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1));
    }
    public static PacketMatrix2Int32AvxMask And(PacketMatrix2Int32AvxMask left, PacketMatrix2Int32AvxMask right) => left & right;

    public static PacketMatrix2Int32AvxMask Or(PacketMatrix2Int32AvxMask left, PacketMatrix2Int32AvxMask right) => left | right;

    public static PacketMatrix2Int32AvxMask Xor(PacketMatrix2Int32AvxMask left, PacketMatrix2Int32AvxMask right) => left ^ right;

    public static PacketMatrix2Int32AvxMask Not(PacketMatrix2Int32AvxMask value) => ~value;


    public static PacketMatrix2Int32AvxMask operator &(PacketMatrix2Int32AvxMask left, PacketMatrix2Int32AvxMask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1);

    public static PacketMatrix2Int32AvxMask operator |(PacketMatrix2Int32AvxMask left, PacketMatrix2Int32AvxMask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1);

    public static PacketMatrix2Int32AvxMask operator ^(PacketMatrix2Int32AvxMask left, PacketMatrix2Int32AvxMask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1);

    public static PacketMatrix2Int32AvxMask operator ~(PacketMatrix2Int32AvxMask value) => new(~value.Row0, ~value.Row1);

    public static PacketMatrix2Int32AvxMask operator ==(PacketMatrix2Int32AvxMask left, PacketMatrix2Int32AvxMask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1);

    public static PacketMatrix2Int32AvxMask operator !=(PacketMatrix2Int32AvxMask left, PacketMatrix2Int32AvxMask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix2Int32AvxMask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1);
}
