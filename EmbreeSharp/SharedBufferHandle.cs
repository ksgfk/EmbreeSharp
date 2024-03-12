﻿using System;
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
        }

        protected override bool ReleaseHandle()
        {
            try
            {
                var alloc = _allocation.Allocator;
                alloc.Free(_allocation);
                _allocation = null!;
                handle = 0;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
