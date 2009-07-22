using System;
using System.Linq;
using NHibDataFoundation.Events;
using NHibDataFoundation.Helpers;
using NHibDataFoundation_Test.Contexts;
using NHibDataFoundation_Test.Mocks;
using NHibernate.Linq;
using NUnit.Framework;

namespace NHibDataFoundation_Test.Events.EventRepositoryTests
{
    [TestFixture]
    public class When_recording_an_event : NHibEventContext
    {
        private MockDomainEvent _event;

        protected override void When()
        {
            base.When();

            _event = new MockDomainEvent();
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
    }
}