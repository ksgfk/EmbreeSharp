using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    /// <summary>
    /// Structure of a packet of 8 query points
    /// </summary>
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RTCPointQuery8
    {
        public const int Alignment = 32;

        /// <summary>
        /// x coordinate of the query point
        /// </summary>
        public fixed float x[8];
        /// <summary>
        /// y coordinate of the query point
        /// </summary>
        public fixed float y[8];
        /// <summary>
        /// z coordinate of the query point
        /// </summary>
        public fixed float z[8];
        /// <summary>
        /// time of the point query
        /// </summary>
        public fixed float time[8];
        /// <summary>
        /// radius of the point query 
        /// </summary>
        public fixed float radius[8];
    }
}
