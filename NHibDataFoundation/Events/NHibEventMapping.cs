using FluentNHibernate.Mapping;

namespace NHibDataFoundation.Events
{
    public class NHibEventMapping : ClassMap<NHibEvent>
    {
        public NHibEventMapping()
        {
            WithTable("Praetorian.EventsTable");

            Id(e => e.Id);
            Map(e => e.Event);
            Map(e => e.EventDate);
        }
    }
}