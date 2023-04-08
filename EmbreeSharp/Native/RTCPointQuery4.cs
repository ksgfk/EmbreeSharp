using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential, Pack = 16)]
public unsafe partial struct RTCPointQuery4
{
    [NativeTypeName("float[4]")]
    public fixed float x[4];

    [NativeTypeName("float[4]")]
    public fixed float y[4];

    [NativeTypeName("float[4]")]
    public fixed float z[4];

    [NativeTypeName("float[4]")]
    public fixed float time[4];

    [NativeTypeName("float[4]")]
    public fixed float radius[4];
}
