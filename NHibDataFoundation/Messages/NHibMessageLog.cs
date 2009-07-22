using Foundation.Messages.Interfaces;
using NHibDataFoundation.Configs;
using NHibDataFoundation.Helpers;
using NHibernate;

namespace NHibDataFoundation.Messages
{
    public class NHibMessageLog : IMessageLog
    {
        private readonly INHibConfig _config;

        public NHibMessageLog(INHibConfig config)
        {
            _config = config;
        }

        public IDomainMessage Log(IDomainMessage message)
        {
            var nhibMessage = new NHibMessage
                                  {
                                      DateSent = message.DateSent,
                                      DateRecieved = message.DateRecieved,
                                      Message = string.Empty
                                  };

            ISession session = _config.GetSession();

            ITransaction transaction = session.BeginTransaction();

            session.Save(nhibMessage);

            message.Id = nhibMessage.Id;
            nhibMessage.Message = XmlSerializationHelper.Serialize(message);
            session.Save(nhibMessage);

            transaction.Commit();

            return message;
        }
    }
}