using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using CommonDomain.Core;
using CommonDomain.Persistence.EventStore;
using Sample.AppService;
using CommonDomain;
using CommonDomain.Persistence;
using EventStore;
using EventStore.Persistence;
using EventStore.Persistence.SqlPersistence;
using EventStore.Persistence.SqlPersistence.SqlDialects;
using EventStore.Serialization;
using EventStore.Dispatcher;
using NanoMessageBus.Core;

namespace Sample.AppServiceHost
{
    class StorageConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => BuildEventStore(c.Resolve<ILifetimeScope>()))
                .As<IStoreEvents>()
                .SingleInstance();

            builder.RegisterType<ConflictDetector>().As<IDetectConflicts>();
            builder.RegisterType<EventStoreRepository>().As<IRepository>();
            builder.RegisterType<AggregateFactory>().As<IConstructAggregates>();
        }

        private static IStoreEvents BuildEventStore(ILifetimeScope container)
        {
            return EventStore.Wireup.Init()
                // use RavenDb to store events
                .UsingRavenPersistence("EventStore")

                // OR use SQL to store events 
                //.UsingSqlPersistence("EventStore")
                //    .InitializeStorageEngine()

                // Serialization is only needed for SQL since raven has it's own serializer
                //.UsingJsonSerialization()
                //.Compress()
                
                .UsingAsynchronousDispatcher()
                    .PublishTo(new DelegateMessagePublisher(c => DispatchCommit(container, c)))
                .Build();
        }

        private static void DispatchCommit(ILifetimeScope container, Commit commit)
        {            
            using (var scope = container.BeginLifetimeScope())
            {
                NanoMessageBus.IPublishMessages publisher = scope.Resolve<NanoMessageBus.IPublishMessages>();

                publisher.Publish(commit.Events.Select(e => e.Body).ToArray());

                // need to complete and dispose the uow to do the actual publishing since 
                // the IHandleUnitOfWork is registered as ExternalyOwned
                using (IHandleUnitOfWork uow = scope.Resolve<IHandleUnitOfWork>())
                {
                    uow.Complete();
                }
            }
        }
    }
}
