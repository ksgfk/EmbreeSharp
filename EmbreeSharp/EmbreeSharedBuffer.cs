using EmbreeSharp.Native;

namespace EmbreeSharp
{
    public class EmbreeSharedBuffer : EmbreeBuffer
    {
        private SharedBufferHandle _bufferHandle;

        public EmbreeSharedBuffer(EmbreeDevice device, SharedBufferHandle handle) : base(CreateRTCBuffer(device, handle), handle.Buffer.View.Length)
        {
            _bufferHandle = handle;
            bool isSucc = false;
            _bufferHandle.DangerousAddRef(ref isSucc);
            if (!isSucc)
            {
                ThrowUtility.InvalidOperation("cannot add ref count");
            }
        }

        private static unsafe RTCBuffer CreateRTCBuffer(EmbreeDevice device, SharedBufferHandle handle)
        {
            if (handle.IsClosed || handle.IsInvalid)
            {
                ThrowUtility.ObjectDisposed(nameof(handle));
            }
            NativeMemoryView<byte> buffer = handle.Buffer.View;
            RTCBuffer result = EmbreeNative.rtcNewSharedBuffer(device.NativeDevice, buffer.UnsafePtr.ToPointer(), buffer.Length);
            return result;
        }

        protected override void Dispose(bool disposing)
        {
            if (!IsDisposed)
            {
                _bufferHandle.DangerousRelease();
                _bufferHandle.Dispose();
                if (disposing)
                {
                    _bufferHandle = null!;
                }
            }
            base.Dispose(disposing);
        }
    }
}
