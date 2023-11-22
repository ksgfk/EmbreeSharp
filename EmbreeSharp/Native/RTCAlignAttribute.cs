using System;

namespace EmbreeSharp.Native
{
    /// <summary>
    /// 这个特性只用于标记结构体内存对齐的量, 并没有实际作用
    /// </summary>
    [AttributeUsage(AttributeTargets.Struct)]
    public class RTCAlignAttribute : Attribute
    {
        /// <summary>
        /// 内存对齐
        /// </summary>
        public int Alignment { get; }

        public RTCAlignAttribute(int alignment)
        {
            Alignment = alignment;
        }
    }
}
