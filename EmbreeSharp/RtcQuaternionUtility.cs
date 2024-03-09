using EmbreeSharp.Native;

namespace EmbreeSharp
{
    public static partial class RtcQuaternionUtility
    {
    }

    public static class RtcQuaternionExtension
    {
        public static void Init(ref this RTCQuaternionDecomposition qdecomp)
        {
            qdecomp.scale_x = 1.0f;
            qdecomp.scale_y = 1.0f;
            qdecomp.scale_z = 1.0f;
            qdecomp.skew_xy = 0.0f;
            qdecomp.skew_xz = 0.0f;
            qdecomp.skew_yz = 0.0f;
            qdecomp.shift_x = 0.0f;
            qdecomp.shift_y = 0.0f;
            qdecomp.shift_z = 0.0f;
            qdecomp.quaternion_r = 1.0f;
            qdecomp.quaternion_i = 0.0f;
            qdecomp.quaternion_j = 0.0f;
            qdecomp.quaternion_k = 0.0f;
            qdecomp.translation_x = 0.0f;
            qdecomp.translation_y = 0.0f;
            qdecomp.translation_z = 0.0f;
        }

        public static void SetQuaternion(ref this RTCQuaternionDecomposition qdecomp, float r, float i, float j, float k)
        {
            qdecomp.quaternion_r = r;
            qdecomp.quaternion_i = i;
            qdecomp.quaternion_j = j;
            qdecomp.quaternion_k = k;
        }

        public static void SetScale(ref this RTCQuaternionDecomposition qdecomp, float scale_x, float scale_y, float scale_z)
        {
            qdecomp.scale_x = scale_x;
            qdecomp.scale_y = scale_y;
            qdecomp.scale_z = scale_z;
        }

        public static void SetSkew(ref this RTCQuaternionDecomposition qdecomp, float skew_xy, float skew_xz, float skew_yz)
        {
            qdecomp.skew_xy = skew_xy;
            qdecomp.skew_xz = skew_xz;
            qdecomp.skew_yz = skew_yz;
        }

        public static void SetShift(ref this RTCQuaternionDecomposition qdecomp, float shift_x, float shift_y, float shift_z)
        {
            qdecomp.shift_x = shift_x;
            qdecomp.shift_y = shift_y;
            qdecomp.shift_z = shift_z;
        }

        public static void SetTranslation(ref this RTCQuaternionDecomposition qdecomp, float translation_x, float translation_y, float translation_z)
        {
            qdecomp.translation_x = translation_x;
            qdecomp.translation_y = translation_y;
            qdecomp.translation_z = translation_z;
        }
    }
}
