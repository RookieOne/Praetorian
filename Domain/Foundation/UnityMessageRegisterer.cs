using System;
using System.Reflection;
using Domain.Messages;
using Microsoft.Practices.Unity;

namespace Domain.Foundation
{
    public class UnityMessageRegisterer
    {
        private readonly Assembly _assembly;

        public UnityMessageRegisterer(Assembly assembly)
        {
            _assembly = assembly;
        }

        public void Register(IUnityContainer container)
        {
            Type[] types = _assembly.GetTypes();

            foreach (Type type in types)
            {
                Type[] interfaces = type.GetInterfaces();

                foreach (Type i in interfaces)
                {
                    if (i.IsGenericType)
                    {
                        var c = i.GetGenericArguments();

                        var gType = c[0];

                        if (gType.GetInterface("IDomainMessage") != null)
                        {
                            string messageName = gType.Name;
                            container.RegisterType(i, type, messageName);
                        }
                    }
                }
            }
        }
    }
}