using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using NHibernate.Cfg;
using NHibernate.ByteCode.LinFu;
using NHibernate.Cfg.Loquacious;
using Sample.ReadModel;
using StorageAccess.NHibernate;
using StorageAccess;
using Autofac.Integration.Mvc;
using NHibernate;
using NHibernate.Transaction;

namespace Sample.Client.Web
{
    public class StorageConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NHibernateStatelessQueryStorage>().As<IQueryStorage>()
                           .InstancePerHttpRequest();
            Configuration config = BuildConfiguration();
            ISessionFactory factory = config.BuildSessionFactory();
            builder.RegisterInstance(factory);

            builder.Register(c => factory.OpenStatelessSession())
                .As<IStatelessSession>()
                .InstancePerLifetimeScope();
        }
        
        private Configuration BuildConfiguration()
        {
            Configuration cfg = new Configuration();
            cfg.SessionFactoryName("Sample.ReadModel");

            cfg.Proxy(p => p.ProxyFactoryFactory<ProxyFactoryFactory>());
            cfg.DataBaseIntegration(db =>
            {
                db.Dialect<NHibernate.Dialect.MySQL5Dialect>();
                db.Driver<NHibernate.Driver.MySqlDataDriver>();
                db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ReadModel"].ConnectionString;
                db.AutoCommentSql = true;
                db.LogSqlInConsole = false;
                db.LogFormatedSql = true;
                db.TransactionFactory<AdoNetTransactionFactory>();
                db.HqlToSqlSubstitutions = "true 1, false 0, yes 'Y', no 'N'";
            });

            cfg.AddAssembly(typeof(Person).Assembly);

            return cfg;
        }
    }
}