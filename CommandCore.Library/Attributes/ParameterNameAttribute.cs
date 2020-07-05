using System;

namespace CommandCore.Library.Attributes
{
    public class ParameterNameAttribute: Attribute
    {
        public string Name { get; }
        public string Alias { get; set; }

        public ParameterNameAttribute(string name)
        {
            Name = name;
        }
    }
}