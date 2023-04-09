using System.Runtime.CompilerServices;

namespace EmbreeSharp;

public static class NativeSpanHelper
{
    public static RtcBufferView<TTo> Cast<TFrom, TTo>(RtcBufferView<TFrom> from) where TFrom : struct where TTo : struct
    {
        if (RuntimeHelpers.IsReferenceOrContainsReferences<TFrom>())
        {
            ThrowInvalidOperation($"{typeof(TFrom).FullName} is reference type");
        }
        if (RuntimeHelpers.IsReferenceOrContainsReferences<TTo>())
        {
            ThrowInvalidOperation($"{typeof(TTo).FullName} is reference type");
        }
        unsafe
        {
            int sizeFrom = sizeof(TFrom);
            int sizeTo = sizeof(TTo);
            long byteFrom = sizeFrom * from.Length;
            if (byteFrom % sizeTo != 0)
            {
                ThrowInvalidOperation($"cannot cast {typeof(TFrom).FullName} to {typeof(TTo).FullName}");
            }
            return new RtcBufferView<TTo>(from.NativePtr.ToPointer(), byteFrom / sizeTo);
        }
    }
}
