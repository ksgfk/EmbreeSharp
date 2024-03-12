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

        public static RTCThreadLocalAllocation NullAllocate(this RTCThreadLocalAllocator alloc)
        {
            unsafe
            {
                return new RTCThreadLocalAllocation(null);
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

        public static ref T Allocate<T>(this RTCThreadLocalAllocator alloc, nuint count, nuint align) where T : unmanaged
        {
            unsafe
            {
                nuint size = (nuint)sizeof(T);
                void* p = EmbreeNative.rtcThreadLocalAlloc(alloc, count * size, align);
                return ref Unsafe.AsRef<T>(p);
            }
        }

        public static ref T NullRef<T>(this RTCThreadLocalAllocator alloc) where T : unmanaged
        {
            return ref Unsafe.NullRef<T>();
        }
    }
}
