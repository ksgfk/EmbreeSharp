using System;
using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    public struct RTCTraversable
    {
        public nint Ptr;
    }

    /// <summary>
    /// Scene flags
    /// </summary>
    [Flags]
    public enum RTCSceneFlags
    {
        RTC_SCENE_FLAG_NONE = 0,
        RTC_SCENE_FLAG_DYNAMIC = (1 << 0),
        RTC_SCENE_FLAG_COMPACT = (1 << 1),
        RTC_SCENE_FLAG_ROBUST = (1 << 2),
        RTC_SCENE_FLAG_FILTER_FUNCTION_IN_ARGUMENTS = (1 << 3),
        RTC_SCENE_FLAG_PREFETCH_USM_SHARED_ON_GPU = (1 << 4),
    }

    /// <summary>
    /// Additional arguments for rtcIntersect1/4/8/16 calls
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RTCIntersectArguments
    {
        /// <summary>
        /// intersection flags
        /// </summary>
        public RTCRayQueryFlags flags;
        /// <summary>
        /// selectively enable features for traversal
        /// </summary>
        public RTCFeatureFlags feature_mask;
        /// <summary>
        /// optional pointer to ray query context
        /// </summary>
        [NativeType("struct RTCRayQueryContext*")] public RTCRayQueryContext* context;
        /// <summary>
        /// filter function to execute
        /// </summary>
        [NativeType("RTCFilterFunctionN")] public IntPtr filter;
        /// <summary>
        /// user geometry intersection callback to execute
        /// </summary>
        [NativeType("RTCIntersectFunctionN")] public IntPtr intersect;
    }

    public static unsafe partial class EmbreeNative
    {
        /// <summary>
        /// Initializes intersection arguments.
        /// </summary>
        public static void rtcInitIntersectArguments(RTCIntersectArguments* args)
        {
            args->flags = RTCRayQueryFlags.RTC_RAY_QUERY_FLAG_INCOHERENT;
            args->feature_mask = RTCFeatureFlags.RTC_FEATURE_FLAG_ALL;
            args->context = null;
            args->filter = nint.Zero;
            args->intersect = nint.Zero;
        }
    }

    /// <summary>
    /// Additional arguments for rtcOccluded1/4/8/16 calls
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RTCOccludedArguments
    {
        /// <summary>
        /// intersection flags
        /// </summary>
        public RTCRayQueryFlags flags;
        /// <summary>
        /// selectively enable features for traversal
        /// </summary>
        public RTCFeatureFlags feature_mask;
        /// <summary>
        /// optional pointer to ray query context
        /// </summary>
        [NativeType("struct RTCRayQueryContext*")] public RTCRayQueryContext* context;
        /// <summary>
        /// filter function to execute
        /// </summary>
        [NativeType("RTCFilterFunctionN")] public IntPtr filter;
        /// <summary>
        /// user geometry occlusion callback to execute
        /// </summary>
        [NativeType("RTCOccludedFunctionN")] public IntPtr occluded;
    }

    public static unsafe partial class EmbreeNative
    {
        /// <summary>
        /// Initializes an intersection arguments.
        /// </summary>
        public static void rtcInitOccludedArguments(RTCOccludedArguments* args)
        {
            args->flags = RTCRayQueryFlags.RTC_RAY_QUERY_FLAG_INCOHERENT;
            args->feature_mask = RTCFeatureFlags.RTC_FEATURE_FLAG_ALL;
            args->context = null;
            args->filter = nint.Zero;
            args->occluded = nint.Zero;
        }

        /// <summary>
        /// Creates a new scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial RTCScene rtcNewScene(RTCDevice device);
        /// <summary>
        /// Returns the device the scene got created in. The reference count of
        /// the device is incremented by this function.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial RTCDevice rtcGetSceneDevice(RTCScene hscene);
        /// <summary>
        /// Retains the scene (increments the reference count).
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcRetainScene(RTCScene scene);
        /// <summary>
        /// Releases the scene (decrements the reference count).
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcReleaseScene(RTCScene scene);
        /// <summary>
        /// Returns the traversable object of the scene which can be passed to ray queries.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial RTCTraversable rtcGetSceneTraversable(RTCScene scene);
        /// <summary>
        /// Attaches the geometry to a scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial uint rtcAttachGeometry(RTCScene scene, RTCGeometry geometry);
        /// <summary>
        /// Attaches the geometry to a scene using the specified geometry ID.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcAttachGeometryByID(RTCScene scene, RTCGeometry geometry, uint geomID);
        /// <summary>
        /// Detaches the geometry from the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcDetachGeometry(RTCScene scene, uint geomID);
        /// <summary>
        /// Gets a geometry handle from the scene. This function is not thread safe and should get used during rendering.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial RTCGeometry rtcGetGeometry(RTCScene scene, uint geomID);
        /// <summary>
        /// Gets a geometry handle from the scene. This function is thread safe and should NOT get used during rendering.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial RTCGeometry rtcGetGeometryThreadSafe(RTCScene scene, uint geomID);

        /// <summary>
        /// Commits the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcCommitScene(RTCScene scene);
        /// <summary>
        /// Commits the scene from multiple threads.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcJoinCommitScene(RTCScene scene);
    }

    /// <summary>
    /// Progress monitor callback function
    /// </summary>
    public unsafe delegate bool RTCProgressMonitorFunction(void* ptr, double n);

    public static unsafe partial class EmbreeNative
    {
        /// <summary>
        /// Sets the progress monitor callback function of the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetSceneProgressMonitorFunction(RTCScene scene, RTCProgressMonitorFunction? progress, void* ptr);
        /// <summary>
        /// Sets the build quality of the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetSceneBuildQuality(RTCScene scene, RTCBuildQuality quality);
        /// <summary>
        /// Sets the scene flags.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcSetSceneFlags(RTCScene scene, RTCSceneFlags flags);
        /// <summary>
        /// Returns the scene flags.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial RTCSceneFlags rtcGetSceneFlags(RTCScene scene);
        /// <summary>
        /// Returns the axis-aligned bounds of the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcGetSceneBounds(RTCScene scene, [NativeType("struct RTCBounds*")] RTCBounds* bounds_o);
        /// <summary>
        /// Returns the linear axis-aligned bounds of the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcGetSceneLinearBounds(RTCScene scene, [NativeType("struct RTCLinearBounds*")] RTCLinearBounds* bounds_o);

        /// <summary>
        /// Gets the user-defined data pointer of the geometry. This function is not thread safe and should get used during rendering.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void* rtcGetGeometryUserDataFromScene(RTCScene scene, uint geomID);
        /// <summary>
        /// Returns the interpolated transformation of an instance for the specified time.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcGetGeometryTransformFromScene(RTCScene scene, uint geomID, float time, RTCFormat format, void* xfm);

        /// <summary>
        /// Perform a closest point query of the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool rtcPointQuery(RTCScene scene, [NativeType("struct RTCPointQuery*")] RTCPointQuery* query, [NativeType("struct RTCPointQueryContext*")] RTCPointQueryContext* context, RTCPointQueryFunction queryFunc, void* userPtr);
        /// <summary>
        /// Perform a closest point query with a packet of 4 points with the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool rtcPointQuery4([NativeType("const int*")] int* valid, RTCScene scene, [NativeType("struct RTCPointQuery4*")] RTCPointQuery4* query, [NativeType("struct RTCPointQueryContext*")] RTCPointQueryContext* context, RTCPointQueryFunction queryFunc, void** userPtr);
        /// <summary>
        /// Perform a closest point query with a packet of 8 points with the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool rtcPointQuery8([NativeType("const int*")] int* valid, RTCScene scene, [NativeType("struct RTCPointQuery8*")] RTCPointQuery8* query, [NativeType("struct RTCPointQueryContext*")] RTCPointQueryContext* context, RTCPointQueryFunction queryFunc, void** userPtr);
        /// <summary>
        /// Perform a closest point query with a packet of 16 points with the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static partial bool rtcPointQuery16([NativeType("const int*")] int* valid, RTCScene scene, [NativeType("struct RTCPointQuery16*")] RTCPointQuery16* query, [NativeType("struct RTCPointQueryContext*")] RTCPointQueryContext* context, RTCPointQueryFunction queryFunc, void** userPtr);

        /// <summary>
        /// Intersects a single ray with the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcIntersect1(RTCScene scene, [NativeType("struct RTCRayHit*")] RTCRayHit* rayhit, [RTCOptionalArgument, NativeType("struct RTCIntersectArguments*")] RTCIntersectArguments* args = null);
        /// <summary>
        /// Intersects a packet of 4 rays with the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcIntersect4([NativeType("const int*")] int* valid, RTCScene scene, [NativeType("struct RTCRayHit4*")] RTCRayHit4* rayhit, [RTCOptionalArgument, NativeType("struct RTCIntersectArguments*")] RTCIntersectArguments* args = null);
        /// <summary>
        /// Intersects a packet of 8 rays with the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcIntersect8([NativeType("const int*")] int* valid, RTCScene scene, [NativeType("struct RTCRayHit8*")] RTCRayHit8* rayhit, [RTCOptionalArgument, NativeType("struct RTCIntersectArguments*")] RTCIntersectArguments* args = null);
        /// <summary>
        /// Intersects a packet of 16 rays with the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcIntersect16([NativeType("const int*")] int* valid, RTCScene scene, [NativeType("struct RTCRayHit16*")] RTCRayHit16* rayhit, [RTCOptionalArgument, NativeType("struct RTCIntersectArguments*")] RTCIntersectArguments* args = null);

        /// <summary>
        /// Forwards ray inside user geometry callback.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcForwardIntersect1([NativeType("const struct RTCInstersectfunctionNArguments*")] RTCIntersectFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay*")] RTCRay* ray, uint instID);
        /// <summary>
        /// Forwards ray inside user geometry callback. Extended to handle instance arrays using instPrimID parameter.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcForwardIntersect1Ex([NativeType("const struct RTCInstersectfunctionNArguments*")] RTCIntersectFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay*")] RTCRay* ray, uint instID, uint instPrimID);
        /// <summary>
        /// Forwards ray packet of size 4 inside user geometry callback.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcForwardIntersect4([NativeType("const int*")] int* valid, [NativeType("const struct RTCInstersectfunctionNArguments*")] RTCIntersectFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay4*")] RTCRay4* ray, uint instID);
        /// <summary>
        /// Forwards ray packet of size 4 inside user geometry callback. Extended to handle instance arrays using instPrimID parameter.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcForwardIntersect4Ex([NativeType("const int*")] int* valid, [NativeType("const struct RTCInstersectfunctionNArguments*")] RTCIntersectFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay4*")] RTCRay4* ray, uint instID, uint primInstID);
        /// <summary>
        /// Forwards ray packet of size 8 inside user geometry callback.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcForwardIntersect8([NativeType("const int*")] int* valid, [NativeType("const struct RTCInstersectfunctionNArguments*")] RTCIntersectFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay8*")] RTCRay8* ray, uint instID);
        /// <summary>
        /// Forwards ray packet of size 8 inside user geometry callback. Extended to handle instance arrays using instPrimID parameter.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcForwardIntersect8Ex([NativeType("const int*")] int* valid, [NativeType("const struct RTCInstersectfunctionNArguments*")] RTCIntersectFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay8*")] RTCRay8* ray, uint instID, uint primInstID);
        /// <summary>
        /// Forwards ray packet of size 16 inside user geometry callback.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcForwardIntersect16([NativeType("const int*")] int* valid, [NativeType("const struct RTCInstersectfunctionNArguments*")] RTCIntersectFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay16*")] RTCRay16* ray, uint instID);
        /// <summary>
        /// Forwards ray packet of size 16 inside user geometry callback. Extended to handle instance arrays using instPrimID parameter.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcForwardIntersect16Ex([NativeType("const int*")] int* valid, [NativeType("const struct RTCInstersectfunctionNArguments*")] RTCIntersectFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay16*")] RTCRay16* ray, uint instID, uint primInstID);

        /// <summary>
        /// Tests a single ray for occlusion with the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcOccluded1(RTCScene scene, [NativeType("struct RTCRay*")] RTCRay* ray, [RTCOptionalArgument, NativeType("struct RTCOccludedArguments*")] RTCOccludedArguments* args = null);
        /// <summary>
        /// Tests a packet of 4 rays for occlusion occluded with the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcOccluded4([NativeType("const int*")] int* valid, RTCScene scene, [NativeType("struct RTCRay4*")] RTCRay4* ray, [RTCOptionalArgument, NativeType("struct RTCOccludedArguments*")] RTCOccludedArguments* args = null);
        /// <summary>
        /// Tests a packet of 8 rays for occlusion with the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcOccluded8([NativeType("const int*")] int* valid, RTCScene scene, [NativeType("struct RTCRay8*")] RTCRay8* ray, [RTCOptionalArgument, NativeType("struct RTCOccludedArguments*")] RTCOccludedArguments* args = null);
        /// <summary>
        /// Tests a packet of 16 rays for occlusion with the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcOccluded16([NativeType("const int*")] int* valid, RTCScene scene, [NativeType("struct RTCRay16*")] RTCRay16* ray, [RTCOptionalArgument, NativeType("struct RTCOccludedArguments*")] RTCOccludedArguments* args = null);

        /// <summary>
        /// Forwards single occlusion ray inside user geometry callback.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcForwardOccluded1([NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay*")] RTCRay* ray, uint instID);
        /// <summary>
        /// Forwards single occlusion ray inside user geometry callback. Extended to handle instance arrays using instPrimID parameter.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcForwardOccluded1Ex([NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay*")] RTCRay* ray, uint instID, uint instPrimID);
        /// <summary>
        /// Forwards occlusion ray packet of size 4 inside user geometry callback.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcForwardOccluded4([NativeType("const int*")] int* valid, [NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay4*")] RTCRay4* ray, uint instID);
        /// <summary>
        /// Forwards occlusion ray packet of size 4 inside user geometry callback. Extended to handle instance arrays using instPrimID parameter.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcForwardOccluded4Ex([NativeType("const int*")] int* valid, [NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay4*")] RTCRay4* ray, uint instID, uint instPrimID);
        /// <summary>
        /// Forwards occlusion ray packet of size 8 inside user geometry callback.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcForwardOccluded8([NativeType("const int*")] int* valid, [NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay8*")] RTCRay8* ray, uint instID);
        /// <summary>
        /// Forwards occlusion ray packet of size 8 inside user geometry callback. Extended to handle instance arrays using instPrimID parameter.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcForwardOccluded8Ex([NativeType("const int*")] int* valid, [NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay8*")] RTCRay8* ray, uint instID, uint instPrimID);
        /// <summary>
        /// Forwards occlusion ray packet of size 16 inside user geometry callback.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcForwardOccluded16([NativeType("const int*")] int* valid, [NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay16*")] RTCRay16* ray, uint instID);
        /// <summary>
        /// Forwards occlusion ray packet of size 16 inside user geometry callback. Extended to handle instance arrays using instPrimID parameter.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcForwardOccluded16Ex([NativeType("const int*")] int* valid, [NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay16*")] RTCRay16* ray, uint instID, uint instPrimID);

        /// <summary>
        /// Gets the user-defined data pointer of the geometry. This function is not thread safe and should get used during rendering.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void* rtcGetGeometryUserDataFromTraversable(RTCTraversable traversable, uint geomID);
        /// <summary>
        /// Returns the interpolated transformation of an instance for the specified time.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcGetGeometryTransformFromTraversable(RTCTraversable traversable, uint geomID, float time, RTCFormat format, void* xfm);
        /// <summary>
        /// Perform a closest point query of the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool rtcTraversablePointQuery(RTCTraversable traversable, RTCPointQuery* query, RTCPointQueryContext* context, RTCPointQueryFunction queryFunc, void* userPtr);
        /// <summary>
        /// Perform a closest point query with a packet of 4 points with the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool rtcTraversablePointQuery4([NativeType("const int*")] int* valid, RTCTraversable traversable, RTCPointQuery4* query, RTCPointQueryContext* context, RTCPointQueryFunction queryFunc, void** userPtr);
        /// <summary>
        /// Perform a closest point query with a packet of 8 points with the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool rtcTraversablePointQuery8([NativeType("const int*")] int* valid, RTCTraversable traversable, RTCPointQuery8* query, RTCPointQueryContext* context, RTCPointQueryFunction queryFunc, void** userPtr);
        /// <summary>
        /// Perform a closest point query with a packet of 16 points with the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        [return: MarshalAs(UnmanagedType.I1)]
        public static partial bool rtcTraversablePointQuery16([NativeType("const int*")] int* valid, RTCTraversable traversable, RTCPointQuery16* query, RTCPointQueryContext* context, RTCPointQueryFunction queryFunc, void** userPtr);
        /// <summary>
        /// Intersects a single ray with the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcTraversableIntersect1(RTCTraversable traversable, RTCRayHit* rayhit, RTCIntersectArguments* args = null);
        /// <summary>
        /// Intersects a packet of 4 rays with the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcTraversableIntersect4([NativeType("const int*")] int* valid, RTCTraversable traversable, RTCRayHit4* rayhit, RTCIntersectArguments* args = null);
        /// <summary>
        /// Intersects a packet of 8 rays with the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcTraversableIntersect8([NativeType("const int*")] int* valid, RTCTraversable traversable, RTCRayHit8* rayhit, RTCIntersectArguments* args = null);
        /// <summary>
        /// Intersects a packet of 16 rays with the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcTraversableIntersect16([NativeType("const int*")] int* valid, RTCTraversable traversable, RTCRayHit16* rayhit, RTCIntersectArguments* args = null);
        /// <summary>
        /// Forwards ray inside user geometry callback.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcTraversableForwardIntersect1([NativeType("const struct RTCIntersectFunctionNArguments*")] RTCIntersectFunctionNArguments* args, RTCTraversable traversable, RTCRay* ray, uint instID);
        /// <summary>
        /// Forwards ray inside user geometry callback. Extended to handle instance arrays using instPrimID parameter.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcTraversableForwardIntersect1Ex([NativeType("const struct RTCIntersectFunctionNArguments*")] RTCIntersectFunctionNArguments* args, RTCTraversable traversable, RTCRay* ray, uint instID, uint instPrimID);
        /// <summary>
        /// Forwards ray packet of size 4 inside user geometry callback.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcTraversableForwardIntersect4([NativeType("const int*")] int* valid, [NativeType("const struct RTCIntersectFunctionNArguments*")] RTCIntersectFunctionNArguments* args, RTCTraversable traversable, RTCRay4* ray, uint instID);
        /// <summary>
        /// Forwards ray packet of size 4 inside user geometry callback. Extended to handle instance arrays using instPrimID parameter.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcTraversableForwardIntersect4Ex([NativeType("const int*")] int* valid, [NativeType("const struct RTCIntersectFunctionNArguments*")] RTCIntersectFunctionNArguments* args, RTCTraversable traversable, RTCRay4* ray, uint instID, uint instPrimID);
        /// <summary>
        /// Forwards ray packet of size 8 inside user geometry callback.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcTraversableForwardIntersect8([NativeType("const int*")] int* valid, [NativeType("const struct RTCIntersectFunctionNArguments*")] RTCIntersectFunctionNArguments* args, RTCTraversable traversable, RTCRay8* ray, uint instID);
        /// <summary>
        /// Forwards ray packet of size 8 inside user geometry callback. Extended to handle instance arrays using instPrimID parameter.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcTraversableForwardIntersect8Ex([NativeType("const int*")] int* valid, [NativeType("const struct RTCIntersectFunctionNArguments*")] RTCIntersectFunctionNArguments* args, RTCTraversable traversable, RTCRay8* ray, uint instID, uint instPrimID);
        /// <summary>
        /// Forwards ray packet of size 16 inside user geometry callback.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcTraversableForwardIntersect16([NativeType("const int*")] int* valid, [NativeType("const struct RTCIntersectFunctionNArguments*")] RTCIntersectFunctionNArguments* args, RTCTraversable traversable, RTCRay16* ray, uint instID);
        /// <summary>
        /// Forwards ray packet of size 16 inside user geometry callback. Extended to handle instance arrays using instPrimID parameter.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcTraversableForwardIntersect16Ex([NativeType("const int*")] int* valid, [NativeType("const struct RTCIntersectFunctionNArguments*")] RTCIntersectFunctionNArguments* args, RTCTraversable traversable, RTCRay16* ray, uint instID, uint instPrimID);
        /// <summary>
        /// Tests a single ray for occlusion with the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcTraversableOccluded1(RTCTraversable traversable, RTCRay* ray, RTCOccludedArguments* args = null);
        /// <summary>
        /// Tests a packet of 4 rays for occlusion occluded with the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcTraversableOccluded4([NativeType("const int*")] int* valid, RTCTraversable traversable, RTCRay4* ray, RTCOccludedArguments* args = null);
        /// <summary>
        /// Tests a packet of 8 rays for occlusion occluded with the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcTraversableOccluded8([NativeType("const int*")] int* valid, RTCTraversable traversable, RTCRay8* ray, RTCOccludedArguments* args = null);
        /// <summary>
        /// Tests a packet of 16 rays for occlusion occluded with the scene.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcTraversableOccluded16([NativeType("const int*")] int* valid, RTCTraversable traversable, RTCRay16* ray, RTCOccludedArguments* args = null);
        /// <summary>
        /// Forwards single occlusion ray inside user geometry callback.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcTraversableForwardOccluded1([NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args, RTCTraversable traversable, RTCRay* ray, uint instID);
        /// <summary>
        /// Forwards single occlusion ray inside user geometry callback. Extended to handle instance arrays using instPrimID parameter.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcTraversableForwardOccluded1Ex([NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args, RTCTraversable traversable, RTCRay* ray, uint instID, uint instPrimID);
        /// <summary>
        /// Forwards occlusion ray packet of size 4 inside user geometry callback.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcTraversableForwardOccluded4([NativeType("const int*")] int* valid, [NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args, RTCTraversable traversable, RTCRay4* ray, uint instID);
        /// <summary>
        /// Forwards occlusion ray packet of size 4 inside user geometry callback. Extended to handle instance arrays using instPrimID parameter.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcTraversableForwardOccluded4Ex([NativeType("const int*")] int* valid, [NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args, RTCTraversable traversable, RTCRay4* ray, uint instID, uint instPrimID);
        /// <summary>
        /// Forwards occlusion ray packet of size 4 inside user geometry callback.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcTraversableForwardOccluded8([NativeType("const int*")] int* valid, [NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args, RTCTraversable traversable, RTCRay8* ray, uint instID);
        /// <summary>
        /// Forwards occlusion ray packet of size 4 inside user geometry callback. Extended to handle instance arrays using instPrimID parameter.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcTraversableForwardOccluded8Ex([NativeType("const int*")] int* valid, [NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args, RTCTraversable traversable, RTCRay8* ray, uint instID, uint instPrimID);
        /// <summary>
        /// Forwards occlusion ray packet of size 4 inside user geometry callback.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcTraversableForwardOccluded16([NativeType("const int*")] int* valid, [NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args, RTCTraversable traversable, RTCRay16* ray, uint instID);
        /// <summary>
        /// Forwards occlusion ray packet of size 4 inside user geometry callback. Extended to handle instance arrays using instPrimID parameter.
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcTraversableForwardOccluded16Ex([NativeType("const int*")] int* valid, [NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args, RTCTraversable traversable, RTCRay16* ray, uint instID, uint instPrimID);
    }

    /// <summary>
    /// collision callback
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct RTCCollision
    {
        public uint geomID0;
        public uint primID0;
        public uint geomID1;
        public uint primID1;
    }

    public unsafe delegate void RTCCollideFunc(void* userPtr, [NativeType("struct RTCCollision*")] RTCCollision* collisions, uint num_collisions);

    public static unsafe partial class EmbreeNative
    {
        /// <summary>
        /// Performs collision detection of two scenes
        /// </summary>
        [LibraryImport(DynamicLibraryName)]
        public static partial void rtcCollide(RTCScene scene0, RTCScene scene1, RTCCollideFunc callback, void* userPtr);
    }
}
