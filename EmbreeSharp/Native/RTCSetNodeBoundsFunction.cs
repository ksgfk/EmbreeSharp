using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void RTCSetNodeBoundsFunction(void* nodePtr, [NativeTypeName("const struct RTCBounds **")] RTCBounds** bounds, [NativeTypeName("unsigned int")] uint childCount, void* userPtr);
