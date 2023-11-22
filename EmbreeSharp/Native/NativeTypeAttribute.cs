using System;

namespace EmbreeSharp.Native
{
    /// <summary>
    /// 标记一下CLR类型对应的Native类型, 没有实际作用
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.ReturnValue)]
    public class NativeTypeAttribute : Attribute
    {
        public string TypeName { get; }

        public NativeTypeAttribute(string typeName)
        {
            TypeName = typeName;
        }
    }
}
