using System;
using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    public struct RTCBuffer
    {
        public IntPtr Ptr;
    }

    public static unsafe partial class Embree
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
