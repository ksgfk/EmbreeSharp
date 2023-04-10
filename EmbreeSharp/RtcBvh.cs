using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using EmbreeSharp.Native;
using static EmbreeSharp.Native.EmbreeNative;

namespace EmbreeSharp;

public readonly struct BvhNodeView<TNode>
{
    readonly unsafe void* _ptr;

    public ref TNode Node
    {
        get
        {
            unsafe
            {
                return ref Unsafe.AsRef<TNode>(_ptr);
            }
        }
    }
}

public delegate ref TNode RtcCreateNodeCallback<TNode>(RTCThreadLocalAllocator allocator, uint childCount);
public delegate void RtcSetNodeChildrenCallback<TNode>(ref TNode node, ReadOnlySpan<BvhNodeView<TNode>> children);
public delegate void RtcSetNodeBoundsCallback<TNode>(ref TNode node, ReadOnlySpan<RTCBounds> bounds);
public delegate ref TLeaf RtcCreateLeafCallback<TLeaf>(RTCThreadLocalAllocator allocator, ReadOnlySpan<RTCBuildPrimitive> primitives);
public delegate void RtcSplitPrimitiveCallback(in RTCBuildPrimitive primitive, uint dimension, float position, ref RTCBounds leftBounds, ref RTCBounds rightBounds);
public delegate bool RtcProgressMonitorFunCallback(double n);

public static class RtcThreadLocalAllocator
{
    public static ref T Alloc<T>(RTCThreadLocalAllocator allocator, int align) where T : struct
    {
        if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
        {
            ThrowInvalidOperation($"{typeof(T).FullName} is reference type");
        }
        unsafe
        {
            void* ptr = rtcThreadLocalAlloc(allocator, (nuint)Unsafe.SizeOf<T>(), (nuint)align);
            return ref Unsafe.AsRef<T>(ptr);
        }
    }
}

public class RtcBvh<TNode, TLeaf> : IDisposable where TNode : struct where TLeaf : struct
{
    RTCBVH _bvh;
    bool _disposedValue;

    public RTCBuildQuality BuildQuality { get; set; } = RTCBuildQuality.RTC_BUILD_QUALITY_MEDIUM;
    public RTCBuildFlags BuildFlags { get; set; } = RTCBuildFlags.RTC_BUILD_FLAG_NONE;
    public int MaxBranchingFactor { get; set; } = 2;
    public int MaxDepth { get; set; } = 32;
    public int SahBlockSize { get; set; } = 1;
    public int MinLeafSize { get; set; } = 1;
    public int MaxLeafSize { get; set; } = (int)RTCBuildConstants.RTC_BUILD_MAX_PRIMITIVES_PER_LEAF;
    public float TraversalCost { get; set; } = 1.0f;
    public float IntersectionCost { get; set; } = 1.0f;
    public RtcCreateNodeCallback<TNode>? CreateNode { get; set; }
    public RtcSetNodeChildrenCallback<TNode>? SetNodeChildren { get; set; }
    public RtcSetNodeBoundsCallback<TNode>? SetNodeBounds { get; set; }
    public RtcCreateLeafCallback<TLeaf>? CreateLeaf { get; set; }
    public RtcSplitPrimitiveCallback? SplitPrimitive { get; set; }
    public RtcProgressMonitorFunCallback? BuildProgress { get; set; }

    public RtcBvh(RTCDevice device)
    {
        if (RuntimeHelpers.IsReferenceOrContainsReferences<TNode>())
        {
            ThrowInvalidOperation($"{typeof(TNode).FullName} is reference type");
        }
        if (RuntimeHelpers.IsReferenceOrContainsReferences<TLeaf>())
        {
            ThrowInvalidOperation($"{typeof(TLeaf).FullName} is reference type");
        }
        _bvh = rtcNewBVH(device);
    }

    public RtcBvh(RtcDevice device) : this(device.NativeHandler) { }

    ~RtcBvh()
    {
        Dispose(disposing: false);
    }

    public ref TLeaf Build(RTCBuildPrimitive[] primitives)
    {
        RTCBuildPrimitive[] prims;
        if (BuildQuality == RTCBuildQuality.RTC_BUILD_QUALITY_HIGH)
        {
            RTCBuildPrimitive[] temp = new RTCBuildPrimitive[primitives.LongLength * 2];
            primitives.CopyTo(temp, 0);
            prims = temp;
        }
        else
        {
            prims = primitives;
        }
        unsafe
        {
            fixed (RTCBuildPrimitive* ptr = prims)
            {
                RTCCreateNodeFunction? ctorNd = null;
                if (CreateNode != null)
                {
                    ctorNd = (RTCThreadLocalAllocator allocator, uint childCount, void* userPtr) =>
                    {
                        ref TNode node = ref CreateNode(allocator, childCount);
                        return Unsafe.AsPointer(ref node);
                    };
                }
                RTCSetNodeChildrenFunction? setNdCh = null;
                if (SetNodeChildren != null)
                {
                    setNdCh = (void* nodePtr, void** children, uint childCount, void* userPtr) =>
                    {
                        ref TNode node = ref Unsafe.AsRef<TNode>(nodePtr);
                        ReadOnlySpan<BvhNodeView<TNode>> c = new(children, (int)childCount);
                        SetNodeChildren(ref node, c);
                    };
                }
                RTCSetNodeBoundsFunction? setNdBd = null;
                if (SetNodeBounds != null)
                {
                    setNdBd = (void* nodePtr, RTCBounds** bounds, uint childCount, void* userPtr) =>
                    {
                        ref TNode node = ref Unsafe.AsRef<TNode>(nodePtr);
                        ReadOnlySpan<RTCBounds> b = new(bounds, (int)childCount);
                        SetNodeBounds(ref node, b);
                    };
                }
                RTCCreateLeafFunction? ctorLf = null;
                if (CreateLeaf != null)
                {
                    ctorLf = (RTCThreadLocalAllocator allocator, RTCBuildPrimitive* primitives, nuint primitiveCount, void* userPtr) =>
                    {
                        ReadOnlySpan<RTCBuildPrimitive> b = new(primitives, (int)primitiveCount);
                        ref TLeaf leaf = ref CreateLeaf(allocator, b);
                        return Unsafe.AsPointer(ref leaf);
                    };
                }
                RTCSplitPrimitiveFunction? spPrim = null;
                if (SplitPrimitive != null)
                {
                    spPrim = (RTCBuildPrimitive* primitive, uint dimension, float position, RTCBounds* leftBounds, RTCBounds* rightBounds, void* userPtr) =>
                    {
                        ref RTCBuildPrimitive prim = ref Unsafe.AsRef<RTCBuildPrimitive>(primitive);
                        ref RTCBounds lb = ref Unsafe.AsRef<RTCBounds>(leftBounds);
                        ref RTCBounds rb = ref Unsafe.AsRef<RTCBounds>(rightBounds);
                        SplitPrimitive(in prim, dimension, position, ref lb, ref rb);
                    };
                }
                RTCProgressMonitorFunction? progress = null;
                if (BuildProgress != null)
                {
                    progress = (void* ptr, double n) =>
                    {
                        return BuildProgress(n);
                    };
                }
                RTCBuildArguments args = rtcDefaultBuildArguments();
                args.buildQuality = BuildQuality;
                args.buildFlags = BuildFlags;
                args.maxBranchingFactor = (uint)MaxBranchingFactor;
                args.maxDepth = (uint)MaxDepth;
                args.sahBlockSize = (uint)SahBlockSize;
                args.minLeafSize = (uint)MinLeafSize;
                args.maxLeafSize = (uint)MaxLeafSize;
                args.traversalCost = TraversalCost;
                args.intersectionCost = IntersectionCost;
                args.bvh = _bvh;
                args.primitives = ptr;
                args.primitiveCount = (nuint)primitives.LongLength;
                args.primitiveArrayCapacity = (nuint)prims.LongLength;
                args.createNode = ctorNd == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(ctorNd);
                args.setNodeChildren = setNdCh == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(setNdCh);
                args.setNodeBounds = setNdBd == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(setNdBd);
                args.createLeaf = ctorLf == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(ctorLf);
                args.splitPrimitive = spPrim == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(spPrim);
                args.buildProgress = progress == null ? IntPtr.Zero : Marshal.GetFunctionPointerForDelegate(progress);
                void* root = rtcBuildBVH(&args);
                return ref Unsafe.AsRef<TLeaf>(root);
            }
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            rtcReleaseBVH(_bvh);
            _bvh = default;

            _disposedValue = true;
        }
    }

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
