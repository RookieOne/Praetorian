using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation.Messages;

namespace NHibDataFoundation_Test.Mocks
{
    public class MockMessage : DomainMessage
    {        
        public MockMessage()
        {
            DateSent = DateTime.Today;
            DateRecieved = DateTime.Today;
        }
    }
}
