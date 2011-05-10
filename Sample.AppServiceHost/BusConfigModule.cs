using System;
using Autofac;
using NanoMessageBus.Core;
using NanoMessageBus.SubscriptionStorage;
using NanoMessageBus.SubscriptionStorage.Raven;
using NanoMessageBus.Wireup;
using NanoMessageBus;
using Raven.Client;
using Raven.Client.Document;
using Sample.AppService.Funds;
using Sample.AppService.People;
using Sample.Messages.Commands.Funds;
using Sample.Messages.Commands.People;

namespace Sample.AppServiceHost
{
    class BusConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            IWireup wireup = new WireupModule();

            wireup = wireup.Configure<LoggingWireup>()
                        .UseLog4Net();

            wireup = wireup.Configure<TransportWireup>()
                        .ReceiveWith(1.Threads()); // number of threads to use for handlers

            wireup = wireup.Configure<SerializationWireup>()
                        .WithJsonSerializer()
                            .CompressMessages();

            wireup = wireup.Configure<EndpointWireup>()
                        .ListenOn("msmq://./Sample.AppService")
                        .ForwardPoisonMessagesTo("msmq://./Sample.Error")
                        .RetryAtLeast(3.Times());

            IDocumentStore store = new DocumentStore() { ConnectionStringName = "SubscriptionStorage" };
            store.Initialize();
            // maybe register the store in the container?
            
            RavenSubscriptionStorage subscriptionStorage = new RavenSubscriptionStorage(store);

            builder.RegisterInstance<RavenSubscriptionStorage>(subscriptionStorage)
                .As<IStoreSubscriptions>()
                .SingleInstance()
                .ExternallyOwned();

            wireup = wireup.Configure<SubscriptionStorageWireup>()
                    .WithCustomSubscriptionStorage(subscriptionStorage);
            //            .ConnectTo("SubscriptionStorage");

            Type[] commandTypes = new Type[] {
                typeof(CreatePerson),
                typeof(MovePerson),
                typeof(KillPerson),
                typeof(CreateDocument),
                typeof(CreateShareClass),
                typeof(AssociateShareClassToDocument)
            };

            wireup = wireup.Configure<MessageBusWireup>()
                        .RegisterMessageEndpoint("msmq://./Sample.AppService", commandTypes)
                        .RegisterMessageTimeToLive(TimeSpan.MaxValue, commandTypes)
                        .RegisterTransientMessage(commandTypes);

            builder.RegisterType<CreatePersonCommandHandler>();
            builder.RegisterType<MovePersonCommandHandler>();
            builder.RegisterType<KillPersonCommandHandler>();

            builder.RegisterType<CreateDocumentCommandHandler>();
            builder.RegisterType<CreateShareClassCommandHandler>();
            builder.RegisterType<AssociateShareClassToDocumentCommandHandler>();


            wireup = wireup.Configure<MessageHandlerWireup>()
                        .AddHandler(c => c.Resolve<CreatePersonCommandHandler>())
                        .AddHandler(c => c.Resolve<MovePersonCommandHandler>())
                        .AddHandler(c => c.Resolve<KillPersonCommandHandler>())
                        .AddHandler(c => c.Resolve<CreateDocumentCommandHandler>())
                        .AddHandler(c => c.Resolve<CreateShareClassCommandHandler>())
                        .AddHandler(c => c.Resolve<AssociateShareClassToDocumentCommandHandler>());

            wireup.Register(builder);

            builder.RegisterType<TransactionScopeUnitOfWork>()
                .As<IHandleUnitOfWork>()
                .InstancePerLifetimeScope()
                .ExternallyOwned();

        }
    }
}
