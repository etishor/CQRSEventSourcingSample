using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using NanoMessageBus.Wireup;
using Sample.AppService;
using Sample.Messages.Commands;
using NanoMessageBus;
using CommonDomain.Persistence;
using NanoMessageBus.Core;

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
                        .JsonSerializer()
                        .CompressMessages();

            wireup = wireup.Configure<EndpointWireup>()
                        .ListenOn("msmq://./Sample.AppService")
                        .ForwardPoisonMessagesTo("msmq://./Sample.Error")
                        .RetryAtLeast(3.Times());

            wireup = wireup.Configure<SubscriptionStorageWireup>()
                        .ConnectTo("SubscriptionStorage");

            wireup = wireup.Configure<MessageBusWireup>()
                        .RegisterMessageEndpoint("msmq://./Sample.AppService", typeof(CreatePerson))
                        .RegisterMessageTimeToLive(TimeSpan.MaxValue, typeof(CreatePerson))
                        .RegisterTransientMessage(typeof(CreatePerson))

                        .RegisterMessageEndpoint("msmq://./Sample.AppService", typeof(MovePerson))
                        .RegisterMessageTimeToLive(TimeSpan.MaxValue, typeof(MovePerson))
                        .RegisterTransientMessage(typeof(MovePerson));

            builder.RegisterType<CreatePersonCommandHandler>();
            builder.RegisterType<MovePersonCommandHandler>();
            builder.RegisterType<KillPersonCommandHandler>();

            wireup = wireup.Configure<MessageHandlerWireup>()
                        .AddHandler(c => c.Resolve<CreatePersonCommandHandler>())
                        .AddHandler(c => c.Resolve<MovePersonCommandHandler>())
                        .AddHandler(c => c.Resolve<KillPersonCommandHandler>());

            wireup.Register(builder);

            builder.RegisterType<TransactionScopeUnitOfWork>()
                .As<IHandleUnitOfWork>()
                .InstancePerLifetimeScope()
                .ExternallyOwned();

        }
    }
}
