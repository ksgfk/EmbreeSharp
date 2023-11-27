using System;
using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    public struct RTCBuffer
    {
        public IntPtr Ptr;
    }

    /// <summary>
    /// Types of buffers
    /// </summary>
    public enum RTCBufferType
    {
        RTC_BUFFER_TYPE_INDEX = 0,
        RTC_BUFFER_TYPE_VERTEX = 1,
        RTC_BUFFER_TYPE_VERTEX_ATTRIBUTE = 2,
        RTC_BUFFER_TYPE_NORMAL = 3,
        RTC_BUFFER_TYPE_TANGENT = 4,
        RTC_BUFFER_TYPE_NORMAL_DERIVATIVE = 5,

        RTC_BUFFER_TYPE_GRID = 8,

        RTC_BUFFER_TYPE_FACE = 16,
        RTC_BUFFER_TYPE_LEVEL = 17,
        RTC_BUFFER_TYPE_EDGE_CREASE_INDEX = 18,
        RTC_BUFFER_TYPE_EDGE_CREASE_WEIGHT = 19,
        RTC_BUFFER_TYPE_VERTEX_CREASE_INDEX = 20,
        RTC_BUFFER_TYPE_VERTEX_CREASE_WEIGHT = 21,
        RTC_BUFFER_TYPE_HOLE = 22,

        RTC_BUFFER_TYPE_TRANSFORM = 23,

        RTC_BUFFER_TYPE_FLAGS = 32
    }

    public static unsafe partial class GlobalFunctions
    {
        /// <summary>
        /// Creates a new buffer.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern RTCBuffer rtcNewBuffer(RTCDevice device, [NativeType("size_t")] IntPtr byteSize);
        /// <summary>
        /// Creates a new shared buffer.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern RTCBuffer rtcNewSharedBuffer(RTCDevice device, void* ptr, [NativeType("size_t")] IntPtr byteSize);
        /// <summary>
        /// Returns a pointer to the buffer data.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void* rtcGetBufferData(RTCBuffer buffer);
        /// <summary>
        /// Retains the buffer (increments the reference count).
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcRetainBuffer(RTCBuffer buffer);
        /// <summary>
        /// Releases the buffer (decrements the reference count).
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcReleaseBuffer(RTCBuffer buffer);
    }
}
