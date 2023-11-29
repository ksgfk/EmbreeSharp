using System;
using System.Runtime.CompilerServices;
using EmbreeSharp.Native;

namespace EmbreeSharp
{
    public class RtcBuffer : IDisposable
    {
        private RTCBuffer _buffer;
        private readonly nuint _byteSize;
        private bool _disposedValue = false;

        public RTCBuffer NativeBuffer
        {
            get
            {
                if (IsDisposed)
                {
                    ThrowUtility.ObjectDisposed();
                }
                return _buffer;
            }
        }
        public nuint ByteSize => _byteSize;
        public bool IsDisposed => _disposedValue;

        public RtcBuffer(RtcDevice device, nuint byteSize)
        {
            _buffer = GlobalFunctions.rtcNewBuffer(device.NativeDevice, byteSize);
            _byteSize = byteSize;
        }

        public RtcBuffer(RtcBuffer other)
        {
            if (other.IsDisposed)
            {
                ThrowUtility.ObjectDisposed(nameof(other));
            }
            GlobalFunctions.rtcRetainBuffer(other._buffer);
            _buffer = other._buffer;
            _byteSize = other._byteSize;
        }

        ~RtcBuffer()
        {
            Dispose(disposing: false);
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                // if (disposing) { }
                GlobalFunctions.rtcReleaseBuffer(_buffer);
                _buffer = RTCBuffer.Null;
                _disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

        public NativeMemoryView<byte> GetData()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            unsafe
            {
                void* dst = GlobalFunctions.rtcGetBufferData(_buffer);
                return new NativeMemoryView<byte>(dst, _byteSize);
            }
        }

        public NativeMemoryView<T> GetData<T>() where T : unmanaged
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            var count = _byteSize / (nuint)Unsafe.SizeOf<T>();
            unsafe
            {
                void* dst = GlobalFunctions.rtcGetBufferData(_buffer);
                return new NativeMemoryView<T>(dst, count);
            }
        }

        public void CopyFrom<T>(ReadOnlySpan<T> src, nuint dstStart = 0) where T : unmanaged
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            NativeMemoryView<T> data = GetData<T>();
            NativeMemoryView<T> dst = data.Slice(dstStart);
            dst.CopyFrom(src);
        }

        public void CopyFrom<T>(NativeMemoryView<T> src, nuint dstStart = 0) where T : unmanaged
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            NativeMemoryView<T> data = GetData<T>();
            NativeMemoryView<T> dst = data.Slice(dstStart);
            src.CopyTo(dst);
        }
    }
}
