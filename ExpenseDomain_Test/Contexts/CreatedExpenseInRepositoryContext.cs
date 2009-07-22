using System;
using ExpenseShared.ReadDtos;

namespace ExpenseDomain_Test.Contexts
{
    public class CreatedExpenseInRepositoryContext : WriteRepositoriesContext
    {
        protected ExpenseDto _expense;

        protected override void Given()
        {
            base.Given();

            _expense = new ExpenseDto
                           {
                               Id = Guid.NewGuid(),                               
                               ApprovalDate = null,
                               Title = "Test"
                           };
            _database.Save(_expense);
            _database.Commit();
        }
    }
}