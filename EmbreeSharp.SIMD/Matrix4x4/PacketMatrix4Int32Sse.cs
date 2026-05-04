namespace EmbreeSharp.SIMD;

public readonly struct PacketMatrix4Int32Sse :
    ISimdIntegerMatrix4<PacketMatrix4Int32Sse, PacketVector4Int32Sse, PacketInt32Sse, int, PacketMatrix4Int32SseMask, PacketVector4Int32SseMask, PacketInt32SseMask>
{
    public PacketMatrix4Int32Sse(PacketVector4Int32Sse row0, PacketVector4Int32Sse row1, PacketVector4Int32Sse row2, PacketVector4Int32Sse row3)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
        Row3 = row3;
    }

    public static int LaneCount => PacketVector4Int32Sse.LaneCount;

    public PacketVector4Int32Sse Row0 { get; }

    public PacketVector4Int32Sse Row1 { get; }

    public PacketVector4Int32Sse Row2 { get; }

    public PacketVector4Int32Sse Row3 { get; }

    public static PacketMatrix4Int32Sse Identity
    {
        get
        {
            PacketInt32Sse zero = PacketInt32Sse.Broadcast(0);
            PacketInt32Sse one = PacketInt32Sse.Broadcast(1);
            return new(
                new PacketVector4Int32Sse(one, zero, zero, zero),
                new PacketVector4Int32Sse(zero, one, zero, zero),
                new PacketVector4Int32Sse(zero, zero, one, zero),
                new PacketVector4Int32Sse(zero, zero, zero, one));
        }
    }

    public static PacketMatrix4Int32Sse Create(PacketVector4Int32Sse row0, PacketVector4Int32Sse row1, PacketVector4Int32Sse row2, PacketVector4Int32Sse row3) => new(row0, row1, row2, row3);

    public static PacketMatrix4Int32Sse Broadcast(int value)
    {
        PacketVector4Int32Sse row = PacketVector4Int32Sse.Broadcast(value);
        return new(row, row, row, row);
    }

    public static PacketMatrix4Int32Sse Select(PacketMatrix4Int32SseMask mask, PacketMatrix4Int32Sse ifTrue, PacketMatrix4Int32Sse ifFalse)
    {
        return new(
            PacketVector4Int32Sse.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Int32Sse.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Int32Sse.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Int32Sse.Select(mask.Row3, ifTrue.Row3, ifFalse.Row3));
    }

    public static PacketMatrix4Int32Sse Select(PacketVector4Int32SseMask mask, PacketMatrix4Int32Sse ifTrue, PacketMatrix4Int32Sse ifFalse)
    {
        return new(
            PacketVector4Int32Sse.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Int32Sse.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Int32Sse.Select(mask, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Int32Sse.Select(mask, ifTrue.Row3, ifFalse.Row3));
    }

    public static PacketMatrix4Int32Sse Select(PacketInt32SseMask mask, PacketMatrix4Int32Sse ifTrue, PacketMatrix4Int32Sse ifFalse)
    {
        return new(
            PacketVector4Int32Sse.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Int32Sse.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Int32Sse.Select(mask, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Int32Sse.Select(mask, ifTrue.Row3, ifFalse.Row3));
    }

    public static PacketMatrix4Int32Sse Multiply(PacketMatrix4Int32Sse left, PacketMatrix4Int32Sse right) => left * right;

    public static PacketMatrix4Int32Sse Multiply(PacketMatrix4Int32Sse matrix, PacketInt32Sse scalar) => matrix * scalar;

    public static PacketMatrix4Int32Sse Multiply(PacketInt32Sse scalar, PacketMatrix4Int32Sse matrix) => scalar * matrix;

    public static PacketMatrix4Int32Sse operator *(PacketMatrix4Int32Sse left, PacketMatrix4Int32Sse right)
    {
        return new(
            new PacketVector4Int32Sse(
                left.Row0.X * right.Row0.X + left.Row0.Y * right.Row1.X + left.Row0.Z * right.Row2.X + left.Row0.W * right.Row3.X,
                left.Row0.X * right.Row0.Y + left.Row0.Y * right.Row1.Y + left.Row0.Z * right.Row2.Y + left.Row0.W * right.Row3.Y,
                left.Row0.X * right.Row0.Z + left.Row0.Y * right.Row1.Z + left.Row0.Z * right.Row2.Z + left.Row0.W * right.Row3.Z,
                left.Row0.X * right.Row0.W + left.Row0.Y * right.Row1.W + left.Row0.Z * right.Row2.W + left.Row0.W * right.Row3.W),
            new PacketVector4Int32Sse(
                left.Row1.X * right.Row0.X + left.Row1.Y * right.Row1.X + left.Row1.Z * right.Row2.X + left.Row1.W * right.Row3.X,
                left.Row1.X * right.Row0.Y + left.Row1.Y * right.Row1.Y + left.Row1.Z * right.Row2.Y + left.Row1.W * right.Row3.Y,
                left.Row1.X * right.Row0.Z + left.Row1.Y * right.Row1.Z + left.Row1.Z * right.Row2.Z + left.Row1.W * right.Row3.Z,
                left.Row1.X * right.Row0.W + left.Row1.Y * right.Row1.W + left.Row1.Z * right.Row2.W + left.Row1.W * right.Row3.W),
            new PacketVector4Int32Sse(
                left.Row2.X * right.Row0.X + left.Row2.Y * right.Row1.X + left.Row2.Z * right.Row2.X + left.Row2.W * right.Row3.X,
                left.Row2.X * right.Row0.Y + left.Row2.Y * right.Row1.Y + left.Row2.Z * right.Row2.Y + left.Row2.W * right.Row3.Y,
                left.Row2.X * right.Row0.Z + left.Row2.Y * right.Row1.Z + left.Row2.Z * right.Row2.Z + left.Row2.W * right.Row3.Z,
                left.Row2.X * right.Row0.W + left.Row2.Y * right.Row1.W + left.Row2.Z * right.Row2.W + left.Row2.W * right.Row3.W),
            new PacketVector4Int32Sse(
                left.Row3.X * right.Row0.X + left.Row3.Y * right.Row1.X + left.Row3.Z * right.Row2.X + left.Row3.W * right.Row3.X,
                left.Row3.X * right.Row0.Y + left.Row3.Y * right.Row1.Y + left.Row3.Z * right.Row2.Y + left.Row3.W * right.Row3.Y,
                left.Row3.X * right.Row0.Z + left.Row3.Y * right.Row1.Z + left.Row3.Z * right.Row2.Z + left.Row3.W * right.Row3.Z,
                left.Row3.X * right.Row0.W + left.Row3.Y * right.Row1.W + left.Row3.Z * right.Row2.W + left.Row3.W * right.Row3.W));
    }

    public static PacketVector4Int32Sse operator *(PacketMatrix4Int32Sse matrix, PacketVector4Int32Sse vector)
    {
        return new(
            matrix.Row0.X * vector.X + matrix.Row0.Y * vector.Y + matrix.Row0.Z * vector.Z + matrix.Row0.W * vector.W,
            matrix.Row1.X * vector.X + matrix.Row1.Y * vector.Y + matrix.Row1.Z * vector.Z + matrix.Row1.W * vector.W,
            matrix.Row2.X * vector.X + matrix.Row2.Y * vector.Y + matrix.Row2.Z * vector.Z + matrix.Row2.W * vector.W,
            matrix.Row3.X * vector.X + matrix.Row3.Y * vector.Y + matrix.Row3.Z * vector.Z + matrix.Row3.W * vector.W);
    }

    public static PacketMatrix4Int32Sse operator *(PacketMatrix4Int32Sse matrix, PacketInt32Sse scalar)
    {
        PacketVector4Int32Sse scalarVector = new(scalar, scalar, scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector, matrix.Row2 * scalarVector, matrix.Row3 * scalarVector);
    }

    public static PacketMatrix4Int32Sse operator *(PacketInt32Sse scalar, PacketMatrix4Int32Sse matrix) => matrix * scalar;

    public static PacketMatrix4Int32SseMask operator ==(PacketMatrix4Int32Sse left, PacketMatrix4Int32Sse right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2, left.Row3 == right.Row3);

    public static PacketMatrix4Int32SseMask operator !=(PacketMatrix4Int32Sse left, PacketMatrix4Int32Sse right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2, left.Row3 != right.Row3);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix4Int32Sse other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2) &&
               Row3.Equals(other.Row3);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2, Row3);
}

public readonly struct PacketMatrix4Int32SseMask :
    ISimdMatrix4Mask<PacketMatrix4Int32SseMask, PacketVector4Int32SseMask, PacketInt32SseMask>
{
    public PacketMatrix4Int32SseMask(PacketVector4Int32SseMask row0, PacketVector4Int32SseMask row1, PacketVector4Int32SseMask row2, PacketVector4Int32SseMask row3)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
        Row3 = row3;
    }

    public static int LaneCount => PacketVector4Int32SseMask.LaneCount;

    public PacketVector4Int32SseMask Row0 { get; }

    public PacketVector4Int32SseMask Row1 { get; }

    public PacketVector4Int32SseMask Row2 { get; }

    public PacketVector4Int32SseMask Row3 { get; }

    public static PacketMatrix4Int32SseMask True => new(PacketVector4Int32SseMask.True, PacketVector4Int32SseMask.True, PacketVector4Int32SseMask.True, PacketVector4Int32SseMask.True);

    public static PacketMatrix4Int32SseMask False => new(PacketVector4Int32SseMask.False, PacketVector4Int32SseMask.False, PacketVector4Int32SseMask.False, PacketVector4Int32SseMask.False);

    public static PacketMatrix4Int32SseMask Create(PacketVector4Int32SseMask row0, PacketVector4Int32SseMask row1, PacketVector4Int32SseMask row2, PacketVector4Int32SseMask row3) => new(row0, row1, row2, row3);

    public static PacketMatrix4Int32SseMask Broadcast(PacketVector4Int32SseMask value) => new(value, value, value, value);

    public static PacketVector4Int32SseMask All(PacketMatrix4Int32SseMask value) => value.Row0 & value.Row1 & value.Row2 & value.Row3;

    public static PacketVector4Int32SseMask Any(PacketMatrix4Int32SseMask value) => value.Row0 | value.Row1 | value.Row2 | value.Row3;

    public static PacketVector4Int32SseMask None(PacketMatrix4Int32SseMask value) => ~(value.Row0 | value.Row1 | value.Row2 | value.Row3);

    public static PacketMatrix4Int32SseMask AndNot(PacketMatrix4Int32SseMask left, PacketMatrix4Int32SseMask right)
    {
        return new(
            PacketVector4Int32SseMask.AndNot(left.Row0, right.Row0),
            PacketVector4Int32SseMask.AndNot(left.Row1, right.Row1),
            PacketVector4Int32SseMask.AndNot(left.Row2, right.Row2),
            PacketVector4Int32SseMask.AndNot(left.Row3, right.Row3));
    }

    public static PacketMatrix4Int32SseMask Select(PacketMatrix4Int32SseMask mask, PacketMatrix4Int32SseMask ifTrue, PacketMatrix4Int32SseMask ifFalse)
    {
        return new(
            PacketVector4Int32SseMask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Int32SseMask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Int32SseMask.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Int32SseMask.Select(mask.Row3, ifTrue.Row3, ifFalse.Row3));
    }
    public static PacketMatrix4Int32SseMask And(PacketMatrix4Int32SseMask left, PacketMatrix4Int32SseMask right) => left & right;

    public static PacketMatrix4Int32SseMask Or(PacketMatrix4Int32SseMask left, PacketMatrix4Int32SseMask right) => left | right;

    public static PacketMatrix4Int32SseMask Xor(PacketMatrix4Int32SseMask left, PacketMatrix4Int32SseMask right) => left ^ right;

    public static PacketMatrix4Int32SseMask Not(PacketMatrix4Int32SseMask value) => ~value;


    public static PacketMatrix4Int32SseMask operator &(PacketMatrix4Int32SseMask left, PacketMatrix4Int32SseMask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1, left.Row2 & right.Row2, left.Row3 & right.Row3);

    public static PacketMatrix4Int32SseMask operator |(PacketMatrix4Int32SseMask left, PacketMatrix4Int32SseMask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1, left.Row2 | right.Row2, left.Row3 | right.Row3);

    public static PacketMatrix4Int32SseMask operator ^(PacketMatrix4Int32SseMask left, PacketMatrix4Int32SseMask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1, left.Row2 ^ right.Row2, left.Row3 ^ right.Row3);

    public static PacketMatrix4Int32SseMask operator ~(PacketMatrix4Int32SseMask value) => new(~value.Row0, ~value.Row1, ~value.Row2, ~value.Row3);

    public static PacketMatrix4Int32SseMask operator ==(PacketMatrix4Int32SseMask left, PacketMatrix4Int32SseMask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2, left.Row3 == right.Row3);

    public static PacketMatrix4Int32SseMask operator !=(PacketMatrix4Int32SseMask left, PacketMatrix4Int32SseMask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2, left.Row3 != right.Row3);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix4Int32SseMask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2) &&
               Row3.Equals(other.Row3);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2, Row3);
}
