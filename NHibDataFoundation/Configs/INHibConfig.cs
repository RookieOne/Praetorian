using NHibernate;

namespace NHibDataFoundation.Configs
{
    public interface INHibConfig
    {
        string DatabaseName { get; }
        string Server { get; }
        ISession GetSession();
    }
}