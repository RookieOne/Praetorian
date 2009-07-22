using System;
using ExpenseDomain_Test.Contexts;
using ExpenseShared.Events;
using ExpenseShared.Messages;
using Foundation.Builders;
using NUnit.Framework;

namespace ExpenseDomain_Test.Aggregates.ExpenseTests
{
    [TestFixture]
    public class When_approving_an_expense : CreatedExpenseContext
    {
        private DateTime _approvalDate;

        protected override void When()
        {
            base.When();

            _approvalDate = new DateTime(2000, 1, 1);

            New.Message()
                .ApproveExpense(_expenseId)
                .On(_approvalDate)
                .Send();
        }

        [Test]
        public void Should_have_one_approved_event()
        {
            Assert.AreEqual(1, _eventRepository.EventCount<ExpenseApprovedEvent>());
        }

        [Test]
        public void Should_set_date_on_approved_event()
        {
            var approvedEvents = _eventRepository.GetEvent<ExpenseApprovedEvent>();

            Assert.AreEqual(_approvalDate, approvedEvents.ApprovalDate);
        }
    }
}