using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RTCFilterFunctionNArguments
    {
        public int* valid;
        public void* geometryUserPtr;
        public RTCRayQueryContext* context;
        // RTCRayN* ray;
        // RTCHitN* hit;
        public uint N;
    }
}
