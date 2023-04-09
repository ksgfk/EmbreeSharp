using EmbreeSharp.Native;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static EmbreeSharp.Native.EmbreeNative;

namespace EmbreeSharp;

public class RtcBuffer : IDisposable
{
    RTCBuffer _buffer;
    readonly long _length;
    bool _disposedValue;

    public RtcBuffer(RTCDevice device, long byteSize)
    {
        if (byteSize < 0)
        {
            ThrowArgumentOutOfRange(nameof(byteSize));
        }
        _buffer = rtcNewBuffer(device, (nuint)byteSize);
        _length = byteSize;
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

    public RtcBufferView<byte> GetDataView()
    {
        IntPtr data = GetData();
        unsafe
        {
            return new RtcBufferView<byte>(data.ToPointer(), _length);
        }
    }

    public RtcBufferView<T> GetDataView<T>() where T : struct
    {
        IntPtr data = GetData();
        unsafe
        {
            return new RtcBufferView<T>(data.ToPointer(), _length / Unsafe.SizeOf<T>());
        }
    }

    public Span<byte> GetSpan()
    {
        if (_length >= int.MaxValue)
        {
            ThrowArgumentOutOfRange(nameof(_length));
        }
        IntPtr data = GetData();
        unsafe
        {
            return new Span<byte>(data.ToPointer(), (int)_length);
        }
    }

    public Span<T> GetSpan<T>() where T : struct
    {
        return MemoryMarshal.Cast<byte, T>(GetSpan());
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            rtcReleaseBuffer(_buffer);
            _buffer = default;

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
