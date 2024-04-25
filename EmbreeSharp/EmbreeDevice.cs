using EmbreeSharp.Native;
using System;
using System.Runtime.InteropServices;
using System.Text;

namespace EmbreeSharp
{
    public delegate void ErrorFunction(RTCError code, string str);
    public delegate bool MemoryMonitorFunction(long bytes, bool post);

    public class EmbreeDevice : IDisposable
    {
        private GCHandle _gcHandle;
        private RTCDeviceHandle _device;
        private ErrorFunction? _errorFunc;
        private MemoryMonitorFunction? _memMonitor;
        private bool _disposedValue = false;

        public RTCDevice NativeDevice
        {
            get
            {
                if (IsDisposed)
                {
                    ThrowUtility.ObjectDisposed();
                }
                return new RTCDevice() { Ptr = _device.DangerousGetHandle() };
            }
        }
        public bool IsDisposed => _disposedValue;

        public EmbreeDevice()
        {
            _gcHandle = GCHandle.Alloc(this);
            unsafe
            {
                var device = EmbreeNative.rtcNewDevice(null);
                _device = new RTCDeviceHandle(device);
            }
        }

        public unsafe EmbreeDevice(string config)
        {
            _gcHandle = GCHandle.Alloc(this, GCHandleType.Weak);
            int byteLength = Encoding.UTF8.GetByteCount(config);
            Span<byte> configBytes = byteLength <= 256 ? stackalloc byte[256] : new byte[byteLength];
            Encoding.UTF8.GetBytes(config, configBytes);
            fixed (byte* ptr = configBytes)
            {
                var device = EmbreeNative.rtcNewDevice(ptr);
                _device = new RTCDeviceHandle(device);
            }
        }

        ~EmbreeDevice()
        {
            Dispose(disposing: false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _errorFunc = null;
                    _memMonitor = null;
                }
                unsafe
                {
                    EmbreeNative.rtcSetDeviceErrorFunction(NativeDevice, null, null);
                    EmbreeNative.rtcSetDeviceMemoryMonitorFunction(NativeDevice, null, null);
                }
                _gcHandle.Free();
                _device.Dispose();
                if (disposing)
                {
                    _gcHandle = default;
                    _device = null!;
                }
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
            var result = EmbreeNative.rtcGetDeviceProperty(NativeDevice, prop);
            return result.ToInt64();
        }

        public void SetProperty(RTCDeviceProperty prop, long value)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcSetDeviceProperty(NativeDevice, prop, new nint(value));
        }

        public RTCError GetError()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            return EmbreeNative.rtcGetDeviceError(NativeDevice);
        }

        private static unsafe void ErrorFunctionImpl(void* userPtr, RTCError code, byte* str)
        {
            nuint len = InteropUtility.Strlen(str);
            int byteCnt = len <= int.MaxValue ? (int)len : int.MaxValue;
            var mgrStr = Encoding.UTF8.GetString(str, byteCnt);
            GCHandle gcHandle = GCHandle.FromIntPtr(new nint(userPtr));
            if (!gcHandle.IsAllocated)
            {
                return;
            }
            EmbreeDevice device = (EmbreeDevice)gcHandle.Target!;
            device._errorFunc?.Invoke(code, mgrStr);
        }

        public unsafe void SetErrorFunction(ErrorFunction? func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _errorFunc = func;
            if (func == null)
            {
                EmbreeNative.rtcSetDeviceErrorFunction(NativeDevice, null, null);
            }
            else
            {
                EmbreeNative.rtcSetDeviceErrorFunction(NativeDevice, ErrorFunctionImpl, GCHandle.ToIntPtr(_gcHandle).ToPointer());
            }
        }

        private static unsafe bool MemoryMonitorFunctionImpl(void* ptr, nint bytes, bool post)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(new nint(ptr));
            EmbreeDevice device = (EmbreeDevice)gcHandle.Target!;
            return device._memMonitor?.Invoke(bytes.ToInt64(), post) ?? true;
        }

        public unsafe void SetMemoryMonitorFunction(MemoryMonitorFunction? func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _memMonitor = func;
            if (func == null)
            {
                EmbreeNative.rtcSetDeviceMemoryMonitorFunction(NativeDevice, null, null);
            }
            else
            {
                EmbreeNative.rtcSetDeviceMemoryMonitorFunction(NativeDevice, MemoryMonitorFunctionImpl, GCHandle.ToIntPtr(_gcHandle).ToPointer());
            }
        }
    }
}
