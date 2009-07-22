using ExpenseDomain_Test.Contexts;
using ExpenseShared.Events;
using Foundation.Builders;
using NUnit.Framework;
using ExpenseShared.Messages;

namespace ExpenseDomain_Test.Aggregates.ExpenseTests
{
    [TestFixture]
    public class When_changing_title_on_expense : CreatedExpenseContext
    {
        private string _title;

        protected override void When()
        {
            base.When();

            _title = "New Title";

            New.Message()
                .ChangeTitleOn(_expenseId)
                .To(_title)
                .Send();
        }

        [Test]
        public void Should_have_one_approved_event()
        {
            Assert.AreEqual(1, _eventRepository.EventCount<TitleChangedOnExpenseEvent>());
        }

        [Test]
        public void Should_set_title_on_event()
        {
            var approvedEvents = _eventRepository.GetEvent<TitleChangedOnExpenseEvent>();

            Assert.AreEqual(_title, approvedEvents.Title);
        }
    }
}