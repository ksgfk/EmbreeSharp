using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential)]
public partial struct RTCGrid
{
    [NativeTypeName("unsigned int")]
    public uint startVertexID;

    [NativeTypeName("unsigned int")]
    public uint stride;

    [NativeTypeName("unsigned short")]
    public ushort width;

    [NativeTypeName("unsigned short")]
    public ushort height;
}
