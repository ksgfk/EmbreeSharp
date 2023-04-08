using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential, Pack = 16, Size = 48)]
public unsafe partial struct RTCPointQueryFunctionArguments
{
    [NativeTypeName("struct RTCPointQuery *")]
    public RTCPointQuery* query;

    public void* userPtr;

    [NativeTypeName("unsigned int")]
    public uint primID;

    [NativeTypeName("unsigned int")]
    public uint geomID;

    [NativeTypeName("struct RTCPointQueryContext *")]
    public RTCPointQueryContext* context;

    public float similarityScale;
}
