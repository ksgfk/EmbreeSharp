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
        public fixed uint instID[GlobalFunctions.RTC_MAX_INSTANCE_LEVEL_COUNT];
        /// <summary>
        /// instance primitive ID
        /// </summary>
        public fixed uint instPrimID[GlobalFunctions.RTC_MAX_INSTANCE_LEVEL_COUNT];
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
        public fixed uint instID[GlobalFunctions.RTC_MAX_INSTANCE_LEVEL_COUNT * 4];
        public fixed uint instPrimID[GlobalFunctions.RTC_MAX_INSTANCE_LEVEL_COUNT * 4];
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
        public fixed uint instID[GlobalFunctions.RTC_MAX_INSTANCE_LEVEL_COUNT * 8];
        public fixed uint instPrimID[GlobalFunctions.RTC_MAX_INSTANCE_LEVEL_COUNT * 8];
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
        public fixed uint instID[GlobalFunctions.RTC_MAX_INSTANCE_LEVEL_COUNT * 16];
        public fixed uint instPrimID[GlobalFunctions.RTC_MAX_INSTANCE_LEVEL_COUNT * 16];
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
}
