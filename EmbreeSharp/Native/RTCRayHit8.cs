using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential)]
public partial struct RTCRayHit8
{
    [NativeTypeName("struct RTCRay8")]
    public RTCRay8 ray;

    [NativeTypeName("struct RTCHit8")]
    public RTCHit8 hit;
}
