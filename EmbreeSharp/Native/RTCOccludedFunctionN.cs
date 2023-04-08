using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void RTCOccludedFunctionN([NativeTypeName("const struct RTCOccludedFunctionNArguments *")] RTCOccludedFunctionNArguments* args);
