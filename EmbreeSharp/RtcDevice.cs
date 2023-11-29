using System;
using System.Runtime.InteropServices;
using System.Text;
using EmbreeSharp.Native;

namespace EmbreeSharp
{
    public delegate void ErrorFunction(RTCError code, string str);
    public delegate bool MemoryMonitorFunction(long bytes, bool post);

    public class RtcDevice : IDisposable
    {
        private GCHandle _gcHandle;
        private RTCDevice _device;
        private RTCErrorFunction? _nativeErrorFunc;
        private ErrorFunction? _managedErrorFunc;
        private RTCMemoryMonitorFunction? _nativeMemMonitor;
        private MemoryMonitorFunction? _managedMemMonitor;
        private bool _disposedValue = false;

        public RTCDevice NativeDevice
        {
            get
            {
                if (IsDisposed)
                {
                    ThrowUtility.ObjectDisposed();
                }
                return _device;
            }
        }
        public bool IsDisposed => _disposedValue;

        public RtcDevice()
        {
            _gcHandle = GCHandle.Alloc(this);
            unsafe
            {
                _device = GlobalFunctions.rtcNewDevice(null);
            }
        }

        public unsafe RtcDevice(string config)
        {
            _gcHandle = GCHandle.Alloc(this);
            int byteLength = Encoding.UTF8.GetByteCount(config);
            Span<byte> configBytes = byteLength <= 256 ? stackalloc byte[256] : new byte[byteLength];
            Encoding.UTF8.GetBytes(config, configBytes);
            fixed (byte* ptr = configBytes)
            {
                _device = GlobalFunctions.rtcNewDevice(ptr);
            }
        }

        public RtcDevice(RtcDevice other)
        {
            if (other.IsDisposed)
            {
                ThrowUtility.ObjectDisposed(nameof(other));
            }
            _gcHandle = GCHandle.Alloc(this);
            GlobalFunctions.rtcRetainDevice(_device);
            _device = other._device;
        }

        ~RtcDevice()
        {
            Dispose(disposing: false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _managedErrorFunc = null;
                    _managedMemMonitor = null;
                }
                unsafe
                {
                    if (_nativeErrorFunc != null)
                    {
                        GlobalFunctions.rtcSetDeviceErrorFunction(_device, nint.Zero, null);
                    }
                    _nativeErrorFunc = null;
                    if (_nativeMemMonitor != null)
                    {
                        GlobalFunctions.rtcSetDeviceMemoryMonitorFunction(_device, nint.Zero, null);
                    }
                    _nativeMemMonitor = null;
                }
                GlobalFunctions.rtcReleaseDevice(_device);
                _device = RTCDevice.Null;
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

        public long GetProperty(RTCDeviceProperty prop)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            var result = GlobalFunctions.rtcGetDeviceProperty(_device, prop);
            return result.ToInt64();
        }

        public void SetProperty(RTCDeviceProperty prop, long value)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            GlobalFunctions.rtcSetDeviceProperty(_device, prop, new nint(value));
        }

        public RTCError GetError()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            return GlobalFunctions.rtcGetDeviceError(_device);
        }

        private static unsafe void ErrorFunctionImpl(void* userPtr, RTCError code, byte* str)
        {
            nuint len = InteropUtility.Strlen(str);
            int byteCnt = len <= int.MaxValue ? (int)len : int.MaxValue;
            var mgrStr = Encoding.UTF8.GetString(str, byteCnt);
            GCHandle gcHandle = GCHandle.FromIntPtr(new nint(userPtr));
            RtcDevice device = (RtcDevice)gcHandle.Target!;
            device._managedErrorFunc?.Invoke(code, mgrStr);
        }

        public unsafe void SetErrorFunction(ErrorFunction? func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            if (_nativeErrorFunc != null)
            {
                GlobalFunctions.rtcSetDeviceErrorFunction(_device, nint.Zero, null);
            }
            if (func == null)
            {
                _nativeErrorFunc = null;
                _managedErrorFunc = null;
            }
            else
            {
                _managedErrorFunc = func;
                _nativeErrorFunc = ErrorFunctionImpl;
                var ptr = Marshal.GetFunctionPointerForDelegate(_nativeErrorFunc);
                GlobalFunctions.rtcSetDeviceErrorFunction(_device, ptr, GCHandle.ToIntPtr(_gcHandle).ToPointer());
            }
        }

        private static unsafe bool MemoryMonitorFunctionImpl(void* ptr, nint bytes, bool post)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(new nint(ptr));
            RtcDevice device = (RtcDevice)gcHandle.Target!;
            return device._managedMemMonitor?.Invoke(bytes.ToInt64(), post) ?? true;
        }

        public unsafe void SetMemoryMonitorFunction(RTCDevice device, MemoryMonitorFunction? func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            if (_nativeMemMonitor != null)
            {
                GlobalFunctions.rtcSetDeviceMemoryMonitorFunction(device, nint.Zero, null);
            }
            if (func == null)
            {
                _nativeMemMonitor = null;
                _managedMemMonitor = null;
            }
            else
            {
                _managedMemMonitor = func;
                _nativeMemMonitor = MemoryMonitorFunctionImpl;
                var ptr = Marshal.GetFunctionPointerForDelegate(_nativeMemMonitor);
                GlobalFunctions.rtcSetDeviceMemoryMonitorFunction(device, ptr, GCHandle.ToIntPtr(_gcHandle).ToPointer());
            }
        }
    }
}
