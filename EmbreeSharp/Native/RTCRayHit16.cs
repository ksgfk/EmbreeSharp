using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential)]
public partial struct RTCRayHit16
{
    [NativeTypeName("struct RTCRay16")]
    public RTCRay16 ray;

    [NativeTypeName("struct RTCHit16")]
    public RTCHit16 hit;
}
