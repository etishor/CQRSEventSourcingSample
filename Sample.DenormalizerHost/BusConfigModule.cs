using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using NanoMessageBus.Wireup;
using Sample.Messages.Events;
using Sample.Denormalizer;
using NanoMessageBus;
using Sample.Messages.Commands;
using Sample.Messages;

namespace Sample.DenormalizerHost
{
    class BusConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            IWireup wireup = new WireupModule();

            wireup = wireup.Configure<LoggingWireup>()
                        .UseOutputWindow()
                        .UseConsoleWindow();

            wireup = wireup.Configure<TransportWireup>()
                        .ReceiveWith(1.Threads());

            wireup = wireup.Configure<SerializationWireup>()
                        .JsonSerializer()
                        .CompressMessages();

            wireup = wireup.Configure<EndpointWireup>()
                        .ListenOn("msmq://./Sample.Denormalizer")
                        .ForwardPoisonMessagesTo("msmq://./Sample.Error")
                        .RetryAtLeast(3.Times());

            wireup = wireup.Configure<MessageSubscriberWireup>()
                        .AddSubscription("msmq://./Sample.AppService", typeof(PersonCreated))
                        .AddSubscription("msmq://./Sample.AppService", typeof(PersonMoved));

            wireup = wireup.Configure<MessageBusWireup>()
                        .RegisterMessageEndpoint("msmq://./Sample.AppService", typeof(CreatePerson))
                        .RegisterMessageTimeToLive(TimeSpan.MaxValue, typeof(CreatePerson))
                        .RegisterTransientMessage(typeof(CreatePerson))

                        .RegisterMessageEndpoint("msmq://./Sample.AppService", typeof(MovePerson))
                        .RegisterMessageTimeToLive(TimeSpan.MaxValue, typeof(MovePerson))
                        .RegisterTransientMessage(typeof(MovePerson));

            builder.RegisterType<PersonEventsHandler>()
                .As<IHandleMessages<PersonCreated>>()
                .As<IHandleMessages<PersonMoved>>()
                .As<IHandleMessages<IEvent>>();

            wireup = wireup.Configure<MessageHandlerWireup>()
                 .AddHandler(c => c.Resolve<IHandleMessages<PersonCreated>>())
                 .AddHandler(c => c.Resolve<IHandleMessages<PersonMoved>>())
                 .AddHandler(c => c.Resolve<IHandleMessages<IEvent>>());

            wireup.Register(builder);
        }
    }
}
