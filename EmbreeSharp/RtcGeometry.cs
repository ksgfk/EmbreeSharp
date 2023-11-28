using System;
using EmbreeSharp.Native;

namespace EmbreeSharp
{
    public class RtcGeometry : IDisposable
    {
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
        public int Id { get => unchecked((int)_id); internal set => _id = unchecked((uint)value); }
        internal uint IdInternal { get => _id; set => _id = value; }
        public bool IsDisposed => _disposedValue;

        public RtcGeometry(RtcDevice device, RTCGeometryType type)
        {
            _geometry = GlobalFunctions.rtcNewGeometry(device.NativeDevice, type);
            _type = type;
        }

        public RtcGeometry(RtcGeometry other)
        {
            if (other.IsDisposed)
            {
                ThrowUtility.ObjectDisposed(nameof(other));
            }
            GlobalFunctions.rtcRetainGeometry(other._geometry);
            _geometry = other._geometry;
            _type = other._type;
        }

        ~RtcGeometry()
        {
            Dispose(disposing: false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                // if (disposing) { }
                GlobalFunctions.rtcReleaseGeometry(_geometry);
                _geometry = RTCGeometry.Null;
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
