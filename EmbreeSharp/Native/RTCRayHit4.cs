using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential)]
public partial struct RTCRayHit4
{
    [NativeTypeName("struct RTCRay4")]
    public RTCRay4 ray;

    [NativeTypeName("struct RTCHit4")]
    public RTCHit4 hit;
}
