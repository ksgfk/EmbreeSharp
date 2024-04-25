using System;
using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    /// <summary>
    /// Ray structure for a single ray
    /// </summary>
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential)]
    public struct RTCRay
    {
        public const int Alignment = 16;

        /// <summary>
        /// x coordinate of ray origin
        /// </summary>
        public float org_x;
        /// <summary>
        /// y coordinate of ray origin
        /// </summary>
        public float org_y;
        /// <summary>
        /// z coordinate of ray origin
        /// </summary>
        public float org_z;
        /// <summary>
        /// start of ray segment
        /// </summary>
        public float tnear;

        /// <summary>
        /// x coordinate of ray direction
        /// </summary>
        public float dir_x;
        /// <summary>
        /// y coordinate of ray direction
        /// </summary>
        public float dir_y;
        /// <summary>
        /// z coordinate of ray direction
        /// </summary>
        public float dir_z;
        /// <summary>
        /// time of this ray for motion blur
        /// </summary>
        public float time;

        /// <summary>
        /// end of ray segment (set to hit distance)
        /// </summary>
        public float tfar;
        /// <summary>
        /// ray mask
        /// </summary>
        public uint mask;
        /// <summary>
        /// ray ID
        /// </summary>
        public uint id;
        /// <summary>
        /// ray flags
        /// </summary>
        public uint flags;
    }

    /// <summary>
    /// Hit structure for a single ray
    /// </summary>
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential, Size = 48)]
    public unsafe struct RTCHit
    {
        public const int Alignment = 16;

        /// <summary>
        /// x coordinate of geometry normal
        /// </summary>
        public float Ng_x;
        /// <summary>
        /// y coordinate of geometry normal
        /// </summary>
        public float Ng_y;
        /// <summary>
        /// z coordinate of geometry normal
        /// </summary>
        public float Ng_z;

        /// <summary>
        /// barycentric u coordinate of hit
        /// </summary>
        public float u;
        /// <summary>
        /// barycentric v coordinate of hit
        /// </summary>
        public float v;

        /// <summary>
        /// primitive ID
        /// </summary>
        public uint primID;
        /// <summary>
        /// geometry ID
        /// </summary>
        public uint geomID;
        /// <summary>
        /// instance ID
        /// </summary>
        public fixed uint instID[EmbreeNative.RTC_MAX_INSTANCE_LEVEL_COUNT];
        /// <summary>
        /// instance primitive ID
        /// </summary>
        public fixed uint instPrimID[EmbreeNative.RTC_MAX_INSTANCE_LEVEL_COUNT];
    }

    /// <summary>
    /// Combined ray/hit structure for a single ray
    /// </summary>
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential)]
    public struct RTCRayHit
    {
        public const int Alignment = 16;

        public RTCRay ray;
        public RTCHit hit;
    }

    /// <summary>
    /// Ray structure for a packet of 4 rays
    /// </summary>
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RTCRay4
    {
        public const int Alignment = 16;

        public fixed float org_x[4];
        public fixed float org_y[4];
        public fixed float org_z[4];
        public fixed float tnear[4];

        public fixed float dir_x[4];
        public fixed float dir_y[4];
        public fixed float dir_z[4];
        public fixed float time[4];

        public fixed float tfar[4];
        public fixed uint mask[4];
        public fixed uint id[4];
        public fixed uint flags[4];
    }

    /// <summary>
    /// Hit structure for a packet of 4 rays
    /// </summary>
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RTCHit4
    {
        public const int Alignment = 16;

        public fixed float Ng_x[4];
        public fixed float Ng_y[4];
        public fixed float Ng_z[4];

        public fixed float u[4];
        public fixed float v[4];

        public fixed uint primID[4];
        public fixed uint geomID[4];
        public fixed uint instID[EmbreeNative.RTC_MAX_INSTANCE_LEVEL_COUNT * 4];
        public fixed uint instPrimID[EmbreeNative.RTC_MAX_INSTANCE_LEVEL_COUNT * 4];
    }

    /// <summary>
    /// Combined ray/hit structure for a packet of 4 rays
    /// </summary>
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential)]
    public struct RTCRayHit4
    {
        public const int Alignment = 16;

        public RTCRay4 ray;
        public RTCHit4 hit;
    }

    /// <summary>
    /// Ray structure for a packet of 8 rays
    /// </summary>
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RTCRay8
    {
        public const int Alignment = 32;

        public fixed float org_x[8];
        public fixed float org_y[8];
        public fixed float org_z[8];
        public fixed float tnear[8];

        public fixed float dir_x[8];
        public fixed float dir_y[8];
        public fixed float dir_z[8];
        public fixed float time[8];

        public fixed float tfar[8];
        public fixed uint mask[8];
        public fixed uint id[8];
        public fixed uint flags[8];
    }

    /// <summary>
    /// Hit structure for a packet of 8 rays
    /// </summary>
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RTCHit8
    {
        public const int Alignment = 32;

        public fixed float Ng_x[8];
        public fixed float Ng_y[8];
        public fixed float Ng_z[8];

        public fixed float u[8];
        public fixed float v[8];

        public fixed uint primID[8];
        public fixed uint geomID[8];
        public fixed uint instID[EmbreeNative.RTC_MAX_INSTANCE_LEVEL_COUNT * 8];
        public fixed uint instPrimID[EmbreeNative.RTC_MAX_INSTANCE_LEVEL_COUNT * 8];
    }

    /// <summary>
    /// Combined ray/hit structure for a packet of 8 rays
    /// </summary>
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential)]
    public struct RTCRayHit8
    {
        public const int Alignment = 32;

        public RTCRay8 ray;
        public RTCHit8 hit;
    }

    /// <summary>
    /// Ray structure for a packet of 16 rays
    /// </summary>
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RTCRay16
    {
        public const int Alignment = 64;

        public fixed float org_x[16];
        public fixed float org_y[16];
        public fixed float org_z[16];
        public fixed float tnear[16];

        public fixed float dir_x[16];
        public fixed float dir_y[16];
        public fixed float dir_z[16];
        public fixed float time[16];

        public fixed float tfar[16];
        public fixed uint mask[16];
        public fixed uint id[16];
        public fixed uint flags[16];
    }

    /// <summary>
    /// Hit structure for a packet of 16 rays
    /// </summary>
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RTCHit16
    {
        public const int Alignment = 64;

        public fixed float Ng_x[16];
        public fixed float Ng_y[16];
        public fixed float Ng_z[16];

        public fixed float u[16];
        public fixed float v[16];

        public fixed uint primID[16];
        public fixed uint geomID[16];
        public fixed uint instID[EmbreeNative.RTC_MAX_INSTANCE_LEVEL_COUNT * 16];
        public fixed uint instPrimID[EmbreeNative.RTC_MAX_INSTANCE_LEVEL_COUNT * 16];
    }

    /// <summary>
    /// Combined ray/hit structure for a packet of 16 rays
    /// </summary>
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential)]
    public struct RTCRayHit16
    {
        public const int Alignment = 64;

        public RTCRay16 ray;
        public RTCHit16 hit;
    }

    public struct RTCRayN
    {
        public IntPtr Ptr;
    }

    public struct RTCHitN
    {
        public IntPtr Ptr;
    }

    public struct RTCRayHitN
    {
        public IntPtr Ptr;
    }

    public static unsafe partial class EmbreeNative
    {
        /* Helper functions to access ray packets of runtime size N */
        public static float* RTCRayN_org_x(RTCRayN ray, uint N, uint i) { return &((float*)ray.Ptr)[0 * N + i]; }
        public static float* RTCRayN_org_y(RTCRayN ray, uint N, uint i) { return &((float*)ray.Ptr)[1 * N + i]; }
        public static float* RTCRayN_org_z(RTCRayN ray, uint N, uint i) { return &((float*)ray.Ptr)[2 * N + i]; }
        public static float* RTCRayN_tnear(RTCRayN ray, uint N, uint i) { return &((float*)ray.Ptr)[3 * N + i]; }

        public static float* RTCRayN_dir_x(RTCRayN ray, uint N, uint i) { return &((float*)ray.Ptr)[4 * N + i]; }
        public static float* RTCRayN_dir_y(RTCRayN ray, uint N, uint i) { return &((float*)ray.Ptr)[5 * N + i]; }
        public static float* RTCRayN_dir_z(RTCRayN ray, uint N, uint i) { return &((float*)ray.Ptr)[6 * N + i]; }
        public static float* RTCRayN_time(RTCRayN ray, uint N, uint i) { return &((float*)ray.Ptr)[7 * N + i]; }

        public static float* RTCRayN_tfar(RTCRayN ray, uint N, uint i) { return &((float*)ray.Ptr)[8 * N + i]; }
        public static uint* RTCRayN_mask(RTCRayN ray, uint N, uint i) { return &((uint*)ray.Ptr)[9 * N + i]; }
        public static uint* RTCRayN_id(RTCRayN ray, uint N, uint i) { return &((uint*)ray.Ptr)[10 * N + i]; }
        public static uint* RTCRayN_flags(RTCRayN ray, uint N, uint i) { return &((uint*)ray.Ptr)[11 * N + i]; }

        /* Helper functions to access hit packets of runtime size N */
        public static float* RTCHitN_Ng_x(RTCHitN hit, uint N, uint i) { return &((float*)hit.Ptr)[0 * N + i]; }
        public static float* RTCHitN_Ng_y(RTCHitN hit, uint N, uint i) { return &((float*)hit.Ptr)[1 * N + i]; }
        public static float* RTCHitN_Ng_z(RTCHitN hit, uint N, uint i) { return &((float*)hit.Ptr)[2 * N + i]; }

        public static float* RTCHitN_u(RTCHitN hit, uint N, uint i) { return &((float*)hit.Ptr)[3 * N + i]; }
        public static float* RTCHitN_v(RTCHitN hit, uint N, uint i) { return &((float*)hit.Ptr)[4 * N + i]; }

        public static uint* RTCHitN_primID(RTCHitN hit, uint N, uint i) { return &((uint*)hit.Ptr)[5 * N + i]; }
        public static uint* RTCHitN_geomID(RTCHitN hit, uint N, uint i) { return &((uint*)hit.Ptr)[6 * N + i]; }
        public static uint* RTCHitN_instID(RTCHitN hit, uint N, uint i, uint l) { return &((uint*)hit.Ptr)[7 * N + N * l + i]; }
        public static uint* RTCHitN_instPrimID(RTCHitN hit, uint N, uint i, uint l) { return &((uint*)hit.Ptr)[7 * N + N * RTC_MAX_INSTANCE_LEVEL_COUNT + N * l + i]; }

        /* Helper functions to extract RTCRayN and RTCHitN from RTCRayHitN */
        public static RTCRayN RTCRayHitN_RayN(RTCRayHitN rayhit, uint N)
        {
            var addr = &((float*)rayhit.Ptr)[0 * N];
            return new RTCRayN() { Ptr = new nint(addr) };
        }
        public static RTCHitN RTCRayHitN_HitN(RTCRayHitN rayhit, uint N)
        {
            var addr = &((float*)rayhit.Ptr)[12 * N];
            return new RTCHitN() { Ptr = new nint(addr) };
        }

        public static RTCRay rtcGetRayFromRayN(RTCRayN rayN, uint N, uint i)
        {
            RTCRay ray;
            ray.org_x = *RTCRayN_org_x(rayN, N, i);
            ray.org_y = *RTCRayN_org_y(rayN, N, i);
            ray.org_z = *RTCRayN_org_z(rayN, N, i);
            ray.tnear = *RTCRayN_tnear(rayN, N, i);
            ray.dir_x = *RTCRayN_dir_x(rayN, N, i);
            ray.dir_y = *RTCRayN_dir_y(rayN, N, i);
            ray.dir_z = *RTCRayN_dir_z(rayN, N, i);
            ray.time = *RTCRayN_time(rayN, N, i);
            ray.tfar = *RTCRayN_tfar(rayN, N, i);
            ray.mask = *RTCRayN_mask(rayN, N, i);
            ray.id = *RTCRayN_id(rayN, N, i);
            ray.flags = *RTCRayN_flags(rayN, N, i);
            return ray;
        }

        public static RTCHit rtcGetHitFromHitN(RTCHitN hitN, uint N, uint i)
        {
            RTCHit hit;
            hit.Ng_x = *RTCHitN_Ng_x(hitN, N, i);
            hit.Ng_y = *RTCHitN_Ng_y(hitN, N, i);
            hit.Ng_z = *RTCHitN_Ng_z(hitN, N, i);
            hit.u = *RTCHitN_u(hitN, N, i);
            hit.v = *RTCHitN_v(hitN, N, i);
            hit.primID = *RTCHitN_primID(hitN, N, i);
            hit.geomID = *RTCHitN_geomID(hitN, N, i);
            for (uint l = 0; l < RTC_MAX_INSTANCE_LEVEL_COUNT; l++)
            {
                hit.instID[l] = *RTCHitN_instID(hitN, N, i, l);
                hit.instPrimID[l] = *RTCHitN_instPrimID(hitN, N, i, l);
            }
            return hit;
        }

        public static void rtcCopyHitToHitN(RTCHitN hitN, [NativeType("const RTCHit*")] RTCHit* hit, uint N, uint i)
        {
            *RTCHitN_Ng_x(hitN, N, i) = hit->Ng_x;
            *RTCHitN_Ng_y(hitN, N, i) = hit->Ng_y;
            *RTCHitN_Ng_z(hitN, N, i) = hit->Ng_z;
            *RTCHitN_u(hitN, N, i) = hit->u;
            *RTCHitN_v(hitN, N, i) = hit->v;
            *RTCHitN_primID(hitN, N, i) = hit->primID;
            *RTCHitN_geomID(hitN, N, i) = hit->geomID;
            for (uint l = 0; l < RTC_MAX_INSTANCE_LEVEL_COUNT; l++)
            {
                *RTCHitN_instID(hitN, N, i, l) = hit->instID[l];
                *RTCHitN_instPrimID(hitN, N, i, l) = hit->instPrimID[l];
            }
        }

        public static RTCRayHit rtcGetRayHitFromRayHitN(RTCRayHitN rayhitN, uint N, uint i)
        {
            RTCRayHit rh = default;

            RTCRayN ray = RTCRayHitN_RayN(rayhitN, N);
            rh.ray.org_x = *RTCRayN_org_x(ray, N, i);
            rh.ray.org_y = *RTCRayN_org_y(ray, N, i);
            rh.ray.org_z = *RTCRayN_org_z(ray, N, i);
            rh.ray.tnear = *RTCRayN_tnear(ray, N, i);
            rh.ray.dir_x = *RTCRayN_dir_x(ray, N, i);
            rh.ray.dir_y = *RTCRayN_dir_y(ray, N, i);
            rh.ray.dir_z = *RTCRayN_dir_z(ray, N, i);
            rh.ray.time = *RTCRayN_time(ray, N, i);
            rh.ray.tfar = *RTCRayN_tfar(ray, N, i);
            rh.ray.mask = *RTCRayN_mask(ray, N, i);
            rh.ray.id = *RTCRayN_id(ray, N, i);
            rh.ray.flags = *RTCRayN_flags(ray, N, i);

            RTCHitN hit = RTCRayHitN_HitN(rayhitN, N);
            rh.hit.Ng_x = *RTCHitN_Ng_x(hit, N, i);
            rh.hit.Ng_y = *RTCHitN_Ng_y(hit, N, i);
            rh.hit.Ng_z = *RTCHitN_Ng_z(hit, N, i);
            rh.hit.u = *RTCHitN_u(hit, N, i);
            rh.hit.v = *RTCHitN_v(hit, N, i);
            rh.hit.primID = *RTCHitN_primID(hit, N, i);
            rh.hit.geomID = *RTCHitN_geomID(hit, N, i);
            for (uint l = 0; l < RTC_MAX_INSTANCE_LEVEL_COUNT; l++)
            {
                rh.hit.instID[l] = *RTCHitN_instID(hit, N, i, l);
                rh.hit.instPrimID[l] = *RTCHitN_instPrimID(hit, N, i, l);
            }
            return rh;
        }
    }
}
