using EmbreeSharp.Native;
using System;
using System.Runtime.CompilerServices;

namespace EmbreeSharp
{
    public class EmbreeBuffer : IDisposable
    {
        private RTCBufferHandle _buffer;
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
                return new RTCBuffer() { Ptr = _buffer.DangerousGetHandle() };
            }
        }
        public nuint ByteSize => _byteSize;
        public bool IsDisposed => _disposedValue;

        public EmbreeBuffer(EmbreeDevice device, nuint byteSize) : this(EmbreeNative.rtcNewBuffer(device.NativeDevice, byteSize), byteSize) { }

        protected EmbreeBuffer(RTCBuffer rtcBuffer, nuint byteSize)
        {
            _buffer = new RTCBufferHandle(rtcBuffer);
            _byteSize = byteSize;
        }

        ~EmbreeBuffer()
        {
            Dispose(disposing: false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposedValue)
            {
                _buffer.Dispose();
                _buffer = null!;
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
                void* dst = EmbreeNative.rtcGetBufferData(NativeBuffer);
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
                void* dst = EmbreeNative.rtcGetBufferData(NativeBuffer);
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
