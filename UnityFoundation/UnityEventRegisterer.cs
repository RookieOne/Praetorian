using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;

namespace UnityFoundation
{
    public class UnityEventRegisterer<T>
    {
        private readonly IUnityContainer _container;

        public UnityEventRegisterer(IUnityContainer container)
        {
            _container = container;
        }

        public void Register(Assembly assembly)
        {
            var types = new List<Type>(assembly.GetTypes());
            types = types.Where(t => t.GetInterface(typeof (T).Name) != null).ToList();

            foreach (var type in types)
            {
                Type[] interfaces = type.GetInterfaces();

                foreach (var i in interfaces)
                {
                    if (i.IsGenericType)
                    {
                        Type[] c = i.GetGenericArguments();

                        Type gType = c[0];

                        if (gType.GetInterface("IDomainEvent") != null)
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