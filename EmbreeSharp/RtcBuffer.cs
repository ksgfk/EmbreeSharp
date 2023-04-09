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

    public abstract long Length { get; }

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
            return new RtcBufferView<byte>(data.ToPointer(), Length);
        }
    }

    public RtcBufferView<T> GetDataView<T>() where T : struct
    {
        IntPtr data = GetData();
        unsafe
        {
            return new RtcBufferView<T>(data.ToPointer(), Length / Unsafe.SizeOf<T>());
        }
    }

    public Span<byte> GetSpan()
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

    public Span<T> GetSpan<T>() where T : struct
    {
        return MemoryMarshal.Cast<byte, T>(GetSpan());
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
    long _length;

    public override long Length => _length;

    public RtcUniqueBuffer(RTCDevice device, long byteSize)
    {
        if (byteSize < 0)
        {
            ThrowArgumentOutOfRange(nameof(byteSize));
        }
        _buffer = rtcNewBuffer(device, (nuint)byteSize);
        _length = byteSize;
    }

    public RtcUniqueBuffer(RtcDevice device, long byteSize) : this(device.NativeHandler, byteSize) { }

    protected override void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            rtcReleaseBuffer(_buffer);
            _buffer = default;
            _length = 0;

            _disposedValue = true;
        }
    }
}

//public interface ISharedMemory : IDisposable
//{
//    IntPtr Ptr { get; }
//    long Length { get; }
//}

/*
 * TODO: ref count? or managed by GC... or, only support managed memory.
 * I believe that almost no one will uses native memory in C#. :)
 * So, if someone want use native memory. The best way is: user make sure that that memory is accessible during the life time of RTCBuffer.
 */
public sealed class RtcSharedBuffer : RtcBuffer
{
    //ISharedMemory _memory;

    //public override long Length => _memory.Length;

    //public RtcSharedBuffer(RTCDevice device, ISharedMemory memory)
    //{
    //    _memory = memory ?? throw new ArgumentNullException(nameof(memory));
    //    unsafe
    //    {
    //        _buffer = rtcNewSharedBuffer(device, memory.Ptr.ToPointer(), (nuint)memory.Length);
    //    }
    //}

    //public RtcSharedBuffer(RtcDevice device, ISharedMemory memory) : this(device.NativeHandler, memory) { }

    //protected override void Dispose(bool disposing)
    //{
    //    if (!_disposedValue)
    //    {
    //        if (disposing)
    //        {
    //            _memory = null!;
    //        }

    //        rtcReleaseBuffer(_buffer);
    //        _buffer = default;

    //        _disposedValue = true;
    //    }
    //}
    public override long Length => throw new NotImplementedException();

    protected override void Dispose(bool disposing)
    {
        throw new NotImplementedException();
    }
}
