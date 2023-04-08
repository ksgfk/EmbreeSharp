using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void RTCDisplacementFunctionN([NativeTypeName("const struct RTCDisplacementFunctionNArguments *")] RTCDisplacementFunctionNArguments* args);
