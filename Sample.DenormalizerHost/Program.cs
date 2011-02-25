using System;
using Autofac;
using NanoMessageBus.Transports;
using System.IO;

namespace Sample.DenormalizerHost
{
    class Program
    {
        static void Main(string[] args)
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(@"../../../log4net.xml"));

            var builder = new ContainerBuilder();
            builder.RegisterModule(new BusConfigModule());
            builder.RegisterModule(new StorageConfigModule());

            using (var container = builder.Build())
            {
                Console.WriteLine("Denormalizer Listening...");
                var transport = container.Resolve<ITransportMessages>();
                transport.StartListening();

                Console.WriteLine("Waiting...");
                Console.ReadKey();
                Console.WriteLine("Stopping...");
                transport.StopListening();
            }
        }
         
    }
}
