using System;
using System.Runtime.InteropServices;
using System.Text;
using EmbreeSharp.Native;

namespace EmbreeSharp
{
    public delegate void ErrorFunction(RTCError code, string str);
    public delegate bool MemoryMonitorFunction(long bytes, bool post);

    public static partial class RayTracingCore
    {
        private static RTCErrorFunction? _nativeErrorFunc;
        private static ErrorFunction? _managedErrorFunc;
        private static RTCMemoryMonitorFunction? _nativeMemMonitor;
        private static MemoryMonitorFunction? _managedMemMonitor;

        public static unsafe RTCDevice NewDevice(string? config)
        {
            RTCDevice result;
            if (config == null)
            {
                result = GlobalFunctions.rtcNewDevice(null);
            }
            else
            {
                int byteLength = Encoding.UTF8.GetByteCount(config);
                Span<byte> configBytes = byteLength <= 256 ? stackalloc byte[256] : new byte[byteLength];
                Encoding.UTF8.GetBytes(config, configBytes);
                fixed (byte* ptr = configBytes)
                {
                    result = GlobalFunctions.rtcNewDevice(ptr);
                }
            }
            return result;
        }

        public static void RetainDevice(RTCDevice device) => GlobalFunctions.rtcRetainDevice(device);

        public static void ReleaseDevice(RTCDevice device) => GlobalFunctions.rtcReleaseDevice(device);

        public static long GetDeviceProperty(RTCDevice device, RTCDeviceProperty prop)
        {
            var result = GlobalFunctions.rtcGetDeviceProperty(device, prop);
            return result.ToInt64();
        }

        public static void SetDeviceProperty(RTCDevice device, RTCDeviceProperty prop, long value)
        {
            GlobalFunctions.rtcSetDeviceProperty(device, prop, new nint(value));
        }

        public static RTCError GetDeviceError(RTCDevice device)
        {
            return GlobalFunctions.rtcGetDeviceError(device);
        }

        private static unsafe void DeviceErrorFunctionImpl(void* userPtr, RTCError code, byte* str)
        {
            long len = InteropUtlity.Strlen(str);
            int byteCnt = len <= int.MaxValue ? (int)len : int.MaxValue;
            var mgrStr = Encoding.UTF8.GetString(str, byteCnt);
            _managedErrorFunc?.Invoke(code, mgrStr);
        }

        public static unsafe void SetDeviceErrorFunction(RTCDevice device, ErrorFunction? func)
        {
            if (_nativeErrorFunc != null)
            {
                GlobalFunctions.rtcSetDeviceErrorFunction(device, nint.Zero, null);
            }
            if (func == null)
            {
                _nativeErrorFunc = null;
                _managedErrorFunc = null;
            }
            else
            {
                _managedErrorFunc = func;
                _nativeErrorFunc = DeviceErrorFunctionImpl;
                var ptr = Marshal.GetFunctionPointerForDelegate(_nativeErrorFunc);
                GlobalFunctions.rtcSetDeviceErrorFunction(device, ptr, null);
            }
        }

        private static unsafe bool DeviceMemoryMonitorFunctionImpl(void* ptr, nint bytes, bool post)
        {
            return _managedMemMonitor?.Invoke(bytes.ToInt64(), post) ?? true;
        }

        public static unsafe void SetDeviceMemoryMonitorFunction(RTCDevice device, MemoryMonitorFunction? func)
        {
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
                _nativeMemMonitor = DeviceMemoryMonitorFunctionImpl;
                var ptr = Marshal.GetFunctionPointerForDelegate(_nativeMemMonitor);
                GlobalFunctions.rtcSetDeviceMemoryMonitorFunction(device, ptr, null);
            }
        }
    }
}
