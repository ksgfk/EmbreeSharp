namespace EmbreeSharp.Native
{
    /// <summary>
    /// Ray query flags
    /// </summary>
    enum RTCRayQueryFlags
    {
        /// <summary>
        /// matching intel_ray_flags_t layout
        /// </summary>
        RTC_RAY_QUERY_FLAG_NONE = 0,
        /// <summary>
        /// enable argument filter for each geometry
        /// </summary>
        RTC_RAY_QUERY_FLAG_INVOKE_ARGUMENT_FILTER = 1 << 1,

        /// <summary>
        /// optimize for incoherent rays
        /// </summary>
        RTC_RAY_QUERY_FLAG_INCOHERENT = 0 << 16,
        /// <summary>
        /// optimize for coherent rays
        /// </summary>
        RTC_RAY_QUERY_FLAG_COHERENT = 1 << 16,
    }
}
