using System;

namespace ExcelConverter.Components
{
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class EnumTextAttribute : Attribute
    {
        public string Text { get; }
        public EnumTextAttribute(string text)
        {
            Text = text;
        }
    }
}
