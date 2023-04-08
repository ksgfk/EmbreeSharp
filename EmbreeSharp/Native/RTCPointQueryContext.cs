using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential, Pack = 16)]
public unsafe partial struct RTCPointQueryContext
{
    [NativeTypeName("float[1][16]")]
    public fixed float world2inst[1 * 16];

    [NativeTypeName("float[1][16]")]
    public fixed float inst2world[1 * 16];

    [NativeTypeName("unsigned int[1]")]
    public fixed uint instID[1];

    [NativeTypeName("unsigned int")]
    public uint instStackSize;
}
