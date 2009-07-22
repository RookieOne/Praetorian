using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibDataFoundation.Events;
using NHibDataFoundation.Helpers;
using NHibDataFoundation_Test.Contexts;
using NHibDataFoundation_Test.Mocks;
using NHibernate.Linq;
using NUnit.Framework;

namespace NHibDataFoundation_Test.Events.EventRepositoryTests
{
    [TestFixture]
    public class When_recording_an_aggregate_event : NHibEventContext
    {
        private MockAggregateEvent _event;

        protected override void When()
        {
            base.When();

            _event = new MockAggregateEvent().SetAggregateId(Guid.NewGuid());

            _eventRepository.Record(_event);
        }

        [Test]
        public void Should_save_event_in_database()
        {            
            AssertEventInDatabase(_event);
        }

        [Test]
        public void Should_save_event_as_serialized_xml()
        {            
            var savedEvent = GetEventInDatabaseAs(_event);

            Assert.AreEqual(_event.Id, savedEvent.Id);
            Assert.AreEqual(_event.EventDate, savedEvent.EventDate);
        }

        [Test]
        public void Should_save_event_and_aggregate_relation()
        {
            var session = _config.GetSession();

            var events = from e in session.Linq<NHibAggregateEvent>()
                         where e.EventId == _event.Id
                         select e;
            var nhibAggregateEvent = events.FirstOrDefault();

            Assert.AreEqual(_event.Id, nhibAggregateEvent.EventId);
            Assert.AreEqual(_event.AggregateId, nhibAggregateEvent.AggregateId);
        }
    }
}
