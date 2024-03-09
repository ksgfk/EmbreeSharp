using EmbreeSharp.Native;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace EmbreeSharp
{
    public delegate RtcThreadLocalAllocation CreateNodeFunction(RTCThreadLocalAllocator allocator, uint childCount);
    public delegate ref T CreateNodeFunction<T>(RTCThreadLocalAllocator allocator, uint childCount) where T : unmanaged;

    public delegate void SetNodeChildrenFunction(RtcThreadLocalAllocation node, NativeMemoryView<RtcThreadLocalAllocation> children);
    public delegate void SetNodeChildrenFunction<T>(ref T node, NativeMemoryView<Ref<T>> children) where T : unmanaged;

    public delegate void SetNodeBoundsFunction(RtcThreadLocalAllocation node, NativeMemoryView<Ref<RTCBounds>> bounds);
    public delegate void SetNodeBoundsFunction<T>(ref T node, NativeMemoryView<Ref<RTCBounds>> bounds) where T : unmanaged;

    public delegate RtcThreadLocalAllocation CreateLeafFunction(RTCThreadLocalAllocator allocator, NativeMemoryView<RTCBuildPrimitive> primitives);
    public delegate ref T CreateLeafFunction<T>(RTCThreadLocalAllocator allocator, NativeMemoryView<RTCBuildPrimitive> primitives) where T : unmanaged;

    public delegate void SplitPrimitiveFunction(ref readonly RTCBuildPrimitive primitive, uint dimension, float position, ref RTCBounds leftBounds, ref RTCBounds rightBounds);

    public class RtcBuilder : IDisposable
    {
        private bool _disposedValue;

        private GCHandle _gcHandle;
        private RTCBVH _bvh;
        private RtcThreadLocalAllocation _result;

        private RTCBuildQuality _buildQuality;
        private RTCBuildFlags _buildFlags;
        private uint _maxBranchingFactor;
        private uint _maxDepth;
        private uint _sahBlockSize;
        private uint _minLeafSize;
        private uint _maxLeafSize;
        private float _traversalCost;
        private float _intersectionCost;
        private RTCBuildPrimitive[]? _prims;
        private CreateNodeFunction? _createNode;
        private SetNodeChildrenFunction? _setNodeChildren;
        private SetNodeBoundsFunction? _setNodeBounds;
        private CreateLeafFunction? _createLeaf;
        private SplitPrimitiveFunction? _splitPrimitive;
        private ProgressMonitorFunction? _progressMonitor;

        private RTCCreateNodeFunction? _nativeCreateNode;
        private RTCSetNodeChildrenFunction? _nativeSetNodeChildren;
        private RTCSetNodeBoundsFunction? _nativeSetNodeBounds;
        private RTCCreateLeafFunction? _nativeCreateLeaf;
        private RTCSplitPrimitiveFunction? _nativeSplitPrimitive;
        private RTCProgressMonitorFunction? _nativeProgressMonitor;

        public bool IsDisposed => _disposedValue;
        public RTCBVH NativeBVH => _bvh;
        public RtcThreadLocalAllocation Result => _result;

        public RtcBuilder(RtcDevice device)
        {
            _gcHandle = GCHandle.Alloc(this, GCHandleType.Weak);
            _bvh = GlobalFunctions.rtcNewBVH(device.NativeDevice);
            _buildQuality = RTCBuildQuality.RTC_BUILD_QUALITY_MEDIUM;
            _buildFlags = RTCBuildFlags.RTC_BUILD_FLAG_NONE;
            _maxBranchingFactor = 2;
            _maxDepth = 32;
            _sahBlockSize = 1;
            _minLeafSize = 1;
            _maxLeafSize = (uint)RTCBuildConstants.RTC_BUILD_MAX_PRIMITIVES_PER_LEAF;
            _traversalCost = 1.0f;
            _intersectionCost = 1.0f;
        }

        public RtcBuilder(RtcBuilder other)
        {
            if (other.IsDisposed)
            {
                ThrowUtility.ObjectDisposed(nameof(other));
            }
            _gcHandle = GCHandle.Alloc(this, GCHandleType.Weak);
            GlobalFunctions.rtcRetainBVH(other._bvh);
            _bvh = other._bvh;
            _buildQuality = other._buildQuality;
            _buildFlags = other._buildFlags;
            _maxBranchingFactor = other._maxBranchingFactor;
            _maxDepth = other._maxDepth;
            _sahBlockSize = other._sahBlockSize;
            _minLeafSize = other._minLeafSize;
            _maxLeafSize = other._maxLeafSize;
            _traversalCost = other._traversalCost;
            _intersectionCost = other._intersectionCost;
            _prims = other._prims;
            _createNode = other._createNode;
            _setNodeChildren = other._setNodeChildren;
            _setNodeBounds = other._setNodeBounds;
            _createLeaf = other._createLeaf;
            _splitPrimitive = other._splitPrimitive;
            _progressMonitor = other._progressMonitor;
        }

        ~RtcBuilder()
        {
            Dispose(disposing: false);
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _prims = null;
                    _createNode = null;
                    _setNodeChildren = null;
                    _setNodeBounds = null;
                    _createLeaf = null;
                    _splitPrimitive = null;
                    _progressMonitor = null;
                    _nativeCreateNode = null;
                    _nativeSetNodeChildren = null;
                    _nativeSetNodeBounds = null;
                    _nativeCreateLeaf = null;
                    _nativeSplitPrimitive = null;
                    _nativeProgressMonitor = null;
                }
                _gcHandle.Free();
                _gcHandle = default;
                GlobalFunctions.rtcReleaseBVH(_bvh);
                _bvh = default;
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public RtcBuilder SetBuildQuality(RTCBuildQuality quality)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _buildQuality = quality;
            return this;
        }

        public RtcBuilder SetBuildFlag(RTCBuildFlags flags)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _buildFlags = flags;
            return this;
        }

        public RtcBuilder SetMaxBranchingFactor(uint value)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _maxBranchingFactor = value;
            return this;
        }

        public RtcBuilder SetMaxDepth(uint value)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _maxDepth = value;
            return this;
        }

        public RtcBuilder SetSahBlockSize(uint value)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _sahBlockSize = value;
            return this;
        }

        public RtcBuilder SetMinLeafSize(uint value)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _minLeafSize = value;
            return this;
        }

        public RtcBuilder SetMaxLeafSize(uint value)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _maxLeafSize = value;
            return this;
        }

        public RtcBuilder SetTraversalCost(float value)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _traversalCost = value;
            return this;
        }

        public RtcBuilder SetIntersectionCost(float value)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _intersectionCost = value;
            return this;
        }

        public RtcBuilder SetBuildPrimitive(RTCBuildPrimitive[] prims)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _prims = prims;
            return this;
        }

        public RtcBuilder SetBuildPrimitive(Span<RTCBuildPrimitive> prims)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _prims = prims.ToArray();
            return this;
        }

        public RtcBuilder SetCreateNodeFunction(CreateNodeFunction func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _createNode = func;
            return this;
        }

        public RtcBuilder SetSetNodeChildrenFunction(SetNodeChildrenFunction func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _setNodeChildren = func;
            return this;
        }

        public RtcBuilder SetSetNodeBoundsFunction(SetNodeBoundsFunction func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _setNodeBounds = func;
            return this;
        }

        public RtcBuilder SetCreateLeafFunction(CreateLeafFunction func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _createLeaf = func;
            return this;
        }

        public RtcBuilder SetSplitPrimitiveFunction(SplitPrimitiveFunction func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _splitPrimitive = func;
            return this;
        }

        public RtcBuilder SetProgressMonitorFunction(ProgressMonitorFunction func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _progressMonitor = func;
            return this;
        }

        private static unsafe RtcBuilder? GetThisFromUserPtr(void* userPtr)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(new nint(userPtr));
            if (!gcHandle.IsAllocated)
            {
                return null;
            }
            RtcBuilder builder = (RtcBuilder)gcHandle.Target!;
            return builder;
        }

        private static unsafe void* CreateNodeFunctionImpl(RTCThreadLocalAllocator allocator, uint childCount, void* userPtr)
        {
            RtcBuilder? builder = GetThisFromUserPtr(userPtr);
            if (builder == null)
            {
                return null;
            }
            RtcThreadLocalAllocation allocation = builder._createNode?.Invoke(allocator, childCount) ?? default;
            return allocation.Ptr;
        }

        private static unsafe void SetNodeChildrenImpl(void* nodePtr, void** children, uint childCount, void* userPtr)
        {
            RtcBuilder? builder = GetThisFromUserPtr(userPtr);
            if (builder == null)
            {
                return;
            }
            RtcThreadLocalAllocation node = new(nodePtr);
            NativeMemoryView<RtcThreadLocalAllocation> childs = new(children, childCount);
            builder._setNodeChildren?.Invoke(node, childs);
        }

        private static unsafe void SetNodeBoundsImpl(void* nodePtr, RTCBounds** bounds, uint childCount, void* userPtr)
        {
            RtcBuilder? builder = GetThisFromUserPtr(userPtr);
            if (builder == null)
            {
                return;
            }
            RtcThreadLocalAllocation node = new(nodePtr);
            NativeMemoryView<Ref<RTCBounds>> bound = new(bounds, childCount);
            builder._setNodeBounds?.Invoke(node, bound);
        }

        private static unsafe void* CreateLeafImpl(RTCThreadLocalAllocator allocator, RTCBuildPrimitive* primitives, nuint primitiveCount, void* userPtr)
        {
            RtcBuilder? builder = GetThisFromUserPtr(userPtr);
            if (builder == null)
            {
                return null;
            }
            NativeMemoryView<RTCBuildPrimitive> prims = new(primitives, primitiveCount);
            RtcThreadLocalAllocation leaf = builder._createLeaf?.Invoke(allocator, prims) ?? default;
            return leaf.Ptr;
        }

        private static unsafe void SplitPrimitiveImpl(RTCBuildPrimitive* primitive, uint dimension, float position, RTCBounds* leftBounds, RTCBounds* rightBounds, void* userPtr)
        {
            RtcBuilder? builder = GetThisFromUserPtr(userPtr);
            if (builder == null)
            {
                return;
            }
            builder._splitPrimitive?.Invoke(ref Unsafe.AsRef<RTCBuildPrimitive>(primitive), dimension, position, ref Unsafe.AsRef<RTCBounds>(leftBounds), ref Unsafe.AsRef<RTCBounds>(rightBounds));
        }

        private static unsafe bool ProgressMonitorImpl(void* ptr, double n)
        {
            RtcBuilder? builder = GetThisFromUserPtr(ptr);
            if (builder == null)
            {
                return true;
            }
            return builder._progressMonitor?.Invoke(n) ?? true;
        }

        public unsafe RtcThreadLocalAllocation Build()
        {
            if (IsDisposed) { ThrowUtility.ObjectDisposed(); }
            if (_prims == null) { ThrowUtility.InvalidOperation("primitives cannot be null"); }
            _nativeCreateNode = CreateNodeFunctionImpl;
            _nativeSetNodeChildren = SetNodeChildrenImpl;
            _nativeSetNodeBounds = SetNodeBoundsImpl;
            _nativeCreateLeaf = CreateLeafImpl;
            _nativeSplitPrimitive = SplitPrimitiveImpl;
            _nativeProgressMonitor = ProgressMonitorImpl;
            RTCBuildPrimitive* primitives = null;
            nuint primitiveCount = (nuint)_prims.Length;
            nuint primitiveArrayCapacity = _buildQuality == RTCBuildQuality.RTC_BUILD_QUALITY_HIGH ? primitiveCount * 2 : primitiveCount;
            try
            {
                primitives = (RTCBuildPrimitive*)NativeMemory.AlignedAlloc((nuint)sizeof(RTCBuildPrimitive) * primitiveArrayCapacity, RTCBuildPrimitive.Alignment);
                {
                    NativeMemoryView<RTCBuildPrimitive> dst = new(primitives, primitiveCount);
                    dst.CopyFrom(_prims);
                }
                RTCBuildArguments args = new()
                {
                    byteSize = (nuint)sizeof(RTCBuildArguments),
                    buildQuality = _buildQuality,
                    buildFlags = _buildFlags,
                    maxBranchingFactor = _maxBranchingFactor,
                    maxDepth = _maxDepth,
                    sahBlockSize = _sahBlockSize,
                    minLeafSize = _minLeafSize,
                    maxLeafSize = _maxLeafSize,
                    traversalCost = _traversalCost,
                    intersectionCost = _intersectionCost,
                    bvh = _bvh,
                    primitives = primitives,
                    primitiveCount = primitiveCount,
                    primitiveArrayCapacity = primitiveArrayCapacity,
                    createNode = _createNode == null ? 0 : Marshal.GetFunctionPointerForDelegate(_nativeCreateNode),
                    setNodeChildren = _setNodeChildren == null ? 0 : Marshal.GetFunctionPointerForDelegate(_nativeSetNodeChildren),
                    setNodeBounds = _setNodeBounds == null ? 0 : Marshal.GetFunctionPointerForDelegate(_nativeSetNodeBounds),
                    createLeaf = _createLeaf == null ? 0 : Marshal.GetFunctionPointerForDelegate(_nativeCreateLeaf),
                    splitPrimitive = _splitPrimitive == null ? 0 : Marshal.GetFunctionPointerForDelegate(_nativeSplitPrimitive),
                    buildProgress = _progressMonitor == null ? 0 : Marshal.GetFunctionPointerForDelegate(_nativeProgressMonitor),
                    userPtr = GCHandle.ToIntPtr(_gcHandle).ToPointer()
                };
                void* root = GlobalFunctions.rtcBuildBVH(&args);
                _result = new RtcThreadLocalAllocation(root);
                return _result;
            }
            finally
            {
                if (_prims != null)
                {
                    NativeMemory.AlignedFree(primitives);
                    primitives = null;
                }
            }
        }
    }

    public class RtcBuilder<TNode, TLeaf> : IDisposable where TNode : unmanaged where TLeaf : unmanaged
    {
        private bool _disposedValue;

        private GCHandle _gcHandle;
        private RTCBVH _bvh;
        private RtcThreadLocalAllocation _result;

        private RTCBuildQuality _buildQuality;
        private RTCBuildFlags _buildFlags;
        private uint _maxBranchingFactor;
        private uint _maxDepth;
        private uint _sahBlockSize;
        private uint _minLeafSize;
        private uint _maxLeafSize;
        private float _traversalCost;
        private float _intersectionCost;
        private RTCBuildPrimitive[]? _prims;
        private CreateNodeFunction<TNode>? _createNode;
        private SetNodeChildrenFunction<TNode>? _setNodeChildren;
        private SetNodeBoundsFunction<TNode>? _setNodeBounds;
        private CreateLeafFunction<TLeaf>? _createLeaf;
        private SplitPrimitiveFunction? _splitPrimitive;
        private ProgressMonitorFunction? _progressMonitor;

        private RTCCreateNodeFunction? _nativeCreateNode;
        private RTCSetNodeChildrenFunction? _nativeSetNodeChildren;
        private RTCSetNodeBoundsFunction? _nativeSetNodeBounds;
        private RTCCreateLeafFunction? _nativeCreateLeaf;
        private RTCSplitPrimitiveFunction? _nativeSplitPrimitive;
        private RTCProgressMonitorFunction? _nativeProgressMonitor;

        public bool IsDisposed => _disposedValue;
        public RTCBVH NativeBVH => _bvh;
        public RtcThreadLocalAllocation Result => _result;

        public RtcBuilder(RtcDevice device)
        {
            _gcHandle = GCHandle.Alloc(this, GCHandleType.Weak);
            _bvh = GlobalFunctions.rtcNewBVH(device.NativeDevice);
            _buildQuality = RTCBuildQuality.RTC_BUILD_QUALITY_MEDIUM;
            _buildFlags = RTCBuildFlags.RTC_BUILD_FLAG_NONE;
            _maxBranchingFactor = 2;
            _maxDepth = 32;
            _sahBlockSize = 1;
            _minLeafSize = 1;
            _maxLeafSize = (uint)RTCBuildConstants.RTC_BUILD_MAX_PRIMITIVES_PER_LEAF;
            _traversalCost = 1.0f;
            _intersectionCost = 1.0f;
        }

        public RtcBuilder(RtcBuilder<TNode, TLeaf> other)
        {
            if (other.IsDisposed)
            {
                ThrowUtility.ObjectDisposed(nameof(other));
            }
            _gcHandle = GCHandle.Alloc(this, GCHandleType.Weak);
            GlobalFunctions.rtcRetainBVH(other._bvh);
            _bvh = other._bvh;
            _buildQuality = other._buildQuality;
            _buildFlags = other._buildFlags;
            _maxBranchingFactor = other._maxBranchingFactor;
            _maxDepth = other._maxDepth;
            _sahBlockSize = other._sahBlockSize;
            _minLeafSize = other._minLeafSize;
            _maxLeafSize = other._maxLeafSize;
            _traversalCost = other._traversalCost;
            _intersectionCost = other._intersectionCost;
            _prims = other._prims;
            _createNode = other._createNode;
            _setNodeChildren = other._setNodeChildren;
            _setNodeBounds = other._setNodeBounds;
            _createLeaf = other._createLeaf;
            _splitPrimitive = other._splitPrimitive;
            _progressMonitor = other._progressMonitor;
        }

        ~RtcBuilder()
        {
            Dispose(disposing: false);
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _prims = null;
                    _createNode = null;
                    _setNodeChildren = null;
                    _setNodeBounds = null;
                    _createLeaf = null;
                    _splitPrimitive = null;
                    _progressMonitor = null;
                    _nativeCreateNode = null;
                    _nativeSetNodeChildren = null;
                    _nativeSetNodeBounds = null;
                    _nativeCreateLeaf = null;
                    _nativeSplitPrimitive = null;
                    _nativeProgressMonitor = null;
                }
                _gcHandle.Free();
                _gcHandle = default;
                GlobalFunctions.rtcReleaseBVH(_bvh);
                _bvh = default;
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public RtcBuilder<TNode, TLeaf> SetBuildQuality(RTCBuildQuality quality)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _buildQuality = quality;
            return this;
        }

        public RtcBuilder<TNode, TLeaf> SetBuildFlag(RTCBuildFlags flags)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _buildFlags = flags;
            return this;
        }

        public RtcBuilder<TNode, TLeaf> SetMaxBranchingFactor(uint value)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _maxBranchingFactor = value;
            return this;
        }

        public RtcBuilder<TNode, TLeaf> SetMaxDepth(uint value)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _maxDepth = value;
            return this;
        }

        public RtcBuilder<TNode, TLeaf> SetSahBlockSize(uint value)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _sahBlockSize = value;
            return this;
        }

        public RtcBuilder<TNode, TLeaf> SetMinLeafSize(uint value)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _minLeafSize = value;
            return this;
        }

        public RtcBuilder<TNode, TLeaf> SetMaxLeafSize(uint value)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _maxLeafSize = value;
            return this;
        }

        public RtcBuilder<TNode, TLeaf> SetTraversalCost(float value)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _traversalCost = value;
            return this;
        }

        public RtcBuilder<TNode, TLeaf> SetIntersectionCost(float value)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _intersectionCost = value;
            return this;
        }

        public RtcBuilder<TNode, TLeaf> SetBuildPrimitive(RTCBuildPrimitive[] prims)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _prims = prims;
            return this;
        }

        public RtcBuilder<TNode, TLeaf> SetBuildPrimitive(Span<RTCBuildPrimitive> prims)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _prims = prims.ToArray();
            return this;
        }

        public RtcBuilder<TNode, TLeaf> SetCreateNodeFunction(CreateNodeFunction<TNode> func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _createNode = func;
            return this;
        }

        public RtcBuilder<TNode, TLeaf> SetSetNodeChildrenFunction(SetNodeChildrenFunction<TNode> func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _setNodeChildren = func;
            return this;
        }

        public RtcBuilder<TNode, TLeaf> SetSetNodeBoundsFunction(SetNodeBoundsFunction<TNode> func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _setNodeBounds = func;
            return this;
        }

        public RtcBuilder<TNode, TLeaf> SetCreateLeafFunction(CreateLeafFunction<TLeaf> func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _createLeaf = func;
            return this;
        }

        public RtcBuilder<TNode, TLeaf> SetSplitPrimitiveFunction(SplitPrimitiveFunction func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _splitPrimitive = func;
            return this;
        }

        public RtcBuilder<TNode, TLeaf> SetProgressMonitorFunction(ProgressMonitorFunction func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _progressMonitor = func;
            return this;
        }

        private static unsafe RtcBuilder<TNode, TLeaf>? GetThisFromUserPtr(void* userPtr)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(new nint(userPtr));
            if (!gcHandle.IsAllocated)
            {
                return null;
            }
            RtcBuilder<TNode, TLeaf> builder = (RtcBuilder<TNode, TLeaf>)gcHandle.Target!;
            return builder;
        }

        private static unsafe void* CreateNodeFunctionImpl(RTCThreadLocalAllocator allocator, uint childCount, void* userPtr)
        {
            RtcBuilder<TNode, TLeaf>? builder = GetThisFromUserPtr(userPtr);
            if (builder == null || builder._createNode == null)
            {
                return null;
            }
            ref TNode result = ref builder._createNode(allocator, childCount);
            return Unsafe.AsPointer(ref result);
        }

        private static unsafe void SetNodeChildrenImpl(void* nodePtr, void** children, uint childCount, void* userPtr)
        {
            RtcBuilder<TNode, TLeaf>? builder = GetThisFromUserPtr(userPtr);
            if (builder == null)
            {
                return;
            }
            ref TNode node = ref Unsafe.AsRef<TNode>(nodePtr);
            NativeMemoryView<Ref<TNode>> childs = new(children, childCount);
            builder._setNodeChildren?.Invoke(ref node, childs);
        }

        private static unsafe void SetNodeBoundsImpl(void* nodePtr, RTCBounds** bounds, uint childCount, void* userPtr)
        {
            RtcBuilder<TNode, TLeaf>? builder = GetThisFromUserPtr(userPtr);
            if (builder == null)
            {
                return;
            }
            ref TNode node = ref Unsafe.AsRef<TNode>(nodePtr);
            NativeMemoryView<Ref<RTCBounds>> bound = new(bounds, childCount);
            builder._setNodeBounds?.Invoke(ref node, bound);
        }

        private static unsafe void* CreateLeafImpl(RTCThreadLocalAllocator allocator, RTCBuildPrimitive* primitives, nuint primitiveCount, void* userPtr)
        {
            RtcBuilder<TNode, TLeaf>? builder = GetThisFromUserPtr(userPtr);
            if (builder == null || builder._createLeaf == null)
            {
                return null;
            }
            NativeMemoryView<RTCBuildPrimitive> prims = new(primitives, primitiveCount);
            ref TLeaf leaf = ref builder._createLeaf(allocator, prims);
            return Unsafe.AsPointer(ref leaf);
        }

        private static unsafe void SplitPrimitiveImpl(RTCBuildPrimitive* primitive, uint dimension, float position, RTCBounds* leftBounds, RTCBounds* rightBounds, void* userPtr)
        {
            RtcBuilder<TNode, TLeaf>? builder = GetThisFromUserPtr(userPtr);
            if (builder == null)
            {
                return;
            }
            builder._splitPrimitive?.Invoke(ref Unsafe.AsRef<RTCBuildPrimitive>(primitive), dimension, position, ref Unsafe.AsRef<RTCBounds>(leftBounds), ref Unsafe.AsRef<RTCBounds>(rightBounds));
        }

        private static unsafe bool ProgressMonitorImpl(void* ptr, double n)
        {
            RtcBuilder<TNode, TLeaf>? builder = GetThisFromUserPtr(ptr);
            if (builder == null)
            {
                return true;
            }
            return builder._progressMonitor?.Invoke(n) ?? true;
        }

        public unsafe RtcThreadLocalAllocation Build()
        {
            if (IsDisposed) { ThrowUtility.ObjectDisposed(); }
            if (_prims == null) { ThrowUtility.InvalidOperation("primitives cannot be null"); }
            _nativeCreateNode = CreateNodeFunctionImpl;
            _nativeSetNodeChildren = SetNodeChildrenImpl;
            _nativeSetNodeBounds = SetNodeBoundsImpl;
            _nativeCreateLeaf = CreateLeafImpl;
            _nativeSplitPrimitive = SplitPrimitiveImpl;
            _nativeProgressMonitor = ProgressMonitorImpl;
            RTCBuildPrimitive* primitives = null;
            nuint primitiveCount = (nuint)_prims.Length;
            nuint primitiveArrayCapacity = _buildQuality == RTCBuildQuality.RTC_BUILD_QUALITY_HIGH ? primitiveCount * 2 : primitiveCount;
            try
            {
                primitives = (RTCBuildPrimitive*)NativeMemory.AlignedAlloc((nuint)sizeof(RTCBuildPrimitive) * primitiveArrayCapacity, RTCBuildPrimitive.Alignment);
                {
                    NativeMemoryView<RTCBuildPrimitive> dst = new(primitives, primitiveCount);
                    dst.CopyFrom(_prims);
                }
                RTCBuildArguments args = new()
                {
                    byteSize = (nuint)sizeof(RTCBuildArguments),
                    buildQuality = _buildQuality,
                    buildFlags = _buildFlags,
                    maxBranchingFactor = _maxBranchingFactor,
                    maxDepth = _maxDepth,
                    sahBlockSize = _sahBlockSize,
                    minLeafSize = _minLeafSize,
                    maxLeafSize = _maxLeafSize,
                    traversalCost = _traversalCost,
                    intersectionCost = _intersectionCost,
                    bvh = _bvh,
                    primitives = primitives,
                    primitiveCount = primitiveCount,
                    primitiveArrayCapacity = primitiveArrayCapacity,
                    createNode = _createNode == null ? 0 : Marshal.GetFunctionPointerForDelegate(_nativeCreateNode),
                    setNodeChildren = _setNodeChildren == null ? 0 : Marshal.GetFunctionPointerForDelegate(_nativeSetNodeChildren),
                    setNodeBounds = _setNodeBounds == null ? 0 : Marshal.GetFunctionPointerForDelegate(_nativeSetNodeBounds),
                    createLeaf = _createLeaf == null ? 0 : Marshal.GetFunctionPointerForDelegate(_nativeCreateLeaf),
                    splitPrimitive = _splitPrimitive == null ? 0 : Marshal.GetFunctionPointerForDelegate(_nativeSplitPrimitive),
                    buildProgress = _progressMonitor == null ? 0 : Marshal.GetFunctionPointerForDelegate(_nativeProgressMonitor),
                    userPtr = GCHandle.ToIntPtr(_gcHandle).ToPointer()
                };
                void* root = GlobalFunctions.rtcBuildBVH(&args);
                _result = new RtcThreadLocalAllocation(root);
                return _result;
            }
            finally
            {
                if (_prims != null)
                {
                    NativeMemory.AlignedFree(primitives);
                    primitives = null;
                }
            }
        }
    }
}
