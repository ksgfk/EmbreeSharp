using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential)]
public partial struct RTCCollision
{
    [NativeTypeName("unsigned int")]
    public uint geomID0;

    [NativeTypeName("unsigned int")]
    public uint primID0;

    [NativeTypeName("unsigned int")]
    public uint geomID1;

    [NativeTypeName("unsigned int")]
    public uint primID1;
}
