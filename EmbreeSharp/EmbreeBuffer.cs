using EmbreeSharp.Native;
using System;
using System.Runtime.CompilerServices;

namespace EmbreeSharp
{
    public class EmbreeBuffer : IDisposable
    {
        private readonly RTCBuffer _buffer;
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

        public EmbreeBuffer(EmbreeDevice device, nuint byteSize)
        {
            _buffer = EmbreeNative.rtcNewBuffer(device.NativeDevice, byteSize);
            _byteSize = byteSize;
        }

        ~EmbreeBuffer()
        {
            Dispose(disposing: false);
        }

        protected void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                // if (disposing) { }
                _buffer.Dispose();
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
                void* dst = EmbreeNative.rtcGetBufferData(_buffer);
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
                void* dst = EmbreeNative.rtcGetBufferData(_buffer);
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
