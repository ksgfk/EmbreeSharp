using System.Runtime.CompilerServices;

namespace EmbreeSharp
{
    public readonly unsafe struct Ref<T> where T : unmanaged
    {
        public readonly void* Ptr;

        public ref T Value => ref Unsafe.AsRef<T>(Ptr);

        internal Ref(void* ptr)
        {
            Ptr = ptr;
        }
    }
}
