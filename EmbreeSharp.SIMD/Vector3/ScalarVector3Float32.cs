using System.Runtime.CompilerServices;
using System.Numerics;

namespace EmbreeSharp.SIMD;

public readonly struct ScalarVector3Float32 :
    ISimdFloatingPointVector3<ScalarVector3Float32, ScalarFloat32, float, ScalarVector3Float32Mask, ScalarFloat32Mask>
{
    internal readonly Vector3 _value;

    public ScalarVector3Float32(ScalarFloat32 x, ScalarFloat32 y, ScalarFloat32 z) => _value = new Vector3(x._value, y._value, z._value);

    internal ScalarVector3Float32(Vector3 value) => _value = value;

    public static int LaneCount => ScalarFloat32.LaneCount;

    public ScalarFloat32 X => new(_value.X);

    public ScalarFloat32 Y => new(_value.Y);

    public ScalarFloat32 Z => new(_value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float32 Create(ScalarFloat32 x, ScalarFloat32 y, ScalarFloat32 z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float32 Broadcast(float value) => new(new Vector3(value));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float32 Select(ScalarVector3Float32Mask mask, ScalarVector3Float32 ifTrue, ScalarVector3Float32 ifFalse)
    {
        return new(
            ScalarFloat32.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarFloat32.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarFloat32.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float32 Select(ScalarFloat32Mask mask, ScalarVector3Float32 ifTrue, ScalarVector3Float32 ifFalse)
    {
        return new(
            ScalarFloat32.Select(mask, ifTrue.X, ifFalse.X),
            ScalarFloat32.Select(mask, ifTrue.Y, ifFalse.Y),
            ScalarFloat32.Select(mask, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFloat32 Dot(ScalarVector3Float32 left, ScalarVector3Float32 right)
    {
        return ScalarFloat32.FusedMultiplyAdd(
            left.Z,
            right.Z,
            ScalarFloat32.FusedMultiplyAdd(left.Y, right.Y, left.X * right.X));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float32 Cross(ScalarVector3Float32 left, ScalarVector3Float32 right) => new(Vector3.Cross(left._value, right._value));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float32 operator +(ScalarVector3Float32 value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float32 operator +(ScalarVector3Float32 left, ScalarVector3Float32 right) => new(left._value + right._value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float32 operator -(ScalarVector3Float32 left, ScalarVector3Float32 right) => new(left._value - right._value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float32 operator *(ScalarVector3Float32 left, ScalarVector3Float32 right) => new(left._value * right._value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float32 operator -(ScalarVector3Float32 value) => new(-value._value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float32Mask operator ==(ScalarVector3Float32 left, ScalarVector3Float32 right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float32Mask operator !=(ScalarVector3Float32 left, ScalarVector3Float32 right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj) => obj is ScalarVector3Float32 other && _value.Equals(other._value);

    public override int GetHashCode() => _value.GetHashCode();
}

public readonly struct ScalarVector3Float32Mask :
    ISimdVector3Mask<ScalarVector3Float32Mask, ScalarFloat32Mask>
{
    public ScalarVector3Float32Mask(ScalarFloat32Mask x, ScalarFloat32Mask y, ScalarFloat32Mask z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static int LaneCount => ScalarFloat32Mask.LaneCount;

    public ScalarFloat32Mask X { get; }

    public ScalarFloat32Mask Y { get; }

    public ScalarFloat32Mask Z { get; }

    public static ScalarVector3Float32Mask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarFloat32Mask.True, ScalarFloat32Mask.True, ScalarFloat32Mask.True);
    }

    public static ScalarVector3Float32Mask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarFloat32Mask.False, ScalarFloat32Mask.False, ScalarFloat32Mask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float32Mask Create(ScalarFloat32Mask x, ScalarFloat32Mask y, ScalarFloat32Mask z) => new(x, y, z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float32Mask Broadcast(ScalarFloat32Mask value) => new(value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFloat32Mask All(ScalarVector3Float32Mask value) => value.X & value.Y & value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFloat32Mask Any(ScalarVector3Float32Mask value) => value.X | value.Y | value.Z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFloat32Mask None(ScalarVector3Float32Mask value) => ~(value.X | value.Y | value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float32Mask AndNot(ScalarVector3Float32Mask left, ScalarVector3Float32Mask right)
    {
        return new(
            ScalarFloat32Mask.AndNot(left.X, right.X),
            ScalarFloat32Mask.AndNot(left.Y, right.Y),
            ScalarFloat32Mask.AndNot(left.Z, right.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float32Mask Select(ScalarVector3Float32Mask mask, ScalarVector3Float32Mask ifTrue, ScalarVector3Float32Mask ifFalse)
    {
        return new(
            ScalarFloat32Mask.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarFloat32Mask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarFloat32Mask.Select(mask.Z, ifTrue.Z, ifFalse.Z));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float32Mask And(ScalarVector3Float32Mask left, ScalarVector3Float32Mask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float32Mask Or(ScalarVector3Float32Mask left, ScalarVector3Float32Mask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float32Mask Xor(ScalarVector3Float32Mask left, ScalarVector3Float32Mask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float32Mask Not(ScalarVector3Float32Mask value) => ~value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float32Mask operator &(ScalarVector3Float32Mask left, ScalarVector3Float32Mask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float32Mask operator |(ScalarVector3Float32Mask left, ScalarVector3Float32Mask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float32Mask operator ^(ScalarVector3Float32Mask left, ScalarVector3Float32Mask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float32Mask operator ~(ScalarVector3Float32Mask value) => new(~value.X, ~value.Y, ~value.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float32Mask operator ==(ScalarVector3Float32Mask left, ScalarVector3Float32Mask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector3Float32Mask operator !=(ScalarVector3Float32Mask left, ScalarVector3Float32Mask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z);

    public override bool Equals(object? obj) => obj is ScalarVector3Float32Mask other && X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z);

    public override int GetHashCode() => HashCode.Combine(X, Y, Z);
}
