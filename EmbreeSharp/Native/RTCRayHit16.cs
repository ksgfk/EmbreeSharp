namespace EmbreeSharp.Native
{
    /// <summary>
    /// Combined ray/hit structure for a packet of 16 rays
    /// </summary>
    [RTCAlign(Alignment)]
    public struct RTCRayHit16
    {
        public const int Alignment = 64;

        public RTCRay16 ray;
        public RTCHit16 hit;
    }
}
