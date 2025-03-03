using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using EmbreeSharp.Native;

namespace EmbreeSharp
{
    public delegate void GeometryFilterFunctionN(ref readonly RTCFilterFunctionNArguments args);
    public delegate void GeometryIntersectFunctionN(ref readonly RTCIntersectFunctionNArguments args);
    public delegate bool GeometryPointQueryFunction(ref readonly RTCPointQueryFunctionArguments args);
    public delegate void GeometryBoundsFunction(ref readonly RTCBoundsFunctionArguments args);
    public delegate void GeometryOccludedFunctionN(ref readonly RTCOccludedFunctionNArguments args);
    public delegate void GeometryDisplacementFunctionN(ref readonly RTCDisplacementFunctionNArguments args);

    public class EmbreeGeometry : IDisposable
    {
        private GCHandle _gcHandle;
        private RTCGeometryHandle _geometry;
        private uint _id;
        private readonly RTCGeometryType _type;
        private bool _disposedValue;
        private readonly Dictionary<uint, EmbreeBuffer> _attachedEmbreeBuffers = [];
        private readonly Dictionary<uint, SharedBufferHandle> _attachedSharedBuffer = [];
        private GeometryFilterFunctionN? _intersectFilterFunc;
        private GeometryFilterFunctionN? _occludedFilterFunc;
        private GeometryPointQueryFunction? _pointQueryFunc;
        private GeometryIntersectFunctionN? _intersectFunc;
        private GeometryBoundsFunction? _boundsFunc;
        private GeometryOccludedFunctionN? _occludedFunc;
        private GeometryDisplacementFunctionN? _displacementFunc;

        public RTCGeometry NativeGeometry
        {
            get
            {
                if (IsDisposed)
                {
                    ThrowUtility.ObjectDisposed();
                }
                return new RTCGeometry() { Ptr = _geometry.DangerousGetHandle() };
            }
        }
        public RTCGeometryType Type => _type;
        public uint Id { get => _id; set => _id = value; }
        public bool IsDisposed => _disposedValue;

        public EmbreeGeometry(EmbreeDevice device, RTCGeometryType type)
        {
            _gcHandle = GCHandle.Alloc(this, GCHandleType.Weak);
            var geo = EmbreeNative.rtcNewGeometry(device.NativeDevice, type);
            _geometry = new RTCGeometryHandle(geo);
            _type = type;
            unsafe
            {
                EmbreeNative.rtcSetGeometryUserData(NativeGeometry, GCHandle.ToIntPtr(_gcHandle).ToPointer());
            }
        }

        ~EmbreeGeometry()
        {
            Dispose(disposing: false);
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                foreach (var i in _attachedSharedBuffer.Values)
                {
                    i.DangerousRelease();
                    i.Dispose();
                }
                _attachedSharedBuffer.Clear();
                _attachedEmbreeBuffers.Clear();
                unsafe
                {
                    EmbreeNative.rtcSetGeometryUserData(NativeGeometry, null);
                }
                _gcHandle.Free();
                _geometry.Dispose();
                if (disposing)
                {
                    _gcHandle = default;
                    _geometry = null!;
                }
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public void SetBuffer(RTCBufferType type, uint slot, RTCFormat format, EmbreeBuffer buffer, nuint byteOffset, nuint byteStride, nuint itemCount)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            if (byteStride * itemCount + byteOffset > buffer.ByteSize)
            {
                ThrowUtility.ArgumentOutOfRange();
            }
            TryRemoveSharedBufferInSlotAndRelease(slot);
            EmbreeNative.rtcSetGeometryBuffer(NativeGeometry, type, slot, format, buffer.NativeBuffer, byteOffset, byteStride, itemCount);
            _attachedEmbreeBuffers[slot] = buffer;
        }

        public unsafe NativeMemoryView<byte> SetNewBuffer(RTCBufferType type, uint slot, RTCFormat format, nuint byteStride, nuint itemCount)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            TryRemoveSharedBufferInSlotAndRelease(slot);
            void* ptr = EmbreeNative.rtcSetNewGeometryBuffer(NativeGeometry, type, slot, format, byteStride, itemCount);
            nuint byteCount = byteStride * itemCount;
            _attachedEmbreeBuffers.Remove(slot);
            return new NativeMemoryView<byte>(ptr, byteCount);
        }

        public unsafe void SetSharedBuffer(RTCBufferType type, uint slot, RTCFormat format, SharedBufferHandle handle, nuint byteOffset, nuint byteStride, nuint itemCount)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            if (handle.IsClosed)
            {
                ThrowUtility.ObjectDisposed(nameof(handle));
            }
            bool isSucc = false;
            handle.DangerousAddRef(ref isSucc);
            if (!isSucc)
            {
                ThrowUtility.InvalidOperation("cannot add ref count");
            }
            TryRemoveSharedBufferInSlotAndRelease(slot);
            _attachedSharedBuffer.Add(slot, handle);
            EmbreeNative.rtcSetSharedGeometryBuffer(NativeGeometry, type, slot, format, handle.Buffer.View.UnsafePtr.ToPointer(), byteOffset, byteStride, itemCount);
            _attachedEmbreeBuffers.Remove(slot);
        }

        private void TryRemoveSharedBufferInSlotAndRelease(uint slot)
        {
            if (_attachedSharedBuffer.TryGetValue(slot, out SharedBufferHandle? handle))
            {
                _attachedSharedBuffer.Remove(slot);
                handle.DangerousRelease();
                handle.Dispose();
            }
        }

        public void UpdateBuffer(RTCBufferType type, uint slot)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcUpdateGeometryBuffer(NativeGeometry, type, slot);
        }

        public void Commit()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcCommitGeometry(NativeGeometry);
        }

        public void Enable()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcEnableGeometry(NativeGeometry);
        }

        public void Disable()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcDisableGeometry(NativeGeometry);
        }

        public void SetTimeStepCount(uint timeStepCount)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcSetGeometryTimeStepCount(NativeGeometry, timeStepCount);
        }

        public void SetTimeRange(float startTime, float endTime)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcSetGeometryTimeRange(NativeGeometry, startTime, endTime);
        }

        public void SetVertexAttributeCount(uint vertexAttributeCount)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcSetGeometryVertexAttributeCount(NativeGeometry, vertexAttributeCount);
        }

        public void SetMask(uint mask)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcSetGeometryMask(NativeGeometry, mask);
        }

        public void SetBuildQuality(RTCBuildQuality quality)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcSetGeometryBuildQuality(NativeGeometry, quality);
        }

        public void SetMaxRadiusScale(float maxRadiusScale)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcSetGeometryMaxRadiusScale(NativeGeometry, maxRadiusScale);
        }

        public void SetInstancedScene(EmbreeScene scene)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            RTCScene s = scene.NativeScene;
            EmbreeNative.rtcSetGeometryInstancedScene(NativeGeometry, s);
        }

        public unsafe Matrix4x4 GetTransform4x4(float time)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            Span<float> mat = stackalloc float[16];
            fixed (float* ptr = mat)
            {
                EmbreeNative.rtcGetGeometryTransform(NativeGeometry, time, RTCFormat.RTC_FORMAT_FLOAT4X4_COLUMN_MAJOR, ptr);
            }
            return InteropUtility.RTCFloat4x4ToMatrix4x4ColumnMajor(mat);
        }

        public unsafe Matrix4x4 GetTransform4x4Ex(uint instPrimID, float time)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            Span<float> mat = stackalloc float[16];
            fixed (float* ptr = mat)
            {
                EmbreeNative.rtcGetGeometryTransformEx(NativeGeometry, instPrimID, time, RTCFormat.RTC_FORMAT_FLOAT4X4_COLUMN_MAJOR, ptr);
            }
            return InteropUtility.RTCFloat4x4ToMatrix4x4ColumnMajor(mat);
        }

        public unsafe void SetTransform4x4(uint timeStep, Matrix4x4 mat)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            Span<float> v = stackalloc float[16];
            InteropUtility.Matrix4x4ToRTCFloat4x4ColumnMajor(mat, v);
            fixed (float* ptr = v)
            {
                EmbreeNative.rtcSetGeometryTransform(NativeGeometry, timeStep, RTCFormat.RTC_FORMAT_FLOAT4X4_COLUMN_MAJOR, ptr);
            }
        }

        public unsafe void SetTransformQuaternion(uint timeStep, ref readonly RTCQuaternionDecomposition quat)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            fixed (RTCQuaternionDecomposition* ptr = &quat)
            {
                EmbreeNative.rtcSetGeometryTransformQuaternion(NativeGeometry, timeStep, ptr);
            }
        }

        private static unsafe EmbreeGeometry? GetThisFromUserPtr(void* userPtr)
        {
            GCHandle gcHandle = GCHandle.FromIntPtr(new nint(userPtr));
            if (!gcHandle.IsAllocated)
            {
                return null;
            }
            return gcHandle.Target == null ? null : (EmbreeGeometry)gcHandle.Target;
        }

        private static unsafe void IntersectFilterFunctionNImpl(RTCFilterFunctionNArguments* args)
        {
            EmbreeGeometry? that = GetThisFromUserPtr(args->geometryUserPtr);
            if (that == null)
            {
                return;
            }
            that._intersectFilterFunc?.Invoke(ref Unsafe.AsRef<RTCFilterFunctionNArguments>(args));
        }

        private static unsafe void OccludedFilterFunctionNImpl(RTCFilterFunctionNArguments* args)
        {
            EmbreeGeometry? that = GetThisFromUserPtr(args->geometryUserPtr);
            if (that == null)
            {
                return;
            }
            that._occludedFilterFunc?.Invoke(ref Unsafe.AsRef<RTCFilterFunctionNArguments>(args));
        }

        private static unsafe void IntersectFunctionNImpl(RTCIntersectFunctionNArguments* args)
        {
            EmbreeGeometry? that = GetThisFromUserPtr(args->geometryUserPtr);
            if (that == null)
            {
                return;
            }
            that._intersectFunc?.Invoke(ref Unsafe.AsRef<RTCIntersectFunctionNArguments>(args));
        }

        private static unsafe bool PointQueryFunctionImpl(RTCPointQueryFunctionArguments* args)
        {
            EmbreeGeometry? that = GetThisFromUserPtr(args->userPtr);
            if (that == null)
            {
                return false;
            }
            return that._pointQueryFunc?.Invoke(ref Unsafe.AsRef<RTCPointQueryFunctionArguments>(args)) ?? false;
        }

        private static unsafe void BoundsFunctionImpl(RTCBoundsFunctionArguments* args)
        {
            EmbreeGeometry? that = GetThisFromUserPtr(args->geometryUserPtr);
            if (that == null)
            {
                return;
            }
            that._boundsFunc?.Invoke(ref Unsafe.AsRef<RTCBoundsFunctionArguments>(args));
        }

        private static unsafe void OccludedFunctionNImpl(RTCOccludedFunctionNArguments* args)
        {
            EmbreeGeometry? that = GetThisFromUserPtr(args->geometryUserPtr);
            if (that == null)
            {
                return;
            }
            that._occludedFunc?.Invoke(ref Unsafe.AsRef<RTCOccludedFunctionNArguments>(args));
        }

        private static unsafe void DisplacementFunctionNImpl(RTCDisplacementFunctionNArguments* args)
        {
            EmbreeGeometry? that = GetThisFromUserPtr(args->geometryUserPtr);
            if (that == null)
            {
                return;
            }
            that._displacementFunc?.Invoke(ref Unsafe.AsRef<RTCDisplacementFunctionNArguments>(args));
        }

        public unsafe void SetIntersectFilterFunction(GeometryFilterFunctionN? func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _intersectFilterFunc = func;
            if (func == null)
            {
                EmbreeNative.rtcSetGeometryIntersectFilterFunction(NativeGeometry, null);
            }
            else
            {
                EmbreeNative.rtcSetGeometryIntersectFilterFunction(NativeGeometry, IntersectFilterFunctionNImpl);
            }
        }

        public unsafe void SetOccludedFilterFunction(GeometryFilterFunctionN? func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _occludedFilterFunc = func;
            if (func == null)
            {
                EmbreeNative.rtcSetGeometryOccludedFilterFunction(NativeGeometry, null);
            }
            else
            {
                EmbreeNative.rtcSetGeometryOccludedFilterFunction(NativeGeometry, OccludedFilterFunctionNImpl);
            }
        }

        public void SetEnableFilterFunctionFromArguments(bool enable)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcSetGeometryEnableFilterFunctionFromArguments(NativeGeometry, enable);
        }

        public unsafe void SetPointQueryFunction(GeometryPointQueryFunction? func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _pointQueryFunc = func;
            if (func == null)
            {
                EmbreeNative.rtcSetGeometryPointQueryFunction(NativeGeometry, null);
            }
            else
            {
                EmbreeNative.rtcSetGeometryPointQueryFunction(NativeGeometry, PointQueryFunctionImpl);
            }
        }

        public void SetUserPrimitiveCount(uint userPrimitiveCount)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcSetGeometryUserPrimitiveCount(NativeGeometry, userPrimitiveCount);
        }

        public unsafe void SetBoundsFunction(GeometryBoundsFunction? func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _boundsFunc = func;
            if (func == null)
            {
                EmbreeNative.rtcSetGeometryBoundsFunction(NativeGeometry, null, null);
            }
            else
            {
                EmbreeNative.rtcSetGeometryBoundsFunction(NativeGeometry, BoundsFunctionImpl, GCHandle.ToIntPtr(_gcHandle).ToPointer());
            }
        }

        public unsafe void SetIntersectFunction(GeometryIntersectFunctionN? func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _intersectFunc = func;
            if (func == null)
            {
                EmbreeNative.rtcSetGeometryIntersectFunction(NativeGeometry, null);
            }
            else
            {
                EmbreeNative.rtcSetGeometryIntersectFunction(NativeGeometry, IntersectFunctionNImpl);
            }
        }

        public unsafe void SetOccludedFunction(GeometryOccludedFunctionN? func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _occludedFunc = func;
            if (func == null)
            {
                EmbreeNative.rtcSetGeometryOccludedFunction(NativeGeometry, null);
            }
            else
            {
                EmbreeNative.rtcSetGeometryOccludedFunction(NativeGeometry, OccludedFunctionNImpl);
            }
        }

        public unsafe void SetInstancedScenes(IEnumerable<EmbreeScene> scenes)
        {
            RTCScene[] e = [.. scenes.Select(i => i.NativeScene)];
            fixed (RTCScene* ptr = e)
            {
                EmbreeNative.rtcSetGeometryInstancedScenes(NativeGeometry, ptr, (nuint)e.Length);
            }
        }

        public void SetTessellationRate(float tessellationRate)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcSetGeometryTessellationRate(NativeGeometry, tessellationRate);
        }

        public void SetTopologyCount(uint topologyCount)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcSetGeometryTopologyCount(NativeGeometry, topologyCount);
        }

        public void SetSubdivisionMode(uint topologyID, RTCSubdivisionMode mode)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcSetGeometrySubdivisionMode(NativeGeometry, topologyID, mode);
        }

        public void SetVertexAttributeTopology(uint vertexAttributeID, uint topologyID)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcSetGeometryVertexAttributeTopology(NativeGeometry, vertexAttributeID, topologyID);
        }

        public unsafe void SetDisplacementFunction(GeometryDisplacementFunctionN? func)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            _displacementFunc = func;
            if (func == null)
            {
                EmbreeNative.rtcSetGeometryDisplacementFunction(NativeGeometry, null);
            }
            else
            {
                EmbreeNative.rtcSetGeometryDisplacementFunction(NativeGeometry, DisplacementFunctionNImpl);
            }
        }

        public uint GetFirstHalfEdge(uint faceID)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            return EmbreeNative.rtcGetGeometryFirstHalfEdge(NativeGeometry, faceID);
        }

        public uint GetFace(uint edgeID)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            return EmbreeNative.rtcGetGeometryFace(NativeGeometry, edgeID);
        }

        public uint GetNextHalfEdge(uint edgeID)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            return EmbreeNative.rtcGetGeometryNextHalfEdge(NativeGeometry, edgeID);
        }

        public uint GetPreviousHalfEdge(uint edgeID)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            return EmbreeNative.rtcGetGeometryPreviousHalfEdge(NativeGeometry, edgeID);
        }

        public uint GetOppositeHalfEdge(uint topologyID, uint edgeID)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            return EmbreeNative.rtcGetGeometryOppositeHalfEdge(NativeGeometry, topologyID, edgeID);
        }
    }
}
