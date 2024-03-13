using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace EmbreeSharp
{
    /// <summary>
    /// This struct only used for unmanaged memory. Please do not use it in managed memory. This is not safe
    /// This struct does not manage memory. Only user guarantees that the lifecycle of memory is longer than this view
    /// </summary>
    public readonly struct NativeMemoryView<T> where T : unmanaged
    {
        internal readonly nuint _ptr;
        private readonly nuint _length;

        public nuint Length => _length;
        public unsafe nuint ByteCount => _length * (nuint)sizeof(T);
        public bool IsEmpty => _length == 0;
        public nuint UnsafePtr => _ptr;

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

        public unsafe NativeMemoryView(ref T ptr, nuint length)
        {
            _ptr = new(Unsafe.AsPointer(ref ptr));
            _length = length;
        }

        public unsafe void Clear()
        {
            NativeMemory.Clear((void*)_ptr, ByteCount);
        }

        public unsafe void Fill(byte value)
        {
            NativeMemory.Fill(_ptr.ToPointer(), ByteCount, value);
        }

        public unsafe void CopyTo(NativeMemoryView<T> dst)
        {
            if (_length <= dst._length)
            {
                NativeMemory.Copy((void*)_ptr, (void*)dst._ptr, ByteCount);
            }
            else
            {
                ThrowUtility.ArgumentOutOfRange(nameof(dst));
            }
        }

        public unsafe bool TryCopyTo(NativeMemoryView<T> dst)
        {
            bool result = false;
            if (_length <= dst._length)
            {
                NativeMemory.Copy((void*)_ptr, (void*)dst._ptr, ByteCount);
                result = true;
            }
            return result;
        }

        public unsafe void CopyFrom(ReadOnlySpan<T> src)
        {
            if ((ulong)src.Length <= _length.ToUInt64())
            {
                fixed (T* srcPtr = src)
                {
                    NativeMemory.Copy(srcPtr, (void*)_ptr, (nuint)src.Length * (nuint)sizeof(T));
                }
            }
            else
            {
                ThrowUtility.ArgumentOutOfRange(nameof(src));
            }
        }

        public unsafe NativeMemoryView<T> Slice(nuint start)
        {
            if (start > _length)
            {
                ThrowUtility.ArgumentOutOfRange();
            }
            return new NativeMemoryView<T>(ref Unsafe.Add(ref Unsafe.AsRef<T>(_ptr.ToPointer()), start), _length - start);
        }

        public unsafe NativeMemoryView<T> Slice(nuint start, nuint length)
        {
            if ((UInt128)start + length > _length)
            {
                ThrowUtility.ArgumentOutOfRange();
            }
            return new NativeMemoryView<T>(ref Unsafe.Add(ref Unsafe.AsRef<T>(_ptr.ToPointer()), start), length);
        }

        public unsafe Span<T> AsSpan()
        {
            if (((ulong)_length) > int.MaxValue)
            {
                ThrowUtility.InvalidOperation();
            }
            return new Span<T>(_ptr.ToPointer(), (int)_length);
        }

        public unsafe Span<T> AsSpan(int start)
        {
            if (start < 0)
            {
                ThrowUtility.ArgumentOutOfRange();
            }
            if ((ulong)start > _length)
            {
                ThrowUtility.ArgumentOutOfRange();
            }
            if (_length - (ulong)start > int.MaxValue)
            {
                ThrowUtility.InvalidOperation();
            }
            return new Span<T>(Unsafe.Add<T>(_ptr.ToPointer(), start), (int)(_length - (ulong)start));
        }

        public unsafe Span<T> AsSpan(int start, int length)
        {
            if (start < 0 || length < 0)
            {
                ThrowUtility.ArgumentOutOfRange();
            }
            if ((ulong)start + (ulong)length > _length)
            {
                ThrowUtility.ArgumentOutOfRange();
            }
            return new Span<T>(Unsafe.Add<T>(_ptr.ToPointer(), start), length);
        }

        public ref struct Enumerator
        {
            private readonly nuint _end;
            private nuint _ptr;

            internal unsafe Enumerator(NativeMemoryView<T> view)
            {
                _end = (nuint)Unsafe.AsPointer(ref Unsafe.Add(ref Unsafe.AsRef<T>((void*)view._ptr), view._length));
                _ptr = (nuint)Unsafe.Add<T>((void*)view._ptr, -1);
            }

            public unsafe bool MoveNext()
            {
                nuint index = (nuint)Unsafe.Add<T>((void*)_ptr, 1);
                if (index < _end)
                {
                    _ptr = index;
                    return true;
                }
                return false;
            }

            public readonly unsafe ref T Current => ref Unsafe.AsRef<T>((void*)_ptr);
        }

        public Enumerator GetEnumerator() => new(this);
    }

    public static class NativeMemoryViewExtension
    {
        public static NativeMemoryView<TTo> Cast<TFrom, TTo>(this NativeMemoryView<TFrom> from) where TFrom : unmanaged where TTo : unmanaged
        {
            nuint fromSize = (nuint)Unsafe.SizeOf<TFrom>();
            nuint toSize = (nuint)Unsafe.SizeOf<TTo>();
            nuint fromLength = from.Length;
            nuint toLength;
            if (fromSize == toSize)
            {
                toLength = fromLength;
            }
            else if (fromSize == 1)
            {
                toLength = fromLength / toSize;
            }
            else
            {
                UInt128 toLengthUInt64 = (UInt128)fromLength * (UInt128)fromSize / (UInt128)toSize;
                toLength = checked((nuint)(ulong)toLengthUInt64);
            }
            unsafe
            {
                return new NativeMemoryView<TTo>(from._ptr.ToPointer(), toLength);
            }
        }
    }
}
