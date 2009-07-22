namespace NHibData.Configs
{
    public interface INHibernateConfig
    {
        string DatabaseName { get; }
        string Server { get; }
    }
}