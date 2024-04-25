using EmbreeSharp.Native;

namespace EmbreeSharp
{
    public static class RTCQuaternionExtension
    {
        public static void Init(ref this RTCQuaternionDecomposition qdecomp)
        {
            unsafe
            {
                fixed (RTCQuaternionDecomposition* ptr = &qdecomp)
                {
                    EmbreeNative.rtcInitQuaternionDecomposition(ptr);
                }
            }
        }

        public static void SetQuaternion(ref this RTCQuaternionDecomposition qdecomp, float r, float i, float j, float k)
        {
            unsafe
            {
                fixed (RTCQuaternionDecomposition* ptr = &qdecomp)
                {
                    EmbreeNative.rtcQuaternionDecompositionSetQuaternion(ptr, r, i, j, k);
                }
            }
        }

        public static void SetScale(ref this RTCQuaternionDecomposition qdecomp, float scale_x, float scale_y, float scale_z)
        {
            unsafe
            {
                fixed (RTCQuaternionDecomposition* ptr = &qdecomp)
                {
                    EmbreeNative.rtcQuaternionDecompositionSetScale(ptr, scale_x, scale_y, scale_z);
                }
            }
        }

        public static void SetSkew(ref this RTCQuaternionDecomposition qdecomp, float skew_xy, float skew_xz, float skew_yz)
        {
            unsafe
            {
                fixed (RTCQuaternionDecomposition* ptr = &qdecomp)
                {
                    EmbreeNative.rtcQuaternionDecompositionSetSkew(ptr, skew_xy, skew_xz, skew_yz);
                }
            }
        }

        public static void SetShift(ref this RTCQuaternionDecomposition qdecomp, float shift_x, float shift_y, float shift_z)
        {
            unsafe
            {
                fixed (RTCQuaternionDecomposition* ptr = &qdecomp)
                {
                    EmbreeNative.rtcQuaternionDecompositionSetShift(ptr, shift_x, shift_y, shift_z);
                }
            }
        }

        public static void SetTranslation(ref this RTCQuaternionDecomposition qdecomp, float translation_x, float translation_y, float translation_z)
        {
            unsafe
            {
                fixed (RTCQuaternionDecomposition* ptr = &qdecomp)
                {
                    EmbreeNative.rtcQuaternionDecompositionSetTranslation(ptr, translation_x, translation_y, translation_z);
                }
            }
        }
    }
}
