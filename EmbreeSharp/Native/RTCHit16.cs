using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
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
        public fixed uint instID[Embree.RTC_MAX_INSTANCE_LEVEL_COUNT * 16];
        public fixed uint instPrimID[Embree.RTC_MAX_INSTANCE_LEVEL_COUNT * 16];
    }
}
