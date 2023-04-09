using System;
using System.Runtime.CompilerServices;

namespace EmbreeSharp;

#pragma warning disable CS8500

public readonly unsafe ref struct RtcBufferView<T>
{
    readonly T* _ptr;
    readonly long _length;

    public static RtcBufferView<T> Empty => new(null, 0);

    public bool IsEmpty => _length == 0;
    public long Length => _length;
    public ref T this[long i]
    {
        get
        {
            if (i >= _length) ThrowArgumentOutOfRange(nameof(i));
            return ref _ptr[i];
        }
    }
    public IntPtr NativePtr => new(_ptr);

    public RtcBufferView()
    {
        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
        {
            ThrowInvalidOperation($"{typeof(T).FullName} is reference type");
        }
    }

    internal RtcBufferView(void* ptr, long length)
    {
        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
        {
            ThrowInvalidOperation($"{typeof(T).FullName} is reference type");
        }
        if (length < 0)
        {
            ThrowArgumentOutOfRange(nameof(length));
        }
        _ptr = (T*)ptr;
        _length = length;
    }

    public RtcBufferView(ref T ptr, long length) : this(Unsafe.AsPointer<T>(ref ptr), length) { }

    public void Clear()
    {
        for (long i = 0; i < Length; i++)
        {
            _ptr[i] = default;
        }
    }

    public void CopyTo(RtcBufferView<T> destination)
    {
        Buffer.MemoryCopy(_ptr, destination._ptr, sizeof(T) * destination.Length, sizeof(T) * _length);
    }

    public void CopyTo(Span<T> destination)
    {
        fixed (T* dst = destination)
        {
            Buffer.MemoryCopy(_ptr, dst, sizeof(T) * (long)destination.Length, sizeof(T) * _length);
        }
    }

    public void Fill(T value)
    {
        for (long i = 0; i < Length; i++)
        {
            _ptr[i] = value;
        }
    }

    public RtcBufferView<T> Slice(long start)
    {
        if (start < 0 || start >= Length)
        {
            ThrowArgumentOutOfRange(nameof(start));
        }
        return new RtcBufferView<T>(_ptr + start, _length - start);
    }

    public RtcBufferView<T> Slice(long start, long length)
    {
        if (start < 0 || start + length >= Length)
        {
            ThrowArgumentOutOfRange(nameof(start));
        }
        return new RtcBufferView<T>(_ptr + start, length);
    }

    public Span<T> Slice(int start, int length)
    {
        if (start < 0 || (long)start + length >= Length)
        {
            ThrowArgumentOutOfRange(nameof(start));
        }
        return new Span<T>(_ptr + start, length);
    }

    public Span<T> AsSpan()
    {
        if (Length >= int.MaxValue)
        {
            ThrowArgumentOutOfRange(nameof(Length));
        }
        return new Span<T>(_ptr, (int)Length);
    }

    public Enumerator GetEnumerator()
    {
        return new Enumerator(this);
    }

    public ref struct Enumerator
    {
        readonly T* _end;
        T* _current;

        public ref T Current
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => ref *_current;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal Enumerator(RtcBufferView<T> span)
        {
            _end = span._ptr + span._length;
            _current = span._ptr - 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool MoveNext()
        {
            T* next = _current + 1;
            if (next != _end)
            {
                _current = next;
                return true;
            }
            return false;
        }
    }
}

#pragma warning restore CS8500
