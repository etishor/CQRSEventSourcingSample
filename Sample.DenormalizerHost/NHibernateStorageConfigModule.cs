using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using NHibernate.Cfg;
using NHibernate;
using Sample.ReadModel.NHibernatePersistence;
using StorageAccess.NHibernate;
using StorageAccess;

namespace Sample.DenormalizerHost
{ 
    public class NHibernateStorageConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            Configuration config = BuildConfiguration();
            ISessionFactory factory = config.BuildSessionFactory();
            builder.RegisterInstance(factory);

            builder.Register(c => factory.OpenSession())
                .As<ISession>()
                .InstancePerDependency()
                .OnActivated(c => c.Instance.BeginTransaction());

            builder.RegisterType<NHibernateStorage>().As<IUpdateStorage>()
               .InstancePerDependency();
        }

        private static Configuration BuildConfiguration()
        {
            Configuration cfg = new Configuration();
            cfg.SessionFactoryName("Sample.ReadModel");

            cfg.DataBaseIntegration(db =>
            {
                db.Dialect<NHibernate.Dialect.MsSql2008Dialect>();
                db.Driver<NHibernate.Driver.SqlClientDriver>();
                db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
                db.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ReadModel"].ConnectionString;
                db.AutoCommentSql = true;
                db.LogSqlInConsole = false;
                db.LogFormatedSql = true;
                db.HqlToSqlSubstitutions = "true 1, false 0, yes 'Y', no 'N'";
            });

            cfg.AddAssembly(typeof(NHibernateMappings).Assembly);

            return cfg;
        }

    }
}
