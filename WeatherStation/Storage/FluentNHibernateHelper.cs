using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;

namespace WeatherStation.Storage
{
    public static class FluentNHibernateHelper

    {
        public static void CreateDatabase()
        {
            var configuration = CreateConfiguration();
            configuration.ExposeConfiguration(c => new SchemaExport(c).Execute(true, true, false)).BuildConfiguration();
        }

        public static ISession OpenSession()
        {
            return Fluently.Configure().Database(SQLiteConfiguration.Standard.ConnectionString(c => c.Is("Data Source=WeatherStation.db")))
                .Mappings(x => x.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
                .BuildSessionFactory()
                .OpenSession();
        }

        public static FluentConfiguration CreateConfiguration()
        {
            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.ConnectionString(c => c.Is("Data Source=WeatherStation.db")))
                .Mappings(x => x.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()));
        }
    }
}