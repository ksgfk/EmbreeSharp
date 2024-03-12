using EmbreeSharp.Native;

namespace EmbreeSharp
{
    public static class RTCBuildPrimitiveExtension
    {
        public static RTCBounds GetBounds(ref readonly this RTCBuildPrimitive prim)
        {
            RTCBounds result = new();
            result.SetLowerVector3(new(prim.lower_x, prim.lower_y, prim.lower_z));
            result.SetUpperVector3(new(prim.upper_x, prim.upper_y, prim.upper_z));
            return result;
        }
    }
}
