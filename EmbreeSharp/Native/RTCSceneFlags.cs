namespace EmbreeSharp.Native;

public enum RTCSceneFlags
{
    RTC_SCENE_FLAG_NONE = 0,
    RTC_SCENE_FLAG_DYNAMIC = (1 << 0),
    RTC_SCENE_FLAG_COMPACT = (1 << 1),
    RTC_SCENE_FLAG_ROBUST = (1 << 2),
    RTC_SCENE_FLAG_FILTER_FUNCTION_IN_ARGUMENTS = (1 << 3),
}
