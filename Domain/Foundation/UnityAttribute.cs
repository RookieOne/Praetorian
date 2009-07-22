using System;

namespace Domain.Foundation
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