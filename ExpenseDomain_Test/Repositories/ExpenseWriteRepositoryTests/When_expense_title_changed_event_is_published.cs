using ExpenseDomain_Test.Contexts;
using ExpenseShared.Events;
using ExpenseShared.ReadDtos;
using Foundation.Builders;
using NUnit.Framework;

namespace ExpenseDomain_Test.Repositories.ExpenseWriteRepositoryTests
{
    [TestFixture]
    public class When_expense_title_changed_event_is_published : CreatedExpenseInRepositoryContext
    {
        private TitleChangedOnExpenseEvent _event;
        private string _title;

        protected override void When()
        {
            base.When();

            _title = "New Title";

            _event = New.Event()
                .TitleChangedOn(_expense.Id)
                .To(_title)
                .Publish();
        }

        [Test]
        public void should_set_title()
        {
            var expense = _database.GetById<ExpenseDto>(_event.AggregateId);
            Assert.AreEqual(_event.Title, expense.Title);
        }

        [Test]
        public void should_update_expense_in_database()
        {
            var expense = _database.GetById<ExpenseDto>(_event.AggregateId);
            Assert.IsNotNull(expense);
        }
    }
}