using System;
using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    public struct RTCBVH
    {
        public static RTCBVH Null => new() { Ptr = nint.Zero };

        public IntPtr Ptr;
    }

    /// <summary>
    /// Input build primitives for the builder
    /// </summary>
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential)]
    public struct RTCBuildPrimitive
    {
        public const int Alignment = 32;

        public float lower_x, lower_y, lower_z;
        public uint geomID;
        public float upper_x, upper_y, upper_z;
        public uint primID;
    }

    public struct RTCThreadLocalAllocator
    {
        public IntPtr Ptr;
    }

    /// <summary>
    /// Callback to create a node
    /// </summary>
    public unsafe delegate void* RTCCreateNodeFunction(RTCThreadLocalAllocator allocator, uint childCount, void* userPtr);
    /// <summary>
    /// Callback to set the pointer to all children
    /// </summary>
    public unsafe delegate void RTCSetNodeChildrenFunction(void* nodePtr, void** children, uint childCount, void* userPtr);
    /// <summary>
    /// Callback to set the bounds of all children
    /// </summary>
    public unsafe delegate void RTCSetNodeBoundsFunction(void* nodePtr, [NativeType("const struct RTCBounds**")] RTCBounds** bounds, uint childCount, void* userPtr);
    /// <summary>
    /// Callback to create a leaf node
    /// </summary>
    public unsafe delegate void* RTCCreateLeafFunction(RTCThreadLocalAllocator allocator, [NativeType("const struct RTCBuildPrimitive*")] RTCBuildPrimitive* primitives, [NativeType("size_t")] nuint primitiveCount, void* userPtr);
    /// <summary>
    /// Callback to split a build primitive
    /// </summary>
    public unsafe delegate void RTCSplitPrimitiveFunction([NativeType("const struct RTCBuildPrimitive*")] RTCBuildPrimitive* primitive, uint dimension, float position, [NativeType("struct RTCBounds*")] RTCBounds* leftBounds, [NativeType("struct RTCBounds*")] RTCBounds* rightBounds, void* userPtr);


    /// <summary>
    /// Build flags
    /// </summary>
    [Flags]
    public enum RTCBuildFlags
    {
        RTC_BUILD_FLAG_NONE = 0,
        RTC_BUILD_FLAG_DYNAMIC = (1 << 0),
    }

    public enum RTCBuildConstants
    {
        RTC_BUILD_MAX_PRIMITIVES_PER_LEAF = 32
    }

    /// <summary>
    /// Input for builders
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RTCBuildArguments
    {
        [NativeType("size_t")] public nuint byteSize;

        public RTCBuildQuality buildQuality;
        public RTCBuildFlags buildFlags;
        public uint maxBranchingFactor;
        public uint maxDepth;
        public uint sahBlockSize;
        public uint minLeafSize;
        public uint maxLeafSize;
        public float traversalCost;
        public float intersectionCost;

        public RTCBVH bvh;
        [NativeType("struct RTCBuildPrimitive*")] public RTCBuildPrimitive* primitives;
        [NativeType("size_t")] public nuint primitiveCount;
        [NativeType("size_t")] public nuint primitiveArrayCapacity;

        [NativeType("RTCCreateNodeFunction")] public IntPtr createNode;
        [NativeType("RTCSetNodeChildrenFunction")] public IntPtr setNodeChildren;
        [NativeType("RTCSetNodeBoundsFunction")] public IntPtr setNodeBounds;
        [NativeType("RTCCreateLeafFunction")] public IntPtr createLeaf;
        [NativeType("RTCSplitPrimitiveFunction")] public IntPtr splitPrimitive;
        [NativeType("RTCProgressMonitorFunction")] public IntPtr buildProgress;
        public void* userPtr;
    }

    public static unsafe partial class GlobalFunctions
    {
        /// <summary>
        /// Creates a new BVH.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern RTCBVH rtcNewBVH(RTCDevice device);
        /// <summary>
        /// Builds a BVH.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void* rtcBuildBVH([NativeType("const struct RTCBuildArguments*")] RTCBuildArguments* args);
        /// <summary>
        /// Allocates memory using the thread local allocator.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void* rtcThreadLocalAlloc(RTCThreadLocalAllocator allocator, [NativeType("size_t")] nuint bytes, [NativeType("size_t")] nuint align);
        /// <summary>
        /// Retains the BVH (increments reference count).
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcRetainBVH(RTCBVH bvh);
        /// <summary>
        /// Releases the BVH (decrements reference count).
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcReleaseBVH(RTCBVH bvh);
    }
}
