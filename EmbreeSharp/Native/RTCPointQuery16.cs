using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential, Pack = 64)]
public unsafe partial struct RTCPointQuery16
{
    [NativeTypeName("float[16]")]
    public fixed float x[16];

    [NativeTypeName("float[16]")]
    public fixed float y[16];

    [NativeTypeName("float[16]")]
    public fixed float z[16];

    [NativeTypeName("float[16]")]
    public fixed float time[16];

    [NativeTypeName("float[16]")]
    public fixed float radius[16];
}
