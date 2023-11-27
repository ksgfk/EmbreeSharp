using System.Runtime.InteropServices;

namespace EmbreeSharp.Native
{
    [RTCAlign(Alignment)]
    [StructLayout(LayoutKind.Sequential, Size = 144)]
    public unsafe struct RTCPointQueryContext
    {
        public const int Alignment = 16;

        /// <summary>
        /// accumulated 4x4 column major matrices from world space to instance space.
        /// undefined if size == 0.
        /// </summary>
        public fixed float world2inst[Embree.RTC_MAX_INSTANCE_LEVEL_COUNT * 16];
        /// <summary>
        /// accumulated 4x4 column major matrices from instance space to world space.
        /// undefined if size == 0.
        /// </summary>
        public fixed float inst2world[Embree.RTC_MAX_INSTANCE_LEVEL_COUNT * 16];
        /// <summary>
        /// instance ids.
        /// </summary>
        public fixed uint instID[Embree.RTC_MAX_INSTANCE_LEVEL_COUNT];
        /// <summary>
        /// instance prim ids.
        /// </summary>
        public fixed uint instPrimID[Embree.RTC_MAX_INSTANCE_LEVEL_COUNT];
        /// <summary>
        /// number of instances currently on the stack.
        /// </summary>
        public uint instStackSize;
    }
}
