using System;

namespace UnityFoundation
{
    public class UnityRegisterAttribute : Attribute
    {
        public Type Type { get; set; }

        public UnityRegisterAttribute(Type type)
        {
            Type = type;
        }
    }
}