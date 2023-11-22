using System;
using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    public unsafe static class Embree
    {
        public const int RTC_MAX_INSTANCE_LEVEL_COUNT = 1;
        public const uint RTC_INVALID_GEOMETRY_ID = unchecked((uint)-1);
        public const string DynamicLibraryName = "embree4";

        /// <summary>
        /// Creates a new Embree device.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern RTCDevice rtcNewDevice([NativeType("const char*")] byte* config);
        /// <summary>
        /// Retains the Embree device (increments the reference count).
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcRetainDevice(RTCDevice device);
        /// <summary>
        /// Releases an Embree device (decrements the reference count).
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcReleaseDevice(RTCDevice device);

        /// <summary>
        /// Gets a device property.
        /// </summary>
        /// <returns></returns>
        [DllImport(DynamicLibraryName)]
        [return: NativeType("ssize_t")]
        public static extern IntPtr rtcGetDeviceProperty(RTCDevice device, RTCDeviceProperty prop);
        /// <summary>
        /// Sets a device property.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetDeviceProperty(RTCDevice device, RTCDeviceProperty prop, [NativeType("ssize_t")] IntPtr value);
        /// <summary>
        /// Returns the error code.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern RTCError rtcGetDeviceError(RTCDevice device);
        /// <summary>
        /// Sets the error callback function.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetDeviceErrorFunction(RTCDevice device, RTCErrorFunction error, void* userPtr);
        /// <summary>
        /// Sets the memory monitor callback function.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetDeviceMemoryMonitorFunction(RTCDevice device, RTCMemoryMonitorFunction memoryMonitor, void* userPtr);
    }
}