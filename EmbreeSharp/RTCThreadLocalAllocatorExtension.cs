using EmbreeSharp.Native;
using System.Runtime.CompilerServices;

namespace EmbreeSharp
{
    public unsafe readonly struct RtcThreadLocalAllocation
    {
        public readonly void* Ptr;

        internal RtcThreadLocalAllocation(void* ptr)
        {
            Ptr = ptr;
        }

        public readonly unsafe Ref<T> AsRef<T>(nuint index = 0) where T : unmanaged
        {
            return new Ref<T>(((T*)Ptr) + index);
        }

        public readonly ref T Ref<T>(nuint index = 0)
        {
            unsafe
            {
                return ref Unsafe.Add(ref Unsafe.AsRef<T>(Ptr), index);
            }
        }

        public readonly T Read<T>(nuint index = 0)
        {
            unsafe
            {
                return Unsafe.Add(ref Unsafe.AsRef<T>(Ptr), index);
            }
        }

        public readonly void Write<T>(T value, nuint index = 0)
        {
            unsafe
            {
                Unsafe.Add(ref Unsafe.AsRef<T>(Ptr), index) = value;
            }
        }
    }

    public static class RTCThreadLocalAllocatorExtension
    {
        public static RtcThreadLocalAllocation Allocate(this RTCThreadLocalAllocator alloc, nuint bytes, nuint align)
        {
            unsafe
            {
                void* p = EmbreeNative.rtcThreadLocalAlloc(alloc, bytes, align);
                return new RtcThreadLocalAllocation(p);
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
