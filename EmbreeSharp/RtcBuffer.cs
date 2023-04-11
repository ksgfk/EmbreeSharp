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
    public RTCBuffer NativeHandler => _buffer;

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

    public RtcBufferView<T> AsView<T>() where T : unmanaged
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

    public Span<T> AsSpan<T>() where T : unmanaged
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

// managed by embree. concept similar unique_ptr in C++
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

//user should ensure that ptr is valid in the life time of buffer
public class RtcSharedBuffer : RtcBuffer
{
    public RtcSharedBuffer(RTCDevice device, IntPtr ptr, long byteSize) : base(byteSize)
    {
        unsafe
        {
            _buffer = rtcNewSharedBuffer(device, ptr.ToPointer(), (nuint)byteSize);
        }
    }

    public RtcSharedBuffer(RtcDevice device, IntPtr ptr, long byteSize) : this(device.NativeHandler, ptr, byteSize) { }

    //DO NOT forget to init _buffer
    protected RtcSharedBuffer(long byteSize) : base(byteSize) { }

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

// C# array as buffer
public sealed class ManagedRtcSharedBuffer<T> : RtcSharedBuffer where T : unmanaged
{
    T[] _array;
    MemoryHandle _handle;

    public ManagedRtcSharedBuffer(RTCDevice device, T[] array) : base(Unsafe.SizeOf<T>() * array.LongLength)
    {
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
