using EmbreeSharp.Native;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static EmbreeSharp.Native.EmbreeNative;

namespace EmbreeSharp;

public class RtcGeometry : IDisposable
{
    RTCGeometry _geometry;
    bool _disposedValue;

    public RTCGeometry NativeHandler => _geometry;

    public RtcGeometry(RTCDevice device, RTCGeometryType type)
    {
        _geometry = rtcNewGeometry(device, type);
    }

    public RtcGeometry(RtcDevice device, RTCGeometryType type) : this(device.NativeHandler, type) { }

    public Span<byte> SetNewBuffer(RTCBufferType type, uint slot, RTCFormat format, int byteStride, int itemCount)
    {
        if (byteStride < 0) { ThrowArgumentOutOfRange(nameof(byteStride)); }
        if (itemCount < 0) { ThrowArgumentOutOfRange(nameof(itemCount)); }
        unsafe
        {
            void* data = rtcSetNewGeometryBuffer(_geometry, type, slot, format, (nuint)byteStride, (nuint)itemCount);
            return new Span<byte>(data, byteStride * itemCount);
        }
    }

    public Span<T> SetNewBuffer<T>(RTCBufferType type, uint slot, RTCFormat format, int itemCount) where T : struct
    {
        Span<byte> data = SetNewBuffer(type, slot, format, Unsafe.SizeOf<T>(), itemCount);
        return MemoryMarshal.Cast<byte, T>(data);
    }

    public void Commit()
    {
        rtcCommitGeometry(_geometry);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // 释放托管状态(托管对象)
            }

            rtcReleaseGeometry(_geometry);
            _geometry = default;

            _disposedValue = true;
        }
    }

    ~RtcGeometry()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
