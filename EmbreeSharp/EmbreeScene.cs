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
        private readonly RTCScene _scene;
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
                return _scene;
            }
        }
        public bool IsDisposed => _disposedValue;

        public EmbreeScene(EmbreeDevice device)
        {
            _gcHandle = GCHandle.Alloc(this, GCHandleType.Weak);
            _scene = EmbreeNative.rtcNewScene(device.NativeDevice);
        }

        public EmbreeScene(EmbreeScene other)
        {
            if (other.IsDisposed)
            {
                ThrowUtility.ObjectDisposed(nameof(other));
            }
            _gcHandle = GCHandle.Alloc(this, GCHandleType.Weak);
            EmbreeNative.rtcRetainScene(other._scene);
            _scene = other._scene;
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
                    EmbreeNative.rtcSetSceneProgressMonitorFunction(_scene, null, null);
                }
                _scene.Dispose();
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

        public void AttachGeometry(EmbreeGeometry geometry)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            uint id = EmbreeNative.rtcAttachGeometry(_scene, geometry.NativeGeometry);
            geometry.Id = id;
        }

        public void AttachGeometry(EmbreeGeometry geometry, uint id)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcAttachGeometryByID(_scene, geometry.NativeGeometry, id);
            geometry.Id = id;
        }

        public void DetachGeometry(EmbreeGeometry geometry)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcDetachGeometry(_scene, geometry.Id);
        }

        public void Commit()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcCommitScene(_scene);
        }

        public void JoinCommit()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcJoinCommitScene(_scene);
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
                EmbreeNative.rtcSetSceneProgressMonitorFunction(_scene, null, null);
            }
            else
            {
                EmbreeNative.rtcSetSceneProgressMonitorFunction(_scene, ProgressMonitorFunctionImpl, GCHandle.ToIntPtr(_gcHandle).ToPointer());
            }
        }

        public void SetBuildQuality(RTCBuildQuality quality)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcSetSceneBuildQuality(_scene, quality);
        }

        public void SetFlags(RTCSceneFlags flags)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcSetSceneFlags(_scene, flags);
        }

        public RTCSceneFlags GetFlags()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            return EmbreeNative.rtcGetSceneFlags(_scene);
        }

        public unsafe RTCBounds GetBounds()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            Span<byte> bounds = stackalloc byte[sizeof(RTCBounds) + RTCBounds.Alignment];
            ref RTCBounds result = ref InteropUtility.StackAllocAligned<RTCBounds>(bounds, RTCBounds.Alignment);
            EmbreeNative.rtcGetSceneBounds(_scene, (RTCBounds*)Unsafe.AsPointer(ref result));
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
            EmbreeNative.rtcGetSceneLinearBounds(_scene, (RTCLinearBounds*)Unsafe.AsPointer(ref result));
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
            EmbreeNative.rtcIntersect1(_scene, (RTCRayHit*)Unsafe.AsPointer(ref rayHitAligned), null);
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
                EmbreeNative.rtcIntersect4(validPtr, _scene, (RTCRayHit4*)Unsafe.AsPointer(ref rayHitAligned), null);
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
                EmbreeNative.rtcIntersect8(validPtr, _scene, (RTCRayHit8*)Unsafe.AsPointer(ref rayHitAligned), null);
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
                EmbreeNative.rtcIntersect16(validPtr, _scene, (RTCRayHit16*)Unsafe.AsPointer(ref rayHitAligned), null);
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
                EmbreeNative.rtcGetGeometryTransformFromScene(_scene, geomID, time, RTCFormat.RTC_FORMAT_FLOAT4X4_COLUMN_MAJOR, ptr);
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
