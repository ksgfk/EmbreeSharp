using System;

namespace EmbreeSharp.Native;

[Flags]
public enum RTCCurveFlags : int
{
    RTC_CURVE_FLAG_NEIGHBOR_LEFT = (1 << 0),
    RTC_CURVE_FLAG_NEIGHBOR_RIGHT = (1 << 1),
}
