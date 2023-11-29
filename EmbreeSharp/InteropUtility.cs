using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace EmbreeSharp
{
    public static class InteropUtility
    {
        /// <summary>
        /// C-style string length
        /// </summary>
        public static unsafe nuint Strlen(byte* s)
        {
            byte* sc;
            for (sc = s; *sc != '\0'; ++sc) ;
            return new nuint((ulong)(sc - s));
        }

        /// <summary>
        /// Hepler function to alloc memory aligned on stack
        /// </summary>
        public static unsafe ref T StackAllocAligned<T>(Span<byte> stack, nuint alignment) where T : unmanaged
        {
            return ref Unsafe.AsRef<T>((void*)(((nint)Unsafe.AsPointer(ref MemoryMarshal.GetReference(stack)) + ((nint)alignment - 1)) & ~(nint)(alignment - 1)));
        }
    }
}
