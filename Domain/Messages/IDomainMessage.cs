using System;

namespace Domain.Messages
{
    public interface IDomainMessage
    {
        Guid Id { get;  }
    }
}