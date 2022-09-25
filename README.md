# OAuth/OpenId Connect Demo

### 1. Update a .NET 4.7 MVC Application to use Dependency Injection:

- Dependency Injection using Owin
- Startup.cs file
- Adding IAppBuilder and IServiceCollection
- See [this](https://scottdorman.blog/2016/03/17/integrating-asp-net-core-dependency-injection-in-mvc-4/) for details.

1. Create a .NET Framework app targeting 4.7
2. Add a reference to Microsoft.Extensions.DependencyInjection
3. Add a reference to Microsoft.Extensions.DependencyInjection.Abstractions
4. Add a reference to Owin
5. Add a reference to Microsoft.Owin.Host.SystemWeb
6. Right click project | add new item | OwinStartup Class
7. Add the following 2 classes:

ServiceProviderExtensions:

        public static class ServiceProviderExtensions
        {
            public static IServiceCollection AddControllersAsServices(this IServiceCollection services, IEnumerable<Type> controllerTypes)
            {
                foreach (var type in controllerTypes)
                {
                    services.AddTransient(type);
                }

                return services;
            }
        }

DefaultDependencyResolver:

        public class DefaultDependencyResolver : IDependencyResolver
        {
                protected IServiceProvider serviceProvider;

                public DefaultDependencyResolver(IServiceProvider serviceProvider)
                {
                    this.serviceProvider = serviceProvider;
                }

                public object GetService(Type serviceType)
                {
                    return this.serviceProvider.GetService(serviceType);
                }

                public IEnumerable<object> GetServices(Type serviceType)
                {
                    return this.serviceProvider.GetServices(serviceType);
                }
        }


The Startup class will look like this:

        using Microsoft.Extensions.DependencyInjection;
        using Microsoft.Owin;
        using Owin;
        using System;
        using System.Linq;
        using System.Reflection;
        using System.Web.Mvc;

        [assembly: OwinStartupAttribute(typeof(Web.Startup))]
        namespace Web
        {
            public partial class Startup
            {
                public void Configuration(IAppBuilder app)
                {

                    var services = new ServiceCollection();
                    //ConfigureAuth(app);
                    ConfigureServices(services);
                    var resolver = new DefaultDependencyResolver(services.BuildServiceProvider());
                    DependencyResolver.SetResolver(resolver);
                }

                public void ConfigureServices(IServiceCollection services)
                {
                    services.AddControllersAsServices(typeof(Startup).Assembly.GetExportedTypes()
                    .Where(t => !t.IsAbstract && !t.IsGenericTypeDefinition)
                    .Where(t => typeof(IController).IsAssignableFrom(t)
                    || t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)));


                    services.AddTransient<ISomethingToInject, SomethingToInject>();
                }
            }
        }


At this point you will now be able to inject into your contructors as long as you register the interface and concrete implementation:
                
        services.AddTransient<ISomethingToInject, SomethingToInject>();
        ...
        public HomeController(ISomethingToInject somethingToInject)


### 2. Register with GitHub

 - Go to https://github.com/settings/developers to setup a new OAuth app.
 - See [this](https://www.oauth.com/)for details.
