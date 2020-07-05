using System;

namespace CommandCore.Library.Attributes
{
    public class VerbNameAttribute: Attribute
    {
        public string Name { get; }
        public string Description { get; set; }

        public VerbNameAttribute(string name)
        {
            Name = name;
        }
    }
}