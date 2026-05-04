namespace EmbreeSharp.SIMD;

public readonly struct PacketMatrix4Float64Sse :
    ISimdFloatingPointMatrix4<PacketMatrix4Float64Sse, PacketVector4Float64Sse, PacketFloat64Sse, double, PacketMatrix4Float64SseMask, PacketVector4Float64SseMask, PacketFloat64SseMask>
{
    public PacketMatrix4Float64Sse(PacketVector4Float64Sse row0, PacketVector4Float64Sse row1, PacketVector4Float64Sse row2, PacketVector4Float64Sse row3)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
        Row3 = row3;
    }

    public static int LaneCount => PacketVector4Float64Sse.LaneCount;

    public PacketVector4Float64Sse Row0 { get; }

    public PacketVector4Float64Sse Row1 { get; }

    public PacketVector4Float64Sse Row2 { get; }

    public PacketVector4Float64Sse Row3 { get; }

    public static PacketMatrix4Float64Sse Identity
    {
        get
        {
            PacketFloat64Sse zero = PacketFloat64Sse.Broadcast(0d);
            PacketFloat64Sse one = PacketFloat64Sse.Broadcast(1d);
            return new(
                new PacketVector4Float64Sse(one, zero, zero, zero),
                new PacketVector4Float64Sse(zero, one, zero, zero),
                new PacketVector4Float64Sse(zero, zero, one, zero),
                new PacketVector4Float64Sse(zero, zero, zero, one));
        }
    }

    public static PacketMatrix4Float64Sse Create(PacketVector4Float64Sse row0, PacketVector4Float64Sse row1, PacketVector4Float64Sse row2, PacketVector4Float64Sse row3) => new(row0, row1, row2, row3);

    public static PacketMatrix4Float64Sse Broadcast(double value)
    {
        PacketVector4Float64Sse row = PacketVector4Float64Sse.Broadcast(value);
        return new(row, row, row, row);
    }

    public static PacketMatrix4Float64Sse Select(PacketMatrix4Float64SseMask mask, PacketMatrix4Float64Sse ifTrue, PacketMatrix4Float64Sse ifFalse)
    {
        return new(
            PacketVector4Float64Sse.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Float64Sse.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Float64Sse.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Float64Sse.Select(mask.Row3, ifTrue.Row3, ifFalse.Row3));
    }

    public static PacketMatrix4Float64Sse Select(PacketVector4Float64SseMask mask, PacketMatrix4Float64Sse ifTrue, PacketMatrix4Float64Sse ifFalse)
    {
        return new(
            PacketVector4Float64Sse.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Float64Sse.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Float64Sse.Select(mask, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Float64Sse.Select(mask, ifTrue.Row3, ifFalse.Row3));
    }

    public static PacketMatrix4Float64Sse Select(PacketFloat64SseMask mask, PacketMatrix4Float64Sse ifTrue, PacketMatrix4Float64Sse ifFalse)
    {
        return new(
            PacketVector4Float64Sse.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Float64Sse.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Float64Sse.Select(mask, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Float64Sse.Select(mask, ifTrue.Row3, ifFalse.Row3));
    }

    public static PacketVector4Float64Sse Transform(PacketMatrix4Float64Sse matrix, PacketVector4Float64Sse vector)
    {
        return new(
            PacketFloat64Sse.FusedMultiplyAdd(matrix.Row0.W, vector.W, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row0.Z, vector.Z, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, matrix.Row0.X * vector.X))),
            PacketFloat64Sse.FusedMultiplyAdd(matrix.Row1.W, vector.W, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row1.Z, vector.Z, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, matrix.Row1.X * vector.X))),
            PacketFloat64Sse.FusedMultiplyAdd(matrix.Row2.W, vector.W, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row2.Z, vector.Z, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row2.Y, vector.Y, matrix.Row2.X * vector.X))),
            PacketFloat64Sse.FusedMultiplyAdd(matrix.Row3.W, vector.W, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row3.Z, vector.Z, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row3.Y, vector.Y, matrix.Row3.X * vector.X))));
    }

    public static PacketMatrix4Float64Sse Multiply(PacketMatrix4Float64Sse left, PacketMatrix4Float64Sse right) => left * right;

    public static PacketMatrix4Float64Sse Multiply(PacketMatrix4Float64Sse matrix, PacketFloat64Sse scalar) => matrix * scalar;

    public static PacketMatrix4Float64Sse Multiply(PacketFloat64Sse scalar, PacketMatrix4Float64Sse matrix) => scalar * matrix;

    public static PacketMatrix4Float64Sse FusedMultiplyAdd(PacketMatrix4Float64Sse left, PacketMatrix4Float64Sse right, PacketMatrix4Float64Sse addend)
    {
        return new(
            new PacketVector4Float64Sse(
                PacketFloat64Sse.FusedMultiplyAdd(left.Row0.W, right.Row3.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.X, right.Row0.X, addend.Row0.X)))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row0.W, right.Row3.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.X, right.Row0.Y, addend.Row0.Y)))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row0.W, right.Row3.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.X, right.Row0.Z, addend.Row0.Z)))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row0.W, right.Row3.W, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.W, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.W, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.X, right.Row0.W, addend.Row0.W))))),
            new PacketVector4Float64Sse(
                PacketFloat64Sse.FusedMultiplyAdd(left.Row1.W, right.Row3.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.X, right.Row0.X, addend.Row1.X)))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row1.W, right.Row3.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.X, right.Row0.Y, addend.Row1.Y)))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row1.W, right.Row3.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.X, right.Row0.Z, addend.Row1.Z)))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row1.W, right.Row3.W, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.W, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.W, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.X, right.Row0.W, addend.Row1.W))))),
            new PacketVector4Float64Sse(
                PacketFloat64Sse.FusedMultiplyAdd(left.Row2.W, right.Row3.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.X, right.Row0.X, addend.Row2.X)))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row2.W, right.Row3.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.X, right.Row0.Y, addend.Row2.Y)))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row2.W, right.Row3.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.X, right.Row0.Z, addend.Row2.Z)))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row2.W, right.Row3.W, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.W, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.W, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.X, right.Row0.W, addend.Row2.W))))),
            new PacketVector4Float64Sse(
                PacketFloat64Sse.FusedMultiplyAdd(left.Row3.W, right.Row3.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row3.Z, right.Row2.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row3.Y, right.Row1.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row3.X, right.Row0.X, addend.Row3.X)))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row3.W, right.Row3.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row3.Z, right.Row2.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row3.Y, right.Row1.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row3.X, right.Row0.Y, addend.Row3.Y)))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row3.W, right.Row3.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row3.Z, right.Row2.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row3.Y, right.Row1.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row3.X, right.Row0.Z, addend.Row3.Z)))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row3.W, right.Row3.W, PacketFloat64Sse.FusedMultiplyAdd(left.Row3.Z, right.Row2.W, PacketFloat64Sse.FusedMultiplyAdd(left.Row3.Y, right.Row1.W, PacketFloat64Sse.FusedMultiplyAdd(left.Row3.X, right.Row0.W, addend.Row3.W))))));
    }

    public static PacketVector4Float64Sse FusedMultiplyAdd(PacketMatrix4Float64Sse matrix, PacketVector4Float64Sse vector, PacketVector4Float64Sse addend)
    {
        return new(
            PacketFloat64Sse.FusedMultiplyAdd(matrix.Row0.W, vector.W, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row0.Z, vector.Z, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row0.X, vector.X, addend.X)))),
            PacketFloat64Sse.FusedMultiplyAdd(matrix.Row1.W, vector.W, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row1.Z, vector.Z, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row1.X, vector.X, addend.Y)))),
            PacketFloat64Sse.FusedMultiplyAdd(matrix.Row2.W, vector.W, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row2.Z, vector.Z, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row2.Y, vector.Y, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row2.X, vector.X, addend.Z)))),
            PacketFloat64Sse.FusedMultiplyAdd(matrix.Row3.W, vector.W, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row3.Z, vector.Z, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row3.Y, vector.Y, PacketFloat64Sse.FusedMultiplyAdd(matrix.Row3.X, vector.X, addend.W)))));
    }

    public static PacketMatrix4Float64Sse Transpose(PacketMatrix4Float64Sse matrix)
    {
        return new(
            new(matrix.Row0.X, matrix.Row1.X, matrix.Row2.X, matrix.Row3.X),
            new(matrix.Row0.Y, matrix.Row1.Y, matrix.Row2.Y, matrix.Row3.Y),
            new(matrix.Row0.Z, matrix.Row1.Z, matrix.Row2.Z, matrix.Row3.Z),
            new(matrix.Row0.W, matrix.Row1.W, matrix.Row2.W, matrix.Row3.W));
    }

    public static PacketMatrix4Float64Sse Scale(PacketVector4Float64Sse scale) => Scale(scale.X, scale.Y, scale.Z);

    public static PacketMatrix4Float64Sse Scale(PacketFloat64Sse x, PacketFloat64Sse y, PacketFloat64Sse z)
    {
        PacketFloat64Sse zero = PacketFloat64Sse.Broadcast(0d);
        PacketFloat64Sse one = PacketFloat64Sse.Broadcast(1d);
        return new(
            new(x, zero, zero, zero),
            new(zero, y, zero, zero),
            new(zero, zero, z, zero),
            new(zero, zero, zero, one));
    }

    public static PacketMatrix4Float64Sse Translate(PacketVector4Float64Sse translation) => Translate(translation.X, translation.Y, translation.Z);

    public static PacketMatrix4Float64Sse Translate(PacketFloat64Sse x, PacketFloat64Sse y, PacketFloat64Sse z)
    {
        PacketFloat64Sse zero = PacketFloat64Sse.Broadcast(0d);
        PacketFloat64Sse one = PacketFloat64Sse.Broadcast(1d);
        return new(
            new(one, zero, zero, x),
            new(zero, one, zero, y),
            new(zero, zero, one, z),
            new(zero, zero, zero, one));
    }

    public static PacketMatrix4Float64Sse operator *(PacketMatrix4Float64Sse left, PacketMatrix4Float64Sse right)
    {
        return new(
            new PacketVector4Float64Sse(
                PacketFloat64Sse.FusedMultiplyAdd(left.Row0.W, right.Row3.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, left.Row0.X * right.Row0.X))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row0.W, right.Row3.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, left.Row0.X * right.Row0.Y))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row0.W, right.Row3.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, left.Row0.X * right.Row0.Z))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row0.W, right.Row3.W, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.W, PacketFloat64Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.W, left.Row0.X * right.Row0.W)))),
            new PacketVector4Float64Sse(
                PacketFloat64Sse.FusedMultiplyAdd(left.Row1.W, right.Row3.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, left.Row1.X * right.Row0.X))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row1.W, right.Row3.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, left.Row1.X * right.Row0.Y))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row1.W, right.Row3.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, left.Row1.X * right.Row0.Z))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row1.W, right.Row3.W, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.W, PacketFloat64Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.W, left.Row1.X * right.Row0.W)))),
            new PacketVector4Float64Sse(
                PacketFloat64Sse.FusedMultiplyAdd(left.Row2.W, right.Row3.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, left.Row2.X * right.Row0.X))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row2.W, right.Row3.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, left.Row2.X * right.Row0.Y))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row2.W, right.Row3.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, left.Row2.X * right.Row0.Z))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row2.W, right.Row3.W, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.W, PacketFloat64Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.W, left.Row2.X * right.Row0.W)))),
            new PacketVector4Float64Sse(
                PacketFloat64Sse.FusedMultiplyAdd(left.Row3.W, right.Row3.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row3.Z, right.Row2.X, PacketFloat64Sse.FusedMultiplyAdd(left.Row3.Y, right.Row1.X, left.Row3.X * right.Row0.X))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row3.W, right.Row3.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row3.Z, right.Row2.Y, PacketFloat64Sse.FusedMultiplyAdd(left.Row3.Y, right.Row1.Y, left.Row3.X * right.Row0.Y))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row3.W, right.Row3.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row3.Z, right.Row2.Z, PacketFloat64Sse.FusedMultiplyAdd(left.Row3.Y, right.Row1.Z, left.Row3.X * right.Row0.Z))),
                PacketFloat64Sse.FusedMultiplyAdd(left.Row3.W, right.Row3.W, PacketFloat64Sse.FusedMultiplyAdd(left.Row3.Z, right.Row2.W, PacketFloat64Sse.FusedMultiplyAdd(left.Row3.Y, right.Row1.W, left.Row3.X * right.Row0.W)))));
    }

    public static PacketVector4Float64Sse operator *(PacketMatrix4Float64Sse matrix, PacketVector4Float64Sse vector) => Transform(matrix, vector);

    public static PacketMatrix4Float64Sse operator *(PacketMatrix4Float64Sse matrix, PacketFloat64Sse scalar)
    {
        PacketVector4Float64Sse scalarVector = new(scalar, scalar, scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector, matrix.Row2 * scalarVector, matrix.Row3 * scalarVector);
    }

    public static PacketMatrix4Float64Sse operator *(PacketFloat64Sse scalar, PacketMatrix4Float64Sse matrix) => matrix * scalar;
    public static PacketMatrix4Float64Sse Divide(PacketMatrix4Float64Sse matrix, PacketFloat64Sse scalar) => matrix / scalar;

    public static PacketMatrix4Float64Sse operator /(PacketMatrix4Float64Sse matrix, PacketFloat64Sse scalar) => matrix * PacketFloat64Sse.Reciprocal(scalar);

    public static PacketMatrix4Float64Sse InverseTranspose(PacketMatrix4Float64Sse matrix)
    {
        static PacketVector4Float64Sse Shuffle2301(PacketVector4Float64Sse value) => new(value.Z, value.W, value.X, value.Y);

        static PacketVector4Float64Sse Shuffle1032(PacketVector4Float64Sse value) => new(value.Y, value.X, value.W, value.Z);

        static PacketVector4Float64Sse Fmadd(PacketVector4Float64Sse left, PacketVector4Float64Sse right, PacketVector4Float64Sse addend)
        {
            return new(
                PacketFloat64Sse.FusedMultiplyAdd(left.X, right.X, addend.X),
                PacketFloat64Sse.FusedMultiplyAdd(left.Y, right.Y, addend.Y),
                PacketFloat64Sse.FusedMultiplyAdd(left.Z, right.Z, addend.Z),
                PacketFloat64Sse.FusedMultiplyAdd(left.W, right.W, addend.W));
        }

        static PacketVector4Float64Sse Fmsub(PacketVector4Float64Sse left, PacketVector4Float64Sse right, PacketVector4Float64Sse subtrahend)
        {
            return new(
                PacketFloat64Sse.FusedMultiplyAdd(left.X, right.X, -subtrahend.X),
                PacketFloat64Sse.FusedMultiplyAdd(left.Y, right.Y, -subtrahend.Y),
                PacketFloat64Sse.FusedMultiplyAdd(left.Z, right.Z, -subtrahend.Z),
                PacketFloat64Sse.FusedMultiplyAdd(left.W, right.W, -subtrahend.W));
        }

        static PacketVector4Float64Sse Fnmadd(PacketVector4Float64Sse left, PacketVector4Float64Sse right, PacketVector4Float64Sse addend)
        {
            return new(
                PacketFloat64Sse.FusedMultiplyAdd(-left.X, right.X, addend.X),
                PacketFloat64Sse.FusedMultiplyAdd(-left.Y, right.Y, addend.Y),
                PacketFloat64Sse.FusedMultiplyAdd(-left.Z, right.Z, addend.Z),
                PacketFloat64Sse.FusedMultiplyAdd(-left.W, right.W, addend.W));
        }

        PacketVector4Float64Sse row0 = matrix.Row0;
        PacketVector4Float64Sse row1 = matrix.Row1;
        PacketVector4Float64Sse row2 = matrix.Row2;
        PacketVector4Float64Sse row3 = matrix.Row3;

        row1 = Shuffle2301(row1);
        row3 = Shuffle2301(row3);

        PacketVector4Float64Sse temp = Shuffle1032(row2 * row3);
        PacketVector4Float64Sse col0 = row1 * temp;
        PacketVector4Float64Sse col1 = row0 * temp;
        temp = Shuffle2301(temp);
        col0 = Fmsub(row1, temp, col0);
        col1 = Shuffle2301(Fmsub(row0, temp, col1));

        temp = Shuffle1032(row1 * row2);
        col0 = Fmadd(row3, temp, col0);
        PacketVector4Float64Sse col3 = row0 * temp;
        temp = Shuffle2301(temp);
        col0 = Fnmadd(row3, temp, col0);
        col3 = Shuffle2301(Fmsub(row0, temp, col3));

        temp = Shuffle1032(Shuffle2301(row1) * row3);
        row2 = Shuffle2301(row2);
        col0 = Fmadd(row2, temp, col0);
        PacketVector4Float64Sse col2 = row0 * temp;
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

        PacketFloat64Sse invDet = PacketFloat64Sse.Reciprocal(PacketVector4Float64Sse.Dot(row0, col0));
        PacketVector4Float64Sse invDetVector = new(invDet, invDet, invDet, invDet);
        return new(col0 * invDetVector, col1 * invDetVector, col2 * invDetVector, col3 * invDetVector);
    }

    public static PacketMatrix4Float64Sse Inverse(PacketMatrix4Float64Sse matrix) => Transpose(InverseTranspose(matrix));

    public static PacketMatrix4Float64Sse Rotate(PacketVector4Float64Sse axis, PacketFloat64Sse angle)
    {
        var (sin, cos) = PacketFloat64Sse.SinCos(angle);
        PacketFloat64Sse zero = PacketFloat64Sse.Broadcast(0d);
        PacketFloat64Sse one = PacketFloat64Sse.Broadcast(1d);
        PacketFloat64Sse oneMinusCos = one - cos;

        PacketFloat64Sse row0X = PacketFloat64Sse.FusedMultiplyAdd(axis.X * axis.X, oneMinusCos, cos);
        PacketFloat64Sse row1Y = PacketFloat64Sse.FusedMultiplyAdd(axis.Y * axis.Y, oneMinusCos, cos);
        PacketFloat64Sse row2Z = PacketFloat64Sse.FusedMultiplyAdd(axis.Z * axis.Z, oneMinusCos, cos);
        PacketFloat64Sse row0Y = PacketFloat64Sse.FusedMultiplyAdd(axis.Y * axis.X, oneMinusCos, -(axis.Z * sin));
        PacketFloat64Sse row0Z = PacketFloat64Sse.FusedMultiplyAdd(axis.Z * axis.X, oneMinusCos, axis.Y * sin);
        PacketFloat64Sse row1X = PacketFloat64Sse.FusedMultiplyAdd(axis.X * axis.Y, oneMinusCos, axis.Z * sin);
        PacketFloat64Sse row1Z = PacketFloat64Sse.FusedMultiplyAdd(axis.Z * axis.Y, oneMinusCos, -(axis.X * sin));
        PacketFloat64Sse row2X = PacketFloat64Sse.FusedMultiplyAdd(axis.X * axis.Z, oneMinusCos, -(axis.Y * sin));
        PacketFloat64Sse row2Y = PacketFloat64Sse.FusedMultiplyAdd(axis.Y * axis.Z, oneMinusCos, axis.X * sin);

        return new(
            new(row0X, row0Y, row0Z, zero),
            new(row1X, row1Y, row1Z, zero),
            new(row2X, row2Y, row2Z, zero),
            new(zero, zero, zero, one));
    }

    public static PacketMatrix4Float64SseMask operator ==(PacketMatrix4Float64Sse left, PacketMatrix4Float64Sse right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2, left.Row3 == right.Row3);

    public static PacketMatrix4Float64SseMask operator !=(PacketMatrix4Float64Sse left, PacketMatrix4Float64Sse right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2, left.Row3 != right.Row3);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix4Float64Sse other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2) &&
               Row3.Equals(other.Row3);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2, Row3);
}

public readonly struct PacketMatrix4Float64SseMask :
    ISimdMatrix4Mask<PacketMatrix4Float64SseMask, PacketVector4Float64SseMask, PacketFloat64SseMask>
{
    public PacketMatrix4Float64SseMask(PacketVector4Float64SseMask row0, PacketVector4Float64SseMask row1, PacketVector4Float64SseMask row2, PacketVector4Float64SseMask row3)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
        Row3 = row3;
    }

    public static int LaneCount => PacketVector4Float64SseMask.LaneCount;

    public PacketVector4Float64SseMask Row0 { get; }

    public PacketVector4Float64SseMask Row1 { get; }

    public PacketVector4Float64SseMask Row2 { get; }

    public PacketVector4Float64SseMask Row3 { get; }

    public static PacketMatrix4Float64SseMask True => new(PacketVector4Float64SseMask.True, PacketVector4Float64SseMask.True, PacketVector4Float64SseMask.True, PacketVector4Float64SseMask.True);

    public static PacketMatrix4Float64SseMask False => new(PacketVector4Float64SseMask.False, PacketVector4Float64SseMask.False, PacketVector4Float64SseMask.False, PacketVector4Float64SseMask.False);

    public static PacketMatrix4Float64SseMask Create(PacketVector4Float64SseMask row0, PacketVector4Float64SseMask row1, PacketVector4Float64SseMask row2, PacketVector4Float64SseMask row3) => new(row0, row1, row2, row3);

    public static PacketMatrix4Float64SseMask Broadcast(PacketVector4Float64SseMask value) => new(value, value, value, value);

    public static PacketVector4Float64SseMask All(PacketMatrix4Float64SseMask value) => value.Row0 & value.Row1 & value.Row2 & value.Row3;

    public static PacketVector4Float64SseMask Any(PacketMatrix4Float64SseMask value) => value.Row0 | value.Row1 | value.Row2 | value.Row3;

    public static PacketVector4Float64SseMask None(PacketMatrix4Float64SseMask value) => ~(value.Row0 | value.Row1 | value.Row2 | value.Row3);

    public static PacketMatrix4Float64SseMask AndNot(PacketMatrix4Float64SseMask left, PacketMatrix4Float64SseMask right)
    {
        return new(
            PacketVector4Float64SseMask.AndNot(left.Row0, right.Row0),
            PacketVector4Float64SseMask.AndNot(left.Row1, right.Row1),
            PacketVector4Float64SseMask.AndNot(left.Row2, right.Row2),
            PacketVector4Float64SseMask.AndNot(left.Row3, right.Row3));
    }

    public static PacketMatrix4Float64SseMask Select(PacketMatrix4Float64SseMask mask, PacketMatrix4Float64SseMask ifTrue, PacketMatrix4Float64SseMask ifFalse)
    {
        return new(
            PacketVector4Float64SseMask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Float64SseMask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Float64SseMask.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Float64SseMask.Select(mask.Row3, ifTrue.Row3, ifFalse.Row3));
    }
    public static PacketMatrix4Float64SseMask And(PacketMatrix4Float64SseMask left, PacketMatrix4Float64SseMask right) => left & right;

    public static PacketMatrix4Float64SseMask Or(PacketMatrix4Float64SseMask left, PacketMatrix4Float64SseMask right) => left | right;

    public static PacketMatrix4Float64SseMask Xor(PacketMatrix4Float64SseMask left, PacketMatrix4Float64SseMask right) => left ^ right;

    public static PacketMatrix4Float64SseMask Not(PacketMatrix4Float64SseMask value) => ~value;


    public static PacketMatrix4Float64SseMask operator &(PacketMatrix4Float64SseMask left, PacketMatrix4Float64SseMask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1, left.Row2 & right.Row2, left.Row3 & right.Row3);

    public static PacketMatrix4Float64SseMask operator |(PacketMatrix4Float64SseMask left, PacketMatrix4Float64SseMask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1, left.Row2 | right.Row2, left.Row3 | right.Row3);

    public static PacketMatrix4Float64SseMask operator ^(PacketMatrix4Float64SseMask left, PacketMatrix4Float64SseMask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1, left.Row2 ^ right.Row2, left.Row3 ^ right.Row3);

    public static PacketMatrix4Float64SseMask operator ~(PacketMatrix4Float64SseMask value) => new(~value.Row0, ~value.Row1, ~value.Row2, ~value.Row3);

    public static PacketMatrix4Float64SseMask operator ==(PacketMatrix4Float64SseMask left, PacketMatrix4Float64SseMask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2, left.Row3 == right.Row3);

    public static PacketMatrix4Float64SseMask operator !=(PacketMatrix4Float64SseMask left, PacketMatrix4Float64SseMask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2, left.Row3 != right.Row3);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix4Float64SseMask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2) &&
               Row3.Equals(other.Row3);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2, Row3);
}
