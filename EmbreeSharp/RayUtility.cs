using EmbreeSharp.Native;
using System.Numerics;
using static EmbreeSharp.Native.EmbreeNative;

namespace EmbreeSharp;

public static class RayUtility
{
    public static RTCRayHit InitRayHit()
    {
        RTCRayHit rayhit = default;
        rayhit.ray.org_x = 0;
        rayhit.ray.org_y = 0;
        rayhit.ray.org_z = 0;
        rayhit.ray.dir_x = 0;
        rayhit.ray.dir_y = 0;
        rayhit.ray.dir_z = 0;
        rayhit.ray.time = 0;
        rayhit.ray.tnear = 0;
        rayhit.ray.tfar = float.PositiveInfinity;
        rayhit.ray.mask = uint.MaxValue;
        rayhit.ray.flags = 0;
        rayhit.hit.geomID = RTC_INVALID_GEOMETRY_ID;
        unsafe
        {
            rayhit.hit.instID[0] = RTC_INVALID_GEOMETRY_ID;
        }
        return rayhit;
    }

    public static RTCRayHit InitRayHit(float originX, float originY, float originZ, float dirX, float dirY, float dirZ, float near = 0, float far = float.PositiveInfinity, float time = 0)
    {
        RTCRayHit rayhit = default;
        rayhit.ray.org_x = originX;
        rayhit.ray.org_y = originY;
        rayhit.ray.org_z = originZ;
        rayhit.ray.dir_x = dirX;
        rayhit.ray.dir_y = dirY;
        rayhit.ray.dir_z = dirZ;
        rayhit.ray.time = time;
        rayhit.ray.tnear = near;
        rayhit.ray.tfar = far;
        rayhit.ray.mask = uint.MaxValue;
        rayhit.ray.flags = 0;
        rayhit.hit.geomID = RTC_INVALID_GEOMETRY_ID;
        unsafe
        {
            rayhit.hit.instID[0] = RTC_INVALID_GEOMETRY_ID;
        }
        return rayhit;
    }

    public static RTCRayHit InitRayHit(in Vector3 origin, in Vector3 dir, float near = 0, float far = float.PositiveInfinity, float time = 0)
    {
        RTCRayHit rayhit = default;
        rayhit.ray.org_x = origin.X;
        rayhit.ray.org_y = origin.Y;
        rayhit.ray.org_z = origin.Z;
        rayhit.ray.dir_x = dir.X;
        rayhit.ray.dir_y = dir.Y;
        rayhit.ray.dir_z = dir.Z;
        rayhit.ray.time = time;
        rayhit.ray.tnear = near;
        rayhit.ray.tfar = far;
        rayhit.ray.mask = uint.MaxValue;
        rayhit.ray.flags = 0;
        rayhit.hit.geomID = RTC_INVALID_GEOMETRY_ID;
        unsafe
        {
            rayhit.hit.instID[0] = RTC_INVALID_GEOMETRY_ID;
        }
        return rayhit;
    }

    public static bool IsHit(in RTCRayHit rayhit)
    {
        return rayhit.hit.geomID != RTC_INVALID_GEOMETRY_ID;
    }
}
