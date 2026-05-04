namespace EmbreeSharp.SIMD;

public readonly struct PacketMatrix3Int32Sse :
    ISimdIntegerMatrix3<PacketMatrix3Int32Sse, PacketVector3Int32Sse, PacketInt32Sse, int, PacketMatrix3Int32SseMask, PacketVector3Int32SseMask, PacketInt32SseMask>
{
    public PacketMatrix3Int32Sse(PacketVector3Int32Sse row0, PacketVector3Int32Sse row1, PacketVector3Int32Sse row2)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
    }

    public static int LaneCount => PacketVector3Int32Sse.LaneCount;

    public PacketVector3Int32Sse Row0 { get; }

    public PacketVector3Int32Sse Row1 { get; }

    public PacketVector3Int32Sse Row2 { get; }

    public static PacketMatrix3Int32Sse Identity
    {
        get
        {
            PacketInt32Sse zero = PacketInt32Sse.Broadcast(0);
            PacketInt32Sse one = PacketInt32Sse.Broadcast(1);
            return new(
                new PacketVector3Int32Sse(one, zero, zero),
                new PacketVector3Int32Sse(zero, one, zero),
                new PacketVector3Int32Sse(zero, zero, one));
        }
    }

    public static PacketMatrix3Int32Sse Create(PacketVector3Int32Sse row0, PacketVector3Int32Sse row1, PacketVector3Int32Sse row2) => new(row0, row1, row2);

    public static PacketMatrix3Int32Sse Broadcast(int value)
    {
        PacketVector3Int32Sse row = PacketVector3Int32Sse.Broadcast(value);
        return new(row, row, row);
    }

    public static PacketMatrix3Int32Sse Select(PacketMatrix3Int32SseMask mask, PacketMatrix3Int32Sse ifTrue, PacketMatrix3Int32Sse ifFalse)
    {
        return new(
            PacketVector3Int32Sse.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Int32Sse.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Int32Sse.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketMatrix3Int32Sse Select(PacketVector3Int32SseMask mask, PacketMatrix3Int32Sse ifTrue, PacketMatrix3Int32Sse ifFalse)
    {
        return new(
            PacketVector3Int32Sse.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Int32Sse.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Int32Sse.Select(mask, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketMatrix3Int32Sse Select(PacketInt32SseMask mask, PacketMatrix3Int32Sse ifTrue, PacketMatrix3Int32Sse ifFalse)
    {
        return new(
            PacketVector3Int32Sse.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Int32Sse.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Int32Sse.Select(mask, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketMatrix3Int32Sse Multiply(PacketMatrix3Int32Sse left, PacketMatrix3Int32Sse right) => left * right;

    public static PacketMatrix3Int32Sse Multiply(PacketMatrix3Int32Sse matrix, PacketInt32Sse scalar) => matrix * scalar;

    public static PacketMatrix3Int32Sse Multiply(PacketInt32Sse scalar, PacketMatrix3Int32Sse matrix) => scalar * matrix;

    public static PacketMatrix3Int32Sse operator *(PacketMatrix3Int32Sse left, PacketMatrix3Int32Sse right)
    {
        return new(
            new PacketVector3Int32Sse(
                left.Row0.X * right.Row0.X + left.Row0.Y * right.Row1.X + left.Row0.Z * right.Row2.X,
                left.Row0.X * right.Row0.Y + left.Row0.Y * right.Row1.Y + left.Row0.Z * right.Row2.Y,
                left.Row0.X * right.Row0.Z + left.Row0.Y * right.Row1.Z + left.Row0.Z * right.Row2.Z),
            new PacketVector3Int32Sse(
                left.Row1.X * right.Row0.X + left.Row1.Y * right.Row1.X + left.Row1.Z * right.Row2.X,
                left.Row1.X * right.Row0.Y + left.Row1.Y * right.Row1.Y + left.Row1.Z * right.Row2.Y,
                left.Row1.X * right.Row0.Z + left.Row1.Y * right.Row1.Z + left.Row1.Z * right.Row2.Z),
            new PacketVector3Int32Sse(
                left.Row2.X * right.Row0.X + left.Row2.Y * right.Row1.X + left.Row2.Z * right.Row2.X,
                left.Row2.X * right.Row0.Y + left.Row2.Y * right.Row1.Y + left.Row2.Z * right.Row2.Y,
                left.Row2.X * right.Row0.Z + left.Row2.Y * right.Row1.Z + left.Row2.Z * right.Row2.Z));
    }

    public static PacketVector3Int32Sse operator *(PacketMatrix3Int32Sse matrix, PacketVector3Int32Sse vector)
    {
        return new(
            matrix.Row0.X * vector.X + matrix.Row0.Y * vector.Y + matrix.Row0.Z * vector.Z,
            matrix.Row1.X * vector.X + matrix.Row1.Y * vector.Y + matrix.Row1.Z * vector.Z,
            matrix.Row2.X * vector.X + matrix.Row2.Y * vector.Y + matrix.Row2.Z * vector.Z);
    }

    public static PacketMatrix3Int32Sse operator *(PacketMatrix3Int32Sse matrix, PacketInt32Sse scalar)
    {
        PacketVector3Int32Sse scalarVector = new(scalar, scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector, matrix.Row2 * scalarVector);
    }

    public static PacketMatrix3Int32Sse operator *(PacketInt32Sse scalar, PacketMatrix3Int32Sse matrix) => matrix * scalar;

    public static PacketMatrix3Int32SseMask operator ==(PacketMatrix3Int32Sse left, PacketMatrix3Int32Sse right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2);

    public static PacketMatrix3Int32SseMask operator !=(PacketMatrix3Int32Sse left, PacketMatrix3Int32Sse right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix3Int32Sse other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2);
}

public readonly struct PacketMatrix3Int32SseMask :
    ISimdMatrix3Mask<PacketMatrix3Int32SseMask, PacketVector3Int32SseMask, PacketInt32SseMask>
{
    public PacketMatrix3Int32SseMask(PacketVector3Int32SseMask row0, PacketVector3Int32SseMask row1, PacketVector3Int32SseMask row2)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
    }

    public static int LaneCount => PacketVector3Int32SseMask.LaneCount;

    public PacketVector3Int32SseMask Row0 { get; }

    public PacketVector3Int32SseMask Row1 { get; }

    public PacketVector3Int32SseMask Row2 { get; }

    public static PacketMatrix3Int32SseMask True => new(PacketVector3Int32SseMask.True, PacketVector3Int32SseMask.True, PacketVector3Int32SseMask.True);

    public static PacketMatrix3Int32SseMask False => new(PacketVector3Int32SseMask.False, PacketVector3Int32SseMask.False, PacketVector3Int32SseMask.False);

    public static PacketMatrix3Int32SseMask Create(PacketVector3Int32SseMask row0, PacketVector3Int32SseMask row1, PacketVector3Int32SseMask row2) => new(row0, row1, row2);

    public static PacketMatrix3Int32SseMask Broadcast(PacketVector3Int32SseMask value) => new(value, value, value);

    public static PacketVector3Int32SseMask All(PacketMatrix3Int32SseMask value) => value.Row0 & value.Row1 & value.Row2;

    public static PacketVector3Int32SseMask Any(PacketMatrix3Int32SseMask value) => value.Row0 | value.Row1 | value.Row2;

    public static PacketVector3Int32SseMask None(PacketMatrix3Int32SseMask value) => ~(value.Row0 | value.Row1 | value.Row2);

    public static PacketMatrix3Int32SseMask AndNot(PacketMatrix3Int32SseMask left, PacketMatrix3Int32SseMask right)
    {
        return new(
            PacketVector3Int32SseMask.AndNot(left.Row0, right.Row0),
            PacketVector3Int32SseMask.AndNot(left.Row1, right.Row1),
            PacketVector3Int32SseMask.AndNot(left.Row2, right.Row2));
    }

    public static PacketMatrix3Int32SseMask Select(PacketMatrix3Int32SseMask mask, PacketMatrix3Int32SseMask ifTrue, PacketMatrix3Int32SseMask ifFalse)
    {
        return new(
            PacketVector3Int32SseMask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Int32SseMask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Int32SseMask.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2));
    }
    public static PacketMatrix3Int32SseMask And(PacketMatrix3Int32SseMask left, PacketMatrix3Int32SseMask right) => left & right;

    public static PacketMatrix3Int32SseMask Or(PacketMatrix3Int32SseMask left, PacketMatrix3Int32SseMask right) => left | right;

    public static PacketMatrix3Int32SseMask Xor(PacketMatrix3Int32SseMask left, PacketMatrix3Int32SseMask right) => left ^ right;

    public static PacketMatrix3Int32SseMask Not(PacketMatrix3Int32SseMask value) => ~value;


    public static PacketMatrix3Int32SseMask operator &(PacketMatrix3Int32SseMask left, PacketMatrix3Int32SseMask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1, left.Row2 & right.Row2);

    public static PacketMatrix3Int32SseMask operator |(PacketMatrix3Int32SseMask left, PacketMatrix3Int32SseMask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1, left.Row2 | right.Row2);

    public static PacketMatrix3Int32SseMask operator ^(PacketMatrix3Int32SseMask left, PacketMatrix3Int32SseMask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1, left.Row2 ^ right.Row2);

    public static PacketMatrix3Int32SseMask operator ~(PacketMatrix3Int32SseMask value) => new(~value.Row0, ~value.Row1, ~value.Row2);

    public static PacketMatrix3Int32SseMask operator ==(PacketMatrix3Int32SseMask left, PacketMatrix3Int32SseMask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2);

    public static PacketMatrix3Int32SseMask operator !=(PacketMatrix3Int32SseMask left, PacketMatrix3Int32SseMask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix3Int32SseMask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2);
}
