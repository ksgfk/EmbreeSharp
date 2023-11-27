using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
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
        public fixed uint instID[Embree.RTC_MAX_INSTANCE_LEVEL_COUNT * 8];
        public fixed uint instPrimID[Embree.RTC_MAX_INSTANCE_LEVEL_COUNT * 8];
    }
}
