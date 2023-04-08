using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential, Pack = 16)]
public partial struct RTCLinearBounds
{
    [NativeTypeName("struct RTCBounds")]
    public RTCBounds bounds0;

    [NativeTypeName("struct RTCBounds")]
    public RTCBounds bounds1;
}
