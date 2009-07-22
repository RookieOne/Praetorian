using System;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity;

namespace Domain.Foundation
{
    public class UnityAssemblyLoader
    {
        private Assembly Assembly { get; set; }

        public UnityAssemblyLoader(Assembly assembly)
        {
            Assembly = assembly;
        }

        public void Register(IUnityContainer container)
        {
            if (Assembly == null)
                return;

            Type[] types = Assembly.GetTypes();

            for (int t = 0; t < types.Count(); t++)
            {
                Type currentType = types[t];

                if (currentType.IsClass)
                {
                    MemberInfo[] memberInfos = currentType.GetMembers();

                    foreach (MemberInfo memberInfo in memberInfos)
                    {
                        object[] attributes = memberInfo.GetCustomAttributes(typeof (UnityRegisterAttribute), false);

                        if (attributes.Count() > 0)
                        {
                            var attribute = attributes[0] as UnityRegisterAttribute;

                            if (attribute != null)
                            {
                                container.RegisterType(attribute.Type, currentType);
                            }
                        }
                    }
                }
            }
        }
    }
}