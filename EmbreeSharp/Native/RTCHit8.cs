using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential, Pack = 32)]
public unsafe partial struct RTCHit8
{
    [NativeTypeName("float[8]")]
    public fixed float Ng_x[8];

    [NativeTypeName("float[8]")]
    public fixed float Ng_y[8];

    [NativeTypeName("float[8]")]
    public fixed float Ng_z[8];

    [NativeTypeName("float[8]")]
    public fixed float u[8];

    [NativeTypeName("float[8]")]
    public fixed float v[8];

    [NativeTypeName("unsigned int[8]")]
    public fixed uint primID[8];

    [NativeTypeName("unsigned int[8]")]
    public fixed uint geomID[8];

    [NativeTypeName("unsigned int[1][8]")]
    public fixed uint instID[1 * 8];
}
