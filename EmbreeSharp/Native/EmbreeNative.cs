using System;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

public static unsafe partial class EmbreeNative
{
    [NativeTypeName("#define RTC_INVALID_GEOMETRY_ID ((unsigned int)-1)")]
    public const uint RTC_INVALID_GEOMETRY_ID = unchecked((uint)(-1));

    [NativeTypeName("#define RTC_MAX_TIME_STEP_COUNT 129")]
    public const int RTC_MAX_TIME_STEP_COUNT = 129;

    [NativeTypeName("#define RTC_VERSION_MAJOR 4")]
    public const int RTC_VERSION_MAJOR = 4;

    [NativeTypeName("#define RTC_VERSION_MINOR 0")]
    public const int RTC_VERSION_MINOR = 0;

    [NativeTypeName("#define RTC_VERSION_PATCH 1")]
    public const int RTC_VERSION_PATCH = 1;

    [NativeTypeName("#define RTC_VERSION 40001")]
    public const int RTC_VERSION = 40001;

    [NativeTypeName("#define RTC_VERSION_STRING \"4.0.1\"")]
    public static ReadOnlySpan<byte> RTC_VERSION_STRING => new byte[] { 0x34, 0x2E, 0x30, 0x2E, 0x31, 0x00 };

    [NativeTypeName("#define RTC_MAX_INSTANCE_LEVEL_COUNT 1")]
    public const int RTC_MAX_INSTANCE_LEVEL_COUNT = 1;

    [NativeTypeName("#define EMBREE_SYCL_GEOMETRY_CALLBACK 0")]
    public const int EMBREE_SYCL_GEOMETRY_CALLBACK = 0;

    [NativeTypeName("#define EMBREE_MIN_WIDTH 0")]
    public const int EMBREE_MIN_WIDTH = 0;

    [NativeTypeName("#define RTC_MIN_WIDTH EMBREE_MIN_WIDTH")]
    public const int RTC_MIN_WIDTH = 0;

    static EmbreeNative()
    {
        NativeLibrary.SetDllImportResolver(typeof(EmbreeNative).Assembly, (string libraryName, Assembly assembly, DllImportSearchPath? searchPath) =>
        {
            if (libraryName != "embree4")
            {
                return IntPtr.Zero;
            }
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            IntPtr target = IntPtr.Zero;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                NativeLibrary.Load(Path.Combine(dir, "runtimes", $"win-{GetRid()}", "native", "tbbmalloc.dll"), assembly, searchPath);
                NativeLibrary.Load(Path.Combine(dir, "runtimes", $"win-{GetRid()}", "native", "tbb12.dll"), assembly, searchPath);
                target = NativeLibrary.Load(Path.Combine(dir, "runtimes", $"win-{GetRid()}", "native", "embree4.dll"), assembly, searchPath);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                NativeLibrary.Load(Path.Combine(dir, "runtimes", $"linux-{GetRid()}", "native", "libtbbmalloc.so"), assembly, searchPath);
                NativeLibrary.Load(Path.Combine(dir, "runtimes", $"linux-{GetRid()}", "native", "libtbb.so"), assembly, searchPath);
                target = NativeLibrary.Load(Path.Combine(dir, "runtimes", $"linux-{GetRid()}", "native", "libembree4.so"), assembly, searchPath);
            }
            else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                NativeLibrary.Load(Path.Combine(dir, "runtimes", $"osx-{GetRid()}", "native", "libtbb.dylib"), assembly, searchPath);
                target = NativeLibrary.Load(Path.Combine(dir, "runtimes", $"osx-{GetRid()}", "native", "libembree4.dylib"), assembly, searchPath);
            }
            //TODO: other platforms?
            return target;
        });

        static string GetRid()
        {
            return RuntimeInformation.OSArchitecture switch
            {
                Architecture.X64 => "x64",
                Architecture.Arm64 => "arm64",
                _ => string.Empty
            };
        }
    }

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: NativeTypeName("RTCBuffer")]
    public static partial RTCBuffer rtcNewBuffer([NativeTypeName("RTCDevice")] RTCDevice device, [NativeTypeName("size_t")] nuint byteSize);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: NativeTypeName("RTCBuffer")]
    public static partial RTCBuffer rtcNewSharedBuffer([NativeTypeName("RTCDevice")] RTCDevice device, void* ptr, [NativeTypeName("size_t")] nuint byteSize);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void* rtcGetBufferData([NativeTypeName("RTCBuffer")] RTCBuffer buffer);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcRetainBuffer([NativeTypeName("RTCBuffer")] RTCBuffer buffer);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcReleaseBuffer([NativeTypeName("RTCBuffer")] RTCBuffer buffer);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: NativeTypeName("RTCBVH")]
    public static partial RTCBVH rtcNewBVH([NativeTypeName("RTCDevice")] RTCDevice device);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void* rtcBuildBVH([NativeTypeName("const struct RTCBuildArguments *")] RTCBuildArguments* args);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void* rtcThreadLocalAlloc([NativeTypeName("RTCThreadLocalAllocator")] RTCThreadLocalAllocator allocator, [NativeTypeName("size_t")] nuint bytes, [NativeTypeName("size_t")] nuint align);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcRetainBVH([NativeTypeName("RTCBVH")] RTCBVH bvh);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcReleaseBVH([NativeTypeName("RTCBVH")] RTCBVH bvh);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: NativeTypeName("RTCDevice")]
    public static partial RTCDevice rtcNewDevice([NativeTypeName("const char *")] sbyte* config);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcRetainDevice([NativeTypeName("RTCDevice")] RTCDevice device);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcReleaseDevice([NativeTypeName("RTCDevice")] RTCDevice device);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: NativeTypeName("ssize_t")]
    public static partial long rtcGetDeviceProperty([NativeTypeName("RTCDevice")] RTCDevice device, [NativeTypeName("enum RTCDeviceProperty")] RTCDeviceProperty prop);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetDeviceProperty([NativeTypeName("RTCDevice")] RTCDevice device, [NativeTypeName("const enum RTCDeviceProperty")] RTCDeviceProperty prop, [NativeTypeName("ssize_t")] long value);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: NativeTypeName("enum RTCError")]
    public static partial RTCError rtcGetDeviceError([NativeTypeName("RTCDevice")] RTCDevice device);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetDeviceErrorFunction([NativeTypeName("RTCDevice")] RTCDevice device, [NativeTypeName("RTCErrorFunction")] IntPtr error, void* userPtr);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetDeviceMemoryMonitorFunction([NativeTypeName("RTCDevice")] RTCDevice device, [NativeTypeName("RTCMemoryMonitorFunction")] IntPtr memoryMonitor, void* userPtr);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: NativeTypeName("RTCGeometry")]
    public static partial RTCGeometry rtcNewGeometry([NativeTypeName("RTCDevice")] RTCDevice device, [NativeTypeName("enum RTCGeometryType")] RTCGeometryType type);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcRetainGeometry([NativeTypeName("RTCGeometry")] RTCGeometry geometry);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcReleaseGeometry([NativeTypeName("RTCGeometry")] RTCGeometry geometry);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcCommitGeometry([NativeTypeName("RTCGeometry")] RTCGeometry geometry);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcEnableGeometry([NativeTypeName("RTCGeometry")] RTCGeometry geometry);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcDisableGeometry([NativeTypeName("RTCGeometry")] RTCGeometry geometry);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetGeometryTimeStepCount([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("unsigned int")] uint timeStepCount);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetGeometryTimeRange([NativeTypeName("RTCGeometry")] RTCGeometry geometry, float startTime, float endTime);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetGeometryVertexAttributeCount([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("unsigned int")] uint vertexAttributeCount);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetGeometryMask([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("unsigned int")] uint mask);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetGeometryBuildQuality([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("enum RTCBuildQuality")] RTCBuildQuality quality);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetGeometryMaxRadiusScale([NativeTypeName("RTCGeometry")] RTCGeometry geometry, float maxRadiusScale);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetGeometryBuffer([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("enum RTCBufferType")] RTCBufferType type, [NativeTypeName("unsigned int")] uint slot, [NativeTypeName("enum RTCFormat")] RTCFormat format, [NativeTypeName("RTCBuffer")] RTCBuffer buffer, [NativeTypeName("size_t")] nuint byteOffset, [NativeTypeName("size_t")] nuint byteStride, [NativeTypeName("size_t")] nuint itemCount);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetSharedGeometryBuffer([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("enum RTCBufferType")] RTCBufferType type, [NativeTypeName("unsigned int")] uint slot, [NativeTypeName("enum RTCFormat")] RTCFormat format, [NativeTypeName("const void *")] void* ptr, [NativeTypeName("size_t")] nuint byteOffset, [NativeTypeName("size_t")] nuint byteStride, [NativeTypeName("size_t")] nuint itemCount);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void* rtcSetNewGeometryBuffer([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("enum RTCBufferType")] RTCBufferType type, [NativeTypeName("unsigned int")] uint slot, [NativeTypeName("enum RTCFormat")] RTCFormat format, [NativeTypeName("size_t")] nuint byteStride, [NativeTypeName("size_t")] nuint itemCount);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void* rtcGetGeometryBufferData([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("enum RTCBufferType")] RTCBufferType type, [NativeTypeName("unsigned int")] uint slot);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcUpdateGeometryBuffer([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("enum RTCBufferType")] RTCBufferType type, [NativeTypeName("unsigned int")] uint slot);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetGeometryIntersectFilterFunction([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("RTCFilterFunctionN")] IntPtr filter);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetGeometryOccludedFilterFunction([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("RTCFilterFunctionN")] IntPtr filter);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetGeometryEnableFilterFunctionFromArguments([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("bool")] byte enable);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetGeometryUserData([NativeTypeName("RTCGeometry")] RTCGeometry geometry, void* ptr);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void* rtcGetGeometryUserData([NativeTypeName("RTCGeometry")] RTCGeometry geometry);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetGeometryPointQueryFunction([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("RTCPointQueryFunction")] IntPtr pointQuery);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetGeometryUserPrimitiveCount([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("unsigned int")] uint userPrimitiveCount);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetGeometryBoundsFunction([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("RTCBoundsFunction")] IntPtr bounds, void* userPtr);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetGeometryIntersectFunction([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("RTCIntersectFunctionN")] IntPtr intersect);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetGeometryOccludedFunction([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("RTCOccludedFunctionN")] IntPtr occluded);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcInvokeIntersectFilterFromGeometry([NativeTypeName("const struct RTCIntersectFunctionNArguments *")] RTCIntersectFunctionNArguments* args, [NativeTypeName("const struct RTCFilterFunctionNArguments *")] RTCFilterFunctionNArguments* filterArgs);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcInvokeOccludedFilterFromGeometry([NativeTypeName("const struct RTCOccludedFunctionNArguments *")] RTCOccludedFunctionNArguments* args, [NativeTypeName("const struct RTCFilterFunctionNArguments *")] RTCFilterFunctionNArguments* filterArgs);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetGeometryInstancedScene([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("RTCScene")] RTCScene scene);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetGeometryTransform([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("unsigned int")] uint timeStep, [NativeTypeName("enum RTCFormat")] RTCFormat format, [NativeTypeName("const void *")] void* xfm);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetGeometryTransformQuaternion([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("unsigned int")] uint timeStep, [NativeTypeName("const struct RTCQuaternionDecomposition *")] RTCQuaternionDecomposition* qd);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcGetGeometryTransform([NativeTypeName("RTCGeometry")] RTCGeometry geometry, float time, [NativeTypeName("enum RTCFormat")] RTCFormat format, void* xfm);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetGeometryTessellationRate([NativeTypeName("RTCGeometry")] RTCGeometry geometry, float tessellationRate);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetGeometryTopologyCount([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("unsigned int")] uint topologyCount);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetGeometrySubdivisionMode([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("unsigned int")] uint topologyID, [NativeTypeName("enum RTCSubdivisionMode")] RTCSubdivisionMode mode);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetGeometryVertexAttributeTopology([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("unsigned int")] uint vertexAttributeID, [NativeTypeName("unsigned int")] uint topologyID);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetGeometryDisplacementFunction([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("RTCDisplacementFunctionN")] IntPtr displacement);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: NativeTypeName("unsigned int")]
    public static partial uint rtcGetGeometryFirstHalfEdge([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("unsigned int")] uint faceID);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: NativeTypeName("unsigned int")]
    public static partial uint rtcGetGeometryFace([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("unsigned int")] uint edgeID);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: NativeTypeName("unsigned int")]
    public static partial uint rtcGetGeometryNextHalfEdge([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("unsigned int")] uint edgeID);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: NativeTypeName("unsigned int")]
    public static partial uint rtcGetGeometryPreviousHalfEdge([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("unsigned int")] uint edgeID);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: NativeTypeName("unsigned int")]
    public static partial uint rtcGetGeometryOppositeHalfEdge([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("unsigned int")] uint topologyID, [NativeTypeName("unsigned int")] uint edgeID);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcInterpolate([NativeTypeName("const struct RTCInterpolateArguments *")] RTCInterpolateArguments* args);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcInterpolateN([NativeTypeName("const struct RTCInterpolateNArguments *")] RTCInterpolateNArguments* args);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: NativeTypeName("RTCScene")]
    public static partial RTCScene rtcNewScene([NativeTypeName("RTCDevice")] RTCDevice device);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: NativeTypeName("RTCDevice")]
    public static partial RTCDevice rtcGetSceneDevice([NativeTypeName("RTCScene")] RTCScene hscene);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcRetainScene([NativeTypeName("RTCScene")] RTCScene scene);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcReleaseScene([NativeTypeName("RTCScene")] RTCScene scene);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: NativeTypeName("unsigned int")]
    public static partial uint rtcAttachGeometry([NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("RTCGeometry")] RTCGeometry geometry);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcAttachGeometryByID([NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("unsigned int")] uint geomID);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcDetachGeometry([NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("unsigned int")] uint geomID);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: NativeTypeName("RTCGeometry")]
    public static partial RTCGeometry rtcGetGeometry([NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("unsigned int")] uint geomID);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: NativeTypeName("RTCGeometry")]
    public static partial RTCGeometry rtcGetGeometryThreadSafe([NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("unsigned int")] uint geomID);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void* rtcGetGeometryUserDataFromScene([NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("unsigned int")] uint geomID);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcCommitScene([NativeTypeName("RTCScene")] RTCScene scene);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcJoinCommitScene([NativeTypeName("RTCScene")] RTCScene scene);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetSceneProgressMonitorFunction([NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("RTCProgressMonitorFunction")] IntPtr progress, void* ptr);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetSceneBuildQuality([NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("enum RTCBuildQuality")] RTCBuildQuality quality);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcSetSceneFlags([NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("enum RTCSceneFlags")] RTCSceneFlags flags);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: NativeTypeName("enum RTCSceneFlags")]
    public static partial RTCSceneFlags rtcGetSceneFlags([NativeTypeName("RTCScene")] RTCScene scene);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcGetSceneBounds([NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("struct RTCBounds *")] RTCBounds* bounds_o);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcGetSceneLinearBounds([NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("struct RTCLinearBounds *")] RTCLinearBounds* bounds_o);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: NativeTypeName("bool")]
    public static partial byte rtcPointQuery([NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("struct RTCPointQuery *")] RTCPointQuery* query, [NativeTypeName("struct RTCPointQueryContext *")] RTCPointQueryContext* context, [NativeTypeName("RTCPointQueryFunction")] IntPtr queryFunc, void* userPtr);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: NativeTypeName("bool")]
    public static partial byte rtcPointQuery4([NativeTypeName("const int *")] int* valid, [NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("struct RTCPointQuery4 *")] RTCPointQuery4* query, [NativeTypeName("struct RTCPointQueryContext *")] RTCPointQueryContext* context, [NativeTypeName("RTCPointQueryFunction")] IntPtr queryFunc, void** userPtr);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: NativeTypeName("bool")]
    public static partial byte rtcPointQuery8([NativeTypeName("const int *")] int* valid, [NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("struct RTCPointQuery8 *")] RTCPointQuery8* query, [NativeTypeName("struct RTCPointQueryContext *")] RTCPointQueryContext* context, [NativeTypeName("RTCPointQueryFunction")] IntPtr queryFunc, void** userPtr);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    [return: NativeTypeName("bool")]
    public static partial byte rtcPointQuery16([NativeTypeName("const int *")] int* valid, [NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("struct RTCPointQuery16 *")] RTCPointQuery16* query, [NativeTypeName("struct RTCPointQueryContext *")] RTCPointQueryContext* context, [NativeTypeName("RTCPointQueryFunction")] IntPtr queryFunc, void** userPtr);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcIntersect1([NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("struct RTCRayHit *")] RTCRayHit* rayhit, [NativeTypeName("struct RTCIntersectArguments *")] RTCIntersectArguments* args = null);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcIntersect4([NativeTypeName("const int *")] int* valid, [NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("struct RTCRayHit4 *")] RTCRayHit4* rayhit, [NativeTypeName("struct RTCIntersectArguments *")] RTCIntersectArguments* args = null);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcIntersect8([NativeTypeName("const int *")] int* valid, [NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("struct RTCRayHit8 *")] RTCRayHit8* rayhit, [NativeTypeName("struct RTCIntersectArguments *")] RTCIntersectArguments* args = null);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcIntersect16([NativeTypeName("const int *")] int* valid, [NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("struct RTCRayHit16 *")] RTCRayHit16* rayhit, [NativeTypeName("struct RTCIntersectArguments *")] RTCIntersectArguments* args = null);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcForwardIntersect1([NativeTypeName("const struct RTCIntersectFunctionNArguments *")] RTCIntersectFunctionNArguments* args, [NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("struct RTCRay *")] RTCRay* ray, [NativeTypeName("unsigned int")] uint instID);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcForwardIntersect4([NativeTypeName("const int *")] int* valid, [NativeTypeName("const struct RTCIntersectFunctionNArguments *")] RTCIntersectFunctionNArguments* args, [NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("struct RTCRay4 *")] RTCRay4* ray, [NativeTypeName("unsigned int")] uint instID);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcForwardIntersect8([NativeTypeName("const int *")] int* valid, [NativeTypeName("const struct RTCIntersectFunctionNArguments *")] RTCIntersectFunctionNArguments* args, [NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("struct RTCRay8 *")] RTCRay8* ray, [NativeTypeName("unsigned int")] uint instID);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcForwardIntersect16([NativeTypeName("const int *")] int* valid, [NativeTypeName("const struct RTCIntersectFunctionNArguments *")] RTCIntersectFunctionNArguments* args, [NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("struct RTCRay16 *")] RTCRay16* ray, [NativeTypeName("unsigned int")] uint instID);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcOccluded1([NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("struct RTCRay *")] RTCRay* ray, [NativeTypeName("struct RTCOccludedArguments *")] RTCOccludedArguments* args = null);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcOccluded4([NativeTypeName("const int *")] int* valid, [NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("struct RTCRay4 *")] RTCRay4* ray, [NativeTypeName("struct RTCOccludedArguments *")] RTCOccludedArguments* args = null);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcOccluded8([NativeTypeName("const int *")] int* valid, [NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("struct RTCRay8 *")] RTCRay8* ray, [NativeTypeName("struct RTCOccludedArguments *")] RTCOccludedArguments* args = null);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcOccluded16([NativeTypeName("const int *")] int* valid, [NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("struct RTCRay16 *")] RTCRay16* ray, [NativeTypeName("struct RTCOccludedArguments *")] RTCOccludedArguments* args = null);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcForwardOccluded1([NativeTypeName("const struct RTCOccludedFunctionNArguments *")] RTCOccludedFunctionNArguments* args, [NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("struct RTCRay *")] RTCRay* ray, [NativeTypeName("unsigned int")] uint instID);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcForwardOccluded4([NativeTypeName("const int *")] int* valid, [NativeTypeName("const struct RTCOccludedFunctionNArguments *")] RTCOccludedFunctionNArguments* args, [NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("struct RTCRay4 *")] RTCRay4* ray, [NativeTypeName("unsigned int")] uint instID);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcForwardOccluded8([NativeTypeName("const int *")] int* valid, [NativeTypeName("const struct RTCOccludedFunctionNArguments *")] RTCOccludedFunctionNArguments* args, [NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("struct RTCRay8 *")] RTCRay8* ray, [NativeTypeName("unsigned int")] uint instID);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcForwardOccluded16([NativeTypeName("const int *")] int* valid, [NativeTypeName("const struct RTCOccludedFunctionNArguments *")] RTCOccludedFunctionNArguments* args, [NativeTypeName("RTCScene")] RTCScene scene, [NativeTypeName("struct RTCRay16 *")] RTCRay16* ray, [NativeTypeName("unsigned int")] uint instID);

    [LibraryImport("embree4")]
    [UnmanagedCallConv(CallConvs = new Type[] { typeof(CallConvCdecl) })]
    public static partial void rtcCollide([NativeTypeName("RTCScene")] RTCScene scene0, [NativeTypeName("RTCScene")] RTCScene scene1, [NativeTypeName("RTCCollideFunc")] IntPtr callback, void* userPtr);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NativeTypeName("struct RTCBuildArguments")]
    public static RTCBuildArguments rtcDefaultBuildArguments()
    {
        RTCBuildArguments args = new RTCBuildArguments();

        args.byteSize = ((nuint)sizeof(RTCBuildArguments));
        args.buildQuality = RTCBuildQuality.RTC_BUILD_QUALITY_MEDIUM;
        args.buildFlags = RTCBuildFlags.RTC_BUILD_FLAG_NONE;
        args.maxBranchingFactor = 2;
        args.maxDepth = 32;
        args.sahBlockSize = 1;
        args.minLeafSize = 1;
        args.maxLeafSize = (uint)(RTCBuildConstants.RTC_BUILD_MAX_PRIMITIVES_PER_LEAF);
        args.traversalCost = 1.0f;
        args.intersectionCost = 1.0f;
        args.bvh = new RTCBVH() { Ptr = IntPtr.Zero };
        args.primitives = null;
        args.primitiveCount = 0;
        args.primitiveArrayCapacity = 0;
        args.createNode = IntPtr.Zero;
        args.setNodeChildren = IntPtr.Zero;
        args.setNodeBounds = IntPtr.Zero;
        args.createLeaf = IntPtr.Zero;
        args.splitPrimitive = IntPtr.Zero;
        args.buildProgress = IntPtr.Zero;
        args.userPtr = null;
        return args;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void rtcInitQuaternionDecomposition([NativeTypeName("struct RTCQuaternionDecomposition *")] RTCQuaternionDecomposition* qdecomp)
    {
        qdecomp->scale_x = 1.0f;
        qdecomp->scale_y = 1.0f;
        qdecomp->scale_z = 1.0f;
        qdecomp->skew_xy = 0.0f;
        qdecomp->skew_xz = 0.0f;
        qdecomp->skew_yz = 0.0f;
        qdecomp->shift_x = 0.0f;
        qdecomp->shift_y = 0.0f;
        qdecomp->shift_z = 0.0f;
        qdecomp->quaternion_r = 1.0f;
        qdecomp->quaternion_i = 0.0f;
        qdecomp->quaternion_j = 0.0f;
        qdecomp->quaternion_k = 0.0f;
        qdecomp->translation_x = 0.0f;
        qdecomp->translation_y = 0.0f;
        qdecomp->translation_z = 0.0f;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void rtcQuaternionDecompositionSetQuaternion([NativeTypeName("struct RTCQuaternionDecomposition *")] RTCQuaternionDecomposition* qdecomp, float r, float i, float j, float k)
    {
        qdecomp->quaternion_r = r;
        qdecomp->quaternion_i = i;
        qdecomp->quaternion_j = j;
        qdecomp->quaternion_k = k;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void rtcQuaternionDecompositionSetScale([NativeTypeName("struct RTCQuaternionDecomposition *")] RTCQuaternionDecomposition* qdecomp, float scale_x, float scale_y, float scale_z)
    {
        qdecomp->scale_x = scale_x;
        qdecomp->scale_y = scale_y;
        qdecomp->scale_z = scale_z;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void rtcQuaternionDecompositionSetSkew([NativeTypeName("struct RTCQuaternionDecomposition *")] RTCQuaternionDecomposition* qdecomp, float skew_xy, float skew_xz, float skew_yz)
    {
        qdecomp->skew_xy = skew_xy;
        qdecomp->skew_xz = skew_xz;
        qdecomp->skew_yz = skew_yz;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void rtcQuaternionDecompositionSetShift([NativeTypeName("struct RTCQuaternionDecomposition *")] RTCQuaternionDecomposition* qdecomp, float shift_x, float shift_y, float shift_z)
    {
        qdecomp->shift_x = shift_x;
        qdecomp->shift_y = shift_y;
        qdecomp->shift_z = shift_z;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void rtcQuaternionDecompositionSetTranslation([NativeTypeName("struct RTCQuaternionDecomposition *")] RTCQuaternionDecomposition* qdecomp, float translation_x, float translation_y, float translation_z)
    {
        qdecomp->translation_x = translation_x;
        qdecomp->translation_y = translation_y;
        qdecomp->translation_z = translation_z;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void rtcInitRayQueryContext([NativeTypeName("struct RTCRayQueryContext *")] RTCRayQueryContext* context)
    {
        uint l = 0;

        for (; l < 1; ++l)
        {
            context->instID[l] = unchecked((uint)(-1));
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void rtcInitPointQueryContext([NativeTypeName("struct RTCPointQueryContext *")] RTCPointQueryContext* context)
    {
        context->instStackSize = 0;
        context->instID[0] = unchecked((uint)(-1));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void rtcInterpolate0([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("unsigned int")] uint primID, float u, float v, [NativeTypeName("enum RTCBufferType")] RTCBufferType bufferType, [NativeTypeName("unsigned int")] uint bufferSlot, float* P, [NativeTypeName("unsigned int")] uint valueCount)
    {
        RTCInterpolateArguments args = new RTCInterpolateArguments();

        args.geometry = geometry;
        args.primID = primID;
        args.u = u;
        args.v = v;
        args.bufferType = bufferType;
        args.bufferSlot = bufferSlot;
        args.P = P;
        args.dPdu = null;
        args.dPdv = null;
        args.ddPdudu = null;
        args.ddPdvdv = null;
        args.ddPdudv = null;
        args.valueCount = valueCount;
        rtcInterpolate(&args);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void rtcInterpolate1([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("unsigned int")] uint primID, float u, float v, [NativeTypeName("enum RTCBufferType")] RTCBufferType bufferType, [NativeTypeName("unsigned int")] uint bufferSlot, float* P, float* dPdu, float* dPdv, [NativeTypeName("unsigned int")] uint valueCount)
    {
        RTCInterpolateArguments args = new RTCInterpolateArguments();

        args.geometry = geometry;
        args.primID = primID;
        args.u = u;
        args.v = v;
        args.bufferType = bufferType;
        args.bufferSlot = bufferSlot;
        args.P = P;
        args.dPdu = dPdu;
        args.dPdv = dPdv;
        args.ddPdudu = null;
        args.ddPdvdv = null;
        args.ddPdudv = null;
        args.valueCount = valueCount;
        rtcInterpolate(&args);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void rtcInterpolate2([NativeTypeName("RTCGeometry")] RTCGeometry geometry, [NativeTypeName("unsigned int")] uint primID, float u, float v, [NativeTypeName("enum RTCBufferType")] RTCBufferType bufferType, [NativeTypeName("unsigned int")] uint bufferSlot, float* P, float* dPdu, float* dPdv, float* ddPdudu, float* ddPdvdv, float* ddPdudv, [NativeTypeName("unsigned int")] uint valueCount)
    {
        RTCInterpolateArguments args = new RTCInterpolateArguments();

        args.geometry = geometry;
        args.primID = primID;
        args.u = u;
        args.v = v;
        args.bufferType = bufferType;
        args.bufferSlot = bufferSlot;
        args.P = P;
        args.dPdu = dPdu;
        args.dPdv = dPdv;
        args.ddPdudu = ddPdudu;
        args.ddPdvdv = ddPdvdv;
        args.ddPdudv = ddPdudv;
        args.valueCount = valueCount;
        rtcInterpolate(&args);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NativeTypeName("float &")]
    public static float* RTCRayN_org_x(RTCRayN* ray, [NativeTypeName("unsigned int")] uint N, [NativeTypeName("unsigned int")] uint i)
    {
        return &unchecked((float*)(ray))[0 * N + i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NativeTypeName("float &")]
    public static float* RTCRayN_org_y(RTCRayN* ray, [NativeTypeName("unsigned int")] uint N, [NativeTypeName("unsigned int")] uint i)
    {
        return &unchecked((float*)(ray))[1 * N + i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NativeTypeName("float &")]
    public static float* RTCRayN_org_z(RTCRayN* ray, [NativeTypeName("unsigned int")] uint N, [NativeTypeName("unsigned int")] uint i)
    {
        return &unchecked((float*)(ray))[2 * N + i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NativeTypeName("float &")]
    public static float* RTCRayN_tnear(RTCRayN* ray, [NativeTypeName("unsigned int")] uint N, [NativeTypeName("unsigned int")] uint i)
    {
        return &unchecked((float*)(ray))[3 * N + i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NativeTypeName("float &")]
    public static float* RTCRayN_dir_x(RTCRayN* ray, [NativeTypeName("unsigned int")] uint N, [NativeTypeName("unsigned int")] uint i)
    {
        return &unchecked((float*)(ray))[4 * N + i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NativeTypeName("float &")]
    public static float* RTCRayN_dir_y(RTCRayN* ray, [NativeTypeName("unsigned int")] uint N, [NativeTypeName("unsigned int")] uint i)
    {
        return &unchecked((float*)(ray))[5 * N + i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NativeTypeName("float &")]
    public static float* RTCRayN_dir_z(RTCRayN* ray, [NativeTypeName("unsigned int")] uint N, [NativeTypeName("unsigned int")] uint i)
    {
        return &unchecked((float*)(ray))[6 * N + i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NativeTypeName("float &")]
    public static float* RTCRayN_time(RTCRayN* ray, [NativeTypeName("unsigned int")] uint N, [NativeTypeName("unsigned int")] uint i)
    {
        return &unchecked((float*)(ray))[7 * N + i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NativeTypeName("float &")]
    public static float* RTCRayN_tfar(RTCRayN* ray, [NativeTypeName("unsigned int")] uint N, [NativeTypeName("unsigned int")] uint i)
    {
        return &unchecked((float*)(ray))[8 * N + i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NativeTypeName("unsigned int &")]
    public static uint* RTCRayN_mask(RTCRayN* ray, [NativeTypeName("unsigned int")] uint N, [NativeTypeName("unsigned int")] uint i)
    {
        return &((uint*)(ray))[9 * N + i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NativeTypeName("unsigned int &")]
    public static uint* RTCRayN_id(RTCRayN* ray, [NativeTypeName("unsigned int")] uint N, [NativeTypeName("unsigned int")] uint i)
    {
        return &((uint*)(ray))[10 * N + i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NativeTypeName("unsigned int &")]
    public static uint* RTCRayN_flags(RTCRayN* ray, [NativeTypeName("unsigned int")] uint N, [NativeTypeName("unsigned int")] uint i)
    {
        return &((uint*)(ray))[11 * N + i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NativeTypeName("float &")]
    public static float* RTCHitN_Ng_x(RTCHitN* hit, [NativeTypeName("unsigned int")] uint N, [NativeTypeName("unsigned int")] uint i)
    {
        return &unchecked((float*)(hit))[0 * N + i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NativeTypeName("float &")]
    public static float* RTCHitN_Ng_y(RTCHitN* hit, [NativeTypeName("unsigned int")] uint N, [NativeTypeName("unsigned int")] uint i)
    {
        return &unchecked((float*)(hit))[1 * N + i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NativeTypeName("float &")]
    public static float* RTCHitN_Ng_z(RTCHitN* hit, [NativeTypeName("unsigned int")] uint N, [NativeTypeName("unsigned int")] uint i)
    {
        return &unchecked((float*)(hit))[2 * N + i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NativeTypeName("float &")]
    public static float* RTCHitN_u(RTCHitN* hit, [NativeTypeName("unsigned int")] uint N, [NativeTypeName("unsigned int")] uint i)
    {
        return &unchecked((float*)(hit))[3 * N + i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NativeTypeName("float &")]
    public static float* RTCHitN_v(RTCHitN* hit, [NativeTypeName("unsigned int")] uint N, [NativeTypeName("unsigned int")] uint i)
    {
        return &unchecked((float*)(hit))[4 * N + i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NativeTypeName("unsigned int &")]
    public static uint* RTCHitN_primID(RTCHitN* hit, [NativeTypeName("unsigned int")] uint N, [NativeTypeName("unsigned int")] uint i)
    {
        return &((uint*)(hit))[5 * N + i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NativeTypeName("unsigned int &")]
    public static uint* RTCHitN_geomID(RTCHitN* hit, [NativeTypeName("unsigned int")] uint N, [NativeTypeName("unsigned int")] uint i)
    {
        return &((uint*)(hit))[6 * N + i];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [return: NativeTypeName("unsigned int &")]
    public static uint* RTCHitN_instID(RTCHitN* hit, [NativeTypeName("unsigned int")] uint N, [NativeTypeName("unsigned int")] uint i, [NativeTypeName("unsigned int")] uint l)
    {
        return &((uint*)(hit))[7 * N + i + N * l];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RTCRayN* RTCRayHitN_RayN(RTCRayHitN* rayhit, [NativeTypeName("unsigned int")] uint N)
    {
        return (RTCRayN*)(&unchecked((float*)(rayhit))[0 * N]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RTCHitN* RTCRayHitN_HitN(RTCRayHitN* rayhit, [NativeTypeName("unsigned int")] uint N)
    {
        return (RTCHitN*)(&unchecked((float*)(rayhit))[12 * N]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RTCRay rtcGetRayFromRayN(RTCRayN* rayN, [NativeTypeName("unsigned int")] uint N, [NativeTypeName("unsigned int")] uint i)
    {
        RTCRay ray = new RTCRay();

        ray.org_x = *RTCRayN_org_x(rayN, N, i);
        ray.org_y = *RTCRayN_org_y(rayN, N, i);
        ray.org_z = *RTCRayN_org_z(rayN, N, i);
        ray.tnear = *RTCRayN_tnear(rayN, N, i);
        ray.dir_x = *RTCRayN_dir_x(rayN, N, i);
        ray.dir_y = *RTCRayN_dir_y(rayN, N, i);
        ray.dir_z = *RTCRayN_dir_z(rayN, N, i);
        ray.time = *RTCRayN_time(rayN, N, i);
        ray.tfar = *RTCRayN_tfar(rayN, N, i);
        ray.mask = *RTCRayN_mask(rayN, N, i);
        ray.id = *RTCRayN_id(rayN, N, i);
        ray.flags = *RTCRayN_flags(rayN, N, i);
        return ray;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RTCHit rtcGetHitFromHitN(RTCHitN* hitN, [NativeTypeName("unsigned int")] uint N, [NativeTypeName("unsigned int")] uint i)
    {
        RTCHit hit = new RTCHit();

        hit.Ng_x = *RTCHitN_Ng_x(hitN, N, i);
        hit.Ng_y = *RTCHitN_Ng_y(hitN, N, i);
        hit.Ng_z = *RTCHitN_Ng_z(hitN, N, i);
        hit.u = *RTCHitN_u(hitN, N, i);
        hit.v = *RTCHitN_v(hitN, N, i);
        hit.primID = *RTCHitN_primID(hitN, N, i);
        hit.geomID = *RTCHitN_geomID(hitN, N, i);
        for (uint l = 0; l < 1; l++)
        {
            hit.instID[l] = *RTCHitN_instID(hitN, N, i, l);
        }

        return hit;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void rtcCopyHitToHitN(RTCHitN* hitN, [NativeTypeName("const RTCHit *")] RTCHit* hit, [NativeTypeName("unsigned int")] uint N, [NativeTypeName("unsigned int")] uint i)
    {
        *RTCHitN_Ng_x(hitN, N, i) = hit->Ng_x;
        *RTCHitN_Ng_y(hitN, N, i) = hit->Ng_y;
        *RTCHitN_Ng_z(hitN, N, i) = hit->Ng_z;
        *RTCHitN_u(hitN, N, i) = hit->u;
        *RTCHitN_v(hitN, N, i) = hit->v;
        *RTCHitN_primID(hitN, N, i) = hit->primID;
        *RTCHitN_geomID(hitN, N, i) = hit->geomID;
        for (uint l = 0; l < 1; l++)
        {
            *RTCHitN_instID(hitN, N, i, l) = hit->instID[l];
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RTCRayHit rtcGetRayHitFromRayHitN(RTCRayHitN* rayhitN, [NativeTypeName("unsigned int")] uint N, [NativeTypeName("unsigned int")] uint i)
    {
        RTCRayHit rh = new RTCRayHit();
        RTCRayN* ray = RTCRayHitN_RayN(rayhitN, N);

        rh.ray.org_x = *RTCRayN_org_x(ray, N, i);
        rh.ray.org_y = *RTCRayN_org_y(ray, N, i);
        rh.ray.org_z = *RTCRayN_org_z(ray, N, i);
        rh.ray.tnear = *RTCRayN_tnear(ray, N, i);
        rh.ray.dir_x = *RTCRayN_dir_x(ray, N, i);
        rh.ray.dir_y = *RTCRayN_dir_y(ray, N, i);
        rh.ray.dir_z = *RTCRayN_dir_z(ray, N, i);
        rh.ray.time = *RTCRayN_time(ray, N, i);
        rh.ray.tfar = *RTCRayN_tfar(ray, N, i);
        rh.ray.mask = *RTCRayN_mask(ray, N, i);
        rh.ray.id = *RTCRayN_id(ray, N, i);
        rh.ray.flags = *RTCRayN_flags(ray, N, i);
        RTCHitN* hit = RTCRayHitN_HitN(rayhitN, N);

        rh.hit.Ng_x = *RTCHitN_Ng_x(hit, N, i);
        rh.hit.Ng_y = *RTCHitN_Ng_y(hit, N, i);
        rh.hit.Ng_z = *RTCHitN_Ng_z(hit, N, i);
        rh.hit.u = *RTCHitN_u(hit, N, i);
        rh.hit.v = *RTCHitN_v(hit, N, i);
        rh.hit.primID = *RTCHitN_primID(hit, N, i);
        rh.hit.geomID = *RTCHitN_geomID(hit, N, i);
        for (uint l = 0; l < 1; l++)
        {
            rh.hit.instID[l] = *RTCHitN_instID(hit, N, i, l);
        }

        return rh;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void rtcInitIntersectArguments([NativeTypeName("struct RTCIntersectArguments *")] RTCIntersectArguments* args)
    {
        args->flags = RTCRayQueryFlags.RTC_RAY_QUERY_FLAG_INCOHERENT;
        args->feature_mask = RTCFeatureFlags.RTC_FEATURE_FLAG_ALL;
        args->context = null;
        args->filter = IntPtr.Zero;
        args->intersect = IntPtr.Zero;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void rtcInitOccludedArguments([NativeTypeName("struct RTCOccludedArguments *")] RTCOccludedArguments* args)
    {
        args->flags = RTCRayQueryFlags.RTC_RAY_QUERY_FLAG_INCOHERENT;
        args->feature_mask = RTCFeatureFlags.RTC_FEATURE_FLAG_ALL;
        args->context = null;
        args->filter = IntPtr.Zero;
        args->occluded = IntPtr.Zero;
    }
}
