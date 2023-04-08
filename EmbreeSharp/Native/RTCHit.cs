using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential, Pack = 16)]
public unsafe partial struct RTCHit
{
    public float Ng_x;

    public float Ng_y;

    public float Ng_z;

    public float u;

    public float v;

    [NativeTypeName("unsigned int")]
    public uint primID;

    [NativeTypeName("unsigned int")]
    public uint geomID;

    [NativeTypeName("unsigned int[1]")]
    public fixed uint instID[1];
}
