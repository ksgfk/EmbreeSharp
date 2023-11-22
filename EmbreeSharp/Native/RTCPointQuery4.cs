using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    /// <summary>
    /// Structure of a packet of 4 query points
    /// </summary>
    [RTCAlign(16)]
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RTCPointQuery4
    {
        /// <summary>
        /// x coordinate of the query point
        /// </summary>
        public fixed float x[4];
        /// <summary>
        /// y coordinate of the query point
        /// </summary>
        public fixed float y[4];
        /// <summary>
        /// z coordinate of the query point
        /// </summary>
        public fixed float z[4];
        /// <summary>
        /// time of the point query
        /// </summary>
        public fixed float time[4];
        /// <summary>
        /// radius of the point query 
        /// </summary>
        public fixed float radius[4];
    }
}
