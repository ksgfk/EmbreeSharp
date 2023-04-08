using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential, Pack = 16)]
public partial struct RTCBounds
{
    public float lower_x;

    public float lower_y;

    public float lower_z;

    public float align0;

    public float upper_x;

    public float upper_y;

    public float upper_z;

    public float align1;
}
