using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation.Messages;
using Foundation.Messages.Interfaces;
using NHibDataFoundation.Helpers;
using NHibDataFoundation.Messages;
using NHibernate.Linq;
using NUnit.Framework;

namespace NHibDataFoundation_Test.Contexts
{
    public class NHibMessageContext : NHibContext
    {
        protected NHibMessageLog _messageLog;

        protected override void Given()
        {
            base.Given();

            _messageLog = new NHibMessageLog(_config);
        }

        protected void AssertMessageInDatabase<T>(T message) where T : IDomainMessage
        {
            var nhibMessage = GetMessageInDatabase(message);
            Assert.IsNotNull(nhibMessage);
        }

        protected NHibMessage GetMessageInDatabase<T>(T message) where T : IDomainMessage
        {
            var session = _config.GetSession();

            var messages = from m in session.Linq<NHibMessage>()
                         where m.Id == message.Id
                         select m;
            return messages.FirstOrDefault();
        }

        protected T GetMessageInDatabaseAs<T>(T message) where T : IDomainMessage
        {
            var nhibMessage = GetMessageInDatabase(message);

            return XmlSerializationHelper.Deserialize<T>(nhibMessage.Message);
        }
    }
}
