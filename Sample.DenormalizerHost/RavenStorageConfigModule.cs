using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using StorageAccess;
using Sample.ReadModel;
using Sample.ReadModel.People;

using Raven.Client;
using Raven.Client.Document;
using StorageAccess.Raven;

namespace Sample.DenormalizerHost
{
    public class RavenStorageConfigModule : Module
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
            
            builder.RegisterType<RavenStorage>().As<IUpdateStorage>()
                .OnActivated(r => r.Instance.IdStrategy = strategy)
               .InstancePerDependency();
        }
    }
}
