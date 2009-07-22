using System.Reflection;
using Microsoft.Practices.Unity;
using NHibDataFoundation.Configs;
using NHibDataFoundation.Events;

namespace NHibDataFoundation_Test.Contexts
{
    public class NHibContext
    {
        protected IUnityContainer _container;
        protected INHibConfig _config;

        protected NHibContext()
        {
            _container = new UnityContainer();
            _container.RegisterInstance(_container);

            _config = NHibConfig.Create()
                .DatabaseNameIs("Praetorian")
                .ServerIs("Lubu\\SQLEXPRESS2")
                .RegisterMappings(Assembly.GetAssembly(typeof(NHibEventRepository)))
                .Build();                

            Given();
            When();
        }

        protected virtual void Given()
        {
        }

        protected virtual void When()
        {
        }
    }
}