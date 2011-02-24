using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using NHibernate;
using StorageAccess.NHibernate;
using StorageAccess;
using NHibernate.ByteCode.LinFu;
using Sample.ReadModel;
using NHibernate.Transaction;

namespace Sample.DenormalizerHost
{
    public class StorageConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<NHibernateStorage>().As<IUpdateStorage>()
               .InstancePerDependency();

            Configuration config = BuildConfiguration();
            ISessionFactory factory = config.BuildSessionFactory();
            builder.RegisterInstance(factory);

            builder.Register(c => factory.OpenSession())
                .As<ISession>()
                .InstancePerDependency()
                .OnActivating(c => c.Instance.BeginTransaction())
                .OnRelease(s => s.Transaction.Commit());
        }

        private static Configuration BuildConfiguration()
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
