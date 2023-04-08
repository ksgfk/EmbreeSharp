using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential, Pack = 16)]
public unsafe partial struct RTCRay4
{
    [NativeTypeName("float[4]")]
    public fixed float org_x[4];

    [NativeTypeName("float[4]")]
    public fixed float org_y[4];

    [NativeTypeName("float[4]")]
    public fixed float org_z[4];

    [NativeTypeName("float[4]")]
    public fixed float tnear[4];

    [NativeTypeName("float[4]")]
    public fixed float dir_x[4];

    [NativeTypeName("float[4]")]
    public fixed float dir_y[4];

    [NativeTypeName("float[4]")]
    public fixed float dir_z[4];

    [NativeTypeName("float[4]")]
    public fixed float time[4];

    [NativeTypeName("float[4]")]
    public fixed float tfar[4];

    [NativeTypeName("unsigned int[4]")]
    public fixed uint mask[4];

    [NativeTypeName("unsigned int[4]")]
    public fixed uint id[4];

    [NativeTypeName("unsigned int[4]")]
    public fixed uint flags[4];
}
