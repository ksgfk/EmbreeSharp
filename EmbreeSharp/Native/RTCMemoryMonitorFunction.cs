using System;

namespace EmbreeSharp.Native
{
    /// <summary>
    /// Memory monitor callback function
    /// </summary>
    public unsafe delegate bool RTCMemoryMonitorFunction(void* ptr, [NativeType("ssize_t")] IntPtr bytes, bool post);
}
