using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cat.itb.M6NF2Prac.Connections
{
    public class SessionFactoryStore
    {
        private static readonly string connectionString = "Server = postgresql-santiagovr.alwaysdata.net; port = 5432; Database = santiagovr_m6nf2prac; Username = santiagovr; Password = Chistrees69@";
        public static ISessionFactory CreateSessionFactory()
        {
            var conn = new ConnectionCloud();
            return Fluently.Configure()
                .Database(PostgreSQLConfiguration.PostgreSQL82.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Program>())
                .BuildSessionFactory();
        }

        public static ISession Open()
        {
            ISessionFactory session = CreateSessionFactory();
            return session.OpenSession();
        }
    }
}
