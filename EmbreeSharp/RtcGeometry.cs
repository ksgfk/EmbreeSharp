using EmbreeSharp.Native;
using System;
using System.Numerics;
using System.Runtime.InteropServices;

namespace EmbreeSharp
{
    public class RtcGeometry : IDisposable
    {
        private GCHandle _gcHandle;
        private RTCGeometry _geometry;
        private uint _id;
        private readonly RTCGeometryType _type;
        private bool _disposedValue;

        public RTCGeometry NativeGeometry
        {
            get
            {
                if (IsDisposed)
                {
                    ThrowUtility.ObjectDisposed();
                }
                return _geometry;
            }
        }
        public RTCGeometryType Type => _type;
        public uint Id { get => _id; set => _id = value; }
        public bool IsDisposed => _disposedValue;

        public RtcGeometry(EmbreeDevice device, RTCGeometryType type)
        {
            _gcHandle = GCHandle.Alloc(this, GCHandleType.Weak);
            _geometry = EmbreeNative.rtcNewGeometry(device.NativeDevice, type);
            _type = type;
            unsafe
            {
                EmbreeNative.rtcSetGeometryUserData(_geometry, GCHandle.ToIntPtr(_gcHandle).ToPointer());
            }
        }

        public RtcGeometry(RtcGeometry other)
        {
            if (other.IsDisposed)
            {
                ThrowUtility.ObjectDisposed(nameof(other));
            }
            _gcHandle = GCHandle.Alloc(this, GCHandleType.Weak);
            EmbreeNative.rtcRetainGeometry(other._geometry);
            _geometry = other._geometry;
            _type = other._type;
            unsafe
            {
                EmbreeNative.rtcSetGeometryUserData(_geometry, GCHandle.ToIntPtr(_gcHandle).ToPointer());
            }
        }

        ~RtcGeometry()
        {
            Dispose(disposing: false);
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                // if (disposing) { }
                unsafe
                {
                    EmbreeNative.rtcSetGeometryUserData(_geometry, null);
                }
                _gcHandle.Free();
                _gcHandle = default;
                EmbreeNative.rtcReleaseGeometry(_geometry);
                _geometry = RTCGeometry.Null;
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public void SetBuffer(RTCBufferType type, uint slot, RTCFormat format, RtcBuffer buffer, nuint byteOffset, nuint byteStride, nuint itemCount)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            if (byteStride * itemCount + byteOffset > buffer.ByteSize)
            {
                ThrowUtility.ArgumentOutOfRange();
            }
            EmbreeNative.rtcSetGeometryBuffer(_geometry, type, slot, format, buffer.NativeBuffer, byteOffset, byteStride, itemCount);
        }

        public unsafe NativeMemoryView<byte> SetNewBuffer(RTCBufferType type, uint slot, RTCFormat format, nuint byteStride, nuint itemCount)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            void* ptr = EmbreeNative.rtcSetNewGeometryBuffer(_geometry, type, slot, format, byteStride, itemCount);
            nuint byteCount = byteStride * itemCount;
            return new NativeMemoryView<byte>(ptr, byteCount);
        }

        public void UpdateBuffer(RTCBufferType type, uint slot)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcUpdateGeometryBuffer(_geometry, type, slot);
        }

        public void Commit()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcCommitGeometry(_geometry);
        }

        public void Enable()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcEnableGeometry(_geometry);
        }

        public void Disable()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcDisableGeometry(_geometry);
        }

        public void SetTimeStepCount(uint timeStepCount)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcSetGeometryTimeStepCount(_geometry, timeStepCount);
        }

        public void SetTimeRange(float startTime, float endTime)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcSetGeometryTimeRange(_geometry, startTime, endTime);
        }

        public void SetVertexAttributeCount(uint vertexAttributeCount)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcSetGeometryVertexAttributeCount(_geometry, vertexAttributeCount);
        }

        public void SetMask(uint mask)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcSetGeometryMask(_geometry, mask);
        }

        public void SetBuildQuality(RTCBuildQuality quality)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcSetGeometryBuildQuality(_geometry, quality);
        }

        public void SetMaxRadiusScale(float maxRadiusScale)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            EmbreeNative.rtcSetGeometryMaxRadiusScale(_geometry, maxRadiusScale);
        }

        public void SetInstancedScene(RtcScene scene)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            RTCScene s = scene.NativeScene;
            EmbreeNative.rtcSetGeometryInstancedScene(_geometry, s);
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
                EmbreeNative.rtcGetGeometryTransform(_geometry, time, RTCFormat.RTC_FORMAT_FLOAT4X4_COLUMN_MAJOR, ptr);
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
            EmbreeNative.rtcSetGeometryTransform(_geometry, timeStep, RTCFormat.RTC_FORMAT_FLOAT4X4_COLUMN_MAJOR, &column);
        }

        public unsafe void SetTransformQuaternion(uint timeStep, ref readonly RTCQuaternionDecomposition quat)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            fixed (RTCQuaternionDecomposition* ptr = &quat)
            {
                EmbreeNative.rtcSetGeometryTransformQuaternion(_geometry, timeStep, ptr);
            }
        }
    }
}
