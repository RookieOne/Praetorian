namespace NHibData.Configs
{
    public class NHibernateConfig : INHibernateConfig
    {
        public string DatabaseName { get; private set; }

        public string Server { get; private set; }

        private NHibernateConfig()
        {
        }

        public static NHibernateConfig Create()
        {
            return new NHibernateConfig();
        }

        public NHibernateConfig ServerIs(string server)
        {
            Server = server;
            return this;
        }

        public NHibernateConfig DatabaseNameIs(string databaseName)
        {
            DatabaseName = databaseName;
            return this;
        }
    }
}