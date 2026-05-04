namespace EmbreeSharp.SIMD;

public readonly struct PacketMatrix2Int32Sse :
    ISimdIntegerMatrix2<PacketMatrix2Int32Sse, PacketVector2Int32Sse, PacketInt32Sse, int, PacketMatrix2Int32SseMask, PacketVector2Int32SseMask, PacketInt32SseMask>
{
    public PacketMatrix2Int32Sse(PacketVector2Int32Sse row0, PacketVector2Int32Sse row1)
    {
        Row0 = row0;
        Row1 = row1;
    }

    public static int LaneCount => PacketVector2Int32Sse.LaneCount;

    public PacketVector2Int32Sse Row0 { get; }

    public PacketVector2Int32Sse Row1 { get; }

    public static PacketMatrix2Int32Sse Identity
    {
        get
        {
            PacketInt32Sse zero = PacketInt32Sse.Broadcast(0);
            PacketInt32Sse one = PacketInt32Sse.Broadcast(1);
            return new(
                new PacketVector2Int32Sse(one, zero),
                new PacketVector2Int32Sse(zero, one));
        }
    }

    public static PacketMatrix2Int32Sse Create(PacketVector2Int32Sse row0, PacketVector2Int32Sse row1) => new(row0, row1);

    public static PacketMatrix2Int32Sse Broadcast(int value)
    {
        PacketVector2Int32Sse row = PacketVector2Int32Sse.Broadcast(value);
        return new(row, row);
    }

    public static PacketMatrix2Int32Sse Select(PacketMatrix2Int32SseMask mask, PacketMatrix2Int32Sse ifTrue, PacketMatrix2Int32Sse ifFalse)
    {
        return new(
            PacketVector2Int32Sse.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Int32Sse.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1));
    }

    public static PacketMatrix2Int32Sse Select(PacketVector2Int32SseMask mask, PacketMatrix2Int32Sse ifTrue, PacketMatrix2Int32Sse ifFalse)
    {
        return new(
            PacketVector2Int32Sse.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Int32Sse.Select(mask, ifTrue.Row1, ifFalse.Row1));
    }

    public static PacketMatrix2Int32Sse Select(PacketInt32SseMask mask, PacketMatrix2Int32Sse ifTrue, PacketMatrix2Int32Sse ifFalse)
    {
        return new(
            PacketVector2Int32Sse.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Int32Sse.Select(mask, ifTrue.Row1, ifFalse.Row1));
    }

    public static PacketMatrix2Int32Sse Multiply(PacketMatrix2Int32Sse left, PacketMatrix2Int32Sse right) => left * right;

    public static PacketMatrix2Int32Sse Multiply(PacketMatrix2Int32Sse matrix, PacketInt32Sse scalar) => matrix * scalar;

    public static PacketMatrix2Int32Sse Multiply(PacketInt32Sse scalar, PacketMatrix2Int32Sse matrix) => scalar * matrix;

    public static PacketMatrix2Int32Sse operator *(PacketMatrix2Int32Sse left, PacketMatrix2Int32Sse right)
    {
        return new(
            new PacketVector2Int32Sse(
                left.Row0.X * right.Row0.X + left.Row0.Y * right.Row1.X,
                left.Row0.X * right.Row0.Y + left.Row0.Y * right.Row1.Y),
            new PacketVector2Int32Sse(
                left.Row1.X * right.Row0.X + left.Row1.Y * right.Row1.X,
                left.Row1.X * right.Row0.Y + left.Row1.Y * right.Row1.Y));
    }

    public static PacketVector2Int32Sse operator *(PacketMatrix2Int32Sse matrix, PacketVector2Int32Sse vector)
    {
        return new(
            matrix.Row0.X * vector.X + matrix.Row0.Y * vector.Y,
            matrix.Row1.X * vector.X + matrix.Row1.Y * vector.Y);
    }

    public static PacketMatrix2Int32Sse operator *(PacketMatrix2Int32Sse matrix, PacketInt32Sse scalar)
    {
        PacketVector2Int32Sse scalarVector = new(scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector);
    }

    public static PacketMatrix2Int32Sse operator *(PacketInt32Sse scalar, PacketMatrix2Int32Sse matrix) => matrix * scalar;

    public static PacketMatrix2Int32SseMask operator ==(PacketMatrix2Int32Sse left, PacketMatrix2Int32Sse right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1);

    public static PacketMatrix2Int32SseMask operator !=(PacketMatrix2Int32Sse left, PacketMatrix2Int32Sse right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1);

    public static PacketMatrix2Int32SseMask operator <(PacketMatrix2Int32Sse left, PacketMatrix2Int32Sse right) => new(
        new PacketVector2Int32SseMask(left.Row0.X < right.Row0.X, left.Row0.Y < right.Row0.Y),
        new PacketVector2Int32SseMask(left.Row1.X < right.Row1.X, left.Row1.Y < right.Row1.Y));

    public static PacketMatrix2Int32SseMask operator >(PacketMatrix2Int32Sse left, PacketMatrix2Int32Sse right) => new(
        new PacketVector2Int32SseMask(left.Row0.X > right.Row0.X, left.Row0.Y > right.Row0.Y),
        new PacketVector2Int32SseMask(left.Row1.X > right.Row1.X, left.Row1.Y > right.Row1.Y));

    public static PacketMatrix2Int32SseMask operator <=(PacketMatrix2Int32Sse left, PacketMatrix2Int32Sse right) => new(
        new PacketVector2Int32SseMask(left.Row0.X <= right.Row0.X, left.Row0.Y <= right.Row0.Y),
        new PacketVector2Int32SseMask(left.Row1.X <= right.Row1.X, left.Row1.Y <= right.Row1.Y));

    public static PacketMatrix2Int32SseMask operator >=(PacketMatrix2Int32Sse left, PacketMatrix2Int32Sse right) => new(
        new PacketVector2Int32SseMask(left.Row0.X >= right.Row0.X, left.Row0.Y >= right.Row0.Y),
        new PacketVector2Int32SseMask(left.Row1.X >= right.Row1.X, left.Row1.Y >= right.Row1.Y));

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix2Int32Sse other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1);
}

public readonly struct PacketMatrix2Int32SseMask :
    ISimdMatrix2Mask<PacketMatrix2Int32SseMask, PacketVector2Int32SseMask, PacketInt32SseMask>
{
    public PacketMatrix2Int32SseMask(PacketVector2Int32SseMask row0, PacketVector2Int32SseMask row1)
    {
        Row0 = row0;
        Row1 = row1;
    }

    public static int LaneCount => PacketVector2Int32SseMask.LaneCount;

    public PacketVector2Int32SseMask Row0 { get; }

    public PacketVector2Int32SseMask Row1 { get; }

    public static PacketMatrix2Int32SseMask True => new(PacketVector2Int32SseMask.True, PacketVector2Int32SseMask.True);

    public static PacketMatrix2Int32SseMask False => new(PacketVector2Int32SseMask.False, PacketVector2Int32SseMask.False);

    public static PacketMatrix2Int32SseMask Create(PacketVector2Int32SseMask row0, PacketVector2Int32SseMask row1) => new(row0, row1);

    public static PacketMatrix2Int32SseMask Broadcast(PacketVector2Int32SseMask value) => new(value, value);

    public static PacketVector2Int32SseMask All(PacketMatrix2Int32SseMask value) => value.Row0 & value.Row1;

    public static PacketVector2Int32SseMask Any(PacketMatrix2Int32SseMask value) => value.Row0 | value.Row1;

    public static PacketVector2Int32SseMask None(PacketMatrix2Int32SseMask value) => ~(value.Row0 | value.Row1);

    public static PacketMatrix2Int32SseMask AndNot(PacketMatrix2Int32SseMask left, PacketMatrix2Int32SseMask right)
    {
        return new(
            PacketVector2Int32SseMask.AndNot(left.Row0, right.Row0),
            PacketVector2Int32SseMask.AndNot(left.Row1, right.Row1));
    }

    public static PacketMatrix2Int32SseMask Select(PacketMatrix2Int32SseMask mask, PacketMatrix2Int32SseMask ifTrue, PacketMatrix2Int32SseMask ifFalse)
    {
        return new(
            PacketVector2Int32SseMask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Int32SseMask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1));
    }
    public static PacketMatrix2Int32SseMask And(PacketMatrix2Int32SseMask left, PacketMatrix2Int32SseMask right) => left & right;

    public static PacketMatrix2Int32SseMask Or(PacketMatrix2Int32SseMask left, PacketMatrix2Int32SseMask right) => left | right;

    public static PacketMatrix2Int32SseMask Xor(PacketMatrix2Int32SseMask left, PacketMatrix2Int32SseMask right) => left ^ right;

    public static PacketMatrix2Int32SseMask Not(PacketMatrix2Int32SseMask value) => ~value;


    public static PacketMatrix2Int32SseMask operator &(PacketMatrix2Int32SseMask left, PacketMatrix2Int32SseMask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1);

    public static PacketMatrix2Int32SseMask operator |(PacketMatrix2Int32SseMask left, PacketMatrix2Int32SseMask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1);

    public static PacketMatrix2Int32SseMask operator ^(PacketMatrix2Int32SseMask left, PacketMatrix2Int32SseMask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1);

    public static PacketMatrix2Int32SseMask operator ~(PacketMatrix2Int32SseMask value) => new(~value.Row0, ~value.Row1);

    public static PacketMatrix2Int32SseMask operator ==(PacketMatrix2Int32SseMask left, PacketMatrix2Int32SseMask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1);

    public static PacketMatrix2Int32SseMask operator !=(PacketMatrix2Int32SseMask left, PacketMatrix2Int32SseMask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix2Int32SseMask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1);
}
