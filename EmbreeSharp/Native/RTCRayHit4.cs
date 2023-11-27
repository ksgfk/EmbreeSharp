namespace EmbreeSharp.Native
{
    /// <summary>
    /// Combined ray/hit structure for a packet of 4 rays
    /// </summary>
    [RTCAlign(Alignment)]
    public struct RTCRayHit4
    {
        public const int Alignment = 16;

        public RTCRay4 ray;
        public RTCHit4 hit;
    }
}
