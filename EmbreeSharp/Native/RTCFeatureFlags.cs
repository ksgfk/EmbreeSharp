using System;

namespace EmbreeSharp.Native;

[Flags]
public enum RTCFeatureFlags : int
{
    RTC_FEATURE_FLAG_NONE = 0,
    RTC_FEATURE_FLAG_MOTION_BLUR = 1 << 0,
    RTC_FEATURE_FLAG_TRIANGLE = 1 << 1,
    RTC_FEATURE_FLAG_QUAD = 1 << 2,
    RTC_FEATURE_FLAG_GRID = 1 << 3,
    RTC_FEATURE_FLAG_SUBDIVISION = 1 << 4,
    RTC_FEATURE_FLAG_CONE_LINEAR_CURVE = 1 << 5,
    RTC_FEATURE_FLAG_ROUND_LINEAR_CURVE = 1 << 6,
    RTC_FEATURE_FLAG_FLAT_LINEAR_CURVE = 1 << 7,
    RTC_FEATURE_FLAG_ROUND_BEZIER_CURVE = 1 << 8,
    RTC_FEATURE_FLAG_FLAT_BEZIER_CURVE = 1 << 9,
    RTC_FEATURE_FLAG_NORMAL_ORIENTED_BEZIER_CURVE = 1 << 10,
    RTC_FEATURE_FLAG_ROUND_BSPLINE_CURVE = 1 << 11,
    RTC_FEATURE_FLAG_FLAT_BSPLINE_CURVE = 1 << 12,
    RTC_FEATURE_FLAG_NORMAL_ORIENTED_BSPLINE_CURVE = 1 << 13,
    RTC_FEATURE_FLAG_ROUND_HERMITE_CURVE = 1 << 14,
    RTC_FEATURE_FLAG_FLAT_HERMITE_CURVE = 1 << 15,
    RTC_FEATURE_FLAG_NORMAL_ORIENTED_HERMITE_CURVE = 1 << 16,
    RTC_FEATURE_FLAG_ROUND_CATMULL_ROM_CURVE = 1 << 17,
    RTC_FEATURE_FLAG_FLAT_CATMULL_ROM_CURVE = 1 << 18,
    RTC_FEATURE_FLAG_NORMAL_ORIENTED_CATMULL_ROM_CURVE = 1 << 19,
    RTC_FEATURE_FLAG_SPHERE_POINT = 1 << 20,
    RTC_FEATURE_FLAG_DISC_POINT = 1 << 21,
    RTC_FEATURE_FLAG_ORIENTED_DISC_POINT = 1 << 22,
    RTC_FEATURE_FLAG_POINT = RTC_FEATURE_FLAG_SPHERE_POINT | RTC_FEATURE_FLAG_DISC_POINT | RTC_FEATURE_FLAG_ORIENTED_DISC_POINT,
    RTC_FEATURE_FLAG_ROUND_CURVES = RTC_FEATURE_FLAG_ROUND_LINEAR_CURVE | RTC_FEATURE_FLAG_ROUND_BEZIER_CURVE | RTC_FEATURE_FLAG_ROUND_BSPLINE_CURVE | RTC_FEATURE_FLAG_ROUND_HERMITE_CURVE | RTC_FEATURE_FLAG_ROUND_CATMULL_ROM_CURVE,
    RTC_FEATURE_FLAG_FLAT_CURVES = RTC_FEATURE_FLAG_FLAT_LINEAR_CURVE | RTC_FEATURE_FLAG_FLAT_BEZIER_CURVE | RTC_FEATURE_FLAG_FLAT_BSPLINE_CURVE | RTC_FEATURE_FLAG_FLAT_HERMITE_CURVE | RTC_FEATURE_FLAG_FLAT_CATMULL_ROM_CURVE,
    RTC_FEATURE_FLAG_NORMAL_ORIENTED_CURVES = RTC_FEATURE_FLAG_NORMAL_ORIENTED_BEZIER_CURVE | RTC_FEATURE_FLAG_NORMAL_ORIENTED_BSPLINE_CURVE | RTC_FEATURE_FLAG_NORMAL_ORIENTED_HERMITE_CURVE | RTC_FEATURE_FLAG_NORMAL_ORIENTED_CATMULL_ROM_CURVE,
    RTC_FEATURE_FLAG_LINEAR_CURVES = RTC_FEATURE_FLAG_CONE_LINEAR_CURVE | RTC_FEATURE_FLAG_ROUND_LINEAR_CURVE | RTC_FEATURE_FLAG_FLAT_LINEAR_CURVE,
    RTC_FEATURE_FLAG_BEZIER_CURVES = RTC_FEATURE_FLAG_ROUND_BEZIER_CURVE | RTC_FEATURE_FLAG_FLAT_BEZIER_CURVE | RTC_FEATURE_FLAG_NORMAL_ORIENTED_BEZIER_CURVE,
    RTC_FEATURE_FLAG_BSPLINE_CURVES = RTC_FEATURE_FLAG_ROUND_BSPLINE_CURVE | RTC_FEATURE_FLAG_FLAT_BSPLINE_CURVE | RTC_FEATURE_FLAG_NORMAL_ORIENTED_BSPLINE_CURVE,
    RTC_FEATURE_FLAG_HERMITE_CURVES = RTC_FEATURE_FLAG_ROUND_HERMITE_CURVE | RTC_FEATURE_FLAG_FLAT_HERMITE_CURVE | RTC_FEATURE_FLAG_NORMAL_ORIENTED_HERMITE_CURVE,
    RTC_FEATURE_FLAG_CURVES = RTC_FEATURE_FLAG_CONE_LINEAR_CURVE | RTC_FEATURE_FLAG_ROUND_LINEAR_CURVE | RTC_FEATURE_FLAG_FLAT_LINEAR_CURVE | RTC_FEATURE_FLAG_ROUND_BEZIER_CURVE | RTC_FEATURE_FLAG_FLAT_BEZIER_CURVE | RTC_FEATURE_FLAG_NORMAL_ORIENTED_BEZIER_CURVE | RTC_FEATURE_FLAG_ROUND_BSPLINE_CURVE | RTC_FEATURE_FLAG_FLAT_BSPLINE_CURVE | RTC_FEATURE_FLAG_NORMAL_ORIENTED_BSPLINE_CURVE | RTC_FEATURE_FLAG_ROUND_HERMITE_CURVE | RTC_FEATURE_FLAG_FLAT_HERMITE_CURVE | RTC_FEATURE_FLAG_NORMAL_ORIENTED_HERMITE_CURVE | RTC_FEATURE_FLAG_ROUND_CATMULL_ROM_CURVE | RTC_FEATURE_FLAG_FLAT_CATMULL_ROM_CURVE | RTC_FEATURE_FLAG_NORMAL_ORIENTED_CATMULL_ROM_CURVE,
    RTC_FEATURE_FLAG_INSTANCE = 1 << 23,
    RTC_FEATURE_FLAG_FILTER_FUNCTION_IN_ARGUMENTS = 1 << 24,
    RTC_FEATURE_FLAG_FILTER_FUNCTION_IN_GEOMETRY = 1 << 25,
    RTC_FEATURE_FLAG_FILTER_FUNCTION = RTC_FEATURE_FLAG_FILTER_FUNCTION_IN_ARGUMENTS | RTC_FEATURE_FLAG_FILTER_FUNCTION_IN_GEOMETRY,
    RTC_FEATURE_FLAG_USER_GEOMETRY_CALLBACK_IN_ARGUMENTS = 1 << 26,
    RTC_FEATURE_FLAG_USER_GEOMETRY_CALLBACK_IN_GEOMETRY = 1 << 27,
    RTC_FEATURE_FLAG_USER_GEOMETRY = RTC_FEATURE_FLAG_USER_GEOMETRY_CALLBACK_IN_ARGUMENTS | RTC_FEATURE_FLAG_USER_GEOMETRY_CALLBACK_IN_GEOMETRY,
    RTC_FEATURE_FLAG_32_BIT_RAY_MASK = 1 << 28,
    RTC_FEATURE_FLAG_ALL = unchecked((int)(0xffffffff)),
}
