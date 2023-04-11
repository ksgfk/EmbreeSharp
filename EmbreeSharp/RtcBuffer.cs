using EmbreeSharp.Native;
using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static EmbreeSharp.Native.EmbreeNative;

namespace EmbreeSharp;

public abstract class RtcBuffer : IDisposable
{
    protected RTCBuffer _buffer;
    protected bool _disposedValue;
    readonly long _length;

    public long Length => _length;

    protected RtcBuffer(long length)
    {
        if (length < 0)
        {
            ThrowArgumentOutOfRange(nameof(length));
        }
        _length = length;
    }

    ~RtcBuffer()
    {
        Dispose(disposing: false);
    }

    public IntPtr GetData()
    {
        unsafe
        {
            return new IntPtr(rtcGetBufferData(_buffer));
        }
    }

    public RtcBufferView<byte> AsView()
    {
        IntPtr data = GetData();
        unsafe
        {
            return new RtcBufferView<byte>(data.ToPointer(), Length);
        }
    }

    public RtcBufferView<T> AsView<T>() where T : struct
    {
        IntPtr data = GetData();
        unsafe
        {
            return new RtcBufferView<T>(data.ToPointer(), Length / Unsafe.SizeOf<T>());
        }
    }

    public Span<byte> AsSpan()
    {
        if (Length >= int.MaxValue)
        {
            ThrowArgumentOutOfRange(nameof(Length));
        }
        IntPtr data = GetData();
        unsafe
        {
            return new Span<byte>(data.ToPointer(), (int)Length);
        }
    }

    public Span<T> AsSpan<T>() where T : struct
    {
        return MemoryMarshal.Cast<byte, T>(AsSpan());
    }

    protected abstract void Dispose(bool disposing);

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}

public sealed class RtcUniqueBuffer : RtcBuffer
{
    public RtcUniqueBuffer(RTCDevice device, long byteSize) : base(byteSize)
    {
        _buffer = rtcNewBuffer(device, (nuint)byteSize);
    }

    public RtcUniqueBuffer(RtcDevice device, long byteSize) : this(device.NativeHandler, byteSize) { }

    protected override void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            rtcReleaseBuffer(_buffer);
            _buffer = default;

            _disposedValue = true;
        }
    }
}

public class RtcSharedBuffer : RtcBuffer
{
    //users should ensure that ptr is valid in the life time of buffer
    public RtcSharedBuffer(RTCDevice device, IntPtr ptr, long byteSize) : base(byteSize)
    {
        unsafe
        {
            _buffer = rtcNewSharedBuffer(device, ptr.ToPointer(), (nuint)byteSize);
        }
    }

    //DO NOT forget to init _buffer
    protected RtcSharedBuffer(RTCDevice device, long byteSize) : base(byteSize) { }

    public RtcSharedBuffer(RtcDevice device, IntPtr ptr, long byteSize) : this(device.NativeHandler, ptr, byteSize) { }

    protected override void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            rtcReleaseBuffer(_buffer);
            _buffer = default;

            _disposedValue = true;
        }
    }
}

public sealed class ManagedRtcSharedBuffer<T> : RtcSharedBuffer where T : struct
{
    T[] _array;
    MemoryHandle _handle;

    public ManagedRtcSharedBuffer(RTCDevice device, T[] array) : base(device, Unsafe.SizeOf<T>() * array.LongLength)
    {
        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
        {
            ThrowInvalidOperation($"{typeof(T).FullName} is reference type");
        }
        _array = array ?? throw new ArgumentNullException(nameof(array));
        _handle = _array.AsMemory().Pin();
        unsafe
        {
            _buffer = rtcNewSharedBuffer(device, _handle.Pointer, (nuint)Length);
        }
    }

    public ManagedRtcSharedBuffer(RtcDevice device, T[] array) : this(device.NativeHandler, array) { }

    public RtcBufferView<T> AsTypedView()
    {
        unsafe
        {
            return new RtcBufferView<T>((void*)GetData(), Length / Unsafe.SizeOf<T>());
        }
    }

    public Span<T> AsTypedSpan()
    {
        return _array;
    }

    protected override void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                _array = null!;
            }

            rtcReleaseBuffer(_buffer);
            _buffer = default;
            _handle.Dispose();
            _handle = default;

            _disposedValue = true;
        }
    }
}
