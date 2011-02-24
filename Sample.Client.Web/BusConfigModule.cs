using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using NanoMessageBus.Wireup;
using Sample.Messages.Commands;
using NanoMessageBus.Core;
using Autofac.Integration.Mvc;

namespace Sample.Client.Web
{
    public class BusConfigModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            IWireup wireup = new WireupModule();

            wireup = wireup.Configure<LoggingWireup>()
                        .UseOutputWindow();

            wireup = wireup.Configure<TransportWireup>()
                        .ReceiveWith(1.Threads());

            wireup = wireup.Configure<SerializationWireup>()
                        .JsonSerializer()
                        .CompressMessages();

            wireup = wireup.Configure<EndpointWireup>()
                        .ListenOn("msmq://./Sample.Client")
                        .ForwardPoisonMessagesTo("msmq://./Sample.Error")
                        .RetryAtLeast(3.Times());

            wireup = wireup.Configure<MessageBusWireup>()
                        .RegisterMessageEndpoint("msmq://./Sample.AppService", typeof(CreatePerson))
                        .RegisterMessageTimeToLive(TimeSpan.MaxValue, typeof(CreatePerson))
                        .RegisterTransientMessage(typeof(CreatePerson))

                        .RegisterMessageEndpoint("msmq://./Sample.AppService", typeof(MovePerson))
                        .RegisterMessageTimeToLive(TimeSpan.MaxValue, typeof(MovePerson))
                        .RegisterTransientMessage(typeof(MovePerson));

            wireup.Register(builder);


            builder.RegisterType<TransactionScopeUnitOfWork>()
               .As<IHandleUnitOfWork>()
               .OnRelease(u => u.Complete())
               .InstancePerHttpRequest()
               .OwnedByLifetimeScope();
        }
    }
}