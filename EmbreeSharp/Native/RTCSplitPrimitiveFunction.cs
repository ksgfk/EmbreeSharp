using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void RTCSplitPrimitiveFunction([NativeTypeName("const struct RTCBuildPrimitive *")] RTCBuildPrimitive* primitive, [NativeTypeName("unsigned int")] uint dimension, float position, [NativeTypeName("struct RTCBounds *")] RTCBounds* leftBounds, [NativeTypeName("struct RTCBounds *")] RTCBounds* rightBounds, void* userPtr);
