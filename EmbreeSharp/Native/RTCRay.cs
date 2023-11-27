using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    /// <summary>
    /// Ray structure for a single ray
    /// </summary>
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential)]
    public struct RTCRay
    {
        public const int Alignment = 16;

        /// <summary>
        /// x coordinate of ray origin
        /// </summary>
        public float org_x;
        /// <summary>
        /// y coordinate of ray origin
        /// </summary>
        public float org_y;
        /// <summary>
        /// z coordinate of ray origin
        /// </summary>
        public float org_z;
        /// <summary>
        /// start of ray segment
        /// </summary>
        public float tnear;

        /// <summary>
        /// x coordinate of ray direction
        /// </summary>
        public float dir_x;
        /// <summary>
        /// y coordinate of ray direction
        /// </summary>
        public float dir_y;
        /// <summary>
        /// z coordinate of ray direction
        /// </summary>
        public float dir_z;
        /// <summary>
        /// time of this ray for motion blur
        /// </summary>
        public float time;

        /// <summary>
        /// end of ray segment (set to hit distance)
        /// </summary>
        public float tfar;
        /// <summary>
        /// ray mask
        /// </summary>
        public uint mask;
        /// <summary>
        /// ray ID
        /// </summary>
        public uint id;
        /// <summary>
        /// ray flags
        /// </summary>
        public uint flags;
    }
}
