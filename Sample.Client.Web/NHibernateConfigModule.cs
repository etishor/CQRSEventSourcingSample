using Autofac;
using Autofac.Integration.Mvc;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;
using Sample.ReadModel.People;
using StorageAccess;
using StorageAccess.NHibernate;

namespace Sample.Client.Web
{
    public class NHibernateConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            // configure and register hibernate
            Configuration config = BuildConfiguration();

            // keep the read model schema up 2 date - don't do this in production
            SchemaUpdate updater = new SchemaUpdate(config);
            updater.Execute(true, true);

            ISessionFactory factory = config.BuildSessionFactory();
            builder.RegisterInstance(factory);
            
            builder.Register(c => factory.OpenSession())
                .As<ISession>()
                .InstancePerHttpRequest();

            // IQueryStorage registration
            builder.RegisterType<NHibernateStorage>().As<IQueryStorage>()
                           .InstancePerHttpRequest();
        }

        private Configuration BuildConfiguration()
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

            cfg.AddAssembly(typeof(Person).Assembly);

            return cfg;
        }
    }
}