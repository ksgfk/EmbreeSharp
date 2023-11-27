using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
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
        public fixed uint instID[Embree.RTC_MAX_INSTANCE_LEVEL_COUNT * 4];
        public fixed uint instPrimID[Embree.RTC_MAX_INSTANCE_LEVEL_COUNT * 4];
    }
}
