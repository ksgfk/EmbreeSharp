using EmbreeSharp.Native;
using System.Runtime.CompilerServices;

namespace EmbreeSharp
{
    public static class RTCThreadLocalAllocatorExtension
    {
        public static RTCThreadLocalAllocation Allocate(this RTCThreadLocalAllocator alloc, nuint bytes, nuint align)
        {
            unsafe
            {
                void* p = EmbreeNative.rtcThreadLocalAlloc(alloc, bytes, align);
                return new RTCThreadLocalAllocation(p);
            }
        }

        public static ref T Allocate<T>(this RTCThreadLocalAllocator alloc, nuint align) where T : unmanaged
        {
            unsafe
            {
                nuint size = (nuint)sizeof(T);
                void* p = EmbreeNative.rtcThreadLocalAlloc(alloc, size, align);
                return ref Unsafe.AsRef<T>(p);
            }
        }
    }
}
