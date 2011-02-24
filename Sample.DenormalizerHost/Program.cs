using System;
using Autofac;
using NanoMessageBus.Transports;

namespace Sample.DenormalizerHost
{
    class Program
    {
        static void Main(string[] args)
        {
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
            }
        }
         
    }
}
