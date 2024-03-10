using System;
using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
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
        RTC_SCENE_FLAG_FILTER_FUNCTION_IN_ARGUMENTS = (1 << 3)
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
        /// Creates a new scene.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern RTCScene rtcNewScene(RTCDevice device);
        /// <summary>
        /// Returns the device the scene got created in. The reference count of
        /// the device is incremented by this function.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern RTCDevice rtcGetSceneDevice(RTCScene hscene);
        /// <summary>
        /// Retains the scene (increments the reference count).
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcRetainScene(RTCScene scene);
        /// <summary>
        /// Releases the scene (decrements the reference count).
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcReleaseScene(RTCScene scene);

        /// <summary>
        /// Attaches the geometry to a scene.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern uint rtcAttachGeometry(RTCScene scene, RTCGeometry geometry);
        /// <summary>
        /// Attaches the geometry to a scene using the specified geometry ID.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcAttachGeometryByID(RTCScene scene, RTCGeometry geometry, uint geomID);
        /// <summary>
        /// Detaches the geometry from the scene.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcDetachGeometry(RTCScene scene, uint geomID);
        /// <summary>
        /// Gets a geometry handle from the scene. This function is not thread safe and should get used during rendering.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern RTCGeometry rtcGetGeometry(RTCScene scene, uint geomID);
        /// <summary>
        /// Gets a geometry handle from the scene. This function is thread safe and should NOT get used during rendering.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern RTCGeometry rtcGetGeometryThreadSafe(RTCScene scene, uint geomID);
        /// <summary>
        /// Gets the user-defined data pointer of the geometry. This function is not thread safe and should get used during rendering.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void* rtcGetGeometryUserDataFromScene(RTCScene scene, uint geomID);
        /// <summary>
        /// Returns the interpolated transformation of an instance for the specified time.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcGetGeometryTransformFromScene(RTCScene scene, uint geomID, float time, RTCFormat format, void* xfm);

        /// <summary>
        /// Commits the scene.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcCommitScene(RTCScene scene);
        /// <summary>
        /// Commits the scene from multiple threads.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcJoinCommitScene(RTCScene scene);
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
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetSceneProgressMonitorFunction(RTCScene scene, [NativeType("RTCProgressMonitorFunction")] IntPtr progress, void* ptr);
        /// <summary>
        /// Sets the build quality of the scene.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetSceneBuildQuality(RTCScene scene, RTCBuildQuality quality);
        /// <summary>
        /// Sets the scene flags.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcSetSceneFlags(RTCScene scene, RTCSceneFlags flags);
        /// <summary>
        /// Returns the scene flags.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern RTCSceneFlags rtcGetSceneFlags(RTCScene scene);
        /// <summary>
        /// Returns the axis-aligned bounds of the scene.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcGetSceneBounds(RTCScene scene, [NativeType("struct RTCBounds*")] RTCBounds* bounds_o);
        /// <summary>
        /// Returns the linear axis-aligned bounds of the scene.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcGetSceneLinearBounds(RTCScene scene, [NativeType("struct RTCLinearBounds*")] RTCLinearBounds* bounds_o);

        /// <summary>
        /// Perform a closest point query of the scene.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern bool rtcPointQuery(RTCScene scene, [NativeType("struct RTCPointQuery*")] RTCPointQuery* query, [NativeType("struct RTCPointQueryContext*")] RTCPointQueryContext* context, [NativeType("RTCPointQueryFunction")] IntPtr queryFunc, void* userPtr);
        /// <summary>
        /// Perform a closest point query with a packet of 4 points with the scene.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern bool rtcPointQuery4([NativeType("const int*")] int* valid, RTCScene scene, [NativeType("struct RTCPointQuery4*")] RTCPointQuery4* query, [NativeType("struct RTCPointQueryContext*")] RTCPointQueryContext* context, [NativeType("RTCPointQueryFunction")] IntPtr queryFunc, void** userPtr);
        /// <summary>
        /// Perform a closest point query with a packet of 8 points with the scene.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern bool rtcPointQuery8([NativeType("const int*")] int* valid, RTCScene scene, [NativeType("struct RTCPointQuery8*")] RTCPointQuery8* query, [NativeType("struct RTCPointQueryContext*")] RTCPointQueryContext* context, [NativeType("RTCPointQueryFunction")] IntPtr queryFunc, void** userPtr);
        /// <summary>
        /// Perform a closest point query with a packet of 16 points with the scene.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern bool rtcPointQuery16([NativeType("const int*")] int* valid, RTCScene scene, [NativeType("struct RTCPointQuery16*")] RTCPointQuery16* query, [NativeType("struct RTCPointQueryContext*")] RTCPointQueryContext* context, [NativeType("RTCPointQueryFunction")] IntPtr queryFunc, void** userPtr);

        /// <summary>
        /// Intersects a single ray with the scene.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcIntersect1(RTCScene scene, [NativeType("struct RTCRayHit*")] RTCRayHit* rayhit, [RTCOptionalArgument, NativeType("struct RTCIntersectArguments*")] RTCIntersectArguments* args = null);
        /// <summary>
        /// Intersects a packet of 4 rays with the scene.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcIntersect4([NativeType("const int*")] int* valid, RTCScene scene, [NativeType("struct RTCRayHit4*")] RTCRayHit4* rayhit, [RTCOptionalArgument, NativeType("struct RTCIntersectArguments*")] RTCIntersectArguments* args = null);
        /// <summary>
        /// Intersects a packet of 8 rays with the scene.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcIntersect8([NativeType("const int*")] int* valid, RTCScene scene, [NativeType("struct RTCRayHit8*")] RTCRayHit8* rayhit, [RTCOptionalArgument, NativeType("struct RTCIntersectArguments*")] RTCIntersectArguments* args = null);
        /// <summary>
        /// Intersects a packet of 16 rays with the scene.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcIntersect16([NativeType("const int*")] int* valid, RTCScene scene, [NativeType("struct RTCRayHit16*")] RTCRayHit16* rayhit, [RTCOptionalArgument, NativeType("struct RTCIntersectArguments*")] RTCIntersectArguments* args = null);

        /// <summary>
        /// Forwards ray inside user geometry callback.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcForwardIntersect1([NativeType("const struct RTCInstersectfunctionNArguments*")] RTCIntersectFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay*")] RTCRay* ray, uint instID);
        /// <summary>
        /// Forwards ray inside user geometry callback. Extended to handle instance arrays using instPrimID parameter.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcForwardIntersect1Ex([NativeType("const struct RTCInstersectfunctionNArguments*")] RTCIntersectFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay*")] RTCRay* ray, uint instID, uint instPrimID);
        /// <summary>
        /// Forwards ray packet of size 4 inside user geometry callback.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcForwardIntersect4([NativeType("const int*")] int* valid, [NativeType("const struct RTCInstersectfunctionNArguments*")] RTCIntersectFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay4*")] RTCRay4* ray, uint instID);
        /// <summary>
        /// Forwards ray packet of size 4 inside user geometry callback. Extended to handle instance arrays using instPrimID parameter.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcForwardIntersect4Ex([NativeType("const int*")] int* valid, [NativeType("const struct RTCInstersectfunctionNArguments*")] RTCIntersectFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay4*")] RTCRay4* ray, uint instID, uint primInstID);
        /// <summary>
        /// Forwards ray packet of size 8 inside user geometry callback.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcForwardIntersect8([NativeType("const int*")] int* valid, [NativeType("const struct RTCInstersectfunctionNArguments*")] RTCIntersectFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay8*")] RTCRay8* ray, uint instID);
        /// <summary>
        /// Forwards ray packet of size 8 inside user geometry callback. Extended to handle instance arrays using instPrimID parameter.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcForwardIntersect8Ex([NativeType("const int*")] int* valid, [NativeType("const struct RTCInstersectfunctionNArguments*")] RTCIntersectFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay8*")] RTCRay8* ray, uint instID, uint primInstID);
        /// <summary>
        /// Forwards ray packet of size 16 inside user geometry callback.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcForwardIntersect16([NativeType("const int*")] int* valid, [NativeType("const struct RTCInstersectfunctionNArguments*")] RTCIntersectFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay16*")] RTCRay16* ray, uint instID);
        /// <summary>
        /// Forwards ray packet of size 16 inside user geometry callback. Extended to handle instance arrays using instPrimID parameter.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcForwardIntersect16Ex([NativeType("const int*")] int* valid, [NativeType("const struct RTCInstersectfunctionNArguments*")] RTCIntersectFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay16*")] RTCRay16* ray, uint instID, uint primInstID);

        /// <summary>
        /// Tests a single ray for occlusion with the scene.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcOccluded1(RTCScene scene, [NativeType("struct RTCRay*")] RTCRay* ray, [RTCOptionalArgument, NativeType("struct RTCOccludedArguments*")] RTCOccludedArguments* args = null);
        /// <summary>
        /// Tests a packet of 4 rays for occlusion occluded with the scene.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcOccluded4([NativeType("const int*")] int* valid, RTCScene scene, [NativeType("struct RTCRay4*")] RTCRay4* ray, [RTCOptionalArgument, NativeType("struct RTCOccludedArguments*")] RTCOccludedArguments* args = null);
        /// <summary>
        /// Tests a packet of 8 rays for occlusion with the scene.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcOccluded8([NativeType("const int*")] int* valid, RTCScene scene, [NativeType("struct RTCRay8*")] RTCRay8* ray, [RTCOptionalArgument, NativeType("struct RTCOccludedArguments*")] RTCOccludedArguments* args = null);
        /// <summary>
        /// Tests a packet of 16 rays for occlusion with the scene.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcOccluded16([NativeType("const int*")] int* valid, RTCScene scene, [NativeType("struct RTCRay16*")] RTCRay16* ray, [RTCOptionalArgument, NativeType("struct RTCOccludedArguments*")] RTCOccludedArguments* args = null);

        /// <summary>
        /// Forwards single occlusion ray inside user geometry callback.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcForwardOccluded1([NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay*")] RTCRay* ray, uint instID);
        /// <summary>
        /// Forwards single occlusion ray inside user geometry callback. Extended to handle instance arrays using instPrimID parameter.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcForwardOccluded1Ex([NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay*")] RTCRay* ray, uint instID, uint instPrimID);
        /// <summary>
        /// Forwards occlusion ray packet of size 4 inside user geometry callback.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcForwardOccluded4([NativeType("const int*")] int* valid, [NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay4*")] RTCRay4* ray, uint instID);
        /// <summary>
        /// Forwards occlusion ray packet of size 4 inside user geometry callback. Extended to handle instance arrays using instPrimID parameter.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcForwardOccluded4Ex([NativeType("const int*")] int* valid, [NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay4*")] RTCRay4* ray, uint instID, uint instPrimID);
        /// <summary>
        /// Forwards occlusion ray packet of size 8 inside user geometry callback.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcForwardOccluded8([NativeType("const int*")] int* valid, [NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay8*")] RTCRay8* ray, uint instID);
        /// <summary>
        /// Forwards occlusion ray packet of size 8 inside user geometry callback. Extended to handle instance arrays using instPrimID parameter.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcForwardOccluded8Ex([NativeType("const int*")] int* valid, [NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay8*")] RTCRay8* ray, uint instID, uint instPrimID);
        /// <summary>
        /// Forwards occlusion ray packet of size 16 inside user geometry callback.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcForwardOccluded16([NativeType("const int*")] int* valid, [NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay16*")] RTCRay16* ray, uint instID);
        /// <summary>
        /// Forwards occlusion ray packet of size 16 inside user geometry callback. Extended to handle instance arrays using instPrimID parameter.
        /// </summary>
        [DllImport(DynamicLibraryName)]
        public static extern void rtcForwardOccluded16Ex([NativeType("const int*")] int* valid, [NativeType("const struct RTCOccludedFunctionNArguments*")] RTCOccludedFunctionNArguments* args, RTCScene scene, [NativeType("struct RTCRay16*")] RTCRay16* ray, uint instID, uint instPrimID);
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
        [DllImport(DynamicLibraryName)]
        public static extern void rtcCollide(RTCScene scene0, RTCScene scene1, [NativeType("RTCCollideFunc")] IntPtr callback, void* userPtr);
    }
}
