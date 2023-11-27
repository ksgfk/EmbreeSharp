using System;

namespace EmbreeSharp.Native
{
    /// <summary>
    /// 这个特性只用于标记函数参数是可选的, 并没有实际作用
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class RTCOptionalArgumentAttribute : Attribute { }
}
