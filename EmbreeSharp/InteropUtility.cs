using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace EmbreeSharp
{
    public static class InteropUtility
    {
        public static unsafe long Strlen(byte* s)
        {
            byte* sc;
            for (sc = s; *sc != '\0'; ++sc) ;
            return sc - s;
        }

        public static unsafe ref T StackAllocAligned<T>(Span<byte> stack, nuint alignment) where T : unmanaged
        {
            return ref Unsafe.AsRef<T>((void*)(((nint)Unsafe.AsPointer(ref MemoryMarshal.GetReference(stack)) + ((nint)alignment - 1)) & ~(nint)(alignment - 1)));
        }
    }
}
