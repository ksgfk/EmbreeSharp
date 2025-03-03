using EmbreeSharp.Native;
using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace EmbreeSharp
{
    public delegate bool ProgressMonitorFunction(double n);

    public class EmbreeScene : IDisposable
    {
        private GCHandle _gcHandle;
        private RTCSceneHandle _scene;
        private ProgressMonitorFunction? _progMonitor;
        private bool _disposedValue;

        public RTCScene NativeScene
        {
            get
            {
                if (IsDisposed)
                {
                    ThrowUtility.ObjectDisposed();
                }
                return new RTCScene() { Ptr = _scene.DangerousGetHandle() };
            }
        }
        public bool IsDisposed => _disposedValue;

        public EmbreeScene(EmbreeDevice device)
        {
            _gcHandle = GCHandle.Alloc(this, GCHandleType.Weak);
            var scene = EmbreeNative.rtcNewScene(device.NativeDevice);
            _scene = new RTCSceneHandle(scene);
        }

        ~EmbreeScene()
        {
            Dispose(disposing: false);
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _progMonitor = null;
                }
                unsafe
                {
                    EmbreeNative.rtcSetSceneProgressMonitorFunction(NativeScene, null, null);
                }
                _gcHandle.Free();
                _scene.Dispose();
                if (disposing)
                {
                    _gcHandle = default;
                    _scene = null!;
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public void AttachGeometry(EmbreeGeometry geometry)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            uint id = EmbreeNative.rtcAttachGeometry(NativeScene, geometry.NativeGeometry);
            geometry.Id = id;
        }

        public void AttachGeometry(EmbreeGeometry geometry, uint id)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcAttachGeometryByID(NativeScene, geometry.NativeGeometry, id);
            geometry.Id = id;
        }

        public void DetachGeometry(EmbreeGeometry geometry)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcDetachGeometry(NativeScene, geometry.Id);
        }

        public void Commit()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcCommitScene(NativeScene);
        }

        public void JoinCommit()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcJoinCommitScene(NativeScene);
        }

        private static unsafe bool ProgressMonitorFunctionImpl(void* ptr, double n)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(new nint(ptr));
            if (!gcHandle.IsAllocated)
            {
                return true;
            }
            EmbreeScene scene = (EmbreeScene)gcHandle.Target!;
            return scene._progMonitor?.Invoke(n) ?? true;
        }

        public unsafe void SetProgressMonitorFunction(ProgressMonitorFunction? func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _progMonitor = func;
            if (func == null)
            {
                EmbreeNative.rtcSetSceneProgressMonitorFunction(NativeScene, null, null);
            }
            else
            {
                EmbreeNative.rtcSetSceneProgressMonitorFunction(NativeScene, ProgressMonitorFunctionImpl, GCHandle.ToIntPtr(_gcHandle).ToPointer());
            }
        }

        public void SetBuildQuality(RTCBuildQuality quality)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcSetSceneBuildQuality(NativeScene, quality);
        }

        public void SetFlags(RTCSceneFlags flags)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcSetSceneFlags(NativeScene, flags);
        }

        public RTCSceneFlags GetFlags()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            return EmbreeNative.rtcGetSceneFlags(NativeScene);
        }

        public unsafe RTCBounds GetBounds()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            Span<byte> bounds = stackalloc byte[sizeof(RTCBounds) + RTCBounds.Alignment];
            ref RTCBounds result = ref InteropUtility.StackAllocAligned<RTCBounds>(bounds, RTCBounds.Alignment);
            EmbreeNative.rtcGetSceneBounds(NativeScene, (RTCBounds*)Unsafe.AsPointer(ref result));
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
            EmbreeNative.rtcGetSceneLinearBounds(NativeScene, (RTCLinearBounds*)Unsafe.AsPointer(ref result));
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
            EmbreeNative.rtcIntersect1(NativeScene, (RTCRayHit*)Unsafe.AsPointer(ref rayHitAligned), null);
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
                EmbreeNative.rtcIntersect4(validPtr, NativeScene, (RTCRayHit4*)Unsafe.AsPointer(ref rayHitAligned), null);
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
                EmbreeNative.rtcIntersect8(validPtr, NativeScene, (RTCRayHit8*)Unsafe.AsPointer(ref rayHitAligned), null);
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
                EmbreeNative.rtcIntersect16(validPtr, NativeScene, (RTCRayHit16*)Unsafe.AsPointer(ref rayHitAligned), null);
            }
            rayHit = rayHitAligned;
        }

        public unsafe void Occluded(ref RTCRay ray)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            Span<byte> stack = stackalloc byte[sizeof(RTCRay) + RTCRay.Alignment];
            ref RTCRay rayAligned = ref InteropUtility.StackAllocAligned<RTCRay>(stack, RTCRay.Alignment);
            rayAligned = ray;
            EmbreeNative.rtcOccluded1(NativeScene, (RTCRay*)Unsafe.AsPointer(ref rayAligned), null);
            ray = rayAligned;
        }

        public unsafe void Occluded(Span<int> valid, ref RTCRay4 ray)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            if (valid.Length != 4)
            {
                ThrowUtility.ArgumentOutOfRange();
            }
            Span<byte> stack = stackalloc byte[sizeof(RTCRay4) + RTCRay4.Alignment];
            ref RTCRay4 rayAligned = ref InteropUtility.StackAllocAligned<RTCRay4>(stack, RTCRay4.Alignment);
            rayAligned = ray;
            fixed (int* validPtr = valid)
            {
                EmbreeNative.rtcOccluded4(validPtr, NativeScene, (RTCRay4*)Unsafe.AsPointer(ref rayAligned), null);
            }
            ray = rayAligned;
        }

        public unsafe void Occluded(Span<int> valid, ref RTCRay8 ray)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            if (valid.Length != 8)
            {
                ThrowUtility.ArgumentOutOfRange();
            }
            Span<byte> stack = stackalloc byte[sizeof(RTCRay8) + RTCRay8.Alignment];
            ref RTCRay8 rayAligned = ref InteropUtility.StackAllocAligned<RTCRay8>(stack, RTCRay8.Alignment);
            rayAligned = ray;
            fixed (int* validPtr = valid)
            {
                EmbreeNative.rtcOccluded8(validPtr, NativeScene, (RTCRay8*)Unsafe.AsPointer(ref rayAligned), null);
            }
            ray = rayAligned;
        }

        public unsafe void Occluded(Span<int> valid, ref RTCRay16 ray)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            if (valid.Length != 16)
            {
                ThrowUtility.ArgumentOutOfRange();
            }
            Span<byte> stack = stackalloc byte[sizeof(RTCRay16) + RTCRay16.Alignment];
            ref RTCRay16 rayAligned = ref InteropUtility.StackAllocAligned<RTCRay16>(stack, RTCRay16.Alignment);
            rayAligned = ray;
            fixed (int* validPtr = valid)
            {
                EmbreeNative.rtcOccluded16(validPtr, NativeScene, (RTCRay16*)Unsafe.AsPointer(ref rayAligned), null);
            }
            ray = rayAligned;
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
                EmbreeNative.rtcGetGeometryTransformFromScene(NativeScene, geomID, time, RTCFormat.RTC_FORMAT_FLOAT4X4_COLUMN_MAJOR, ptr);
            }
            return InteropUtility.RTCFloat4x4ToMatrix4x4ColumnMajor(mat);
        }
    }
}
