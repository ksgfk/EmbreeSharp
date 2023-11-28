using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace EmbreeSharp
{
    public readonly ref struct NativeMemoryView<T> where T : unmanaged
    {
        private readonly nuint _ptr;
        private readonly nuint _length;

        public nuint Length => _length;
        public unsafe nuint ByteCount => _length * (nuint)sizeof(T);
        public bool IsEmpty => _length == 0;

        public unsafe ref T this[nuint index]
        {
            get
            {
                if (index >= _length)
                {
                    ThrowUtility.IndexOutOfRange(nameof(index));
                }
                return ref Unsafe.Add(ref Unsafe.AsRef<T>((void*)_ptr), index);
            }
        }

        public unsafe NativeMemoryView(void* ptr, nuint length)
        {
            _ptr = new(ptr);
            _length = length;
        }

        public unsafe void Clear()
        {
            NativeMemory.Clear((void*)_ptr, ByteCount);
        }

        public unsafe void CopyTo(NativeMemoryView<T> dst)
        {
            if (dst.Length < Length)
            {
                ThrowUtility.ArgumentOutOfRange(nameof(dst));
            }
            NativeMemory.Copy((void*)_ptr, (void*)dst._ptr, ByteCount);
        }

        public unsafe void CopyFrom(Span<T> src)
        {
            if (Length.ToUInt64() < (ulong)src.Length)
            {
                ThrowUtility.ArgumentOutOfRange(nameof(src));
            }
            fixed (T* srcPtr = src)
            {
                NativeMemory.Copy(srcPtr, (void*)_ptr, (nuint)src.Length * (nuint)sizeof(T));
            }
        }
    }
}
