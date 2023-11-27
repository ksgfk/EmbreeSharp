using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
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
}
