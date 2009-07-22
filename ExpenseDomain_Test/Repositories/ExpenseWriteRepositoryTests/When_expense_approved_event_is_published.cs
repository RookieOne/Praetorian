using System;
using ExpenseDomain_Test.Contexts;
using ExpenseShared.Events;
using ExpenseShared.ReadDtos;
using Foundation.Events;
using NUnit.Framework;

namespace ExpenseDomain_Test.Repositories.ExpenseWriteRepositoryTests
{
    [TestFixture]
    public class When_expense_approved_event_is_published : CreatedExpenseInRepositoryContext
    {
        private ExpenseApprovedEvent _event;

        protected override void When()
        {
            base.When();

            _event = new ExpenseApprovedEvent(_expense.Id, new DateTime(2008, 5, 3));
            EventProcessor.Publish(_event);
        }

        [Test]
        public void should_set_approval_date()
        {
            var expense = _database.GetById<ExpenseDto>(_event.AggregateId);
            Assert.AreEqual(_event.ApprovalDate, expense.ApprovalDate);
        }

        [Test]
        public void should_update_expense_in_database()
        {
            var expense = _database.GetById<ExpenseDto>(_event.AggregateId);
            Assert.IsNotNull(expense);
        }
    }
}