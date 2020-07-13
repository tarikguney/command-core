using System;

namespace CommandCore.Library.Attributes
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class OptionNameAttribute : Attribute
    {
        public string Name { get; }
        public string Alias { get; set; }
        public string Description { get; set; }

        public OptionNameAttribute(string name)
        {
            Name = name;
        }
    }
}