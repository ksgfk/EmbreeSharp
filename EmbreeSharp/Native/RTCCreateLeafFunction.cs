using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void* RTCCreateLeafFunction([NativeTypeName("RTCThreadLocalAllocator")] RTCThreadLocalAllocator allocator, [NativeTypeName("const struct RTCBuildPrimitive *")] RTCBuildPrimitive* primitives, [NativeTypeName("size_t")] nuint primitiveCount, void* userPtr);
