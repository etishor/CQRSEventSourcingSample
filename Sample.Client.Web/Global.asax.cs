using System.IO;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.Web;

namespace Sample.Client.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication, IContainerProviderAccessor
    {
        // Provider that holds the application container.
        static IContainerProvider containerProvider;

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}.aspx/{action}/{id}", // URL with parameters
                new { controller = "Person", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.ConfigureAndWatch(new FileInfo(@"../../../log4net.xml"));

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            ContainerBuilder builder = new ContainerBuilder();
            
            builder.RegisterModule(new BusConfigModule());
            builder.RegisterModule(new StorageConfigModule());
            
            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            containerProvider = new ContainerProvider(builder.Build());

            DependencyResolver.SetResolver(new AutofacDependencyResolver(containerProvider.ApplicationContainer));
        }

        // Instance property that will be used by Autofac HttpModules
        // to resolve and inject dependencies.
        public IContainerProvider ContainerProvider
        {
            get { return containerProvider; }
        }
    }
}