namespace EmbreeSharp.SIMD;

public readonly struct PacketMatrix4Float32Sse :
    ISimdFloatingPointMatrix4<PacketMatrix4Float32Sse, PacketVector4Float32Sse, PacketFloat32Sse, float, PacketMatrix4Float32SseMask, PacketVector4Float32SseMask, PacketFloat32SseMask>
{
    public PacketMatrix4Float32Sse(PacketVector4Float32Sse row0, PacketVector4Float32Sse row1, PacketVector4Float32Sse row2, PacketVector4Float32Sse row3)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
        Row3 = row3;
    }

    public static int LaneCount => PacketVector4Float32Sse.LaneCount;

    public PacketVector4Float32Sse Row0 { get; }

    public PacketVector4Float32Sse Row1 { get; }

    public PacketVector4Float32Sse Row2 { get; }

    public PacketVector4Float32Sse Row3 { get; }

    public static PacketMatrix4Float32Sse Identity
    {
        get
        {
            PacketFloat32Sse zero = PacketFloat32Sse.Broadcast(0f);
            PacketFloat32Sse one = PacketFloat32Sse.Broadcast(1f);
            return new(
                new PacketVector4Float32Sse(one, zero, zero, zero),
                new PacketVector4Float32Sse(zero, one, zero, zero),
                new PacketVector4Float32Sse(zero, zero, one, zero),
                new PacketVector4Float32Sse(zero, zero, zero, one));
        }
    }

    public static PacketMatrix4Float32Sse Create(PacketVector4Float32Sse row0, PacketVector4Float32Sse row1, PacketVector4Float32Sse row2, PacketVector4Float32Sse row3) => new(row0, row1, row2, row3);

    public static PacketMatrix4Float32Sse Broadcast(float value)
    {
        PacketVector4Float32Sse row = PacketVector4Float32Sse.Broadcast(value);
        return new(row, row, row, row);
    }

    public static PacketMatrix4Float32Sse Select(PacketMatrix4Float32SseMask mask, PacketMatrix4Float32Sse ifTrue, PacketMatrix4Float32Sse ifFalse)
    {
        return new(
            PacketVector4Float32Sse.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Float32Sse.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Float32Sse.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Float32Sse.Select(mask.Row3, ifTrue.Row3, ifFalse.Row3));
    }

    public static PacketMatrix4Float32Sse Select(PacketVector4Float32SseMask mask, PacketMatrix4Float32Sse ifTrue, PacketMatrix4Float32Sse ifFalse)
    {
        return new(
            PacketVector4Float32Sse.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Float32Sse.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Float32Sse.Select(mask, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Float32Sse.Select(mask, ifTrue.Row3, ifFalse.Row3));
    }

    public static PacketMatrix4Float32Sse Select(PacketFloat32SseMask mask, PacketMatrix4Float32Sse ifTrue, PacketMatrix4Float32Sse ifFalse)
    {
        return new(
            PacketVector4Float32Sse.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Float32Sse.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Float32Sse.Select(mask, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Float32Sse.Select(mask, ifTrue.Row3, ifFalse.Row3));
    }

    public static PacketVector4Float32Sse Transform(PacketMatrix4Float32Sse matrix, PacketVector4Float32Sse vector)
    {
        return new(
            PacketFloat32Sse.FusedMultiplyAdd(matrix.Row0.W, vector.W, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row0.Z, vector.Z, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, matrix.Row0.X * vector.X))),
            PacketFloat32Sse.FusedMultiplyAdd(matrix.Row1.W, vector.W, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row1.Z, vector.Z, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, matrix.Row1.X * vector.X))),
            PacketFloat32Sse.FusedMultiplyAdd(matrix.Row2.W, vector.W, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row2.Z, vector.Z, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row2.Y, vector.Y, matrix.Row2.X * vector.X))),
            PacketFloat32Sse.FusedMultiplyAdd(matrix.Row3.W, vector.W, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row3.Z, vector.Z, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row3.Y, vector.Y, matrix.Row3.X * vector.X))));
    }

    public static PacketMatrix4Float32Sse Multiply(PacketMatrix4Float32Sse left, PacketMatrix4Float32Sse right) => left * right;

    public static PacketMatrix4Float32Sse Multiply(PacketMatrix4Float32Sse matrix, PacketFloat32Sse scalar) => matrix * scalar;

    public static PacketMatrix4Float32Sse Multiply(PacketFloat32Sse scalar, PacketMatrix4Float32Sse matrix) => scalar * matrix;

    public static PacketMatrix4Float32Sse FusedMultiplyAdd(PacketMatrix4Float32Sse left, PacketMatrix4Float32Sse right, PacketMatrix4Float32Sse addend)
    {
        return new(
            new PacketVector4Float32Sse(
                PacketFloat32Sse.FusedMultiplyAdd(left.Row0.W, right.Row3.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.X, right.Row0.X, addend.Row0.X)))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row0.W, right.Row3.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.X, right.Row0.Y, addend.Row0.Y)))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row0.W, right.Row3.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.X, right.Row0.Z, addend.Row0.Z)))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row0.W, right.Row3.W, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.W, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.W, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.X, right.Row0.W, addend.Row0.W))))),
            new PacketVector4Float32Sse(
                PacketFloat32Sse.FusedMultiplyAdd(left.Row1.W, right.Row3.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.X, right.Row0.X, addend.Row1.X)))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row1.W, right.Row3.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.X, right.Row0.Y, addend.Row1.Y)))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row1.W, right.Row3.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.X, right.Row0.Z, addend.Row1.Z)))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row1.W, right.Row3.W, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.W, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.W, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.X, right.Row0.W, addend.Row1.W))))),
            new PacketVector4Float32Sse(
                PacketFloat32Sse.FusedMultiplyAdd(left.Row2.W, right.Row3.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.X, right.Row0.X, addend.Row2.X)))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row2.W, right.Row3.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.X, right.Row0.Y, addend.Row2.Y)))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row2.W, right.Row3.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.X, right.Row0.Z, addend.Row2.Z)))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row2.W, right.Row3.W, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.W, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.W, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.X, right.Row0.W, addend.Row2.W))))),
            new PacketVector4Float32Sse(
                PacketFloat32Sse.FusedMultiplyAdd(left.Row3.W, right.Row3.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row3.Z, right.Row2.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row3.Y, right.Row1.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row3.X, right.Row0.X, addend.Row3.X)))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row3.W, right.Row3.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row3.Z, right.Row2.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row3.Y, right.Row1.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row3.X, right.Row0.Y, addend.Row3.Y)))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row3.W, right.Row3.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row3.Z, right.Row2.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row3.Y, right.Row1.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row3.X, right.Row0.Z, addend.Row3.Z)))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row3.W, right.Row3.W, PacketFloat32Sse.FusedMultiplyAdd(left.Row3.Z, right.Row2.W, PacketFloat32Sse.FusedMultiplyAdd(left.Row3.Y, right.Row1.W, PacketFloat32Sse.FusedMultiplyAdd(left.Row3.X, right.Row0.W, addend.Row3.W))))));
    }

    public static PacketVector4Float32Sse FusedMultiplyAdd(PacketMatrix4Float32Sse matrix, PacketVector4Float32Sse vector, PacketVector4Float32Sse addend)
    {
        return new(
            PacketFloat32Sse.FusedMultiplyAdd(matrix.Row0.W, vector.W, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row0.Z, vector.Z, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row0.X, vector.X, addend.X)))),
            PacketFloat32Sse.FusedMultiplyAdd(matrix.Row1.W, vector.W, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row1.Z, vector.Z, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row1.X, vector.X, addend.Y)))),
            PacketFloat32Sse.FusedMultiplyAdd(matrix.Row2.W, vector.W, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row2.Z, vector.Z, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row2.Y, vector.Y, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row2.X, vector.X, addend.Z)))),
            PacketFloat32Sse.FusedMultiplyAdd(matrix.Row3.W, vector.W, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row3.Z, vector.Z, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row3.Y, vector.Y, PacketFloat32Sse.FusedMultiplyAdd(matrix.Row3.X, vector.X, addend.W)))));
    }

    public static PacketMatrix4Float32Sse Transpose(PacketMatrix4Float32Sse matrix)
    {
        return new(
            new(matrix.Row0.X, matrix.Row1.X, matrix.Row2.X, matrix.Row3.X),
            new(matrix.Row0.Y, matrix.Row1.Y, matrix.Row2.Y, matrix.Row3.Y),
            new(matrix.Row0.Z, matrix.Row1.Z, matrix.Row2.Z, matrix.Row3.Z),
            new(matrix.Row0.W, matrix.Row1.W, matrix.Row2.W, matrix.Row3.W));
    }

    public static PacketMatrix4Float32Sse Scale(PacketVector4Float32Sse scale) => Scale(scale.X, scale.Y, scale.Z);

    public static PacketMatrix4Float32Sse Scale(PacketFloat32Sse x, PacketFloat32Sse y, PacketFloat32Sse z)
    {
        PacketFloat32Sse zero = PacketFloat32Sse.Broadcast(0f);
        PacketFloat32Sse one = PacketFloat32Sse.Broadcast(1f);
        return new(
            new(x, zero, zero, zero),
            new(zero, y, zero, zero),
            new(zero, zero, z, zero),
            new(zero, zero, zero, one));
    }

    public static PacketMatrix4Float32Sse Translate(PacketVector4Float32Sse translation) => Translate(translation.X, translation.Y, translation.Z);

    public static PacketMatrix4Float32Sse Translate(PacketFloat32Sse x, PacketFloat32Sse y, PacketFloat32Sse z)
    {
        PacketFloat32Sse zero = PacketFloat32Sse.Broadcast(0f);
        PacketFloat32Sse one = PacketFloat32Sse.Broadcast(1f);
        return new(
            new(one, zero, zero, x),
            new(zero, one, zero, y),
            new(zero, zero, one, z),
            new(zero, zero, zero, one));
    }

    public static PacketMatrix4Float32Sse operator *(PacketMatrix4Float32Sse left, PacketMatrix4Float32Sse right)
    {
        return new(
            new PacketVector4Float32Sse(
                PacketFloat32Sse.FusedMultiplyAdd(left.Row0.W, right.Row3.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, left.Row0.X * right.Row0.X))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row0.W, right.Row3.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, left.Row0.X * right.Row0.Y))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row0.W, right.Row3.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, left.Row0.X * right.Row0.Z))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row0.W, right.Row3.W, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Z, right.Row2.W, PacketFloat32Sse.FusedMultiplyAdd(left.Row0.Y, right.Row1.W, left.Row0.X * right.Row0.W)))),
            new PacketVector4Float32Sse(
                PacketFloat32Sse.FusedMultiplyAdd(left.Row1.W, right.Row3.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, left.Row1.X * right.Row0.X))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row1.W, right.Row3.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, left.Row1.X * right.Row0.Y))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row1.W, right.Row3.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, left.Row1.X * right.Row0.Z))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row1.W, right.Row3.W, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Z, right.Row2.W, PacketFloat32Sse.FusedMultiplyAdd(left.Row1.Y, right.Row1.W, left.Row1.X * right.Row0.W)))),
            new PacketVector4Float32Sse(
                PacketFloat32Sse.FusedMultiplyAdd(left.Row2.W, right.Row3.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, left.Row2.X * right.Row0.X))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row2.W, right.Row3.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, left.Row2.X * right.Row0.Y))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row2.W, right.Row3.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, left.Row2.X * right.Row0.Z))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row2.W, right.Row3.W, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Z, right.Row2.W, PacketFloat32Sse.FusedMultiplyAdd(left.Row2.Y, right.Row1.W, left.Row2.X * right.Row0.W)))),
            new PacketVector4Float32Sse(
                PacketFloat32Sse.FusedMultiplyAdd(left.Row3.W, right.Row3.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row3.Z, right.Row2.X, PacketFloat32Sse.FusedMultiplyAdd(left.Row3.Y, right.Row1.X, left.Row3.X * right.Row0.X))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row3.W, right.Row3.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row3.Z, right.Row2.Y, PacketFloat32Sse.FusedMultiplyAdd(left.Row3.Y, right.Row1.Y, left.Row3.X * right.Row0.Y))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row3.W, right.Row3.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row3.Z, right.Row2.Z, PacketFloat32Sse.FusedMultiplyAdd(left.Row3.Y, right.Row1.Z, left.Row3.X * right.Row0.Z))),
                PacketFloat32Sse.FusedMultiplyAdd(left.Row3.W, right.Row3.W, PacketFloat32Sse.FusedMultiplyAdd(left.Row3.Z, right.Row2.W, PacketFloat32Sse.FusedMultiplyAdd(left.Row3.Y, right.Row1.W, left.Row3.X * right.Row0.W)))));
    }

    public static PacketVector4Float32Sse operator *(PacketMatrix4Float32Sse matrix, PacketVector4Float32Sse vector) => Transform(matrix, vector);

    public static PacketMatrix4Float32Sse operator *(PacketMatrix4Float32Sse matrix, PacketFloat32Sse scalar)
    {
        PacketVector4Float32Sse scalarVector = new(scalar, scalar, scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector, matrix.Row2 * scalarVector, matrix.Row3 * scalarVector);
    }

    public static PacketMatrix4Float32Sse operator *(PacketFloat32Sse scalar, PacketMatrix4Float32Sse matrix) => matrix * scalar;
    public static PacketMatrix4Float32Sse Divide(PacketMatrix4Float32Sse matrix, PacketFloat32Sse scalar) => matrix / scalar;

    public static PacketMatrix4Float32Sse operator /(PacketMatrix4Float32Sse matrix, PacketFloat32Sse scalar) => matrix * PacketFloat32Sse.Reciprocal(scalar);

    public static PacketMatrix4Float32Sse InverseTranspose(PacketMatrix4Float32Sse matrix)
    {
        static PacketVector4Float32Sse Shuffle2301(PacketVector4Float32Sse value) => new(value.Z, value.W, value.X, value.Y);

        static PacketVector4Float32Sse Shuffle1032(PacketVector4Float32Sse value) => new(value.Y, value.X, value.W, value.Z);

        static PacketVector4Float32Sse Fmadd(PacketVector4Float32Sse left, PacketVector4Float32Sse right, PacketVector4Float32Sse addend)
        {
            return new(
                PacketFloat32Sse.FusedMultiplyAdd(left.X, right.X, addend.X),
                PacketFloat32Sse.FusedMultiplyAdd(left.Y, right.Y, addend.Y),
                PacketFloat32Sse.FusedMultiplyAdd(left.Z, right.Z, addend.Z),
                PacketFloat32Sse.FusedMultiplyAdd(left.W, right.W, addend.W));
        }

        static PacketVector4Float32Sse Fmsub(PacketVector4Float32Sse left, PacketVector4Float32Sse right, PacketVector4Float32Sse subtrahend)
        {
            return new(
                PacketFloat32Sse.FusedMultiplyAdd(left.X, right.X, -subtrahend.X),
                PacketFloat32Sse.FusedMultiplyAdd(left.Y, right.Y, -subtrahend.Y),
                PacketFloat32Sse.FusedMultiplyAdd(left.Z, right.Z, -subtrahend.Z),
                PacketFloat32Sse.FusedMultiplyAdd(left.W, right.W, -subtrahend.W));
        }

        static PacketVector4Float32Sse Fnmadd(PacketVector4Float32Sse left, PacketVector4Float32Sse right, PacketVector4Float32Sse addend)
        {
            return new(
                PacketFloat32Sse.FusedMultiplyAdd(-left.X, right.X, addend.X),
                PacketFloat32Sse.FusedMultiplyAdd(-left.Y, right.Y, addend.Y),
                PacketFloat32Sse.FusedMultiplyAdd(-left.Z, right.Z, addend.Z),
                PacketFloat32Sse.FusedMultiplyAdd(-left.W, right.W, addend.W));
        }

        PacketVector4Float32Sse row0 = matrix.Row0;
        PacketVector4Float32Sse row1 = matrix.Row1;
        PacketVector4Float32Sse row2 = matrix.Row2;
        PacketVector4Float32Sse row3 = matrix.Row3;

        row1 = Shuffle2301(row1);
        row3 = Shuffle2301(row3);

        PacketVector4Float32Sse temp = Shuffle1032(row2 * row3);
        PacketVector4Float32Sse col0 = row1 * temp;
        PacketVector4Float32Sse col1 = row0 * temp;
        temp = Shuffle2301(temp);
        col0 = Fmsub(row1, temp, col0);
        col1 = Shuffle2301(Fmsub(row0, temp, col1));

        temp = Shuffle1032(row1 * row2);
        col0 = Fmadd(row3, temp, col0);
        PacketVector4Float32Sse col3 = row0 * temp;
        temp = Shuffle2301(temp);
        col0 = Fnmadd(row3, temp, col0);
        col3 = Shuffle2301(Fmsub(row0, temp, col3));

        temp = Shuffle1032(Shuffle2301(row1) * row3);
        row2 = Shuffle2301(row2);
        col0 = Fmadd(row2, temp, col0);
        PacketVector4Float32Sse col2 = row0 * temp;
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

        PacketFloat32Sse invDet = PacketFloat32Sse.Reciprocal(PacketVector4Float32Sse.Dot(row0, col0));
        PacketVector4Float32Sse invDetVector = new(invDet, invDet, invDet, invDet);
        return new(col0 * invDetVector, col1 * invDetVector, col2 * invDetVector, col3 * invDetVector);
    }

    public static PacketMatrix4Float32Sse Inverse(PacketMatrix4Float32Sse matrix) => Transpose(InverseTranspose(matrix));

    public static PacketMatrix4Float32Sse Rotate(PacketVector4Float32Sse axis, PacketFloat32Sse angle)
    {
        var (sin, cos) = PacketFloat32Sse.SinCos(angle);
        PacketFloat32Sse zero = PacketFloat32Sse.Broadcast(0f);
        PacketFloat32Sse one = PacketFloat32Sse.Broadcast(1f);
        PacketFloat32Sse oneMinusCos = one - cos;

        PacketFloat32Sse row0X = PacketFloat32Sse.FusedMultiplyAdd(axis.X * axis.X, oneMinusCos, cos);
        PacketFloat32Sse row1Y = PacketFloat32Sse.FusedMultiplyAdd(axis.Y * axis.Y, oneMinusCos, cos);
        PacketFloat32Sse row2Z = PacketFloat32Sse.FusedMultiplyAdd(axis.Z * axis.Z, oneMinusCos, cos);
        PacketFloat32Sse row0Y = PacketFloat32Sse.FusedMultiplyAdd(axis.Y * axis.X, oneMinusCos, -(axis.Z * sin));
        PacketFloat32Sse row0Z = PacketFloat32Sse.FusedMultiplyAdd(axis.Z * axis.X, oneMinusCos, axis.Y * sin);
        PacketFloat32Sse row1X = PacketFloat32Sse.FusedMultiplyAdd(axis.X * axis.Y, oneMinusCos, axis.Z * sin);
        PacketFloat32Sse row1Z = PacketFloat32Sse.FusedMultiplyAdd(axis.Z * axis.Y, oneMinusCos, -(axis.X * sin));
        PacketFloat32Sse row2X = PacketFloat32Sse.FusedMultiplyAdd(axis.X * axis.Z, oneMinusCos, -(axis.Y * sin));
        PacketFloat32Sse row2Y = PacketFloat32Sse.FusedMultiplyAdd(axis.Y * axis.Z, oneMinusCos, axis.X * sin);

        return new(
            new(row0X, row0Y, row0Z, zero),
            new(row1X, row1Y, row1Z, zero),
            new(row2X, row2Y, row2Z, zero),
            new(zero, zero, zero, one));
    }

    public static PacketMatrix4Float32SseMask operator ==(PacketMatrix4Float32Sse left, PacketMatrix4Float32Sse right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2, left.Row3 == right.Row3);

    public static PacketMatrix4Float32SseMask operator !=(PacketMatrix4Float32Sse left, PacketMatrix4Float32Sse right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2, left.Row3 != right.Row3);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix4Float32Sse other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2) &&
               Row3.Equals(other.Row3);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2, Row3);
}

public readonly struct PacketMatrix4Float32SseMask :
    ISimdMatrix4Mask<PacketMatrix4Float32SseMask, PacketVector4Float32SseMask, PacketFloat32SseMask>
{
    public PacketMatrix4Float32SseMask(PacketVector4Float32SseMask row0, PacketVector4Float32SseMask row1, PacketVector4Float32SseMask row2, PacketVector4Float32SseMask row3)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
        Row3 = row3;
    }

    public static int LaneCount => PacketVector4Float32SseMask.LaneCount;

    public PacketVector4Float32SseMask Row0 { get; }

    public PacketVector4Float32SseMask Row1 { get; }

    public PacketVector4Float32SseMask Row2 { get; }

    public PacketVector4Float32SseMask Row3 { get; }

    public static PacketMatrix4Float32SseMask True => new(PacketVector4Float32SseMask.True, PacketVector4Float32SseMask.True, PacketVector4Float32SseMask.True, PacketVector4Float32SseMask.True);

    public static PacketMatrix4Float32SseMask False => new(PacketVector4Float32SseMask.False, PacketVector4Float32SseMask.False, PacketVector4Float32SseMask.False, PacketVector4Float32SseMask.False);

    public static PacketMatrix4Float32SseMask Create(PacketVector4Float32SseMask row0, PacketVector4Float32SseMask row1, PacketVector4Float32SseMask row2, PacketVector4Float32SseMask row3) => new(row0, row1, row2, row3);

    public static PacketMatrix4Float32SseMask Broadcast(PacketVector4Float32SseMask value) => new(value, value, value, value);

    public static PacketVector4Float32SseMask All(PacketMatrix4Float32SseMask value) => value.Row0 & value.Row1 & value.Row2 & value.Row3;

    public static PacketVector4Float32SseMask Any(PacketMatrix4Float32SseMask value) => value.Row0 | value.Row1 | value.Row2 | value.Row3;

    public static PacketVector4Float32SseMask None(PacketMatrix4Float32SseMask value) => ~(value.Row0 | value.Row1 | value.Row2 | value.Row3);

    public static PacketMatrix4Float32SseMask AndNot(PacketMatrix4Float32SseMask left, PacketMatrix4Float32SseMask right)
    {
        return new(
            PacketVector4Float32SseMask.AndNot(left.Row0, right.Row0),
            PacketVector4Float32SseMask.AndNot(left.Row1, right.Row1),
            PacketVector4Float32SseMask.AndNot(left.Row2, right.Row2),
            PacketVector4Float32SseMask.AndNot(left.Row3, right.Row3));
    }

    public static PacketMatrix4Float32SseMask Select(PacketMatrix4Float32SseMask mask, PacketMatrix4Float32SseMask ifTrue, PacketMatrix4Float32SseMask ifFalse)
    {
        return new(
            PacketVector4Float32SseMask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Float32SseMask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Float32SseMask.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Float32SseMask.Select(mask.Row3, ifTrue.Row3, ifFalse.Row3));
    }
    public static PacketMatrix4Float32SseMask And(PacketMatrix4Float32SseMask left, PacketMatrix4Float32SseMask right) => left & right;

    public static PacketMatrix4Float32SseMask Or(PacketMatrix4Float32SseMask left, PacketMatrix4Float32SseMask right) => left | right;

    public static PacketMatrix4Float32SseMask Xor(PacketMatrix4Float32SseMask left, PacketMatrix4Float32SseMask right) => left ^ right;

    public static PacketMatrix4Float32SseMask Not(PacketMatrix4Float32SseMask value) => ~value;


    public static PacketMatrix4Float32SseMask operator &(PacketMatrix4Float32SseMask left, PacketMatrix4Float32SseMask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1, left.Row2 & right.Row2, left.Row3 & right.Row3);

    public static PacketMatrix4Float32SseMask operator |(PacketMatrix4Float32SseMask left, PacketMatrix4Float32SseMask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1, left.Row2 | right.Row2, left.Row3 | right.Row3);

    public static PacketMatrix4Float32SseMask operator ^(PacketMatrix4Float32SseMask left, PacketMatrix4Float32SseMask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1, left.Row2 ^ right.Row2, left.Row3 ^ right.Row3);

    public static PacketMatrix4Float32SseMask operator ~(PacketMatrix4Float32SseMask value) => new(~value.Row0, ~value.Row1, ~value.Row2, ~value.Row3);

    public static PacketMatrix4Float32SseMask operator ==(PacketMatrix4Float32SseMask left, PacketMatrix4Float32SseMask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2, left.Row3 == right.Row3);

    public static PacketMatrix4Float32SseMask operator !=(PacketMatrix4Float32SseMask left, PacketMatrix4Float32SseMask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2, left.Row3 != right.Row3);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix4Float32SseMask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2) &&
               Row3.Equals(other.Row3);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2, Row3);
}
