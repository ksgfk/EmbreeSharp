namespace EmbreeSharp.Native;

public enum RTCDeviceProperty : int
{
    RTC_DEVICE_PROPERTY_VERSION = 0,
    RTC_DEVICE_PROPERTY_VERSION_MAJOR = 1,
    RTC_DEVICE_PROPERTY_VERSION_MINOR = 2,
    RTC_DEVICE_PROPERTY_VERSION_PATCH = 3,
    RTC_DEVICE_PROPERTY_NATIVE_RAY4_SUPPORTED = 32,
    RTC_DEVICE_PROPERTY_NATIVE_RAY8_SUPPORTED = 33,
    RTC_DEVICE_PROPERTY_NATIVE_RAY16_SUPPORTED = 34,
    RTC_DEVICE_PROPERTY_BACKFACE_CULLING_CURVES_ENABLED = 63,
    RTC_DEVICE_PROPERTY_RAY_MASK_SUPPORTED = 64,
    RTC_DEVICE_PROPERTY_BACKFACE_CULLING_ENABLED = 65,
    RTC_DEVICE_PROPERTY_FILTER_FUNCTION_SUPPORTED = 66,
    RTC_DEVICE_PROPERTY_IGNORE_INVALID_RAYS_ENABLED = 67,
    RTC_DEVICE_PROPERTY_COMPACT_POLYS_ENABLED = 68,
    RTC_DEVICE_PROPERTY_TRIANGLE_GEOMETRY_SUPPORTED = 96,
    RTC_DEVICE_PROPERTY_QUAD_GEOMETRY_SUPPORTED = 97,
    RTC_DEVICE_PROPERTY_SUBDIVISION_GEOMETRY_SUPPORTED = 98,
    RTC_DEVICE_PROPERTY_CURVE_GEOMETRY_SUPPORTED = 99,
    RTC_DEVICE_PROPERTY_USER_GEOMETRY_SUPPORTED = 100,
    RTC_DEVICE_PROPERTY_POINT_GEOMETRY_SUPPORTED = 101,
    RTC_DEVICE_PROPERTY_TASKING_SYSTEM = 128,
    RTC_DEVICE_PROPERTY_JOIN_COMMIT_SUPPORTED = 129,
    RTC_DEVICE_PROPERTY_PARALLEL_COMMIT_SUPPORTED = 130,
}
