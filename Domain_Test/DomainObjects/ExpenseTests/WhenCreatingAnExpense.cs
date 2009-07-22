using Domain.Events;
using Domain.Messages;
using Domain_Test.Contexts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Domain_Test.DomainObjects.ExpenseTests
{
    [TestClass]
    public class WhenCreatingAnExpense : DomainContext
    {
        private string _title;

        protected override void When()
        {
            _title = "Test";
            var message = new CreateExpenseMessage(_title);
            MessageProcessor.Publish(message);
        }

        [TestMethod]
        public void Should_only_have_one_event()
        {
            Assert.AreEqual(1, _eventRepository.EventLog.Count);
        }

        [TestMethod]
        public void Should_create_an_expense()
        {
            Assert.IsInstanceOfType(_eventRepository.EventLog[0], typeof (ExpenseCreatedEvent));
        }

        [TestMethod]
        public void Should_set_title_on_created_event()
        {
            var expenseCreatedEvent = _eventRepository.EventLog[0] as ExpenseCreatedEvent;
            Assert.AreEqual(_title, expenseCreatedEvent.Title);
        }
    }
}