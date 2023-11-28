using System;
using System.Runtime.CompilerServices;
using EmbreeSharp.Native;

namespace EmbreeSharp
{
    public class RtcBuffer : IDisposable
    {
        private RTCBuffer _buffer;
        private readonly long _byteSize;
        private bool _disposedValue = false;

        public RTCBuffer NativeBuffer => _buffer;
        public long ByteSize => _byteSize;
        public bool IsDisposed => _disposedValue;

        public RtcBuffer(RtcDevice device, long byteSize)
        {
            _buffer = GlobalFunctions.rtcNewBuffer(device.NativeDevice, new nint(byteSize));
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

        protected virtual void Dispose(bool disposing)
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

        public Span<byte> GetData()
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            if (ByteSize > int.MaxValue)
            {
                ThrowUtility.IndexOutOfRange($"This buffer is too large to access using span. buffer size: {ByteSize}");
            }
            int length = (int)ByteSize;
            unsafe
            {
                void* dst = GlobalFunctions.rtcGetBufferData(_buffer);
                return new Span<byte>(dst, length);
            }
        }

        public Span<T> GetData<T>() where T : unmanaged
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            unsafe
            {
                var count = ByteSize / sizeof(T);
                var align = ByteSize % sizeof(T);
                if (count > int.MaxValue)
                {
                    ThrowUtility.ArgumentOutOfRange($"This buffer is too large to access using span. buffer count: {count}");
                }
                if (align != 0)
                {
                    ThrowUtility.InvalidOperation($"This buffer cannot reinterpreted by type {typeof(T)}");
                }
                int length = (int)count;
                void* dst = GlobalFunctions.rtcGetBufferData(_buffer);
                return new Span<T>(dst, length);
            }
        }

        public Span<byte> GetData(int start, int length)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            if ((long)start + length > ByteSize)
            {
                ThrowUtility.ArgumentOutOfRange();
            }
            unsafe
            {
                void* dst = GlobalFunctions.rtcGetBufferData(_buffer);
                void* dstStart = Unsafe.Add<byte>(dst, start);
                return new Span<byte>(dstStart, length);
            }
        }

        public Span<T> GetData<T>(int start, int length) where T : unmanaged
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            var elemSize = Unsafe.SizeOf<T>();
            var allBytes = ((long)start + length) * elemSize;
            if (allBytes > ByteSize)
            {
                ThrowUtility.ArgumentOutOfRange();
            }
            unsafe
            {
                void* dst = GlobalFunctions.rtcGetBufferData(_buffer);
                void* dstStart = Unsafe.Add<T>(dst, start);
                return new Span<T>(dstStart, length);
            }
        }

        public void CopyFrom(byte[] srcData, int length, int srcStart = 0, int dstStart = 0)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            Span<byte> src = srcData.AsSpan().Slice(srcStart, length);
            Span<byte> dst = GetData(dstStart, length);
            src.CopyTo(dst);
        }

        public void CopyFrom<T>(T[] srcData, int length, int srcStart = 0, int dstStart = 0) where T : unmanaged
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            Span<T> src = srcData.AsSpan().Slice(srcStart, length);
            Span<T> dst = GetData<T>(dstStart, length);
            src.CopyTo(dst);
        }

        public void CopyFrom(ReadOnlySpan<byte> src, int dstStart = 0)
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            Span<byte> dst = GetData(dstStart, src.Length);
            src.CopyTo(dst);
        }

        public void CopyFrom<T>(ReadOnlySpan<T> src, int dstStart = 0) where T : unmanaged
        {
            if (IsDisposed)
            {
                ThrowUtility.ObjectDisposed();
            }
            Span<T> dst = GetData<T>(dstStart, src.Length);
            src.CopyTo(dst);
        }
    }
}
