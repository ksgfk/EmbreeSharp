using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
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
}
