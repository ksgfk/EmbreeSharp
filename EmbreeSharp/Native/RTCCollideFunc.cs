using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void RTCCollideFunc(void* userPtr, [NativeTypeName("struct RTCCollision *")] RTCCollision* collisions, [NativeTypeName("unsigned int")] uint num_collisions);
