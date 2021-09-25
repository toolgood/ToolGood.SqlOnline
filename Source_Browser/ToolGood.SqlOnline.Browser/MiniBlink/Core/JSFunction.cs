using System;

namespace MiniBlinkPinvoke
{
    /// <summary>
    /// 只有标记为此属性才能配合 BlinkBrowser 里的GlobalObjectJs使用。
    /// </summary>
    public class JSFunc : Attribute
    {
        public string Name { get; private set; }
        public JSFunc() { }
        public JSFunc(string name)
        {
            Name = name;
        }
    }
}
