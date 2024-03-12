using System.Runtime.InteropServices;

namespace EmbreeSharp
{
    public interface ISharedBufferAllocator
    {
        SharedBufferHandle Allocate(nuint size, nuint alignment);

        void Free(ISharedBufferAllocation allocation);
    }

    public class DefaultSharedBufferAllocator : ISharedBufferAllocator
    {
        private unsafe sealed class Allocation(DefaultSharedBufferAllocator allocator, void* ptr, nuint capacity) : ISharedBufferAllocation
        {
            private readonly DefaultSharedBufferAllocator _allocator = allocator;
            internal void* _ptr = ptr;
            private readonly nuint _capacity = capacity;
            public ISharedBufferAllocator Allocator => _allocator;
            public NativeMemoryView<byte> View => new(_ptr, _capacity);
        }

        public unsafe SharedBufferHandle Allocate(nuint size, nuint alignment)
        {
            void* ptr = NativeMemory.AlignedAlloc(size, alignment);
            Allocation alloc = new(this, ptr, alignment);
            return new SharedBufferHandle(alloc);
        }

        public unsafe void Free(ISharedBufferAllocation allocation)
        {
            Allocation alloc = (Allocation)allocation;
            if (alloc.Allocator != this)
            {
                ThrowUtility.InvalidOperation("free allocation in different allocator");
            }
            var ptr = alloc._ptr;
            NativeMemory.AlignedFree(ptr);
            alloc._ptr = null;
        }
    }
}
