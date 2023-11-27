using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RTCFilterFunctionNArguments
    {
        public int* valid;
        public void* geometryUserPtr;
        public RTCRayQueryContext* context;
        [NativeType("RTCRayN*")] public RTCRayN ray;
        [NativeType("RTCHitN*")] public RTCHitN hit;
        public uint N;
    }
}
