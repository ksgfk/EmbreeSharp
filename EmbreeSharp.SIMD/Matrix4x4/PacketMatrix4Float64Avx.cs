namespace EmbreeSharp.SIMD;

public readonly struct PacketMatrix4Float64Avx :
    ISimdFloatingPointMatrix4<PacketMatrix4Float64Avx, PacketVector4Float64Avx, PacketFloat64Avx, double, PacketMatrix4Float64AvxMask, PacketVector4Float64AvxMask, PacketFloat64AvxMask>
{
    public PacketMatrix4Float64Avx(PacketVector4Float64Avx row0, PacketVector4Float64Avx row1, PacketVector4Float64Avx row2, PacketVector4Float64Avx row3)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
        Row3 = row3;
    }

    public static int LaneCount => PacketVector4Float64Avx.LaneCount;

    public PacketVector4Float64Avx Row0 { get; }

    public PacketVector4Float64Avx Row1 { get; }

    public PacketVector4Float64Avx Row2 { get; }

    public PacketVector4Float64Avx Row3 { get; }

    public static PacketMatrix4Float64Avx Identity
    {
        get
        {
            PacketFloat64Avx zero = PacketFloat64Avx.Broadcast(0d);
            PacketFloat64Avx one = PacketFloat64Avx.Broadcast(1d);
            return new(
                new PacketVector4Float64Avx(one, zero, zero, zero),
                new PacketVector4Float64Avx(zero, one, zero, zero),
                new PacketVector4Float64Avx(zero, zero, one, zero),
                new PacketVector4Float64Avx(zero, zero, zero, one));
        }
    }

    public static PacketMatrix4Float64Avx Create(PacketVector4Float64Avx row0, PacketVector4Float64Avx row1, PacketVector4Float64Avx row2, PacketVector4Float64Avx row3) => new(row0, row1, row2, row3);

    public static PacketMatrix4Float64Avx Broadcast(double value)
    {
        PacketVector4Float64Avx row = PacketVector4Float64Avx.Broadcast(value);
        return new(row, row, row, row);
    }

    public static PacketMatrix4Float64Avx Select(PacketMatrix4Float64AvxMask mask, PacketMatrix4Float64Avx ifTrue, PacketMatrix4Float64Avx ifFalse)
    {
        return new(
            PacketVector4Float64Avx.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Float64Avx.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Float64Avx.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Float64Avx.Select(mask.Row3, ifTrue.Row3, ifFalse.Row3));
    }

    public static PacketMatrix4Float64Avx Select(PacketVector4Float64AvxMask mask, PacketMatrix4Float64Avx ifTrue, PacketMatrix4Float64Avx ifFalse)
    {
        return new(
            PacketVector4Float64Avx.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Float64Avx.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Float64Avx.Select(mask, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Float64Avx.Select(mask, ifTrue.Row3, ifFalse.Row3));
    }

    public static PacketMatrix4Float64Avx Select(PacketFloat64AvxMask mask, PacketMatrix4Float64Avx ifTrue, PacketMatrix4Float64Avx ifFalse)
    {
        return new(
            PacketVector4Float64Avx.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Float64Avx.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Float64Avx.Select(mask, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Float64Avx.Select(mask, ifTrue.Row3, ifFalse.Row3));
    }

    public static PacketVector4Float64Avx Transform(PacketMatrix4Float64Avx matrix, PacketVector4Float64Avx vector)
    {
        return new(
            PacketFloat64Avx.FusedMultiplyAdd(matrix.Row0.W, vector.W, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row0.Z, vector.Z, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, matrix.Row0.X * vector.X))),
            PacketFloat64Avx.FusedMultiplyAdd(matrix.Row1.W, vector.W, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row1.Z, vector.Z, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, matrix.Row1.X * vector.X))),
            PacketFloat64Avx.FusedMultiplyAdd(matrix.Row2.W, vector.W, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row2.Z, vector.Z, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row2.Y, vector.Y, matrix.Row2.X * vector.X))),
            PacketFloat64Avx.FusedMultiplyAdd(matrix.Row3.W, vector.W, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row3.Z, vector.Z, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row3.Y, vector.Y, matrix.Row3.X * vector.X))));
    }

    public static PacketMatrix4Float64Avx Multiply(PacketMatrix4Float64Avx left, PacketMatrix4Float64Avx right) => left * right;

    public static PacketMatrix4Float64Avx Multiply(PacketMatrix4Float64Avx matrix, PacketFloat64Avx scalar) => matrix * scalar;

    public static PacketMatrix4Float64Avx Multiply(PacketFloat64Avx scalar, PacketMatrix4Float64Avx matrix) => scalar * matrix;

    public static PacketMatrix4Float64Avx FusedMultiplyAdd(PacketMatrix4Float64Avx left, PacketMatrix4Float64Avx right, PacketMatrix4Float64Avx addend)
    {
        return new(
            new PacketVector4Float64Avx(
                PacketFloat64Avx.FusedMultiplyAdd(left.Row0.W, right.Row3.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.X, right.Row0.X, addend.Row0.X)))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row0.W, right.Row3.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.X, right.Row0.Y, addend.Row0.Y)))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row0.W, right.Row3.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.X, right.Row0.Z, addend.Row0.Z)))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row0.W, right.Row3.W, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Z, right.Row2.W, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.W, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.X, right.Row0.W, addend.Row0.W))))),
            new PacketVector4Float64Avx(
                PacketFloat64Avx.FusedMultiplyAdd(left.Row1.W, right.Row3.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.X, right.Row0.X, addend.Row1.X)))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row1.W, right.Row3.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.X, right.Row0.Y, addend.Row1.Y)))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row1.W, right.Row3.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.X, right.Row0.Z, addend.Row1.Z)))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row1.W, right.Row3.W, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Z, right.Row2.W, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.W, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.X, right.Row0.W, addend.Row1.W))))),
            new PacketVector4Float64Avx(
                PacketFloat64Avx.FusedMultiplyAdd(left.Row2.W, right.Row3.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.X, right.Row0.X, addend.Row2.X)))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row2.W, right.Row3.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.X, right.Row0.Y, addend.Row2.Y)))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row2.W, right.Row3.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.X, right.Row0.Z, addend.Row2.Z)))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row2.W, right.Row3.W, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Z, right.Row2.W, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Y, right.Row1.W, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.X, right.Row0.W, addend.Row2.W))))),
            new PacketVector4Float64Avx(
                PacketFloat64Avx.FusedMultiplyAdd(left.Row3.W, right.Row3.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row3.Z, right.Row2.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row3.Y, right.Row1.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row3.X, right.Row0.X, addend.Row3.X)))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row3.W, right.Row3.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row3.Z, right.Row2.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row3.Y, right.Row1.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row3.X, right.Row0.Y, addend.Row3.Y)))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row3.W, right.Row3.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row3.Z, right.Row2.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row3.Y, right.Row1.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row3.X, right.Row0.Z, addend.Row3.Z)))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row3.W, right.Row3.W, PacketFloat64Avx.FusedMultiplyAdd(left.Row3.Z, right.Row2.W, PacketFloat64Avx.FusedMultiplyAdd(left.Row3.Y, right.Row1.W, PacketFloat64Avx.FusedMultiplyAdd(left.Row3.X, right.Row0.W, addend.Row3.W))))));
    }

    public static PacketVector4Float64Avx FusedMultiplyAdd(PacketMatrix4Float64Avx matrix, PacketVector4Float64Avx vector, PacketVector4Float64Avx addend)
    {
        return new(
            PacketFloat64Avx.FusedMultiplyAdd(matrix.Row0.W, vector.W, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row0.Z, vector.Z, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row0.X, vector.X, addend.X)))),
            PacketFloat64Avx.FusedMultiplyAdd(matrix.Row1.W, vector.W, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row1.Z, vector.Z, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row1.X, vector.X, addend.Y)))),
            PacketFloat64Avx.FusedMultiplyAdd(matrix.Row2.W, vector.W, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row2.Z, vector.Z, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row2.Y, vector.Y, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row2.X, vector.X, addend.Z)))),
            PacketFloat64Avx.FusedMultiplyAdd(matrix.Row3.W, vector.W, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row3.Z, vector.Z, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row3.Y, vector.Y, PacketFloat64Avx.FusedMultiplyAdd(matrix.Row3.X, vector.X, addend.W)))));
    }

    public static PacketMatrix4Float64Avx Transpose(PacketMatrix4Float64Avx matrix)
    {
        return new(
            new(matrix.Row0.X, matrix.Row1.X, matrix.Row2.X, matrix.Row3.X),
            new(matrix.Row0.Y, matrix.Row1.Y, matrix.Row2.Y, matrix.Row3.Y),
            new(matrix.Row0.Z, matrix.Row1.Z, matrix.Row2.Z, matrix.Row3.Z),
            new(matrix.Row0.W, matrix.Row1.W, matrix.Row2.W, matrix.Row3.W));
    }

    public static PacketMatrix4Float64Avx Scale(PacketVector4Float64Avx scale) => Scale(scale.X, scale.Y, scale.Z);

    public static PacketMatrix4Float64Avx Scale(PacketFloat64Avx x, PacketFloat64Avx y, PacketFloat64Avx z)
    {
        PacketFloat64Avx zero = PacketFloat64Avx.Broadcast(0d);
        PacketFloat64Avx one = PacketFloat64Avx.Broadcast(1d);
        return new(
            new(x, zero, zero, zero),
            new(zero, y, zero, zero),
            new(zero, zero, z, zero),
            new(zero, zero, zero, one));
    }

    public static PacketMatrix4Float64Avx Translate(PacketVector4Float64Avx translation) => Translate(translation.X, translation.Y, translation.Z);

    public static PacketMatrix4Float64Avx Translate(PacketFloat64Avx x, PacketFloat64Avx y, PacketFloat64Avx z)
    {
        PacketFloat64Avx zero = PacketFloat64Avx.Broadcast(0d);
        PacketFloat64Avx one = PacketFloat64Avx.Broadcast(1d);
        return new(
            new(one, zero, zero, x),
            new(zero, one, zero, y),
            new(zero, zero, one, z),
            new(zero, zero, zero, one));
    }

    public static PacketMatrix4Float64Avx operator *(PacketMatrix4Float64Avx left, PacketMatrix4Float64Avx right)
    {
        return new(
            new PacketVector4Float64Avx(
                PacketFloat64Avx.FusedMultiplyAdd(left.Row0.W, right.Row3.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, left.Row0.X * right.Row0.X))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row0.W, right.Row3.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, left.Row0.X * right.Row0.Y))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row0.W, right.Row3.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, left.Row0.X * right.Row0.Z))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row0.W, right.Row3.W, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Z, right.Row2.W, PacketFloat64Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.W, left.Row0.X * right.Row0.W)))),
            new PacketVector4Float64Avx(
                PacketFloat64Avx.FusedMultiplyAdd(left.Row1.W, right.Row3.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, left.Row1.X * right.Row0.X))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row1.W, right.Row3.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, left.Row1.X * right.Row0.Y))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row1.W, right.Row3.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, left.Row1.X * right.Row0.Z))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row1.W, right.Row3.W, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Z, right.Row2.W, PacketFloat64Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.W, left.Row1.X * right.Row0.W)))),
            new PacketVector4Float64Avx(
                PacketFloat64Avx.FusedMultiplyAdd(left.Row2.W, right.Row3.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, left.Row2.X * right.Row0.X))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row2.W, right.Row3.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, left.Row2.X * right.Row0.Y))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row2.W, right.Row3.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, left.Row2.X * right.Row0.Z))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row2.W, right.Row3.W, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Z, right.Row2.W, PacketFloat64Avx.FusedMultiplyAdd(left.Row2.Y, right.Row1.W, left.Row2.X * right.Row0.W)))),
            new PacketVector4Float64Avx(
                PacketFloat64Avx.FusedMultiplyAdd(left.Row3.W, right.Row3.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row3.Z, right.Row2.X, PacketFloat64Avx.FusedMultiplyAdd(left.Row3.Y, right.Row1.X, left.Row3.X * right.Row0.X))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row3.W, right.Row3.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row3.Z, right.Row2.Y, PacketFloat64Avx.FusedMultiplyAdd(left.Row3.Y, right.Row1.Y, left.Row3.X * right.Row0.Y))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row3.W, right.Row3.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row3.Z, right.Row2.Z, PacketFloat64Avx.FusedMultiplyAdd(left.Row3.Y, right.Row1.Z, left.Row3.X * right.Row0.Z))),
                PacketFloat64Avx.FusedMultiplyAdd(left.Row3.W, right.Row3.W, PacketFloat64Avx.FusedMultiplyAdd(left.Row3.Z, right.Row2.W, PacketFloat64Avx.FusedMultiplyAdd(left.Row3.Y, right.Row1.W, left.Row3.X * right.Row0.W)))));
    }

    public static PacketVector4Float64Avx operator *(PacketMatrix4Float64Avx matrix, PacketVector4Float64Avx vector) => Transform(matrix, vector);

    public static PacketMatrix4Float64Avx operator *(PacketMatrix4Float64Avx matrix, PacketFloat64Avx scalar)
    {
        PacketVector4Float64Avx scalarVector = new(scalar, scalar, scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector, matrix.Row2 * scalarVector, matrix.Row3 * scalarVector);
    }

    public static PacketMatrix4Float64Avx operator *(PacketFloat64Avx scalar, PacketMatrix4Float64Avx matrix) => matrix * scalar;
    public static PacketMatrix4Float64Avx Divide(PacketMatrix4Float64Avx matrix, PacketFloat64Avx scalar) => matrix / scalar;

    public static PacketMatrix4Float64Avx operator /(PacketMatrix4Float64Avx matrix, PacketFloat64Avx scalar) => matrix * PacketFloat64Avx.Reciprocal(scalar);

    public static PacketMatrix4Float64Avx InverseTranspose(PacketMatrix4Float64Avx matrix)
    {
        static PacketVector4Float64Avx Shuffle2301(PacketVector4Float64Avx value) => new(value.Z, value.W, value.X, value.Y);

        static PacketVector4Float64Avx Shuffle1032(PacketVector4Float64Avx value) => new(value.Y, value.X, value.W, value.Z);

        static PacketVector4Float64Avx Fmadd(PacketVector4Float64Avx left, PacketVector4Float64Avx right, PacketVector4Float64Avx addend)
        {
            return new(
                PacketFloat64Avx.FusedMultiplyAdd(left.X, right.X, addend.X),
                PacketFloat64Avx.FusedMultiplyAdd(left.Y, right.Y, addend.Y),
                PacketFloat64Avx.FusedMultiplyAdd(left.Z, right.Z, addend.Z),
                PacketFloat64Avx.FusedMultiplyAdd(left.W, right.W, addend.W));
        }

        static PacketVector4Float64Avx Fmsub(PacketVector4Float64Avx left, PacketVector4Float64Avx right, PacketVector4Float64Avx subtrahend)
        {
            return new(
                PacketFloat64Avx.FusedMultiplyAdd(left.X, right.X, -subtrahend.X),
                PacketFloat64Avx.FusedMultiplyAdd(left.Y, right.Y, -subtrahend.Y),
                PacketFloat64Avx.FusedMultiplyAdd(left.Z, right.Z, -subtrahend.Z),
                PacketFloat64Avx.FusedMultiplyAdd(left.W, right.W, -subtrahend.W));
        }

        static PacketVector4Float64Avx Fnmadd(PacketVector4Float64Avx left, PacketVector4Float64Avx right, PacketVector4Float64Avx addend)
        {
            return new(
                PacketFloat64Avx.FusedMultiplyAdd(-left.X, right.X, addend.X),
                PacketFloat64Avx.FusedMultiplyAdd(-left.Y, right.Y, addend.Y),
                PacketFloat64Avx.FusedMultiplyAdd(-left.Z, right.Z, addend.Z),
                PacketFloat64Avx.FusedMultiplyAdd(-left.W, right.W, addend.W));
        }

        PacketVector4Float64Avx row0 = matrix.Row0;
        PacketVector4Float64Avx row1 = matrix.Row1;
        PacketVector4Float64Avx row2 = matrix.Row2;
        PacketVector4Float64Avx row3 = matrix.Row3;

        row1 = Shuffle2301(row1);
        row3 = Shuffle2301(row3);

        PacketVector4Float64Avx temp = Shuffle1032(row2 * row3);
        PacketVector4Float64Avx col0 = row1 * temp;
        PacketVector4Float64Avx col1 = row0 * temp;
        temp = Shuffle2301(temp);
        col0 = Fmsub(row1, temp, col0);
        col1 = Shuffle2301(Fmsub(row0, temp, col1));

        temp = Shuffle1032(row1 * row2);
        col0 = Fmadd(row3, temp, col0);
        PacketVector4Float64Avx col3 = row0 * temp;
        temp = Shuffle2301(temp);
        col0 = Fnmadd(row3, temp, col0);
        col3 = Shuffle2301(Fmsub(row0, temp, col3));

        temp = Shuffle1032(Shuffle2301(row1) * row3);
        row2 = Shuffle2301(row2);
        col0 = Fmadd(row2, temp, col0);
        PacketVector4Float64Avx col2 = row0 * temp;
        temp = Shuffle2301(temp);
        col0 = Fnmadd(row2, temp, col0);
        col2 = Shuffle2301(Fmsub(row0, temp, col2));

        temp = Shuffle1032(row0 * row1);
        col2 = Fmadd(row3, temp, col2);
        col3 = Fmsub(row2, temp, col3);
        temp = Shuffle2301(temp);
        col2 = Fmsub(row3, temp, col2);
        col3 = Fnmadd(row2, temp, col3);

        temp = Shuffle1032(row0 * row3);
        col1 = Fnmadd(row2, temp, col1);
        col2 = Fmadd(row1, temp, col2);
        temp = Shuffle2301(temp);
        col1 = Fmadd(row2, temp, col1);
        col2 = Fnmadd(row1, temp, col2);

        temp = Shuffle1032(row0 * row2);
        col1 = Fmadd(row3, temp, col1);
        col3 = Fnmadd(row1, temp, col3);
        temp = Shuffle2301(temp);
        col1 = Fnmadd(row3, temp, col1);
        col3 = Fmadd(row1, temp, col3);

        PacketFloat64Avx invDet = PacketFloat64Avx.Reciprocal(PacketVector4Float64Avx.Dot(row0, col0));
        PacketVector4Float64Avx invDetVector = new(invDet, invDet, invDet, invDet);
        return new(col0 * invDetVector, col1 * invDetVector, col2 * invDetVector, col3 * invDetVector);
    }

    public static PacketMatrix4Float64Avx Inverse(PacketMatrix4Float64Avx matrix) => Transpose(InverseTranspose(matrix));

    public static PacketMatrix4Float64Avx Rotate(PacketVector4Float64Avx axis, PacketFloat64Avx angle)
    {
        var (sin, cos) = PacketFloat64Avx.SinCos(angle);
        PacketFloat64Avx zero = PacketFloat64Avx.Broadcast(0d);
        PacketFloat64Avx one = PacketFloat64Avx.Broadcast(1d);
        PacketFloat64Avx oneMinusCos = one - cos;

        PacketFloat64Avx row0X = PacketFloat64Avx.FusedMultiplyAdd(axis.X * axis.X, oneMinusCos, cos);
        PacketFloat64Avx row1Y = PacketFloat64Avx.FusedMultiplyAdd(axis.Y * axis.Y, oneMinusCos, cos);
        PacketFloat64Avx row2Z = PacketFloat64Avx.FusedMultiplyAdd(axis.Z * axis.Z, oneMinusCos, cos);
        PacketFloat64Avx row0Y = PacketFloat64Avx.FusedMultiplyAdd(axis.Y * axis.X, oneMinusCos, -(axis.Z * sin));
        PacketFloat64Avx row0Z = PacketFloat64Avx.FusedMultiplyAdd(axis.Z * axis.X, oneMinusCos, axis.Y * sin);
        PacketFloat64Avx row1X = PacketFloat64Avx.FusedMultiplyAdd(axis.X * axis.Y, oneMinusCos, axis.Z * sin);
        PacketFloat64Avx row1Z = PacketFloat64Avx.FusedMultiplyAdd(axis.Z * axis.Y, oneMinusCos, -(axis.X * sin));
        PacketFloat64Avx row2X = PacketFloat64Avx.FusedMultiplyAdd(axis.X * axis.Z, oneMinusCos, -(axis.Y * sin));
        PacketFloat64Avx row2Y = PacketFloat64Avx.FusedMultiplyAdd(axis.Y * axis.Z, oneMinusCos, axis.X * sin);

        return new(
            new(row0X, row0Y, row0Z, zero),
            new(row1X, row1Y, row1Z, zero),
            new(row2X, row2Y, row2Z, zero),
            new(zero, zero, zero, one));
    }

    public static PacketMatrix4Float64AvxMask operator ==(PacketMatrix4Float64Avx left, PacketMatrix4Float64Avx right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2, left.Row3 == right.Row3);

    public static PacketMatrix4Float64AvxMask operator !=(PacketMatrix4Float64Avx left, PacketMatrix4Float64Avx right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2, left.Row3 != right.Row3);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix4Float64Avx other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2) &&
               Row3.Equals(other.Row3);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2, Row3);
}

public readonly struct PacketMatrix4Float64AvxMask :
    ISimdMatrix4Mask<PacketMatrix4Float64AvxMask, PacketVector4Float64AvxMask, PacketFloat64AvxMask>
{
    public PacketMatrix4Float64AvxMask(PacketVector4Float64AvxMask row0, PacketVector4Float64AvxMask row1, PacketVector4Float64AvxMask row2, PacketVector4Float64AvxMask row3)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
        Row3 = row3;
    }

    public static int LaneCount => PacketVector4Float64AvxMask.LaneCount;

    public PacketVector4Float64AvxMask Row0 { get; }

    public PacketVector4Float64AvxMask Row1 { get; }

    public PacketVector4Float64AvxMask Row2 { get; }

    public PacketVector4Float64AvxMask Row3 { get; }

    public static PacketMatrix4Float64AvxMask True => new(PacketVector4Float64AvxMask.True, PacketVector4Float64AvxMask.True, PacketVector4Float64AvxMask.True, PacketVector4Float64AvxMask.True);

    public static PacketMatrix4Float64AvxMask False => new(PacketVector4Float64AvxMask.False, PacketVector4Float64AvxMask.False, PacketVector4Float64AvxMask.False, PacketVector4Float64AvxMask.False);

    public static PacketMatrix4Float64AvxMask Create(PacketVector4Float64AvxMask row0, PacketVector4Float64AvxMask row1, PacketVector4Float64AvxMask row2, PacketVector4Float64AvxMask row3) => new(row0, row1, row2, row3);

    public static PacketMatrix4Float64AvxMask Broadcast(PacketVector4Float64AvxMask value) => new(value, value, value, value);

    public static PacketVector4Float64AvxMask All(PacketMatrix4Float64AvxMask value) => value.Row0 & value.Row1 & value.Row2 & value.Row3;

    public static PacketVector4Float64AvxMask Any(PacketMatrix4Float64AvxMask value) => value.Row0 | value.Row1 | value.Row2 | value.Row3;

    public static PacketVector4Float64AvxMask None(PacketMatrix4Float64AvxMask value) => ~(value.Row0 | value.Row1 | value.Row2 | value.Row3);

    public static PacketMatrix4Float64AvxMask AndNot(PacketMatrix4Float64AvxMask left, PacketMatrix4Float64AvxMask right)
    {
        return new(
            PacketVector4Float64AvxMask.AndNot(left.Row0, right.Row0),
            PacketVector4Float64AvxMask.AndNot(left.Row1, right.Row1),
            PacketVector4Float64AvxMask.AndNot(left.Row2, right.Row2),
            PacketVector4Float64AvxMask.AndNot(left.Row3, right.Row3));
    }

    public static PacketMatrix4Float64AvxMask Select(PacketMatrix4Float64AvxMask mask, PacketMatrix4Float64AvxMask ifTrue, PacketMatrix4Float64AvxMask ifFalse)
    {
        return new(
            PacketVector4Float64AvxMask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Float64AvxMask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Float64AvxMask.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Float64AvxMask.Select(mask.Row3, ifTrue.Row3, ifFalse.Row3));
    }
    public static PacketMatrix4Float64AvxMask And(PacketMatrix4Float64AvxMask left, PacketMatrix4Float64AvxMask right) => left & right;

    public static PacketMatrix4Float64AvxMask Or(PacketMatrix4Float64AvxMask left, PacketMatrix4Float64AvxMask right) => left | right;

    public static PacketMatrix4Float64AvxMask Xor(PacketMatrix4Float64AvxMask left, PacketMatrix4Float64AvxMask right) => left ^ right;

    public static PacketMatrix4Float64AvxMask Not(PacketMatrix4Float64AvxMask value) => ~value;


    public static PacketMatrix4Float64AvxMask operator &(PacketMatrix4Float64AvxMask left, PacketMatrix4Float64AvxMask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1, left.Row2 & right.Row2, left.Row3 & right.Row3);

    public static PacketMatrix4Float64AvxMask operator |(PacketMatrix4Float64AvxMask left, PacketMatrix4Float64AvxMask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1, left.Row2 | right.Row2, left.Row3 | right.Row3);

    public static PacketMatrix4Float64AvxMask operator ^(PacketMatrix4Float64AvxMask left, PacketMatrix4Float64AvxMask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1, left.Row2 ^ right.Row2, left.Row3 ^ right.Row3);

    public static PacketMatrix4Float64AvxMask operator ~(PacketMatrix4Float64AvxMask value) => new(~value.Row0, ~value.Row1, ~value.Row2, ~value.Row3);

    public static PacketMatrix4Float64AvxMask operator ==(PacketMatrix4Float64AvxMask left, PacketMatrix4Float64AvxMask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2, left.Row3 == right.Row3);

    public static PacketMatrix4Float64AvxMask operator !=(PacketMatrix4Float64AvxMask left, PacketMatrix4Float64AvxMask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2, left.Row3 != right.Row3);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix4Float64AvxMask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2) &&
               Row3.Equals(other.Row3);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2, Row3);
}
