using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace EmbreeSharp
{
    [StackTraceHidden]
    internal static class ThrowUtility
    {
        [DoesNotReturn]
        public static void ArgumentOutOfRange(string? msg = null) => throw new ArgumentOutOfRangeException(msg);

        [DoesNotReturn]
        public static void IndexOutOfRange(string? msg = null) => throw new IndexOutOfRangeException(msg);

        [DoesNotReturn]
        public static void InvalidOperation(string? msg = null) => throw new InvalidOperationException(msg);

        [DoesNotReturn]
        public static void ObjectDisposed(string? msg = null) => throw new ObjectDisposedException(msg);
    }
}
