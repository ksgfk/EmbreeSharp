using System;
using System.Diagnostics.CodeAnalysis;

namespace EmbreeSharp;

internal static class ExceptionUtility
{
    [DoesNotReturn]
    public static void ThrowArgumentOutOfRange(string? msg = null) => throw new ArgumentOutOfRangeException(msg);
}
