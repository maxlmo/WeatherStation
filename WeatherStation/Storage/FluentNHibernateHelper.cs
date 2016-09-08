using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace WeatherStation.Storage
{
    public static class FluentNHibernateHelper

    {
        public static void InitializeDatabase()
        {
            var configuration = CreateConfiguration();
            configuration.ExposeConfiguration(c => new SchemaUpdate(c).Execute(true, true)).BuildConfiguration();
        }

        public static ISession OpenSession()
        {
            return CreateConfiguration().BuildSessionFactory().OpenSession();
        }

        private static FluentConfiguration CreateConfiguration()
        {
            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.ConnectionString(c => c.Is("Data Source=WeatherStation.db")))
                .Mappings(x => x.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}