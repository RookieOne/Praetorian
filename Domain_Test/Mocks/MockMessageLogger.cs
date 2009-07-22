using System.Collections.Generic;
using Domain.Messages;

namespace Domain_Test.Mocks
{
    public class MockMessageLogger : IMessageLogger
    {
        public List<IDomainMessage> MessageLog { get; set; }

        public MockMessageLogger()
        {
            MessageLog = new List<IDomainMessage>();
        }

        public void Log(IDomainMessage message)
        {
            MessageLog.Add(message);
        }
    }
}