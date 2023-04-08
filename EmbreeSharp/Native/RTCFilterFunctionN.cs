using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void RTCFilterFunctionN([NativeTypeName("const struct RTCFilterFunctionNArguments *")] RTCFilterFunctionNArguments* args);
