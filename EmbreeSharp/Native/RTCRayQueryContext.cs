using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct RTCRayQueryContext
{
    [NativeTypeName("unsigned int[1]")]
    public fixed uint instID[1];
}
