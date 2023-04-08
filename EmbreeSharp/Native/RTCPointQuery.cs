using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential, Pack = 16)]
public partial struct RTCPointQuery
{
    public float x;

    public float y;

    public float z;

    public float time;

    public float radius;
}
