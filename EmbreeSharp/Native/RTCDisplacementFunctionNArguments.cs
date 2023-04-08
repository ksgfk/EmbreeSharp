using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[StructLayout(LayoutKind.Sequential)]
public unsafe partial struct RTCDisplacementFunctionNArguments
{
    public void* geometryUserPtr;

    [NativeTypeName("RTCGeometry")]
    public RTCGeometry geometry;

    [NativeTypeName("unsigned int")]
    public uint primID;

    [NativeTypeName("unsigned int")]
    public uint timeStep;

    [NativeTypeName("const float *")]
    public float* u;

    [NativeTypeName("const float *")]
    public float* v;

    [NativeTypeName("const float *")]
    public float* Ng_x;

    [NativeTypeName("const float *")]
    public float* Ng_y;

    [NativeTypeName("const float *")]
    public float* Ng_z;

    public float* P_x;

    public float* P_y;

    public float* P_z;

    [NativeTypeName("unsigned int")]
    public uint N;
}
