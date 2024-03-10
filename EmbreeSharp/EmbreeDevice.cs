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
        private RTCDevice _device;
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
                return _device;
            }
        }
        public bool IsDisposed => _disposedValue;

        public EmbreeDevice()
        {
            _gcHandle = GCHandle.Alloc(this);
            unsafe
            {
                _device = EmbreeNative.rtcNewDevice(null);
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
                _device = EmbreeNative.rtcNewDevice(ptr);
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
                    EmbreeNative.rtcSetDeviceErrorFunction(_device, null, null);
                    EmbreeNative.rtcSetDeviceMemoryMonitorFunction(_device, null, null);
                }
                _gcHandle.Free();
                _gcHandle = default;
                _device.Dispose();
                _device = null!;
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
            var result = EmbreeNative.rtcGetDeviceProperty(_device, prop);
            return result.ToInt64();
        }

        public void SetProperty(RTCDeviceProperty prop, long value)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcSetDeviceProperty(_device, prop, new nint(value));
        }

        public RTCError GetError()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            return EmbreeNative.rtcGetDeviceError(_device);
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
                EmbreeNative.rtcSetDeviceErrorFunction(_device, null, null);
            }
            else
            {
                EmbreeNative.rtcSetDeviceErrorFunction(_device, ErrorFunctionImpl, GCHandle.ToIntPtr(_gcHandle).ToPointer());
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
                EmbreeNative.rtcSetDeviceMemoryMonitorFunction(_device, null, null);
            }
            else
            {
                EmbreeNative.rtcSetDeviceMemoryMonitorFunction(_device, MemoryMonitorFunctionImpl, GCHandle.ToIntPtr(_gcHandle).ToPointer());
            }
        }
    }
}
