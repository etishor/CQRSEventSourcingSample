using Autofac;
using Autofac.Integration.Mvc;
using NHibernate;
using NHibernate.ByteCode.LinFu;
using NHibernate.Cfg;
using NHibernate.Cfg.Loquacious;
using NHibernate.Tool.hbm2ddl;
using Sample.ReadModel;
using StorageAccess;
using StorageAccess.NHibernate;

namespace Sample.Client.Web
{
    public class StorageConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // IQueryStorage registration
            builder.RegisterType<NHibernateStatelessQueryStorage>().As<IQueryStorage>()
                           .InstancePerHttpRequest();

            // configure and register nhibernate
            Configuration config = BuildConfiguration();

            // keep the read model schema up 2 date - don't do this in production
            SchemaUpdate updater = new SchemaUpdate(config);
            updater.Execute(true, true);

            ISessionFactory factory = config.BuildSessionFactory();
            builder.RegisterInstance(factory);

            builder.Register(c => factory.OpenStatelessSession())
                .As<IStatelessSession>()
                .InstancePerHttpRequest();
        }
        
        private Configuration BuildConfiguration()
        {
            Configuration cfg = new Configuration();
            cfg.SessionFactoryName("Sample.ReadModel");

            cfg.Proxy(p => p.ProxyFactoryFactory<ProxyFactoryFactory>());
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

            cfg.AddAssembly(typeof(Person).Assembly);

            return cfg;
        }
    }
}