using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void RTCBoundsFunction([NativeTypeName("const struct RTCBoundsFunctionArguments *")] RTCBoundsFunctionArguments* args);
