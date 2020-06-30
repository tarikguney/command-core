using System;

namespace CommandCore.Library
{
    public class ParameterNameAttribute: Attribute
    {
        public string Name { get; }

        public ParameterNameAttribute(string name)
        {
            Name = name;
        }
    }
}