using FluentNHibernate.Mapping;

namespace NHibDataFoundation.Messages
{
    public class NHibMessageMapping : ClassMap<NHibMessage>
    {
        public NHibMessageMapping()
        {
            WithTable("Praetorian.MessagesTable");

            Id(m => m.Id);
            Map(m => m.DateRecieved);
            Map(m => m.DateSent);
            Map(m => m.Message);
        }
    }
}