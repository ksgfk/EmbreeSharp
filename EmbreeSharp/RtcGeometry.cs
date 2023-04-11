using EmbreeSharp.Native;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static EmbreeSharp.Native.EmbreeNative;

namespace EmbreeSharp;

//TODO: traceing buffer slot info? to avoid out of bounds
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

    ~RtcGeometry()
    {
        Dispose(disposing: false);
    }

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

    public Span<T> SetNewBuffer<T>(RTCBufferType type, uint slot, RTCFormat format, int itemCount) where T : unmanaged
    {
        Span<byte> data = SetNewBuffer(type, slot, format, Unsafe.SizeOf<T>(), itemCount);
        return MemoryMarshal.Cast<byte, T>(data);
    }

    public void Commit()
    {
        rtcCommitGeometry(_geometry);
    }

    public void Enable()
    {
        rtcEnableGeometry(_geometry);
    }

    public void Disable()
    {
        rtcDisableGeometry(_geometry);
    }

    public void SetTimestepCount(uint timeStepCount)
    {
        rtcSetGeometryTimeStepCount(_geometry, timeStepCount);
    }

    public void SetTimeRange(float startTime, float endTime)
    {
        rtcSetGeometryTimeRange(_geometry, startTime, endTime);
    }

    public void SetVertexAttributeCount(uint vertexAttributeCount)
    {
        rtcSetGeometryVertexAttributeCount(_geometry, vertexAttributeCount);
    }

    public void SetMask(uint mask)
    {
        rtcSetGeometryMask(_geometry, mask);
    }

    public void SetBuildQuality(RTCBuildQuality quality)
    {
        rtcSetGeometryBuildQuality(_geometry, quality);
    }

    public void SetMaxRadiusScale(float maxRadiusScale)
    {
        rtcSetGeometryMaxRadiusScale(_geometry, maxRadiusScale);
    }

    public void SetBuffer(RTCBufferType type, uint slot, RTCFormat format, RTCBuffer buffer, long byteOffset, long byteStride, long itemCount)
    {
        rtcSetGeometryBuffer(_geometry, type, slot, format, buffer, (nuint)byteOffset, (nuint)byteStride, (nuint)itemCount);
    }

    public void SetBuffer(RTCBufferType type, uint slot, RTCFormat format, RtcBuffer buffer, long byteOffset, long byteStride, long itemCount)
    {
        SetBuffer(type, slot, format, buffer.NativeHandler, byteOffset, byteStride, itemCount);
    }

    public void SetSharedBuffer(RTCBufferType type, uint slot, RTCFormat format, IntPtr ptr, long byteOffset, long byteStride, long itemCount)
    {
        unsafe
        {
            rtcSetSharedGeometryBuffer(_geometry, type, slot, format, ptr.ToPointer(), (nuint)byteOffset, (nuint)byteStride, (nuint)itemCount);
        }
    }

    public IntPtr GetBufferData(RTCBufferType type, uint slot)
    {
        unsafe
        {
            void* data = rtcGetGeometryBufferData(_geometry, type, slot);
            return new IntPtr(data);
        }
    }

    public void UpdateBuffer(RTCBufferType type, uint slot)
    {
        rtcUpdateGeometryBuffer(_geometry, type, slot);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            rtcReleaseGeometry(_geometry);
            _geometry = default;

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
