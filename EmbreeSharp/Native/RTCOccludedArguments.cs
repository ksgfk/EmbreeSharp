using System;
using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct RTCOccludedArguments
{
    [NativeTypeName("enum RTCRayQueryFlags")]
    public RTCRayQueryFlags flags;

    [NativeTypeName("enum RTCFeatureFlags")]
    public RTCFeatureFlags feature_mask;

    [NativeTypeName("struct RTCRayQueryContext *")]
    public RTCRayQueryContext* context;

    [NativeTypeName("RTCFilterFunctionN")]
    public IntPtr filter;

    [NativeTypeName("RTCOccludedFunctionN")]
    public IntPtr occluded;
}
