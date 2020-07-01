using System;

namespace CommandCore.Library.Attributes
{
    public class VerbNameAttribute: Attribute
    {
        public string Name { get; }

        public VerbNameAttribute(string name)
        {
            Name = name;
        }
    }
}