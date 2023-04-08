using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential, Pack = 32)]
public partial struct RTCBuildPrimitive
{
    public float lower_x;

    public float lower_y;

    public float lower_z;

    [NativeTypeName("unsigned int")]
    public uint geomID;

    public float upper_x;

    public float upper_y;

    public float upper_z;

    [NativeTypeName("unsigned int")]
    public uint primID;
}
