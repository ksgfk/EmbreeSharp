namespace EmbreeSharp.Native
{
    /// <summary>
    /// Error callback function
    /// </summary>
    public unsafe delegate void RTCErrorFunction(void* userPtr, RTCError code, [NativeType("const char*")] byte* str);
}
