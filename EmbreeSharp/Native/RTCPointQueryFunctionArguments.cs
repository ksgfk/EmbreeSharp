using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential, Size = 48)]
    public unsafe struct RTCPointQueryFunctionArguments
    {
        public const int Alignment = 16;

        /// <summary>
        /// The (world space) query object that was passed as an argument of rtcPointQuery. The
        /// radius of the query can be decreased inside the callback to shrink the
        /// search domain. Increasing the radius or modifying the time or position of
        /// the query results in undefined behaviour.
        /// </summary>
        public RTCPointQuery* query;
        /// <summary>
        /// Used for user input/output data. Will not be read or modified internally.
        /// </summary>
        public void* userPtr;
        /// <summary>
        /// primitive ID of primitive
        /// </summary>
        public uint primID;
        /// <summary>
        /// geometry ID of primitive
        /// </summary>
        public uint geomID;
        /// <summary>
        /// the context with transformation and instance ID stack
        /// </summary>
        public RTCPointQueryContext* context;
        /// <summary>
        /// If the current instance transform M (= context->world2inst[context->instStackSize]) 
        /// is a similarity matrix, i.e there is a constant factor similarityScale such that
        /// for all x,y: dist(Mx, My) = similarityScale * dist(x, y),
        /// The similarity scale is 0, if the current instance transform is not a
        /// similarity transform and vice versa. The similarity scale allows to compute
        /// distance information in instance space and scale the distances into world
        /// space by dividing with the similarity scale, for example, to update the
        /// query radius. If the current instance transform is not a similarity
        /// transform (similarityScale = 0), the distance computation has to be
        /// performed in world space to ensure correctness. if there is no instance
        /// transform (context->instStackSize == 0), the similarity scale is 1.
        /// </summary>
        public float similarityScale;
    }
}
