using EmbreeSharp.Native;

namespace EmbreeSharp
{
    public static class RtcRayUtility
    {
        public static RTCRayHit CreateRay(float ox, float oy, float oz, float dx, float dy, float dz, float tnear = 0, float tfar = float.PositiveInfinity)
        {
            RTCRayHit rayhit = new();
            rayhit.ray.org_x = ox;
            rayhit.ray.org_y = oy;
            rayhit.ray.org_z = oz;
            rayhit.ray.dir_x = dx;
            rayhit.ray.dir_y = dy;
            rayhit.ray.dir_z = dz;
            rayhit.ray.tnear = tnear;
            rayhit.ray.tfar = tfar;
            rayhit.ray.mask = unchecked((uint)-1);
            rayhit.ray.flags = 0;
            rayhit.hit.geomID = EmbreeNative.RTC_INVALID_GEOMETRY_ID;
            unsafe
            {
                rayhit.hit.instID[0] = EmbreeNative.RTC_INVALID_GEOMETRY_ID;
                rayhit.hit.instPrimID[0] = EmbreeNative.RTC_INVALID_GEOMETRY_ID;
            }
            return rayhit;
        }
    }

    public static class RtcRayExtension
    {
        public static bool IsHit(ref readonly this RTCRayHit rayhit)
        {
            return rayhit.hit.geomID != EmbreeNative.RTC_INVALID_GEOMETRY_ID;
        }
    }
}
