using System.Runtime.CompilerServices;
using System.Numerics;

namespace EmbreeSharp.SIMD;

public readonly struct ScalarVector4Float32 :
    ISimdFloatingPointVector4<ScalarVector4Float32, ScalarFloat32, float, ScalarVector4Float32Mask, ScalarFloat32Mask>
{
    internal readonly Vector4 _value;

    public ScalarVector4Float32(ScalarFloat32 x, ScalarFloat32 y, ScalarFloat32 z, ScalarFloat32 w) => _value = new Vector4(x._value, y._value, z._value, w._value);

    internal ScalarVector4Float32(Vector4 value) => _value = value;

    public static int LaneCount => ScalarFloat32.LaneCount;

    public ScalarFloat32 X => new(_value.X);

    public ScalarFloat32 Y => new(_value.Y);

    public ScalarFloat32 Z => new(_value.Z);

    public ScalarFloat32 W => new(_value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float32 Create(ScalarFloat32 x, ScalarFloat32 y, ScalarFloat32 z, ScalarFloat32 w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float32 Broadcast(float value) => new(new Vector4(value));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float32 Select(ScalarVector4Float32Mask mask, ScalarVector4Float32 ifTrue, ScalarVector4Float32 ifFalse)
    {
        return new(
            ScalarFloat32.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarFloat32.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarFloat32.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            ScalarFloat32.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float32 Select(ScalarFloat32Mask mask, ScalarVector4Float32 ifTrue, ScalarVector4Float32 ifFalse)
    {
        return new(
            ScalarFloat32.Select(mask, ifTrue.X, ifFalse.X),
            ScalarFloat32.Select(mask, ifTrue.Y, ifFalse.Y),
            ScalarFloat32.Select(mask, ifTrue.Z, ifFalse.Z),
            ScalarFloat32.Select(mask, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFloat32 Dot(ScalarVector4Float32 left, ScalarVector4Float32 right)
    {
        return ScalarFloat32.FusedMultiplyAdd(
            left.W,
            right.W,
            ScalarFloat32.FusedMultiplyAdd(left.Z, right.Z, ScalarFloat32.FusedMultiplyAdd(left.Y, right.Y, left.X * right.X)));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float32 operator +(ScalarVector4Float32 value) => value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float32 operator +(ScalarVector4Float32 left, ScalarVector4Float32 right) => new(left._value + right._value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float32 operator -(ScalarVector4Float32 left, ScalarVector4Float32 right) => new(left._value - right._value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float32 operator *(ScalarVector4Float32 left, ScalarVector4Float32 right) => new(left._value * right._value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float32 operator -(ScalarVector4Float32 value) => new(-value._value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float32Mask operator ==(ScalarVector4Float32 left, ScalarVector4Float32 right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float32Mask operator !=(ScalarVector4Float32 left, ScalarVector4Float32 right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj) => obj is ScalarVector4Float32 other && _value.Equals(other._value);

    public override int GetHashCode() => _value.GetHashCode();
}

public readonly struct ScalarVector4Float32Mask :
    ISimdVector4Mask<ScalarVector4Float32Mask, ScalarFloat32Mask>
{
    public ScalarVector4Float32Mask(ScalarFloat32Mask x, ScalarFloat32Mask y, ScalarFloat32Mask z, ScalarFloat32Mask w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public static int LaneCount => ScalarFloat32Mask.LaneCount;

    public ScalarFloat32Mask X { get; }

    public ScalarFloat32Mask Y { get; }

    public ScalarFloat32Mask Z { get; }

    public ScalarFloat32Mask W { get; }

    public static ScalarVector4Float32Mask True
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarFloat32Mask.True, ScalarFloat32Mask.True, ScalarFloat32Mask.True, ScalarFloat32Mask.True);
    }

    public static ScalarVector4Float32Mask False
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(ScalarFloat32Mask.False, ScalarFloat32Mask.False, ScalarFloat32Mask.False, ScalarFloat32Mask.False);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float32Mask Create(ScalarFloat32Mask x, ScalarFloat32Mask y, ScalarFloat32Mask z, ScalarFloat32Mask w) => new(x, y, z, w);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float32Mask Broadcast(ScalarFloat32Mask value) => new(value, value, value, value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFloat32Mask All(ScalarVector4Float32Mask value) => value.X & value.Y & value.Z & value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFloat32Mask Any(ScalarVector4Float32Mask value) => value.X | value.Y | value.Z | value.W;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarFloat32Mask None(ScalarVector4Float32Mask value) => ~(value.X | value.Y | value.Z | value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float32Mask AndNot(ScalarVector4Float32Mask left, ScalarVector4Float32Mask right)
    {
        return new(
            ScalarFloat32Mask.AndNot(left.X, right.X),
            ScalarFloat32Mask.AndNot(left.Y, right.Y),
            ScalarFloat32Mask.AndNot(left.Z, right.Z),
            ScalarFloat32Mask.AndNot(left.W, right.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float32Mask Select(ScalarVector4Float32Mask mask, ScalarVector4Float32Mask ifTrue, ScalarVector4Float32Mask ifFalse)
    {
        return new(
            ScalarFloat32Mask.Select(mask.X, ifTrue.X, ifFalse.X),
            ScalarFloat32Mask.Select(mask.Y, ifTrue.Y, ifFalse.Y),
            ScalarFloat32Mask.Select(mask.Z, ifTrue.Z, ifFalse.Z),
            ScalarFloat32Mask.Select(mask.W, ifTrue.W, ifFalse.W));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float32Mask And(ScalarVector4Float32Mask left, ScalarVector4Float32Mask right) => left & right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float32Mask Or(ScalarVector4Float32Mask left, ScalarVector4Float32Mask right) => left | right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float32Mask Xor(ScalarVector4Float32Mask left, ScalarVector4Float32Mask right) => left ^ right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float32Mask Not(ScalarVector4Float32Mask value) => ~value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float32Mask operator &(ScalarVector4Float32Mask left, ScalarVector4Float32Mask right) => new(left.X & right.X, left.Y & right.Y, left.Z & right.Z, left.W & right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float32Mask operator |(ScalarVector4Float32Mask left, ScalarVector4Float32Mask right) => new(left.X | right.X, left.Y | right.Y, left.Z | right.Z, left.W | right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float32Mask operator ^(ScalarVector4Float32Mask left, ScalarVector4Float32Mask right) => new(left.X ^ right.X, left.Y ^ right.Y, left.Z ^ right.Z, left.W ^ right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float32Mask operator ~(ScalarVector4Float32Mask value) => new(~value.X, ~value.Y, ~value.Z, ~value.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float32Mask operator ==(ScalarVector4Float32Mask left, ScalarVector4Float32Mask right) => new(left.X == right.X, left.Y == right.Y, left.Z == right.Z, left.W == right.W);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ScalarVector4Float32Mask operator !=(ScalarVector4Float32Mask left, ScalarVector4Float32Mask right) => new(left.X != right.X, left.Y != right.Y, left.Z != right.Z, left.W != right.W);

    public override bool Equals(object? obj) => obj is ScalarVector4Float32Mask other && X.Equals(other.X) && Y.Equals(other.Y) && Z.Equals(other.Z) && W.Equals(other.W);

    public override int GetHashCode() => HashCode.Combine(X, Y, Z, W);
}
