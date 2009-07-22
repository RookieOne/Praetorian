using System.Linq;
using Foundation.Events;
using NHibDataFoundation.Events;
using NHibDataFoundation.Helpers;
using NHibernate.Linq;
using NUnit.Framework;

namespace NHibDataFoundation_Test.Contexts
{
    public class NHibEventContext : NHibContext
    {
        protected NHibEventRepository _eventRepository;

        protected override void Given()
        {
            base.Given();

            _eventRepository = new NHibEventRepository(_config);
        }

        protected void AssertEventInDatabase<T>(T domainEvent) where T : IDomainEvent
        {
            var nhibEvent = GetEventInDatabase(domainEvent);
            Assert.IsNotNull(nhibEvent);
        }

        protected NHibEvent GetEventInDatabase<T>(T domainEvent) where T : IDomainEvent
        {
            var session = _config.GetSession();

            var events = from e in session.Linq<NHibEvent>()
                         where e.Id == domainEvent.Id
                         select e;
            return events.FirstOrDefault();
        }

        protected T GetEventInDatabaseAs<T>(T domainEvent) where T : IDomainEvent
        {
            var nhibEvent = GetEventInDatabase(domainEvent);

            return XmlSerializationHelper.Deserialize<T>(nhibEvent.Event);
        }
    }
}