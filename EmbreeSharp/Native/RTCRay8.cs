using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential, Pack = 32)]
public unsafe partial struct RTCRay8
{
    [NativeTypeName("float[8]")]
    public fixed float org_x[8];

    [NativeTypeName("float[8]")]
    public fixed float org_y[8];

    [NativeTypeName("float[8]")]
    public fixed float org_z[8];

    [NativeTypeName("float[8]")]
    public fixed float tnear[8];

    [NativeTypeName("float[8]")]
    public fixed float dir_x[8];

    [NativeTypeName("float[8]")]
    public fixed float dir_y[8];

    [NativeTypeName("float[8]")]
    public fixed float dir_z[8];

    [NativeTypeName("float[8]")]
    public fixed float time[8];

    [NativeTypeName("float[8]")]
    public fixed float tfar[8];

    [NativeTypeName("unsigned int[8]")]
    public fixed uint mask[8];

    [NativeTypeName("unsigned int[8]")]
    public fixed uint id[8];

    [NativeTypeName("unsigned int[8]")]
    public fixed uint flags[8];
}
