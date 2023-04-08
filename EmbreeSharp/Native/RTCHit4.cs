using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential, Pack = 16)]
public unsafe partial struct RTCHit4
{
    [NativeTypeName("float[4]")]
    public fixed float Ng_x[4];

    [NativeTypeName("float[4]")]
    public fixed float Ng_y[4];

    [NativeTypeName("float[4]")]
    public fixed float Ng_z[4];

    [NativeTypeName("float[4]")]
    public fixed float u[4];

    [NativeTypeName("float[4]")]
    public fixed float v[4];

    [NativeTypeName("unsigned int[4]")]
    public fixed uint primID[4];

    [NativeTypeName("unsigned int[4]")]
    public fixed uint geomID[4];

    [NativeTypeName("unsigned int[1][4]")]
    public fixed uint instID[1 * 4];
}
