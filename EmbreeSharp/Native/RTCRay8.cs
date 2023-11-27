using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
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
}
