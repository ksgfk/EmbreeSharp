using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential, Pack = 64)]
public unsafe partial struct RTCRay16
{
    [NativeTypeName("float[16]")]
    public fixed float org_x[16];

    [NativeTypeName("float[16]")]
    public fixed float org_y[16];

    [NativeTypeName("float[16]")]
    public fixed float org_z[16];

    [NativeTypeName("float[16]")]
    public fixed float tnear[16];

    [NativeTypeName("float[16]")]
    public fixed float dir_x[16];

    [NativeTypeName("float[16]")]
    public fixed float dir_y[16];

    [NativeTypeName("float[16]")]
    public fixed float dir_z[16];

    [NativeTypeName("float[16]")]
    public fixed float time[16];

    [NativeTypeName("float[16]")]
    public fixed float tfar[16];

    [NativeTypeName("unsigned int[16]")]
    public fixed uint mask[16];

    [NativeTypeName("unsigned int[16]")]
    public fixed uint id[16];

    [NativeTypeName("unsigned int[16]")]
    public fixed uint flags[16];
}
