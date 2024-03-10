using System.Runtime.CompilerServices;

namespace EmbreeSharp
{
    public unsafe readonly struct RTCThreadLocalAllocation
    {
        public readonly void* Ptr;

        internal RTCThreadLocalAllocation(void* ptr)
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
}
