using System;
using System.Runtime.InteropServices;
using EmbreeSharp.Native;

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

        public RtcGeometry(RtcDevice device, RTCGeometryType type)
        {
            _gcHandle = GCHandle.Alloc(this);
            _geometry = GlobalFunctions.rtcNewGeometry(device.NativeDevice, type);
            _type = type;
        }

        public RtcGeometry(RtcGeometry other)
        {
            if (other.IsDisposed)
            {
                ThrowUtility.ObjectDisposed(nameof(other));
            }
            _gcHandle = GCHandle.Alloc(this);
            GlobalFunctions.rtcRetainGeometry(other._geometry);
            _geometry = other._geometry;
            _type = other._type;
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
                GlobalFunctions.rtcReleaseGeometry(_geometry);
                _geometry = RTCGeometry.Null;
                _gcHandle.Free();
                _gcHandle = default;
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
            GlobalFunctions.rtcSetGeometryBuffer(_geometry, type, slot, format, buffer.NativeBuffer, byteOffset, byteStride, itemCount);
        }

        public unsafe NativeMemoryView<byte> SetNewBuffer(RTCBufferType type, uint slot, RTCFormat format, nuint byteStride, nuint itemCount)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            void* ptr = GlobalFunctions.rtcSetNewGeometryBuffer(_geometry, type, slot, format, byteStride, itemCount);
            nuint byteCount = byteStride * itemCount;
            return new NativeMemoryView<byte>(ptr, byteCount);
        }

        public void UpdateBuffer(RTCBufferType type, uint slot)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            GlobalFunctions.rtcUpdateGeometryBuffer(_geometry, type, slot);
        }

        public void Commit()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            GlobalFunctions.rtcCommitGeometry(_geometry);
        }

        public void Enable()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            GlobalFunctions.rtcEnableGeometry(_geometry);
        }

        public void Disable()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            GlobalFunctions.rtcDisableGeometry(_geometry);
        }

        public void SetTimeStepCount(uint timeStepCount)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            GlobalFunctions.rtcSetGeometryTimeStepCount(_geometry, timeStepCount);
        }

        public void SetTimeRange(float startTime, float endTime)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            GlobalFunctions.rtcSetGeometryTimeRange(_geometry, startTime, endTime);
        }

        public void SetVertexAttributeCount(uint vertexAttributeCount)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            GlobalFunctions.rtcSetGeometryVertexAttributeCount(_geometry, vertexAttributeCount);
        }

        public void SetMask(uint mask)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            GlobalFunctions.rtcSetGeometryMask(_geometry, mask);
        }

        public void SetBuildQuality(RTCBuildQuality quality)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            GlobalFunctions.rtcSetGeometryBuildQuality(_geometry, quality);
        }

        public void SetMaxRadiusScale(float maxRadiusScale)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            GlobalFunctions.rtcSetGeometryMaxRadiusScale(_geometry, maxRadiusScale);
        }
    }
}
