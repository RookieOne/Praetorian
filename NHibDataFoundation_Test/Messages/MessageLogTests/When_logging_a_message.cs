using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibDataFoundation.Events;
using NHibDataFoundation.Messages;
using NHibDataFoundation_Test.Contexts;
using NHibDataFoundation_Test.Mocks;
using NHibernate.Linq;
using NUnit.Framework;

namespace NHibDataFoundation_Test.Messages.MessageLogTests
{
    [TestFixture]
    public class When_logging_a_message : NHibMessageContext
    {
        private MockMessage _message;

        protected override void When()
        {
            base.When();

            _message = new MockMessage();
            _messageLog.Log(_message);
        }

        [Test]
        public void Should_save_message_in_database()
        {
            AssertMessageInDatabase(_message);
        }

        [Test]
        public void Should_save_message_as_serialized_xml()
        {            
            var savedMessage = GetMessageInDatabaseAs(_message);

            Assert.AreEqual(_message.Id, savedMessage.Id);
            Assert.AreEqual(_message.DateSent, savedMessage.DateSent);
            Assert.AreEqual(_message.DateRecieved, savedMessage.DateRecieved);
        }
    }
}
