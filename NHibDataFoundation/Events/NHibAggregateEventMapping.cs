using FluentNHibernate.Mapping;

namespace NHibDataFoundation.Events
{
    public class NHibAggregateEventMapping : ClassMap<NHibAggregateEvent>
    {
        public NHibAggregateEventMapping()
        {
            WithTable("Praetorian.AggregateEventsTable");

            Id(e => e.Id);
            Map(e => e.EventId);
            Map(e => e.AggregateId);
        }
    }
}