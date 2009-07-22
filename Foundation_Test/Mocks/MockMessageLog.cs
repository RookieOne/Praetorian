using System.Collections.Generic;
using Foundation.Messages.Interfaces;

namespace Foundation_Test.Mocks
{
    public class MockMessageLog : IMessageLog
    {
        public List<IDomainMessage> MessageLog { get; set; }

        public MockMessageLog()
        {
            MessageLog = new List<IDomainMessage>();
        }

        public IDomainMessage Log(IDomainMessage message)
        {
            MessageLog.Add(message);
            return message;
        }
    }
}