using System.Runtime.CompilerServices;

namespace EmbreeSharp
{
    public readonly unsafe struct Ref<T> where T : unmanaged
    {
        public readonly void* Ptr;

        public ref T Value => ref Unsafe.AsRef<T>(Ptr);

        public bool IsNull => Ptr == null;

        internal Ref(void* ptr)
        {
            Ptr = ptr;
        }

        public Ref<TResult> Cast<TResult>() where TResult : unmanaged
        {
            return new Ref<TResult>(Ptr);
        }
    }
}
