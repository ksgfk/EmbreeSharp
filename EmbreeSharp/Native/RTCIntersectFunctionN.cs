using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void RTCIntersectFunctionN([NativeTypeName("const struct RTCIntersectFunctionNArguments *")] RTCIntersectFunctionNArguments* args);
