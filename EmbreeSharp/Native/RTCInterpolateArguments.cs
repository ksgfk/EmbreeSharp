using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct RTCInterpolateArguments
{
    [NativeTypeName("RTCGeometry")]
    public RTCGeometry geometry;

    [NativeTypeName("unsigned int")]
    public uint primID;

    public float u;

    public float v;

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
