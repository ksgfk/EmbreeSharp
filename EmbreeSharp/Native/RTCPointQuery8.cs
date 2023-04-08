using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential, Pack = 32)]
public unsafe partial struct RTCPointQuery8
{
    [NativeTypeName("float[8]")]
    public fixed float x[8];

    [NativeTypeName("float[8]")]
    public fixed float y[8];

    [NativeTypeName("float[8]")]
    public fixed float z[8];

    [NativeTypeName("float[8]")]
    public fixed float time[8];

    [NativeTypeName("float[8]")]
    public fixed float radius[8];
}
