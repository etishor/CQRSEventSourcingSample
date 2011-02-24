using System;
using Autofac;
using NanoMessageBus.Transports;

namespace Sample.AppServiceHost
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
