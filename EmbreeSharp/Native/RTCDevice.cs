using System;
using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    public struct RTCDevice
    {
        public nint Ptr;
    }

    public class RTCDeviceHandle : SafeHandle
    {
        public override bool IsInvalid => handle == nint.Zero;
        public RTCDeviceHandle(RTCDevice device) : base(0, true)
        {
            handle = device.Ptr;
        }

        protected override bool ReleaseHandle()
        {
            try
            {
                EmbreeNative.rtcReleaseDevice(new RTCDevice() { Ptr = handle });
                handle = 0;
                return true;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("cannot release RTCDevice, exception: {0}\n  at:\n{1}", e.Message, e.StackTrace);
                return false;
            }
        }
    }

    public static unsafe partial class EmbreeNative
    {
        /// <summary>
        /// Creates a new Embree device.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial RTCDevice rtcNewDevice([NativeType("const char*")] byte* config);
        /// <summary>
        /// Retains the Embree device (increments the reference count).
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcRetainDevice(RTCDevice device);
        /// <summary>
        /// Releases an Embree device (decrements the reference count).
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcReleaseDevice(RTCDevice device);
    }

    /// <summary>
    /// Device properties
    /// </summary>
    public enum RTCDeviceProperty
    {
        RTC_DEVICE_PROPERTY_VERSION = 0,
        RTC_DEVICE_PROPERTY_VERSION_MAJOR = 1,
        RTC_DEVICE_PROPERTY_VERSION_MINOR = 2,
        RTC_DEVICE_PROPERTY_VERSION_PATCH = 3,

        RTC_DEVICE_PROPERTY_NATIVE_RAY4_SUPPORTED = 32,
        RTC_DEVICE_PROPERTY_NATIVE_RAY8_SUPPORTED = 33,
        RTC_DEVICE_PROPERTY_NATIVE_RAY16_SUPPORTED = 34,

        RTC_DEVICE_PROPERTY_BACKFACE_CULLING_SPHERES_ENABLED = 62,
        RTC_DEVICE_PROPERTY_BACKFACE_CULLING_CURVES_ENABLED = 63,
        RTC_DEVICE_PROPERTY_RAY_MASK_SUPPORTED = 64,
        RTC_DEVICE_PROPERTY_BACKFACE_CULLING_ENABLED = 65,
        RTC_DEVICE_PROPERTY_FILTER_FUNCTION_SUPPORTED = 66,
        RTC_DEVICE_PROPERTY_IGNORE_INVALID_RAYS_ENABLED = 67,
        RTC_DEVICE_PROPERTY_COMPACT_POLYS_ENABLED = 68,

        RTC_DEVICE_PROPERTY_TRIANGLE_GEOMETRY_SUPPORTED = 96,
        RTC_DEVICE_PROPERTY_QUAD_GEOMETRY_SUPPORTED = 97,
        RTC_DEVICE_PROPERTY_SUBDIVISION_GEOMETRY_SUPPORTED = 98,
        RTC_DEVICE_PROPERTY_CURVE_GEOMETRY_SUPPORTED = 99,
        RTC_DEVICE_PROPERTY_USER_GEOMETRY_SUPPORTED = 100,
        RTC_DEVICE_PROPERTY_POINT_GEOMETRY_SUPPORTED = 101,

        RTC_DEVICE_PROPERTY_TASKING_SYSTEM = 128,
        RTC_DEVICE_PROPERTY_JOIN_COMMIT_SUPPORTED = 129,
        RTC_DEVICE_PROPERTY_PARALLEL_COMMIT_SUPPORTED = 130,

        RTC_DEVICE_PROPERTY_CPU_DEVICE = 140,
        RTC_DEVICE_PROPERTY_SYCL_DEVICE = 141
    }

    public static unsafe partial class EmbreeNative
    {
        /// <summary>
        /// Gets a device property.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        [return: NativeType("ssize_t")]
        public static partial nint rtcGetDeviceProperty(RTCDevice device, RTCDeviceProperty prop);
        /// <summary>
        /// Sets a device property.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetDeviceProperty(RTCDevice device, RTCDeviceProperty prop, [NativeType("ssize_t")] nint value);
    }

    /// <summary>
    /// Error codes
    /// </summary>
    public enum RTCError
    {
        RTC_ERROR_NONE = 0,
        RTC_ERROR_UNKNOWN = 1,
        RTC_ERROR_INVALID_ARGUMENT = 2,
        RTC_ERROR_INVALID_OPERATION = 3,
        RTC_ERROR_OUT_OF_MEMORY = 4,
        RTC_ERROR_UNSUPPORTED_CPU = 5,
        RTC_ERROR_CANCELLED = 6,
        RTC_ERROR_LEVEL_ZERO_RAYTRACING_SUPPORT_MISSING = 7,
    }

    public static unsafe partial class EmbreeNative
    {
        /// <summary>
        /// Returns the string representation for the error code. For example, for RTC_ERROR_UNKNOWN the string "RTC_ERROR_UNKNOWN" will be returned.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial byte* rtcGetErrorString(RTCError error);

        /// <summary>
        /// Returns the error code.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial RTCError rtcGetDeviceError(RTCDevice device);

        /// <summary>
        /// Returns a message corresponding to the last error code (returned by rtcGetDeviceError) which provides details about the error that happened.
        /// The same message will be written to console when verbosity is > 0 or when an error callback function is set for the device.
        /// However, when device creation itself fails this is the only way to get additional information about the error.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial byte* rtcGetDeviceLastErrorMessage(RTCDevice device);
    }

    /// <summary>
    /// Error callback function
    /// </summary>
    public unsafe delegate void RTCErrorFunction(void* userPtr, RTCError code, [NativeType("const char*")] byte* str);

    public static unsafe partial class EmbreeNative
    {
        /// <summary>
        /// Sets the error callback function.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetDeviceErrorFunction(RTCDevice device, RTCErrorFunction? error, void* userPtr);
    }

    /// <summary>
    /// Memory monitor callback function
    /// </summary>
    [return: MarshalAs(UnmanagedType.I1)]
    public unsafe delegate bool RTCMemoryMonitorFunction(void* ptr, [NativeType("ssize_t")] nint bytes, [MarshalAs(UnmanagedType.I1)] bool post);

    public static unsafe partial class EmbreeNative
    {
        /// <summary>
        /// Sets the memory monitor callback function.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetDeviceMemoryMonitorFunction(RTCDevice device, RTCMemoryMonitorFunction? memoryMonitor, void* userPtr);
    }
}
