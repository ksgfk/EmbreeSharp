using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    /// <summary>
    /// Ray query context passed to intersect/occluded calls
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RTCRayQueryContext
    {
        /// <summary>
        /// The current stack of instance ids.
        /// </summary>
        public fixed uint instID[Embree.RTC_MAX_INSTANCE_LEVEL_COUNT];

        /// <summary>
        /// The current stack of instance primitive ids.
        /// </summary>
        public fixed uint instPrimID[Embree.RTC_MAX_INSTANCE_LEVEL_COUNT];
    }
}
