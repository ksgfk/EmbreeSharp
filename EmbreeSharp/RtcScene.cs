using EmbreeSharp.Native;
using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace EmbreeSharp
{
    public delegate bool ProgressMonitorFunction(double n);

    public class RtcScene : IDisposable
    {
        private GCHandle _gcHandle;
        private RTCScene _scene;
        private RTCProgressMonitorFunction? _nativeProgMonitor;
        private ProgressMonitorFunction? _managedProgMonitor;
        private bool _disposedValue;

        public RTCScene NativeScene
        {
            get
            {
                if (IsDisposed)
                {
                    ThrowUtility.ObjectDisposed();
                }
                return _scene;
            }
        }
        public bool IsDisposed => _disposedValue;

        public RtcScene(RtcDevice device)
        {
            _gcHandle = GCHandle.Alloc(this, GCHandleType.Weak);
            _scene = GlobalFunctions.rtcNewScene(device.NativeDevice);
        }

        public RtcScene(RtcScene other)
        {
            if (other.IsDisposed)
            {
                ThrowUtility.ObjectDisposed(nameof(other));
            }
            _gcHandle = GCHandle.Alloc(this, GCHandleType.Weak);
            GlobalFunctions.rtcRetainScene(other._scene);
            _scene = other._scene;
        }

        ~RtcScene()
        {
            Dispose(disposing: false);
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _managedProgMonitor = null;
                }
                unsafe
                {
                    if (_nativeProgMonitor != null)
                    {
                        GlobalFunctions.rtcSetSceneProgressMonitorFunction(_scene, nint.Zero, null);
                    }
                    _nativeProgMonitor = null;
                }
                GlobalFunctions.rtcReleaseScene(_scene);
                _scene = RTCScene.Null;
                _gcHandle.Free();
                _gcHandle = default;
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public void AttachGeometry(RtcGeometry geometry)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            uint id = GlobalFunctions.rtcAttachGeometry(_scene, geometry.NativeGeometry);
            geometry.Id = id;
        }

        public void AttachGeometry(RtcGeometry geometry, uint id)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            GlobalFunctions.rtcAttachGeometryByID(_scene, geometry.NativeGeometry, id);
            geometry.Id = id;
        }

        public void DetachGeometry(RtcGeometry geometry)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            GlobalFunctions.rtcDetachGeometry(_scene, geometry.Id);
        }

        public void Commit()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            GlobalFunctions.rtcCommitScene(_scene);
        }

        public void JoinCommit()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            GlobalFunctions.rtcJoinCommitScene(_scene);
        }

        private static unsafe bool ProgressMonitorFunctionImpl(void* ptr, double n)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(new nint(ptr));
            if (!gcHandle.IsAllocated)
            {
                return true;
            }
            RtcScene scene = (RtcScene)gcHandle.Target!;
            return scene._managedProgMonitor?.Invoke(n) ?? true;
        }

        public unsafe void SetProgressMonitorFunction(ProgressMonitorFunction? func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            if (_nativeProgMonitor != null)
            {
                GlobalFunctions.rtcSetSceneProgressMonitorFunction(_scene, nint.Zero, null);
            }
            if (func == null)
            {
                _nativeProgMonitor = null;
                _managedProgMonitor = null;
            }
            else
            {
                _managedProgMonitor = func;
                _nativeProgMonitor = ProgressMonitorFunctionImpl;
                var ptr = Marshal.GetFunctionPointerForDelegate(_nativeProgMonitor);
                GlobalFunctions.rtcSetSceneProgressMonitorFunction(_scene, ptr, GCHandle.ToIntPtr(_gcHandle).ToPointer());
            }
        }

        public void SetBuildQuality(RTCBuildQuality quality)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            GlobalFunctions.rtcSetSceneBuildQuality(_scene, quality);
        }

        public void SetFlags(RTCSceneFlags flags)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            GlobalFunctions.rtcSetSceneFlags(_scene, flags);
        }

        public RTCSceneFlags GetFlags()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            return GlobalFunctions.rtcGetSceneFlags(_scene);
        }

        public unsafe RTCBounds GetBounds()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            Span<byte> bounds = stackalloc byte[sizeof(RTCBounds) + RTCBounds.Alignment];
            ref RTCBounds result = ref InteropUtility.StackAllocAligned<RTCBounds>(bounds, RTCBounds.Alignment);
            GlobalFunctions.rtcGetSceneBounds(_scene, (RTCBounds*)Unsafe.AsPointer(ref result));
            return result;
        }

        public unsafe RTCLinearBounds GetLinearBounds()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            Span<byte> bounds = stackalloc byte[sizeof(RTCLinearBounds) + RTCLinearBounds.Alignment];
            ref RTCLinearBounds result = ref InteropUtility.StackAllocAligned<RTCLinearBounds>(bounds, RTCLinearBounds.Alignment);
            GlobalFunctions.rtcGetSceneLinearBounds(_scene, (RTCLinearBounds*)Unsafe.AsPointer(ref result));
            return result;
        }

        public unsafe void Intersect(ref RTCRayHit rayHit)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            Span<byte> stack = stackalloc byte[sizeof(RTCRayHit) + RTCRayHit.Alignment];
            ref RTCRayHit rayHitAligned = ref InteropUtility.StackAllocAligned<RTCRayHit>(stack, RTCRayHit.Alignment);
            rayHitAligned = rayHit;
            GlobalFunctions.rtcIntersect1(_scene, (RTCRayHit*)Unsafe.AsPointer(ref rayHitAligned), null);
            rayHit = rayHitAligned;
        }

        public unsafe void Intersect(Span<int> valid, ref RTCRayHit4 rayHit)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            if (valid.Length != 4)
            {
                ThrowUtility.ArgumentOutOfRange();
            }
            Span<byte> stack = stackalloc byte[sizeof(RTCRayHit4) + RTCRayHit4.Alignment];
            ref RTCRayHit4 rayHitAligned = ref InteropUtility.StackAllocAligned<RTCRayHit4>(stack, RTCRayHit4.Alignment);
            rayHitAligned = rayHit;
            fixed (int* validPtr = valid)
            {
                GlobalFunctions.rtcIntersect4(validPtr, _scene, (RTCRayHit4*)Unsafe.AsPointer(ref rayHitAligned), null);
            }
            rayHit = rayHitAligned;
        }

        public unsafe void Intersect(Span<int> valid, ref RTCRayHit8 rayHit)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            if (valid.Length != 8)
            {
                ThrowUtility.ArgumentOutOfRange();
            }
            Span<byte> stack = stackalloc byte[sizeof(RTCRayHit8) + RTCRayHit8.Alignment];
            ref RTCRayHit8 rayHitAligned = ref InteropUtility.StackAllocAligned<RTCRayHit8>(stack, RTCRayHit8.Alignment);
            rayHitAligned = rayHit;
            fixed (int* validPtr = valid)
            {
                GlobalFunctions.rtcIntersect8(validPtr, _scene, (RTCRayHit8*)Unsafe.AsPointer(ref rayHitAligned), null);
            }
            rayHit = rayHitAligned;
        }

        public unsafe void Intersect(Span<int> valid, ref RTCRayHit16 rayHit)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            if (valid.Length != 16)
            {
                ThrowUtility.ArgumentOutOfRange();
            }
            Span<byte> stack = stackalloc byte[sizeof(RTCRayHit16) + RTCRayHit16.Alignment];
            ref RTCRayHit16 rayHitAligned = ref InteropUtility.StackAllocAligned<RTCRayHit16>(stack, RTCRayHit16.Alignment);
            rayHitAligned = rayHit;
            fixed (int* validPtr = valid)
            {
                GlobalFunctions.rtcIntersect16(validPtr, _scene, (RTCRayHit16*)Unsafe.AsPointer(ref rayHitAligned), null);
            }
            rayHit = rayHitAligned;
        }

        public unsafe Matrix4x4 GetGeometryTransform4x4(uint geomID, float time)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            Span<float> mat = stackalloc float[16];
            fixed (float* ptr = mat)
            {
                GlobalFunctions.rtcGetGeometryTransformFromScene(_scene, geomID, time, RTCFormat.RTC_FORMAT_FLOAT4X4_COLUMN_MAJOR, ptr);
            }
            // C# matrix is row-major
            return new Matrix4x4(
                mat[0], mat[4], mat[8], mat[12],
                mat[1], mat[5], mat[9], mat[13],
                mat[2], mat[6], mat[10], mat[14],
                mat[3], mat[7], mat[11], mat[15]);
        }
    }
}
