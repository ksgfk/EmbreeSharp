using EmbreeSharp.Native;
using System;
using System.Runtime.InteropServices;
using static EmbreeSharp.Native.EmbreeNative;

namespace EmbreeSharp;

public class RtcScene : IDisposable
{
    RTCScene _scene;
    RTCMemoryMonitorFunction? _progress;
    bool _disposedValue;

    public RTCScene NativeHandler => _scene;

    public RtcScene(RTCDevice device)
    {
        _scene = rtcNewScene(device);
    }

    public RtcScene(RtcDevice device) : this(device.NativeHandler) { }

    ~RtcScene()
    {
        Dispose(disposing: false);
    }

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

    public void SetProgressMonitorCallback(RtcMemoryMonitorCallback? callback)
    {
        unsafe
        {
            if (callback == null)
            {
                rtcSetSceneProgressMonitorFunction(_scene, IntPtr.Zero, null);
                _progress = null;
            }
            else
            {
                _progress = (void* ptr, long bytes, bool post) =>
                {
                    return callback(bytes, post);
                };
                IntPtr p = Marshal.GetFunctionPointerForDelegate(_progress);
                rtcSetSceneProgressMonitorFunction(_scene, p, null);
            }
        }
    }

    public void SetBuildQuality(RTCBuildQuality quality)
    {
        rtcSetSceneBuildQuality(_scene, quality);
    }

    public void SetFlags(RTCSceneFlags flags)
    {
        rtcSetSceneFlags(_scene, flags);
    }

    public RTCBounds GetBounds()
    {
        RTCBounds bounds = default;
        unsafe
        {
            rtcGetSceneBounds(_scene, &bounds);
        }
        return bounds;
    }

    public RTCLinearBounds GetLinearBounds()
    {
        RTCLinearBounds bounds = default;
        unsafe
        {
            rtcGetSceneLinearBounds(_scene, &bounds);
        }
        return bounds;
    }

    public void Intersect(ref RTCRayHit rayhit)
    {
        unsafe
        {
            fixed (RTCRayHit* ptr = &rayhit)
            {
                rtcIntersect1(_scene, ptr, null);
            }
        }
    }

    public void Intersect(in int valid, ref RTCRayHit4 rayhit)
    {
        unsafe
        {
            fixed (int* p1 = &valid)
            {
                fixed (RTCRayHit4* ptr = &rayhit)
                {
                    rtcIntersect4(p1, _scene, ptr, null);
                }
            }
        }
    }

    public void Intersect(in int valid, ref RTCRayHit8 rayhit)
    {
        unsafe
        {
            fixed (int* p1 = &valid)
            {
                fixed (RTCRayHit8* ptr = &rayhit)
                {
                    rtcIntersect8(p1, _scene, ptr, null);
                }
            }
        }
    }

    public void Intersect(in int valid, ref RTCRayHit16 rayhit)
    {
        unsafe
        {
            fixed (int* p1 = &valid)
            {
                fixed (RTCRayHit16* ptr = &rayhit)
                {
                    rtcIntersect16(p1, _scene, ptr, null);
                }
            }
        }
    }

    public void Intersect(ref RTCRayHit rayhit, in RTCIntersectArguments args)
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

    public void Intersect(in int valid, ref RTCRayHit4 rayhit, in RTCIntersectArguments args)
    {
        unsafe
        {
            fixed (int* p1 = &valid)
            {
                fixed (RTCRayHit4* ptr = &rayhit)
                {
                    fixed (RTCIntersectArguments* argsPtr = &args)
                    {
                        rtcIntersect4(p1, _scene, ptr, argsPtr);
                    }
                }
            }
        }
    }

    public void Intersect(in int valid, ref RTCRayHit8 rayhit, in RTCIntersectArguments args)
    {
        unsafe
        {
            fixed (int* p1 = &valid)
            {
                fixed (RTCRayHit8* ptr = &rayhit)
                {
                    fixed (RTCIntersectArguments* argsPtr = &args)
                    {
                        rtcIntersect8(p1, _scene, ptr, argsPtr);
                    }
                }
            }
        }
    }

    public void Intersect(in int valid, ref RTCRayHit16 rayhit, in RTCIntersectArguments args)
    {
        unsafe
        {
            fixed (int* p1 = &valid)
            {
                fixed (RTCRayHit16* ptr = &rayhit)
                {
                    fixed (RTCIntersectArguments* argsPtr = &args)
                    {
                        rtcIntersect16(p1, _scene, ptr, argsPtr);
                    }
                }
            }
        }
    }

    public void Occluded(ref RTCRay ray)
    {
        unsafe
        {
            fixed (RTCRay* ptr = &ray)
            {
                rtcOccluded1(_scene, ptr, null);
            }
        }
    }

    public void Occluded(in int valid, ref RTCRay4 ray)
    {
        unsafe
        {
            fixed (int* p1 = &valid)
            {
                fixed (RTCRay4* ptr = &ray)
                {
                    rtcOccluded4(p1, _scene, ptr, null);
                }
            }
        }
    }

    public void Occluded(in int valid, ref RTCRay8 ray)
    {
        unsafe
        {
            fixed (int* p1 = &valid)
            {
                fixed (RTCRay8* ptr = &ray)
                {
                    rtcOccluded8(p1, _scene, ptr, null);
                }
            }
        }
    }

    public void Occluded(in int valid, ref RTCRay16 ray)
    {
        unsafe
        {
            fixed (int* p1 = &valid)
            {
                fixed (RTCRay16* ptr = &ray)
                {
                    rtcOccluded16(p1, _scene, ptr, null);
                }
            }
        }
    }

    public void Occluded(ref RTCRay ray, in RTCOccludedArguments args)
    {
        unsafe
        {
            fixed (RTCRay* ptr = &ray)
            {
                fixed (RTCOccludedArguments* argsPtr = &args)
                {
                    rtcOccluded1(_scene, ptr, argsPtr);
                }
            }
        }
    }

    public void Occluded(in int valid, ref RTCRay4 ray, in RTCOccludedArguments args)
    {
        unsafe
        {
            fixed (int* p1 = &valid)
            {
                fixed (RTCRay4* ptr = &ray)
                {
                    fixed (RTCOccludedArguments* argsPtr = &args)
                    {
                        rtcOccluded4(p1, _scene, ptr, argsPtr);
                    }
                }
            }
        }
    }

    public void Occluded(in int valid, ref RTCRay8 ray, in RTCOccludedArguments args)
    {
        unsafe
        {
            fixed (int* p1 = &valid)
            {
                fixed (RTCRay8* ptr = &ray)
                {
                    fixed (RTCOccludedArguments* argsPtr = &args)
                    {
                        rtcOccluded8(p1, _scene, ptr, argsPtr);
                    }
                }
            }
        }
    }

    public void Occluded(in int valid, ref RTCRay16 ray, in RTCOccludedArguments args)
    {
        unsafe
        {
            fixed (int* p1 = &valid)
            {
                fixed (RTCRay16* ptr = &ray)
                {
                    fixed (RTCOccludedArguments* argsPtr = &args)
                    {
                        rtcOccluded16(p1, _scene, ptr, argsPtr);
                    }
                }
            }
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            rtcReleaseScene(_scene);
            _scene = default;

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
