using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void RTCErrorFunction(void* userPtr, [NativeTypeName("enum RTCError")] RTCError code, [NativeTypeName("const char *")] sbyte* str);
