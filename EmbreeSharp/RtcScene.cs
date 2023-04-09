using EmbreeSharp.Native;
using System;
using static EmbreeSharp.Native.EmbreeNative;

namespace EmbreeSharp;

public class RtcScene : IDisposable
{
    RTCScene _scene;
    bool _disposedValue;

    public RTCScene NativeHandler => _scene;

    public RtcScene(RTCDevice device)
    {
        _scene = rtcNewScene(device);
    }

    public RtcScene(RtcDevice device) : this(device.NativeHandler) { }

    public uint AttachGeometry(RTCGeometry geometry)
    {
        return rtcAttachGeometry(_scene, geometry);
    }

    public uint AttachGeometry(RtcGeometry geometry)
    {
        return AttachGeometry(geometry.NativeHandler);
    }

    public void AttachGeometry(RTCGeometry geometry, uint id)
    {
        rtcAttachGeometryByID(_scene, geometry, id);
    }

    public void AttachGeometry(RtcGeometry geometry, uint id)
    {
        AttachGeometry(geometry.NativeHandler, id);
    }

    public void Commit()
    {
        rtcCommitScene(_scene);
    }

    public void JoinCommit()
    {
        rtcJoinCommitScene(_scene);
    }

    public void Intersect(in RTCRayHit rayhit)
    {
        unsafe
        {
            fixed (RTCRayHit* ptr = &rayhit)
            {
                rtcIntersect1(_scene, ptr, null);
            }
        }
    }

    public void Intersect(in RTCRayHit rayhit, in RTCIntersectArguments args)
    {
        unsafe
        {
            fixed (RTCRayHit* rayPtr = &rayhit)
            {
                fixed (RTCIntersectArguments* argsPtr = &args)
                {
                    rtcIntersect1(_scene, rayPtr, argsPtr);
                }
            }
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
                // 释放托管状态(托管对象)
            }

            rtcReleaseScene(_scene);
            _scene = default;

            _disposedValue = true;
        }
    }

    ~RtcScene()
    {
        Dispose(disposing: false);
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
