using System.Runtime.CompilerServices;

namespace EmbreeSharp.SIMD;

public readonly struct PacketMatrix2Float32Avx :
    ISimdFloatingPointMatrix2<PacketMatrix2Float32Avx, PacketVector2Float32Avx, PacketFloat32Avx, float, PacketMatrix2Float32AvxMask, PacketVector2Float32AvxMask, PacketFloat32AvxMask>
{
    public PacketMatrix2Float32Avx(PacketVector2Float32Avx row0, PacketVector2Float32Avx row1)
    {
        Row0 = row0;
        Row1 = row1;
    }

    public static int LaneCount => PacketVector2Float32Avx.LaneCount;

    public PacketVector2Float32Avx Row0 { get; }

    public PacketVector2Float32Avx Row1 { get; }

    public static PacketMatrix2Float32Avx Identity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            PacketFloat32Avx zero = PacketFloat32Avx.Broadcast(0f);
            PacketFloat32Avx one = PacketFloat32Avx.Broadcast(1f);
            return new(
                new PacketVector2Float32Avx(one, zero),
                new PacketVector2Float32Avx(zero, one));
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32Avx Create(PacketVector2Float32Avx row0, PacketVector2Float32Avx row1) => new(row0, row1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32Avx Broadcast(float value)
    {
        PacketVector2Float32Avx row = PacketVector2Float32Avx.Broadcast(value);
        return new(row, row);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32Avx Select(PacketMatrix2Float32AvxMask mask, PacketMatrix2Float32Avx ifTrue, PacketMatrix2Float32Avx ifFalse)
    {
        return new(
            PacketVector2Float32Avx.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Float32Avx.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32Avx Select(PacketVector2Float32AvxMask mask, PacketMatrix2Float32Avx ifTrue, PacketMatrix2Float32Avx ifFalse)
    {
        return new(
            PacketVector2Float32Avx.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Float32Avx.Select(mask, ifTrue.Row1, ifFalse.Row1));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32Avx Select(PacketFloat32AvxMask mask, PacketMatrix2Float32Avx ifTrue, PacketMatrix2Float32Avx ifFalse)
    {
        return new(
            PacketVector2Float32Avx.Select(mask, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Float32Avx.Select(mask, ifTrue.Row1, ifFalse.Row1));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Avx Transform(PacketMatrix2Float32Avx matrix, PacketVector2Float32Avx vector)
    {
        return new(
            PacketFloat32Avx.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, matrix.Row0.X * vector.X),
            PacketFloat32Avx.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, matrix.Row1.X * vector.X));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32Avx Multiply(PacketMatrix2Float32Avx left, PacketMatrix2Float32Avx right) => left * right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32Avx Multiply(PacketMatrix2Float32Avx matrix, PacketFloat32Avx scalar) => matrix * scalar;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32Avx Multiply(PacketFloat32Avx scalar, PacketMatrix2Float32Avx matrix) => scalar * matrix;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32Avx FusedMultiplyAdd(PacketMatrix2Float32Avx left, PacketMatrix2Float32Avx right, PacketMatrix2Float32Avx addend)
    {
        return new(
            new PacketVector2Float32Avx(
                PacketFloat32Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, PacketFloat32Avx.FusedMultiplyAdd(left.Row0.X, right.Row0.X, addend.Row0.X)),
                PacketFloat32Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, PacketFloat32Avx.FusedMultiplyAdd(left.Row0.X, right.Row0.Y, addend.Row0.Y))),
            new PacketVector2Float32Avx(
                PacketFloat32Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, PacketFloat32Avx.FusedMultiplyAdd(left.Row1.X, right.Row0.X, addend.Row1.X)),
                PacketFloat32Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, PacketFloat32Avx.FusedMultiplyAdd(left.Row1.X, right.Row0.Y, addend.Row1.Y))));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Avx FusedMultiplyAdd(PacketMatrix2Float32Avx matrix, PacketVector2Float32Avx vector, PacketVector2Float32Avx addend)
    {
        return new(
            PacketFloat32Avx.FusedMultiplyAdd(matrix.Row0.Y, vector.Y, PacketFloat32Avx.FusedMultiplyAdd(matrix.Row0.X, vector.X, addend.X)),
            PacketFloat32Avx.FusedMultiplyAdd(matrix.Row1.Y, vector.Y, PacketFloat32Avx.FusedMultiplyAdd(matrix.Row1.X, vector.X, addend.Y)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32Avx Transpose(PacketMatrix2Float32Avx matrix)
    {
        return new(
            new(matrix.Row0.X, matrix.Row1.X),
            new(matrix.Row0.Y, matrix.Row1.Y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32Avx Scale(PacketVector2Float32Avx scale) => Scale(scale.X, scale.Y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32Avx Scale(PacketFloat32Avx x, PacketFloat32Avx y)
    {
        PacketFloat32Avx zero = PacketFloat32Avx.Broadcast(0f);
        return new(
            new(x, zero),
            new(zero, y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32Avx operator *(PacketMatrix2Float32Avx left, PacketMatrix2Float32Avx right)
    {
        return new(
            new PacketVector2Float32Avx(
                PacketFloat32Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.X, left.Row0.X * right.Row0.X),
                PacketFloat32Avx.FusedMultiplyAdd(left.Row0.Y, right.Row1.Y, left.Row0.X * right.Row0.Y)),
            new PacketVector2Float32Avx(
                PacketFloat32Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.X, left.Row1.X * right.Row0.X),
                PacketFloat32Avx.FusedMultiplyAdd(left.Row1.Y, right.Row1.Y, left.Row1.X * right.Row0.Y)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32Avx operator *(PacketMatrix2Float32Avx matrix, PacketVector2Float32Avx vector) => Transform(matrix, vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32Avx operator *(PacketMatrix2Float32Avx matrix, PacketFloat32Avx scalar)
    {
        PacketVector2Float32Avx scalarVector = new(scalar, scalar);
        return new(matrix.Row0 * scalarVector, matrix.Row1 * scalarVector);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32Avx operator *(PacketFloat32Avx scalar, PacketMatrix2Float32Avx matrix) => matrix * scalar;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32Avx Divide(PacketMatrix2Float32Avx matrix, PacketFloat32Avx scalar) => matrix / scalar;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32Avx operator /(PacketMatrix2Float32Avx matrix, PacketFloat32Avx scalar) => matrix * PacketFloat32Avx.Reciprocal(scalar);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32Avx Inverse(PacketMatrix2Float32Avx matrix)
    {
        PacketFloat32Avx invDet = PacketFloat32Avx.Reciprocal(PacketFloat32Avx.FusedMultiplyAdd(matrix.Row0.X, matrix.Row1.Y, -(matrix.Row0.Y * matrix.Row1.X)));
        return new(
            new(matrix.Row1.Y * invDet, -matrix.Row0.Y * invDet),
            new(-matrix.Row1.X * invDet, matrix.Row0.X * invDet));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32Avx InverseTranspose(PacketMatrix2Float32Avx matrix)
    {
        PacketFloat32Avx invDet = PacketFloat32Avx.Reciprocal(PacketFloat32Avx.FusedMultiplyAdd(matrix.Row0.X, matrix.Row1.Y, -(matrix.Row0.Y * matrix.Row1.X)));
        return new(
            new(matrix.Row1.Y * invDet, -matrix.Row1.X * invDet),
            new(-matrix.Row0.Y * invDet, matrix.Row0.X * invDet));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32Avx Rotate(PacketFloat32Avx angle)
    {
        var (sin, cos) = PacketFloat32Avx.SinCos(angle);
        return new(
            new(cos, -sin),
            new(sin, cos));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32AvxMask operator ==(PacketMatrix2Float32Avx left, PacketMatrix2Float32Avx right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32AvxMask operator !=(PacketMatrix2Float32Avx left, PacketMatrix2Float32Avx right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix2Float32Avx other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1);
}

public readonly struct PacketMatrix2Float32AvxMask :
    ISimdMatrix2Mask<PacketMatrix2Float32AvxMask, PacketVector2Float32AvxMask, PacketFloat32AvxMask>
{
    public PacketMatrix2Float32AvxMask(PacketVector2Float32AvxMask row0, PacketVector2Float32AvxMask row1)
    {
        Row0 = row0;
        Row1 = row1;
    }

    public static int LaneCount => PacketVector2Float32AvxMask.LaneCount;

    public PacketVector2Float32AvxMask Row0 { get; }

    public PacketVector2Float32AvxMask Row1 { get; }

    public static PacketMatrix2Float32AvxMask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketVector2Float32AvxMask.True, PacketVector2Float32AvxMask.True);
    }

    public static PacketMatrix2Float32AvxMask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(PacketVector2Float32AvxMask.False, PacketVector2Float32AvxMask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32AvxMask Create(PacketVector2Float32AvxMask row0, PacketVector2Float32AvxMask row1) => new(row0, row1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32AvxMask Broadcast(PacketVector2Float32AvxMask value) => new(value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32AvxMask All(PacketMatrix2Float32AvxMask value) => value.Row0 & value.Row1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32AvxMask Any(PacketMatrix2Float32AvxMask value) => value.Row0 | value.Row1;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketVector2Float32AvxMask None(PacketMatrix2Float32AvxMask value) => ~(value.Row0 | value.Row1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32AvxMask AndNot(PacketMatrix2Float32AvxMask left, PacketMatrix2Float32AvxMask right)
    {
        return new(
            PacketVector2Float32AvxMask.AndNot(left.Row0, right.Row0),
            PacketVector2Float32AvxMask.AndNot(left.Row1, right.Row1));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32AvxMask Select(PacketMatrix2Float32AvxMask mask, PacketMatrix2Float32AvxMask ifTrue, PacketMatrix2Float32AvxMask ifFalse)
    {
        return new(
            PacketVector2Float32AvxMask.Select(mask.Row0, ifTrue.Row0, ifFalse.Row0),
            PacketVector2Float32AvxMask.Select(mask.Row1, ifTrue.Row1, ifFalse.Row1));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32AvxMask And(PacketMatrix2Float32AvxMask left, PacketMatrix2Float32AvxMask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32AvxMask Or(PacketMatrix2Float32AvxMask left, PacketMatrix2Float32AvxMask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32AvxMask Xor(PacketMatrix2Float32AvxMask left, PacketMatrix2Float32AvxMask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32AvxMask Not(PacketMatrix2Float32AvxMask value) => ~value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32AvxMask operator &(PacketMatrix2Float32AvxMask left, PacketMatrix2Float32AvxMask right) => new(left.Row0 & right.Row0, left.Row1 & right.Row1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32AvxMask operator |(PacketMatrix2Float32AvxMask left, PacketMatrix2Float32AvxMask right) => new(left.Row0 | right.Row0, left.Row1 | right.Row1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32AvxMask operator ^(PacketMatrix2Float32AvxMask left, PacketMatrix2Float32AvxMask right) => new(left.Row0 ^ right.Row0, left.Row1 ^ right.Row1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32AvxMask operator ~(PacketMatrix2Float32AvxMask value) => new(~value.Row0, ~value.Row1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32AvxMask operator ==(PacketMatrix2Float32AvxMask left, PacketMatrix2Float32AvxMask right) => new(left.Row0 == right.Row0, left.Row1 == right.Row1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static PacketMatrix2Float32AvxMask operator !=(PacketMatrix2Float32AvxMask left, PacketMatrix2Float32AvxMask right) => new(left.Row0 != right.Row0, left.Row1 != right.Row1);

    public override bool Equals(object? obj)
    {
        return obj is PacketMatrix2Float32AvxMask other &&
               Row0.Equals(other.Row0) &&
               Row1.Equals(other.Row1);
    }

    public override int GetHashCode() => HashCode.Combine(Row0, Row1);
}
