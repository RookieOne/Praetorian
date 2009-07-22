using ExpenseDomain_Test.Contexts;
using ExpenseShared.Events;
using ExpenseShared.Messages;
using Foundation.Builders;
using NUnit.Framework;

namespace ExpenseDomain_Test.Aggregates.ExpenseTests
{
    [TestFixture]
    public class When_creating_an_expense : ExpenseDomainContext
    {
        private string _title;

        protected override void When()
        {
            base.When();

            _title = "Test";

            New.Message()
                .CreateExpense()
                .WithTitle(_title)
                .Send();
        }

        [Test]
        public void Should_only_have_one_event()
        {
            Assert.AreEqual(1, _eventRepository.EventLog.Count);
        }

        [Test]
        public void Should_create_an_expense()
        {
            Assert.IsInstanceOfType(typeof (ExpenseCreatedEvent), _eventRepository.EventLog[0]);
        }

        [Test]
        public void Should_set_title_on_created_event()
        {
            var expenseCreatedEvent = _eventRepository.EventLog[0] as ExpenseCreatedEvent;
            Assert.AreEqual(_title, expenseCreatedEvent.Title);
        }
    }
}