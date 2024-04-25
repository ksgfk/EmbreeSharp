using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    /// <summary>
    /// Structure for transformation representation as a matrix decomposition using a quaternion
    /// </summary>
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential)]
    public struct RTCQuaternionDecomposition
    {
        public const int Alignment = 16;

        public float scale_x;
        public float scale_y;
        public float scale_z;
        public float skew_xy;
        public float skew_xz;
        public float skew_yz;
        public float shift_x;
        public float shift_y;
        public float shift_z;
        public float quaternion_r;
        public float quaternion_i;
        public float quaternion_j;
        public float quaternion_k;
        public float translation_x;
        public float translation_y;
        public float translation_z;
    }

    public static unsafe partial class EmbreeNative
    {
        public static void rtcInitQuaternionDecomposition(RTCQuaternionDecomposition* qdecomp)
        {
            qdecomp->scale_x = 1.0f;
            qdecomp->scale_y = 1.0f;
            qdecomp->scale_z = 1.0f;
            qdecomp->skew_xy = 0.0f;
            qdecomp->skew_xz = 0.0f;
            qdecomp->skew_yz = 0.0f;
            qdecomp->shift_x = 0.0f;
            qdecomp->shift_y = 0.0f;
            qdecomp->shift_z = 0.0f;
            qdecomp->quaternion_r = 1.0f;
            qdecomp->quaternion_i = 0.0f;
            qdecomp->quaternion_j = 0.0f;
            qdecomp->quaternion_k = 0.0f;
            qdecomp->translation_x = 0.0f;
            qdecomp->translation_y = 0.0f;
            qdecomp->translation_z = 0.0f;
        }

        public static void rtcQuaternionDecompositionSetQuaternion(
            RTCQuaternionDecomposition* qdecomp,
            float r, float i, float j, float k)
        {
            qdecomp->quaternion_r = r;
            qdecomp->quaternion_i = i;
            qdecomp->quaternion_j = j;
            qdecomp->quaternion_k = k;
        }


        public static void rtcQuaternionDecompositionSetScale(
            RTCQuaternionDecomposition* qdecomp,
            float scale_x, float scale_y, float scale_z)
        {
            qdecomp->scale_x = scale_x;
            qdecomp->scale_y = scale_y;
            qdecomp->scale_z = scale_z;
        }

        public static void rtcQuaternionDecompositionSetSkew(
           RTCQuaternionDecomposition* qdecomp,
            float skew_xy, float skew_xz, float skew_yz)
        {
            qdecomp->skew_xy = skew_xy;
            qdecomp->skew_xz = skew_xz;
            qdecomp->skew_yz = skew_yz;
        }

        public static void rtcQuaternionDecompositionSetShift(
            RTCQuaternionDecomposition* qdecomp,
            float shift_x, float shift_y, float shift_z)
        {
            qdecomp->shift_x = shift_x;
            qdecomp->shift_y = shift_y;
            qdecomp->shift_z = shift_z;
        }

        public static void rtcQuaternionDecompositionSetTranslation(
            RTCQuaternionDecomposition* qdecomp,
            float translation_x, float translation_y, float translation_z)
        {
            qdecomp->translation_x = translation_x;
            qdecomp->translation_y = translation_y;
            qdecomp->translation_z = translation_z;
        }
    }
}
