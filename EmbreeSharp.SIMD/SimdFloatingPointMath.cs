using System.Runtime.CompilerServices;

namespace EmbreeSharp.SIMD;

internal static class SimdFloatingPointMath
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TPacket SinFloat<TPacket, TMask>(TPacket value)
        where TPacket : unmanaged, ISimdFloatingPoint<TPacket, float, TMask>
        where TMask : unmanaged, ISimdPacketMask<TMask, float>
    {
        TPacket zero = TPacket.Broadcast(0f);
        TPacket one = TPacket.Broadcast(1f);
        TPacket two = TPacket.Broadcast(2f);
        TPacket four = TPacket.Broadcast(4f);
        TPacket eight = TPacket.Broadcast(8f);
        TPacket xa = TPacket.Abs(value);

        TPacket j = TPacket.Floor((xa * TPacket.Broadcast(1.2732395447351626862f) + one) * TPacket.Broadcast(0.5f)) * two;
        TPacket y = xa - j * TPacket.Broadcast(0.78515625f)
                       - j * TPacket.Broadcast(2.4187564849853515625e-4f)
                       - j * TPacket.Broadcast(3.77489497744594108e-8f);
        TPacket z = y * y;

        TPacket sinApprox = Horner3Float<TPacket, TMask>(
            z,
            -1.6666654611e-1f,
            8.3321608736e-3f,
            -1.9515295891e-4f) * z;

        TPacket cosApprox = Horner3Float<TPacket, TMask>(
            z,
            4.166664568298827e-2f,
            -1.388731625493765e-3f,
            2.443315711809948e-5f) * z;

        sinApprox = TPacket.FusedMultiplyAdd(sinApprox, y, y);
        cosApprox = TPacket.FusedMultiplyAdd(cosApprox, z, TPacket.FusedMultiplyAdd(z, TPacket.Broadcast(-0.5f), one));

        TPacket jMod4 = j - TPacket.Floor(j * TPacket.Broadcast(0.25f)) * four;
        TMask polynomialMask = jMod4 < two;
        TPacket result = TPacket.Select(polynomialMask, sinApprox, cosApprox);

        TPacket jMod8 = j - TPacket.Floor(j * TPacket.Broadcast(0.125f)) * eight;
        TMask negative = (jMod8 >= four) ^ (value < zero);
        return TPacket.Select(negative, -result, result);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TPacket CosFloat<TPacket, TMask>(TPacket value)
        where TPacket : unmanaged, ISimdFloatingPoint<TPacket, float, TMask>
        where TMask : unmanaged, ISimdPacketMask<TMask, float>
    {
        TPacket one = TPacket.Broadcast(1f);
        TPacket two = TPacket.Broadcast(2f);
        TPacket four = TPacket.Broadcast(4f);
        TPacket eight = TPacket.Broadcast(8f);
        TPacket xa = TPacket.Abs(value);

        TPacket j = TPacket.Floor((xa * TPacket.Broadcast(1.2732395447351626862f) + one) * TPacket.Broadcast(0.5f)) * two;
        TPacket y = xa - j * TPacket.Broadcast(0.78515625f)
                       - j * TPacket.Broadcast(2.4187564849853515625e-4f)
                       - j * TPacket.Broadcast(3.77489497744594108e-8f);
        TPacket z = y * y;

        TPacket sinApprox = Horner3Float<TPacket, TMask>(
            z,
            -1.6666654611e-1f,
            8.3321608736e-3f,
            -1.9515295891e-4f) * z;

        TPacket cosApprox = Horner3Float<TPacket, TMask>(
            z,
            4.166664568298827e-2f,
            -1.388731625493765e-3f,
            2.443315711809948e-5f) * z;

        sinApprox = TPacket.FusedMultiplyAdd(sinApprox, y, y);
        cosApprox = TPacket.FusedMultiplyAdd(cosApprox, z, TPacket.FusedMultiplyAdd(z, TPacket.Broadcast(-0.5f), one));

        TPacket jMod4 = j - TPacket.Floor(j * TPacket.Broadcast(0.25f)) * four;
        TMask polynomialMask = jMod4 < two;
        TPacket result = TPacket.Select(polynomialMask, cosApprox, sinApprox);

        TPacket jMinus2 = j - two;
        TPacket jMinus2Mod8 = jMinus2 - TPacket.Floor(jMinus2 * TPacket.Broadcast(0.125f)) * eight;
        TMask negative = jMinus2Mod8 < four;
        return TPacket.Select(negative, -result, result);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (TPacket Sin, TPacket Cos) SinCosFloat<TPacket, TMask>(TPacket value)
        where TPacket : unmanaged, ISimdFloatingPoint<TPacket, float, TMask>
        where TMask : unmanaged, ISimdPacketMask<TMask, float>
    {
        TPacket zero = TPacket.Broadcast(0f);
        TPacket one = TPacket.Broadcast(1f);
        TPacket two = TPacket.Broadcast(2f);
        TPacket four = TPacket.Broadcast(4f);
        TPacket eight = TPacket.Broadcast(8f);
        TPacket xa = TPacket.Abs(value);

        TPacket j = TPacket.Floor((xa * TPacket.Broadcast(1.2732395447351626862f) + one) * TPacket.Broadcast(0.5f)) * two;
        TPacket y = xa - j * TPacket.Broadcast(0.78515625f)
                       - j * TPacket.Broadcast(2.4187564849853515625e-4f)
                       - j * TPacket.Broadcast(3.77489497744594108e-8f);
        TPacket z = y * y;

        TPacket sinApprox = Horner3Float<TPacket, TMask>(
            z,
            -1.6666654611e-1f,
            8.3321608736e-3f,
            -1.9515295891e-4f) * z;

        TPacket cosApprox = Horner3Float<TPacket, TMask>(
            z,
            4.166664568298827e-2f,
            -1.388731625493765e-3f,
            2.443315711809948e-5f) * z;

        sinApprox = TPacket.FusedMultiplyAdd(sinApprox, y, y);
        cosApprox = TPacket.FusedMultiplyAdd(cosApprox, z, TPacket.FusedMultiplyAdd(z, TPacket.Broadcast(-0.5f), one));

        TPacket jMod4 = j - TPacket.Floor(j * TPacket.Broadcast(0.25f)) * four;
        TMask polynomialMask = jMod4 < two;

        TPacket sinValue = TPacket.Select(polynomialMask, sinApprox, cosApprox);
        TPacket cosValue = TPacket.Select(polynomialMask, cosApprox, sinApprox);

        TPacket jMod8 = j - TPacket.Floor(j * TPacket.Broadcast(0.125f)) * eight;
        TPacket jMinus2 = j - two;
        TPacket jMinus2Mod8 = jMinus2 - TPacket.Floor(jMinus2 * TPacket.Broadcast(0.125f)) * eight;

        TMask sinNegative = (jMod8 >= four) ^ (value < zero);
        TMask cosNegative = jMinus2Mod8 < four;

        sinValue = TPacket.Select(sinNegative, -sinValue, sinValue);
        cosValue = TPacket.Select(cosNegative, -cosValue, cosValue);

        return (sinValue, cosValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TPacket SinDouble<TPacket, TMask>(TPacket value)
        where TPacket : unmanaged, ISimdFloatingPoint<TPacket, double, TMask>
        where TMask : unmanaged, ISimdPacketMask<TMask, double>
    {
        TPacket zero = TPacket.Broadcast(0d);
        TPacket one = TPacket.Broadcast(1d);
        TPacket two = TPacket.Broadcast(2d);
        TPacket four = TPacket.Broadcast(4d);
        TPacket eight = TPacket.Broadcast(8d);
        TPacket xa = TPacket.Abs(value);

        TPacket j = TPacket.Floor((xa * TPacket.Broadcast(1.2732395447351626862d) + one) * TPacket.Broadcast(0.5d)) * two;
        TPacket y = xa - j * TPacket.Broadcast(7.85398125648498535156e-1d)
                       - j * TPacket.Broadcast(3.77489470793079817668e-8d)
                       - j * TPacket.Broadcast(2.69515142907905952645e-15d);
        TPacket z = y * y;

        TPacket sinApprox = Horner6Double<TPacket, TMask>(
            z,
            -1.66666666666666307295e-1d,
            8.33333333332211858878e-3d,
            -1.98412698295895385996e-4d,
            2.75573136213857245213e-6d,
            -2.50507477628578072866e-8d,
            1.58962301576546568060e-10d) * z;

        TPacket cosApprox = Horner6Double<TPacket, TMask>(
            z,
            4.16666666666665929218e-2d,
            -1.38888888888730564116e-3d,
            2.48015872888517045348e-5d,
            -2.75573141792967388112e-7d,
            2.08757008419747316778e-9d,
            -1.13585365213876817300e-11d) * z;

        sinApprox = TPacket.FusedMultiplyAdd(sinApprox, y, y);
        cosApprox = TPacket.FusedMultiplyAdd(cosApprox, z, TPacket.FusedMultiplyAdd(z, TPacket.Broadcast(-0.5d), one));

        TPacket jMod4 = j - TPacket.Floor(j * TPacket.Broadcast(0.25d)) * four;
        TMask polynomialMask = jMod4 < two;
        TPacket result = TPacket.Select(polynomialMask, sinApprox, cosApprox);

        TPacket jMod8 = j - TPacket.Floor(j * TPacket.Broadcast(0.125d)) * eight;
        TMask negative = (jMod8 >= four) ^ (value < zero);
        return TPacket.Select(negative, -result, result);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TPacket CosDouble<TPacket, TMask>(TPacket value)
        where TPacket : unmanaged, ISimdFloatingPoint<TPacket, double, TMask>
        where TMask : unmanaged, ISimdPacketMask<TMask, double>
    {
        TPacket one = TPacket.Broadcast(1d);
        TPacket two = TPacket.Broadcast(2d);
        TPacket four = TPacket.Broadcast(4d);
        TPacket eight = TPacket.Broadcast(8d);
        TPacket xa = TPacket.Abs(value);

        TPacket j = TPacket.Floor((xa * TPacket.Broadcast(1.2732395447351626862d) + one) * TPacket.Broadcast(0.5d)) * two;
        TPacket y = xa - j * TPacket.Broadcast(7.85398125648498535156e-1d)
                       - j * TPacket.Broadcast(3.77489470793079817668e-8d)
                       - j * TPacket.Broadcast(2.69515142907905952645e-15d);
        TPacket z = y * y;

        TPacket sinApprox = Horner6Double<TPacket, TMask>(
            z,
            -1.66666666666666307295e-1d,
            8.33333333332211858878e-3d,
            -1.98412698295895385996e-4d,
            2.75573136213857245213e-6d,
            -2.50507477628578072866e-8d,
            1.58962301576546568060e-10d) * z;

        TPacket cosApprox = Horner6Double<TPacket, TMask>(
            z,
            4.16666666666665929218e-2d,
            -1.38888888888730564116e-3d,
            2.48015872888517045348e-5d,
            -2.75573141792967388112e-7d,
            2.08757008419747316778e-9d,
            -1.13585365213876817300e-11d) * z;

        sinApprox = TPacket.FusedMultiplyAdd(sinApprox, y, y);
        cosApprox = TPacket.FusedMultiplyAdd(cosApprox, z, TPacket.FusedMultiplyAdd(z, TPacket.Broadcast(-0.5d), one));

        TPacket jMod4 = j - TPacket.Floor(j * TPacket.Broadcast(0.25d)) * four;
        TMask polynomialMask = jMod4 < two;
        TPacket result = TPacket.Select(polynomialMask, cosApprox, sinApprox);

        TPacket jMinus2 = j - two;
        TPacket jMinus2Mod8 = jMinus2 - TPacket.Floor(jMinus2 * TPacket.Broadcast(0.125d)) * eight;
        TMask negative = jMinus2Mod8 < four;
        return TPacket.Select(negative, -result, result);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static (TPacket Sin, TPacket Cos) SinCosDouble<TPacket, TMask>(TPacket value)
        where TPacket : unmanaged, ISimdFloatingPoint<TPacket, double, TMask>
        where TMask : unmanaged, ISimdPacketMask<TMask, double>
    {
        TPacket zero = TPacket.Broadcast(0d);
        TPacket one = TPacket.Broadcast(1d);
        TPacket two = TPacket.Broadcast(2d);
        TPacket four = TPacket.Broadcast(4d);
        TPacket eight = TPacket.Broadcast(8d);
        TPacket xa = TPacket.Abs(value);

        TPacket j = TPacket.Floor((xa * TPacket.Broadcast(1.2732395447351626862d) + one) * TPacket.Broadcast(0.5d)) * two;
        TPacket y = xa - j * TPacket.Broadcast(7.85398125648498535156e-1d)
                       - j * TPacket.Broadcast(3.77489470793079817668e-8d)
                       - j * TPacket.Broadcast(2.69515142907905952645e-15d);
        TPacket z = y * y;

        TPacket sinApprox = Horner6Double<TPacket, TMask>(
            z,
            -1.66666666666666307295e-1d,
            8.33333333332211858878e-3d,
            -1.98412698295895385996e-4d,
            2.75573136213857245213e-6d,
            -2.50507477628578072866e-8d,
            1.58962301576546568060e-10d) * z;

        TPacket cosApprox = Horner6Double<TPacket, TMask>(
            z,
            4.16666666666665929218e-2d,
            -1.38888888888730564116e-3d,
            2.48015872888517045348e-5d,
            -2.75573141792967388112e-7d,
            2.08757008419747316778e-9d,
            -1.13585365213876817300e-11d) * z;

        sinApprox = TPacket.FusedMultiplyAdd(sinApprox, y, y);
        cosApprox = TPacket.FusedMultiplyAdd(cosApprox, z, TPacket.FusedMultiplyAdd(z, TPacket.Broadcast(-0.5d), one));

        TPacket jMod4 = j - TPacket.Floor(j * TPacket.Broadcast(0.25d)) * four;
        TMask polynomialMask = jMod4 < two;

        TPacket sinValue = TPacket.Select(polynomialMask, sinApprox, cosApprox);
        TPacket cosValue = TPacket.Select(polynomialMask, cosApprox, sinApprox);

        TPacket jMod8 = j - TPacket.Floor(j * TPacket.Broadcast(0.125d)) * eight;
        TPacket jMinus2 = j - two;
        TPacket jMinus2Mod8 = jMinus2 - TPacket.Floor(jMinus2 * TPacket.Broadcast(0.125d)) * eight;

        TMask sinNegative = (jMod8 >= four) ^ (value < zero);
        TMask cosNegative = jMinus2Mod8 < four;

        sinValue = TPacket.Select(sinNegative, -sinValue, sinValue);
        cosValue = TPacket.Select(cosNegative, -cosValue, cosValue);

        return (sinValue, cosValue);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static TPacket Horner3Float<TPacket, TMask>(TPacket x, float c0, float c1, float c2)
        where TPacket : unmanaged, ISimdFloatingPoint<TPacket, float, TMask>
        where TMask : unmanaged, ISimdPacketMask<TMask, float>
    {
        return TPacket.FusedMultiplyAdd(
            TPacket.FusedMultiplyAdd(TPacket.Broadcast(c2), x, TPacket.Broadcast(c1)),
            x,
            TPacket.Broadcast(c0));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static TPacket Horner6Double<TPacket, TMask>(TPacket x, double c0, double c1, double c2, double c3, double c4, double c5)
        where TPacket : unmanaged, ISimdFloatingPoint<TPacket, double, TMask>
        where TMask : unmanaged, ISimdPacketMask<TMask, double>
    {
        return TPacket.FusedMultiplyAdd(
            TPacket.FusedMultiplyAdd(
                TPacket.FusedMultiplyAdd(
                    TPacket.FusedMultiplyAdd(
                        TPacket.FusedMultiplyAdd(TPacket.Broadcast(c5), x, TPacket.Broadcast(c4)),
                        x,
                        TPacket.Broadcast(c3)),
                    x,
                    TPacket.Broadcast(c2)),
                x,
                TPacket.Broadcast(c1)),
            x,
            TPacket.Broadcast(c0));
    }
}
