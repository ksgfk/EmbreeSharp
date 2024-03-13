using System;
using System.Runtime.InteropServices;

namespace EmbreeSharp
{
    public class SharedBufferHandle : SafeHandle
    {
        private ISharedBufferAllocation _allocation;

        public override bool IsInvalid => _allocation == null;
        public ISharedBufferAllocation Buffer => _allocation;

        public SharedBufferHandle(ISharedBufferAllocation allocation) : base(0, true)
        {
            _allocation = allocation;
            handle = (nint)Buffer.View.UnsafePtr;
            GC.AddMemoryPressure((long)Buffer.View.ByteCount);
        }

        protected override bool ReleaseHandle()
        {
            try
            {
                var cnt = Buffer.View.ByteCount;
                var alloc = _allocation.Allocator;
                alloc.Free(_allocation);
                _allocation = null!;
                handle = 0;
                GC.RemoveMemoryPressure((long)cnt);
                return true;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("cannot release ISharedBufferAllocation, exception: {0}\n  at:\n{1}", e.Message, e.StackTrace);
                return false;
            }
        }
    }
}
