using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate bool RTCMemoryMonitorFunction(void* ptr, [NativeTypeName("ssize_t")] long bytes, bool post);
