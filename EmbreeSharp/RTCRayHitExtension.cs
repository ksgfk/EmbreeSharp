using EmbreeSharp.Native;

namespace EmbreeSharp
{
    public static class RTCRayHitExtension
    {
        public static bool IsHit(ref readonly this RTCRayHit rayhit)
        {
            return rayhit.hit.geomID != EmbreeNative.RTC_INVALID_GEOMETRY_ID;
        }
    }
}
