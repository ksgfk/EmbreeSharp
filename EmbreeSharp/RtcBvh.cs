using EmbreeSharp.Native;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static EmbreeSharp.Native.EmbreeNative;

namespace EmbreeSharp;

public readonly struct BvhBoundRef
{
    readonly unsafe void* _ptr;
    public unsafe ref RTCBounds Bound => ref Unsafe.AsRef<RTCBounds>(_ptr);
    unsafe internal BvhBoundRef(void* ptr)
    {
        _ptr = ptr;
    }
}

public readonly struct BvhNodeRef
{
    readonly unsafe void* _ptr;
    public unsafe IntPtr Ptr => new(_ptr);
    unsafe internal BvhNodeRef(void* ptr)
    {
        _ptr = ptr;
    }

    public ref TNode Read<TNode>() where TNode : unmanaged
    {
        unsafe
        {
            return ref Unsafe.AsRef<TNode>(_ptr);
        }
    }
}

public delegate ref TNode RtcCreateNodeCallback<TNode>(RTCThreadLocalAllocator allocator, uint childCount);
public delegate void RtcSetNodeChildrenCallback<TNode>(ref TNode node, ReadOnlySpan<BvhNodeRef> children);
public delegate void RtcSetNodeBoundsCallback<TNode>(ref TNode node, ReadOnlySpan<BvhBoundRef> bounds);
public delegate ref TLeaf RtcCreateLeafCallback<TLeaf>(RTCThreadLocalAllocator allocator, ReadOnlySpan<RTCBuildPrimitive> primitives);
public delegate void RtcSplitPrimitiveCallback(in RTCBuildPrimitive primitive, uint dimension, float position, ref RTCBounds leftBounds, ref RTCBounds rightBounds);
public delegate bool RtcProgressMonitorFunCallback(double n);

public static class RtcThreadLocalAllocatorExtension
{
    public static ref T Alloc<T>(this RTCThreadLocalAllocator allocator, int align) where T : unmanaged
    {
        unsafe
        {
            void* ptr = rtcThreadLocalAlloc(allocator, (nuint)Unsafe.SizeOf<T>(), (nuint)align);
            return ref Unsafe.AsRef<T>(ptr);
        }
    }

    public static Span<T> Alloc<T>(this RTCThreadLocalAllocator allocator, int count, int align) where T : unmanaged
    {
        unsafe
        {
            void* ptr = rtcThreadLocalAlloc(allocator, (nuint)Unsafe.SizeOf<T>() * (nuint)count, (nuint)align);
            return new Span<T>(ptr, count);
        }
    }

    public static Span<byte> Alloc(this RTCThreadLocalAllocator allocator, int size, int align)
    {
        unsafe
        {
            void* ptr = rtcThreadLocalAlloc(allocator, (nuint)size, (nuint)align);
            return new Span<byte>(ptr, size);
        }
    }
}

public class RtcBvh<TNode, TLeaf> : IDisposable where TNode : unmanaged where TLeaf : unmanaged
{
    RTCBVH _bvh;
    IntPtr _buildResult;
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

    public RTCBVH NativeHandler => _bvh;
    public ref TNode Root
    {
        get
        {
            if (_buildResult == IntPtr.Zero)
            {
                ThrowInvalidOperation("BVH has not built");
            }
            unsafe
            {
                return ref Unsafe.AsRef<TNode>(_buildResult.ToPointer());
            }
        }
    }

    public RtcBvh(RTCDevice device)
    {
        _bvh = rtcNewBVH(device);
    }

    public RtcBvh(RtcDevice device) : this(device.NativeHandler) { }

    ~RtcBvh()
    {
        Dispose(disposing: false);
    }

    public ref TNode Build(RTCBuildPrimitive[] primitives)
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
                        ReadOnlySpan<BvhNodeRef> c = new(children, (int)childCount);
                        SetNodeChildren(ref node, c);
                    };
                }
                RTCSetNodeBoundsFunction? setNdBd = null;
                if (SetNodeBounds != null)
                {
                    setNdBd = (void* nodePtr, RTCBounds** bounds, uint childCount, void* userPtr) =>
                    {
                        ref TNode node = ref Unsafe.AsRef<TNode>(nodePtr);
                        ReadOnlySpan<BvhBoundRef> b = new(bounds, (int)childCount);
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
                _buildResult = new IntPtr(root);
                return ref Root;
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
