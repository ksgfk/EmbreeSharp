using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    /// <summary>
    /// Hit structure for a single ray
    /// </summary>
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential, Size = 48)]
    public unsafe struct RTCHit
    {
        public const int Alignment = 16;

        /// <summary>
        /// x coordinate of geometry normal
        /// </summary>
        public float Ng_x;
        /// <summary>
        /// y coordinate of geometry normal
        /// </summary>
        public float Ng_y;
        /// <summary>
        /// z coordinate of geometry normal
        /// </summary>
        public float Ng_z;

        /// <summary>
        /// barycentric u coordinate of hit
        /// </summary>
        public float u;
        /// <summary>
        /// barycentric v coordinate of hit
        /// </summary>
        public float v;

        /// <summary>
        /// primitive ID
        /// </summary>
        public uint primID;
        /// <summary>
        /// geometry ID
        /// </summary>
        public uint geomID;
        /// <summary>
        /// instance ID
        /// </summary>
        public fixed uint instID[Embree.RTC_MAX_INSTANCE_LEVEL_COUNT];
        /// <summary>
        /// instance primitive ID
        /// </summary>
        public fixed uint instPrimID[Embree.RTC_MAX_INSTANCE_LEVEL_COUNT];
    }
}
