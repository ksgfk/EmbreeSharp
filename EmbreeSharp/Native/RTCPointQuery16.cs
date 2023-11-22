using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    /// <summary>
    /// Structure of a packet of 16 query points
    /// </summary>
    [RTCAlign(64)]
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct RTCPointQuery16
    {
        /// <summary>
        /// x coordinate of the query point
        /// </summary>
        public fixed float x[16];
        /// <summary>
        /// y coordinate of the query point
        /// </summary>
        public fixed float y[16];
        /// <summary>
        /// z coordinate of the query point
        /// </summary>
        public fixed float z[16];
        /// <summary>
        /// time of the point query
        /// </summary>
        public fixed float time[16];
        /// <summary>
        /// radius of the point query 
        /// </summary>
        public fixed float radius[16];
    }
}
