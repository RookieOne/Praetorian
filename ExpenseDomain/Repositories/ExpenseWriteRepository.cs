using ExpenseShared.Events;
using ExpenseShared.ReadDtos;
using Foundation.DDD;
using Foundation.DDD.Interfaces;
using Foundation.Events;

namespace ExpenseDomain.Repositories
{
    public class ExpenseWriteRepository : IWriteRepository,
                                          IHandle<ExpenseCreatedEvent>,
                                          IHandle<ExpenseApprovedEvent>,
                                          IHandle<TitleChangedOnExpenseEvent>
    {
        public IWriteRepositoryStrategy Database { get; set; }

        public ExpenseWriteRepository(IWriteRepositoryStrategy database)
        {
            Database = database;
        }

        public void Handle(ExpenseCreatedEvent domainEvent)
        {
            var newExpense = new ExpenseDto
                                 {
                                     Id = domainEvent.AggregateId,
                                     Title = domainEvent.Title
                                 };
            Database.Insert(newExpense);
            Database.Commit();
        }

        public void Handle(ExpenseApprovedEvent domainEvent)
        {
            var expense = Database.GetById<ExpenseDto>(domainEvent.AggregateId);
            expense.ApprovalDate = domainEvent.ApprovalDate;

            Database.Save(expense);
            Database.Commit();
        }

        public void Handle(TitleChangedOnExpenseEvent domainEvent)
        {
            var expense = Database.GetById<ExpenseDto>(domainEvent.AggregateId);
            expense.Title = domainEvent.Title;

            Database.Save(expense);
            Database.Commit();
        }
    }
}