using System;
using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    public class RTCScene : SafeHandle
    {
        public override bool IsInvalid => handle == nint.Zero;
        public RTCScene() : base(0, true) { }

        protected override bool ReleaseHandle()
        {
            try
            {
                EmbreeNative.rtcReleaseScene(this);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }

    public class RTCGeometry : SafeHandle
    {
        public override bool IsInvalid => handle == nint.Zero;
        public RTCGeometry() : base(0, true) { }

        protected override bool ReleaseHandle()
        {
            try
            {
                EmbreeNative.rtcReleaseGeometry(this);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
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
        [NativeType("RTCGeometry")] public nint geometry;
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

    public static unsafe partial class EmbreeNative
    {
        /// <summary>
        /// Creates a new geometry of specified type.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial RTCGeometry rtcNewGeometry(RTCDevice device, RTCGeometryType type);
        /// <summary>
        /// Retains the geometry (increments the reference count).
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcRetainGeometry(RTCGeometry geometry);
        /// <summary>
        /// Releases the geometry (decrements the reference count)
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcReleaseGeometry(RTCGeometry geometry);
        /// <summary>
        /// Commits the geometry.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcCommitGeometry(RTCGeometry geometry);

        /// <summary>
        /// Enables the geometry.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcEnableGeometry(RTCGeometry geometry);
        /// <summary>
        /// Disables the geometry.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcDisableGeometry(RTCGeometry geometry);

        /// <summary>
        /// Sets the number of motion blur time steps of the geometry.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetGeometryTimeStepCount(RTCGeometry geometry, uint timeStepCount);
        /// <summary>
        /// Sets the motion blur time range of the geometry.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetGeometryTimeRange(RTCGeometry geometry, float startTime, float endTime);
        /// <summary>
        /// Sets the number of vertex attributes of the geometry.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetGeometryVertexAttributeCount(RTCGeometry geometry, uint vertexAttributeCount);
        /// <summary>
        /// Sets the ray mask of the geometry.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetGeometryMask(RTCGeometry geometry, uint mask);
        /// <summary>
        /// Sets the build quality of the geometry.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetGeometryBuildQuality(RTCGeometry geometry, RTCBuildQuality quality);
        /// <summary>
        /// Sets the maximal curve or point radius scale allowed by min-width feature.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetGeometryMaxRadiusScale(RTCGeometry geometry, float maxRadiusScale);

        /// <summary>
        /// Sets a geometry buffer.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetGeometryBuffer(RTCGeometry geometry, RTCBufferType type, uint slot, RTCFormat format, RTCBuffer buffer, [NativeType("size_t")] nuint byteOffset, [NativeType("size_t")] nuint byteStride, [NativeType("size_t")] nuint itemCount);
        /// <summary>
        /// Sets a shared geometry buffer.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetSharedGeometryBuffer(RTCGeometry geometry, RTCBufferType type, uint slot, RTCFormat format, [NativeType("const void*")] void* ptr, [NativeType("size_t")] nuint byteOffset, [NativeType("size_t")] nuint byteStride, [NativeType("size_t")] nuint itemCount);
        /// <summary>
        /// Creates and sets a new geometry buffer.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void* rtcSetNewGeometryBuffer(RTCGeometry geometry, RTCBufferType type, uint slot, RTCFormat format, [NativeType("size_t")] nuint byteStride, [NativeType("size_t")] nuint itemCount);
        /// <summary>
        /// Returns the pointer to the data of a buffer.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void* rtcGetGeometryBufferData(RTCGeometry geometry, RTCBufferType type, uint slot);
        /// <summary>
        /// Updates a geometry buffer.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcUpdateGeometryBuffer(RTCGeometry geometry, RTCBufferType type, uint slot);

        /// <summary>
        /// Sets the intersection filter callback function of the geometry.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetGeometryIntersectFilterFunction(RTCGeometry geometry, RTCFilterFunctionN filter);
        /// <summary>
        /// Sets the occlusion filter callback function of the geometry.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetGeometryOccludedFilterFunction(RTCGeometry geometry, RTCFilterFunctionN filter);
        /// <summary>
        /// Enables argument version of intersection or occlusion filter function.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetGeometryEnableFilterFunctionFromArguments(RTCGeometry geometry, [MarshalAs(UnmanagedType.Bool)] bool enable);
        /// <summary>
        /// Sets the user-defined data pointer of the geometry.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetGeometryUserData(RTCGeometry geometry, void* ptr);
        /// <summary>
        /// Gets the user-defined data pointer of the geometry.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void* rtcGetGeometryUserData(RTCGeometry geometry);
        /// <summary>
        /// Set the point query callback function of a geometry.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetGeometryPointQueryFunction(RTCGeometry geometry, RTCPointQueryFunction pointQuery);
        /// <summary>
        /// Sets the number of primitives of a user geometry.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetGeometryUserPrimitiveCount(RTCGeometry geometry, uint userPrimitiveCount);
        /// <summary>
        /// Sets the bounding callback function to calculate bounding boxes for user primitives.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetGeometryBoundsFunction(RTCGeometry geometry, RTCBoundsFunction bounds, void* userPtr);
        /// <summary>
        /// Set the intersect callback function of a user geometry.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetGeometryIntersectFunction(RTCGeometry geometry, RTCIntersectFunctionN intersect);
        /// <summary>
        /// Set the occlusion callback function of a user geometry.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetGeometryOccludedFunction(RTCGeometry geometry, RTCOccludedFunctionN occluded);
        /// <summary>
        /// Invokes the intersection filter from the intersection callback function.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcInvokeIntersectFilterFromGeometry([NativeType("const struct RTCIntersectFunctionNArguments*")] RTCIntersectFunctionNArguments* args, [NativeType("const struct RTCFilterFunctionNArguments*")] RTCFilterFunctionNArguments* filterArgs);
        /// <summary>
        /// Invokes the occlusion filter from the occlusion callback function.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcInvokeOccludedFilterFromGeometry([NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args, [NativeType("const struct RTCFilterFunctionNArguments*")] RTCFilterFunctionNArguments* filterArgs);
        /// <summary>
        /// Sets the instanced scene of an instance geometry.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetGeometryInstancedScene(RTCGeometry geometry, RTCScene scene);
        /// <summary>
        /// Sets the instanced scenes of an instance array geometry.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetGeometryInstancedScenes(RTCGeometry geometry, [NativeType("RTCScene*")] nint* scenes, [NativeType("size_t")] nuint numScenes);
        /// <summary>
        /// Sets the transformation of an instance for the specified time step.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetGeometryTransform(RTCGeometry geometry, uint timeStep, RTCFormat format, [NativeType("const void*")] void* xfm);
        /// <summary>
        /// Sets the transformation quaternion of an instance for the specified time step.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetGeometryTransformQuaternion(RTCGeometry geometry, uint timeStep, [NativeType("const struct RTCQuaternionDecomposition*")] RTCQuaternionDecomposition* qd);
        /// <summary>
        /// Returns the interpolated transformation of an instance for the specified time.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcGetGeometryTransform(RTCGeometry geometry, float time, RTCFormat format, void* xfm);

        /// <summary>
        /// Returns the interpolated transformation of the instPrimID'th instance of an
        /// instance array for the specified time. If geometry is an regular instance,
        /// instPrimID must be 0.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcGetGeometryTransformEx(RTCGeometry geometry, uint instPrimID, float time, RTCFormat format, void* xfm);
        /// <summary>
        /// Sets the uniform tessellation rate of the geometry.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetGeometryTessellationRate(RTCGeometry geometry, float tessellationRate);
        /// <summary>
        /// Sets the number of topologies of a subdivision surface.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetGeometryTopologyCount(RTCGeometry geometry, uint topologyCount);
        /// <summary>
        /// Sets the subdivision interpolation mode.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetGeometrySubdivisionMode(RTCGeometry geometry, uint topologyID, RTCSubdivisionMode mode);
        /// <summary>
        /// Binds a vertex attribute to a topology of the geometry.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetGeometryVertexAttributeTopology(RTCGeometry geometry, uint vertexAttributeID, uint topologyID);
        /// <summary>
        /// Sets the displacement callback function of a subdivision surface.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetGeometryDisplacementFunction(RTCGeometry geometry, RTCDisplacementFunctionN displacement);
        /// <summary>
        /// Returns the first half edge of a face.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial uint rtcGetGeometryFirstHalfEdge(RTCGeometry geometry, uint faceID);
        /// <summary>
        /// Returns the face the half edge belongs to.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial uint rtcGetGeometryFace(RTCGeometry geometry, uint edgeID);
        /// <summary>
        /// Returns next half edge.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial uint rtcGetGeometryNextHalfEdge(RTCGeometry geometry, uint edgeID);
        /// <summary>
        /// Returns previous half edge.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial uint rtcGetGeometryPreviousHalfEdge(RTCGeometry geometry, uint edgeID);
        /// <summary>
        /// Returns opposite half edge.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial uint rtcGetGeometryOppositeHalfEdge(RTCGeometry geometry, uint topologyID, uint edgeID);
    }

    /// <summary>
    /// Arguments for rtcInterpolate
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 88)]
    public unsafe struct RTCInterpolateArguments
    {
        [NativeType("RTCGeometry")] public nint geometry;
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

    public static unsafe partial class EmbreeNative
    {
        /// <summary>
        /// Interpolates vertex data to some u/v location and optionally calculates all derivatives.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcInterpolate([NativeType("const struct RTCInterpolateArguments*")] RTCInterpolateArguments* args);
    }

    /// <summary>
    /// Arguments for rtcInterpolateN
    /// </summary>
    [StructLayout(LayoutKind.Sequential, Size = 112)]
    public unsafe struct RTCInterpolateNArguments
    {
        [NativeType("RTCGeometry")] public nint geometry;
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

    public static unsafe partial class EmbreeNative
    {
        /// <summary>
        /// Interpolates vertex data to an array of u/v locations.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcInterpolateN([NativeType("const struct RTCInterpolateNArguments*")] RTCInterpolateNArguments* args);
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
