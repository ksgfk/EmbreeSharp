using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    [RTCAlign(16)]
    [StructLayout(LayoutKind.Sequential)]
    public struct RTCBounds
    {
        public float lower_x, lower_y, lower_z, align0;
        public float upper_x, upper_y, upper_z, align1;
    }
}
