using Autofac;
using Autofac.Integration.Mvc;
using Raven.Client;
using Raven.Client.Document;
using StorageAccess;
using StorageAccess.Raven;

namespace Sample.Client.Web
{
    public class RavenConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            IDocumentStore store = new DocumentStore() { ConnectionStringName = "ReadModel" };
            store.Initialize();
            builder.RegisterInstance(store);

            builder.Register(c => store.OpenSession())
                .As<IDocumentSession>()
                .InstancePerLifetimeScope();

            TypeWithIdStrategy.TypeSeparator = "-";
            IIdStrategy strategy = new TypeWithIdStrategy();

            // IQueryStorage registration
            builder.RegisterType<RavenStorage>().As<IQueryStorage>()
                .OnActivated(r => r.Instance.IdStrategy = strategy)
                .InstancePerHttpRequest();

        }
    }
}