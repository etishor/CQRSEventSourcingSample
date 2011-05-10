using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using NanoMessageBus.Wireup;
using Sample.Messages.Commands;
using NanoMessageBus.Core;
using NanoMessageBus;
using Autofac.Integration.Mvc;
using Sample.Messages.Commands.People;
using Sample.Messages.Commands.Funds;

namespace Sample.Client.Web
{
    public class BusConfigModule : Module
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
                        .ListenOn("msmq://./Sample.Client")
                        .ForwardPoisonMessagesTo("msmq://./Sample.Error")
                        .RetryAtLeast(3.Times());

            Type[] messageTypes = new Type[] {
                typeof(CreatePerson),
                typeof(MovePerson),
                typeof(KillPerson),
                typeof(CreateDocument),
                typeof(CreateShareClass),
                typeof(AssociateShareClassToDocument)
            };


            wireup = wireup.Configure<MessageBusWireup>()
                        .RegisterMessageEndpoint("msmq://./Sample.AppService", messageTypes )
                        .RegisterMessageTimeToLive(TimeSpan.MaxValue, messageTypes)
                        .RegisterTransientMessage(messageTypes);

            wireup.Register(builder);


            builder.RegisterType<TransactionScopeUnitOfWork>()
               .As<IHandleUnitOfWork>()
               .OnRelease(u => u.Complete())
               .InstancePerHttpRequest()
               .OwnedByLifetimeScope();
        }
    }
}