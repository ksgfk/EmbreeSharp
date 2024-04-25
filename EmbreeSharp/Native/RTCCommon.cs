using System;
using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    /// <summary>
    /// Formats of buffers and other data structures
    /// </summary>
    public enum RTCFormat
    {
        RTC_FORMAT_UNDEFINED = 0,

        /* 8-bit unsigned integer */
        RTC_FORMAT_UCHAR = 0x1001,
        RTC_FORMAT_UCHAR2,
        RTC_FORMAT_UCHAR3,
        RTC_FORMAT_UCHAR4,

        /* 8-bit signed integer */
        RTC_FORMAT_CHAR = 0x2001,
        RTC_FORMAT_CHAR2,
        RTC_FORMAT_CHAR3,
        RTC_FORMAT_CHAR4,

        /* 16-bit unsigned integer */
        RTC_FORMAT_USHORT = 0x3001,
        RTC_FORMAT_USHORT2,
        RTC_FORMAT_USHORT3,
        RTC_FORMAT_USHORT4,

        /* 16-bit signed integer */
        RTC_FORMAT_SHORT = 0x4001,
        RTC_FORMAT_SHORT2,
        RTC_FORMAT_SHORT3,
        RTC_FORMAT_SHORT4,

        /* 32-bit unsigned integer */
        RTC_FORMAT_UINT = 0x5001,
        RTC_FORMAT_UINT2,
        RTC_FORMAT_UINT3,
        RTC_FORMAT_UINT4,

        /* 32-bit signed integer */
        RTC_FORMAT_INT = 0x6001,
        RTC_FORMAT_INT2,
        RTC_FORMAT_INT3,
        RTC_FORMAT_INT4,

        /* 64-bit unsigned integer */
        RTC_FORMAT_ULLONG = 0x7001,
        RTC_FORMAT_ULLONG2,
        RTC_FORMAT_ULLONG3,
        RTC_FORMAT_ULLONG4,

        /* 64-bit signed integer */
        RTC_FORMAT_LLONG = 0x8001,
        RTC_FORMAT_LLONG2,
        RTC_FORMAT_LLONG3,
        RTC_FORMAT_LLONG4,

        /* 32-bit float */
        RTC_FORMAT_FLOAT = 0x9001,
        RTC_FORMAT_FLOAT2,
        RTC_FORMAT_FLOAT3,
        RTC_FORMAT_FLOAT4,
        RTC_FORMAT_FLOAT5,
        RTC_FORMAT_FLOAT6,
        RTC_FORMAT_FLOAT7,
        RTC_FORMAT_FLOAT8,
        RTC_FORMAT_FLOAT9,
        RTC_FORMAT_FLOAT10,
        RTC_FORMAT_FLOAT11,
        RTC_FORMAT_FLOAT12,
        RTC_FORMAT_FLOAT13,
        RTC_FORMAT_FLOAT14,
        RTC_FORMAT_FLOAT15,
        RTC_FORMAT_FLOAT16,

        /* 32-bit float matrix (row-major order) */
        RTC_FORMAT_FLOAT2X2_ROW_MAJOR = 0x9122,
        RTC_FORMAT_FLOAT2X3_ROW_MAJOR = 0x9123,
        RTC_FORMAT_FLOAT2X4_ROW_MAJOR = 0x9124,
        RTC_FORMAT_FLOAT3X2_ROW_MAJOR = 0x9132,
        RTC_FORMAT_FLOAT3X3_ROW_MAJOR = 0x9133,
        RTC_FORMAT_FLOAT3X4_ROW_MAJOR = 0x9134,
        RTC_FORMAT_FLOAT4X2_ROW_MAJOR = 0x9142,
        RTC_FORMAT_FLOAT4X3_ROW_MAJOR = 0x9143,
        RTC_FORMAT_FLOAT4X4_ROW_MAJOR = 0x9144,

        /* 32-bit float matrix (column-major order) */
        RTC_FORMAT_FLOAT2X2_COLUMN_MAJOR = 0x9222,
        RTC_FORMAT_FLOAT2X3_COLUMN_MAJOR = 0x9223,
        RTC_FORMAT_FLOAT2X4_COLUMN_MAJOR = 0x9224,
        RTC_FORMAT_FLOAT3X2_COLUMN_MAJOR = 0x9232,
        RTC_FORMAT_FLOAT3X3_COLUMN_MAJOR = 0x9233,
        RTC_FORMAT_FLOAT3X4_COLUMN_MAJOR = 0x9234,
        RTC_FORMAT_FLOAT4X2_COLUMN_MAJOR = 0x9242,
        RTC_FORMAT_FLOAT4X3_COLUMN_MAJOR = 0x9243,
        RTC_FORMAT_FLOAT4X4_COLUMN_MAJOR = 0x9244,

        /* special 12-byte format for grids */
        RTC_FORMAT_GRID = 0xA001,

        RTC_FORMAT_QUATERNION_DECOMPOSITION = 0xB001,
    }

    /// <summary>
    /// Build quality levels
    /// </summary>
    public enum RTCBuildQuality
    {
        RTC_BUILD_QUALITY_LOW = 0,
        RTC_BUILD_QUALITY_MEDIUM = 1,
        RTC_BUILD_QUALITY_HIGH = 2,
        RTC_BUILD_QUALITY_REFIT = 3,
    }

    /// <summary>
    /// Axis-aligned bounding box representation
    /// </summary>
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential)]
    public struct RTCBounds
    {
        public const int Alignment = 16;

        public float lower_x, lower_y, lower_z, align0;
        public float upper_x, upper_y, upper_z, align1;
    }

    /// <summary>
    /// Linear axis-aligned bounding box representation
    /// </summary>
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential)]
    public struct RTCLinearBounds
    {
        public const int Alignment = 16;

        public RTCBounds bounds0;
        public RTCBounds bounds1;
    }

    /// <summary>
    /// Feature flags for SYCL specialization constants
    /// </summary>
    [Flags]
    public enum RTCFeatureFlags : uint
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

        RTC_FEATURE_FLAG_POINT =
          RTC_FEATURE_FLAG_SPHERE_POINT |
          RTC_FEATURE_FLAG_DISC_POINT |
          RTC_FEATURE_FLAG_ORIENTED_DISC_POINT,

        RTC_FEATURE_FLAG_ROUND_CURVES =
          RTC_FEATURE_FLAG_ROUND_LINEAR_CURVE |
          RTC_FEATURE_FLAG_ROUND_BEZIER_CURVE |
          RTC_FEATURE_FLAG_ROUND_BSPLINE_CURVE |
          RTC_FEATURE_FLAG_ROUND_HERMITE_CURVE |
          RTC_FEATURE_FLAG_ROUND_CATMULL_ROM_CURVE,

        RTC_FEATURE_FLAG_FLAT_CURVES =
          RTC_FEATURE_FLAG_FLAT_LINEAR_CURVE |
          RTC_FEATURE_FLAG_FLAT_BEZIER_CURVE |
          RTC_FEATURE_FLAG_FLAT_BSPLINE_CURVE |
          RTC_FEATURE_FLAG_FLAT_HERMITE_CURVE |
          RTC_FEATURE_FLAG_FLAT_CATMULL_ROM_CURVE,

        RTC_FEATURE_FLAG_NORMAL_ORIENTED_CURVES =
          RTC_FEATURE_FLAG_NORMAL_ORIENTED_BEZIER_CURVE |
          RTC_FEATURE_FLAG_NORMAL_ORIENTED_BSPLINE_CURVE |
          RTC_FEATURE_FLAG_NORMAL_ORIENTED_HERMITE_CURVE |
          RTC_FEATURE_FLAG_NORMAL_ORIENTED_CATMULL_ROM_CURVE,

        RTC_FEATURE_FLAG_LINEAR_CURVES =
          RTC_FEATURE_FLAG_CONE_LINEAR_CURVE |
          RTC_FEATURE_FLAG_ROUND_LINEAR_CURVE |
          RTC_FEATURE_FLAG_FLAT_LINEAR_CURVE,

        RTC_FEATURE_FLAG_BEZIER_CURVES =
          RTC_FEATURE_FLAG_ROUND_BEZIER_CURVE |
          RTC_FEATURE_FLAG_FLAT_BEZIER_CURVE |
          RTC_FEATURE_FLAG_NORMAL_ORIENTED_BEZIER_CURVE,

        RTC_FEATURE_FLAG_BSPLINE_CURVES =
          RTC_FEATURE_FLAG_ROUND_BSPLINE_CURVE |
          RTC_FEATURE_FLAG_FLAT_BSPLINE_CURVE |
          RTC_FEATURE_FLAG_NORMAL_ORIENTED_BSPLINE_CURVE,

        RTC_FEATURE_FLAG_HERMITE_CURVES =
          RTC_FEATURE_FLAG_ROUND_HERMITE_CURVE |
          RTC_FEATURE_FLAG_FLAT_HERMITE_CURVE |
          RTC_FEATURE_FLAG_NORMAL_ORIENTED_HERMITE_CURVE,

        RTC_FEATURE_FLAG_CURVES =
          RTC_FEATURE_FLAG_CONE_LINEAR_CURVE |
          RTC_FEATURE_FLAG_ROUND_LINEAR_CURVE |
          RTC_FEATURE_FLAG_FLAT_LINEAR_CURVE |
          RTC_FEATURE_FLAG_ROUND_BEZIER_CURVE |
          RTC_FEATURE_FLAG_FLAT_BEZIER_CURVE |
          RTC_FEATURE_FLAG_NORMAL_ORIENTED_BEZIER_CURVE |
          RTC_FEATURE_FLAG_ROUND_BSPLINE_CURVE |
          RTC_FEATURE_FLAG_FLAT_BSPLINE_CURVE |
          RTC_FEATURE_FLAG_NORMAL_ORIENTED_BSPLINE_CURVE |
          RTC_FEATURE_FLAG_ROUND_HERMITE_CURVE |
          RTC_FEATURE_FLAG_FLAT_HERMITE_CURVE |
          RTC_FEATURE_FLAG_NORMAL_ORIENTED_HERMITE_CURVE |
          RTC_FEATURE_FLAG_ROUND_CATMULL_ROM_CURVE |
          RTC_FEATURE_FLAG_FLAT_CATMULL_ROM_CURVE |
          RTC_FEATURE_FLAG_NORMAL_ORIENTED_CATMULL_ROM_CURVE,

        RTC_FEATURE_FLAG_INSTANCE = 1 << 23,

        RTC_FEATURE_FLAG_FILTER_FUNCTION_IN_ARGUMENTS = 1 << 24,
        RTC_FEATURE_FLAG_FILTER_FUNCTION_IN_GEOMETRY = 1 << 25,

        RTC_FEATURE_FLAG_FILTER_FUNCTION =
          RTC_FEATURE_FLAG_FILTER_FUNCTION_IN_ARGUMENTS |
          RTC_FEATURE_FLAG_FILTER_FUNCTION_IN_GEOMETRY,

        RTC_FEATURE_FLAG_USER_GEOMETRY_CALLBACK_IN_ARGUMENTS = 1 << 26,
        RTC_FEATURE_FLAG_USER_GEOMETRY_CALLBACK_IN_GEOMETRY = 1 << 27,

        RTC_FEATURE_FLAG_USER_GEOMETRY =
          RTC_FEATURE_FLAG_USER_GEOMETRY_CALLBACK_IN_ARGUMENTS |
          RTC_FEATURE_FLAG_USER_GEOMETRY_CALLBACK_IN_GEOMETRY,

        RTC_FEATURE_FLAG_32_BIT_RAY_MASK = 1 << 28,

        RTC_FEATURE_FLAG_INSTANCE_ARRAY = 1 << 29,

        RTC_FEATURE_FLAG_ALL = 0xffffffff,
    }

    /// <summary>
    /// Ray query flags
    /// </summary>
    [Flags]
    public enum RTCRayQueryFlags
    {
        /// <summary>
        /// matching intel_ray_flags_t layout
        /// </summary>
        RTC_RAY_QUERY_FLAG_NONE = 0,
        /// <summary>
        /// enable argument filter for each geometry
        /// </summary>
        RTC_RAY_QUERY_FLAG_INVOKE_ARGUMENT_FILTER = 1 << 1,

        /// <summary>
        /// optimize for incoherent rays
        /// </summary>
        RTC_RAY_QUERY_FLAG_INCOHERENT = 0 << 16,
        /// <summary>
        /// optimize for coherent rays
        /// </summary>
        RTC_RAY_QUERY_FLAG_COHERENT = 1 << 16,
    }

    /// <summary>
    /// Arguments for RTCFilterFunctionN
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RTCFilterFunctionNArguments
    {
        public int* valid;
        public void* geometryUserPtr;
        public RTCRayQueryContext* context;
        [NativeType("RTCRayN*")] public RTCRayN ray;
        [NativeType("RTCHitN*")] public RTCHitN hit;
        public uint N;
    }

    /// <summary>
    /// Filter callback function
    /// </summary>
    public unsafe delegate void RTCFilterFunctionN([NativeType("const struct RTCFilterFunctionNArguments*")] RTCFilterFunctionNArguments* args);

    /// <summary>
    /// Intersection callback function
    /// </summary>
    public unsafe delegate void RTCIntersectFunctionN([NativeType("const struct RTCIntersectFunctionNArguments*")] RTCIntersectFunctionNArguments* args);

    /// <summary>
    /// Occlusion callback function
    /// </summary>
    public unsafe delegate void RTCOccludedFunctionN([NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args);

    /// <summary>
    /// Ray query context passed to intersect/occluded calls
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RTCRayQueryContext
    {
        /// <summary>
        /// The current stack of instance ids.
        /// </summary>
        public fixed uint instID[EmbreeNative.RTC_MAX_INSTANCE_LEVEL_COUNT];

        /// <summary>
        /// The current stack of instance primitive ids.
        /// </summary>
        public fixed uint instPrimID[EmbreeNative.RTC_MAX_INSTANCE_LEVEL_COUNT];
    }

    public static unsafe partial class EmbreeNative
    {
        /// <summary>
        /// Initializes an ray query context.
        /// </summary>
        public static void rtcInitRayQueryContext(RTCRayQueryContext* context)
        {
            for (int l = 0; l < RTC_MAX_INSTANCE_LEVEL_COUNT; l++)
            {
                context->instID[l] = RTC_INVALID_GEOMETRY_ID;
                context->instPrimID[l] = RTC_INVALID_GEOMETRY_ID;
            }
        }
    }

    /// <summary>
    /// Point query structure for closest point query
    /// </summary>
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential, Size = 32)]
    public struct RTCPointQuery
    {
        public const int Alignment = 16;

        /// <summary>
        /// x coordinate of the query point
        /// </summary>
        public float x;
        /// <summary>
        /// y coordinate of the query point
        /// </summary>
        public float y;
        /// <summary>
        /// z coordinate of the query point
        /// </summary>
        public float z;
        /// <summary>
        /// time of the point query
        /// </summary>
        public float time;
        /// <summary>
        /// radius of the point query 
        /// </summary>
        public float radius;
    }

    /// <summary>
    /// Structure of a packet of 4 query points
    /// </summary>
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RTCPointQuery4
    {
        public const int Alignment = 16;

        /// <summary>
        /// x coordinate of the query point
        /// </summary>
        public fixed float x[4];
        /// <summary>
        /// y coordinate of the query point
        /// </summary>
        public fixed float y[4];
        /// <summary>
        /// z coordinate of the query point
        /// </summary>
        public fixed float z[4];
        /// <summary>
        /// time of the point query
        /// </summary>
        public fixed float time[4];
        /// <summary>
        /// radius of the point query 
        /// </summary>
        public fixed float radius[4];
    }

    /// <summary>
    /// Structure of a packet of 8 query points
    /// </summary>
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RTCPointQuery8
    {
        public const int Alignment = 32;

        /// <summary>
        /// x coordinate of the query point
        /// </summary>
        public fixed float x[8];
        /// <summary>
        /// y coordinate of the query point
        /// </summary>
        public fixed float y[8];
        /// <summary>
        /// z coordinate of the query point
        /// </summary>
        public fixed float z[8];
        /// <summary>
        /// time of the point query
        /// </summary>
        public fixed float time[8];
        /// <summary>
        /// radius of the point query 
        /// </summary>
        public fixed float radius[8];
    }

    /// <summary>
    /// Structure of a packet of 16 query points
    /// </summary>
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RTCPointQuery16
    {
        public const int Alignment = 64;

        /// <summary>
        /// x coordinate of the query point
        /// </summary>
        public fixed float x[16];
        /// <summary>
        /// y coordinate of the query point
        /// </summary>
        public fixed float y[16];
        /// <summary>
        /// z coordinate of the query point
        /// </summary>
        public fixed float z[16];
        /// <summary>
        /// time of the point query
        /// </summary>
        public fixed float time[16];
        /// <summary>
        /// radius of the point query 
        /// </summary>
        public fixed float radius[16];
    }

    public struct RTCPointQueryN
    {
        public IntPtr Ptr;
    }

    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential, Size = 144)]
    public unsafe struct RTCPointQueryContext
    {
        public const int Alignment = 16;

        /// <summary>
        /// accumulated 4x4 column major matrices from world space to instance space.
        /// undefined if size == 0.
        /// </summary>
        public fixed float world2inst[EmbreeNative.RTC_MAX_INSTANCE_LEVEL_COUNT * 16];
        /// <summary>
        /// accumulated 4x4 column major matrices from instance space to world space.
        /// undefined if size == 0.
        /// </summary>
        public fixed float inst2world[EmbreeNative.RTC_MAX_INSTANCE_LEVEL_COUNT * 16];
        /// <summary>
        /// instance ids.
        /// </summary>
        public fixed uint instID[EmbreeNative.RTC_MAX_INSTANCE_LEVEL_COUNT];
        /// <summary>
        /// instance prim ids.
        /// </summary>
        public fixed uint instPrimID[EmbreeNative.RTC_MAX_INSTANCE_LEVEL_COUNT];
        /// <summary>
        /// number of instances currently on the stack.
        /// </summary>
        public uint instStackSize;
    }

    public static unsafe partial class EmbreeNative
    {
        /// <summary>
        /// Initializes an ray query context.
        /// </summary>
        public static void rtcInitPointQueryContext(RTCPointQueryContext* context)
        {
            context->instStackSize = 0;
            for (int l = 0; l < RTC_MAX_INSTANCE_LEVEL_COUNT; ++l)
            {
                context->instID[l] = RTC_INVALID_GEOMETRY_ID;
                context->instPrimID[l] = RTC_INVALID_GEOMETRY_ID;
            }
        }
    }

    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential, Size = 48)]
    public unsafe struct RTCPointQueryFunctionArguments
    {
        public const int Alignment = 16;

        /// <summary>
        /// The (world space) query object that was passed as an argument of rtcPointQuery. The
        /// radius of the query can be decreased inside the callback to shrink the
        /// search domain. Increasing the radius or modifying the time or position of
        /// the query results in undefined behaviour.
        /// </summary>
        public RTCPointQuery* query;
        /// <summary>
        /// Used for user input/output data. Will not be read or modified internally.
        /// </summary>
        public void* userPtr;
        /// <summary>
        /// primitive ID of primitive
        /// </summary>
        public uint primID;
        /// <summary>
        /// geometry ID of primitive
        /// </summary>
        public uint geomID;
        /// <summary>
        /// the context with transformation and instance ID stack
        /// </summary>
        public RTCPointQueryContext* context;
        /// <summary>
        /// If the current instance transform M (= context->world2inst[context->instStackSize]) 
        /// is a similarity matrix, i.e there is a constant factor similarityScale such that
        /// for all x,y: dist(Mx, My) = similarityScale * dist(x, y),
        /// The similarity scale is 0, if the current instance transform is not a
        /// similarity transform and vice versa. The similarity scale allows to compute
        /// distance information in instance space and scale the distances into world
        /// space by dividing with the similarity scale, for example, to update the
        /// query radius. If the current instance transform is not a similarity
        /// transform (similarityScale = 0), the distance computation has to be
        /// performed in world space to ensure correctness. if there is no instance
        /// transform (context->instStackSize == 0), the similarity scale is 1.
        /// </summary>
        public float similarityScale;
    }

    public unsafe delegate bool RTCPointQueryFunction(RTCPointQueryFunctionArguments* args);
}
