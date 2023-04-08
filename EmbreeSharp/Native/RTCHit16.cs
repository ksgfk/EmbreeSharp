using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential, Pack = 64)]
public unsafe partial struct RTCHit16
{
    [NativeTypeName("float[16]")]
    public fixed float Ng_x[16];

    [NativeTypeName("float[16]")]
    public fixed float Ng_y[16];

    [NativeTypeName("float[16]")]
    public fixed float Ng_z[16];

    [NativeTypeName("float[16]")]
    public fixed float u[16];

    [NativeTypeName("float[16]")]
    public fixed float v[16];

    [NativeTypeName("unsigned int[16]")]
    public fixed uint primID[16];

    [NativeTypeName("unsigned int[16]")]
    public fixed uint geomID[16];

    [NativeTypeName("unsigned int[1][16]")]
    public fixed uint instID[1 * 16];
}
