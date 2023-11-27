using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    /// <summary>
    /// Point query structure for closest point query
    /// </summary>
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential, Size = 32)]
    public struct RTCPointQuery
    {
        public const int Alignment = 16;
        
        /// <summary>
        /// x coordinate of the query point
        /// </summary>
        public float x;
        /// <summary>
        /// y coordinate of the query point
        /// </summary>
        public float y;
        /// <summary>
        /// z coordinate of the query point
        /// </summary>
        public float z;
        /// <summary>
        /// time of the point query
        /// </summary>
        public float time;
        /// <summary>
        /// radius of the point query 
        /// </summary>
        public float radius;
    }
}
