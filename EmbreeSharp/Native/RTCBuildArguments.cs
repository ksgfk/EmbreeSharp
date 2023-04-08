using System;
using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct RTCBuildArguments
{
    [NativeTypeName("size_t")]
    public nuint byteSize;

    [NativeTypeName("enum RTCBuildQuality")]
    public RTCBuildQuality buildQuality;

    [NativeTypeName("enum RTCBuildFlags")]
    public RTCBuildFlags buildFlags;

    [NativeTypeName("unsigned int")]
    public uint maxBranchingFactor;

    [NativeTypeName("unsigned int")]
    public uint maxDepth;

    [NativeTypeName("unsigned int")]
    public uint sahBlockSize;

    [NativeTypeName("unsigned int")]
    public uint minLeafSize;

    [NativeTypeName("unsigned int")]
    public uint maxLeafSize;

    public float traversalCost;

    public float intersectionCost;

    [NativeTypeName("RTCBVH")]
    public RTCBVH bvh;

    [NativeTypeName("struct RTCBuildPrimitive *")]
    public RTCBuildPrimitive* primitives;

    [NativeTypeName("size_t")]
    public nuint primitiveCount;

    [NativeTypeName("size_t")]
    public nuint primitiveArrayCapacity;

    [NativeTypeName("RTCCreateNodeFunction")]
    public IntPtr createNode;

    [NativeTypeName("RTCSetNodeChildrenFunction")]
    public IntPtr setNodeChildren;

    [NativeTypeName("RTCSetNodeBoundsFunction")]
    public IntPtr setNodeBounds;

    [NativeTypeName("RTCCreateLeafFunction")]
    public IntPtr createLeaf;

    [NativeTypeName("RTCSplitPrimitiveFunction")]
    public IntPtr splitPrimitive;

    [NativeTypeName("RTCProgressMonitorFunction")]
    public IntPtr buildProgress;

    public void* userPtr;
}