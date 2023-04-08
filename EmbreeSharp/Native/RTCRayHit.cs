using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential)]
public partial struct RTCRayHit
{
    [NativeTypeName("struct RTCRay")]
    public RTCRay ray;

    [NativeTypeName("struct RTCHit")]
    public RTCHit hit;
}