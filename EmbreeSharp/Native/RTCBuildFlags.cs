using System;

namespace EmbreeSharp.Native;

[Flags]
public enum RTCBuildFlags : int
{
    RTC_BUILD_FLAG_NONE = 0,
    RTC_BUILD_FLAG_DYNAMIC = (1 << 0),
}
