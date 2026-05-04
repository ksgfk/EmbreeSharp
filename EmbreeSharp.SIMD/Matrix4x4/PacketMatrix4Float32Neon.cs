namespace EmbreeSharp.SIMD;

public readonly struct PacketMatrix4Float32Neon :
    ISimdFloatingPointMatrix4<PacketMatrix4Float32Neon, PacketVector4Float32Neon, PacketFloat32Neon, float, PacketMatrix4Float32NeonMask, PacketVector4Float32NeonMask, PacketFloat32NeonMask>
{
    public PacketMatrix4Float32Neon(PacketVector4Float32Neon row0, PacketVector4Float32Neon row1, PacketVector4Float32Neon row2, PacketVector4Float32Neon row3)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
        Row3 = row3;
    }

    public static int LaneCount => PacketVector4Float32Neon.LaneCount;

    public PacketVector4Float32Neon Row0 { get; }

    public PacketVector4Float32Neon Row1 { get; }

    public PacketVector4Float32Neon Row2 { get; }

    public PacketVector4Float32Neon Row3 { get; }

    public static PacketMatrix4Float32Neon Identity
    {
        get
        {
            PacketFloat32Neon zero = PacketFloat32Neon.Broadcast(0f);
            PacketFloat32Neon one = PacketFloat32Neon.Broadcast(1f);
            return new(
                new PacketVector4Float32Neon(one, zero, zero, zero),
                new PacketVector4Float32Neon(zero, one, zero, zero),
                new PacketVector4Float32Neon(zero, zero, one, zero),
                new PacketVector4Float32Neon(zero, zero, zero, one));
        }
    }

    public static PacketMatrix4Float32Neon Create(PacketVector4Float32Neon row0, PacketVector4Float32Neon row1, PacketVector4Float32Neon row2, PacketVector4Float32Neon row3) => new(row0, row1, row2, row3);

    public static PacketMatrix4Float32Neon Broadcast(float value)
    {
        PacketVector4Float32Neon row = PacketVector4Float32Neon.Broadcast(value);
        return new(row, row, row, row);
    }

    public static PacketMatrix4Float32Neon Select(PacketMatrix4Float32NeonMask mask, PacketMatrix4Float32Neon ifTrue, PacketMatrix4Float32Neon ifFalse)
    {
        return new(
            PacketVector4Float32Neon.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Float32Neon.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Float32Neon.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Float32Neon.Select(mask.Row3, ifTrue.Row3, ifFalse.Row3));
    }

    public static PacketMatrix4Float32Neon Select(PacketVector4Float32NeonMask mask, PacketMatrix4Float32Neon ifTrue, PacketMatrix4Float32Neon ifFalse)
    {
        return new(
            PacketVector4Float32Neon.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Float32Neon.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Float32Neon.Select(mask, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Float32Neon.Select(mask, ifTrue.Row3, ifFalse.Row3));
    }

    public static PacketMatrix4Float32Neon Select(PacketFloat32NeonMask mask, PacketMatrix4Float32Neon ifTrue, PacketMatrix4Float32Neon ifFalse)
    {
        return new(
            PacketVector4Float32Neon.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Float32Neon.Select(mask, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Float32Neon.Select(mask, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Float32Neon.Select(mask, ifTrue.Row3, ifFalse.Row3));
    }

    public static PacketVector4Float32Neon Transform(PacketMatrix4Float32Neon matrix, PacketVector4Float32Neon vector)
    {
        return new(
            PacketFloat32Neon.FusedMultiplyAdd(matrix.Row0.W, vector.W, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row0.Z, vector.Z, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, matrix.Row0.X * vector.X))),
            PacketFloat32Neon.FusedMultiplyAdd(matrix.Row1.W, vector.W, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row1.Z, vector.Z, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, matrix.Row1.X * vector.X))),
            PacketFloat32Neon.FusedMultiplyAdd(matrix.Row2.W, vector.W, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row2.Z, vector.Z, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row2.Y, vector.Y, matrix.Row2.X * vector.X))),
            PacketFloat32Neon.FusedMultiplyAdd(matrix.Row3.W, vector.W, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row3.Z, vector.Z, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row3.Y, vector.Y, matrix.Row3.X * vector.X))));
    }

    public static PacketMatrix4Float32Neon Multiply(PacketMatrix4Float32Neon left, PacketMatrix4Float32Neon right) => left * right;

    public static PacketMatrix4Float32Neon Multiply(PacketMatrix4Float32Neon matrix, PacketFloat32Neon scalar) => matrix * scalar;

    public static PacketMatrix4Float32Neon Multiply(PacketFloat32Neon scalar, PacketMatrix4Float32Neon matrix) => scalar * matrix;

    public static PacketMatrix4Float32Neon FusedMultiplyAdd(PacketMatrix4Float32Neon left, PacketMatrix4Float32Neon right, PacketMatrix4Float32Neon addend)
    {
        return new(
            new PacketVector4Float32Neon(
                PacketFloat32Neon.FusedMultiplyAdd(left.Row0.W, right.Row3.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.X, right.Row0.X, addend.Row0.X)))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row0.W, right.Row3.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.X, right.Row0.Y, addend.Row0.Y)))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row0.W, right.Row3.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.X, right.Row0.Z, addend.Row0.Z)))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row0.W, right.Row3.W, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Z, right.Row2.W, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.W, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.X, right.Row0.W, addend.Row0.W))))),
            new PacketVector4Float32Neon(
                PacketFloat32Neon.FusedMultiplyAdd(left.Row1.W, right.Row3.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.X, right.Row0.X, addend.Row1.X)))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row1.W, right.Row3.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.X, right.Row0.Y, addend.Row1.Y)))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row1.W, right.Row3.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.X, right.Row0.Z, addend.Row1.Z)))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row1.W, right.Row3.W, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Z, right.Row2.W, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.W, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.X, right.Row0.W, addend.Row1.W))))),
            new PacketVector4Float32Neon(
                PacketFloat32Neon.FusedMultiplyAdd(left.Row2.W, right.Row3.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.X, right.Row0.X, addend.Row2.X)))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row2.W, right.Row3.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.X, right.Row0.Y, addend.Row2.Y)))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row2.W, right.Row3.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.X, right.Row0.Z, addend.Row2.Z)))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row2.W, right.Row3.W, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Z, right.Row2.W, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Y, right.Row1.W, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.X, right.Row0.W, addend.Row2.W))))),
            new PacketVector4Float32Neon(
                PacketFloat32Neon.FusedMultiplyAdd(left.Row3.W, right.Row3.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row3.Z, right.Row2.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row3.Y, right.Row1.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row3.X, right.Row0.X, addend.Row3.X)))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row3.W, right.Row3.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row3.Z, right.Row2.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row3.Y, right.Row1.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row3.X, right.Row0.Y, addend.Row3.Y)))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row3.W, right.Row3.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row3.Z, right.Row2.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row3.Y, right.Row1.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row3.X, right.Row0.Z, addend.Row3.Z)))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row3.W, right.Row3.W, PacketFloat32Neon.FusedMultiplyAdd(left.Row3.Z, right.Row2.W, PacketFloat32Neon.FusedMultiplyAdd(left.Row3.Y, right.Row1.W, PacketFloat32Neon.FusedMultiplyAdd(left.Row3.X, right.Row0.W, addend.Row3.W))))));
    }

    public static PacketVector4Float32Neon FusedMultiplyAdd(PacketMatrix4Float32Neon matrix, PacketVector4Float32Neon vector, PacketVector4Float32Neon addend)
    {
        return new(
            PacketFloat32Neon.FusedMultiplyAdd(matrix.Row0.W, vector.W, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row0.Z, vector.Z, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row0.X, vector.X, addend.X)))),
            PacketFloat32Neon.FusedMultiplyAdd(matrix.Row1.W, vector.W, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row1.Z, vector.Z, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row1.X, vector.X, addend.Y)))),
            PacketFloat32Neon.FusedMultiplyAdd(matrix.Row2.W, vector.W, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row2.Z, vector.Z, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row2.Y, vector.Y, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row2.X, vector.X, addend.Z)))),
            PacketFloat32Neon.FusedMultiplyAdd(matrix.Row3.W, vector.W, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row3.Z, vector.Z, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row3.Y, vector.Y, PacketFloat32Neon.FusedMultiplyAdd(matrix.Row3.X, vector.X, addend.W)))));
    }

    public static PacketMatrix4Float32Neon Transpose(PacketMatrix4Float32Neon matrix)
    {
        return new(
            new(matrix.Row0.X, matrix.Row1.X, matrix.Row2.X, matrix.Row3.X),
            new(matrix.Row0.Y, matrix.Row1.Y, matrix.Row2.Y, matrix.Row3.Y),
            new(matrix.Row0.Z, matrix.Row1.Z, matrix.Row2.Z, matrix.Row3.Z),
            new(matrix.Row0.W, matrix.Row1.W, matrix.Row2.W, matrix.Row3.W));
    }

    public static PacketMatrix4Float32Neon Scale(PacketVector4Float32Neon scale) => Scale(scale.X, scale.Y, scale.Z);

    public static PacketMatrix4Float32Neon Scale(PacketFloat32Neon x, PacketFloat32Neon y, PacketFloat32Neon z)
    {
        PacketFloat32Neon zero = PacketFloat32Neon.Broadcast(0f);
        PacketFloat32Neon one = PacketFloat32Neon.Broadcast(1f);
        return new(
            new(x, zero, zero, zero),
            new(zero, y, zero, zero),
            new(zero, zero, z, zero),
            new(zero, zero, zero, one));
    }

    public static PacketMatrix4Float32Neon Translate(PacketVector4Float32Neon translation) => Translate(translation.X, translation.Y, translation.Z);

    public static PacketMatrix4Float32Neon Translate(PacketFloat32Neon x, PacketFloat32Neon y, PacketFloat32Neon z)
    {
        PacketFloat32Neon zero = PacketFloat32Neon.Broadcast(0f);
        PacketFloat32Neon one = PacketFloat32Neon.Broadcast(1f);
        return new(
            new(one, zero, zero, x),
            new(zero, one, zero, y),
            new(zero, zero, one, z),
            new(zero, zero, zero, one));
    }

    public static PacketMatrix4Float32Neon operator *(PacketMatrix4Float32Neon left, PacketMatrix4Float32Neon right)
    {
        return new(
            new PacketVector4Float32Neon(
                PacketFloat32Neon.FusedMultiplyAdd(left.Row0.W, right.Row3.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Z, right.Row2.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, left.Row0.X * right.Row0.X))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row0.W, right.Row3.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Z, right.Row2.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, left.Row0.X * right.Row0.Y))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row0.W, right.Row3.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Z, right.Row2.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.Z, left.Row0.X * right.Row0.Z))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row0.W, right.Row3.W, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Z, right.Row2.W, PacketFloat32Neon.FusedMultiplyAdd(left.Row0.Y, right.Row1.W, left.Row0.X * right.Row0.W)))),
            new PacketVector4Float32Neon(
                PacketFloat32Neon.FusedMultiplyAdd(left.Row1.W, right.Row3.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Z, right.Row2.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, left.Row1.X * right.Row0.X))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row1.W, right.Row3.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Z, right.Row2.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, left.Row1.X * right.Row0.Y))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row1.W, right.Row3.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Z, right.Row2.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.Z, left.Row1.X * right.Row0.Z))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row1.W, right.Row3.W, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Z, right.Row2.W, PacketFloat32Neon.FusedMultiplyAdd(left.Row1.Y, right.Row1.W, left.Row1.X * right.Row0.W)))),
            new PacketVector4Float32Neon(
                PacketFloat32Neon.FusedMultiplyAdd(left.Row2.W, right.Row3.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Z, right.Row2.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Y, right.Row1.X, left.Row2.X * right.Row0.X))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row2.W, right.Row3.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Z, right.Row2.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Y, right.Row1.Y, left.Row2.X * right.Row0.Y))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row2.W, right.Row3.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Z, right.Row2.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Y, right.Row1.Z, left.Row2.X * right.Row0.Z))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row2.W, right.Row3.W, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Z, right.Row2.W, PacketFloat32Neon.FusedMultiplyAdd(left.Row2.Y, right.Row1.W, left.Row2.X * right.Row0.W)))),
            new PacketVector4Float32Neon(
                PacketFloat32Neon.FusedMultiplyAdd(left.Row3.W, right.Row3.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row3.Z, right.Row2.X, PacketFloat32Neon.FusedMultiplyAdd(left.Row3.Y, right.Row1.X, left.Row3.X * right.Row0.X))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row3.W, right.Row3.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row3.Z, right.Row2.Y, PacketFloat32Neon.FusedMultiplyAdd(left.Row3.Y, right.Row1.Y, left.Row3.X * right.Row0.Y))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row3.W, right.Row3.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row3.Z, right.Row2.Z, PacketFloat32Neon.FusedMultiplyAdd(left.Row3.Y, right.Row1.Z, left.Row3.X * right.Row0.Z))),
                PacketFloat32Neon.FusedMultiplyAdd(left.Row3.W, right.Row3.W, PacketFloat32Neon.FusedMultiplyAdd(left.Row3.Z, right.Row2.W, PacketFloat32Neon.FusedMultiplyAdd(left.Row3.Y, right.Row1.W, left.Row3.X * right.Row0.W)))));
    }

    public static PacketVector4Float32Neon operator *(PacketMatrix4Float32Neon matrix, PacketVector4Float32Neon vector) => Transform(matrix, vector);

    public static PacketMatrix4Float32Neon operator *(PacketMatrix4Float32Neon matrix, PacketFloat32Neon scalar)
    {
        PacketVector4Float32Neon scalarVector = new(scalar, scalar, scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector, matrix.Row2 * scalarVector, matrix.Row3 * scalarVector);
    }

    public static PacketMatrix4Float32Neon operator *(PacketFloat32Neon scalar, PacketMatrix4Float32Neon matrix) => matrix * scalar;
    public static PacketMatrix4Float32Neon Divide(PacketMatrix4Float32Neon matrix, PacketFloat32Neon scalar) => matrix / scalar;

    public static PacketMatrix4Float32Neon operator /(PacketMatrix4Float32Neon matrix, PacketFloat32Neon scalar) => matrix * PacketFloat32Neon.Reciprocal(scalar);

    public static PacketMatrix4Float32Neon InverseTranspose(PacketMatrix4Float32Neon matrix)
    {
        static PacketVector4Float32Neon Shuffle2301(PacketVector4Float32Neon value) => new(value.Z, value.W, value.X, value.Y);

        static PacketVector4Float32Neon Shuffle1032(PacketVector4Float32Neon value) => new(value.Y, value.X, value.W, value.Z);

        static PacketVector4Float32Neon Fmadd(PacketVector4Float32Neon left, PacketVector4Float32Neon right, PacketVector4Float32Neon addend)
        {
            return new(
                PacketFloat32Neon.FusedMultiplyAdd(left.X, right.X, addend.X),
                PacketFloat32Neon.FusedMultiplyAdd(left.Y, right.Y, addend.Y),
                PacketFloat32Neon.FusedMultiplyAdd(left.Z, right.Z, addend.Z),
                PacketFloat32Neon.FusedMultiplyAdd(left.W, right.W, addend.W));
        }

        static PacketVector4Float32Neon Fmsub(PacketVector4Float32Neon left, PacketVector4Float32Neon right, PacketVector4Float32Neon subtrahend)
        {
            return new(
                PacketFloat32Neon.FusedMultiplyAdd(left.X, right.X, -subtrahend.X),
                PacketFloat32Neon.FusedMultiplyAdd(left.Y, right.Y, -subtrahend.Y),
                PacketFloat32Neon.FusedMultiplyAdd(left.Z, right.Z, -subtrahend.Z),
                PacketFloat32Neon.FusedMultiplyAdd(left.W, right.W, -subtrahend.W));
        }

        static PacketVector4Float32Neon Fnmadd(PacketVector4Float32Neon left, PacketVector4Float32Neon right, PacketVector4Float32Neon addend)
        {
            return new(
                PacketFloat32Neon.FusedMultiplyAdd(-left.X, right.X, addend.X),
                PacketFloat32Neon.FusedMultiplyAdd(-left.Y, right.Y, addend.Y),
                PacketFloat32Neon.FusedMultiplyAdd(-left.Z, right.Z, addend.Z),
                PacketFloat32Neon.FusedMultiplyAdd(-left.W, right.W, addend.W));
        }

        PacketVector4Float32Neon row0 = matrix.Row0;
        PacketVector4Float32Neon row1 = matrix.Row1;
        PacketVector4Float32Neon row2 = matrix.Row2;
        PacketVector4Float32Neon row3 = matrix.Row3;

        row1 = Shuffle2301(row1);
        row3 = Shuffle2301(row3);

        PacketVector4Float32Neon temp = Shuffle1032(row2 * row3);
        PacketVector4Float32Neon col0 = row1 * temp;
        PacketVector4Float32Neon col1 = row0 * temp;
        temp = Shuffle2301(temp);
        col0 = Fmsub(row1, temp, col0);
        col1 = Shuffle2301(Fmsub(row0, temp, col1));

        temp = Shuffle1032(row1 * row2);
        col0 = Fmadd(row3, temp, col0);
        PacketVector4Float32Neon col3 = row0 * temp;
        temp = Shuffle2301(temp);
        col0 = Fnmadd(row3, temp, col0);
        col3 = Shuffle2301(Fmsub(row0, temp, col3));

        temp = Shuffle1032(Shuffle2301(row1) * row3);
        row2 = Shuffle2301(row2);
        col0 = Fmadd(row2, temp, col0);
        PacketVector4Float32Neon col2 = row0 * temp;
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

        PacketFloat32Neon invDet = PacketFloat32Neon.Reciprocal(PacketVector4Float32Neon.Dot(row0, col0));
        PacketVector4Float32Neon invDetVector = new(invDet, invDet, invDet, invDet);
        return new(col0 * invDetVector, col1 * invDetVector, col2 * invDetVector, col3 * invDetVector);
    }

    public static PacketMatrix4Float32Neon Inverse(PacketMatrix4Float32Neon matrix) => Transpose(InverseTranspose(matrix));

    public static PacketMatrix4Float32Neon Rotate(PacketVector4Float32Neon axis, PacketFloat32Neon angle)
    {
        var (sin, cos) = PacketFloat32Neon.SinCos(angle);
        PacketFloat32Neon zero = PacketFloat32Neon.Broadcast(0f);
        PacketFloat32Neon one = PacketFloat32Neon.Broadcast(1f);
        PacketFloat32Neon oneMinusCos = one - cos;

        PacketFloat32Neon row0X = PacketFloat32Neon.FusedMultiplyAdd(axis.X * axis.X, oneMinusCos, cos);
        PacketFloat32Neon row1Y = PacketFloat32Neon.FusedMultiplyAdd(axis.Y * axis.Y, oneMinusCos, cos);
        PacketFloat32Neon row2Z = PacketFloat32Neon.FusedMultiplyAdd(axis.Z * axis.Z, oneMinusCos, cos);
        PacketFloat32Neon row0Y = PacketFloat32Neon.FusedMultiplyAdd(axis.Y * axis.X, oneMinusCos, -(axis.Z * sin));
        PacketFloat32Neon row0Z = PacketFloat32Neon.FusedMultiplyAdd(axis.Z * axis.X, oneMinusCos, axis.Y * sin);
        PacketFloat32Neon row1X = PacketFloat32Neon.FusedMultiplyAdd(axis.X * axis.Y, oneMinusCos, axis.Z * sin);
        PacketFloat32Neon row1Z = PacketFloat32Neon.FusedMultiplyAdd(axis.Z * axis.Y, oneMinusCos, -(axis.X * sin));
        PacketFloat32Neon row2X = PacketFloat32Neon.FusedMultiplyAdd(axis.X * axis.Z, oneMinusCos, -(axis.Y * sin));
        PacketFloat32Neon row2Y = PacketFloat32Neon.FusedMultiplyAdd(axis.Y * axis.Z, oneMinusCos, axis.X * sin);

        return new(
            new(row0X, row0Y, row0Z, zero),
            new(row1X, row1Y, row1Z, zero),
            new(row2X, row2Y, row2Z, zero),
            new(zero, zero, zero, one));
    }

    public static PacketMatrix4Float32NeonMask operator ==(PacketMatrix4Float32Neon left, PacketMatrix4Float32Neon right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2, left.Row3 == right.Row3);

    public static PacketMatrix4Float32NeonMask operator !=(PacketMatrix4Float32Neon left, PacketMatrix4Float32Neon right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2, left.Row3 != right.Row3);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix4Float32Neon other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2) &&
               Row3.Equals(other.Row3);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2, Row3);
}

public readonly struct PacketMatrix4Float32NeonMask :
    ISimdMatrix4Mask<PacketMatrix4Float32NeonMask, PacketVector4Float32NeonMask, PacketFloat32NeonMask>
{
    public PacketMatrix4Float32NeonMask(PacketVector4Float32NeonMask row0, PacketVector4Float32NeonMask row1, PacketVector4Float32NeonMask row2, PacketVector4Float32NeonMask row3)
    {
        Row0 = row0;
        Row1 = row1;
        Row2 = row2;
        Row3 = row3;
    }

    public static int LaneCount => PacketVector4Float32NeonMask.LaneCount;

    public PacketVector4Float32NeonMask Row0 { get; }

    public PacketVector4Float32NeonMask Row1 { get; }

    public PacketVector4Float32NeonMask Row2 { get; }

    public PacketVector4Float32NeonMask Row3 { get; }

    public static PacketMatrix4Float32NeonMask True => new(PacketVector4Float32NeonMask.True, PacketVector4Float32NeonMask.True, PacketVector4Float32NeonMask.True, PacketVector4Float32NeonMask.True);

    public static PacketMatrix4Float32NeonMask False => new(PacketVector4Float32NeonMask.False, PacketVector4Float32NeonMask.False, PacketVector4Float32NeonMask.False, PacketVector4Float32NeonMask.False);

    public static PacketMatrix4Float32NeonMask Create(PacketVector4Float32NeonMask row0, PacketVector4Float32NeonMask row1, PacketVector4Float32NeonMask row2, PacketVector4Float32NeonMask row3) => new(row0, row1, row2, row3);

    public static PacketMatrix4Float32NeonMask Broadcast(PacketVector4Float32NeonMask value) => new(value, value, value, value);

    public static PacketVector4Float32NeonMask All(PacketMatrix4Float32NeonMask value) => value.Row0 & value.Row1 & value.Row2 & value.Row3;

    public static PacketVector4Float32NeonMask Any(PacketMatrix4Float32NeonMask value) => value.Row0 | value.Row1 | value.Row2 | value.Row3;

    public static PacketVector4Float32NeonMask None(PacketMatrix4Float32NeonMask value) => ~(value.Row0 | value.Row1 | value.Row2 | value.Row3);

    public static PacketMatrix4Float32NeonMask AndNot(PacketMatrix4Float32NeonMask left, PacketMatrix4Float32NeonMask right)
    {
        return new(
            PacketVector4Float32NeonMask.AndNot(left.Row0, right.Row0),
            PacketVector4Float32NeonMask.AndNot(left.Row1, right.Row1),
            PacketVector4Float32NeonMask.AndNot(left.Row2, right.Row2),
            PacketVector4Float32NeonMask.AndNot(left.Row3, right.Row3));
    }

    public static PacketMatrix4Float32NeonMask Select(PacketMatrix4Float32NeonMask mask, PacketMatrix4Float32NeonMask ifTrue, PacketMatrix4Float32NeonMask ifFalse)
    {
        return new(
            PacketVector4Float32NeonMask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector4Float32NeonMask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1),
            PacketVector4Float32NeonMask.Select(mask.Row2, ifTrue.Row2, ifFalse.Row2),
            PacketVector4Float32NeonMask.Select(mask.Row3, ifTrue.Row3, ifFalse.Row3));
    }
    public static PacketMatrix4Float32NeonMask And(PacketMatrix4Float32NeonMask left, PacketMatrix4Float32NeonMask right) => left & right;

    public static PacketMatrix4Float32NeonMask Or(PacketMatrix4Float32NeonMask left, PacketMatrix4Float32NeonMask right) => left | right;

    public static PacketMatrix4Float32NeonMask Xor(PacketMatrix4Float32NeonMask left, PacketMatrix4Float32NeonMask right) => left ^ right;

    public static PacketMatrix4Float32NeonMask Not(PacketMatrix4Float32NeonMask value) => ~value;


    public static PacketMatrix4Float32NeonMask operator &(PacketMatrix4Float32NeonMask left, PacketMatrix4Float32NeonMask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1, left.Row2 & right.Row2, left.Row3 & right.Row3);

    public static PacketMatrix4Float32NeonMask operator |(PacketMatrix4Float32NeonMask left, PacketMatrix4Float32NeonMask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1, left.Row2 | right.Row2, left.Row3 | right.Row3);

    public static PacketMatrix4Float32NeonMask operator ^(PacketMatrix4Float32NeonMask left, PacketMatrix4Float32NeonMask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1, left.Row2 ^ right.Row2, left.Row3 ^ right.Row3);

    public static PacketMatrix4Float32NeonMask operator ~(PacketMatrix4Float32NeonMask value) => new(~value.Row0, ~value.Row1, ~value.Row2, ~value.Row3);

    public static PacketMatrix4Float32NeonMask operator ==(PacketMatrix4Float32NeonMask left, PacketMatrix4Float32NeonMask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1, left.Row2 == right.Row2, left.Row3 == right.Row3);

    public static PacketMatrix4Float32NeonMask operator !=(PacketMatrix4Float32NeonMask left, PacketMatrix4Float32NeonMask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1, left.Row2 != right.Row2, left.Row3 != right.Row3);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix4Float32NeonMask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1) &&
               Row2.Equals(other.Row2) &&
               Row3.Equals(other.Row3);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1, Row2, Row3);
}
