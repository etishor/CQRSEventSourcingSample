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
using Sample.Denormalizer.People;
using Sample.Messages.Events.People;
using Sample.Messages.Events.Funds;
using Sample.Denormalizer.Funds;

namespace Sample.DenormalizerHost
{
    class BusConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            IWireup wireup = new WireupModule();

            wireup = wireup.Configure<LoggingWireup>()
                    .UseLog4Net();

            wireup = wireup.Configure<TransportWireup>()
                        .ReceiveWith(1.Threads());

            wireup = wireup.Configure<SerializationWireup>()
                        .WithJsonSerializer()
                            .CompressMessages();

            wireup = wireup.Configure<EndpointWireup>()
                        .ListenOn("msmq://./Sample.Denormalizer")
                        .ForwardPoisonMessagesTo("msmq://./Sample.Error")
                        .RetryAtLeast(3.Times());

            wireup = wireup.Configure<MessageSubscriberWireup>()
                        .AddSubscription("msmq://./Sample.AppService",
                        typeof(PersonCreated), typeof(PersonMoved), typeof(PersonDied),
                        typeof(DocumentCreated), typeof(ShareClassCreated), typeof(DocumentAssociatedWithShareclass));

            builder.RegisterType<PersonUpdater>();
            builder.RegisterType<AddressChangesUpdater>();

            builder.RegisterType<DocumentUpdater>();
            builder.RegisterType<ShareClassUpdater>();

            wireup = wireup.Configure<MessageHandlerWireup>()
                 .AddHandler<PersonCreated>(c => c.Resolve<PersonUpdater>())
                 .AddHandler<PersonMoved>(c => c.Resolve<PersonUpdater>())
                 .AddHandler<PersonDied>(c => c.Resolve<PersonUpdater>())
                 .AddHandler<PersonMoved>(c => c.Resolve<AddressChangesUpdater>())
                 .AddHandler<DocumentCreated>(c => c.Resolve<DocumentUpdater>())
                 .AddHandler<DocumentAssociatedWithShareclass>(c => c.Resolve<DocumentUpdater>())
                 .AddHandler<ShareClassCreated>(c => c.Resolve<ShareClassUpdater>());


            wireup.Register(builder);
        }
    }
}
