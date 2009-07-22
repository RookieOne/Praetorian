using ExpenseShared.ReadDtos;
using FluentNHibernate.Mapping;

namespace NHibExpense.Mappings
{
    public class ExpenseDtoMapping : ClassMap<ExpenseDto>
    {
        public ExpenseDtoMapping()
        {
            WithTable("Praetorian.ExpensesTable");

            Id(e => e.Id);
            Map(e => e.Title);
            Map(e => e.ApprovalDate);
        }
    }
}