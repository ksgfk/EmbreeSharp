using System.Runtime.InteropServices;

namespace EmbreeSharp.Native;

[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
public unsafe delegate void RTCSetNodeChildrenFunction(void* nodePtr, void** children, [NativeTypeName("unsigned int")] uint childCount, void* userPtr);
