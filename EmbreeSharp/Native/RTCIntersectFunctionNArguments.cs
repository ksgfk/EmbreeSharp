using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct RTCIntersectFunctionNArguments
{
    public int* valid;

    public void* geometryUserPtr;

    [NativeTypeName("unsigned int")]
    public uint primID;

    [NativeTypeName("struct RTCRayQueryContext *")]
    public RTCRayQueryContext* context;

    [NativeTypeName("struct RTCRayHitN *")]
    public RTCRayHitN* rayhit;

    [NativeTypeName("unsigned int")]
    public uint N;

    [NativeTypeName("unsigned int")]
    public uint geomID;
}
