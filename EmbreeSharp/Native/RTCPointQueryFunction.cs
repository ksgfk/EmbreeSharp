using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate bool RTCPointQueryFunction([NativeTypeName("struct RTCPointQueryFunctionArguments *")] RTCPointQueryFunctionArguments* args);
