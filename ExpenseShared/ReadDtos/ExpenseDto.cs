using System;
using Foundation.DDD.Interfaces;

namespace ExpenseShared.ReadDtos
{
    public class ExpenseDto : IEntity
    {
        public virtual DateTime? ApprovalDate { get; set; }
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }
    }
}