using System;

namespace EmbreeSharp.Native;

[Flags]
public enum RTCRayQueryFlags : int
{
    RTC_RAY_QUERY_FLAG_NONE = 0,
    RTC_RAY_QUERY_FLAG_INVOKE_ARGUMENT_FILTER = (1 << 1),
    RTC_RAY_QUERY_FLAG_INCOHERENT = (0 << 16),
    RTC_RAY_QUERY_FLAG_COHERENT = (1 << 16),
}
