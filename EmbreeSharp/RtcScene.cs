using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using EmbreeSharp.Native;

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
            _gcHandle = GCHandle.Alloc(this);
            _scene = GlobalFunctions.rtcNewScene(device.NativeDevice);
        }

        public RtcScene(RtcScene other)
        {
            if (other.IsDisposed)
            {
                ThrowUtility.ObjectDisposed(nameof(other));
            }
            _gcHandle = GCHandle.Alloc(this);
            GlobalFunctions.rtcRetainScene(other._scene);
            _scene = other._scene;
        }

        ~RtcScene()
        {
            Dispose(disposing: false);
        }

        protected virtual void Dispose(bool disposing)
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
            Span<byte> bounds = stackalloc byte[sizeof(RTCBounds) + RTCBounds.Alignment];
            ref RTCBounds result = ref InteropUtility.StackAllocAligned<RTCBounds>(bounds, RTCBounds.Alignment);
            GlobalFunctions.rtcGetSceneBounds(_scene, (RTCBounds*)Unsafe.AsPointer(ref result));
            return result;
        }

        public unsafe RTCLinearBounds GetLinearBounds()
        {
            Span<byte> bounds = stackalloc byte[sizeof(RTCLinearBounds) + RTCLinearBounds.Alignment];
            ref RTCLinearBounds result = ref InteropUtility.StackAllocAligned<RTCLinearBounds>(bounds, RTCLinearBounds.Alignment);
            GlobalFunctions.rtcGetSceneLinearBounds(_scene, (RTCLinearBounds*)Unsafe.AsPointer(ref result));
            return result;
        }
    }
}
