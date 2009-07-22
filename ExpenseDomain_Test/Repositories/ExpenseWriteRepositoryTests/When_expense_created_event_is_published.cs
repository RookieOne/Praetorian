using System;
using ExpenseDomain_Test.Contexts;
using ExpenseShared.Events;
using ExpenseShared.ReadDtos;
using NUnit.Framework;

namespace ExpenseDomain_Test.Repositories.ExpenseWriteRepositoryTests
{
    [TestFixture]
    public class When_expense_created_event_is_published : WriteRepositoriesContext
    {
        private ExpenseCreatedEvent _event;

        protected override void When()
        {
            base.When();

            _event = ExpenseCreatedEvent.New()
                .For(Guid.NewGuid())
                .TitleSetTo("Test")
                .Publish();
        }

        [Test]
        public void should_add_expense_to_database()
        {
            var expense = _database.GetById<ExpenseDto>(_event.AggregateId);
            Assert.IsNotNull(expense);
        }

        [Test]
        public void should_set_title()
        {
            var expense = _database.GetById<ExpenseDto>(_event.AggregateId);
            Assert.AreEqual(_event.Title, expense.Title);
        }
    }
}