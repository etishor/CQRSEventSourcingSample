using System;
using Autofac;
using NanoMessageBus.Transports;
using System.IO;

namespace Sample.AppServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch( new FileInfo(@"../../../log4net.xml"));

            var builder = new ContainerBuilder();
            // configure NanoMessageBus
            builder.RegisterModule(new BusConfigModule());
            // configure the EventStore
            builder.RegisterModule(new StorageConfigModule());
                       
            using (var container = builder.Build())
            {                
                // start waiting for commands.
                Console.WriteLine("AppService Listening...");
                var transport = container.Resolve<ITransportMessages>();
                transport.StartListening();

                Console.WriteLine("Waiting...");
                Console.ReadKey();
                transport.StopListening();
                Console.WriteLine("Stopping...");
            }
        }
    }
}
