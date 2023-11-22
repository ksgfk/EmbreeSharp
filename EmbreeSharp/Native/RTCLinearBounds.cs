using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    [RTCAlign(16)]
    [StructLayout(LayoutKind.Sequential)]
    public struct RTCLinearBounds
    {
        public RTCBounds bounds0;
        public RTCBounds bounds1;
    };
}
