namespace EmbreeSharp.Native;

public unsafe partial struct RTCFilterFunctionNArguments
{
    public int* valid;

    public void* geometryUserPtr;

    [NativeTypeName("struct RTCRayQueryContext *")]
    public RTCRayQueryContext* context;

    [NativeTypeName("struct RTCRayN *")]
    public RTCRayN* ray;

    [NativeTypeName("struct RTCHitN *")]
    public RTCHitN* hit;

    [NativeTypeName("unsigned int")]
    public uint N;
}
