using EmbreeSharp.Native;
using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices;

namespace EmbreeSharp
{
    public class EmbreeGeometry : IDisposable
    {
        private GCHandle _gcHandle;
        private RTCGeometryHandle _geometry;
        private uint _id;
        private readonly RTCGeometryType _type;
        private bool _disposedValue;
        private readonly Dictionary<uint, EmbreeBuffer> _attachedEmbreeBuffers = [];
        private readonly Dictionary<uint, SharedBufferHandle> _attachedSharedBuffer = [];

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
                //if (disposing) { }
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
                _gcHandle = default;
                _geometry.Dispose();
                _geometry = null!;
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
            // C# matrix is row-major
            return new Matrix4x4(
                mat[0], mat[4], mat[8], mat[12],
                mat[1], mat[5], mat[9], mat[13],
                mat[2], mat[6], mat[10], mat[14],
                mat[3], mat[7], mat[11], mat[15]);
        }

        public unsafe void SetTransform4x4(uint timeStep, Matrix4x4 mat)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            Matrix4x4 column = Matrix4x4.Transpose(mat);
            EmbreeNative.rtcSetGeometryTransform(NativeGeometry, timeStep, RTCFormat.RTC_FORMAT_FLOAT4X4_COLUMN_MAJOR, &column);
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
    }
}
