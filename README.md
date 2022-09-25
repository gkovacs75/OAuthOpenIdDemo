# OAuth/OpenId Connect Demo

### This is a demonstration of taking a .NET 4.7 MVC application and adding the following:

- Adding dependency injection using Owin
- Creating the Startup.cs file
- Adding IAppBuilder and IServiceCollection
- Oauth/OpenID Connect

1. Create a .NET Framework app targeting 4.7
2. Add a reference to Microsoft.Extensions.DependencyInjection
3. Add a reference to Microsoft.Extensions.DependencyInjection.Abstractions
4. Add a reference to Owin
5. Add a reference to Microsoft.Owin.Host.SystemWeb
6. Right click project | add new item | OwinStartup Class
7. Add the following 2 classes:


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

