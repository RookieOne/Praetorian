using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Foundation.Events;
using NHibDataFoundation.Configs;
using NHibDataFoundation.Helpers;
using NHibernate;
using NHibernate.Linq;

namespace NHibDataFoundation.Events
{
    public class NHibEventRepository : IEventRepository
    {
        private readonly INHibConfig _config;

        public NHibEventRepository(INHibConfig config)
        {
            _config = config;
        }

        public IDomainEvent Record(IDomainEvent domainEvent)
        {            
            var nhibEvent = new NHibEvent
                                {                                       
                                    EventDate = DateTime.Now,
                                    Event = string.Empty
                                };

            ISession session = _config.GetSession();
            
            ITransaction transaction = session.BeginTransaction();

            session.Save(nhibEvent);
            
            domainEvent.Id = nhibEvent.Id;
            nhibEvent.Event = XmlSerializationHelper.Serialize(domainEvent);

            session.Save(nhibEvent);

            var aggregateEvent = domainEvent as IAggregateEvent;

            if (aggregateEvent != null)
            {
                var nhibAggregateEvent = new NHibAggregateEvent
                                             {
                                                 EventId = nhibEvent.Id,
                                                 AggregateId = aggregateEvent.AggregateId
                                             };
                session.Save(nhibAggregateEvent);
            }


            transaction.Commit();
            
            return domainEvent;
        }

        public IEnumerable<IAggregateEvent> GetAggregateEvents(Guid id)
        {
            ISession session = _config.GetSession();

            IQueryable<NHibEvent> events = from e in session.Linq<NHibEvent>()
                                           where e.Id == id
                                           select e;

            return events.ToList().Cast<IAggregateEvent>();
        }
    }
}