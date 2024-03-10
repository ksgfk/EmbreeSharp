using EmbreeSharp.Native;
using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace EmbreeSharp
{
    public delegate RTCThreadLocalAllocation CreateNodeFunction(RTCThreadLocalAllocator allocator, uint childCount);
    public delegate ref T CreateNodeFunction<T>(RTCThreadLocalAllocator allocator, uint childCount) where T : unmanaged;

    public delegate void SetNodeChildrenFunction(RTCThreadLocalAllocation node, NativeMemoryView<RTCThreadLocalAllocation> children);
    public delegate void SetNodeChildrenFunction<T>(ref T node, NativeMemoryView<Ref<T>> children) where T : unmanaged;

    public delegate void SetNodeBoundsFunction(RTCThreadLocalAllocation node, NativeMemoryView<Ref<RTCBounds>> bounds);
    public delegate void SetNodeBoundsFunction<T>(ref T node, NativeMemoryView<Ref<RTCBounds>> bounds) where T : unmanaged;

    public delegate RTCThreadLocalAllocation CreateLeafFunction(RTCThreadLocalAllocator allocator, NativeMemoryView<RTCBuildPrimitive> primitives);
    public delegate ref T CreateLeafFunction<T>(RTCThreadLocalAllocator allocator, NativeMemoryView<RTCBuildPrimitive> primitives) where T : unmanaged;

    public delegate void SplitPrimitiveFunction(ref readonly RTCBuildPrimitive primitive, uint dimension, float position, ref RTCBounds leftBounds, ref RTCBounds rightBounds);

    public abstract class EmbreeBuilder<T> : IDisposable where T : EmbreeBuilder<T>
    {
        private bool _disposedValue;

        private GCHandle _gcHandle;
        private readonly RTCBVH _bvh;

        protected RTCBuildQuality _buildQuality;
        protected RTCBuildFlags _buildFlags;
        protected uint _maxBranchingFactor;
        protected uint _maxDepth;
        protected uint _sahBlockSize;
        protected uint _minLeafSize;
        protected uint _maxLeafSize;
        protected float _traversalCost;
        protected float _intersectionCost;
        protected RTCBuildPrimitive[]? _prims;

        public bool IsDisposed => _disposedValue;
        public RTCBVH NativeBVH => _bvh;
        internal GCHandle Gc => _gcHandle;

        public EmbreeBuilder(EmbreeDevice device)
        {
            _gcHandle = GCHandle.Alloc(this, GCHandleType.Weak);
            _bvh = EmbreeNative.rtcNewBVH(device.NativeDevice);
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

        ~EmbreeBuilder()
        {
            Dispose(disposing: false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                if (disposing)
                {
                    _prims = null;
                }
                _gcHandle.Free();
                _gcHandle = default;
                _bvh.Dispose();
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }


        public T SetBuildQuality(RTCBuildQuality quality)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _buildQuality = quality;
            return (T)this;
        }

        public T SetBuildFlag(RTCBuildFlags flags)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _buildFlags = flags;
            return (T)this;
        }

        public T SetMaxBranchingFactor(uint value)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _maxBranchingFactor = value;
            return (T)this;
        }

        public T SetMaxDepth(uint value)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _maxDepth = value;
            return (T)this;
        }

        public T SetSahBlockSize(uint value)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _sahBlockSize = value;
            return (T)this;
        }

        public T SetMinLeafSize(uint value)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _minLeafSize = value;
            return (T)this;
        }

        public T SetMaxLeafSize(uint value)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _maxLeafSize = value;
            return (T)this;
        }

        public T SetTraversalCost(float value)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _traversalCost = value;
            return (T)this;
        }

        public T SetIntersectionCost(float value)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _intersectionCost = value;
            return (T)this;
        }

        public T SetBuildPrimitive(RTCBuildPrimitive[] prims)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _prims = prims;
            return (T)this;
        }

        public T SetBuildPrimitive(Span<RTCBuildPrimitive> prims)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _prims = prims.ToArray();
            return (T)this;
        }
    }

    public class EmbreeBuilder : EmbreeBuilder<EmbreeBuilder>
    {
        private RTCThreadLocalAllocation _result;
        private CreateNodeFunction? _createNode;
        private SetNodeChildrenFunction? _setNodeChildren;
        private SetNodeBoundsFunction? _setNodeBounds;
        private CreateLeafFunction? _createLeaf;
        private SplitPrimitiveFunction? _splitPrimitive;
        private ProgressMonitorFunction? _progressMonitor;

        public RTCThreadLocalAllocation Result => _result;

        public EmbreeBuilder(EmbreeDevice device) : base(device) { }

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    _createNode = null;
                    _setNodeChildren = null;
                    _setNodeBounds = null;
                    _createLeaf = null;
                    _splitPrimitive = null;
                    _progressMonitor = null;
                }
                _result = default;
            }
            base.Dispose(disposing);
        }

        public EmbreeBuilder SetCreateNodeFunction(CreateNodeFunction func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _createNode = func;
            return this;
        }

        public EmbreeBuilder SetSetNodeChildrenFunction(SetNodeChildrenFunction func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _setNodeChildren = func;
            return this;
        }

        public EmbreeBuilder SetSetNodeBoundsFunction(SetNodeBoundsFunction func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _setNodeBounds = func;
            return this;
        }

        public EmbreeBuilder SetCreateLeafFunction(CreateLeafFunction func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _createLeaf = func;
            return this;
        }

        public EmbreeBuilder SetSplitPrimitiveFunction(SplitPrimitiveFunction func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _splitPrimitive = func;
            return this;
        }

        public EmbreeBuilder SetProgressMonitorFunction(ProgressMonitorFunction func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _progressMonitor = func;
            return this;
        }

        private static unsafe EmbreeBuilder? GetThisFromUserPtr(void* userPtr)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(new nint(userPtr));
            if (!gcHandle.IsAllocated)
            {
                return null;
            }
            EmbreeBuilder builder = (EmbreeBuilder)gcHandle.Target!;
            return builder;
        }

        private static unsafe void* CreateNodeFunctionImpl(RTCThreadLocalAllocator allocator, uint childCount, void* userPtr)
        {
            EmbreeBuilder? builder = GetThisFromUserPtr(userPtr);
            if (builder == null)
            {
                return null;
            }
            RTCThreadLocalAllocation allocation = builder._createNode?.Invoke(allocator, childCount) ?? default;
            return allocation.Ptr;
        }

        private static unsafe void SetNodeChildrenImpl(void* nodePtr, void** children, uint childCount, void* userPtr)
        {
            EmbreeBuilder? builder = GetThisFromUserPtr(userPtr);
            if (builder == null)
            {
                return;
            }
            RTCThreadLocalAllocation node = new(nodePtr);
            NativeMemoryView<RTCThreadLocalAllocation> childs = new(children, childCount);
            builder._setNodeChildren?.Invoke(node, childs);
        }

        private static unsafe void SetNodeBoundsImpl(void* nodePtr, RTCBounds** bounds, uint childCount, void* userPtr)
        {
            EmbreeBuilder? builder = GetThisFromUserPtr(userPtr);
            if (builder == null)
            {
                return;
            }
            RTCThreadLocalAllocation node = new(nodePtr);
            NativeMemoryView<Ref<RTCBounds>> bound = new(bounds, childCount);
            builder._setNodeBounds?.Invoke(node, bound);
        }

        private static unsafe void* CreateLeafImpl(RTCThreadLocalAllocator allocator, RTCBuildPrimitive* primitives, nuint primitiveCount, void* userPtr)
        {
            EmbreeBuilder? builder = GetThisFromUserPtr(userPtr);
            if (builder == null)
            {
                return null;
            }
            NativeMemoryView<RTCBuildPrimitive> prims = new(primitives, primitiveCount);
            RTCThreadLocalAllocation leaf = builder._createLeaf?.Invoke(allocator, prims) ?? default;
            return leaf.Ptr;
        }

        private static unsafe void SplitPrimitiveImpl(RTCBuildPrimitive* primitive, uint dimension, float position, RTCBounds* leftBounds, RTCBounds* rightBounds, void* userPtr)
        {
            EmbreeBuilder? builder = GetThisFromUserPtr(userPtr);
            if (builder == null)
            {
                return;
            }
            builder._splitPrimitive?.Invoke(ref Unsafe.AsRef<RTCBuildPrimitive>(primitive), dimension, position, ref Unsafe.AsRef<RTCBounds>(leftBounds), ref Unsafe.AsRef<RTCBounds>(rightBounds));
        }

        private static unsafe bool ProgressMonitorImpl(void* ptr, double n)
        {
            EmbreeBuilder? builder = GetThisFromUserPtr(ptr);
            if (builder == null)
            {
                return true;
            }
            return builder._progressMonitor?.Invoke(n) ?? true;
        }

        public unsafe RTCThreadLocalAllocation Build()
        {
            if (IsDisposed) { ThrowUtility.ObjectDisposed(); }
            if (_prims == null) { ThrowUtility.InvalidOperation("primitives cannot be null"); }
            nint createNode = 0;
            nint setNodeChildren = 0;
            nint setNodeBounds = 0;
            nint createLeaf = 0;
            nint splitPrim = 0;
            nint progMonitor = 0;
            RTCCreateNodeFunction nCreateNode = CreateNodeFunctionImpl;
            RTCSetNodeChildrenFunction nSetNodeChildren = SetNodeChildrenImpl;
            RTCSetNodeBoundsFunction nSetNodeBounds = SetNodeBoundsImpl;
            RTCCreateLeafFunction nCreateLeaf = CreateLeafImpl;
            RTCSplitPrimitiveFunction nSplitPrim = SplitPrimitiveImpl;
            RTCProgressMonitorFunction nProgMonitor = ProgressMonitorImpl;
            createNode = _createNode == null ? 0 : Marshal.GetFunctionPointerForDelegate(nCreateNode);
            setNodeChildren = _setNodeChildren == null ? 0 : Marshal.GetFunctionPointerForDelegate(nSetNodeChildren);
            setNodeBounds = _setNodeBounds == null ? 0 : Marshal.GetFunctionPointerForDelegate(nSetNodeBounds);
            createLeaf = _createLeaf == null ? 0 : Marshal.GetFunctionPointerForDelegate(nCreateLeaf);
            splitPrim = _splitPrimitive == null ? 0 : Marshal.GetFunctionPointerForDelegate(nSplitPrim);
            progMonitor = _progressMonitor == null ? 0 : Marshal.GetFunctionPointerForDelegate(nProgMonitor);
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
                    bvh = NativeBVH.DangerousGetHandle(),
                    primitives = primitives,
                    primitiveCount = primitiveCount,
                    primitiveArrayCapacity = primitiveArrayCapacity,
                    createNode = createNode,
                    setNodeChildren = setNodeChildren,
                    setNodeBounds = setNodeBounds,
                    createLeaf = createLeaf,
                    splitPrimitive = splitPrim,
                    buildProgress = progMonitor,
                    userPtr = GCHandle.ToIntPtr(Gc).ToPointer()
                };
                void* root = EmbreeNative.rtcBuildBVH(ref args);
                GC.KeepAlive(nCreateNode);
                GC.KeepAlive(nSetNodeChildren);
                GC.KeepAlive(nSetNodeBounds);
                GC.KeepAlive(nCreateLeaf);
                GC.KeepAlive(nSplitPrim);
                GC.KeepAlive(nProgMonitor);
                _result = new RTCThreadLocalAllocation(root);
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

    public class EmbreeBuilder<TNode, TLeaf> : EmbreeBuilder<EmbreeBuilder<TNode, TLeaf>> where TNode : unmanaged where TLeaf : unmanaged
    {
        private Ref<TNode> _result;
        private CreateNodeFunction<TNode>? _createNode;
        private SetNodeChildrenFunction<TNode>? _setNodeChildren;
        private SetNodeBoundsFunction<TNode>? _setNodeBounds;
        private CreateLeafFunction<TLeaf>? _createLeaf;
        private SplitPrimitiveFunction? _splitPrimitive;
        private ProgressMonitorFunction? _progressMonitor;

        public ref TNode Result => ref _result.Value;

        public EmbreeBuilder(EmbreeDevice device) : base(device) { }

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                if (disposing)
                {
                    _createNode = null;
                    _setNodeChildren = null;
                    _setNodeBounds = null;
                    _createLeaf = null;
                    _splitPrimitive = null;
                    _progressMonitor = null;
                }
                _result = default;
            }
            base.Dispose(disposing);
        }

        public EmbreeBuilder<TNode, TLeaf> SetCreateNodeFunction(CreateNodeFunction<TNode> func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _createNode = func;
            return this;
        }

        public EmbreeBuilder<TNode, TLeaf> SetSetNodeChildrenFunction(SetNodeChildrenFunction<TNode> func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _setNodeChildren = func;
            return this;
        }

        public EmbreeBuilder<TNode, TLeaf> SetSetNodeBoundsFunction(SetNodeBoundsFunction<TNode> func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _setNodeBounds = func;
            return this;
        }

        public EmbreeBuilder<TNode, TLeaf> SetCreateLeafFunction(CreateLeafFunction<TLeaf> func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _createLeaf = func;
            return this;
        }

        public EmbreeBuilder<TNode, TLeaf> SetSplitPrimitiveFunction(SplitPrimitiveFunction func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _splitPrimitive = func;
            return this;
        }

        public EmbreeBuilder<TNode, TLeaf> SetProgressMonitorFunction(ProgressMonitorFunction func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _progressMonitor = func;
            return this;
        }

        private static unsafe EmbreeBuilder<TNode, TLeaf>? GetThisFromUserPtr(void* userPtr)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(new nint(userPtr));
            if (!gcHandle.IsAllocated)
            {
                return null;
            }
            EmbreeBuilder<TNode, TLeaf> builder = (EmbreeBuilder<TNode, TLeaf>)gcHandle.Target!;
            return builder;
        }

        private static unsafe void* CreateNodeFunctionImpl(RTCThreadLocalAllocator allocator, uint childCount, void* userPtr)
        {
            EmbreeBuilder<TNode, TLeaf>? builder = GetThisFromUserPtr(userPtr);
            if (builder == null || builder._createNode == null)
            {
                return null;
            }
            ref TNode result = ref builder._createNode(allocator, childCount);
            return Unsafe.AsPointer(ref result);
        }

        private static unsafe void SetNodeChildrenImpl(void* nodePtr, void** children, uint childCount, void* userPtr)
        {
            EmbreeBuilder<TNode, TLeaf>? builder = GetThisFromUserPtr(userPtr);
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
            EmbreeBuilder<TNode, TLeaf>? builder = GetThisFromUserPtr(userPtr);
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
            EmbreeBuilder<TNode, TLeaf>? builder = GetThisFromUserPtr(userPtr);
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
            EmbreeBuilder<TNode, TLeaf>? builder = GetThisFromUserPtr(userPtr);
            if (builder == null)
            {
                return;
            }
            builder._splitPrimitive?.Invoke(ref Unsafe.AsRef<RTCBuildPrimitive>(primitive), dimension, position, ref Unsafe.AsRef<RTCBounds>(leftBounds), ref Unsafe.AsRef<RTCBounds>(rightBounds));
        }

        private static unsafe bool ProgressMonitorImpl(void* ptr, double n)
        {
            EmbreeBuilder<TNode, TLeaf>? builder = GetThisFromUserPtr(ptr);
            if (builder == null)
            {
                return true;
            }
            return builder._progressMonitor?.Invoke(n) ?? true;
        }

        public unsafe ref TNode Build()
        {
            if (IsDisposed) { ThrowUtility.ObjectDisposed(); }
            if (_prims == null) { ThrowUtility.InvalidOperation("primitives cannot be null"); }
            nint createNode = 0;
            nint setNodeChildren = 0;
            nint setNodeBounds = 0;
            nint createLeaf = 0;
            nint splitPrim = 0;
            nint progMonitor = 0;
            RTCCreateNodeFunction nCreateNode = CreateNodeFunctionImpl;
            RTCSetNodeChildrenFunction nSetNodeChildren = SetNodeChildrenImpl;
            RTCSetNodeBoundsFunction nSetNodeBounds = SetNodeBoundsImpl;
            RTCCreateLeafFunction nCreateLeaf = CreateLeafImpl;
            RTCSplitPrimitiveFunction nSplitPrim = SplitPrimitiveImpl;
            RTCProgressMonitorFunction nProgMonitor = ProgressMonitorImpl;
            createNode = _createNode == null ? 0 : Marshal.GetFunctionPointerForDelegate(nCreateNode);
            setNodeChildren = _setNodeChildren == null ? 0 : Marshal.GetFunctionPointerForDelegate(nSetNodeChildren);
            setNodeBounds = _setNodeBounds == null ? 0 : Marshal.GetFunctionPointerForDelegate(nSetNodeBounds);
            createLeaf = _createLeaf == null ? 0 : Marshal.GetFunctionPointerForDelegate(nCreateLeaf);
            splitPrim = _splitPrimitive == null ? 0 : Marshal.GetFunctionPointerForDelegate(nSplitPrim);
            progMonitor = _progressMonitor == null ? 0 : Marshal.GetFunctionPointerForDelegate(nProgMonitor);
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
                    bvh = NativeBVH.DangerousGetHandle(),
                    primitives = primitives,
                    primitiveCount = primitiveCount,
                    primitiveArrayCapacity = primitiveArrayCapacity,
                    createNode = createNode,
                    setNodeChildren = setNodeChildren,
                    setNodeBounds = setNodeBounds,
                    createLeaf = createLeaf,
                    splitPrimitive = splitPrim,
                    buildProgress = progMonitor,
                    userPtr = GCHandle.ToIntPtr(Gc).ToPointer()
                };
                void* root = EmbreeNative.rtcBuildBVH(ref args);
                _result = new Ref<TNode>(root);
                GC.KeepAlive(nCreateNode);
                GC.KeepAlive(nSetNodeChildren);
                GC.KeepAlive(nSetNodeBounds);
                GC.KeepAlive(nCreateLeaf);
                GC.KeepAlive(nSplitPrim);
                GC.KeepAlive(nProgMonitor);
                return ref _result.Value;
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
