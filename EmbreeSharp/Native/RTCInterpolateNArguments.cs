using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct RTCInterpolateNArguments
{
    [NativeTypeName("RTCGeometry")]
    public RTCGeometry geometry;

    [NativeTypeName("const void *")]
    public void* valid;

    [NativeTypeName("const unsigned int *")]
    public uint* primIDs;

    [NativeTypeName("const float *")]
    public float* u;

    [NativeTypeName("const float *")]
    public float* v;

    [NativeTypeName("unsigned int")]
    public uint N;

    [NativeTypeName("enum RTCBufferType")]
    public RTCBufferType bufferType;

    [NativeTypeName("unsigned int")]
    public uint bufferSlot;

    public float* P;

    public float* dPdu;

    public float* dPdv;

    public float* ddPdudu;

    public float* ddPdvdv;

    public float* ddPdudv;

    [NativeTypeName("unsigned int")]
    public uint valueCount;
}
