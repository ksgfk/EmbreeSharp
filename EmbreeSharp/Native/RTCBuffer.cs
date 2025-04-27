using System;
using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    public struct RTCBuffer
    {
        public nint Ptr;
    }

    public class RTCBufferHandle : SafeHandle
    {
        public override bool IsInvalid => handle == nint.Zero;
        public RTCBufferHandle(RTCBuffer buffer) : base(0, true)
        {
            handle = buffer.Ptr;
        }

        protected override bool ReleaseHandle()
        {
            try
            {
                EmbreeNative.rtcReleaseBuffer(new RTCBuffer() { Ptr = handle });
                handle = 0;
                return true;
            }
            catch (Exception e)
            {
                Console.Error.WriteLine("cannot release RTCBuffer, exception: {0}\n  at:\n{1}", e.Message, e.StackTrace);
                return false;
            }
        }
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

    public static unsafe partial class EmbreeNative
    {
        /// <summary>
        /// Creates a new buffer.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial RTCBuffer rtcNewBuffer(RTCDevice device, [NativeType("size_t")] nuint byteSize);
        /// <summary>
        /// Creates a new buffer using explicit host device memory.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial RTCBuffer rtcNewBufferHostDevice(RTCDevice device, [NativeType("size_t")] nuint byteSize);
        /// <summary>
        /// Creates a new shared buffer.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial RTCBuffer rtcNewSharedBuffer(RTCDevice device, void* ptr, [NativeType("size_t")] nuint byteSize);
        /// <summary>
        /// Creates a new shared buffer using explicit host device memory.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial RTCBuffer rtcNewSharedBufferHostDevice(RTCDevice device, void* ptr, [NativeType("size_t")] nuint byteSize);
        /// <summary>
        /// Synchronize host and device memory by copying data from host to device.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcCommitBuffer(RTCBuffer buffer);
        /// <summary>
        /// Returns a pointer to the buffer data.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void* rtcGetBufferData(RTCBuffer buffer);
        /// <summary>
        /// Returns a pointer to the buffer data on the device. Returns the same pointer as
        /// rtcGetBufferData if the device is no SYCL device or if Embree is executed on a
        /// system with unified memory (e.g., iGPUs).
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void* rtcGetBufferDataDevice(RTCBuffer buffer);
        /// <summary>
        /// Retains the buffer (increments the reference count).
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcRetainBuffer(RTCBuffer buffer);
        /// <summary>
        /// Releases the buffer (decrements the reference count).
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcReleaseBuffer(RTCBuffer buffer);
    }
}
