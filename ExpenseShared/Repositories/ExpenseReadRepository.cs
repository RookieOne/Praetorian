using System;
using System.Collections.Generic;
using ExpenseShared.ReadDtos;
using Foundation.DDD.Interfaces;

namespace ExpenseShared.Repositories
{
    public class ExpenseReadRepository
    {
        public IReadRepositoryStrategy Database { get; set; }

        public ExpenseReadRepository(IReadRepositoryStrategy database)
        {
            Database = database;
        }

        public IEnumerable<ExpenseDto> GetAllExpenses()
        {
            return Database.GetAll<ExpenseDto>();
        }

        public ExpenseDto GetById(Guid id)
        {
            return Database.GetById<ExpenseDto>(id);
        }
    }
}