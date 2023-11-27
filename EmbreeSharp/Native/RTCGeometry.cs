using System;
using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    public struct RTCScene
    {
        public IntPtr Ptr;
    }

    public struct RTCGeometry
    {
        public IntPtr Ptr;
    }

    /// <summary>
    /// Types of geometries
    /// </summary>
    public enum RTCGeometryType
    {
        /// <summary>
        /// triangle mesh
        /// </summary>
        RTC_GEOMETRY_TYPE_TRIANGLE = 0,
        /// <summary>
        /// quad (triangle pair) mesh
        /// </summary>
        RTC_GEOMETRY_TYPE_QUAD = 1,
        /// <summary>
        /// grid mesh
        /// </summary>
        RTC_GEOMETRY_TYPE_GRID = 2,

        /// <summary>
        /// Catmull-Clark subdivision surface
        /// </summary>
        RTC_GEOMETRY_TYPE_SUBDIVISION = 8,

        /// <summary>
        /// Cone linear curves - discontinuous at edge boundaries 
        /// </summary>
        RTC_GEOMETRY_TYPE_CONE_LINEAR_CURVE = 15,
        /// <summary>
        /// Round (rounded cone like) linear curves 
        /// </summary>
        RTC_GEOMETRY_TYPE_ROUND_LINEAR_CURVE = 16,
        /// <summary>
        /// flat (ribbon-like) linear curves
        /// </summary>
        RTC_GEOMETRY_TYPE_FLAT_LINEAR_CURVE = 17,

        /// <summary>
        /// round (tube-like) Bezier curves
        /// </summary>
        RTC_GEOMETRY_TYPE_ROUND_BEZIER_CURVE = 24,
        /// <summary>
        /// flat (ribbon-like) Bezier curves
        /// </summary>
        RTC_GEOMETRY_TYPE_FLAT_BEZIER_CURVE = 25,
        /// <summary>
        /// flat normal-oriented Bezier curves
        /// </summary>
        RTC_GEOMETRY_TYPE_NORMAL_ORIENTED_BEZIER_CURVE = 26,

        /// <summary>
        /// round (tube-like) B-spline curves
        /// </summary>
        RTC_GEOMETRY_TYPE_ROUND_BSPLINE_CURVE = 32,
        /// <summary>
        /// flat (ribbon-like) B-spline curves
        /// </summary>
        RTC_GEOMETRY_TYPE_FLAT_BSPLINE_CURVE = 33,
        /// <summary>
        /// flat normal-oriented B-spline curves
        /// </summary>
        RTC_GEOMETRY_TYPE_NORMAL_ORIENTED_BSPLINE_CURVE = 34,

        /// <summary>
        /// round (tube-like) Hermite curves
        /// </summary>
        RTC_GEOMETRY_TYPE_ROUND_HERMITE_CURVE = 40,
        /// <summary>
        /// flat (ribbon-like) Hermite curves
        /// </summary>
        RTC_GEOMETRY_TYPE_FLAT_HERMITE_CURVE = 41,
        /// <summary>
        /// flat normal-oriented Hermite curves
        /// </summary>
        RTC_GEOMETRY_TYPE_NORMAL_ORIENTED_HERMITE_CURVE = 42,

        RTC_GEOMETRY_TYPE_SPHERE_POINT = 50,
        RTC_GEOMETRY_TYPE_DISC_POINT = 51,
        RTC_GEOMETRY_TYPE_ORIENTED_DISC_POINT = 52,

        /// <summary>
        /// round (tube-like) Catmull-Rom curves
        /// </summary>
        RTC_GEOMETRY_TYPE_ROUND_CATMULL_ROM_CURVE = 58,
        /// <summary>
        /// flat (ribbon-like) Catmull-Rom curves
        /// </summary>
        RTC_GEOMETRY_TYPE_FLAT_CATMULL_ROM_CURVE = 59,
        /// <summary>
        /// flat normal-oriented Catmull-Rom curves
        /// </summary>
        RTC_GEOMETRY_TYPE_NORMAL_ORIENTED_CATMULL_ROM_CURVE = 60,

        /// <summary>
        /// user-defined geometry
        /// </summary>
        RTC_GEOMETRY_TYPE_USER = 120,
        /// <summary>
        /// scene instance
        /// </summary>
        RTC_GEOMETRY_TYPE_INSTANCE = 121,
        /// <summary>
        /// scene instance array
        /// </summary>
        RTC_GEOMETRY_TYPE_INSTANCE_ARRAY = 122,
    }

    /// <summary>
    /// Interpolation modes for subdivision surfaces
    /// </summary>
    public enum RTCSubdivisionMode
    {
        RTC_SUBDIVISION_MODE_NO_BOUNDARY = 0,
        RTC_SUBDIVISION_MODE_SMOOTH_BOUNDARY = 1,
        RTC_SUBDIVISION_MODE_PIN_CORNERS = 2,
        RTC_SUBDIVISION_MODE_PIN_BOUNDARY = 3,
        RTC_SUBDIVISION_MODE_PIN_ALL = 4,
    }


    /// <summary>
    /// Curve segment flags
    /// </summary>
    public enum RTCCurveFlags
    {
        /// <summary>
        /// left segments exists
        /// </summary>
        RTC_CURVE_FLAG_NEIGHBOR_LEFT = (1 << 0),
        /// <summary>
        /// right segment exists
        /// </summary>
        RTC_CURVE_FLAG_NEIGHBOR_RIGHT = (1 << 1)
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RTCBoundsFunctionArguments
    {
        public void* geometryUserPtr;
        public uint primID;
        public uint timeStep;
        [NativeType("struct RTCBounds*")] public RTCBounds* bounds_o;
    }

    /// <summary>
    /// Bounding callback function
    /// </summary>
    public unsafe delegate void RTCBoundsFunction([NativeType("const struct RTCBoundsFunctionArguments*")] RTCBoundsFunctionArguments* args);

    /// <summary>
    /// Arguments for RTCIntersectFunctionN
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RTCIntersectFunctionNArguments
    {
        public int* valid;
        public void* geometryUserPtr;
        public uint primID;
        [NativeType("struct RTCRayQueryContext*")] public RTCRayQueryContext* context;
        [NativeType("struct RTCRayHitN*")] public RTCRayHitN rayhit;
        public uint N;
        public uint geomID;
    }

    /// <summary>
    /// Arguments for RTCOccludedFunctionN
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RTCOccludedFunctionNArguments
    {
        public int* valid;
        public void* geometryUserPtr;
        public uint primID;
        [NativeType("struct RTCRayQueryContext*")] public RTCRayQueryContext* context;
        [NativeType("struct RTCRayN*")] public RTCRayN ray;
        public uint N;
        public uint geomID;
    }

    /// <summary>
    /// Arguments for RTCDisplacementFunctionN
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RTCDisplacementFunctionNArguments
    {
        public void* geometryUserPtr;
        public RTCGeometry geometry;
        public uint primID;
        public uint timeStep;
        [NativeType("const float*")] public float* u;
        [NativeType("const float*")] public float* v;
        [NativeType("const float*")] public float* Ng_x;
        [NativeType("const float*")] public float* Ng_y;
        [NativeType("const float*")] public float* Ng_z;
        public float* P_x;
        public float* P_y;
        public float* P_z;
        public uint N;
    }

    public unsafe delegate void RTCDisplacementFunctionN([NativeType("const struct RTCDisplacementFunctionNArguments*")] RTCDisplacementFunctionNArguments* args);

    public static unsafe partial class GlobalFunctions
    {
        /// <summary>
        /// Creates a new geometry of specified type.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern RTCGeometry rtcNewGeometry(RTCDevice device, RTCGeometryType type);
        /// <summary>
        /// Retains the geometry (increments the reference count).
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcRetainGeometry(RTCGeometry geometry);
        /// <summary>
        /// Releases the geometry (decrements the reference count)
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcReleaseGeometry(RTCGeometry geometry);
        /// <summary>
        /// Commits the geometry.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcCommitGeometry(RTCGeometry geometry);

        /// <summary>
        /// Enables the geometry.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcEnableGeometry(RTCGeometry geometry);
        /// <summary>
        /// Disables the geometry.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcDisableGeometry(RTCGeometry geometry);

        /// <summary>
        /// Sets the number of motion blur time steps of the geometry.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetGeometryTimeStepCount(RTCGeometry geometry, uint timeStepCount);
        /// <summary>
        /// Sets the motion blur time range of the geometry.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetGeometryTimeRange(RTCGeometry geometry, float startTime, float endTime);
        /// <summary>
        /// Sets the number of vertex attributes of the geometry.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetGeometryVertexAttributeCount(RTCGeometry geometry, uint vertexAttributeCount);
        /// <summary>
        /// Sets the ray mask of the geometry.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetGeometryMask(RTCGeometry geometry, uint mask);
        /// <summary>
        /// Sets the build quality of the geometry.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetGeometryBuildQuality(RTCGeometry geometry, RTCBuildQuality quality);
        /// <summary>
        /// Sets the maximal curve or point radius scale allowed by min-width feature.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetGeometryMaxRadiusScale(RTCGeometry geometry, float maxRadiusScale);

        /// <summary>
        /// Sets a geometry buffer.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetGeometryBuffer(RTCGeometry geometry, RTCBufferType type, uint slot, RTCFormat format, RTCBuffer buffer, [NativeType("size_t")] nuint byteOffset, [NativeType("size_t")] nuint byteStride, [NativeType("size_t")] nuint itemCount);
        /// <summary>
        /// Sets a shared geometry buffer.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetSharedGeometryBuffer(RTCGeometry geometry, RTCBufferType type, uint slot, RTCFormat format, [NativeType("const void*")] void* ptr, [NativeType("size_t")] nuint byteOffset, [NativeType("size_t")] nuint byteStride, [NativeType("size_t")] nuint itemCount);
        /// <summary>
        /// Creates and sets a new geometry buffer.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void* rtcSetNewGeometryBuffer(RTCGeometry geometry, RTCBufferType type, uint slot, RTCFormat format, [NativeType("size_t")] nuint byteStride, [NativeType("size_t")] nuint itemCount);
        /// <summary>
        /// Returns the pointer to the data of a buffer.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void* rtcGetGeometryBufferData(RTCGeometry geometry, RTCBufferType type, uint slot);
        /// <summary>
        /// Updates a geometry buffer.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcUpdateGeometryBuffer(RTCGeometry geometry, RTCBufferType type, uint slot);

        /// <summary>
        /// Sets the intersection filter callback function of the geometry.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetGeometryIntersectFilterFunction(RTCGeometry geometry, [NativeType("RTCFilterFunctionN")] IntPtr filter);
        /// <summary>
        /// Sets the occlusion filter callback function of the geometry.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetGeometryOccludedFilterFunction(RTCGeometry geometry, [NativeType("RTCFilterFunctionN")] IntPtr filter);
        /// <summary>
        /// Enables argument version of intersection or occlusion filter function.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetGeometryEnableFilterFunctionFromArguments(RTCGeometry geometry, bool enable);
        /// <summary>
        /// Sets the user-defined data pointer of the geometry.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetGeometryUserData(RTCGeometry geometry, void* ptr);
        /// <summary>
        /// Gets the user-defined data pointer of the geometry.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void* rtcGetGeometryUserData(RTCGeometry geometry);
        /// <summary>
        /// Set the point query callback function of a geometry.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetGeometryPointQueryFunction(RTCGeometry geometry, [NativeType("RTCPointQueryFunction")] IntPtr pointQuery);
        /// <summary>
        /// Sets the number of primitives of a user geometry.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetGeometryUserPrimitiveCount(RTCGeometry geometry, uint userPrimitiveCount);
        /// <summary>
        /// Sets the bounding callback function to calculate bounding boxes for user primitives.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetGeometryBoundsFunction(RTCGeometry geometry, [NativeType("RTCBoundsFunction")] IntPtr bounds, void* userPtr);
        /// <summary>
        /// Set the intersect callback function of a user geometry.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetGeometryIntersectFunction(RTCGeometry geometry, [NativeType("RTCIntersectFunctionN")] IntPtr intersect);
        /// <summary>
        /// Set the occlusion callback function of a user geometry.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetGeometryOccludedFunction(RTCGeometry geometry, [NativeType("RTCOccludedFunctionN")] IntPtr occluded);
        /// <summary>
        /// Invokes the intersection filter from the intersection callback function.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcInvokeIntersectFilterFromGeometry([NativeType("const struct RTCIntersectFunctionNArguments*")] RTCIntersectFunctionNArguments* args, [NativeType("const struct RTCFilterFunctionNArguments*")] RTCFilterFunctionNArguments* filterArgs);
        /// <summary>
        /// Invokes the occlusion filter from the occlusion callback function.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcInvokeOccludedFilterFromGeometry([NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args, [NativeType("const struct RTCFilterFunctionNArguments*")] RTCFilterFunctionNArguments* filterArgs);
        /// <summary>
        /// Sets the instanced scene of an instance geometry.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetGeometryInstancedScene(RTCGeometry geometry, RTCScene scene);
        /// <summary>
        /// Sets the instanced scenes of an instance array geometry.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetGeometryInstancedScenes(RTCGeometry geometry, RTCScene* scenes, [NativeType("size_t")] nuint numScenes);
        /// <summary>
        /// Sets the transformation of an instance for the specified time step.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetGeometryTransform(RTCGeometry geometry, uint timeStep, RTCFormat format, [NativeType("const void*")] void* xfm);
        /// <summary>
        /// Sets the transformation quaternion of an instance for the specified time step.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetGeometryTransformQuaternion(RTCGeometry geometry, uint timeStep, [NativeType("const struct RTCQuaternionDecomposition*")] RTCQuaternionDecomposition* qd);
        /// <summary>
        /// Returns the interpolated transformation of an instance for the specified time.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcGetGeometryTransform(RTCGeometry geometry, float time, RTCFormat format, void* xfm);

        /// <summary>
        /// Returns the interpolated transformation of the instPrimID'th instance of an
        /// instance array for the specified time. If geometry is an regular instance,
        /// instPrimID must be 0.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcGetGeometryTransformEx(RTCGeometry geometry, uint instPrimID, float time, RTCFormat format, void* xfm);
        /// <summary>
        /// Sets the uniform tessellation rate of the geometry.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetGeometryTessellationRate(RTCGeometry geometry, float tessellationRate);
        /// <summary>
        /// Sets the number of topologies of a subdivision surface.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetGeometryTopologyCount(RTCGeometry geometry, uint topologyCount);
        /// <summary>
        /// Sets the subdivision interpolation mode.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetGeometrySubdivisionMode(RTCGeometry geometry, uint topologyID, RTCSubdivisionMode mode);
        /// <summary>
        /// Binds a vertex attribute to a topology of the geometry.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetGeometryVertexAttributeTopology(RTCGeometry geometry, uint vertexAttributeID, uint topologyID);
        /// <summary>
        /// Sets the displacement callback function of a subdivision surface.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetGeometryDisplacementFunction(RTCGeometry geometry, [NativeType("RTCDisplacementFunctionN")] IntPtr displacement);
        /// <summary>
        /// Returns the first half edge of a face.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern uint rtcGetGeometryFirstHalfEdge(RTCGeometry geometry, uint faceID);
        /// <summary>
        /// Returns the face the half edge belongs to.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern uint rtcGetGeometryFace(RTCGeometry geometry, uint edgeID);
        /// <summary>
        /// Returns next half edge.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern uint rtcGetGeometryNextHalfEdge(RTCGeometry geometry, uint edgeID);
        /// <summary>
        /// Returns previous half edge.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern uint rtcGetGeometryPreviousHalfEdge(RTCGeometry geometry, uint edgeID);
        /// <summary>
        /// Returns opposite half edge.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern uint rtcGetGeometryOppositeHalfEdge(RTCGeometry geometry, uint topologyID, uint edgeID);
    }

    /// <summary>
    /// Arguments for rtcInterpolate
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 88)]
    public unsafe struct RTCInterpolateArguments
    {
        public RTCGeometry geometry;
        public uint primID;
        public float u;
        public float v;
        public RTCBufferType bufferType;
        public uint bufferSlot;
        public float* P;
        public float* dPdu;
        public float* dPdv;
        public float* ddPdudu;
        public float* ddPdvdv;
        public float* ddPdudv;
        public uint valueCount;
    }

    public static unsafe partial class GlobalFunctions
    {
        /// <summary>
        /// Interpolates vertex data to some u/v location and optionally calculates all derivatives.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcInterpolate([NativeType("const struct RTCInterpolateArguments*")] RTCInterpolateArguments* args);
    }

    /// <summary>
    /// Arguments for rtcInterpolateN
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 112)]
    public unsafe struct RTCInterpolateNArguments
    {
        public RTCGeometry geometry;
        [NativeType("const void*")] public void* valid;
        [NativeType("const unsigned int*")] public uint* primIDs;
        [NativeType("const float*")] public float* u;
        [NativeType("const float*")] public float* v;
        public uint N;
        public RTCBufferType bufferType;
        public uint bufferSlot;
        public float* P;
        public float* dPdu;
        public float* dPdv;
        public float* ddPdudu;
        public float* ddPdvdv;
        public float* ddPdudv;
        public uint valueCount;
    }

    public static unsafe partial class GlobalFunctions
    {
        /// <summary>
        /// Interpolates vertex data to an array of u/v locations.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcInterpolateN([NativeType("const struct RTCInterpolateNArguments*")] RTCInterpolateNArguments* args);
    }

    /// <summary>
    /// RTCGrid primitive for grid mesh
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RTCGrid
    {
        public uint startVertexID;
        public uint stride;
        /// <summary>
        /// max is a 32k x 32k grid
        /// </summary>
        public ushort width, height;
    }
}
