using System;
using System.Reflection;
using Microsoft.Practices.Unity;

namespace UnityFoundation
{
    public class UnityMessageRegisterer
    {
        private readonly IUnityContainer _container;

        public UnityMessageRegisterer(IUnityContainer container)
        {
            _container = container;
        }

        public void Register(Assembly assembly)
        {
            Type[] types = assembly.GetTypes();

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
                            _container.RegisterType(i, type, messageName);
                        }
                    }
                }
            }
        }
    }
}