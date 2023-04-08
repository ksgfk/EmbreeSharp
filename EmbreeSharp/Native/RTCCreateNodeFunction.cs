using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void* RTCCreateNodeFunction([NativeTypeName("RTCThreadLocalAllocator")] RTCThreadLocalAllocator allocator, [NativeTypeName("unsigned int")] uint childCount, void* userPtr);
