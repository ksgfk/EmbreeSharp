namespace EmbreeSharp.SIMD;

public readonly struct PacketMatrix3Int32Avx :
    ISimdIntegerMatrix3<PacketMatrix3Int32Avx, PacketVector3Int32Avx, PacketInt32Avx, int, PacketMatrix3Int32AvxMask, PacketVector3Int32AvxMask, PacketInt32AvxMask>
{
    public PacketMatrix3Int32Avx(PacketVector3Int32Avx row0, PacketVector3Int32Avx row1, PacketVector3Int32Avx row2)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
    }

    public static int LaneCount => PacketVector3Int32Avx.LaneCount;

    public PacketVector3Int32Avx Row0 { get; }

    public PacketVector3Int32Avx Row1 { get; }

    public PacketVector3Int32Avx Row2 { get; }

    public static PacketMatrix3Int32Avx Identity
    {
        get
        {
            PacketInt32Avx zero = PacketInt32Avx.Broadcast(0);
            PacketInt32Avx one = PacketInt32Avx.Broadcast(1);
            return new(
                new PacketVector3Int32Avx(one, zero, zero),
                new PacketVector3Int32Avx(zero, one, zero),
                new PacketVector3Int32Avx(zero, zero, one));
        }
    }

    public static PacketMatrix3Int32Avx Create(PacketVector3Int32Avx row0, PacketVector3Int32Avx row1, PacketVector3Int32Avx row2) => new(row0, row1, row2);

    public static PacketMatrix3Int32Avx Broadcast(int value)
    {
        PacketVector3Int32Avx row = PacketVector3Int32Avx.Broadcast(value);
        return new(row, row, row);
    }

    public static PacketMatrix3Int32Avx Select(PacketMatrix3Int32AvxMask mask, PacketMatrix3Int32Avx ifTrue, PacketMatrix3Int32Avx ifFalse)
    {
        return new(
            PacketVector3Int32Avx.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Int32Avx.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Int32Avx.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketMatrix3Int32Avx Select(PacketVector3Int32AvxMask mask, PacketMatrix3Int32Avx ifTrue, PacketMatrix3Int32Avx ifFalse)
    {
        return new(
            PacketVector3Int32Avx.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Int32Avx.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Int32Avx.Select(mask, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketMatrix3Int32Avx Select(PacketInt32AvxMask mask, PacketMatrix3Int32Avx ifTrue, PacketMatrix3Int32Avx ifFalse)
    {
        return new(
            PacketVector3Int32Avx.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Int32Avx.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Int32Avx.Select(mask, ifTrue.Row2, ifFalse.Row2));
    }

    public static PacketMatrix3Int32Avx Multiply(PacketMatrix3Int32Avx left, PacketMatrix3Int32Avx right) => left * right;

    public static PacketMatrix3Int32Avx Multiply(PacketMatrix3Int32Avx matrix, PacketInt32Avx scalar) => matrix * scalar;

    public static PacketMatrix3Int32Avx Multiply(PacketInt32Avx scalar, PacketMatrix3Int32Avx matrix) => scalar * matrix;

    public static PacketMatrix3Int32Avx operator *(PacketMatrix3Int32Avx left, PacketMatrix3Int32Avx right)
    {
        return new(
            new PacketVector3Int32Avx(
                left.Row0.X * right.Row0.X + left.Row0.Y * right.Row1.X + left.Row0.Z * right.Row2.X,
                left.Row0.X * right.Row0.Y + left.Row0.Y * right.Row1.Y + left.Row0.Z * right.Row2.Y,
                left.Row0.X * right.Row0.Z + left.Row0.Y * right.Row1.Z + left.Row0.Z * right.Row2.Z),
            new PacketVector3Int32Avx(
                left.Row1.X * right.Row0.X + left.Row1.Y * right.Row1.X + left.Row1.Z * right.Row2.X,
                left.Row1.X * right.Row0.Y + left.Row1.Y * right.Row1.Y + left.Row1.Z * right.Row2.Y,
                left.Row1.X * right.Row0.Z + left.Row1.Y * right.Row1.Z + left.Row1.Z * right.Row2.Z),
            new PacketVector3Int32Avx(
                left.Row2.X * right.Row0.X + left.Row2.Y * right.Row1.X + left.Row2.Z * right.Row2.X,
                left.Row2.X * right.Row0.Y + left.Row2.Y * right.Row1.Y + left.Row2.Z * right.Row2.Y,
                left.Row2.X * right.Row0.Z + left.Row2.Y * right.Row1.Z + left.Row2.Z * right.Row2.Z));
    }

    public static PacketVector3Int32Avx operator *(PacketMatrix3Int32Avx matrix, PacketVector3Int32Avx vector)
    {
        return new(
            matrix.Row0.X * vector.X + matrix.Row0.Y * vector.Y + matrix.Row0.Z * vector.Z,
            matrix.Row1.X * vector.X + matrix.Row1.Y * vector.Y + matrix.Row1.Z * vector.Z,
            matrix.Row2.X * vector.X + matrix.Row2.Y * vector.Y + matrix.Row2.Z * vector.Z);
    }

    public static PacketMatrix3Int32Avx operator *(PacketMatrix3Int32Avx matrix, PacketInt32Avx scalar)
    {
        PacketVector3Int32Avx scalarVector = new(scalar, scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector, matrix.Row2 * scalarVector);
    }

    public static PacketMatrix3Int32Avx operator *(PacketInt32Avx scalar, PacketMatrix3Int32Avx matrix) => matrix * scalar;

    public static PacketMatrix3Int32AvxMask operator ==(PacketMatrix3Int32Avx left, PacketMatrix3Int32Avx right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2);

    public static PacketMatrix3Int32AvxMask operator !=(PacketMatrix3Int32Avx left, PacketMatrix3Int32Avx right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix3Int32Avx other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2);
}

public readonly struct PacketMatrix3Int32AvxMask :
    ISimdMatrix3Mask<PacketMatrix3Int32AvxMask, PacketVector3Int32AvxMask, PacketInt32AvxMask>
{
    public PacketMatrix3Int32AvxMask(PacketVector3Int32AvxMask row0, PacketVector3Int32AvxMask row1, PacketVector3Int32AvxMask row2)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
    }

    public static int LaneCount => PacketVector3Int32AvxMask.LaneCount;

    public PacketVector3Int32AvxMask Row0 { get; }

    public PacketVector3Int32AvxMask Row1 { get; }

    public PacketVector3Int32AvxMask Row2 { get; }

    public static PacketMatrix3Int32AvxMask True => new(PacketVector3Int32AvxMask.True, PacketVector3Int32AvxMask.True, PacketVector3Int32AvxMask.True);

    public static PacketMatrix3Int32AvxMask False => new(PacketVector3Int32AvxMask.False, PacketVector3Int32AvxMask.False, PacketVector3Int32AvxMask.False);

    public static PacketMatrix3Int32AvxMask Create(PacketVector3Int32AvxMask row0, PacketVector3Int32AvxMask row1, PacketVector3Int32AvxMask row2) => new(row0, row1, row2);

    public static PacketMatrix3Int32AvxMask Broadcast(PacketVector3Int32AvxMask value) => new(value, value, value);

    public static PacketVector3Int32AvxMask All(PacketMatrix3Int32AvxMask value) => value.Row0 & value.Row1 & value.Row2;

    public static PacketVector3Int32AvxMask Any(PacketMatrix3Int32AvxMask value) => value.Row0 | value.Row1 | value.Row2;

    public static PacketVector3Int32AvxMask None(PacketMatrix3Int32AvxMask value) => ~(value.Row0 | value.Row1 | value.Row2);

    public static PacketMatrix3Int32AvxMask AndNot(PacketMatrix3Int32AvxMask left, PacketMatrix3Int32AvxMask right)
    {
        return new(
            PacketVector3Int32AvxMask.AndNot(left.Row0, right.Row0),
            PacketVector3Int32AvxMask.AndNot(left.Row1, right.Row1),
            PacketVector3Int32AvxMask.AndNot(left.Row2, right.Row2));
    }

    public static PacketMatrix3Int32AvxMask Select(PacketMatrix3Int32AvxMask mask, PacketMatrix3Int32AvxMask ifTrue, PacketMatrix3Int32AvxMask ifFalse)
    {
        return new(
            PacketVector3Int32AvxMask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector3Int32AvxMask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector3Int32AvxMask.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2));
    }
    public static PacketMatrix3Int32AvxMask And(PacketMatrix3Int32AvxMask left, PacketMatrix3Int32AvxMask right) => left & right;

    public static PacketMatrix3Int32AvxMask Or(PacketMatrix3Int32AvxMask left, PacketMatrix3Int32AvxMask right) => left | right;

    public static PacketMatrix3Int32AvxMask Xor(PacketMatrix3Int32AvxMask left, PacketMatrix3Int32AvxMask right) => left ^ right;

    public static PacketMatrix3Int32AvxMask Not(PacketMatrix3Int32AvxMask value) => ~value;


    public static PacketMatrix3Int32AvxMask operator &(PacketMatrix3Int32AvxMask left, PacketMatrix3Int32AvxMask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1, left.Row2 & right.Row2);

    public static PacketMatrix3Int32AvxMask operator |(PacketMatrix3Int32AvxMask left, PacketMatrix3Int32AvxMask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1, left.Row2 | right.Row2);

    public static PacketMatrix3Int32AvxMask operator ^(PacketMatrix3Int32AvxMask left, PacketMatrix3Int32AvxMask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1, left.Row2 ^ right.Row2);

    public static PacketMatrix3Int32AvxMask operator ~(PacketMatrix3Int32AvxMask value) => new(~value.Row0, ~value.Row1, ~value.Row2);

    public static PacketMatrix3Int32AvxMask operator ==(PacketMatrix3Int32AvxMask left, PacketMatrix3Int32AvxMask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2);

    public static PacketMatrix3Int32AvxMask operator !=(PacketMatrix3Int32AvxMask left, PacketMatrix3Int32AvxMask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix3Int32AvxMask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2);
}
