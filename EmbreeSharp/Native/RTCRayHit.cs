namespace EmbreeSharp.Native
{
    /// <summary>
    /// Combined ray/hit structure for a single ray
    /// </summary>
    [RTCAlign(Alignment)]
    public struct RTCRayHit
    {
        public const int Alignment = 16;

        public RTCRay ray;
        public RTCHit hit;
    }
}
