namespace EmbreeSharp
{
    public interface ISharedBufferAllocation
    {
        ISharedBufferAllocator Allocator { get; }

        NativeMemoryView<byte> View { get; }
    }
}
