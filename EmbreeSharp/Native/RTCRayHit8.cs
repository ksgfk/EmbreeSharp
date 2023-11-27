namespace EmbreeSharp.Native
{
    /// <summary>
    /// Combined ray/hit structure for a packet of 8 rays
    /// </summary>
    [RTCAlign(Alignment)]
    public struct RTCRayHit8
    {
        public const int Alignment = 32;

        public RTCRay8 ray;
        public RTCHit8 hit;
    }
}
