using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential)]
    public struct RTCLinearBounds
    {
        public const int Alignment = 16;

        public RTCBounds bounds0;
        public RTCBounds bounds1;
    }
}
