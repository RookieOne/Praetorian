using System;
using Foundation.Events;

namespace NHibDataFoundation_Test.Mocks
{
    public class MockAggregateEvent : AggregateEvent
    {
        public MockAggregateEvent SetAggregateId(Guid id)
        {
            AggregateId = id;
            return this;
        }
    }
}