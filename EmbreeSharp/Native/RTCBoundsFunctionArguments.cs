using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct RTCBoundsFunctionArguments
{
    public void* geometryUserPtr;

    [NativeTypeName("unsigned int")]
    public uint primID;

    [NativeTypeName("unsigned int")]
    public uint timeStep;

    [NativeTypeName("struct RTCBounds *")]
    public RTCBounds* bounds_o;
}
