using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential, Pack = 16)]
public partial struct RTCRay
{
    public float org_x;

    public float org_y;

    public float org_z;

    public float tnear;

    public float dir_x;

    public float dir_y;

    public float dir_z;

    public float time;

    public float tfar;

    [NativeTypeName("unsigned int")]
    public uint mask;

    [NativeTypeName("unsigned int")]
    public uint id;

    [NativeTypeName("unsigned int")]
    public uint flags;
}
